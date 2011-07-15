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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelSettings = new System.Windows.Forms.Panel();
			this.dataGridViewReport = new System.Windows.Forms.DataGridView();
			this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnFitness = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnComplexity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBoxLabyrinth = new System.Windows.Forms.GroupBox();
			this.labelMazeGenerator = new System.Windows.Forms.Label();
			this.comboBoxGenerator = new System.Windows.Forms.ComboBox();
			this.buttonRegenerate = new System.Windows.Forms.Button();
			this.comboBoxSize = new System.Windows.Forms.ComboBox();
			this.labelDimension = new System.Windows.Forms.Label();
			this.groupBoxGeneration = new System.Windows.Forms.GroupBox();
			this.labelGenerationSize = new System.Windows.Forms.Label();
			this.labelRuntime = new System.Windows.Forms.Label();
			this.numericUpDownRuntime = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownGenerationSize = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelInformation = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelGeneration = new System.Windows.Forms.Label();
			this.buttonRun = new System.Windows.Forms.Button();
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
			this.mazePanel = new MazeEvolution.MazePanel();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
			this.groupBoxLabyrinth.SuspendLayout();
			this.groupBoxGeneration.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRuntime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerationSize)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelInformation.SuspendLayout();
			this.panel1.SuspendLayout();
			this.toolStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 521);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(711, 22);
			this.statusStrip.TabIndex = 0;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(711, 496);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(711, 521);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripMain);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.75668F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.24332F));
			this.tableLayoutPanel.Controls.Add(this.panelSettings, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.tableLayoutPanel1, 1, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 496F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(711, 496);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// panelSettings
			// 
			this.panelSettings.Controls.Add(this.dataGridViewReport);
			this.panelSettings.Controls.Add(this.groupBoxLabyrinth);
			this.panelSettings.Controls.Add(this.groupBoxGeneration);
			this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSettings.Location = new System.Drawing.Point(3, 3);
			this.panelSettings.Name = "panelSettings";
			this.panelSettings.Size = new System.Drawing.Size(297, 490);
			this.panelSettings.TabIndex = 0;
			// 
			// dataGridViewReport
			// 
			this.dataGridViewReport.AllowUserToAddRows = false;
			this.dataGridViewReport.AllowUserToDeleteRows = false;
			this.dataGridViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnId,
            this.columnFitness,
            this.columnComplexity,
            this.columnAge,
            this.columnState});
			this.dataGridViewReport.Location = new System.Drawing.Point(9, 223);
			this.dataGridViewReport.Name = "dataGridViewReport";
			this.dataGridViewReport.ReadOnly = true;
			this.dataGridViewReport.RowHeadersVisible = false;
			this.dataGridViewReport.ShowEditingIcon = false;
			this.dataGridViewReport.Size = new System.Drawing.Size(285, 264);
			this.dataGridViewReport.TabIndex = 16;
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
			// groupBoxLabyrinth
			// 
			this.groupBoxLabyrinth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxLabyrinth.Controls.Add(this.labelMazeGenerator);
			this.groupBoxLabyrinth.Controls.Add(this.comboBoxGenerator);
			this.groupBoxLabyrinth.Controls.Add(this.buttonRegenerate);
			this.groupBoxLabyrinth.Controls.Add(this.comboBoxSize);
			this.groupBoxLabyrinth.Controls.Add(this.labelDimension);
			this.groupBoxLabyrinth.Location = new System.Drawing.Point(9, 99);
			this.groupBoxLabyrinth.Name = "groupBoxLabyrinth";
			this.groupBoxLabyrinth.Size = new System.Drawing.Size(288, 118);
			this.groupBoxLabyrinth.TabIndex = 15;
			this.groupBoxLabyrinth.TabStop = false;
			this.groupBoxLabyrinth.Text = "Labyrinth";
			// 
			// labelMazeGenerator
			// 
			this.labelMazeGenerator.AutoSize = true;
			this.labelMazeGenerator.Location = new System.Drawing.Point(6, 26);
			this.labelMazeGenerator.Name = "labelMazeGenerator";
			this.labelMazeGenerator.Size = new System.Drawing.Size(54, 13);
			this.labelMazeGenerator.TabIndex = 11;
			this.labelMazeGenerator.Text = "Generator";
			// 
			// comboBoxGenerator
			// 
			this.comboBoxGenerator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxGenerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGenerator.FormattingEnabled = true;
			this.comboBoxGenerator.Items.AddRange(new object[] {
            "Recursive Backtracker",
            "Randomized Prim",
            "Randomized Kruskal"});
			this.comboBoxGenerator.Location = new System.Drawing.Point(87, 26);
			this.comboBoxGenerator.Name = "comboBoxGenerator";
			this.comboBoxGenerator.Size = new System.Drawing.Size(184, 21);
			this.comboBoxGenerator.TabIndex = 9;
			this.comboBoxGenerator.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGeneratorSelectedIndexChanged);
			// 
			// buttonRegenerate
			// 
			this.buttonRegenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRegenerate.Location = new System.Drawing.Point(87, 79);
			this.buttonRegenerate.Name = "buttonRegenerate";
			this.buttonRegenerate.Size = new System.Drawing.Size(185, 25);
			this.buttonRegenerate.TabIndex = 13;
			this.buttonRegenerate.Text = "&Regenerate";
			this.buttonRegenerate.UseVisualStyleBackColor = true;
			this.buttonRegenerate.Click += new System.EventHandler(this.ButtonRegenerateClick);
			// 
			// comboBoxSize
			// 
			this.comboBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSize.FormattingEnabled = true;
			this.comboBoxSize.Items.AddRange(new object[] {
            "10x10",
            "15x15",
            "20x20",
            "30x30",
            "40x40"});
			this.comboBoxSize.Location = new System.Drawing.Point(87, 52);
			this.comboBoxSize.Name = "comboBoxSize";
			this.comboBoxSize.Size = new System.Drawing.Size(184, 21);
			this.comboBoxSize.TabIndex = 10;
			this.comboBoxSize.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSizeSelectedIndexChanged);
			// 
			// labelDimension
			// 
			this.labelDimension.AutoSize = true;
			this.labelDimension.Location = new System.Drawing.Point(6, 52);
			this.labelDimension.Name = "labelDimension";
			this.labelDimension.Size = new System.Drawing.Size(56, 13);
			this.labelDimension.TabIndex = 12;
			this.labelDimension.Text = "Dimension";
			// 
			// groupBoxGeneration
			// 
			this.groupBoxGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGeneration.Controls.Add(this.labelGenerationSize);
			this.groupBoxGeneration.Controls.Add(this.labelRuntime);
			this.groupBoxGeneration.Controls.Add(this.numericUpDownRuntime);
			this.groupBoxGeneration.Controls.Add(this.numericUpDownGenerationSize);
			this.groupBoxGeneration.Location = new System.Drawing.Point(9, 3);
			this.groupBoxGeneration.Name = "groupBoxGeneration";
			this.groupBoxGeneration.Size = new System.Drawing.Size(288, 90);
			this.groupBoxGeneration.TabIndex = 14;
			this.groupBoxGeneration.TabStop = false;
			this.groupBoxGeneration.Text = "Generation";
			// 
			// labelGenerationSize
			// 
			this.labelGenerationSize.AutoSize = true;
			this.labelGenerationSize.Location = new System.Drawing.Point(6, 30);
			this.labelGenerationSize.Name = "labelGenerationSize";
			this.labelGenerationSize.Size = new System.Drawing.Size(56, 13);
			this.labelGenerationSize.TabIndex = 6;
			this.labelGenerationSize.Text = "Individuen";
			// 
			// labelRuntime
			// 
			this.labelRuntime.AutoSize = true;
			this.labelRuntime.Location = new System.Drawing.Point(6, 56);
			this.labelRuntime.Name = "labelRuntime";
			this.labelRuntime.Size = new System.Drawing.Size(75, 13);
			this.labelRuntime.TabIndex = 8;
			this.labelRuntime.Text = "Laufzeit (Sek.)";
			// 
			// numericUpDownRuntime
			// 
			this.numericUpDownRuntime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownRuntime.Location = new System.Drawing.Point(87, 54);
			this.numericUpDownRuntime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.numericUpDownRuntime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownRuntime.Name = "numericUpDownRuntime";
			this.numericUpDownRuntime.Size = new System.Drawing.Size(184, 20);
			this.numericUpDownRuntime.TabIndex = 7;
			this.numericUpDownRuntime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownRuntime.ValueChanged += new System.EventHandler(this.NumericUpDownRuntimeValueChanged);
			// 
			// numericUpDownGenerationSize
			// 
			this.numericUpDownGenerationSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownGenerationSize.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numericUpDownGenerationSize.Location = new System.Drawing.Point(87, 28);
			this.numericUpDownGenerationSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownGenerationSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDownGenerationSize.Name = "numericUpDownGenerationSize";
			this.numericUpDownGenerationSize.Size = new System.Drawing.Size(184, 20);
			this.numericUpDownGenerationSize.TabIndex = 5;
			this.numericUpDownGenerationSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numericUpDownGenerationSize.ValueChanged += new System.EventHandler(this.NumericUpDownGenerationSizeValueChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.panelInformation, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(306, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.63265F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.36735F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 490);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// panelInformation
			// 
			this.panelInformation.Controls.Add(this.mazePanel);
			this.panelInformation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelInformation.Location = new System.Drawing.Point(3, 108);
			this.panelInformation.Name = "panelInformation";
			this.panelInformation.Size = new System.Drawing.Size(391, 379);
			this.panelInformation.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.labelGeneration);
			this.panel1.Controls.Add(this.buttonRun);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(391, 99);
			this.panel1.TabIndex = 4;
			// 
			// labelGeneration
			// 
			this.labelGeneration.AutoSize = true;
			this.labelGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelGeneration.Location = new System.Drawing.Point(116, 30);
			this.labelGeneration.Name = "labelGeneration";
			this.labelGeneration.Size = new System.Drawing.Size(51, 37);
			this.labelGeneration.TabIndex = 1;
			this.labelGeneration.Text = "#1";
			// 
			// buttonRun
			// 
			this.buttonRun.Location = new System.Drawing.Point(19, 25);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(91, 44);
			this.buttonRun.TabIndex = 0;
			this.buttonRun.Text = "&Start";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.ButtonRunClick);
			// 
			// toolStripMain
			// 
			this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExit});
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.Size = new System.Drawing.Size(711, 25);
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
			// mazePanel
			// 
			this.mazePanel.DisableMouseClickInteractions = false;
			this.mazePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mazePanel.Location = new System.Drawing.Point(0, 0);
			this.mazePanel.Name = "mazePanel";
			this.mazePanel.Size = new System.Drawing.Size(391, 379);
			this.mazePanel.TabIndex = 3;
			// 
			// Testbed
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(711, 543);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.statusStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Testbed";
			this.Text = "Testbed";
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.panelSettings.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
			this.groupBoxLabyrinth.ResumeLayout(false);
			this.groupBoxLabyrinth.PerformLayout();
			this.groupBoxGeneration.ResumeLayout(false);
			this.groupBoxGeneration.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRuntime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerationSize)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelInformation.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelSettings;
		private System.Windows.Forms.Label labelGenerationSize;
		private System.Windows.Forms.NumericUpDown numericUpDownGenerationSize;
		private System.Windows.Forms.NumericUpDown numericUpDownRuntime;
		private System.Windows.Forms.Label labelRuntime;
		private System.Windows.Forms.Label labelDimension;
		private System.Windows.Forms.ComboBox comboBoxSize;
		private System.Windows.Forms.ComboBox comboBoxGenerator;
		private System.Windows.Forms.Label labelMazeGenerator;
		private System.Windows.Forms.GroupBox groupBoxLabyrinth;
		private System.Windows.Forms.Button buttonRegenerate;
		private System.Windows.Forms.GroupBox groupBoxGeneration;
		private System.Windows.Forms.DataGridView dataGridViewReport;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnFitness;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnComplexity;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnAge;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnState;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panelInformation;
		private MazePanel mazePanel;
		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripButton toolStripButtonExit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelGeneration;
		private System.Windows.Forms.Button buttonRun;
	}
}