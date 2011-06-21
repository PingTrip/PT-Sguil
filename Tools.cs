using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT_Sguil
{
    class Tools
    {
        internal static string Base64Decode(string data)
        {
            string result = String.Empty;

            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                result = new String(decoded_char);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return result;
        }

        internal static string Base64Encode(string data)
        {
            string encodedData = string.Empty;

            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                encodedData = Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return encodedData;
        }

        internal static DecodedICMP DecodeICMP(int icmpType, int icmpCode, string icmpPayload)
        {
            DecodedICMP icmpList = new DecodedICMP();

            if (icmpType == 3 || icmpType == 5 || icmpType == 11)
            {
                if (icmpCode == 0 || icmpCode == 1 || icmpCode == 2 || icmpCode == 3 || icmpCode == 4 || icmpCode == 9 || icmpCode == 13)
                {
                    int offset = 0;

                    if (icmpPayload.StartsWith("00000000") || icmpType == 5)
                    {
                        offset = 8;

                        if (icmpType == 5)
                        {
                            icmpList.gatewayIP = "";
                        }

                    }

                    // Build the protocol
                    icmpList.protocol = string.Format("{0:X}", icmpPayload[offset + 18] + icmpPayload[offset + 19]);

                    // Build the source IP address
                    icmpList.srcIP += string.Format("{0:X}.", icmpPayload[offset + 24] + icmpPayload[offset + 25]);
                    icmpList.srcIP += string.Format("{0:X}.", icmpPayload[offset + 26] + icmpPayload[offset + 27]);
                    icmpList.srcIP += string.Format("{0:X}.", icmpPayload[offset + 28] + icmpPayload[offset + 29]);
                    icmpList.srcIP += string.Format("{0:X}", icmpPayload[offset + 30] + icmpPayload[offset + 31]);

                    // Build the destination IP address
                    icmpList.dstIP += string.Format("{0:X}.", icmpPayload[offset + 32] + icmpPayload[offset + 33]);
                    icmpList.dstIP += string.Format("{0:X}.", icmpPayload[offset + 34] + icmpPayload[offset + 35]);
                    icmpList.dstIP += string.Format("{0:X}.", icmpPayload[offset + 36] + icmpPayload[offset + 37]);
                    icmpList.dstIP += string.Format("{0:X}", icmpPayload[offset + 38] + icmpPayload[offset + 39]);

                    // Get the header offset
                    int hdrOffset = int.Parse(icmpPayload.Substring(offset + 1, 1)) * 8 + offset;

                    // Build the source port
                    icmpList.srcPort = string.Format("{0:X}", icmpPayload.Substring(hdrOffset, hdrOffset + 3));

                    // Build the destination port
                    icmpList.dstPort = string.Format("{0:X}", icmpPayload.Substring(hdrOffset + 4, hdrOffset + 7));
                }
                else
                {
                    // Unknown ICMP Code
                    icmpList.isValidCode = false;
                }
            }
            else
            {
                // Unknown ICMP Type
                icmpList.isValidType = false;
            }

            return icmpList;
        }
    }

    class DecodedICMP
    {
        public string dstIP { get; set; }
        public string dstPort { get; set; }
        public string protocol { get; set; }
        public string srcIP { get; set; }
        public string srcPort { get; set; }
        public string gatewayIP { get; set; }
        public bool isValidType { get; set; }
        public bool isValidCode { get; set; }

        internal DecodedICMP()
        {
            this.isValidCode = true;
            this.isValidType = true;
        }
    }
}
