using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace PT_Sguil
{
    partial class MainForm : Form
    {
        internal static Dictionary<string, XscriptWinForm> xscriptWindows = new Dictionary<string, XscriptWinForm>();
        internal static SyncList<SensorStatus> SensorStatList;
        internal static SyncList<SnortStatus> SnortStatList;
        internal static List<SyncList<SguilEvent>> RTEventLists;
        internal static SyncList<SguilEvent> EventListES;
        internal static List<DataGridView> dgvList;
        private static DataGridView cntxObj = null;
        internal static AutoResetEvent reportSignal = new AutoResetEvent(false);
        internal static Queue reportQueue = new Queue(32);

        public MainForm()
        {
            InitializeComponent();
            
            ConfigurationSupport.LoadConfig();

            if (!ConfigurationSupport.showGMTClock)
            {
                this.timerGMTClock.Enabled = false;
                this.toolStripClock.Visible = false;
            }

            SnortStatList = new SyncList<SnortStatus>(this);
            EventListES = new SyncList<SguilEvent>(this);
            SensorStatList = new SyncList<SensorStatus>(this);
            RTEventLists = new List<SyncList<SguilEvent>>();
            dgvList = new List<DataGridView>();

            dgvList.Add(this.RealTimeEvents0DataGridView);
            dgvList.Add(this.RealTimeEvents1DataGridView);
            dgvList.Add(this.RealTimeEvents2DataGridView);

            for (int i = 0; i < 2; i++)
            {
                RTEventLists.Add(new SyncList<SguilEvent>(this));
                dgvList[i].AutoGenerateColumns = false;
                dgvList[i].DataSource = RTEventLists[i];
            }

            this.dgvESEvents.AutoGenerateColumns = false;
            this.dgvESEvents.DataSource = EventListES;
            this.SnortStatisticsDataGridView.AutoGenerateColumns = false;
            this.SnortStatisticsDataGridView.DataSource = SnortStatList;
            this.SensorStatsDataGridView.AutoGenerateColumns = false;
            this.SensorStatsDataGridView.Columns["agntLast"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.SensorStatsDataGridView.Columns["agntStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.SensorStatsDataGridView.DataSource = SensorStatList;
            this.tlPacket.Controls.Add(this.tlHdrUDP, 1, 3);
            this.tlPacket.Controls.Add(this.tlHdrICMP, 1, 3);
            this.tlHdrUDP.Hide();
            this.tlHdrICMP.Hide();
            this.packetDataHexRichTextBoxSyncronized.BindScroll(this.packetDataAsciiRichTextBoxSyncronized);
        }

        private void btnSensorUpdateInt_Click(object sender, EventArgs e)
        {
            SguildConnection.SendToSguild("SendClientSensorStatusInfo");
        }

        private void bwIPResolution_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = ExternalData.GetReverseDNS(this.tbDNSSrcIP.Text, this.tbDNSDstIP.Text);
        }

        private void bwIPResolution_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<string, string> result = e.Result as Dictionary<string, string>;
            this.tbDNSSrcName.Text = result["srcName"];
            this.tbDNSDstName.Text = result["dstName"];
        }

        private void cbSensorUpdateInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            char keyChar = e.KeyChar;
            if ((!char.IsDigit(keyChar) && (keyChar != '\b')) && (keyChar != '\r'))
            {
                e.Handled = true;
            }
            if (keyChar == '\r')
            {
                if (!this.cbSensorUpdateInt.Items.Contains(box.Text))
                {
                    this.cbSensorUpdateInt.Items.Add(box.Text.ToString());
                }
                this.cbSensorUpdateInt.SelectedItem = box.Text;
            }
        }

        private void cbSensorUpdateInt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            string selectedItem = (string) box.SelectedItem;
            int num = 30;
            if (selectedItem != string.Empty)
            {
                num = int.Parse(selectedItem);
            }
            this.timerSensorStatusUpdate.Interval = num * 0x3e8;
        }

        private void chkbEnableDNS_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkbEnableDNS.Checked)
            {
                this.timerRTEDelayedSelect.Start();
            }
            else
            {
                this.tbDNSSrcIP.Clear();
                this.tbDNSDstIP.Clear();
                this.tbDNSSrcName.Clear();
                this.tbDNSDstName.Clear();
            }
        }

        private void chkbShowPacket_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkbShowPacket.Checked)
            {
                this.timerRTEDelayedSelect.Start();
            }
            else
            {
                this.ipHeaderSourceIPTextBox.Text = string.Empty;
                this.ipHeaderDestinationIPTextBox.Text = string.Empty;
                this.ipHeaderVersionTextBox.Text = string.Empty;
                this.tbHdrIPhl.Text = string.Empty;
                this.ipHeaderTypeOfServiceTextBox.Text = string.Empty;
                this.ipHeaderLengthTextBox.Text = string.Empty;
                this.tbHdrIPid.Text = string.Empty;
                this.ipHeaderFlagsTextBox.Text = string.Empty;
                this.ipHeaderOffsetTextBox.Text = string.Empty;
                this.ipHeaderTimeToLiveTextBox.Text = string.Empty;
                this.tbHdrIPchksum.Text = string.Empty;
                this.tbHdrICMPType.Text = string.Empty;
                this.tbHdrICMPCode.Text = string.Empty;
                this.tbHdrICMPChkSum.Text = string.Empty;
                this.tbHdrICMPID.Text = string.Empty;
                this.tbHdrICMPSeqNum.Text = string.Empty;
                this.tbHdrUDPChkSum.Text = string.Empty;
                this.tbHdrUDPDestPort.Text = string.Empty;
                this.tbHdrUDPSrcPort.Text = string.Empty;
                this.tbHdrUDPLength.Text = string.Empty;
                this.tcpHeaderSourcePortTextBox.Text = string.Empty;
                this.tcpHeaderDestinationPortTextBox.Text = string.Empty;
                this.tcpHeaderSequenceNumberTextBox.Text = string.Empty;
                this.tcpHeaderAcknowledgementNumberTextBox.Text = string.Empty;
                this.tcpHeaderOffsetTextBox.Text = string.Empty;
                this.tcpHeaderReservedTextBox.Text = string.Empty;
                this.tcpHeaderR1TextBox.Text = string.Empty;
                this.tcpHeaderR0TextBox.Text = string.Empty;
                this.tcpHeaderUrgentTextBox.Text = string.Empty;
                this.tcpHeaderAcknowledgmentTextBox.Text = string.Empty;
                this.tcpHeaderPushTextBox.Text = string.Empty;
                this.tcpHeaderResetTextBox.Text = string.Empty;
                this.tcpHeaderSynchronizeTextBox.Text = string.Empty;
                this.tcpHeaderFinishedTextBox.Text = string.Empty;
                this.tcpHeaderWindowTextBox.Text = string.Empty;
                this.tbHdrTCPurp.Text = string.Empty;
                this.tbHdrTCPchksum.Text = string.Empty;
                this.packetDataAsciiRichTextBoxSyncronized.Text = string.Empty;
                this.packetDataHexRichTextBoxSyncronized.Text = string.Empty;
                this.searchPacketDataButton.Enabled = false;
                ConfigurationSupport.packetAsciiDataFormatted = string.Empty;
            }
        }

        private void chkbShowRule_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkbShowRule.Checked)
            {
                this.timerRTEDelayedSelect.Start();
            }
            else
            {
                this.IDSRuleRichTextBox.Clear();
            }
        }

        internal void ChooseSensorToMonitor(Dictionary<string, List<string>> sensorList)
        {
            SensorSelectForm select = new SensorSelectForm(sensorList);
            select.ShowDialog();

            if (select.DialogResult.Equals(DialogResult.OK))
            {
                string str = string.Join(" ", ConfigurationSupport.monitoredSensors.ToArray());
                if (ConfigurationSupport.monitoredSensors.Count > 1)
                {
                    str = "{" + str + "}";
                }
                SguildConnection.SendToSguild("MonitorSensors " + str);
            }
            else
            {
                this.DisconnectFromSguild();
            }
        }

        private void clearPCAPFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Delete all PCAP files from\n{0}", ConfigurationSupport.wiresharkStorageDir), "Delete PCAP Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("All PCAP files successfully deleted.", "Clear PCAP Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void cntxStatus_Opening(object sender, CancelEventArgs e)
        {
            if ((sender as ContextMenuStrip).SourceControl != null)
            {
                cntxObj = (DataGridView) (sender as ContextMenuStrip).SourceControl;
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.connectToolStripMenuItem.Text == "Connect")
            {
                this.ConnectToSguild();
            }
            else
            {
                this.DisconnectFromSguild();
            }
        }

        private void ConnectToSguild()
        {
            ConfigurationSupport.isInitialLoad = true;
            AuthenticationPromptForm prompt = new AuthenticationPromptForm();

            while (!SguildConnection.isConnected && !prompt.DialogResult.Equals(DialogResult.Cancel))
            {
                string text = string.Empty;
                
                prompt.ShowDialog();

                if (prompt.DialogResult.Equals(DialogResult.OK))
                {
                    text = SguildConnection.OpenConnection();
                }
                
                if (text != string.Empty)
                {
                    MessageBox.Show(text, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            if (SguildConnection.isConnected)
            {
                this.connectToolStripMenuItem.Text = "Disconnect";
                this.tsStatusServer.Text = ConfigurationSupport.currentHost;
                this.tsStatusUserName.Text = ConfigurationSupport.currentUsername;
                this.tsStatusUserID.Text = ConfigurationSupport.userID;

                foreach (SyncList<SguilEvent> list in RTEventLists)
                {
                    list.Clear();
                }

                EventListES.Clear();
                
                SguildConnection.SendToSguild("SendDBInfo");
                SguildConnection.SendToSguild("SendSensorList");

                while (SguildConnection.isConnected && (ConfigurationSupport.monitoredSensors.Count == 0))
                {
                    Application.DoEvents();
                }

                if (SguildConnection.isConnected)
                {
                    SguildConnection.SendToSguild("SendEscalatedEvents");
                    SguildConnection.SendToSguild("SendClientSensorStatusInfo");
                    this.timerSensorStatusUpdate.Start();
                }
            }
        }

        private void dgvEvents_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView view = (DataGridView) sender;
            if ((view.Columns[e.ColumnIndex].DataPropertyName == "Status") && (e.Value != null))
            {
                Color evntColor = ConfigurationSupport.eventConfigsByPriorty[(int) view.Rows[e.RowIndex].Cells[11].Value].EvntColor;
                if (!evntColor.IsEmpty)
                {
                    e.CellStyle.BackColor = evntColor;
                    e.CellStyle.SelectionBackColor = evntColor;
                }
            }
        }

        private void dgvEvents_Enter(object sender, EventArgs e)
        {
            for (int i = 0; i < ConfigurationSupport.numberOfRTPanes; i++)
            {
                if (!dgvList[i].Focused)
                {
                    dgvList[i].ClearSelection();
                }
            }
            if (!Program.MainForm.dgvESEvents.Focused)
            {
                Program.MainForm.dgvESEvents.ClearSelection();
            }
        }

        private void dgvEvents_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView selectedDGV = (DataGridView) sender;
            int status = 0;
            bool withComment = false;
            bool isValidKey = false;

            switch (e.KeyCode)
            {
                case Keys.F1:
                    status = 11;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F2:
                    status = 12;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F3:
                    status = 13;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F4:
                    status = 14;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F5:
                    status = 15;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F6:
                    status = 16;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F7:
                    status = 17;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F8:
                    status = 1;
                    if (e.Modifiers == Keys.Shift)
                    {
                        withComment = true;
                    }
                    isValidKey = true;
                    break;

                case Keys.F9:
                    status = 2;
                    withComment = true;
                    isValidKey = true;
                    break;
            }

            if (isValidKey)
            {
                List<string> list = new List<string>();

                foreach (DataGridViewRow row in selectedDGV.SelectedRows)
                {
                    SguilEvent dataBoundItem = (SguilEvent) row.DataBoundItem;
                    list.Add(dataBoundItem.AlertID);
                }

                string eventIDS = string.Join(" ", list.ToArray());
                
                if (list.Count > 1)
                {
                    eventIDS = "{" + eventIDS + "}";
                }

                SguildCommands.ValidateEvent(eventIDS, status, withComment);
            }
        }

        private void dgvEvents_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView view = (DataGridView) sender;
            if (view != null)
            {
                view.Focus();
                if (e.Button == MouseButtons.Right)
                {
                    DataGridView.HitTestInfo info = view.HitTest(e.X, e.Y);
                    view.ClearSelection();
                    view.Rows[info.RowIndex].Selected = true;
                }
            }
        }

        private void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView) sender;
            if (view.Focused)
            {
                if ((this.chkbShowPacket.Checked || this.chkbShowRule.Checked) || this.chkbEnableDNS.Checked)
                {
                    this.timerRTEDelayedSelect.Stop();
                    this.timerRTEDelayedSelect.Start();
                }
            }
            else
            {
                view.ClearSelection();
            }
        }

        private delegate void dgvRefreshDelegate(int rtPane);
        internal void dgvRefresh(int rtPane)
        {
            if (dgvList[rtPane].InvokeRequired)
            {
                dgvList[rtPane].Invoke(new dgvRefreshDelegate(this.dgvRefresh), new object[] { rtPane });
            }
            else
            {
                dgvList[rtPane].Refresh();
            }
        }

        private void dgvSensorStats_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.SensorStatsDataGridView.Columns[e.ColumnIndex].Name == "agntStatus") && (e.Value != null))
            {
                //DataRowView dataBoundItem = this.dgvSensorStats.Rows[e.RowIndex].DataBoundItem as DataRowView;
                switch (this.SensorStatsDataGridView.Rows[e.RowIndex].Cells["agntStatus"].Value.ToString())
                {
                    case "DOWN":
                        e.CellStyle.BackColor = Color.Red;
                        break;

                    case "UP":
                        e.CellStyle.BackColor = Color.Green;
                        break;
                }
            }
        }

        private void dgvSensorStats_SelectionChanged(object sender, EventArgs e)
        {
            if (this.SensorStatsDataGridView.SelectedCells.Count > 0)
            {
                this.SensorStatsDataGridView.SelectedCells[0].Selected = false;
            }
        }

        private void dgvSnortStats_SelectionChanged(object sender, EventArgs e)
        {
            if (this.SnortStatisticsDataGridView.SelectedCells.Count > 0)
            {
                this.SnortStatisticsDataGridView.SelectedCells[0].Selected = false;
            }
        }

        private delegate void DisconnectFromSguildDelegate();
        private void DisconnectFromSguild()
        {
            if (this.statusStrip1.InvokeRequired)
            {
                this.statusStrip1.Invoke(new DisconnectFromSguildDelegate(this.DisconnectFromSguild));
            }
            else
            {
                SguildConnection.CloseConnection();
                this.connectToolStripMenuItem.Text = "Connect";
                ConfigurationSupport.monitoredSensors.Clear();
                this.tsStatusServer.Text = "------";
            }
        }

        private void eventhistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new XscriptWinForm().Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (MessageBox.Show("Are you sure you want to exit?", "Exit PT-Sguil", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    SguildConnection.CloseConnection();
                    this.notifyIcon.Visible = false;
                    e.Cancel = false;
                    break;

                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    if (e.Modifiers == Keys.Control)
                    {
                        MessageBox.Show("Query Mode");
                    }
                    break;

                case Keys.S: // Put client in "sleep" mode
                    if (e.Modifiers == Keys.Control)
                    {
                        ConfigurationSupport.isSleeping = true;
                        base.WindowState = FormWindowState.Minimized;
                        base.Hide();
                        this.notifyIcon.Visible = true;
                    }
                    break;

                case Keys.F:
                    if (e.Modifiers == Keys.Control)
                    {
                        if (this.tlPacket.RowStyles[5].Height > 0f)
                        {
                            this.tlPacket.RowStyles[5] = new RowStyle(SizeType.Percent, 0f);
                        }
                        else
                        {
                            this.tlPacket.RowStyles[5] = new RowStyle(SizeType.Percent, 24f);
                        }
                    }
                    break;
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (ConfigurationSupport.isConfigured)
            {
                if (ConfigurationSupport.numberOfRTPanes == 1)
                {
                    this.scRTPanesA.Panel2Collapsed = true;
                    this.scRTPanesB.Panel1Collapsed = true;
                    this.scRTPanesB.Panel2Collapsed = true;
                }
                else if (ConfigurationSupport.numberOfRTPanes == 2)
                {
                    this.scRTPanesA.Panel2Collapsed = false;
                    this.scRTPanesB.Panel2Collapsed = true;
                }
                else if (ConfigurationSupport.numberOfRTPanes == 3)
                {
                    this.scRTPanesA.Panel2Collapsed = false;
                    this.scRTPanesB.Panel2Collapsed = false;
                }

                if (!ConfigurationSupport.selectedRowBackgroundColor.IsEmpty)
                {
                    foreach (DataGridView view in dgvList)
                    {
                        view.DefaultCellStyle.SelectionBackColor = ConfigurationSupport.selectedRowBackgroundColor;
                    }
                    this.dgvESEvents.DefaultCellStyle.SelectionBackColor = ConfigurationSupport.selectedRowBackgroundColor;
                }
                
                if (!ConfigurationSupport.selectedRowForegroundColor.IsEmpty)
                {
                    foreach (DataGridView view in dgvList)
                    {
                        view.DefaultCellStyle.SelectionForeColor = ConfigurationSupport.selectedRowForegroundColor;
                    }
                    this.dgvESEvents.DefaultCellStyle.SelectionForeColor = ConfigurationSupport.selectedRowForegroundColor;
                }
                
                this.timerBalloonClear.Interval = ConfigurationSupport.sleepBalloonTimeout;
                
                // bring the app window to the foreground
                this.Activate();

                this.ConnectToSguild();
            }
        }

        internal void IncomingPcapFile(string strData)
        {
            string[] strArray = strData.Split(new char[] { ' ' }, 2);
            new FetchWireSharkPCAPForm(strData).ShowDialog();
            Process.Start(ConfigurationSupport.wiresharkPath, string.Format("{0}/{1}", ConfigurationSupport.wiresharkStorageDir, strArray[0]));
        }

        private delegate void InsertICMPHdrDelegate(List<string> a);
        internal void InsertICMPHdr(List<string> a)
        {
            if (this.tlHdrICMP.InvokeRequired)
            {
                this.tlHdrICMP.Invoke(new InsertICMPHdrDelegate(this.InsertICMPHdr), new object[] { a });
            }
            else
            {
                this.tbHdrICMPType.Text = a[0];
                this.tbHdrICMPCode.Text = a[1];
                this.tbHdrICMPChkSum.Text = a[2];
                this.tbHdrICMPID.Text = a[3];
                this.tbHdrICMPSeqNum.Text = a[4];
            }
        }

        private delegate void InsertIPHdrDelegate(List<string> a);
        internal void InsertIPHdr(List<string> a)
        {
            if (this.tlHdrIP.InvokeRequired)
            {
                this.tlHdrIP.Invoke(new InsertIPHdrDelegate(this.InsertIPHdr), new object[] { a });
            }
            else
            {
                this.ipHeaderSourceIPTextBox.Text = a[0];
                this.ipHeaderDestinationIPTextBox.Text = a[1];
                this.ipHeaderVersionTextBox.Text = a[2];
                this.tbHdrIPhl.Text = a[3];
                this.ipHeaderTypeOfServiceTextBox.Text = a[4];
                this.ipHeaderLengthTextBox.Text = a[5];
                this.tbHdrIPid.Text = a[6];
                this.ipHeaderFlagsTextBox.Text = a[7];
                this.ipHeaderOffsetTextBox.Text = a[8];
                this.ipHeaderTimeToLiveTextBox.Text = a[9];
                this.tbHdrIPchksum.Text = a[10];
            }
        }

        private delegate void InsertPayloadDataDelegate(string payload);
        internal void InsertPayloadData(string payload)
        {
            if (this.packetDataAsciiRichTextBoxSyncronized.InvokeRequired)
            {
                this.packetDataAsciiRichTextBoxSyncronized.Invoke(new InsertPayloadDataDelegate(this.InsertPayloadData), new object[] { payload });
            }
            else
            {
                this.packetDataAsciiRichTextBoxSyncronized.Clear();
                this.packetDataHexRichTextBoxSyncronized.Clear();
                bool isPortScan = false;

                if (payload.Length >= 15 && payload.Substring(0, 15) == "5072696F72697479")
                {
                    isPortScan = true;
                }

                if (payload == string.Empty)
                {
                    this.packetDataAsciiRichTextBoxSyncronized.Text = "None.";
                    this.packetDataHexRichTextBoxSyncronized.Text = "None.";
                }
                else if (!isPortScan)
                {
                    StringBuilder asciiData = new StringBuilder();
                    StringBuilder hexData = new StringBuilder();
                    StringBuilder asciiDataFormatted = new StringBuilder();

                    string s = string.Empty;
                    int byteCount = 2;
                
                    for (int i = 0; i <= (payload.Length - 2); i += 2)
                    {
                        s = payload.Substring(i, 2);
                        hexData.Append(s + " ");
                        
                        char ch = Convert.ToChar(int.Parse(s, NumberStyles.HexNumber));

                        if (s == "0A")
                        {
                            asciiDataFormatted.Append(Environment.NewLine);
                        }
                        else if ((ch < ' ') || (ch > '~'))
                        {
                            asciiData.Append(".");

                            if (s != "0D")
                                asciiDataFormatted.Append(".");
                        }
                        else
                        {
                            asciiData.Append(ch.ToString());
                            asciiDataFormatted.Append(ch.ToString());
                        }

                        if (byteCount == 32)
                        {
                            asciiData.Append("\n");
                            hexData.Append("\n");
                            byteCount = 2;
                        }
                        else
                        {
                            byteCount += 2;
                        }
                    }
                    this.packetDataAsciiRichTextBoxSyncronized.AppendText(asciiData.ToString());
                    this.packetDataHexRichTextBoxSyncronized.AppendText(hexData.ToString());
                    ConfigurationSupport.packetAsciiDataFormatted = asciiDataFormatted.ToString();

                    asciiData.Length = 0;
                    hexData.Length = 0;
                    asciiDataFormatted.Length = 0;

                    this.searchPacketDataButton.Enabled = true;
                }
            }
        }

        private delegate void InsertRuleDataDelegate(string s);
        internal void InsertRuleData(string s)
        {
            if (this.IDSRuleRichTextBox.InvokeRequired)
            {
                this.IDSRuleRichTextBox.Invoke(new InsertRuleDataDelegate(this.InsertRuleData), new object[] { s });
            }
            else
            {
                string[] strArray = s.Split(new char[] { ';' });
                StringBuilder builder = new StringBuilder("");
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                for (int i = 0; i < strArray.Length; i++)
                {
                    string key = strArray[i].Trim();
                    if (key.StartsWith("sid:"))
                    {
                        dictionary.Add(key, "http://www.google.com");
                    }
                    else if (key.StartsWith("reference:"))
                    {
                        dictionary.Add(key, "http://www.google.com");
                    }
                    else
                    {
                        if (strArray.Length == 1)
                        {
                            builder.AppendLine();
                        }
                        builder.Append(strArray[i]);
                    }
                    if (strArray.Length > 1)
                    {
                        builder.Append(";");
                    }
                }
                this.IDSRuleRichTextBox.AppendText(builder.ToString());
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    this.IDSRuleRichTextBox.AppendText(string.Format(Environment.NewLine + "{0} {1}", pair.Key, pair.Value));
                }
            }
        }

        private delegate void InsertSystemInfoMsgDelegate(string s);
        internal void InsertSystemInfoMsg(string s)
        {
            if (this.SystemMessagesTextBox.InvokeRequired)
            {
                this.SystemMessagesTextBox.Invoke(new InsertSystemInfoMsgDelegate(this.InsertSystemInfoMsg), new object[] { s });
            }
            else
            {
                this.SystemMessagesTextBox.AppendText(s);
            }
        }

        private delegate void InsertTCPHdrDelegate(List<string> a);
        internal void InsertTCPHdr(List<string> a)
        {
            if (this.tlHdrTCP.InvokeRequired)
            {
                this.tlHdrTCP.Invoke(new InsertTCPHdrDelegate(this.InsertTCPHdr), new object[] { a });
            }
            else
            {
                string[] strArray = a[8].ToString().Split(new char[] { ' ' }, 2);
                int result = 0;
                this.tcpHeaderSourcePortTextBox.Text = strArray[0];
                this.tcpHeaderDestinationPortTextBox.Text = strArray[1];

                this.tcpHeaderSequenceNumberTextBox.Text = a[0];
                this.tcpHeaderAcknowledgementNumberTextBox.Text = a[1];
                this.tcpHeaderOffsetTextBox.Text = a[2];
                this.tcpHeaderReservedTextBox.Text = a[3];
                this.tcpHeaderR1TextBox.Text = "";
                this.tcpHeaderR0TextBox.Text = "";
                this.tcpHeaderUrgentTextBox.Text = "";
                this.tcpHeaderAcknowledgmentTextBox.Text = "";
                this.tcpHeaderPushTextBox.Text = "";
                this.tcpHeaderResetTextBox.Text = "";
                this.tcpHeaderSynchronizeTextBox.Text = "";
                this.tcpHeaderFinishedTextBox.Text = "";

                if (int.TryParse(a[4], out result))
                {
                    if ((result & (int)TCPFlagsEnum.R1) == (int)TCPFlagsEnum.R1)
                    {
                        this.tcpHeaderR1TextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.R0) == (int)TCPFlagsEnum.R0)
                    {
                        this.tcpHeaderR0TextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.Urg) == (int)TCPFlagsEnum.Urg)
                    {
                        this.tcpHeaderUrgentTextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.Ack) == (int)TCPFlagsEnum.Ack)
                    {
                        this.tcpHeaderAcknowledgmentTextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.Psh) == (int)TCPFlagsEnum.Psh)
                    {
                        this.tcpHeaderPushTextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.Rst) == (int)TCPFlagsEnum.Rst)
                    {
                        this.tcpHeaderResetTextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.Syn) == (int)TCPFlagsEnum.Syn)
                    {
                        this.tcpHeaderSynchronizeTextBox.Text = "X";
                    }
                    if ((result & (int)TCPFlagsEnum.Fin) == (int)TCPFlagsEnum.Fin)
                    {
                        this.tcpHeaderFinishedTextBox.Text = "X";
                    }
                }
                this.tcpHeaderWindowTextBox.Text = a[5];
                this.tbHdrTCPurp.Text = a[6];
                this.tbHdrTCPchksum.Text = a[7];
            }
        }

        private delegate void InsertUDPHdrDelegate(string s);
        internal void InsertUDPHdr(string s)
        {
            if (this.tlHdrUDP.InvokeRequired)
            {
                this.tlHdrUDP.Invoke(new InsertUDPHdrDelegate(this.InsertUDPHdr), new object[] { s });
            }
            else
            {
                s = s.Replace("{", string.Empty).Replace("}", string.Empty);
                string[] strArray = s.Split(new char[] { ' ' });
                this.tbHdrUDPLength.Text = strArray[0];
                this.tbHdrUDPChkSum.Text = strArray[1];
                this.tbHdrUDPSrcPort.Text = strArray[2];
                this.tbHdrUDPDestPort.Text = strArray[3];
            }
        }

        private delegate void notifyIcon_BalloonTextDelegate(string title, string alert);
        internal void notifyIcon_BalloonText(string title, string alert)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new notifyIcon_BalloonTextDelegate(this.notifyIcon_BalloonText), new object[] { title, alert });
            }
            else
            {
                this.notifyIcon.ShowBalloonTip(0x2710, title, alert, ToolTipIcon.Warning);
                this.timerBalloonClear.Start();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ConfigurationSupport.isSleeping = false;
            base.Show();
            base.StartPosition = FormStartPosition.CenterScreen;
            base.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm config = new ConfigForm();
            config.ShowDialog();
        }

        private void SOMe_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string str = string.Empty;
            string[] strArray = e.LinkText.Split(new char[] { ',' }, 2);

            switch (strArray[0])
            {
                case "url":
                    str = strArray[1];
                    break;

                case "bugtraq":
                    str = "www.securityfocus.com/bid/" + strArray[1];
                    break;

                case "cve":
                    str = "nvd.nist.gov/nvd.cfm?cvename=CAN-" + strArray[1];
                    break;

                case "nessus":
                    str = "cgi.nessus.org/plugins/dump.php3?id=" + strArray[1];
                    break;

                case "mcafee":
                    str = "vil.nai.com/vil/content/v_" + strArray[1];
                    break;

                case "arachnids":
                    MessageBox.Show("ArachNIDS references are no long supported.", "Rule Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;

                default:
                    if (Regex.IsMatch(strArray[0], @"^\d+$"))
                    {
                        int result = 0;
                        if (int.TryParse(strArray[0], out result))
                        {
                            if (result < 0xf4240)
                            {
                                str = "www.snort.org/pub-bin/sigs.cgi?sid=" + strArray[0];
                            }
                            else if ((result > 0x1e847f) && (result < 0x1c9c380))
                            {
                                str = "doc.emergingthreats.net/" + strArray[0];
                            }
                            else
                            {
                                MessageBox.Show("Sid " + strArray[0] + " is a locally managed signature/rule.", "Rule Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unknown reference in rule: " + e.LinkText, "Rule Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
            }
            if (str != string.Empty)
            {
                Process.Start("http://" + str);
            }
        }

        private void timerBalloonClear_Tick(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            this.timerBalloonClear.Stop();
            this.notifyIcon.Visible = true;
        }

        private void timerGMTClock_Tick(object sender, EventArgs e)
        {
            this.toolStripClock.Text = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd HH':'mm':'ss' GMT'");
        }

        private void timerRTEDelayedSelect_Tick(object sender, EventArgs e)
        {
            DataGridView dgvESEvents = null;
            SguilEvent dataBoundItem;
            this.timerRTEDelayedSelect.Stop();
            int proto = 0;
            string alertid = string.Empty;

            if ((this.chkbShowPacket.Checked || this.chkbShowRule.Checked) || this.chkbEnableDNS.Checked)
            {
                foreach (DataGridView view2 in dgvList)
                {
                    if (view2.SelectedRows.Count > 0)
                    {
                        dgvESEvents = view2;
                    }
                }
                if ((dgvESEvents == null) && (this.dgvESEvents.SelectedRows.Count > 0))
                {
                    dgvESEvents = this.dgvESEvents;
                }
            }

            if ((this.chkbShowPacket.Checked && (dgvESEvents != null)) && (dgvESEvents.SelectedRows.Count > 0))
            {
                proto = int.Parse(dgvESEvents.SelectedRows[0].Cells[9].Value.ToString());
                alertid = dgvESEvents.SelectedRows[0].Cells[3].Value.ToString();

                if (proto != 0)
                {
                    if (!(proto == (int)ProtocolFlagsEnums.ICMP))
                    {
                        if (proto == (int)ProtocolFlagsEnums.TCP)
                        {
                            this.tlHdrUDP.Hide();
                            this.tlHdrICMP.Hide();
                            this.tlHdrTCP.Show();
                            this.lblHdrRow.Text = "TCP";
                            SguildCommands.RequestPacketData(alertid, proto);
                        }
                        else if (proto == (int)ProtocolFlagsEnums.UDP)
                        {
                            this.tlHdrICMP.Hide();
                            this.tlHdrTCP.Hide();
                            this.tlHdrUDP.Show();
                            this.lblHdrRow.Text = "UDP";
                            SguildCommands.RequestPacketData(alertid, proto);
                        }
                    }
                    else
                    {
                        this.tlHdrUDP.Hide();
                        this.tlHdrTCP.Hide();
                        this.tlHdrICMP.Show();
                        this.lblHdrRow.Text = "ICMP";
                        SguildCommands.RequestPacketData(alertid, proto);
                    }
                }
            }

            if ((this.chkbShowRule.Checked && (dgvESEvents != null)) && (dgvESEvents.SelectedRows.Count > 0))
            {
                dataBoundItem = (SguilEvent) dgvESEvents.SelectedRows[0].DataBoundItem;
                if (dataBoundItem.GenID != "1")
                {
                    this.InsertRuleData("Rules and signatures are not available for the generator ID " + dataBoundItem.GenID);
                }
                else
                {
                    this.IDSRuleRichTextBox.Clear();
                    SguildConnection.SendToSguild(string.Format("RuleRequest {0} {1} {2} {3} {4}", new object[] { dataBoundItem.AlertID, dataBoundItem.Sensor, dataBoundItem.GenID, dataBoundItem.SigID, dataBoundItem.SigRev }));
                }
            }
            if ((this.chkbEnableDNS.Checked && (dgvESEvents != null)) && (dgvESEvents.SelectedRows.Count > 0))
            {
                dataBoundItem = (SguilEvent) dgvESEvents.SelectedRows[0].DataBoundItem;
                this.tbDNSSrcIP.Text = dataBoundItem.SrcIP;
                this.tbDNSDstIP.Text = dataBoundItem.DstIP;
                this.bwIPResolution.RunWorkerAsync();
            }
        }

        private void timerSensorStatusUpdate_Tick(object sender, EventArgs e)
        {
            SguildConnection.SendToSguild("SendClientSensorStatusInfo");
        }

        private void transcriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            ContextMenuStrip owner = (ContextMenuStrip) item.Owner;
            DataGridView sourceControl = (DataGridView) owner.SourceControl;
            SguilEvent dataBoundItem = (SguilEvent) sourceControl.SelectedRows[0].DataBoundItem;
            ExternalData.GetXscript(sender.ToString(), dataBoundItem);
        }

        private void tsStatusSound_DoubleClick(object sender, EventArgs e)
        {
            ConfigurationSupport.isSoundActive = !ConfigurationSupport.isSoundActive;
            if (ConfigurationSupport.isSoundActive)
            {
                this.tsStatusSound.Text = "On";
            }
            else
            {
                this.tsStatusSound.Text = "Off";
            }
        }

        private void ValidateEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num;
            bool withComment = false;
            string input = Regex.Match((sender as ToolStripMenuItem).Text, @"\((.*)\)").Value;

            if (input.Contains("Shft"))
            {
                withComment = true;
            }

            if (int.TryParse(Regex.Match(input, @"(\d+)").Value, out num))
            {
                SguilEvent dataBoundItem = (SguilEvent) cntxObj.SelectedRows[0].DataBoundItem;
                SguildCommands.ValidateEvent(dataBoundItem.AlertID, num, withComment);
            }
        }

        private void wiresharkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((ConfigurationSupport.wiresharkPath != string.Empty) && (ConfigurationSupport.wiresharkStorageDir != string.Empty))
            {
                ToolStripMenuItem item = (ToolStripMenuItem) sender;
                ContextMenuStrip owner = (ContextMenuStrip) item.Owner;
                DataGridView sourceControl = (DataGridView) owner.SourceControl;
                SguilEvent dataBoundItem = (SguilEvent) sourceControl.SelectedRows[0].DataBoundItem;
                ExternalData.GetXscript(sender.ToString(), dataBoundItem);
            }
            else
            {
                MessageBox.Show("Check the Wireshark path setting in your configuration.");
            }
        }

        private void encodeDecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolEncoderForm encoder = new ToolEncoderForm(string.Empty);
            encoder.ShowDialog();
        }

        private void cntxPacketDataItm_CopyData_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ConfigurationSupport.packetAsciiDataFormatted);
        }

        private void cntxPacketData_Opening(object sender, CancelEventArgs e)
        {
            cntxPacketDataItm_DecodeSelection.Enabled = !string.IsNullOrEmpty(packetDataAsciiRichTextBoxSyncronized.SelectedText);
        }

        private void cntxPacketDataItm_DecodeSelection_Click(object sender, EventArgs e)
        {
            ToolEncoderForm encoder = new ToolEncoderForm(packetDataAsciiRichTextBoxSyncronized.SelectedText);
            encoder.ShowDialog();
        }

        private void aboutPTSguilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxForm abtBox = new AboutBoxForm();
            abtBox.Show();
        }

        private void exportEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int dgvPane = -1;

            if (dgvList[0].SelectedRows.Count > 0)
                dgvPane = 0;
            else if (dgvList[1].SelectedRows.Count > 0)
                dgvPane = 1;
            else if (dgvList[2].SelectedRows.Count > 0)
                dgvPane = 2;

            ReportExportForm expRep = new ReportExportForm(dgvPane);
            expRep.Show();
        }

        private void tsStatusAutoScroll_DoubleClick(object sender, EventArgs e)
        {
            ConfigurationSupport.isAutoScrollEnabled = !ConfigurationSupport.isAutoScrollEnabled;

            if (ConfigurationSupport.isAutoScrollEnabled)
            {
                this.tsStatusAutoScroll.Text = "On";
            }
            else
            {
                this.tsStatusAutoScroll.Text = "Off";
            }
        }
    }
}

