namespace PT_Sguil
{
    partial class FetchWireSharkPCAPForm
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
            this.btnWiresharkOk = new System.Windows.Forms.Button();
            this.lblPCAPFilename = new System.Windows.Forms.Label();
            this.lblFetchStatus = new System.Windows.Forms.Label();
            this.pbWiresharkDL = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnWiresharkOk
            // 
            this.btnWiresharkOk.Enabled = false;
            this.btnWiresharkOk.Location = new System.Drawing.Point(62, 105);
            this.btnWiresharkOk.Name = "btnWiresharkOk";
            this.btnWiresharkOk.Size = new System.Drawing.Size(75, 23);
            this.btnWiresharkOk.TabIndex = 7;
            this.btnWiresharkOk.TabStop = false;
            this.btnWiresharkOk.Text = "Ok";
            this.btnWiresharkOk.UseVisualStyleBackColor = true;
            this.btnWiresharkOk.Click += new System.EventHandler(this.btnWiresharkOk_Click);
            // 
            // lblPCAPFilename
            // 
            this.lblPCAPFilename.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPCAPFilename.Location = new System.Drawing.Point(11, 39);
            this.lblPCAPFilename.Name = "lblPCAPFilename";
            this.lblPCAPFilename.Size = new System.Drawing.Size(171, 27);
            this.lblPCAPFilename.TabIndex = 6;
            this.lblPCAPFilename.Text = "...";
            this.lblPCAPFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFetchStatus
            // 
            this.lblFetchStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFetchStatus.Location = new System.Drawing.Point(13, 12);
            this.lblFetchStatus.Name = "lblFetchStatus";
            this.lblFetchStatus.Size = new System.Drawing.Size(169, 27);
            this.lblFetchStatus.TabIndex = 5;
            this.lblFetchStatus.Text = "Fetching...";
            this.lblFetchStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbWiresharkDL
            // 
            this.pbWiresharkDL.Location = new System.Drawing.Point(11, 69);
            this.pbWiresharkDL.Name = "pbWiresharkDL";
            this.pbWiresharkDL.Size = new System.Drawing.Size(171, 23);
            this.pbWiresharkDL.TabIndex = 4;
            // 
            // frmFetchWireSharkPCAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 141);
            this.ControlBox = false;
            this.Controls.Add(this.btnWiresharkOk);
            this.Controls.Add(this.lblPCAPFilename);
            this.Controls.Add(this.lblFetchStatus);
            this.Controls.Add(this.pbWiresharkDL);
            this.Name = "frmFetchWireSharkPCAP";
            this.Text = "Fetch WireShark PCAP";
            this.Shown += new System.EventHandler(this.frmFetchWireSharkPCAP_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWiresharkOk;
        private System.Windows.Forms.Label lblPCAPFilename;
        private System.Windows.Forms.Label lblFetchStatus;
        internal System.Windows.Forms.ProgressBar pbWiresharkDL;
    }
}