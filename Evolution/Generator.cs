﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Evolution
{
	/// <summary>
	/// Generatorklasse für evolutionäre Programmierung
	/// </summary>
	public sealed class Generator<T> where T: class
	{
		/// <summary>
		/// Einfacher Zufallszahlengenerator
		/// </summary>
		private readonly Random _simpleRandomGenerator = new Random();

		/// <summary>
		/// Die Funktion, die zum Gewinnen von Zufallswerten verwendet wird
		/// </summary>
		private Func<double> _randomFunction;

		/// <summary>
		/// Die Liste der Entscheidungsfunktionen
		/// </summary>
		private List<MethodInfo> _decisions;

		/// <summary>
		/// Die Liste der Terminals
		/// </summary>
		private List<MethodInfo> _terminals;

		/// <summary>
		/// Bezieht oder setzt die Funktion, die zum Gewinnen von Zufallswerten verwendet wird.
		/// <seealso cref="GetRandomValue()"/>
		/// <seealso cref="GetRandomValue(int,int)"/>
		/// </summary>
		/// <value>Die Zufallsfunktion.</value>
		public Func<double> RandomFunction
		{
			[Pure] get { return _randomFunction; }
			set
			{
				Contract.Requires(value != null);
				_randomFunction = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Generator&lt;T&gt;"/> class.
		/// </summary>
		/// <exception cref="ClassDecorationException">Klasse ist nicht mit dem <see cref="EvolutionaryClassAttribute"/> markiert.</exception>
		/// <exception cref="MethodSignatureException">Eine oder mehrere Methoden, die mit dem <see cref="EvolutionaryMethodAttribute"/> markiert sind, haben eine ungültige Signatur.</exception>
		/// <exception cref="GeneratorException">Es wurden keine Terminalmethoden ermittelt.</exception>
		public Generator()
		{
			DetermineFunctionsAndTerminals();
		}

		#region Zufallszahlen

		/// <summary>
		/// Bezieht eine Zufallszahl im Bereich 0..1
		/// <para>>Ist keine Zufallsfunktion in <see cref="RandomFunction"/> gesetzt, wird die Standardimplementierung verwendet.</para>
		/// </summary>
		/// <returns>Die Zufallszahl.</returns>
		private double GetRandomValue()
		{
			Contract.Ensures(Contract.Result<double>() >= 0 && Contract.Result<double>() <= 1);

			double value = (_randomFunction ?? _simpleRandomGenerator.NextDouble)();
			return Math.Max(0, Math.Min(1, value));
		}

		/// <summary>
		/// Bezieht eine Zufallszahl im Bereich <paramref name="minValue"/>..<paramref name="maxValue"/>
		/// <para>>Ist keine Zufallsfunktion in <see cref="RandomFunction"/> gesetzt, wird die Standardimplementierung verwendet.</para>
		/// <seealso cref="GetRandomValue()"/>
		/// </summary>
		/// <returns>Die Zufallszahl.</returns>
		private int GetRandomValue(int minValue, int maxValue)
		{
			Contract.Requires(minValue <= maxValue);
			Contract.Ensures(Contract.Result<int>() >= minValue && Contract.Result<int>() <= maxValue);

			double randomValue = GetRandomValue();
			return (int)Math.Round(randomValue*(maxValue - minValue) + minValue);
		}

		#endregion Zufallszahlen

		#region Ermitteln der Funktionen und Terminals

		/// <summary>
		/// Baut einen zufälligen Ausdrucksbaum auf
		/// </summary>
		/// <exception cref="ClassDecorationException">Klasse ist nicht mit dem <see cref="EvolutionaryClassAttribute"/> markiert.</exception>
		/// <exception cref="MethodSignatureException">Eine oder mehrere Methoden, die mit dem <see cref="EvolutionaryMethodAttribute"/> markiert sind, haben eine ungültige Signatur.</exception>
		/// <exception cref="GeneratorException">Es wurden keine Terminalmethoden ermittelt.</exception>
		internal void DetermineFunctionsAndTerminals()
		{
			Contract.Ensures(_decisions != null);
			Contract.Ensures(_terminals != null);

			// Affentest
			Type type = typeof(T);
			if (type.GetCustomAttributes(typeof(EvolutionaryClassAttribute), true).Length == 0)
				throw new ClassDecorationException("Klasse ist nicht mit dem EvolutionaryClass-Attribut markiert");

			// Alle Methoden ermitteln
			IEnumerable<MethodInfo> methods =
				from method in type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
				where method.GetCustomAttributes(typeof (EvolutionaryMethodAttribute), true).Length > 0
				select method;

			// Methoden auswerten
			_decisions = new List<MethodInfo>();
			_terminals = new List<MethodInfo>();
			List<string> invalidMethodList = new List<string>();
			foreach (MethodInfo method in methods)
			{
				if (method.GetParameters().Length > 0) invalidMethodList.Add(method.Name + ": Keine Parameter erlaubt.");
				if (method.ReturnType == typeof(void)) _terminals.Add(method);
				else if (method.ReturnType == typeof(bool)) _decisions.Add(method);
				else invalidMethodList.Add(method.Name + ": Nur Rückgabewerte bool und void erlaubt.");
			}

			// Excepticon
			if (invalidMethodList.Count > 0)
			{
				StringBuilder builder = new StringBuilder();
				builder.AppendLine("Eine oder mehrere Methoden des Objektes haben eine ungültige Signatur:");
				for (int i = 0; i < invalidMethodList.Count; ++i )
				{
					builder.AppendLine("- " + invalidMethodList[i]);
				}
				throw new MethodSignatureException(builder.ToString());
			}
			if (_terminals.Count == 0) throw new GeneratorException("Es wurden keine Terminalmethoden ermittelt.");
		}

		#endregion Ermitteln der Funktionen und Terminals

		#region Bauen des Ausdrucksbaumes

		/// <summary>
		/// Erzeugt einen zufälligen Ausdrucksbaum
		/// </summary>
		/// <returns>Der Ausdrucksbaum</returns>
		internal CodeExpression<T> BuildRandomExpressionTree()
		{
			Contract.Ensures(Contract.Result<CodeExpression<T>>() != null);

			int complexityHelperValue = Math.Min(_terminals.Count, _decisions.Count);
			int randomComplexity = GetRandomValue(1 + complexityHelperValue, 1 + complexityHelperValue*4);
			Debug.WriteLine("Random complexity is " + randomComplexity);

			Contract.Assert(randomComplexity >= 1);
			Contract.Assume(_terminals.Count > 0);

			CodeExpression<T> action = BuildExpressionTreeRecursive(null, randomComplexity, _decisions.Count > 0);
			return action;
		}

		/// <summary>
		/// Baut den Entscheidungsbaum rekursiv auf
		/// </summary>
		/// <param name="parent">Der Elternknoten</param>
		/// <param name="complexity">Die maximale Tiefe des Baumes</param>
		/// <param name="forceDecision">Gibt an, ob mit einer Entscheidungsfunktion begonnen werden muss</param>
		/// <returns></returns>
		private CodeExpression<T> BuildExpressionTreeRecursive(CodeExpression<T> parent, int complexity, bool forceDecision)
		{
			Contract.Requires(complexity > 0);
			Contract.Requires(_terminals.Count > 0);
			Contract.Requires(forceDecision && _decisions.Count > 0 || !forceDecision);
			Contract.Ensures(Contract.Result<CodeExpression<T>>() != null);

			// Zufällig Entscheidung oder Terminal erzeugen
			bool isDecisionNode = forceDecision || (GetRandomValue() > 0.5);

			// Einfaches Terminal verwenden, wenn:
			// - Komplexität ist eins
			// - Komplxität ist höher, aber keine Entscheidungsfunktionen vorhanden
			if (complexity == 1 || _decisions.Count == 0 || !isDecisionNode)
			{
				MethodInfo method = SelectRandomTerminal();
				return new CodeExpression<T>(parent, method);
			}
			
			MethodInfo decisionMethod = SelectRandomDecision();
			Contract.Assume(decisionMethod != null);
			ConditionalCodeExpression<T> decisionFunc = new ConditionalCodeExpression<T>(parent, decisionMethod);
			decisionFunc.LeftAction = BuildExpressionTreeRecursive(decisionFunc, complexity - 1, false);
			decisionFunc.RightAction = BuildExpressionTreeRecursive(decisionFunc, complexity - 1, false);
			return decisionFunc;
		}

		/// <summary>
		/// Wählt ein zufälliges Terminal aus
		/// </summary>
		/// <returns></returns>
		private MethodInfo SelectRandomTerminal()
		{
			Contract.Requires(_terminals.Count > 0);
			Contract.Ensures(_terminals.Count == Contract.OldValue(_terminals.Count));
			Contract.Ensures(Contract.Result<MethodInfo>() != null);

			int methodIndex = GetRandomValue(0, _terminals.Count - 1);
			MethodInfo method = _terminals[methodIndex];
			Contract.Assume(method != null);
			return method;
		}

		/// <summary>
		/// Wählt eine zufällige Entscheidungsfunktion aus
		/// </summary>
		/// <returns></returns>
		private MethodInfo SelectRandomDecision()
		{
			Contract.Requires(_decisions.Count > 0);
			Contract.Ensures(_decisions.Count == Contract.OldValue(_decisions.Count));
			Contract.Ensures(Contract.Result<MethodInfo>() != null);

			int methodIndex = GetRandomValue(0, _decisions.Count - 1);
			MethodInfo method = _decisions[methodIndex];
			Contract.Assume(method != null);
			return method;
		}

		#endregion Bauen des Ausdrucksbaumes

		#region Genetische Methoden

		/// <summary>
		/// Führt eine Crossover-Operation zwischen zwei Knoten durch
		/// </summary>
		/// <param name="left">Ein Knoten</param>
		/// <param name="right">Ein anderer Knoten</param>
		internal void Crossover(ref CodeExpression<T> left, ref CodeExpression<T> right)
		{
			Contract.Requires(left != null && left != null && !ReferenceEquals(left, right));

			IList<CodeExpression<T>> leftNodes = left.GetChildNodes().ToList();
			IList<CodeExpression<T>> rightNodes = right.GetChildNodes().ToList();

			// Zufällige Nodes aussuchen
			int leftIndex = GetRandomValue(0, leftNodes.Count);
			int rightIndex = GetRandomValue(0, rightNodes.Count);

			// Trennpunkte ermitteln
			CodeExpression<T> leftSubNode = leftNodes[leftIndex];
			CodeExpression<T> rightSubNode = rightNodes[rightIndex];

			// Links verdrehen
			ConditionalCodeExpression<T> leftParent = leftSubNode.Parent as ConditionalCodeExpression<T>;
			if (leftParent != null)
			{
				if (leftParent.LeftAction == leftSubNode)
					leftParent.LeftAction = rightSubNode;
				else
					leftParent.RightAction = rightSubNode;
			}

			// Rechts verdrehen
			ConditionalCodeExpression<T> rightParent = rightSubNode.Parent as ConditionalCodeExpression<T>;
			if (rightParent != null)
			{
				if (rightParent.LeftAction == rightSubNode)
					rightParent.LeftAction = leftSubNode;
				else
					rightParent.RightAction = leftSubNode;
			}

			// Neue Eltern setzen
			rightSubNode.Parent = leftParent;
			leftSubNode.Parent = rightParent;
		}

		/// <summary>
		/// Mutiert den Teilbaum
		/// </summary>
		/// <param name="tree"></param>
		internal void Mutate(ref CodeExpression<T> tree)
		{
			IList<CodeExpression<T>> nodes = tree.GetChildNodes().ToList();

			// Zufälligen Node aussuchen
			int leftIndex = GetRandomValue(0, nodes.Count);

			// Trennpunkte ermitteln
			CodeExpression<T> subnode = nodes[leftIndex];

			// Links verdrehen
			ConditionalCodeExpression<T> parent = subnode.Parent as ConditionalCodeExpression<T>;
			if (parent != null)
			{
				int depth = tree.GetDepth();
				int randomComplexity = GetRandomValue(Math.Max(1, depth/2), depth*2);

				if (parent.LeftAction == tree)
					parent.LeftAction = BuildExpressionTreeRecursive(parent, randomComplexity, false);
				else
					parent.RightAction = BuildExpressionTreeRecursive(parent, randomComplexity, false);
			}
		}

		#endregion Genetische Methoden
	}
}