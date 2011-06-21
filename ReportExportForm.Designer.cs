namespace PT_Sguil
{
    partial class ReportExportForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.chkbReportSanitized = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbReportLevelSummary = new System.Windows.Forms.RadioButton();
            this.rbReportLevelDetailed = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bwReportGen = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.84163F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.15837F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbReportType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkbReportSanitized, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(221, 74);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Detail Level:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sanitized:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "Human-Readable"});
            this.cbReportType.Location = new System.Drawing.Point(80, 3);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(138, 21);
            this.cbReportType.TabIndex = 3;
            // 
            // chkbReportSanitized
            // 
            this.chkbReportSanitized.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkbReportSanitized.AutoSize = true;
            this.chkbReportSanitized.Location = new System.Drawing.Point(80, 55);
            this.chkbReportSanitized.Name = "chkbReportSanitized";
            this.chkbReportSanitized.Size = new System.Drawing.Size(15, 14);
            this.chkbReportSanitized.TabIndex = 4;
            this.chkbReportSanitized.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbReportLevelSummary);
            this.panel1.Controls.Add(this.rbReportLevelDetailed);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(80, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 19);
            this.panel1.TabIndex = 5;
            // 
            // rbReportLevelSummary
            // 
            this.rbReportLevelSummary.AutoSize = true;
            this.rbReportLevelSummary.Location = new System.Drawing.Point(69, 1);
            this.rbReportLevelSummary.Name = "rbReportLevelSummary";
            this.rbReportLevelSummary.Size = new System.Drawing.Size(68, 17);
            this.rbReportLevelSummary.TabIndex = 1;
            this.rbReportLevelSummary.Text = "Summary";
            this.rbReportLevelSummary.UseVisualStyleBackColor = true;
            // 
            // rbReportLevelDetailed
            // 
            this.rbReportLevelDetailed.AutoSize = true;
            this.rbReportLevelDetailed.Checked = true;
            this.rbReportLevelDetailed.Location = new System.Drawing.Point(4, 1);
            this.rbReportLevelDetailed.Name = "rbReportLevelDetailed";
            this.rbReportLevelDetailed.Size = new System.Drawing.Size(64, 17);
            this.rbReportLevelDetailed.TabIndex = 0;
            this.rbReportLevelDetailed.TabStop = true;
            this.rbReportLevelDetailed.Text = "Detailed";
            this.rbReportLevelDetailed.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(48, 190);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(130, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(48, 153);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(157, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // bwReportGen
            // 
            this.bwReportGen.WorkerReportsProgress = true;
            this.bwReportGen.WorkerSupportsCancellation = true;
            this.bwReportGen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwReportGen_DoWork);
            this.bwReportGen.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwReportGen_ProgressChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Text files|*.txt|All files|*.*";
            this.saveFileDialog1.Title = "Save Report File...";
            // 
            // frmReportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(246, 231);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmReportExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Events";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.CheckBox chkbReportSanitized;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbReportLevelSummary;
        private System.Windows.Forms.RadioButton rbReportLevelDetailed;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bwReportGen;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}