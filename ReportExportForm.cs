using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace PT_Sguil
{
    public partial class ReportExportForm : Form
    {
        private static int selectedDGVPane;
        private static List<string> rptList;
        int progress;
        int totalEvents;
        bool isSanitized = false;
        string reportText = string.Empty;

        public ReportExportForm(int param)
        {
            selectedDGVPane = param;
            InitializeComponent();
            cbReportType.SelectedIndex = 0;
            progress = 0;
        }

        private int CalculateProgress()
        {
            int calculatedProgress = 0;
            progress++;

            int txtRpt = 4; //TextReport has steps
            calculatedProgress = (100 / (txtRpt * totalEvents)) * progress;

            return calculatedProgress;
        }

        private string TextReport()
        {
            List<string> list = new List<string>();
            StringBuilder returnString = new StringBuilder();

            totalEvents = MainForm.dgvList[selectedDGVPane].SelectedRows.Count;

            foreach (DataGridViewRow row in MainForm.dgvList[selectedDGVPane].SelectedRows)
            {
                SguilEvent evnt = (SguilEvent)row.DataBoundItem;

                if (evnt.EventMsg != "spp_portscan:")
                {
                    returnString.AppendLine("------------------------------------------------------------------------");
                    returnString.AppendLine(string.Format("Count:{0} Event#{1} {2}", evnt.Count, evnt.AlertID, evnt.Timestamp));
                    returnString.AppendLine(evnt.EventMsg);

                    if (!isSanitized)
                    {
                        returnString.AppendLine(string.Format("{0} -> {1}", evnt.SrcIP, evnt.DstIP));
                    }
                    else
                    {
                        returnString.AppendLine("a.b.c.d -> e.f.g.h");
                    }

                    // Get the IP hdr details
                    SguildConnection.SendToSguild(string.Format("ReportRequest IP {0} {1}", evnt.AlertID.Split(new char[] { '.' }, 2)));

                    // Wait for results
                    MainForm.reportSignal.WaitOne(5000);
                    rptList = (List<string>)MainForm.reportQueue.Dequeue();
                    this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Get IP Header

                    returnString.AppendLine(string.Format("IPVer={0} hlen={1} tos={2} dlen={3} ID={4} flags={5} offset={6} ttl={7} chksum={8}", rptList[2], rptList[3], rptList[4], rptList[5], rptList[6], rptList[7], rptList[8], rptList[9], rptList[10]));
                    returnString.Append(string.Format("Protocol: {0} ", evnt.Proto));


                    // If TCP/UDP add port numbers to report
                    if (evnt.Proto == (int)ProtocolFlagsEnums.TCP || evnt.Proto == (int)ProtocolFlagsEnums.UDP)
                    {
                        returnString.AppendLine(string.Format("sport={0} -> dport={1}", evnt.SrcPort, evnt.DstPort));
                    }

                    returnString.AppendLine("");

                    // If TCP add TCP Header to report
                    if (evnt.Proto == (int)ProtocolFlagsEnums.TCP)
                    {
                        int result;

                        // Get the TCP hdr details
                        SguildConnection.SendToSguild(string.Format("ReportRequest TCP {0} {1}", evnt.AlertID.Split(new char[] { '.' }, 2)));

                        // Wait for results
                        MainForm.reportSignal.WaitOne(5000);
                        rptList = (List<string>)MainForm.reportQueue.Dequeue();
                        this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Get TCP/UDP/ICMP Header

                        returnString.Append(string.Format("Seq={0} Ack={1} Off={2} Res={3}", rptList[0], rptList[1], rptList[2], rptList[3]));

                        returnString.Append("Flags=");

                        if (int.TryParse(rptList[4], out result))
                        {
                            if ((result & (int)TCPFlagsEnum.R1) == (int)TCPFlagsEnum.R1)
                                returnString.Append("1");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.R0) == (int)TCPFlagsEnum.R0)
                                returnString.Append("0");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.Urg) == (int)TCPFlagsEnum.Urg)
                                returnString.Append("U");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.Ack) == (int)TCPFlagsEnum.Ack)
                                returnString.Append("A");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.Psh) == (int)TCPFlagsEnum.Psh)
                                returnString.Append("P");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.Rst) == (int)TCPFlagsEnum.Rst)
                                returnString.Append("R");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.Syn) == (int)TCPFlagsEnum.Syn)
                                returnString.Append("S");
                            else
                                returnString.Append("*");

                            if ((result & (int)TCPFlagsEnum.Fin) == (int)TCPFlagsEnum.Fin)
                                returnString.Append("F");
                            else
                                returnString.Append("*");
                        }

                        returnString.AppendFormat(" Win={0} urp={1} chksum={2}{3}", rptList[5], rptList[6], rptList[7], Environment.NewLine);
                    }


                    // If UDP add UDP header to report
                    if (evnt.Proto == (int)ProtocolFlagsEnums.UDP)
                    {
                        // Get the UDP hdr details
                        SguildConnection.SendToSguild(string.Format("ReportRequest UDP {0} {1}", evnt.AlertID.Split(new char[] { '.' }, 2)));

                        // Wait for results
                        MainForm.reportSignal.WaitOne(5000);
                        rptList = (List<string>)MainForm.reportQueue.Dequeue();
                        this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Get TCP/UDP/ICMP Header

                        returnString.AppendFormat("len={0} chksum={1}{2}", rptList[0], rptList[1], Environment.NewLine);
                    }


                    // If ICMP add ICMP header and payload to report
                    if (evnt.Proto == (int)ProtocolFlagsEnums.ICMP)
                    {
                        // Get the ICMP hdr details
                        SguildConnection.SendToSguild(string.Format("ReportRequest ICMP {0} {1}", evnt.AlertID.Split(new char[] { '.' }, 2)));

                        // Wait for results
                        MainForm.reportSignal.WaitOne(5000);
                        rptList = (List<string>)MainForm.reportQueue.Dequeue();
                        this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Get TCP/UDP/ICMP Header

                        returnString.AppendFormat("Type={0} Code={1} chksum={2} ID={3} seq={4}{5}", rptList[0], rptList[1], rptList[2], rptList[3], rptList[4], Environment.NewLine);

                        // Decode ICMP Packet
                        DecodedICMP icmpList = Tools.DecodeICMP(int.Parse(rptList[0]), int.Parse(rptList[1]), rptList[5]);

                        if (icmpList.isValidType && icmpList.isValidCode)
                        {
                            if (icmpList.gatewayIP != string.Empty)
                                returnString.AppendFormat("Gateway Address={0}{1} ", icmpList.gatewayIP);

                            returnString.AppendFormat("Protocol={0} ", icmpList.protocol);

                            if (!isSanitized)
                            {
                                returnString.AppendFormat("Orig Src IP:Port->Dst IP:Port {0}:{1}->{2}:{3}{4}", icmpList.srcIP, icmpList.srcPort, icmpList.dstIP, icmpList.dstPort, Environment.NewLine);
                            }
                            else
                            {
                                returnString.AppendFormat("Orig Src IP:Port->Dst IP:Port {0}:{1}->{2}:{3}", "a.b.c.d", icmpList.srcPort, "e.f.g.h", icmpList.dstPort);
                            }
                        }
                    }

                    this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Headers Complete

                    if (rbReportLevelDetailed.Checked)
                    {
                        // Get the payload details
                        SguildConnection.SendToSguild(string.Format("ReportRequest PAYLOAD {0} {1}", evnt.AlertID.Split(new char[] { '.' }, 2)));

                        // Wait for results
                        MainForm.reportSignal.WaitOne(5000);
                        rptList = (List<string>)MainForm.reportQueue.Dequeue();
                        this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Get Payload

                        string payload = rptList[0];

                        returnString.AppendLine("Payload:");

                        StringBuilder asciiStr = new StringBuilder();
                        StringBuilder hexStr = new StringBuilder();

                        string s = string.Empty;
                        int byteCount = 2;

                        for (int i = 0; i <= (payload.Length - 2); i += 2)
                        {
                            s = payload.Substring(i, 2);

                            hexStr.Append(s + " ");

                            char ch = Convert.ToChar(int.Parse(s, NumberStyles.HexNumber));

                            if ((ch < ' ') || (ch > '~'))
                            {
                                asciiStr.Append(".");
                            }
                            else
                            {
                                asciiStr.Append(ch.ToString());
                            }

                            if (byteCount == 32)
                            {
                                returnString.AppendFormat("{0} {1}{2}", hexStr, asciiStr, Environment.NewLine);
                                asciiStr.Length = 0;
                                hexStr.Length = 0;
                                byteCount = 2;
                            }
                            else
                            {
                                byteCount += 2;
                            }
                        }
                        returnString.AppendFormat("{0,-48} {1}{2}", hexStr, asciiStr, Environment.NewLine);
                    }
                    else
                    {
                        this.bwReportGen.ReportProgress(CalculateProgress()); //Step = Get Payload (skipped)
                    }
                }
                else
                {
                    // Build Port Scan Report
                }
            }

            return returnString.ToString();
        }

        private void saveReportFile()
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName);
                sw.WriteLine(reportText);
                sw.Close();
            }
        }

        private void bwReportGen_DoWork(object sender, DoWorkEventArgs e)
        {
            reportText = TextReport();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnOk.Enabled = false;

            if (this.chkbReportSanitized.Checked)
                isSanitized = true;

            this.bwReportGen.RunWorkerAsync();

            saveReportFile();
            
            this.Close();
        }

        private void bwReportGen_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }


}
