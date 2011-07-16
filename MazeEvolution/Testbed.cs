using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Forms;
using Evolution;
using MazeEvolution.Properties;

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
			_controller.TimeProgress += ControllerTimeProgress;
			_controller.SetGenerationSize(2000);
			_controller.SetRuntime(TimeSpan.FromSeconds(1));

			// Icons setzen
			toolStripButtonRun.Image = Resources.run;
			toolStripButtonAutoEvolve.Image = Resources.runloop;
			toolStripButtonAbort.Image = Resources.abortrun;
			
			// Initiales erzeugen
			_controller.RegenerateMaze();
			_controller.CreateGeneration();

			// Probanden eintragen
			dataGridViewReport.RowCount = _controller.Probanden.Count;
			int index = 0;
			foreach (Proband proband in _controller.Probanden)
			{
				dataGridViewReport.Rows[index].SetValues(proband.Id, "-", proband.GeneticCode.GetDepth(), 0, "initial");
				dataGridViewReport.Rows[index++].Tag = proband;
			}
		}

		/// <summary>
		/// Handles the TimeProgress event of the _controller control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ControllerTimeProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			Invoke((MethodInvoker) delegate
			                       	{
			                       		toolStripTimeProgress.Value = e.ProgressPercentage;
			                       	});
		}
		
		/// <summary>
		/// Handles the Click event of the toolStripButtonExit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonExitClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if (MessageBox.Show(this, "Wirklich beenden?", "Beenden", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
			{
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonRun control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonRunClick(object sender, EventArgs e)
		{
			Run();
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonAutoEvolve control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonAutoEvolveClick(object sender, EventArgs e)
		{
			RunLoop();
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonAbort control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonAbortClick(object sender, EventArgs e)
		{
			AbortRun();
		}

		/// <summary>
		/// Sperrt die GUI ab, so dass ein Durchlauf sicher geschehen kann
		/// </summary>
		private void LockGuiForRun()
		{
			UseWaitCursor = true;
			toolStripButtonRun.Enabled = false;
			toolStripButtonAutoEvolve.Enabled = false;
			toolStripButtonAbort.Enabled = true;
			panelMazeControl.Enabled = false;
			dataGridViewReport.Enabled = false;
			toolStripButtonSettings.Enabled = false;
		}

		/// <summary>
		/// Startet einen Durchlauf
		/// </summary>
		private void Run()
		{
			LockGuiForRun();
			_controller.PerformRun(false);
		}

		/// <summary>
		/// Startet eine Schleife
		/// </summary>
		private void RunLoop()
		{
			LockGuiForRun();
			_controller.PerformRun(true);
		}

		/// <summary>
		/// Bricht einen Durchlauf ab
		/// </summary>
		private void AbortRun()
		{
			_controller.AbortRun();
		}

		/// <summary>
		/// Handles the RunCompleted event of the _controller control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		void ControllerRunCompleted(object sender, GenerationReportEventArgs<Proband> e)
		{
			toolStripStatusLabelGeneration.Text = "Generation " + _controller.CurrentGeneration;
			groupBoxLastGeneration.Text = "Generation " + (_controller.CurrentGeneration - 1) + " -> " +
			                              _controller.CurrentGeneration;

			// Report übernehmen
			labelSelected.Text = e.Report.SelectedElements.Count.ToString();
			labelDeceased.Text = e.Report.DeceasedElements.Count.ToString();
			labelMutated.Text = e.Report.MutatedElements.Count.ToString();
			labelCrossover.Text = e.Report.CrossedElements.Count.ToString();
			labelBorn.Text = e.Report.NewlyGeneratedElements.Count.ToString();

			if (e.Report.SelectedElements.Count > 0)
			{
				labelMaxFitness.Text = e.Report.SelectedElements.Max(element => element.GetFitness()).ToString();
				labelMinFitness.Text = e.Report.SelectedElements.Min(element => element.GetFitness()).ToString();
				labelComplexity.Text = e.Report.SelectedElements.Min(element => element.GeneticCode.GetDepth()) + " .. " +
				                       e.Report.SelectedElements.Max(element => element.GeneticCode.GetDepth());
			}
			else
			{
				labelMaxFitness.Text = "-";
				labelMinFitness.Text = "-";
				labelComplexity.Text = "-";
			}
			
			// Tabelle bauen
			dataGridViewReport.RowCount = e.Report.SelectedElements.Count + e.Report.MutatedElements.Count +
										  e.Report.CrossedElements.Count + e.Report.DeceasedElements.Count;
			int index = 0;
			foreach (Proband proband in e.Report.SelectedElements.OrderBy(element => -1 * element.GetFitness()).ThenBy(element => element.GeneticCode.GetDepth()))
			{
				dataGridViewReport.Rows[index].SetValues(proband.Id, proband.GetFitness().ToString(), proband.GeneticCode.GetDepth(), _controller.CurrentGeneration - proband.SourceGeneration - 1, "selected");
				dataGridViewReport.Rows[index++].Tag = proband;
			}
			foreach (Proband proband in e.Report.CrossedElements.Select(tuple => tuple.Item3))
			{
				dataGridViewReport.Rows[index].SetValues(proband.Id, "-", proband.GeneticCode.GetDepth(), 1, "crossed");
				dataGridViewReport.Rows[index++].Tag = proband;
			}
			foreach (Proband proband in e.Report.MutatedElements.Select(tuple => tuple.Item2))
			{
				dataGridViewReport.Rows[index].SetValues(proband.Id, "-", proband.GeneticCode.GetDepth(), 1, "mutated");
				dataGridViewReport.Rows[index++].Tag = proband;
			}
			foreach (Proband proband in e.Report.DeceasedElements)
			{
				dataGridViewReport.Rows[index].SetValues(proband.Id, proband.GetFitness().ToString(), proband.GeneticCode.GetDepth(), _controller.CurrentGeneration - proband.SourceGeneration - 1, "deceased");
				dataGridViewReport.Rows[index++].Tag = proband;
			}

			if (_controller.AutoEvolveMode) return;

			UseWaitCursor = false;
			dataGridViewReport.Enabled = true;
			toolStripButtonRun.Enabled = true;
			toolStripButtonAutoEvolve.Enabled = true;
			toolStripButtonAbort.Enabled = false;
			panelMazeControl.Enabled = true;
			toolStripButtonSettings.Enabled = true;
		}

		/// <summary>
		/// Handles the Click event of the buttonConfigureMaze control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ButtonConfigureMazeClick(object sender, EventArgs e)
		{
			MazeSettings settings = new MazeSettings();
			settings.MazeGenerator = _controller.MazeGenerator;
			settings.MazeDimension = _controller.MazeDimension.Item1;
			if (settings.ShowDialog(this) == DialogResult.OK)
			{
				_controller.SetDimension(settings.MazeDimension, settings.MazeDimension);
				_controller.SetGenerator(settings.MazeGenerator);
				_controller.RegenerateMaze();
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
		/// Handles the CellDoubleClick event of the dataGridViewReport control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void DataGridViewReportCellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;
			Proband proband = dataGridViewReport.Rows[e.RowIndex].Tag as Proband;
			Contract.Assert(proband != null);

			CodeView view = new CodeView(proband.GeneticCode);
			view.ShowDialog(this);
		}

		/// <summary>
		/// Handles the Click event of the toolStripButtonSettings control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ToolStripButtonSettingsClick(object sender, EventArgs e)
		{
			GenerationSettings settings = new GenerationSettings();
			settings.GenerationSize = _controller.GenerationSize;
			settings.GenerationRunTime = _controller.RunDuration;
			if (settings.ShowDialog(this) == DialogResult.OK)
			{
				_controller.SetRuntime(settings.GenerationRunTime);
				_controller.SetGenerationSize(settings.GenerationSize);
			}
		}
	}
}
