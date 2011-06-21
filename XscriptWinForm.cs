using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PT_Sguil
{
    partial class XscriptWinForm : Form
    {
        private string originalRtf = string.Empty;
        private string sessionState = string.Empty;
        private bool wasAborted = false;

        public XscriptWinForm()
        {
            InitializeComponent();
        }

        private void abortXscript()
        {
            this.wasAborted = true;
            SguildConnection.SendToSguild("AbortXscript " + this.Text);
            this.btnFrmXscriptWinAbort.Enabled = false;
        }

        private void btnFrmXscriptWinAbort_Click(object sender, EventArgs e)
        {
            this.abortXscript();
        }

        private void btnFrmXscriptWinClose_Click(object sender, EventArgs e)
        {
            this.closeXscript();
        }

        private void btnSearch_ButtonClick(object sender, EventArgs e)
        {
            this.SearchXscript(true);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.closeXscript();
        }

        private void closeXscript()
        {
            if (this.btnFrmXscriptWinAbort.Enabled)
            {
                this.abortXscript();
            }
            base.Close();
        }

        private void frmXscriptWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.xscriptWindows.Remove(this.Text);
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SearchXscript(false);
        }

        private void SearchXscript(bool caseSensitve)
        {
            string text = this.tbSearch.Text;
            int num = 0;
            if (caseSensitve)
            {
                num = 4;
            }
            if (this.originalRtf == string.Empty)
            {
                this.originalRtf = this.rtbXscript.Rtf;
            }

            MessageBox.Show(this.originalRtf);
            
            if (text.Length > 0)
            {
                int start = -1;
                int num3 = 0;
                this.rtbXscript.Rtf = this.originalRtf;
                while ((start = this.rtbXscript.Find(text, num3, (RichTextBoxFinds) num)) > -1)
                {
                    this.rtbXscript.Select(start, text.Length);
                    this.rtbXscript.SelectionBackColor = Color.Yellow;
                    num3 = start + text.Length;
                }
                this.rtbXscript.Select(0, 0);
            }
            else
            {
                this.rtbXscript.Rtf = this.originalRtf;
            }
        }

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            this.SearchXscript(true);
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                this.SearchXscript(true);
            }
        }

        private delegate void XscriptDebugMsgDelegate(string s);
        public void XscriptDebugMsg(string s)
        {
            if (this.textBox1.InvokeRequired)
            {
                this.textBox1.Invoke(new XscriptDebugMsgDelegate(this.XscriptDebugMsg), new object[] { s });
            }
            else
            {
                s = s.Trim(new char[] { '{', '}' });
                this.textBox1.AppendText(s + Environment.NewLine);
            }
        }

        private delegate void XscriptMainMsgDelegate(string s);
        public void XscriptMainMsg(string s)
        {
            if (this.rtbXscript.InvokeRequired)
            {
                this.rtbXscript.Invoke(new XscriptMainMsgDelegate(this.XscriptMainMsg), new object[] { s });
                return;
            }
            if (this.wasAborted && (s != "DONE"))
            {
                return;
            }
            switch (s)
            {
                case "HDR":
                case "SRC":
                case "DST":
                case "DEBUG":
                case "ERROR":
                    this.sessionState = s;
                    goto Label_02BF;

                case "DONE":
                    this.sessionState = string.Empty;
                    goto Label_02BF;

                default:
                    s = s.Trim(new char[] { '{', '}' });
                    s = s.PadRight(this.rtbXscript.Width - 4, ' ');
                    s = s + "\n";
                    this.rtbXscript.Select(this.rtbXscript.TextLength, 0);
                    switch (this.sessionState)
                    {
                        case "HDR":
                            this.rtbXscript.SelectionBackColor = ColorTranslator.FromHtml("#00FFFF");
                            this.rtbXscript.SelectionColor = Color.Black;
                            break;

                        case "SRC":
                            this.rtbXscript.SelectionBackColor = Color.White;
                            this.rtbXscript.SelectionColor = Color.Blue;
                            s = "SRC: " + s;
                            break;

                        case "DST":
                            this.rtbXscript.SelectionBackColor = Color.White;
                            this.rtbXscript.SelectionColor = Color.Red;
                            s = "DST: " + s;
                            break;

                        case "DONE":
                            this.btnFrmXscriptWinAbort.Enabled = false;
                            break;
                    }
                    break;
            }
            this.rtbXscript.AppendText(s);
        Label_02BF:
            if (!this.wasAborted && (this.rtbXscript.TextLength >= ConfigurationSupport.XscriptAutoAbortByteCount))
            {
                int xscriptAutoAbortByteCount = ConfigurationSupport.XscriptAutoAbortByteCount;
                if (xscriptAutoAbortByteCount > 0x3e8)
                {
                    this.XscriptDebugMsg(string.Format("Auto-Abort called based on config setting of {0}K bytes.", xscriptAutoAbortByteCount / 0x3e8));
                }
                else
                {
                    this.XscriptDebugMsg(string.Format("Auto-Abort called based on config setting of {0} bytes.", xscriptAutoAbortByteCount));
                }
                this.abortXscript();
            }
        }
    }
}

