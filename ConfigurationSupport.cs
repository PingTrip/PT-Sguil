namespace PT_Sguil
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal static class ConfigurationSupport
    {
        internal static string browserPath;
        internal static readonly int bufferSize = 0x100;
        internal static string configFilePath;
        internal static string currentHost;
        internal static string currentPassword;
        internal static string currentPort;
        internal static string currentUsername;
        internal static bool debug = false;
        internal static bool displayPacketSearchBox = true;
        internal static string emailCC;
        internal static string emailFrom;
        internal static string emailHead;
        internal static string emailSubject;
        internal static string emailTail;
        internal static Dictionary<int, EventPriorityConfig> eventConfigsByPriorty = new Dictionary<int, EventPriorityConfig>();
        internal static string externalDNSServer;
        internal static string heloHostname;
        internal static Dictionary<IPAddress, IPAddress> homeNet = new Dictionary<IPAddress, IPAddress>();
        internal static bool isConfigured = false;
        internal static bool isInitialLoad = true;
        internal static bool isSleeping = false;
        internal static bool isSoundActive = false;
        internal static string mailServer;
        internal static int maxPSRows = 200;
        internal static List<string> monitoredSensors = new List<string>();
        internal static int numberOfRTPanes = 1;
        internal static int reverseDNSTimeout = 5000;
        internal static Color selectedRowBackgroundColor = Color.Empty;
        internal static Color selectedRowForegroundColor = Color.Empty;
        internal static string CfgServerHost;
        internal static int CfgServerPort = 7734;
        internal static bool showGMTClock = true;
        internal static int sleepBalloonTimeout = 5000;
        internal static int statusUpdateInterval = 15;
        internal static bool useExternalDns = false;
        internal static string userID;
        internal static readonly string version = "SGUIL-0.8.0 OPENSSL ENABLED";
        internal static string wiresharkPath;
        internal static string wiresharkStorageDir;
        internal static int XscriptAutoAbortByteCount = 32000;
        internal static string packetAsciiDataFormatted = String.Empty;
        internal static bool isAutoScrollEnabled = false;

        private static Color GetColorObject(string colorCode)
        {
            if (string.IsNullOrEmpty(colorCode))
                throw new ArgumentException("Argument 'colorCode' cannot be null or empty.");

            Color empty = Color.Empty;

            if (colorCode.Contains("#"))
            {
                try
                {
                    return ColorTranslator.FromHtml(colorCode);
                }
                catch (Exception)
                {
                    return Color.Empty;
                }
            }

            if (Color.FromName(colorCode).IsNamedColor)
            {
                empty = Color.FromName(colorCode);
            }

            return empty;
        }

        internal static bool LoadConfig()
        {
            if (Application.UserAppDataRegistry.GetValue("ConfigFile") != null)
            {
                configFilePath = Application.UserAppDataRegistry.GetValue("ConfigFile").ToString();
            }
            else if (System.IO.File.Exists(Application.StartupPath + @"\sguil.conf"))
            {
                configFilePath = Application.StartupPath + @"\sguil.conf";
            }
            else if (MessageBox.Show("Please select a Sguil config file to use.", "Configure PTSguil", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                configFilePath = SelectConfigFile(Application.StartupPath.ToString());
            }

            if (configFilePath != null)
            {
                isConfigured = ParseConfigFile(configFilePath);
            }
            return isConfigured;
        }

        private static bool ParseConfigFile(string configFile)
        {
            string[] strArray;
            int num7;
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
            Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
            string s = null;
            if (System.IO.File.Exists(configFile))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(configFile))
                    {
                        for (s = reader.ReadLine(); s != null; s = reader.ReadLine())
                        {
                            if (s.StartsWith("set "))
                            {
                                List<string> list = SguildCommands.ParseTclList(s);
                                string input = list[1].ToLower();
                                string str3 = list[2];
                                switch (input)
                                {
                                    case "serverport":
                                        if (int.TryParse(str3, out CfgServerPort) && ((CfgServerPort < 1) || (CfgServerPort > 65535)))
                                        {
                                            CfgServerPort = 7734;
                                        }
                                        break;

                                    case "serverhost":
                                        CfgServerHost = str3;
                                        break;

                                    case "debug":
                                        if (str3 == "1")
                                        {
                                            debug = true;
                                        }
                                        break;

                                    case "wireshark_path":
                                        if (System.IO.File.Exists(str3))
                                        {
                                            wiresharkPath = str3;
                                        }
                                        else
                                        {
                                            wiresharkPath = string.Empty;
                                        }
                                        break;

                                    case "wireshark_store_dir":
                                        if (Directory.Exists(str3))
                                        {
                                            wiresharkStorageDir = str3;
                                        }
                                        else
                                        {
                                            wiresharkPath = string.Empty;
                                        }
                                        break;

                                    case "browser_path":
                                        browserPath = str3;
                                        break;

                                    case "status_updates":
                                        if (int.TryParse(str3, out statusUpdateInterval) && (statusUpdateInterval < 0))
                                        {
                                            statusUpdateInterval = 15;
                                        }
                                        break;

                                    case "ext_dns":
                                        if (str3 == "1")
                                        {
                                            useExternalDns = true;
                                        }
                                        break;

                                    case "ext_dns_server":
                                        externalDNSServer = str3;
                                        break;

                                    case "home_net":
                                    {
                                        int result = 0;
                                        foreach (string str4 in str3.Trim(new char[] { '"' }).Split(new char[] { ' ' }))
                                        {
                                            Match match = Regex.Match(str4, @"^(.+)/(\d+)$");
                                            if (int.TryParse(match.Groups[2].Value, out result))
                                            {
                                                string ipString = string.Empty;
                                                long num2 = (long) ((ulong) ~(((int) (-1)) >> result));
                                                for (int i = 0; i < 4; i++)
                                                {
                                                    int num4 = (int) (((double) num2) / Math.Pow(256.0, (double) (3 - i)));
                                                    num2 -= (long) (num4 * Math.Pow(256.0, (double) (3 - i)));
                                                    if (i == 0)
                                                    {
                                                        ipString = num4.ToString();
                                                    }
                                                    else
                                                    {
                                                        ipString = ipString + "." + num4.ToString();
                                                    }
                                                }
                                                homeNet.Add(IPAddress.Parse(match.Groups[1].Value), IPAddress.Parse(ipString));
                                            }
                                        }
                                        break;
                                    }
                                    case "searchframe":
                                        if (str3 == "0")
                                        {
                                            displayPacketSearchBox = false;
                                        }
                                        break;

                                    case "rtpanes":
                                        if (int.TryParse(str3, out numberOfRTPanes) && ((numberOfRTPanes < 1) || (numberOfRTPanes > 3)))
                                        {
                                            numberOfRTPanes = 1;
                                        }
                                        break;

                                    default:
                                        if (input.StartsWith("rtpane_priority("))
                                        {
                                            int num5 = 0;
                                            if (int.TryParse(Regex.Match(input, @"\d+").Value, out num5))
                                            {
                                                dictionary[num5] = str3;
                                            }
                                        }
                                        else
                                        {
                                            int num6;
                                            if (input.StartsWith("rtcolor_priority("))
                                            {
                                                num6 = 0;
                                                if (int.TryParse(Regex.Match(input, @"\d+").Value, out num6))
                                                {
                                                    dictionary2[num6] = str3;
                                                }
                                            }
                                            else if (input.StartsWith("rtcolor_name("))
                                            {
                                                num6 = 0;
                                                if (int.TryParse(Regex.Match(input, @"\d+").Value, out num6))
                                                {
                                                    dictionary3[num6] = str3;
                                                }
                                            }
                                            else if (!input.StartsWith("category_color("))
                                            {
                                                if (input == "selectbackground")
                                                {
                                                    selectedRowBackgroundColor = GetColorObject(str3);
                                                }
                                                else if (input == "selectforeground")
                                                {
                                                    selectedRowForegroundColor = GetColorObject(str3);
                                                }
                                                else if (input == "max_ps_rows")
                                                {
                                                    int.TryParse(str3, out maxPSRows);
                                                }
                                                else if (input == "gmtclock")
                                                {
                                                    if (str3 == "0")
                                                    {
                                                        showGMTClock = false;
                                                    }
                                                }
                                                else if (input == "mailserver")
                                                {
                                                    mailServer = str3;
                                                }
                                                else if (input == "hostname")
                                                {
                                                    heloHostname = str3;
                                                }
                                                else if (input == "email_from")
                                                {
                                                    emailFrom = str3;
                                                }
                                                else if (input == "email_cc")
                                                {
                                                    emailCC = str3;
                                                }
                                                else if (input == "email_subject")
                                                {
                                                    emailSubject = str3;
                                                }
                                                else if (input == "email_head")
                                                {
                                                    emailHead = str3;
                                                }
                                                else if (input == "email_tail")
                                                {
                                                    emailTail = str3;
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        isConfigured = true;
                    }
                }
                catch (Exception exception)
                {
                    isConfigured = false;
                    MessageBox.Show(exception.ToString());
                }
            }
            eventConfigsByPriorty[0] = new EventPriorityConfig(0, 1, GetColorObject("blue"));
            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                strArray = pair.Value.Split(new char[] { ' ' });
                num7 = 0;
                foreach (string str6 in strArray)
                {
                    if (int.TryParse(str6, out num7))
                    {
                        if (eventConfigsByPriorty.ContainsKey(num7))
                        {
                            eventConfigsByPriorty[num7].RTPane = pair.Key;
                        }
                        else
                        {
                            eventConfigsByPriorty[num7] = new EventPriorityConfig(num7, pair.Key, GetColorObject("purple"));
                        }
                    }
                }
            }
            foreach (KeyValuePair<int, string> pair in dictionary2)
            {
                strArray = pair.Value.Split(new char[] { ' ' });
                num7 = 0;
                foreach (string str6 in strArray)
                {
                    if (int.TryParse(str6, out num7))
                    {
                        if (eventConfigsByPriorty.ContainsKey(num7))
                        {
                            eventConfigsByPriorty[num7].EvntColor = GetColorObject(dictionary3[pair.Key]);
                        }
                        else
                        {
                            eventConfigsByPriorty[num7] = new EventPriorityConfig(num7, 1, GetColorObject(dictionary3[pair.Key]));
                        }
                    }
                }
            }
            return isConfigured;
        }

        private static string SelectConfigFile(string initialDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "conf files (*.conf)|*.conf|All files (*.*)|*.*",
                InitialDirectory = initialDirectory,
                Title = "Select a Sguil config file"
            };
            return ((dialog.ShowDialog() == DialogResult.OK) ? dialog.FileName : null);
        }
    }
}

