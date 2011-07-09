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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.mazePanel = new MazeEvolution.MazePanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripComboBoxGenerator = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripButtonGenerate = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.mazePanel);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(484, 437);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(484, 462);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// mazePanel
			// 
			this.mazePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mazePanel.Location = new System.Drawing.Point(0, 0);
			this.mazePanel.Name = "mazePanel";
			this.mazePanel.Size = new System.Drawing.Size(484, 437);
			this.mazePanel.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxGenerator,
            this.toolStripButtonGenerate});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(252, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// toolStripComboBoxGenerator
			// 
			this.toolStripComboBoxGenerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxGenerator.Items.AddRange(new object[] {
            "Recursive Backtacker",
            "Randomized Prim",
            "Randomized Kruskal"});
			this.toolStripComboBoxGenerator.Name = "toolStripComboBoxGenerator";
			this.toolStripComboBoxGenerator.Size = new System.Drawing.Size(150, 25);
			// 
			// toolStripButtonGenerate
			// 
			this.toolStripButtonGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonGenerate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGenerate.Image")));
			this.toolStripButtonGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonGenerate.Name = "toolStripButtonGenerate";
			this.toolStripButtonGenerate.Size = new System.Drawing.Size(57, 22);
			this.toolStripButtonGenerate.Text = "&generate";
			this.toolStripButtonGenerate.Click += new System.EventHandler(this.ToolStripButtonGenerateClick);
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
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private MazePanel mazePanel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxGenerator;
		private System.Windows.Forms.ToolStripButton toolStripButtonGenerate;
	}
}

