namespace MazeEvolution
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.mazePanel = new MazeEvolution.MazePanel();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.mazePanel);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(484, 462);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(484, 462);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// mazePanel
			// 
			this.mazePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mazePanel.Location = new System.Drawing.Point(0, 0);
			this.mazePanel.Name = "mazePanel";
			this.mazePanel.Size = new System.Drawing.Size(484, 462);
			this.mazePanel.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 462);
			this.Controls.Add(this.toolStripContainer);
			this.Name = "MainForm";
			this.Text = "Maze Evolution";
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private MazePanel mazePanel;
	}
}

