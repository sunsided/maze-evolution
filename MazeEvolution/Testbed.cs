using System;
using System.Windows.Forms;
using Evolution;
using Labyrinth;

namespace MazeEvolution
{
	public partial class Testbed : Form
	{
		/// <summary>
		/// Der Controller
		/// </summary>
		private readonly Controller _controller;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form"/> class.
		/// </summary>
		/// <remarks></remarks>
		public Testbed()
		{
			InitializeComponent();
			mazePanel.DisableMouseClickInteractions = true;

			_controller = new Controller(mazePanel);
			_controller.RunCompleted += ControllerRunCompleted;

			// Defaults
			comboBoxGenerator.SelectedIndex = 0;
			comboBoxSize.SelectedIndex = 0;
			numericUpDownGenerationSize.Value = 2000;
			numericUpDownRuntime.Value = 5;

			// Initiales erzeugen
			_controller.RegenerateMaze();
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBoxGenerator control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ComboBoxGeneratorSelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboBoxGenerator.SelectedIndex)
			{
				case 0:
					_controller.SetGenerator(new RecursiveBacktracker4());
					break;
				case 1:
					_controller.SetGenerator(new RandomizedPrim4());
					break;
				case 2:
					_controller.SetGenerator(new RandomizedKruskal4());
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
		/// <remarks></remarks>
		private void ComboBoxSizeSelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboBoxSize.SelectedIndex)
			{
				// 10x10
				case 0:
					_controller.SetDimension(10, 10);
					break;
				// 15x15
				case 1:
					_controller.SetDimension(15, 15);
					break;
				// 20x20
				case 2:
					_controller.SetDimension(20, 20);
					break;
				// 30x30
				case 3:
					_controller.SetDimension(30, 30);
					break;
				// 40x40
				case 4:
					_controller.SetDimension(40, 40);
					break;
				default:
					throw new InvalidOperationException("Unbekannter State.");
			}
		}

		/// <summary>
		/// Handles the Click event of the buttonRegenerate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ButtonRegenerateClick(object sender, EventArgs e)
		{
			_controller.RegenerateMaze();
		}

		/// <summary>
		/// Handles the ValueChanged event of the numericUpDownGenerationSize control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void NumericUpDownGenerationSizeValueChanged(object sender, EventArgs e)
		{
			_controller.SetGenerationSize((int)numericUpDownGenerationSize.Value);
		}

		/// <summary>
		/// Handles the ValueChanged event of the numericUpDownRuntime control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void NumericUpDownRuntimeValueChanged(object sender, EventArgs e)
		{
			_controller.SetRuntime(TimeSpan.FromSeconds((int)numericUpDownRuntime.Value));
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonExit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonExitClick(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Wirklich beenden?", "Beenden", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				Close();
			}
		}

		/// <summary>
		/// Handles the Click event of the buttonRun control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ButtonRunClick(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			panelSettings.Enabled = false;
			toolStripMain.Enabled = false;
			
			if (_controller.IsRunning) _controller.AbortRun();
			else _controller.PerformRun();
			buttonRun.Text = _controller.IsRunning ? "&Abbrechen" : "&Start";
		}

		/// <summary>
		/// Handles the RunCompleted event of the _controller control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ControllerRunCompleted(object sender, GenerationReportEventArgs<Proband> e)
		{
			UseWaitCursor = false;
			panelSettings.Enabled = true;
			toolStripMain.Enabled = true;
			buttonRun.Text = _controller.IsRunning ? "&Abbrechen" : "&Start";
			labelGeneration.Text = "#" + _controller.CurrentGeneration;
		}
	}
}
