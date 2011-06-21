namespace PT_Sguil
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal class ExternalData
    {
        private static bool CreateXscriptWin(string winName)
        {
            if (MainForm.xscriptWindows.ContainsKey(winName))
            {
                MessageBox.Show("You are already viewing this transcript.\n\nPlease close the '" + winName + "' window before requesting a new one.", "Transcript Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            MainForm.xscriptWindows.Add(winName, new XscriptWinForm());
            MainForm.xscriptWindows[winName].Text = winName;
            MainForm.xscriptWindows[winName].Show();
            return true;
        }

        internal static Dictionary<string, string> GetReverseDNS(string srcIP, string dstIP)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("srcName", ResovleHost(srcIP));
            dictionary.Add("dstName", ResovleHost(dstIP));
            return dictionary;
        }

        internal static void GetXscript(string type, SguilEvent sguilEvent)
        {
            string[] strArray = sguilEvent.AlertID.Split(new char[] { '.' });
            string str = strArray[1];
            string str2 = strArray[0];
            string str3 = "0";
            string winName = string.Format("{0}_{1}", sguilEvent.Sensor.ToLower(), str);
            if (type.EndsWith("(Force New)"))
            {
                str3 = "1";
            }
            string timestamp = sguilEvent.Timestamp;
            if (type.StartsWith("Transcript"))
            {
                if (sguilEvent.Proto != (int)ProtocolFlagsEnums.TCP)
                {
                    MessageBox.Show("Sguild can only generate transcripts for TCP traffic.", "Transcript Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (CreateXscriptWin(winName))
                {
                    SguildConnection.SendToSguild(string.Format("XscriptRequest {0} {1} {2} {{{3}}} {4} {5} {6} {7} {8}", new object[] { sguilEvent.Sensor, str2, winName, timestamp, sguilEvent.SrcIP, sguilEvent.SrcPort, sguilEvent.DstIP, sguilEvent.DstPort, str3 }));
                }
            }
            else if (type.StartsWith("Wireshark"))
            {
                if (System.IO.File.Exists(ConfigurationSupport.wiresharkPath))
                {
                    SguildConnection.SendToSguild(string.Format("WiresharkRequest {0} {1} {{{2}}} {3} {4} {5} {6} {7} {8}", new object[] { sguilEvent.Sensor, str2, timestamp, sguilEvent.SrcIP, sguilEvent.SrcPort, sguilEvent.DstIP, sguilEvent.DstPort, sguilEvent.Proto, str3 }));
                }
                else
                {
                    MessageBox.Show("Unable to execute WireShark.\n\nPlease check your configuration setting.", "Transcript Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
        
        

        /// <summary>
        /// Resolves an IP Address to Host
        /// </summary>
        /// <param name="ipAddress">The IP Address to resolve.</param>
        /// <returns>The Hostname that resolves from <paramref name="ipAddress"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="ipAddress"/> is null or empty.</exception>
        private static string ResovleHost(string ipAddress)
        {
            string input = string.Empty;
            bool flag = false;
            string str2 = ipAddress;

            IPAddress address = IPAddress.Parse(ipAddress);
            IPAddress address2 = IPAddress.Parse("10.0.0.0");
            IPAddress subnetMask = IPAddress.Parse("255.0.0.0");
            IPAddress address4 = IPAddress.Parse("172.16.0.0");
            IPAddress address5 = IPAddress.Parse("255.240.0.0");
            IPAddress address6 = IPAddress.Parse("192.168.0.0");
            IPAddress address7 = IPAddress.Parse("255.255.0.0");

            if ((address.IsInSameSubnet(address2, subnetMask) || address.IsInSameSubnet(address4, address5)) || address.IsInSameSubnet(address6, address7))
            {
                flag = true;
            }
            if (!(!ConfigurationSupport.useExternalDns || flag))
            {
                str2 = str2 + " " + ConfigurationSupport.externalDNSServer;
            }
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "nslookup.exe";
                process.StartInfo.Arguments = str2;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                input = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            Match match = Regex.Match(input, @"Name:\s+(?<Host>\S+)\r", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return (match.Groups["Host"].Success ? match.Groups["Host"].Value : "Empty");
        }
    }
}

