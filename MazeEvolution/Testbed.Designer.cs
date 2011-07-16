using MazeEvolution.Properties;

namespace MazeEvolution
{
	partial class Testbed
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Testbed));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelGeneration = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelSpring = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripTimeProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.groupBoxLastGeneration = new System.Windows.Forms.GroupBox();
			this.labelComplexity = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelMinFitness = new System.Windows.Forms.Label();
			this.labelMaxFitness = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelBorn = new System.Windows.Forms.Label();
			this.labelCrossover = new System.Windows.Forms.Label();
			this.labelMutated = new System.Windows.Forms.Label();
			this.labelDeceased = new System.Windows.Forms.Label();
			this.labelSelected = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panelMazeBlock = new System.Windows.Forms.Panel();
			this.mazePanel = new MazeEvolution.MazePanel();
			this.panelMazeControl = new System.Windows.Forms.Panel();
			this.buttonRegenerateMaze = new System.Windows.Forms.Button();
			this.buttonConfigureMaze = new System.Windows.Forms.Button();
			this.dataGridViewReport = new System.Windows.Forms.DataGridView();
			this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnFitness = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnComplexity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAutoEvolve = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAbort = new System.Windows.Forms.ToolStripButton();
			this.statusStrip.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.groupBoxLastGeneration.SuspendLayout();
			this.panelMazeBlock.SuspendLayout();
			this.panelMazeControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
			this.toolStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelGeneration,
            this.toolStripStatusLabelSpring,
            this.toolStripTimeProgress});
			this.statusStrip.Location = new System.Drawing.Point(0, 411);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(663, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 0;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabelGeneration
			// 
			this.toolStripStatusLabelGeneration.Name = "toolStripStatusLabelGeneration";
			this.toolStripStatusLabelGeneration.Size = new System.Drawing.Size(103, 17);
			this.toolStripStatusLabelGeneration.Text = "Initiale Generation";
			// 
			// toolStripStatusLabelSpring
			// 
			this.toolStripStatusLabelSpring.Name = "toolStripStatusLabelSpring";
			this.toolStripStatusLabelSpring.Size = new System.Drawing.Size(412, 17);
			this.toolStripStatusLabelSpring.Spring = true;
			// 
			// toolStripTimeProgress
			// 
			this.toolStripTimeProgress.Name = "toolStripTimeProgress";
			this.toolStripTimeProgress.Size = new System.Drawing.Size(100, 16);
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.groupBoxLastGeneration);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMazeBlock);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGridViewReport);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(663, 386);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(663, 411);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripMain);
			// 
			// groupBoxLastGeneration
			// 
			this.groupBoxLastGeneration.Controls.Add(this.labelComplexity);
			this.groupBoxLastGeneration.Controls.Add(this.label8);
			this.groupBoxLastGeneration.Controls.Add(this.labelMinFitness);
			this.groupBoxLastGeneration.Controls.Add(this.labelMaxFitness);
			this.groupBoxLastGeneration.Controls.Add(this.label7);
			this.groupBoxLastGeneration.Controls.Add(this.label6);
			this.groupBoxLastGeneration.Controls.Add(this.labelBorn);
			this.groupBoxLastGeneration.Controls.Add(this.labelCrossover);
			this.groupBoxLastGeneration.Controls.Add(this.labelMutated);
			this.groupBoxLastGeneration.Controls.Add(this.labelDeceased);
			this.groupBoxLastGeneration.Controls.Add(this.labelSelected);
			this.groupBoxLastGeneration.Controls.Add(this.label5);
			this.groupBoxLastGeneration.Controls.Add(this.label4);
			this.groupBoxLastGeneration.Controls.Add(this.label3);
			this.groupBoxLastGeneration.Controls.Add(this.label2);
			this.groupBoxLastGeneration.Controls.Add(this.label1);
			this.groupBoxLastGeneration.Location = new System.Drawing.Point(401, 6);
			this.groupBoxLastGeneration.Name = "groupBoxLastGeneration";
			this.groupBoxLastGeneration.Size = new System.Drawing.Size(257, 129);
			this.groupBoxLastGeneration.TabIndex = 20;
			this.groupBoxLastGeneration.TabStop = false;
			this.groupBoxLastGeneration.Text = "Initiale Generation";
			// 
			// labelComplexity
			// 
			this.labelComplexity.AutoSize = true;
			this.labelComplexity.Location = new System.Drawing.Point(181, 106);
			this.labelComplexity.Name = "labelComplexity";
			this.labelComplexity.Size = new System.Drawing.Size(10, 13);
			this.labelComplexity.TabIndex = 15;
			this.labelComplexity.Text = "-";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(181, 93);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(69, 12);
			this.label8.TabIndex = 14;
			this.label8.Text = "Komplexität:";
			// 
			// labelMinFitness
			// 
			this.labelMinFitness.AutoSize = true;
			this.labelMinFitness.Location = new System.Drawing.Point(97, 106);
			this.labelMinFitness.Name = "labelMinFitness";
			this.labelMinFitness.Size = new System.Drawing.Size(10, 13);
			this.labelMinFitness.TabIndex = 13;
			this.labelMinFitness.Text = "-";
			// 
			// labelMaxFitness
			// 
			this.labelMaxFitness.AutoSize = true;
			this.labelMaxFitness.Location = new System.Drawing.Point(6, 106);
			this.labelMaxFitness.Name = "labelMaxFitness";
			this.labelMaxFitness.Size = new System.Drawing.Size(10, 13);
			this.labelMaxFitness.TabIndex = 12;
			this.labelMaxFitness.Text = "-";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(96, 93);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 12);
			this.label7.TabIndex = 11;
			this.label7.Text = "Min. Fitness:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(6, 93);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 12);
			this.label6.TabIndex = 10;
			this.label6.Text = "Max. Fitness:";
			// 
			// labelBorn
			// 
			this.labelBorn.AutoSize = true;
			this.labelBorn.Location = new System.Drawing.Point(181, 32);
			this.labelBorn.Name = "labelBorn";
			this.labelBorn.Size = new System.Drawing.Size(10, 13);
			this.labelBorn.TabIndex = 9;
			this.labelBorn.Text = "-";
			// 
			// labelCrossover
			// 
			this.labelCrossover.AutoSize = true;
			this.labelCrossover.Location = new System.Drawing.Point(97, 70);
			this.labelCrossover.Name = "labelCrossover";
			this.labelCrossover.Size = new System.Drawing.Size(10, 13);
			this.labelCrossover.TabIndex = 8;
			this.labelCrossover.Text = "-";
			// 
			// labelMutated
			// 
			this.labelMutated.AutoSize = true;
			this.labelMutated.Location = new System.Drawing.Point(6, 70);
			this.labelMutated.Name = "labelMutated";
			this.labelMutated.Size = new System.Drawing.Size(10, 13);
			this.labelMutated.TabIndex = 7;
			this.labelMutated.Text = "-";
			// 
			// labelDeceased
			// 
			this.labelDeceased.AutoSize = true;
			this.labelDeceased.Location = new System.Drawing.Point(97, 32);
			this.labelDeceased.Name = "labelDeceased";
			this.labelDeceased.Size = new System.Drawing.Size(10, 13);
			this.labelDeceased.TabIndex = 6;
			this.labelDeceased.Text = "-";
			// 
			// labelSelected
			// 
			this.labelSelected.AutoSize = true;
			this.labelSelected.Location = new System.Drawing.Point(6, 32);
			this.labelSelected.Name = "labelSelected";
			this.labelSelected.Size = new System.Drawing.Size(10, 13);
			this.labelSelected.TabIndex = 5;
			this.labelSelected.Text = "-";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(181, 19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "Neu erzeugt:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(96, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "Crossover:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(6, 57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "Mutation:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(96, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Verstorben:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Übernommen:";
			// 
			// panelMazeBlock
			// 
			this.panelMazeBlock.Controls.Add(this.mazePanel);
			this.panelMazeBlock.Controls.Add(this.panelMazeControl);
			this.panelMazeBlock.Location = new System.Drawing.Point(3, 3);
			this.panelMazeBlock.Name = "panelMazeBlock";
			this.panelMazeBlock.Size = new System.Drawing.Size(395, 379);
			this.panelMazeBlock.TabIndex = 19;
			// 
			// mazePanel
			// 
			this.mazePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mazePanel.DisableMouseClickInteractions = false;
			this.mazePanel.Location = new System.Drawing.Point(3, 32);
			this.mazePanel.Name = "mazePanel";
			this.mazePanel.Size = new System.Drawing.Size(389, 347);
			this.mazePanel.TabIndex = 3;
			// 
			// panelMazeControl
			// 
			this.panelMazeControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelMazeControl.Controls.Add(this.buttonRegenerateMaze);
			this.panelMazeControl.Controls.Add(this.buttonConfigureMaze);
			this.panelMazeControl.Location = new System.Drawing.Point(3, 3);
			this.panelMazeControl.Name = "panelMazeControl";
			this.panelMazeControl.Size = new System.Drawing.Size(389, 28);
			this.panelMazeControl.TabIndex = 18;
			// 
			// buttonRegenerateMaze
			// 
			this.buttonRegenerateMaze.Location = new System.Drawing.Point(0, 3);
			this.buttonRegenerateMaze.Name = "buttonRegenerateMaze";
			this.buttonRegenerateMaze.Size = new System.Drawing.Size(103, 21);
			this.buttonRegenerateMaze.TabIndex = 13;
			this.buttonRegenerateMaze.Text = "&Regenerate";
			this.buttonRegenerateMaze.UseVisualStyleBackColor = true;
			this.buttonRegenerateMaze.Click += new System.EventHandler(this.ButtonRegenerateClick);
			// 
			// buttonConfigureMaze
			// 
			this.buttonConfigureMaze.Location = new System.Drawing.Point(109, 3);
			this.buttonConfigureMaze.Name = "buttonConfigureMaze";
			this.buttonConfigureMaze.Size = new System.Drawing.Size(103, 21);
			this.buttonConfigureMaze.TabIndex = 17;
			this.buttonConfigureMaze.Text = "&Konfigurieren";
			this.buttonConfigureMaze.UseVisualStyleBackColor = true;
			this.buttonConfigureMaze.Click += new System.EventHandler(this.ButtonConfigureMazeClick);
			// 
			// dataGridViewReport
			// 
			this.dataGridViewReport.AllowUserToAddRows = false;
			this.dataGridViewReport.AllowUserToDeleteRows = false;
			this.dataGridViewReport.AllowUserToResizeRows = false;
			this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnId,
            this.columnFitness,
            this.columnComplexity,
            this.columnAge,
            this.columnState});
			this.dataGridViewReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewReport.Location = new System.Drawing.Point(401, 141);
			this.dataGridViewReport.MultiSelect = false;
			this.dataGridViewReport.Name = "dataGridViewReport";
			this.dataGridViewReport.ReadOnly = true;
			this.dataGridViewReport.RowHeadersVisible = false;
			this.dataGridViewReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewReport.ShowEditingIcon = false;
			this.dataGridViewReport.Size = new System.Drawing.Size(257, 239);
			this.dataGridViewReport.TabIndex = 16;
			this.dataGridViewReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewReportCellDoubleClick);
			// 
			// columnId
			// 
			this.columnId.HeaderText = "Id";
			this.columnId.Name = "columnId";
			this.columnId.ReadOnly = true;
			this.columnId.Width = 50;
			// 
			// columnFitness
			// 
			this.columnFitness.HeaderText = "Fitness";
			this.columnFitness.Name = "columnFitness";
			this.columnFitness.ReadOnly = true;
			this.columnFitness.Width = 50;
			// 
			// columnComplexity
			// 
			this.columnComplexity.HeaderText = "Komplexität";
			this.columnComplexity.Name = "columnComplexity";
			this.columnComplexity.ReadOnly = true;
			this.columnComplexity.Width = 50;
			// 
			// columnAge
			// 
			this.columnAge.HeaderText = "Alter";
			this.columnAge.Name = "columnAge";
			this.columnAge.ReadOnly = true;
			this.columnAge.Width = 50;
			// 
			// columnState
			// 
			this.columnState.FillWeight = 110F;
			this.columnState.HeaderText = "Status";
			this.columnState.Name = "columnState";
			this.columnState.ReadOnly = true;
			this.columnState.Width = 50;
			// 
			// toolStripMain
			// 
			this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExit,
            this.toolStripSeparator1,
            this.toolStripButtonSettings,
            this.toolStripSeparator2,
            this.toolStripButtonRun,
            this.toolStripButtonAutoEvolve,
            this.toolStripButtonAbort});
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.Size = new System.Drawing.Size(663, 25);
			this.toolStripMain.Stretch = true;
			this.toolStripMain.TabIndex = 0;
			// 
			// toolStripButtonExit
			// 
			this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExit.Image")));
			this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonExit.Name = "toolStripButtonExit";
			this.toolStripButtonExit.Size = new System.Drawing.Size(57, 22);
			this.toolStripButtonExit.Text = "&Beenden";
			this.toolStripButtonExit.Click += new System.EventHandler(this.ToolStripButtonExitClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonSettings
			// 
			this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButtonSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSettings.Image")));
			this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSettings.Name = "toolStripButtonSettings";
			this.toolStripButtonSettings.Size = new System.Drawing.Size(82, 22);
			this.toolStripButtonSettings.Text = "&Einstellungen";
			this.toolStripButtonSettings.Click += new System.EventHandler(this.ToolStripButtonSettingsClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonRun
			// 
			this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRun.Name = "toolStripButtonRun";
			this.toolStripButtonRun.Size = new System.Drawing.Size(102, 22);
			this.toolStripButtonRun.Text = "Durchlauf &starten";
			this.toolStripButtonRun.Click += new System.EventHandler(this.ToolStripButtonRunClick);
			// 
			// toolStripButtonAutoEvolve
			// 
			this.toolStripButtonAutoEvolve.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAutoEvolve.Name = "toolStripButtonAutoEvolve";
			this.toolStripButtonAutoEvolve.Size = new System.Drawing.Size(76, 22);
			this.toolStripButtonAutoEvolve.Text = "Auto-&Evolve";
			this.toolStripButtonAutoEvolve.Click += new System.EventHandler(this.ToolStripButtonAutoEvolveClick);
			// 
			// toolStripButtonAbort
			// 
			this.toolStripButtonAbort.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAbort.Name = "toolStripButtonAbort";
			this.toolStripButtonAbort.Size = new System.Drawing.Size(69, 22);
			this.toolStripButtonAbort.Text = "&Abbrechen";
			this.toolStripButtonAbort.Click += new System.EventHandler(this.ToolStripButtonAbortClick);
			// 
			// Testbed
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(663, 433);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.statusStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Testbed";
			this.Text = "Evolutionary Programming Testbed";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.groupBoxLastGeneration.ResumeLayout(false);
			this.groupBoxLastGeneration.PerformLayout();
			this.panelMazeBlock.ResumeLayout(false);
			this.panelMazeControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.Button buttonRegenerateMaze;
		private System.Windows.Forms.DataGridView dataGridViewReport;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnFitness;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnComplexity;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnAge;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnState;
		private MazePanel mazePanel;
		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripButton toolStripButtonExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonRun;
		private System.Windows.Forms.ToolStripButton toolStripButtonAutoEvolve;
		private System.Windows.Forms.ToolStripButton toolStripButtonAbort;
		private System.Windows.Forms.Button buttonConfigureMaze;
		private System.Windows.Forms.Panel panelMazeControl;
		private System.Windows.Forms.Panel panelMazeBlock;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGeneration;
		private System.Windows.Forms.GroupBox groupBoxLastGeneration;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelBorn;
		private System.Windows.Forms.Label labelCrossover;
		private System.Windows.Forms.Label labelMutated;
		private System.Windows.Forms.Label labelDeceased;
		private System.Windows.Forms.Label labelSelected;
		private System.Windows.Forms.Label labelMinFitness;
		private System.Windows.Forms.Label labelMaxFitness;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label labelComplexity;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripProgressBar toolStripTimeProgress;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpring;
	}
}