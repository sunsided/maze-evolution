namespace MazeEvolution
{
    partial class GenerationReport
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
            this.buttonStartRun = new System.Windows.Forms.Button();
            this.numericUpDownGenerationSize = new System.Windows.Forms.NumericUpDown();
            this.labelGenerationSize = new System.Windows.Forms.Label();
            this.labelRuntime = new System.Windows.Forms.Label();
            this.numericUpDownRuntime = new System.Windows.Forms.NumericUpDown();
            this.comboBoxGenerator = new System.Windows.Forms.ComboBox();
            this.comboBoxSize = new System.Windows.Forms.ComboBox();
            this.labelMazeGenerator = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxGenerationSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoRepeat = new System.Windows.Forms.CheckBox();
            this.groupBoxReport = new System.Windows.Forms.GroupBox();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFitness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnComplexity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRegenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerationSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRuntime)).BeginInit();
            this.groupBoxGenerationSettings.SuspendLayout();
            this.groupBoxReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartRun
            // 
            this.buttonStartRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartRun.Location = new System.Drawing.Point(640, 23);
            this.buttonStartRun.Name = "buttonStartRun";
            this.buttonStartRun.Size = new System.Drawing.Size(136, 46);
            this.buttonStartRun.TabIndex = 0;
            this.buttonStartRun.Text = "Durchlauf &starten";
            this.buttonStartRun.UseVisualStyleBackColor = true;
            this.buttonStartRun.Click += new System.EventHandler(this.ButtonStartRunClick);
            // 
            // numericUpDownGenerationSize
            // 
            this.numericUpDownGenerationSize.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownGenerationSize.Location = new System.Drawing.Point(125, 26);
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
            this.numericUpDownGenerationSize.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownGenerationSize.TabIndex = 1;
            this.numericUpDownGenerationSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownGenerationSize.ValueChanged += new System.EventHandler(this.NumericUpDownGenerationSizeValueChanged);
            // 
            // labelGenerationSize
            // 
            this.labelGenerationSize.AutoSize = true;
            this.labelGenerationSize.Location = new System.Drawing.Point(17, 28);
            this.labelGenerationSize.Name = "labelGenerationSize";
            this.labelGenerationSize.Size = new System.Drawing.Size(91, 13);
            this.labelGenerationSize.TabIndex = 2;
            this.labelGenerationSize.Text = "Generationsgröße";
            // 
            // labelRuntime
            // 
            this.labelRuntime.AutoSize = true;
            this.labelRuntime.Location = new System.Drawing.Point(17, 54);
            this.labelRuntime.Name = "labelRuntime";
            this.labelRuntime.Size = new System.Drawing.Size(102, 13);
            this.labelRuntime.TabIndex = 4;
            this.labelRuntime.Text = "Laufzeit (Sekunden)";
            // 
            // numericUpDownRuntime
            // 
            this.numericUpDownRuntime.Location = new System.Drawing.Point(125, 52);
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
            this.numericUpDownRuntime.Size = new System.Drawing.Size(61, 20);
            this.numericUpDownRuntime.TabIndex = 3;
            this.numericUpDownRuntime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRuntime.ValueChanged += new System.EventHandler(this.NumericUpDownRuntimeValueChanged);
            // 
            // comboBoxGenerator
            // 
            this.comboBoxGenerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGenerator.FormattingEnabled = true;
            this.comboBoxGenerator.Items.AddRange(new object[] {
            "Recursive Backtracker",
            "Randomized Prim",
            "Randomized Kruskal"});
            this.comboBoxGenerator.Location = new System.Drawing.Point(291, 25);
            this.comboBoxGenerator.Name = "comboBoxGenerator";
            this.comboBoxGenerator.Size = new System.Drawing.Size(151, 21);
            this.comboBoxGenerator.TabIndex = 5;
            this.comboBoxGenerator.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGeneratorSelectedIndexChanged);
            // 
            // comboBoxSize
            // 
            this.comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSize.FormattingEnabled = true;
            this.comboBoxSize.Items.AddRange(new object[] {
            "10x10",
            "15x15",
            "20x20",
            "30x30",
            "40x40"});
            this.comboBoxSize.Location = new System.Drawing.Point(291, 51);
            this.comboBoxSize.Name = "comboBoxSize";
            this.comboBoxSize.Size = new System.Drawing.Size(151, 21);
            this.comboBoxSize.TabIndex = 6;
            this.comboBoxSize.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSizeSelectedIndexChanged);
            // 
            // labelMazeGenerator
            // 
            this.labelMazeGenerator.AutoSize = true;
            this.labelMazeGenerator.Location = new System.Drawing.Point(214, 28);
            this.labelMazeGenerator.Name = "labelMazeGenerator";
            this.labelMazeGenerator.Size = new System.Drawing.Size(54, 13);
            this.labelMazeGenerator.TabIndex = 7;
            this.labelMazeGenerator.Text = "Generator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dimensionen";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 115);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(764, 23);
            this.progressBar.TabIndex = 9;
            // 
            // groupBoxGenerationSettings
            // 
            this.groupBoxGenerationSettings.Controls.Add(this.buttonRegenerate);
            this.groupBoxGenerationSettings.Controls.Add(this.labelGenerationSize);
            this.groupBoxGenerationSettings.Controls.Add(this.numericUpDownGenerationSize);
            this.groupBoxGenerationSettings.Controls.Add(this.label1);
            this.groupBoxGenerationSettings.Controls.Add(this.comboBoxSize);
            this.groupBoxGenerationSettings.Controls.Add(this.comboBoxGenerator);
            this.groupBoxGenerationSettings.Controls.Add(this.numericUpDownRuntime);
            this.groupBoxGenerationSettings.Controls.Add(this.labelMazeGenerator);
            this.groupBoxGenerationSettings.Controls.Add(this.labelRuntime);
            this.groupBoxGenerationSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxGenerationSettings.Name = "groupBoxGenerationSettings";
            this.groupBoxGenerationSettings.Size = new System.Drawing.Size(601, 87);
            this.groupBoxGenerationSettings.TabIndex = 10;
            this.groupBoxGenerationSettings.TabStop = false;
            this.groupBoxGenerationSettings.Text = "Einstellungen für nächste Generation";
            // 
            // checkBoxAutoRepeat
            // 
            this.checkBoxAutoRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoRepeat.AutoSize = true;
            this.checkBoxAutoRepeat.Enabled = false;
            this.checkBoxAutoRepeat.Location = new System.Drawing.Point(640, 75);
            this.checkBoxAutoRepeat.Name = "checkBoxAutoRepeat";
            this.checkBoxAutoRepeat.Size = new System.Drawing.Size(144, 17);
            this.checkBoxAutoRepeat.TabIndex = 11;
            this.checkBoxAutoRepeat.Text = "Automatisch &wiederholen";
            this.checkBoxAutoRepeat.UseVisualStyleBackColor = true;
            // 
            // groupBoxReport
            // 
            this.groupBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxReport.Controls.Add(this.textBoxCode);
            this.groupBoxReport.Controls.Add(this.dataGridViewReport);
            this.groupBoxReport.Location = new System.Drawing.Point(12, 154);
            this.groupBoxReport.Name = "groupBoxReport";
            this.groupBoxReport.Size = new System.Drawing.Size(764, 380);
            this.groupBoxReport.TabIndex = 12;
            this.groupBoxReport.TabStop = false;
            this.groupBoxReport.Text = "Report";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCode.Location = new System.Drawing.Point(291, 30);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.Size = new System.Drawing.Size(456, 330);
            this.textBoxCode.TabIndex = 14;
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnId,
            this.columnFitness,
            this.columnComplexity,
            this.columnAge,
            this.columnState});
            this.dataGridViewReport.Location = new System.Drawing.Point(20, 30);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.RowHeadersVisible = false;
            this.dataGridViewReport.ShowEditingIcon = false;
            this.dataGridViewReport.Size = new System.Drawing.Size(262, 330);
            this.dataGridViewReport.TabIndex = 13;
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
            // buttonRegenerate
            // 
            this.buttonRegenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRegenerate.Location = new System.Drawing.Point(464, 33);
            this.buttonRegenerate.Name = "buttonRegenerate";
            this.buttonRegenerate.Size = new System.Drawing.Size(115, 30);
            this.buttonRegenerate.TabIndex = 9;
            this.buttonRegenerate.Text = "&Regenerate";
            this.buttonRegenerate.UseVisualStyleBackColor = true;
            this.buttonRegenerate.Click += new System.EventHandler(this.ButtonRegenerateClick);
            // 
            // GenerationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 546);
            this.Controls.Add(this.groupBoxReport);
            this.Controls.Add(this.checkBoxAutoRepeat);
            this.Controls.Add(this.groupBoxGenerationSettings);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonStartRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerationReport";
            this.Text = "Generation Report";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGenerationSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRuntime)).EndInit();
            this.groupBoxGenerationSettings.ResumeLayout(false);
            this.groupBoxGenerationSettings.PerformLayout();
            this.groupBoxReport.ResumeLayout(false);
            this.groupBoxReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartRun;
        private System.Windows.Forms.NumericUpDown numericUpDownGenerationSize;
        private System.Windows.Forms.Label labelGenerationSize;
        private System.Windows.Forms.Label labelRuntime;
        private System.Windows.Forms.NumericUpDown numericUpDownRuntime;
        private System.Windows.Forms.ComboBox comboBoxGenerator;
        private System.Windows.Forms.ComboBox comboBoxSize;
        private System.Windows.Forms.Label labelMazeGenerator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBoxGenerationSettings;
        private System.Windows.Forms.CheckBox checkBoxAutoRepeat;
        private System.Windows.Forms.GroupBox groupBoxReport;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFitness;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComplexity;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnState;
        private System.Windows.Forms.Button buttonRegenerate;

    }
}