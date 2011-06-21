using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace PT_Sguil
{
    public partial class FetchWireSharkPCAPForm : Form
    {
        private string[] _pcapInfo;
        private Thread sguildReaderThread = null;

        public FetchWireSharkPCAPForm(string strData)
        {
            InitializeComponent();
            this._pcapInfo = strData.Split(new char[] { ' ' }, 2);
            this.lblPCAPFilename.Text = this._pcapInfo[0];
            SguildConnection.IncomingPcapFileProgress += new SguildConnection.IncomingPcapFileProgressEventHandler(this.SguildConnection_IncomingPcapFileProgressEvent);
        }

        private void btnWiresharkOk_Click(object sender, EventArgs e)
        {
            this.sguildReaderThread.Join();
            base.Close();
        }

        private void frmFetchWireSharkPCAP_Shown(object sender, EventArgs e)
        {
            this.sguildReaderThread = new Thread(new ParameterizedThreadStart(SguildConnection.ReceivePcapFile));
            this.sguildReaderThread.IsBackground = true;
            this.sguildReaderThread.Start(this._pcapInfo);
        }

        private delegate void SguildConnection_IncomingPcapFileProgressEventDelegate(object sender, EventArgs e, int pcapPercentage);
        private void SguildConnection_IncomingPcapFileProgressEvent(object sender, EventArgs e, int pcapPercentage)
        {
            if (this.pbWiresharkDL.InvokeRequired)
            {
                this.pbWiresharkDL.Invoke(new SguildConnection_IncomingPcapFileProgressEventDelegate(this.SguildConnection_IncomingPcapFileProgressEvent), new object[] { sender, e, pcapPercentage });
            }
            else
            {
                this.pbWiresharkDL.Value = pcapPercentage;
                if (this.pbWiresharkDL.Value == 100)
                {
                    this.lblFetchStatus.Text = "Fetch Complete.";
                    this.btnWiresharkOk.Enabled = true;
                }
            }
        }
    }
}
