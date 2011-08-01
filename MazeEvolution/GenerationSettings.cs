using System;
using System.Windows.Forms;

namespace MazeEvolution
{
	/// <summary>
	/// Einstellungen zur Generation
	/// </summary>
	/// <remarks></remarks>
	public partial class GenerationSettings : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Form"/> class.
		/// </summary>
		/// <remarks></remarks>
		public GenerationSettings()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Liest oder setzt die Generationsgröße
		/// </summary>
		public int GenerationSize
		{
			get { return (int) numericUpDownGenerationSize.Value; }
			set
			{
				// TODO: Bereich abtesten
				numericUpDownGenerationSize.Value = value;
			}
		}

		/// <summary>
		/// Liest oder setzt die Laufzeit
		/// </summary>
		public TimeSpan GenerationRunTime
		{
			get { return TimeSpan.FromSeconds((float) numericUpDownRuntime.Value); }
			set
			{
				// TODO: Bereich abtesten
				numericUpDownRuntime.Value = (decimal)value.TotalSeconds;
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
	}
}
