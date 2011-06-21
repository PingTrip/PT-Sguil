using System;

namespace PT_Sguil
{
    [Flags]
    public enum ProtocolFlagsEnums
    {
        None = 0,
        ICMP = 1,
        TCP = 6,
        UDP = 17
    }

}

