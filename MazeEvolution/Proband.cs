using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Evolution;
using Labyrinth;

namespace MazeEvolution
{
	/// <summary>
	/// Ein Proband
	/// </summary>
	[EvolutionaryClass]
	[DebuggerDisplay("{Id}, Fitness: {Fitness}")]
	public sealed class Proband : IFitnessProvider, ICodeProvider<Proband>
	{
        /// <summary>
        /// Die Generation, in der das Objekt erzeugt wurde
        /// </summary>
        public int SourceGeneration { get; private set; }

        /// <summary>
        /// Der Index innerhalb dieser Generation
        /// </summary>
        public int IntraGenerationIndex { get; private set; }

        /// <summary>
        /// Der genetische Code
        /// </summary>
        public CodeExpression<Proband> GeneticCode { get; private set; }

        /// <summary>
        /// Bezieht den genetischen Code
        /// </summary>
        /// <returns>Der Code</returns>
        CodeExpression<Proband> ICodeProvider<Proband>.GetCode()
        {
            return GeneticCode;
        }

        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public void SetCode(CodeExpression<Proband> expression)
        {
            Contract.Requires(expression != null);
            GeneticCode = expression;
        }

        /// <summary>
        /// Die ID des Objektes
        /// </summary>
	    public string Id
	    {
	        [Pure] get { return SourceGeneration + "-" + IntraGenerationIndex; }
	    }

		/// <summary>
		/// Das Labyrinth
		/// </summary>
		private readonly Maze4 _maze;

		/// <summary>
		/// Die Marschierungs-Hilfsklasse
		/// </summary>
		private Marcher4 _marcher;

		/// <summary>
		/// X-Koordinate der Zielzelle
		/// </summary>
		private int _targetX;

		/// <summary>
		/// Y-Koordinate der Zielzelle
		/// </summary>
		private int _targetY;

		/// <summary>
		/// Anzahl der gemachten Züge
		/// </summary>
		private int _stepsTaken;

		/// <summary>
		/// Anzahl der gemachten Schritte vorwärts
		/// </summary>
		private int _stepsTakenForward;

		/// <summary>
		/// Set der durchschrittenene Türen
		/// </summary>
		private readonly HashSet<Tuple<int, int, Door4>> _visitedDoors = new HashSet<Tuple<int, int, Door4>>();

		/// <summary>
		/// Der aktuelle Raum
		/// </summary>
		public IRoom4 CurrentRoom
		{
			get { return _maze.Rooms[_marcher.X, _marcher.Y]; }
		}

		/// <summary>
		/// Die derzeitig erreichbaren Türen
		/// </summary>
		public Door4 CurrentDoors
		{
			get { return _maze.Rooms[_marcher.X, _marcher.Y].Doors; }
		}

		/// <summary>
		/// Ermittelt, ob das Ziel erreicht wurde
		/// </summary>
		public bool TargetReached
		{
			get { return _marcher != null && ( _marcher.X == _targetX && _marcher.Y == _targetY ); }
		}

	    /// <summary>
	    /// Ermittelt, ob das Element verhungert ist, d.h. unabhängig
	    /// von der Fitness aus dem Pool genommen werden soll.
	    /// </summary>
	    public bool IsStarved
	    {
            [Pure] get { return _stepsTakenForward == 0; }
	    }

	    /// <summary>
        /// Initializes a new instance of the <see cref="Proband"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="sourceGeneration">The source generation.</param>
        /// <param name="intraGenerationIndex">Index of the intra generation.</param>
        /// <param name="code">The code.</param>
        /// <remarks></remarks>
		public Proband(Maze4 maze, int sourceGeneration, int intraGenerationIndex, CodeExpression<Proband> code)
            : this(maze, sourceGeneration, intraGenerationIndex)
		{
			Contract.Requires(maze != null);
            Contract.Requires(code != null);
            Contract.Requires(sourceGeneration >= 0);
            Contract.Requires(intraGenerationIndex >= 0);

            GeneticCode = code;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Proband"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="sourceGeneration">The source generation.</param>
        /// <param name="intraGenerationIndex">Index of the intra generation.</param>
        /// <remarks></remarks>
        private Proband(Maze4 maze, int sourceGeneration, int intraGenerationIndex)
        {
            Contract.Requires(maze != null);
            Contract.Requires(sourceGeneration >= 0);
            Contract.Requires(intraGenerationIndex >= 0);

            _maze = maze;
            SourceGeneration = sourceGeneration;
            IntraGenerationIndex = intraGenerationIndex;
        }

		/// <summary>
		/// Setzt die Instanz für einen neuen Testlauf zurück
		/// </summary>
		/// <param name="targetX">The target X.</param>
		/// <param name="targetY">The target Y.</param>
		/// <remarks></remarks>
		public void Reset(int targetX, int targetY)
		{
			_targetX = targetX;
			_targetY = targetY;
			_marcher = new Marcher4(Door4.East, 0, 0);
			_stepsTaken = 0;
			_visitedDoors.Clear();
		}

		/// <summary>
		/// Bewegt den Probanden vorwärts, wenn er kann
		/// </summary>
		[EvolutionaryMethod]
		public void MoveForward()
		{
			++_stepsTaken;
			if (!_marcher.CanMoveForward(CurrentDoors)) return;

			// Tür als durchschritten markieren
			if (!DoorInViewingDirectionVisited()) _visitedDoors.Add(new Tuple<int, int, Door4>(_marcher.X, _marcher.Y, _marcher.Direction));

			// Und ab dafür
			_marcher.MoveForward();
			++_stepsTakenForward;
		}

		/// <summary>
		/// Ermittelt, ob die Tür in Sichtrichtung bereits durchschritten wurde
		/// </summary>
		/// <returns></returns>
		[EvolutionaryMethod]
		public bool DoorInViewingDirectionVisited()
		{
			return _visitedDoors.Contains(new Tuple<int, int, Door4>(_marcher.X, _marcher.Y, _marcher.Direction));
		}

		/// <summary>
		/// Ermittelt, ob sich der Proband vorwärts bewegen kann
		/// </summary>
		[EvolutionaryMethod]
		public bool CanMoveForward()
		{
			return _marcher.CanMoveForward(CurrentDoors);
		}

		/// <summary>
		/// Ermittelt, ob sich der Proband nach links bewegen kann
		/// </summary>
		[EvolutionaryMethod]
		public bool CanTurnLeft()
		{
			return _marcher.CanTurnLeft(CurrentDoors);
		}

		/// <summary>
		/// Ermittelt, ob sich der Proband nach rechts bewegen kann
		/// </summary>
		[EvolutionaryMethod]
		public bool CanTurnRight()
		{
			return _marcher.CanTurnRight(CurrentDoors);
		}

		/// <summary>
		/// Ermittelt, ob der Raum einen Ausgang hat
		/// </summary>
		[EvolutionaryMethod]
		public bool RoomHasExit()
		{
			return !CurrentDoors.ExactlyOneValueSet() && CurrentDoors != Door4.None;
		}

		/// <summary>
		/// Dreht den Probanden nach links
		/// </summary>
		[EvolutionaryMethod]
		public void TurnLeft()
		{
			_marcher.TurnLeft();
			++_stepsTaken;
		}

		/// <summary>
		/// Dreht den Probanden nach rechts
		/// </summary>
		[EvolutionaryMethod]
		public void TurnRight()
		{
			_marcher.TurnRight();
			++_stepsTaken;
		}

		/// <summary>
		/// Dreht den Probanden um
		/// </summary>
		[EvolutionaryMethod]
		public void TurnAround()
		{
			_marcher.TurnAround();
			++_stepsTaken;
		}

		/// <summary>
		/// Bezieht die Fitness.
		/// Höhere Werte bedeuten höhere Fitness.
		/// </summary>
		/// <returns>Der Fitness-Faktor</returns>
		/// <remarks></remarks>
		public double Fitness
		{
			get { return GetFitness(); }
		}

		/// <summary>
		/// Bezieht die Fitness.
		/// Höhere Werte bedeuten höhere Fitness.
		/// </summary>
		/// <returns>Der Fitness-Faktor</returns>
		/// <remarks></remarks>
		public double GetFitness()
		{
		    // TODO !? return TargetReached ? _stepsTaken : -(_stepsTaken - _stepsTakenForward);
			return TargetReached ? Double.MaxValue - _stepsTaken : Double.MinValue + _stepsTakenForward;
		}

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Proband Clone()
        {
            return new Proband(_maze, SourceGeneration, IntraGenerationIndex, GeneticCode.Clone());
        }
	}
}
