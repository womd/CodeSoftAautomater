namespace codesofttool
{
    partial class Form1
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
            this.buttonSaveSampleXml = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxSourcesFolder = new System.Windows.Forms.TextBox();
            this.btnSetWatchFolder = new System.Windows.Forms.Button();
            this.textBoxJobFilePattern = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewLogs = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLabFilesPath = new System.Windows.Forms.TextBox();
            this.btnSetLabFolder = new System.Windows.Forms.Button();
            this.textBoxPrinter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelectPrinter = new System.Windows.Forms.Button();
            this.checkBoxMTest = new System.Windows.Forms.CheckBox();
            this.groupBoxMtest = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).BeginInit();
            this.groupBoxMtest.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSaveSampleXml
            // 
            this.buttonSaveSampleXml.Location = new System.Drawing.Point(446, 74);
            this.buttonSaveSampleXml.Name = "buttonSaveSampleXml";
            this.buttonSaveSampleXml.Size = new System.Drawing.Size(97, 23);
            this.buttonSaveSampleXml.TabIndex = 1;
            this.buttonSaveSampleXml.Text = "save sample xml";
            this.buttonSaveSampleXml.UseVisualStyleBackColor = true;
            this.buttonSaveSampleXml.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxSourcesFolder
            // 
            this.textBoxSourcesFolder.Location = new System.Drawing.Point(130, 6);
            this.textBoxSourcesFolder.Name = "textBoxSourcesFolder";
            this.textBoxSourcesFolder.Size = new System.Drawing.Size(279, 20);
            this.textBoxSourcesFolder.TabIndex = 2;
            this.textBoxSourcesFolder.TextChanged += new System.EventHandler(this.textBoxSourcesFolder_TextChanged);
            // 
            // btnSetWatchFolder
            // 
            this.btnSetWatchFolder.Location = new System.Drawing.Point(415, 4);
            this.btnSetWatchFolder.Name = "btnSetWatchFolder";
            this.btnSetWatchFolder.Size = new System.Drawing.Size(128, 23);
            this.btnSetWatchFolder.TabIndex = 3;
            this.btnSetWatchFolder.Text = "select";
            this.btnSetWatchFolder.UseVisualStyleBackColor = true;
            this.btnSetWatchFolder.Click += new System.EventHandler(this.btnSetFolder_Click);
            // 
            // textBoxJobFilePattern
            // 
            this.textBoxJobFilePattern.Location = new System.Drawing.Point(95, 76);
            this.textBoxJobFilePattern.Name = "textBoxJobFilePattern";
            this.textBoxJobFilePattern.Size = new System.Drawing.Size(100, 20);
            this.textBoxJobFilePattern.TabIndex = 4;
            this.textBoxJobFilePattern.TextChanged += new System.EventHandler(this.textBoxJobFilePattern_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "watch for changes in:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "job-file-pattern:";
            // 
            // dataGridViewLogs
            // 
            this.dataGridViewLogs.AllowUserToAddRows = false;
            this.dataGridViewLogs.AllowUserToDeleteRows = false;
            this.dataGridViewLogs.AllowUserToOrderColumns = true;
            this.dataGridViewLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLogs.ColumnHeadersVisible = false;
            this.dataGridViewLogs.Location = new System.Drawing.Point(0, 124);
            this.dataGridViewLogs.MaximumSize = new System.Drawing.Size(548, 146);
            this.dataGridViewLogs.MinimumSize = new System.Drawing.Size(548, 146);
            this.dataGridViewLogs.Name = "dataGridViewLogs";
            this.dataGridViewLogs.Size = new System.Drawing.Size(548, 146);
            this.dataGridViewLogs.TabIndex = 7;
            this.dataGridViewLogs.VirtualMode = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "search for *.lab files in:";
            // 
            // textBoxLabFilesPath
            // 
            this.textBoxLabFilesPath.Location = new System.Drawing.Point(130, 28);
            this.textBoxLabFilesPath.Name = "textBoxLabFilesPath";
            this.textBoxLabFilesPath.Size = new System.Drawing.Size(279, 20);
            this.textBoxLabFilesPath.TabIndex = 9;
            // 
            // btnSetLabFolder
            // 
            this.btnSetLabFolder.Location = new System.Drawing.Point(415, 26);
            this.btnSetLabFolder.Name = "btnSetLabFolder";
            this.btnSetLabFolder.Size = new System.Drawing.Size(128, 23);
            this.btnSetLabFolder.TabIndex = 10;
            this.btnSetLabFolder.Text = "select";
            this.btnSetLabFolder.UseVisualStyleBackColor = true;
            this.btnSetLabFolder.Click += new System.EventHandler(this.btnSelectLabFiles);
            // 
            // textBoxPrinter
            // 
            this.textBoxPrinter.Location = new System.Drawing.Point(130, 51);
            this.textBoxPrinter.Name = "textBoxPrinter";
            this.textBoxPrinter.Size = new System.Drawing.Size(279, 20);
            this.textBoxPrinter.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "use printer:";
            // 
            // buttonSelectPrinter
            // 
            this.buttonSelectPrinter.Location = new System.Drawing.Point(415, 48);
            this.buttonSelectPrinter.Name = "buttonSelectPrinter";
            this.buttonSelectPrinter.Size = new System.Drawing.Size(128, 23);
            this.buttonSelectPrinter.TabIndex = 13;
            this.buttonSelectPrinter.Text = "select";
            this.buttonSelectPrinter.UseVisualStyleBackColor = true;
            this.buttonSelectPrinter.Click += new System.EventHandler(this.buttonSelectPrinter_Click);
            // 
            // checkBoxMTest
            // 
            this.checkBoxMTest.AutoSize = true;
            this.checkBoxMTest.Location = new System.Drawing.Point(6, 19);
            this.checkBoxMTest.Name = "checkBoxMTest";
            this.checkBoxMTest.Size = new System.Drawing.Size(55, 17);
            this.checkBoxMTest.TabIndex = 14;
            this.checkBoxMTest.Text = "on/off";
            this.checkBoxMTest.UseVisualStyleBackColor = true;
            this.checkBoxMTest.CheckedChanged += new System.EventHandler(this.checkBoxMTest_CheckedChanged);
            // 
            // groupBoxMtest
            // 
            this.groupBoxMtest.Controls.Add(this.checkBoxMTest);
            this.groupBoxMtest.Location = new System.Drawing.Point(201, 74);
            this.groupBoxMtest.Name = "groupBoxMtest";
            this.groupBoxMtest.Size = new System.Drawing.Size(239, 45);
            this.groupBoxMtest.TabIndex = 15;
            this.groupBoxMtest.TabStop = false;
            this.groupBoxMtest.Text = "mTest";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 271);
            this.Controls.Add(this.groupBoxMtest);
            this.Controls.Add(this.buttonSelectPrinter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPrinter);
            this.Controls.Add(this.btnSetLabFolder);
            this.Controls.Add(this.textBoxLabFilesPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewLogs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxJobFilePattern);
            this.Controls.Add(this.btnSetWatchFolder);
            this.Controls.Add(this.textBoxSourcesFolder);
            this.Controls.Add(this.buttonSaveSampleXml);
            this.MaximumSize = new System.Drawing.Size(564, 310);
            this.MinimumSize = new System.Drawing.Size(564, 310);
            this.Name = "Form1";
            this.Text = "CodeSoftTool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).EndInit();
            this.groupBoxMtest.ResumeLayout(false);
            this.groupBoxMtest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSaveSampleXml;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox textBoxSourcesFolder;
        private System.Windows.Forms.Button btnSetWatchFolder;
        private System.Windows.Forms.TextBox textBoxJobFilePattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewLogs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLabFilesPath;
        private System.Windows.Forms.Button btnSetLabFolder;
        private System.Windows.Forms.TextBox textBoxPrinter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSelectPrinter;
        private System.Windows.Forms.CheckBox checkBoxMTest;
        private System.Windows.Forms.GroupBox groupBoxMtest;
    }
}

