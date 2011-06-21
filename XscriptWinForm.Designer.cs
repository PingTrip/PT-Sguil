namespace PT_Sguil
{
    partial class XscriptWinForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbXscript = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnFrmXscriptWinAbort = new System.Windows.Forms.Button();
            this.btnFrmXscriptWinClose = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(657, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rtbXscript);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 306);
            this.panel1.TabIndex = 1;
            // 
            // rtbXscript
            // 
            this.rtbXscript.BackColor = System.Drawing.SystemColors.Window;
            this.rtbXscript.DetectUrls = false;
            this.rtbXscript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbXscript.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbXscript.Location = new System.Drawing.Point(0, 0);
            this.rtbXscript.Name = "rtbXscript";
            this.rtbXscript.ReadOnly = true;
            this.rtbXscript.Size = new System.Drawing.Size(633, 306);
            this.rtbXscript.TabIndex = 0;
            this.rtbXscript.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSearch,
            this.btnSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 454);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(657, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbSearch
            // 
            this.tbSearch.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(150, 25);
            this.tbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem});
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 22);
            this.btnSearch.Text = "Search";
            this.btnSearch.ButtonClick += new System.EventHandler(this.btnSearch_ButtonClick);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.searchToolStripMenuItem.Text = "Search (No Case)";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(12, 384);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(633, 67);
            this.panel2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(633, 67);
            this.textBox1.TabIndex = 0;
            // 
            // btnFrmXscriptWinAbort
            // 
            this.btnFrmXscriptWinAbort.Location = new System.Drawing.Point(172, 349);
            this.btnFrmXscriptWinAbort.Name = "btnFrmXscriptWinAbort";
            this.btnFrmXscriptWinAbort.Size = new System.Drawing.Size(75, 23);
            this.btnFrmXscriptWinAbort.TabIndex = 4;
            this.btnFrmXscriptWinAbort.Text = "Abort";
            this.btnFrmXscriptWinAbort.UseVisualStyleBackColor = true;
            this.btnFrmXscriptWinAbort.Click += new System.EventHandler(this.btnFrmXscriptWinAbort_Click);
            // 
            // btnFrmXscriptWinClose
            // 
            this.btnFrmXscriptWinClose.Location = new System.Drawing.Point(374, 349);
            this.btnFrmXscriptWinClose.Name = "btnFrmXscriptWinClose";
            this.btnFrmXscriptWinClose.Size = new System.Drawing.Size(75, 23);
            this.btnFrmXscriptWinClose.TabIndex = 5;
            this.btnFrmXscriptWinClose.Text = "Close";
            this.btnFrmXscriptWinClose.UseVisualStyleBackColor = true;
            this.btnFrmXscriptWinClose.Click += new System.EventHandler(this.btnFrmXscriptWinClose_Click);
            // 
            // frmXscriptWin
            // 
            this.ClientSize = new System.Drawing.Size(657, 479);
            this.Controls.Add(this.btnFrmXscriptWinClose);
            this.Controls.Add(this.btnFrmXscriptWinAbort);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmXscriptWin";
            this.Text = "frmXscriptWin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmXscriptWin_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFrmXscriptWinAbort;
        private System.Windows.Forms.Button btnFrmXscriptWinClose;
        private System.Windows.Forms.ToolStripSplitButton btnSearch;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbXscript;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox tbSearch;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}
