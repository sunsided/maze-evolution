using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Evolution;
using Labyrinth;

namespace MazeEvolution
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// Anzahl der Individuen in einer Generation
		/// </summary>
		private const int GenerationSize = 2000;

		/// <summary>
		/// Der Generator
		/// </summary>
		private IMazeGenerator _mazeGenerator = new RecursiveBacktracker4();

		/// <summary>
		/// Der Evo-Generator
		/// </summary>
		private Generator<Proband> _evolutionGenerator = new Generator<Proband>();

		/// <summary>
		/// Die Probanden
		/// </summary>
		private IList<Proband> _probanden;

		/// <summary>
		/// Die genetisch erzeugten Algorithmen
		/// </summary>
		private IList<CodeExpression<Proband>> _codes;

		/// <summary>
		/// Der Forscher
		/// </summary>
		private Researcher _researcher;

		/// <summary>
		/// Das Labyrinth
		/// </summary>
		private Maze4 _maze;

		/// <summary>
		/// Der Timeout-Timer
		/// </summary>
		private readonly Timer _timeout = new Timer {Interval = 1000};

		/// <summary>
		/// Der Timeout-Tick (überschrittene Sekunden)
		/// </summary>
		private int _timeoutTick;

		/// <summary>
		/// Die Anazhl der Sekunden pro Generation
		/// </summary>
		public const int TotalSecondsPerGeneration = 10;

		/// <summary>
		/// Erzeugt eine neue Generation
		/// </summary>
		/// <remarks></remarks>
		private void GenerateGeneration(Maze4 maze)
		{
			Tuple<IList<Proband>, IList<CodeExpression<Proband>>> result = _evolutionGenerator.CreateGeneration(() => new Proband(maze), GenerationSize);
			_probanden = result.Item1;
			_codes = result.Item2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form"/> class.
		/// </summary>
		/// <remarks></remarks>
		public MainForm()
		{
			InitializeComponent();
			toolStripComboBoxGenerator.SelectedIndexChanged += ToolStripComboBoxGeneratorSelectedIndexChanged;
			toolStripComboBoxGenerator.SelectedIndex = 0;
			mazePanel.DisableMouseInteractions = true;
			_timeout.Tick += TimeoutTick;
		}

		/// <summary>
		/// Tools the strip combo box generator selected index changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ToolStripComboBoxGeneratorSelectedIndexChanged(object sender, EventArgs e)
		{
			switch(toolStripComboBoxGenerator.SelectedIndex)
			{
				case 0:
					_mazeGenerator = new RecursiveBacktracker4();
					break;
				case 1:
					_mazeGenerator = new RandomizedPrim4();
					break;
				case 2:
					_mazeGenerator = new RandomizedKruskal4();
					break;
			}

			UpdateMaze();
		}

		/// <summary>
		/// Zuletzt gewählte Labyrinthbreite
		/// </summary>
		private int _lastMazeWidth = 10;

		/// <summary>
		/// Zuletzt gewählte Labyrinthhöhe
		/// </summary>
		private int _lastMazeHeight = 10;

		/// <summary>
		/// Updates the maze.
		/// </summary>
		/// <remarks></remarks>
		private Maze4 UpdateMaze()
		{
			return UpdateMaze(_lastMazeWidth, _lastMazeHeight);
		}

		/// <summary>
		/// Updates the maze.
		/// </summary>
		/// <remarks></remarks>
		private Maze4 UpdateMaze(int width, int height)
		{
			_lastMazeWidth = width;
			_lastMazeHeight = height;

			toolStrip1.Enabled = false;
			UseWaitCursor = true;
			try
			{
				Maze4 maze = new Maze4(_mazeGenerator);
				maze.GenerateNew(width, height);
				mazePanel.SetMaze(maze);
				if (_researcher != null) _researcher.Maze = maze;
				return maze;
			}
			finally
			{
				UseWaitCursor = false;
				toolStrip1.Enabled = true;
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_maze = UpdateMaze();
			GenerateGeneration(_maze);
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonGenerate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonGenerateClick(object sender, EventArgs e)
		{
			UpdateMaze();
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonEvolution control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonEvolutionClick(object sender, EventArgs e)
		{
			toolStrip1.Enabled = false;
			UseWaitCursor = true;

			_researcher = new Researcher(_maze, _probanden, _codes);
			_researcher.RunWorkerCompleted += ResearcherRunWorkerCompleted;
			_researcher.ProgressChanged += ResearcherProgressChanged;
			_researcher.RunWorkerAsync();
			_timeout.Start();
		}

		/// <summary>
		/// Handles the Tick event of the _timeout control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void TimeoutTick(object sender, EventArgs e)
		{
			toolStripProgressBarTimeout.Value = (_timeoutTick + 1)*100/TotalSecondsPerGeneration;
			++_timeoutTick;
			if (_timeoutTick == TotalSecondsPerGeneration)
			{
				_researcher.CancelAsync();
				_timeout.Stop();
				_timeoutTick = 0;
			}
		}

		/// <summary>
		/// Handles the ProgressChanged event of the _researcher control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ResearcherProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			evolutionProgressBar.Value = Math.Min(e.ProgressPercentage, 100);
			evolutionProgressBar.Invalidate();
		}

		/// <summary>
		/// Handles the RunWorkerCompleted event of the _researcher control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ResearcherRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_timeout.Stop();
			_timeoutTick = 0;

			//MessageBox.Show("Simulation abgeschlossen. Führe Evolution durch.");
			IList<Proband> probanden = new List<Proband>(_probanden);
			_codes = _evolutionGenerator.EvolveGeneration(() => new Proband(_maze), ref probanden, _codes);
			MessageBox.Show("Durchgang abgeschlossen.");
			_probanden = probanden;

			toolStrip1.Enabled = true;
			UseWaitCursor = false;
		}

		private void x10ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateMaze(10, 10);
		}

		private void x15ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateMaze(15, 15);
		}

		private void x20ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateMaze(20, 20);
		}

		private void x25ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateMaze(25, 25);
		}

		private void x30ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateMaze(30, 30);
		}

		private void toolStripSplitButtonGenerate_ButtonClick(object sender, EventArgs e)
		{
			UpdateMaze();
		}
	}
}
