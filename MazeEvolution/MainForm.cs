using System;
using System.Windows.Forms;
using Labyrinth;

namespace MazeEvolution
{
	public partial class MainForm : Form
	{
		private IMazeGenerator _generator = new RecursiveBacktracker4();

		public MainForm()
		{
			InitializeComponent();
			toolStripComboBoxGenerator.SelectedIndexChanged += ToolStripComboBoxGeneratorSelectedIndexChanged;
			toolStripComboBoxGenerator.SelectedIndex = 0;
		}

		void ToolStripComboBoxGeneratorSelectedIndexChanged(object sender, EventArgs e)
		{
			switch(toolStripComboBoxGenerator.SelectedIndex)
			{
				case 0:
					_generator = new RecursiveBacktracker4();
					break;
				case 1:
					_generator = new RandomizedPrim4();
					break;
				case 2:
					_generator = new RandomizedKruskal4();
					break;
			}

			UpdateMaze();
		}

		/// <summary>
		/// Updates the maze.
		/// </summary>
		/// <remarks></remarks>
		private void UpdateMaze()
		{
			toolStrip1.Enabled = false;
			UseWaitCursor = true;
			try
			{

				Maze4 maze = new Maze4(_generator);
				maze.GenerateNew(50, 50);
				mazePanel.SetMaze(maze);

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
			UpdateMaze();
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
	}
}
