namespace PT_Sguil
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal static class SguildCommands
    {
        private static void DeleteEventID()
        {
        }

        private static void DeleteEventIDList(string s)
        {
            s = s.Trim(new char[] { '{', '}' });
            List<string> strEvents = new List<string>(s.Split(new char[] { ' ' }));
            bool isLastRowSelected = false;

            // Clear event from RT Panes
            for (int i = 0; i < ConfigurationSupport.numberOfRTPanes; i++)
            {
                if (MainForm.dgvList[i].SelectedRows.Count > 0 && MainForm.dgvList[i].SelectedRows[0].Index == MainForm.dgvList[i].RowCount - 1)
                    isLastRowSelected = true;

                for (int j = MainForm.RTEventLists[i].Count - 1; j >= 0; j--)
                {
                    SguilEvent event2 = MainForm.RTEventLists[i][j];

                    if (strEvents.Contains(event2.AlertID))
                        MainForm.RTEventLists[i].RemoveAt(j);
                }

                if (isLastRowSelected && MainForm.dgvList[i].Rows.Count > 0)
                    MainForm.dgvList[i].Rows[MainForm.dgvList[i].RowCount - 1].Selected = true;
            }

            // Clear event from ES Pane
            for (int j = MainForm.EventListES.Count - 1; j >= 0; j--)
            {
                SguilEvent event2 = MainForm.EventListES[j];

                if (strEvents.Contains(event2.AlertID))
                    MainForm.EventListES.RemoveAt(j);
            }
        }

        private static void ErrorMessage(string s)
        {
            MessageBox.Show(s.Trim(new char[] { '{', '}' }), "ErrMsg From Sguild", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private static void GlobalQryList()
        {
        }

        private static void IncrEvent(string s)
        {
            string[] strArray = s.Split(new char[] { ' ' });
            int eventIndex = -1;
            string eventMsg = string.Empty;

            int rTPane = ConfigurationSupport.eventConfigsByPriorty[int.Parse(strArray[2])].RTPane;

            // Add count to RT Event
            eventIndex = MainForm.RTEventLists[rTPane].Find("AlertID", strArray[0]);

            if (eventIndex != -1)
            {
                eventMsg = ((SguilEvent)MainForm.RTEventLists[rTPane][eventIndex]).EventMsg;

                ((SguilEvent)MainForm.RTEventLists[rTPane][eventIndex]).Count = int.Parse(strArray[1]);

                if (ConfigurationSupport.isSleeping)
                {
                    Program.MainForm.notifyIcon_BalloonText("Event Incremented", eventMsg);
                }
                else
                {
                    Program.MainForm.dgvRefresh(rTPane);
                }
            }
        }

        private static void InfoMessage()
        {
        }

        private static void InsertEmailIPHdr()
        {
        }

        private static void InsertEscalatedEvent(string s)
        {
            List<string> evnt = ParseTclList(s);

            MainForm.EventListES.Add(new SguilEvent("ES", evnt));


            if (ConfigurationSupport.isSleeping)
            {
                Program.MainForm.notifyIcon_BalloonText("ES Event", evnt[7]);
            }
        }

        private static void InsertEvent(string s)
        {
            List<string> evnt = ParseTclList(s);
            int rTPane = 0;
            int key = int.Parse(evnt[1]);
            
            if (ConfigurationSupport.numberOfRTPanes > 1)
            {
                if (ConfigurationSupport.eventConfigsByPriorty.ContainsKey(key))
                {
                    rTPane = ConfigurationSupport.eventConfigsByPriorty[key].RTPane;
                }
                else
                {
                    rTPane = ConfigurationSupport.numberOfRTPanes;
                }
            }

            MainForm.RTEventLists[rTPane].Add(new SguilEvent("RT", evnt));

            if (ConfigurationSupport.isSleeping)
            {
                Program.MainForm.notifyIcon_BalloonText("RT Event", evnt[7]);
            }

            if (ConfigurationSupport.isAutoScrollEnabled)
            {
                MainForm.dgvList[rTPane].FirstDisplayedScrollingRowIndex = MainForm.dgvList[rTPane].RowCount - 1;
            }
        }

        private static void InsertGenericDetail()
        {
        }

        private static void InsertHistoryResults()
        {
        }

        private static void InsertIcmpHdr(string s)
        {
            List<string> a = ParseTclList(s);
            Program.MainForm.InsertICMPHdr(a);
        }

        private static void InsertIPHdr(string s)
        {
            List<string> a = ParseTclList(s);
            Program.MainForm.InsertIPHdr(a);
        }

        private static void InsertOpenPortsData()
        {
        }

        private static void InsertPadsBanner()
        {
        }

        private static void InsertPayloadData(string s)
        {
            List<string> list = ParseTclList(s);
            Program.MainForm.InsertPayloadData(list[0].ToString());
        }

        private static void InsertQueryResults()
        {
        }

        private static void InsertRuleData(string s)
        {
            s = s.Trim(new char[] { '{', '}' });
            Program.MainForm.InsertRuleData(s);
        }

        private static void InsertSancpFlags()
        {
        }

        private static void InsertSystemInfoMsg(string s)
        {
            List<string> list = ParseTclList(s);
            string str = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd HH':'mm':'ss' GMT'");
            Program.MainForm.InsertSystemInfoMsg(string.Format("[{0}] {1}: {2}{3}", new object[] { str, list[0], list[1], Environment.NewLine }));
        }

        private static void InsertTcpHdr(string s)
        {
            List<string> a = ParseTclList(s);
            Program.MainForm.InsertTCPHdr(a);
        }

        private static void InsertUdpHdr(string s)
        {
            Program.MainForm.InsertUDPHdr(s);
        }

        private static void NewSnortStats(string s)
        {
            SortedList<string, List<string>> list = ParseTclArray(s);
            MainForm.SnortStatList.Clear();
            
            foreach (KeyValuePair<string, List<string>> pair in list)
            {
                MainForm.SnortStatList.Add(new SnortStatus(pair.Value));
            }
        }

        private static void PassChange()
        {
        }

        private static void PONG()
        {
        }

        private static void PSDataResults()
        {
        }

        private static void ReportQryList()
        {
        }

        private static void ReportResponse(string s)
        {
            // IP {10.20.30.40 100.101.102.103 4 5 0 545 14094 0 0 63 48577}
            // IP done
            string[] strArray = s.Split(new char[] { ' ' }, 2);

            if (strArray[1] == "done")
            {
                MainForm.reportSignal.Set();
            }
            else
            {
                List<string> rptList = SguildCommands.ParseTclList(strArray[1]);
                MainForm.reportQueue.Enqueue(rptList);
            }
            
        }

        internal static void RequestPacketData(string alertid, int proto)
        {
            string[] strArray = alertid.Split(new char[] { '.' });

            SguildConnection.SendToSguild(string.Format("GetIPData {0} {1}", strArray[0], strArray[1]));

            if (!(proto == (int)ProtocolFlagsEnums.ICMP))
            {
                if (proto == (int)ProtocolFlagsEnums.TCP)
                {
                    SguildConnection.SendToSguild(string.Format("GetTcpData {0} {1}", strArray[0], strArray[1]));
                }
                else if (proto == (int)ProtocolFlagsEnums.UDP)
                {
                    SguildConnection.SendToSguild(string.Format("GetUdpData {0} {1}", strArray[0], strArray[1]));
                }
            }
            else
            {
                SguildConnection.SendToSguild(string.Format("GetIcmpData {0} {1}", strArray[0], strArray[1]));
            }

            SguildConnection.SendToSguild(string.Format("GetPayloadData {0} {1}", strArray[0], strArray[1]));
        }

        private static void SensorList(string s)
        {
            // SensorList {GTWY unmonitored}
            Dictionary<string, List<string>> sensorList = new Dictionary<string, List<string>>();
            MatchCollection matchs = Regex.Matches(s, "{[^{}]*(((?<Start>{)[^{}]*)+((?<End-Start>})[^{}]*)+)*(?(Start)(?!))}");
            
            foreach (Match match in matchs)
            {
                string str = match.Value;

                if (str.StartsWith("{") && str.EndsWith("}"))
                {
                    str = str.Remove(str.Length - 1, 1).Remove(0, 1);
                }

                string[] strArray = str.Split(new char[] { ' ' }, 2);
                
                if (strArray[1].StartsWith("{"))
                {
                    sensorList[strArray[0]] = ParseTclList(strArray[1]);
                }
                else
                {
                    List<string> list = new List<string> {
                        strArray[1]
                    };
                    sensorList[strArray[0]] = list;
                }
            }

            Program.MainForm.ChooseSensorToMonitor(sensorList);
        }

        private static void SensorStatusUpdate(string s)
        {
            SortedList<string, List<string>> list = ParseTclArray(s);
            string status = string.Empty;
            MainForm.SensorStatList.Clear();

            foreach (KeyValuePair<string, List<string>> pair in list)
            {
                if (pair.Value[4] == "0")
                {
                    status = "DOWN";
                }
                else
                {
                    status = "UP";
                }
                MainForm.SensorStatList.Add(new SensorStatus(pair.Key, pair.Value[0], pair.Value[1], pair.Value[2], pair.Value[3], status));
            }
        }

        internal static void ServerCommandRcvd(string msg)
        {
            string[] strArray = msg.Split(new char[] { ' ' }, 2);

            try
            {
                System.Type.GetType(MethodBase.GetCurrentMethod().DeclaringType.ToString()).InvokeMember(strArray[0], BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static, null, null, new object[] { strArray[1] });
            }
            catch (MissingMethodException exception)
            {
                MessageBox.Show("UnhandledCMD:\n" + exception.Message);
            }
        }

        private static void TableColumns(string s)
        {
        }

        private static void TableNameList(string s)
        {
        }

        private static void UpdateSnortStats(string s)
        {
            List<string> snort = ParseTclList(s);
            for (int i = 0; i < MainForm.SnortStatList.Count; i++)
            {
                if (MainForm.SnortStatList[i].Sid == snort[0].ToString())
                {
                    MainForm.SnortStatList[i].UpdateSnortStats(snort);
                }
            }
        }

        private static void UserID()
        {
        }

        private static void UserMessage()
        {
        }

        internal static void ValidateEvent(string eventIDS, int status, bool withComment)
        {
            string commentText = string.Empty;
            DialogResult cancel = DialogResult.Cancel;

            if (!SguildConnection.isConnected)
            {
                MessageBox.Show("Not Connected to Sguild. Cannot validate events at this time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (withComment)
                {
                    EventCommentForm comment = new EventCommentForm();
                    cancel = comment.ShowDialog();
                    commentText = comment.CommentText;
                }

                if ((withComment && (cancel == DialogResult.OK)) || !withComment)
                {
                    if (commentText == string.Empty)
                    {
                        commentText = "none";
                    }
                    else
                    {
                        commentText = commentText = "{" + commentText + "}";
                    }
                    DeleteEventIDList(eventIDS);
                    SguildConnection.SendToSguild(string.Format("DeleteEventIDList {0} {1} {2}", status, commentText, eventIDS));
                }
            }
        }

        private static void XscriptDebugMsg(string s)
        {
            string[] strArray = s.Split(new char[] { ' ' }, 2);
            string str = strArray[0];
            string str2 = strArray[1];
            MainForm.xscriptWindows[strArray[0]].XscriptDebugMsg(strArray[1]);
        }

        private static void XscriptMainMsg(string s)
        {
            string[] strArray = s.Split(new char[] { ' ' }, 2);
            MainForm.xscriptWindows[strArray[0]].XscriptMainMsg(strArray[1]);
        }


        // Custom parsing routines to convert TCL Lists into C# objects
        //
        internal static List<string> ParseTclList(string s)
        {
            s = s.Trim(new char[] { '{', '}' });
            bool flag = false;
            bool flag2 = false;
            int num = 0;
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();

            if (s.Length == 0)
            {
                list.Add(string.Empty);
                return list;
            }

            foreach (char ch in s)
            {
                num++;
                string str2 = ch.ToString();
                if (str2 == null)
                {
                    goto Label_0129;
                }
                if (!(str2 == "{"))
                {
                    if (str2 == "}")
                    {
                        goto Label_00C5;
                    }
                    if (str2 == "\"")
                    {
                        goto Label_00CC;
                    }
                    if (str2 == " ")
                    {
                        goto Label_00F3;
                    }
                    goto Label_0129;
                }
                flag = true;
                goto Label_0155;
            Label_00C5:
                flag = false;
                goto Label_0155;
            Label_00CC:
                flag2 = !flag2;
                if (s.Length == num)
                {
                    list.Add(builder.ToString());
                }
                goto Label_0155;
            Label_00F3:
                if (!(flag || flag2))
                {
                    list.Add(builder.ToString());
                    builder.Length = 0;
                }
                else
                {
                    builder.Append(ch);
                }
                goto Label_0155;
            Label_0129:
                builder.Append(ch);
                if (s.Length == num)
                {
                    list.Add(builder.ToString());
                }
            Label_0155: ;
            }
            return list;
        }

        internal static SortedList<string, List<string>> ParseTclArray(string s)
        {
            bool flag;
            bool flag2;

            if (s.StartsWith("{") && s.EndsWith("}"))
            {
                s = s.Remove(s.Length - 1, 1).Remove(0, 1);
            }

            SortedList<string, List<string>> list = new SortedList<string, List<string>>();
            List<string> collection = new List<string>();
            string str = string.Empty;

            if (s.StartsWith("{"))
            {
                flag = false;
                flag2 = true;
            }
            else
            {
                flag = true;
                flag2 = false;
            }

            int num = 0;
            bool flag3 = false;
            int num2 = 0;
            StringBuilder builder = new StringBuilder();

            if (s.Length == 0)
            {
                collection.Add(string.Empty);
                return list;
            }

            foreach (char ch in s)
            {
                num2++;
                string str3 = ch.ToString();

                if (str3 == null)
                {
                    goto Label_02A3;
                }

                if (!(str3 == "{"))
                {
                    if (str3 == "}")
                    {
                        goto Label_0124;
                    }
                    if (str3 == "\"")
                    {
                        goto Label_01E1;
                    }
                    if (str3 == " ")
                    {
                        goto Label_020E;
                    }
                    goto Label_02A3;
                }
                num++;
                goto Label_02D2;
            Label_0124:
                num--;
                if (flag && (num == 0))
                {
                    collection.Add(builder.ToString());
                    builder.Length = 0;
                    list[str] = new List<string>(collection);
                    str = string.Empty;
                    collection.Clear();
                }
                else if (builder.Length == 0)
                {
                    collection.Add(string.Empty);
                }
                else if (flag2)
                {
                    collection.Add(builder.ToString());
                    builder.Length = 0;
                    list[collection[0].ToString()] = new List<string>(collection);
                    collection.Clear();
                }
                goto Label_02D2;
            Label_01E1:
                flag3 = !flag3;
                if (s.Length == num2)
                {
                    collection.Add(builder.ToString());
                }
                goto Label_02D2;
            Label_020E:
                if (((num == 0) && (str == string.Empty)) && (builder.Length > 0))
                {
                    str = builder.ToString();
                    builder.Length = 0;
                }
                else if ((num == 1) && (builder.Length > 0))
                {
                    collection.Add(builder.ToString());
                    builder.Length = 0;
                }
                else if ((num > 1) || flag3)
                {
                    builder.Append(ch);
                }
                goto Label_02D2;
            Label_02A3:
                builder.Append(ch);
                if (s.Length == num2)
                {
                    collection.Add(builder.ToString());
                }
            Label_02D2: ;
            }
            return list;
        }
    }
}

