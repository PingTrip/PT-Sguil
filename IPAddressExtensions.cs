namespace PT_Sguil
{
    using System;
    using System.Net;
    using System.Runtime.CompilerServices;

    // Knom's Developer Corner
    // http://blogs.msdn.com/b/knom/archive/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks.aspx

    public static class IPAddressExtensions
    {
        private static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] addressBytes = address.GetAddressBytes();
            byte[] buffer2 = subnetMask.GetAddressBytes();
            if (addressBytes.Length != buffer2.Length)
            {
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");
            }
            byte[] buffer3 = new byte[addressBytes.Length];
            for (int i = 0; i < buffer3.Length; i++)
            {
                buffer3[i] = (byte) (addressBytes[i] | (buffer2[i] ^ 0xff));
            }
            return new IPAddress(buffer3);
        }

        private static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] addressBytes = address.GetAddressBytes();
            byte[] buffer2 = subnetMask.GetAddressBytes();
            if (addressBytes.Length != buffer2.Length)
            {
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");
            }
            byte[] buffer3 = new byte[addressBytes.Length];
            for (int i = 0; i < buffer3.Length; i++)
            {
                buffer3[i] = (byte) (addressBytes[i] & buffer2[i]);
            }
            return new IPAddress(buffer3);
        }

        internal static bool IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            IPAddress networkAddress = address.GetNetworkAddress(subnetMask);
            IPAddress address4 = address2.GetNetworkAddress(subnetMask);
            return networkAddress.Equals(address4);
        }
    }
}

