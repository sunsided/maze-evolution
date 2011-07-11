using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
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
		/// <seealso cref="GetRandomValue(double,double)"/>
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
		/// <param name="object">The @object.</param>
		/// <exception cref="ClassDecorationException">Klasse ist nicht mit dem <see cref="EvolutionaryClassAttribute"/> markiert.</exception>
		/// <exception cref="MethodSignatureException">Eine oder mehrere Methoden, die mit dem <see cref="EvolutionaryMethodAttribute"/> markiert sind, haben eine ungültige Signatur.</exception>
		/// <exception cref="GeneratorException">Es wurden keine Terminalmethoden ermittelt.</exception>
		public Generator()
		{
			DetermineFunctionsAndTerminals();
		}
		
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
		/// <seealso cref="GetRandomValue(double,double)"/>
		/// </summary>
		/// <returns>Die Zufallszahl.</returns>
		private int GetRandomValue(int minValue, int maxValue)
		{
			Contract.Requires(minValue <= maxValue);
			Contract.Ensures(Contract.Result<int>() >= minValue && Contract.Result<int>() <= maxValue);

			double randomValue = GetRandomValue();
			return (int)(randomValue*(maxValue - minValue) + minValue);
		}

		/// <summary>
		/// Bezieht eine Zufallszahl im Bereich <paramref name="minValue"/>..<paramref name="maxValue"/>
		/// <para>>Ist keine Zufallsfunktion in <see cref="RandomFunction"/> gesetzt, wird die Standardimplementierung verwendet.</para>
		/// <seealso cref="GetRandomValue()"/>
		/// <seealso cref="GetRandomValue(int,int)"/>
		/// </summary>
		/// <returns>Die Zufallszahl.</returns>
		private double GetRandomValue(double minValue, double maxValue)
		{
			Contract.Requires(minValue <= maxValue);
			Contract.Ensures(Contract.Result<double>() >= minValue && Contract.Result<double>() <= maxValue);

			double randomValue = GetRandomValue();
			return (int)(randomValue * (maxValue - minValue) + minValue);
		}

		/// <summary>
		/// Baut einen zufälligen Ausdrucksbaum auf
		/// </summary>
		/// <param name="obj">Das Objekt, für das der Baum zu generieren ist</param>
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
				throw new ClassDecorationException("Klasse ist nicht mit dem EvolutionaryClass-Attribut markiert",
				                                   new ArgumentException( "Objekt ist nicht mit dem EvolutionaryClass-Attribut markiert", "obj"));

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
				throw new MethodSignatureException(builder.ToString(), new ArgumentException("Eine oder mehrere Methoden des Objektes haben eine ungültige Signatur", "obj"));
			}
			if (_terminals.Count == 0) throw new GeneratorException("Es wurden keine Terminalmethoden ermittelt.", new ArgumentException("Keine Terminalmethoden ermittelt.", "obj"));
		}

		/// <summary>
		/// Erzeugt einen zufälligen Ausdrucksbaum
		/// </summary>
		/// <param name="target">Das Objekt, für das die Expression erzeugt wird</param>
		/// <returns>Der Ausdrucksbaum</returns>
		internal Expression<Action> BuildRandomExpressionTree(T target)
		{
			Contract.Ensures(Contract.Result<Expression>() != null);

			int complexityHelperValue = Math.Min(_terminals.Count, _decisions.Count);
			int randomComplexity = GetRandomValue(1 + complexityHelperValue, 1 + complexityHelperValue*4);
			Debug.WriteLine("Random complexity is " + randomComplexity);

			Contract.Assert(randomComplexity >= 1);
			Contract.Assume(_terminals.Count > 0);

			Expression tree = BuildExpressionTreeRecursive(target, randomComplexity, _decisions.Count > 0);
			Expression<Action> lambda = Expression.Lambda<Action>(tree);
			Contract.Assert(lambda != null);

			return lambda;
		}

		/// <summary>
		/// Baut den Entscheidungsbaum rekursiv auf
		/// </summary>
		/// <param name="targetObject">Das Objekt, für das die Expression erzeugt wird</param>
		/// <param name="complexity">Die maximale Tiefe des Baumes</param>
		/// <param name="forceDecision">Gibt an, ob mit einer Entscheidungsfunktion begonnen werden muss</param>
		/// <returns></returns>
		private Expression BuildExpressionTreeRecursive(T targetObject, int complexity, bool forceDecision)
		{
			Contract.Requires(complexity > 0);
			Contract.Requires(_terminals.Count > 0);
			Contract.Requires(forceDecision && _decisions.Count > 0 || !forceDecision);
			Contract.Ensures(Contract.Result<Expression>() != null);

			var targetParameter = Expression.Parameter(typeof(T));

			// Einfaches Terminal verwenden, wenn:
			// - Komplexität ist eins
			// - Komplxität ist höher, aber keine Entscheidungsfunktionen vorhanden
			if (complexity == 1 || _decisions.Count == 0)
			{
				MethodInfo method = SelectRandomTerminal();



				return Expression.Call(targetParameter, method);
			}

			// Zufällig Entscheidung oder Terminal erzeugen
			bool isDecisionNode = forceDecision || (GetRandomValue() > 0.5);
			Expression expression;
			if (isDecisionNode && _decisions.Count > 0)
			{
				MethodInfo method = SelectRandomDecision();
				Contract.Assume(method != null);
				Expression testFunction = Expression.Call(targetParameter, method);
		
				// linken Baum erzeugen
				Contract.Assume(_terminals.Count > 0);
				Expression ifTrue = BuildExpressionTreeRecursive(targetObject, complexity - 1, false);

				// rechten Baum erzeugen
				Contract.Assume(_terminals.Count > 0);
				Expression ifFalse = BuildExpressionTreeRecursive(targetObject, complexity - 1, false);

				expression = Expression.IfThenElse(testFunction, ifTrue, ifFalse);
			}
			else
			{
				Contract.Assume(_terminals.Count > 0);
				MethodInfo method = SelectRandomTerminal();
				Contract.Assume(method != null);
				expression = Expression.Call(targetParameter, method);
			}

			Contract.Assert(expression != null);
			return expression;
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
	}
}
