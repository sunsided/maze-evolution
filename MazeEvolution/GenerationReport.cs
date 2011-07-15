using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Evolution;
using Labyrinth;

namespace MazeEvolution
{
    /// <summary>
    /// Generationsreport
    /// </summary>
    public partial class GenerationReport : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationReport"/> class.
        /// </summary>
        public GenerationReport()
        {
            InitializeComponent();
            comboBoxGenerator.SelectedIndex = 0;
            comboBoxSize.SelectedIndex = 0;
            numericUpDownGenerationSize.Value = 2000;
            numericUpDownRuntime.Value = 5;
        }

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
        /// Handles the SelectedIndexChanged event of the comboBoxGenerator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBoxGeneratorSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxGenerator.SelectedIndex)
            {
                case 0:
                    MazeGenerator = new RecursiveBacktracker4();
                    break;
                case 1:
                    MazeGenerator = new RandomizedPrim4();
                    break;
                case 2:
                    MazeGenerator = new RandomizedKruskal4();
                    break;
                default:
                    throw new InvalidOperationException("Unbekannter State.");
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBoxSizeSelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBoxSize.SelectedIndex)
            {
                // 10x10
                case 0:
                    MazeDimension = new Tuple<int, int>(10, 10);
                    break;
                // 15x15
                case 1:
                    MazeDimension = new Tuple<int, int>(15, 15);
                    break;
                // 20x20
                case 2:
                    MazeDimension = new Tuple<int, int>(20, 20);
                    break;
                // 30x30
                case 3:
                    MazeDimension = new Tuple<int, int>(30, 30);
                    break;
                // 40x40
                case 4:
                    MazeDimension = new Tuple<int, int>(40, 40);
                    break;
                default:
                    throw new InvalidOperationException("Unbekannter State.");
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the numericUpDownGenerationSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NumericUpDownGenerationSizeValueChanged(object sender, EventArgs e)
        {
            GenerationSize = (int)numericUpDownGenerationSize.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the numericUpDownRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NumericUpDownRuntimeValueChanged(object sender, EventArgs e)
        {
            RunDuration = TimeSpan.FromSeconds((int)numericUpDownRuntime.Value);
        }

        /// <summary>
        /// Handles the Click event of the buttonStartRun control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonStartRunClick(object sender, EventArgs e)
        {
            Generator<Proband> generator = new Generator<Proband>();

            int generation = CurrentGeneration++;
            int index = 0;
            IList<Proband> probanden = new List<Proband>(Probanden);
            GenerationReport<Proband> report = generator.EvolveGeneration(GenerationSize, probanden, code => new Proband(Maze, generation, Interlocked.Increment(ref index), code));
            Probanden = report.NewGeneration.ToList();
        }

        /// <summary>
        /// Handles the Click event of the buttonRegenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonRegenerateClick(object sender, EventArgs e)
        {
            Maze = new Maze4(MazeGenerator);
            Maze.GenerateNew(MazeDimension.Item1, MazeDimension.Item2);
        }
    }
}
