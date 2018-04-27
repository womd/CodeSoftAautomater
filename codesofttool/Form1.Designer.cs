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
            this.btnSetFolder = new System.Windows.Forms.Button();
            this.textBoxJobFilePattern = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSaveSampleXml
            // 
            this.buttonSaveSampleXml.Location = new System.Drawing.Point(415, 37);
            this.buttonSaveSampleXml.Name = "buttonSaveSampleXml";
            this.buttonSaveSampleXml.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveSampleXml.TabIndex = 1;
            this.buttonSaveSampleXml.Text = "save sample xml";
            this.buttonSaveSampleXml.UseVisualStyleBackColor = true;
            this.buttonSaveSampleXml.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxSourcesFolder
            // 
            this.textBoxSourcesFolder.Location = new System.Drawing.Point(91, 13);
            this.textBoxSourcesFolder.Name = "textBoxSourcesFolder";
            this.textBoxSourcesFolder.Size = new System.Drawing.Size(318, 20);
            this.textBoxSourcesFolder.TabIndex = 2;
            // 
            // btnSetFolder
            // 
            this.btnSetFolder.Location = new System.Drawing.Point(415, 10);
            this.btnSetFolder.Name = "btnSetFolder";
            this.btnSetFolder.Size = new System.Drawing.Size(128, 23);
            this.btnSetFolder.TabIndex = 3;
            this.btnSetFolder.Text = "Ordner auswählen";
            this.btnSetFolder.UseVisualStyleBackColor = true;
            this.btnSetFolder.Click += new System.EventHandler(this.btnSetFolder_Click);
            // 
            // textBoxJobFilePattern
            // 
            this.textBoxJobFilePattern.Location = new System.Drawing.Point(91, 39);
            this.textBoxJobFilePattern.Name = "textBoxJobFilePattern";
            this.textBoxJobFilePattern.Size = new System.Drawing.Size(100, 20);
            this.textBoxJobFilePattern.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Quelle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Job-FilePattern";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(16, 77);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 121);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxJobFilePattern);
            this.Controls.Add(this.btnSetFolder);
            this.Controls.Add(this.textBoxSourcesFolder);
            this.Controls.Add(this.buttonSaveSampleXml);
            this.MaximumSize = new System.Drawing.Size(584, 160);
            this.MinimumSize = new System.Drawing.Size(584, 160);
            this.Name = "Form1";
            this.Text = "CodeSoftTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSaveSampleXml;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox textBoxSourcesFolder;
        private System.Windows.Forms.Button btnSetFolder;
        private System.Windows.Forms.TextBox textBoxJobFilePattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelStatus;
    }
}

