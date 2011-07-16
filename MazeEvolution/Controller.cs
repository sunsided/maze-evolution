using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Evolution;
using Labyrinth;
using Timer = System.Windows.Forms.Timer;

namespace MazeEvolution
{
	public sealed class Controller
	{
		/// <summary>
		/// Das Darstellungspanel
		/// </summary>
		private readonly MazePanel _panel;

		/// <summary>
		/// Die Nummer der aktuellen Generation
		/// </summary>
		public int CurrentGeneration { get; private set; }

		/// <summary>
		/// Die Dimension des für die nächste Generation zu erzeugenden Labyrinthes
		/// </summary>
		public Tuple<int, int> MazeDimension { get; private set; }

		/// <summary>
		/// Der für die nächste Generation zu verwendende Labyrinthgenerator
		/// </summary>
		public IMazeGenerator MazeGenerator { get; private set; }

		/// <summary>
		/// Das verwendete Labyrinth
		/// </summary>
		public Maze4 Maze { get; private set; }

		/// <summary>
		/// Die Größe der nächsten Generation
		/// </summary>
		public int GenerationSize { get; private set; }

		/// <summary>
		/// Laufzeit einer Generation
		/// </summary>
		public TimeSpan RunDuration { get; private set; }

		/// <summary>
		/// Die Probanden
		/// </summary>
		public IList<Proband> Probanden { get; private set; }

		/// <summary>
		/// Der zuletzt erzeugte Report
		/// </summary>
		public GenerationReport<Proband> LastReport { get; private set; }

		/// <summary>
		/// Der Forscher
		/// </summary>
		private Researcher _researcher;

		/// <summary>
		/// Wird gerufen, wenn ein Durchlauf abgeschlossen wurde
		/// </summary>
		public event EventHandler<GenerationReportEventArgs<Proband>> RunCompleted;

		/// <summary>
		/// Der Watchdog-Timer
		/// </summary>
		private Timer Timeout = new Timer { Interval = 1000 };

		/// <summary>
		/// Der Timeout-Tick (überschrittene Sekunden)
		/// </summary>
		private int _timeoutTick;

		/// <summary>
		/// Gibt an, ob der Controller in den AutoEvolve-Modus geschaltet wird
		/// </summary>
		public bool AutoEvolveMode { get; private set; }

		/// <summary>
		/// Wird gerufen, wenn das Labyrinth verändert wurde
		/// </summary>
		public event EventHandler<MazeEventArgs> MazeChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="Controller"/> class.
		/// </summary>
		/// <param name="panel">The panel.</param>
		/// <remarks></remarks>
		public Controller(MazePanel panel)
		{
			Contract.Requires(panel != null);

			MazeGenerator = new RecursiveBacktracker4();
			MazeDimension = new Tuple<int, int>(10, 10);

			_panel = panel;
			Timeout.Tick += TimeoutTick;
			CreateGeneration();
		}

		/// <summary>
		/// Handles the Tick event of the Timeout control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void TimeoutTick(object sender, EventArgs e)
		{
			++_timeoutTick;
			if (_timeoutTick >= RunDuration.TotalSeconds)
			{
				_researcher.CancelAsync();
				Timeout.Stop();
				_timeoutTick = 0;
			}
		}

		/// <summary>
		/// Sets the generator.
		/// </summary>
		/// <param name="generator">The generator.</param>
		/// <remarks></remarks>
		public void SetGenerator(IMazeGenerator generator)
		{
			Contract.Requires(generator != null);
			MazeGenerator = generator;
		}

		/// <summary>
		/// Sets the dimension.
		/// </summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <remarks></remarks>
		public void SetDimension(int width, int height)
		{
			Contract.Requires(width > 0 && width > 0);
			MazeDimension = new Tuple<int, int>(width, height);
		}

		/// <summary>
		/// Sets the size of the generation.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <remarks></remarks>
		public void SetGenerationSize(int size)
		{
			Contract.Requires(size >= 100);
			GenerationSize = size;
		}

		/// <summary>
		/// Sets the runtime.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <remarks></remarks>
		public void SetRuntime(TimeSpan timeout)
		{
			RunDuration = timeout;
		}

		/// <summary>
		/// Regenerates the maze.
		/// </summary>
		/// <remarks></remarks>
		public void RegenerateMaze()
		{
			Maze = new Maze4(MazeGenerator);
			Maze.MazeChanged += MazeMazeChanged;
			Maze.GenerateNew(MazeDimension.Item1, MazeDimension.Item2);

			// Aktualisieren
			if (_panel.InvokeRequired) _panel.Invoke((MethodInvoker)delegate
			                                                        	{
			                                                        		_panel.SetMaze(Maze);
																			_panel.SetStartingPoint(0, 0);
			                                                        	});
			else
			{
				_panel.SetMaze(Maze);
				_panel.SetStartingPoint(0, 0);
			}
		}

		/// <summary>
		/// Handles the MazeChanged event of the Maze control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Labyrinth.MazeEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void MazeMazeChanged(object sender, MazeEventArgs e)
		{
			EventHandler<MazeEventArgs> handler = MazeChanged;
			if (handler != null) handler(this, new MazeEventArgs(Maze));
		}

		/// <summary>
		/// Erzeugt eine Generation.
		/// </summary>
		/// <remarks></remarks>
		public void CreateGeneration()
		{
			Generator<Proband> evolutionGenerator = new Generator<Proband>();

			Maze4 maze = Maze;
			int generation = CurrentGeneration++;
			int index = 0;
			Probanden = evolutionGenerator.CreateGeneration(GenerationSize, code => new Proband(maze, generation, Interlocked.Increment(ref index), code));
		}

		/// <summary>
		/// Gets a value indicating whether this instance is running.
		/// </summary>
		/// <remarks></remarks>
		public bool IsRunning { get; private set; }

		/// <summary>
		/// Führt einen Run durch
		/// </summary>
		public void PerformRun(bool autoLoop)
		{
			Debug.WriteLine("Starte Lauf ...");
			AutoEvolveMode = autoLoop;
			
			IsRunning = true;
			
			_researcher = new Researcher(Maze, Probanden);
			_researcher.RunWorkerCompleted += ResearcherRunWorkerCompleted;
			_researcher.RunWorkerAsync();

			// Timer starten
			_timeoutTick = 0;
			Timeout.Start();
		}

		/// <summary>
		/// Handles the RunWorkerCompleted event of the _researcher control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ResearcherRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Evolve();
		}

		/// <summary>
		/// Führt die Evolution durch
		/// </summary>
		private void Evolve()
		{
			Debug.WriteLine("Evolving ...");
			Generator<Proband> evolutionGenerator = new Generator<Proband>();
			
			// Evolution durchführen
			IList<Proband> probanden = new List<Proband>(Probanden);
			Maze4 maze = Maze;
			int generation = CurrentGeneration++;
			int index = 0;
			GenerationReport<Proband> report = evolutionGenerator.EvolveGeneration(GenerationSize, probanden, code => new Proband(maze, generation, Interlocked.Increment(ref index), code));
			LastReport = report;
			Probanden = report.NewGeneration.ToList();

			// Feddsch.
			IsRunning = false;

			// Erfolg vermelden
			EventHandler<GenerationReportEventArgs<Proband>> func = RunCompleted;
			if (func == null) return;
			func(this, new GenerationReportEventArgs<Proband>(report));

			// AutoEvolve verwenden
			if (AutoEvolveMode) PerformRun(true);
		}

		/// <summary>
		/// Bricht einen Run ab
		/// </summary>
		public void AbortRun()
		{
			if (_researcher == null) return;
			if (AutoEvolveMode)
			{
				AutoEvolveMode = false;
				return;
			}
			
			_researcher.CancelAsync();
			Timeout.Stop();
			_timeoutTick = 0;
		}
	}
}
