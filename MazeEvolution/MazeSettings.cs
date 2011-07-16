using System;
using System.Windows.Forms;
using Labyrinth;

namespace MazeEvolution
{
	/// <summary>
	/// Einstellungen zur Generation
	/// </summary>
	/// <remarks></remarks>
	public partial class MazeSettings : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form"/> class.
		/// </summary>
		/// <remarks></remarks>
		public MazeSettings()
		{
			InitializeComponent();
			// Defaults
			comboBoxGenerator.SelectedIndex = 0;
			comboBoxDimension.SelectedIndex = 0;
		}

		/// <summary>
		/// Liest oder setzt die Generationsgröße
		/// </summary>
		public IMazeGenerator MazeGenerator
		{
			get
			{
				switch (comboBoxGenerator.SelectedIndex)
				{
					case 0:
						return new RecursiveBacktracker4();
					case 1:
						return new RandomizedPrim4();
					case 2:
						return new RandomizedKruskal4();
					default:
						throw new InvalidOperationException("Unbekannter State.");
				}
			}
			set
			{
				if (value is RecursiveBacktracker4)
				{
					comboBoxGenerator.SelectedIndex = 0;
				}
				else if (value is RandomizedPrim4)
				{
					comboBoxGenerator.SelectedIndex = 1;
				}
				else if (value is RandomizedKruskal4)
				{
					comboBoxGenerator.SelectedIndex = 2;
				}
				else 
				{
					throw new InvalidOperationException("Unbekannter State.");
				}
			}
		}

		/// <summary>
		/// Liest oder setzt die Laufzeit
		/// </summary>
		public int MazeDimension
		{
			get
			{
				switch (comboBoxDimension.SelectedIndex)
				{
					// 10x10
					case 0:
						return 10;
					// 15x15
					case 1:
						return 15;
					// 20x20
					case 2:
						return 20;
					// 30x30
					case 3:
						return 30;
					// 40x40
					case 4:
						return 40;
					default:
						throw new InvalidOperationException("Unbekannter State.");
				}
			}
			set
			{
				switch (value)
				{
					// 10x10
					case 10:
						comboBoxDimension.SelectedIndex = 0;
						break;
					// 15x15
					case 15:
						comboBoxDimension.SelectedIndex = 1;
						break;
					// 20x20
					case 20:
						comboBoxDimension.SelectedIndex = 2;
						break;
					// 30x30
					case 30:
						comboBoxDimension.SelectedIndex = 3;
						break;
					// 40x40
					case 40:
						comboBoxDimension.SelectedIndex = 4;
						break;
					default:
						throw new InvalidOperationException("Unbekannter State.");
				}
			}
		}

		/// <summary>
		/// Handles the Click event of the buttonOk control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ButtonOkClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Handles the Click event of the buttonCancel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ButtonCancelClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBoxGenerator control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ComboBoxGeneratorSelectedIndexChanged(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBoxDimension control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void ComboBoxDimensionSelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
