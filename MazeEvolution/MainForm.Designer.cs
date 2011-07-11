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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripComboBoxGenerator = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonEvolution = new System.Windows.Forms.ToolStripButton();
			this.evolutionProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripProgressBarTimeout = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripSplitButtonGenerate = new System.Windows.Forms.ToolStripSplitButton();
			this.x10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x15ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x20ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x25ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x30ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mazePanel = new MazeEvolution.MazePanel();
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
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(501, 437);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(501, 462);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxGenerator,
            this.toolStripSplitButtonGenerate,
            this.toolStripSeparator1,
            this.toolStripButtonEvolution,
            this.evolutionProgressBar,
            this.toolStripProgressBarTimeout});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(498, 25);
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
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonEvolution
			// 
			this.toolStripButtonEvolution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonEvolution.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEvolution.Image")));
			this.toolStripButtonEvolution.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonEvolution.Name = "toolStripButtonEvolution";
			this.toolStripButtonEvolution.Size = new System.Drawing.Size(48, 22);
			this.toolStripButtonEvolution.Text = "Evolve!";
			this.toolStripButtonEvolution.Click += new System.EventHandler(this.ToolStripButtonEvolutionClick);
			// 
			// evolutionProgressBar
			// 
			this.evolutionProgressBar.Name = "evolutionProgressBar";
			this.evolutionProgressBar.Size = new System.Drawing.Size(100, 22);
			// 
			// toolStripProgressBarTimeout
			// 
			this.toolStripProgressBarTimeout.ForeColor = System.Drawing.Color.Firebrick;
			this.toolStripProgressBarTimeout.Name = "toolStripProgressBarTimeout";
			this.toolStripProgressBarTimeout.Size = new System.Drawing.Size(100, 22);
			// 
			// toolStripSplitButtonGenerate
			// 
			this.toolStripSplitButtonGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSplitButtonGenerate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x10ToolStripMenuItem,
            this.x15ToolStripMenuItem,
            this.x20ToolStripMenuItem,
            this.x25ToolStripMenuItem,
            this.x30ToolStripMenuItem});
			this.toolStripSplitButtonGenerate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonGenerate.Image")));
			this.toolStripSplitButtonGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButtonGenerate.Name = "toolStripSplitButtonGenerate";
			this.toolStripSplitButtonGenerate.Size = new System.Drawing.Size(69, 22);
			this.toolStripSplitButtonGenerate.Text = "&generate";
			this.toolStripSplitButtonGenerate.ButtonClick += new System.EventHandler(this.toolStripSplitButtonGenerate_ButtonClick);
			// 
			// x10ToolStripMenuItem
			// 
			this.x10ToolStripMenuItem.Name = "x10ToolStripMenuItem";
			this.x10ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x10ToolStripMenuItem.Text = "10x10";
			this.x10ToolStripMenuItem.Click += new System.EventHandler(this.x10ToolStripMenuItem_Click);
			// 
			// x15ToolStripMenuItem
			// 
			this.x15ToolStripMenuItem.Name = "x15ToolStripMenuItem";
			this.x15ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x15ToolStripMenuItem.Text = "15x15";
			this.x15ToolStripMenuItem.Click += new System.EventHandler(this.x15ToolStripMenuItem_Click);
			// 
			// x20ToolStripMenuItem
			// 
			this.x20ToolStripMenuItem.Name = "x20ToolStripMenuItem";
			this.x20ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x20ToolStripMenuItem.Text = "20x20";
			this.x20ToolStripMenuItem.Click += new System.EventHandler(this.x20ToolStripMenuItem_Click);
			// 
			// x25ToolStripMenuItem
			// 
			this.x25ToolStripMenuItem.Name = "x25ToolStripMenuItem";
			this.x25ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x25ToolStripMenuItem.Text = "25x25";
			this.x25ToolStripMenuItem.Click += new System.EventHandler(this.x25ToolStripMenuItem_Click);
			// 
			// x30ToolStripMenuItem
			// 
			this.x30ToolStripMenuItem.Name = "x30ToolStripMenuItem";
			this.x30ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.x30ToolStripMenuItem.Text = "30x30";
			this.x30ToolStripMenuItem.Click += new System.EventHandler(this.x30ToolStripMenuItem_Click);
			// 
			// mazePanel
			// 
			this.mazePanel.DisableMouseInteractions = false;
			this.mazePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mazePanel.Enabled = false;
			this.mazePanel.Location = new System.Drawing.Point(0, 0);
			this.mazePanel.Name = "mazePanel";
			this.mazePanel.Size = new System.Drawing.Size(501, 437);
			this.mazePanel.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(501, 462);
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonEvolution;
		private System.Windows.Forms.ToolStripProgressBar evolutionProgressBar;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarTimeout;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonGenerate;
		private System.Windows.Forms.ToolStripMenuItem x10ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x15ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x20ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x25ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x30ToolStripMenuItem;
	}
}

