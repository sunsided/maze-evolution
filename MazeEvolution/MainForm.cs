using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Evolution;
using Labyrinth;
using Timer = System.Windows.Forms.Timer;

namespace MazeEvolution
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// Anzahl der Individuen in einer Generation
		/// </summary>
		private const int GenerationSize = 3000;

        /// <summary>
        /// Die Nummer der Generation
        /// </summary>
	    private int GenerationNumber = 0;

		/// <summary>
		/// Der Generator
		/// </summary>
		private IMazeGenerator _mazeGenerator = new RecursiveBacktracker4();

		/// <summary>
		/// Der Evo-Generator
		/// </summary>
		private readonly Generator<Proband> _evolutionGenerator = new Generator<Proband>();

		/// <summary>
		/// Die Probanden
		/// </summary>
		private IList<Proband> _probanden;

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
		public const int TotalSecondsPerGeneration = 2;

		/// <summary>
		/// Erzeugt eine neue Generation
		/// </summary>
		/// <remarks></remarks>
		private void GenerateGeneration(Maze4 maze)
		{
		    int generation = GenerationNumber++;
		    int index = 0;
            _probanden = _evolutionGenerator.CreateGeneration(GenerationSize, code => new Proband(maze, generation, Interlocked.Increment(ref index), code));
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
		/// Handles the Click event of the toolStripButtonEvolution control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonEvolutionClick(object sender, EventArgs e)
		{
			toolStrip1.Enabled = false;
			UseWaitCursor = true;

			_researcher = new Researcher(_maze, _probanden);
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
		    int generation = GenerationNumber++;
		    int index = 0;
		    GenerationReport<Proband> report = _evolutionGenerator.EvolveGeneration(GenerationSize, probanden, code => new Proband(_maze, generation, Interlocked.Increment( ref index), code));

		    StringBuilder builder = new StringBuilder();
		    builder.AppendLine(report.ToString());

		    MessageBox.Show(this, builder.ToString(), "Report für Durchlauf #" + (GenerationNumber - 1), MessageBoxButtons.OK, MessageBoxIcon.Information);
		    _probanden = report.NewGeneration.ToList();

		    toolStrip1.Enabled = true;
		    UseWaitCursor = false;
		}

	    #region Labyrinth neugenerieren

		/// <summary>
		/// Handles the Click event of the x10ToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void X10ToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateMaze(10, 10);
		}

		/// <summary>
		/// Handles the Click event of the x15ToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void X15ToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateMaze(15, 15);
		}

		/// <summary>
		/// Handles the Click event of the x20ToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void X20ToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateMaze(20, 20);
		}

		/// <summary>
		/// Handles the Click event of the x25ToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void X25ToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateMaze(25, 25);
		}

		/// <summary>
		/// Handles the Click event of the x30ToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void X30ToolStripMenuItemClick(object sender, EventArgs e)
		{
			UpdateMaze(30, 30);
		}

		/// <summary>
		/// Handles the ButtonClick event of the toolStripSplitButtonGenerate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripSplitButtonGenerateButtonClick(object sender, EventArgs e)
		{
			UpdateMaze();
		}

		#endregion Labyrinth neugenerieren
	}
}
