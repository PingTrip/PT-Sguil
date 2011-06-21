using System;

namespace PT_Sguil
{


    [Flags]
    public enum TCPFlagsEnum
    {
        Ack = 0x10,
        Fin = 1,
        None = 0,
        Psh = 8,
        R0 = 0x40,
        R1 = 0x80,
        Rst = 4,
        Syn = 2,
        Urg = 0x20
    }
}
