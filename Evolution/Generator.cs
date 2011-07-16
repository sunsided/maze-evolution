using System;
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
	public sealed class Generator<T> where T : class, IFitnessProvider, ICodeProvider<T> 
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
			int leftIndex = GetRandomValue(0, leftNodes.Count-1);
			int rightIndex = GetRandomValue(0, rightNodes.Count-1);

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
			else
			{
				left = rightSubNode;
				left.Parent = null;
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
			else
			{
				right = leftSubNode;
				right.Parent = null;
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
			int leftIndex = GetRandomValue(0, nodes.Count-1);

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

		#region Erzeugen von Generationen
        
        /// <summary>
        /// Erzeugt ein neues Element
        /// </summary>
        /// <param name="newCode">Der neue Code</param>
        /// <returns>Das neu erzeugte Element</returns>
        public delegate T CreateElement(CodeExpression<T> newCode);

        /// <summary>
        /// Erzeugt ein neues Element mittels Mutation eines Referenzelementes
        /// </summary>
        /// <param name="reference">Das Ursprungselement</param>
        /// <param name="newCode">Der neue Code</param>
        /// <returns>Das neu erzeugte Element</returns>
	    public delegate T CreateElementByMutation(T reference, CodeExpression<T> newCode);

        /// <summary>
        /// Erzeugt ein neues Element mittels Crossover aus zwei Elternelementen
        /// </summary>
        /// <param name="parentA">Elternelement A</param>
        /// <param name="parentB">Elternelement B</param>
        /// <param name="newCode">Der neue Code</param>
        /// <returns>Das neu erzeugte Element</returns>
        public delegate T CreateElementByCrossover(T parentA, T parentB, CodeExpression<T> newCode);

        /// <summary>
        /// Erzeugt eine neue Generation an Elementen
        /// </summary>
        /// <param name="regularCreator">Die Erzeugerfunktion.</param>
        /// <param name="generationSize">Die Anzahl der Elemente.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public IList<T> CreateGeneration(int generationSize, CreateElement regularCreator)
        {
            Contract.Requires(generationSize >= 0);
            Contract.Requires(regularCreator != null, "Erzeugerfunktion darf nicht null sein.");

            List<T> elements = new List<T>(generationSize);
            for (int i = 0; i < generationSize; ++i)
            {
                CodeExpression<T> expression = BuildRandomExpressionTree();
                elements.Add(regularCreator(expression));
            }
            return elements;
        }


        /// <summary>
        /// Erzeugt eine neue Generation aus einer bestehenden
        /// </summary>
        /// <param name="newGenerationSize">New size of the generation.</param>
        /// <param name="fitnesses">The fitnesses.</param>
        /// <param name="regularCreator">Methode, die ein neues Element erzeugt.</param>
        /// <param name="crossoverCreator">Methode, die ein neues Element aus einer Crossover-Operation erzeugt.</param>
        /// <param name="mutationCreator">Methode, die ein neues Element aus einer Mutations-Operation erzeugt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public GenerationReport<T> EvolveGeneration(int newGenerationSize, IList<T> fitnesses, CreateElement regularCreator, CreateElementByCrossover crossoverCreator, CreateElementByMutation mutationCreator)
		{
            Contract.Requires(newGenerationSize > 0, "Größe der Generation muss positiv sein");
            Contract.Requires(fitnesses != null);
            Contract.Requires(regularCreator != null, "Erzeugerfunktion darf nicht null sein.");
            Contract.Requires(crossoverCreator != null, "Erzeugerfunktion darf nicht null sein.");
            Contract.Requires(mutationCreator != null, "Erzeugerfunktion darf nicht null sein.");

            return EvolveGeneration(newGenerationSize, fitnesses, regularCreator, crossoverCreator, mutationCreator, 0.1D, 0.1d, 0.05d);
		}

        /// <summary>
        /// Erzeugt eine neue Generation aus einer bestehenden
        /// </summary>
        /// <param name="newGenerationSize">New size of the generation.</param>
        /// <param name="fitnesses">The fitnesses.</param>
        /// <param name="regularCreator">Methode, die ein neues Element erzeugt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public GenerationReport<T> EvolveGeneration(int newGenerationSize, IList<T> fitnesses, CreateElement regularCreator)
        {
            Contract.Requires(newGenerationSize > 0, "Größe der Generation muss positiv sein");
            Contract.Requires(fitnesses != null);
            Contract.Requires(regularCreator != null, "Erzeugerfunktion darf nicht null sein.");

            return EvolveGeneration(newGenerationSize, fitnesses, regularCreator, (ignored1, ignored2, code) => regularCreator(code), (ignored, code) => regularCreator(code), 0.1D, 0.1d, 0.05d);
        }

        /// <summary>
        /// Erzeugt eine neue Generation aus einer bestehenden
        /// </summary>
        /// <param name="newGenerationSize">New size of the generation.</param>
        /// <param name="fitnesses">The fitnesses.</param>
        /// <param name="regularCreator">Methode, die ein neues Element erzeugt.</param>
        /// <param name="keepPercentage">Anzahl der Elemente, die behalten werden sollen (z.B. <c>0.1</c> für top ten).</param>
        /// <param name="crossoverPercentage">Die Wahrscheinlichkeit, dass ein crossover auftritt.</param>
        /// <param name="mutationPercentage">Die Wahrscheinlichkeit, dass eine Mutation auftritt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public GenerationReport<T> EvolveGeneration(int newGenerationSize, IList<T> fitnesses, CreateElement regularCreator, double keepPercentage, double crossoverPercentage, double mutationPercentage)
        {
            Contract.Requires(newGenerationSize > 0, "Größe der Generation muss positiv sein");
            Contract.Requires(fitnesses != null);
            Contract.Requires(regularCreator != null, "Erzeugerfunktion darf nicht null sein.");

            return EvolveGeneration(newGenerationSize, fitnesses, regularCreator, (ignored1, ignored2, code) => regularCreator(code), (ignored, code) => regularCreator(code), keepPercentage, crossoverPercentage, mutationPercentage);
        }

        /// <summary>
        /// Erzeugt eine neue Generation aus einer bestehenden
        /// </summary>
        /// <param name="newGenerationSize">New size of the generation.</param>
        /// <param name="fitnesses">The fitnesses.</param>
        /// <param name="regularCreator">Methode, die ein neues Element erzeugt.</param>
        /// <param name="crossoverCreator">Methode, die ein neues Element aus einer Crossover-Operation erzeugt.</param>
        /// <param name="mutationCreator">Methode, die ein neues Element aus einer Mutations-Operation erzeugt.</param>
        /// <param name="keepPercentage">Anzahl der Elemente, die behalten werden sollen (z.B. <c>0.1</c> für top ten).</param>
        /// <param name="crossoverPercentage">Die Wahrscheinlichkeit, dass ein crossover auftritt.</param>
        /// <param name="mutationPercentage">Die Wahrscheinlichkeit, dass eine Mutation auftritt.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public GenerationReport<T> EvolveGeneration(int newGenerationSize, IList<T> fitnesses, CreateElement regularCreator, CreateElementByCrossover crossoverCreator, CreateElementByMutation mutationCreator, double keepPercentage, double crossoverPercentage, double mutationPercentage)
		{
            Contract.Requires(newGenerationSize > 0, "Größe der Generation muss positiv sein");
            Contract.Requires(regularCreator != null, "Erzeugerfunktion darf nicht null sein.");
            Contract.Requires(crossoverCreator != null, "Erzeugerfunktion darf nicht null sein.");
            Contract.Requires(mutationCreator != null, "Erzeugerfunktion darf nicht null sein.");
			Contract.Requires(fitnesses != null);
			Contract.Requires(keepPercentage > 0 && keepPercentage <= 1);
			Contract.Requires(crossoverPercentage >= 0 && crossoverPercentage <= 1);
			Contract.Requires(mutationPercentage >= 0 && mutationPercentage <= 1);

			// Nach Fitness sortieren
			// Elemente mit kürzeren Hypothesen werden bevorzugt (Ockhams Skalpell)
            List<T> fitnessSorted = new List<T>(fitnesses.OrderBy(element => element.GetFitness()).ThenBy(element => element.GetCode().GetDepth()));

            // Selektion
            Func<int, int, double, double> selectionProbability = (index, maxCount, percentage) => Math.Exp(-((1.0D / percentage) * index) / maxCount); // TODO: Sigmoid-Funktion verwenden
            List<T> selectedElements = new List<T>();
            List<T> deceasedElements = new List<T>();
            for (int i = 0; i < fitnessSorted.Count; ++i)
            {
                double value = GetRandomValue();
                double judge = selectionProbability(i, fitnessSorted.Count, keepPercentage);
                if (value > judge || fitnessSorted[i].IsStarved)
                {
                    deceasedElements.Add(fitnessSorted[i]);
                    continue;
                }
                selectedElements.Add(fitnessSorted[i]);
            }
            int selectedItemCount = selectedElements.Count;

			// Crossover
		    List<Tuple<T, T, T>> crossoverResults = new List<Tuple<T, T, T>>();
		    Func<int, int, double, double> crossoverProbability = selectionProbability;
            for (int i = 0; i < selectedItemCount; ++i)
			{
                // Wahrscheinlichkeit für Crossover-Operation dieses Elementes
				if (GetRandomValue() > crossoverPercentage) continue;

                // Crossover-Kandidaten ermitteln
                List<T> crossoverCandidates = new List<T>();
                for (int c = 0; c < selectedItemCount; ++c)
                {
                    double value = GetRandomValue();
                    double judge = crossoverProbability(i, fitnessSorted.Count, keepPercentage * 0.5);
                    if (value > judge) continue;
                    crossoverCandidates.Add(selectedElements[c]);
                }

                // Aus Kandidaten Element auswählen
			    int selectedCandidateIndex = GetRandomValue(0, crossoverCandidates.Count - 1);
			    T elementA = selectedElements[i];
                T elementB = crossoverCandidates[selectedCandidateIndex];

                // Codes ermitteln
			    CodeExpression<T> codeA = elementA.GetCode().Clone();
			    CodeExpression<T> codeB = elementB.GetCode().Clone();

                // Crossen der Codes
				Crossover(ref codeA, ref codeB);

                // Erzeugen und registrieren
                T newElementA = crossoverCreator(elementA, elementB, codeA);
                T newElementB = crossoverCreator(elementA, elementB, codeB);
                crossoverResults.Add(new Tuple<T, T, T>(elementA, elementB, newElementA));
                crossoverResults.Add(new Tuple<T, T, T>(elementA, elementB, newElementB));
			}

			// Mutation
            List<Tuple<T, T>> mutationResults = new List<Tuple<T, T>>();
            for (int i = 0; i < selectedItemCount; ++i)
			{
                // Nach Wahrscheinlichkeit Element auswählen
				if (GetRandomValue() > mutationPercentage) continue;
                T element = selectedElements[i];
				CodeExpression<T> a = element.GetCode().Clone();

                // Mutieren des Codes
				Mutate(ref a);

                // Erzeugen und registrieren
                T newElement = mutationCreator(element, a);
                mutationResults.Add(new Tuple<T, T>(element, newElement));
			}

            // Neue Generation beginnen ...
            List<T> newGeneration = new List<T>();
            newGeneration.AddRange(selectedElements);

            // ... gecrosste und mutierte Elemente durchmischen, dann hinzufügen ...
            List<T> newElements = new List<T>();
            newElements.AddRange(crossoverResults.Select(element => element.Item3));
            newElements.AddRange(mutationResults.Select(element => element.Item2));
            newGeneration.AddRange(newElements.OrderBy(r => GetRandomValue()));

            // ... zuletzt fehlende Elemente hinzufügen oder Liste trimmen
        	IList<T> newlyGenerated;
            if (newGeneration.Count < newGenerationSize)
            {
                int count = newGenerationSize - newGeneration.Count;
                Debug.WriteLine("Fülle neue Generation auf bis zur Zielgröße. (Erzeuge " + count + ".)");
				newlyGenerated = CreateGeneration(count, regularCreator);
				newGeneration.AddRange(newlyGenerated);
            }
            else
            {
				newlyGenerated = new List<T>(0);
                // Vom Ende entfernen
                Debug.WriteLine("Reduziere neue Generation auf neue Zielgröße.");
                while (newGeneration.Count > newGenerationSize)
                {
                    newGeneration.RemoveAt(newGeneration.Count - 1);
                }
            }

            // Report erzeugen
			GenerationReport<T> report = new GenerationReport<T>(fitnessSorted, selectedElements, deceasedElements, mutationResults, crossoverResults, newlyGenerated, newGeneration);
            return report;
		}

		#endregion Erzeugen von Generationen
	}
}
