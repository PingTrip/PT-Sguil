namespace PT_Sguil
{
    using System;
    using System.Collections.Generic;

    internal class SguilEvent
    {
        private string _alertid;
        private int _count;
        private string _dstip;
        private string _dstport;
        private string _eventmsg;
        private string _genID;
        private int _priority;
        private int _proto;
        private string _sensor;
        private string _sigID;
        private string _sigRev;
        private string _srcip;
        private string _srcport;
        private string _status;
        private string _timestamp;

        internal SguilEvent(string status, List<string> evnt)
        {
            this._status = status;
            if (status == "RT")
            {
                this._count = int.Parse(evnt[0x12]);
            }
            else
            {
                this._count = 1;
            }
            this._priority = int.Parse(evnt[1]);
            this._sensor = evnt[3];
            this._alertid = evnt[5] + "." + evnt[6];
            this._timestamp = evnt[4];
            this._srcip = evnt[8];
            this._srcport = evnt[11];
            this._dstip = evnt[9];
            this._dstport = evnt[12];
            this._proto = int.Parse(evnt[10]);
            this._eventmsg = evnt[7];

            if (evnt.Count >= 14)
                this._genID = evnt[13];
            if (evnt.Count >= 15)
                this._sigID = evnt[14];
            if (evnt.Count == 16)
                this._sigRev = evnt[15];
        }

        public string AlertID
        {
            get
            {
                return this._alertid;
            }
            set
            {
                this._alertid = value;
            }
        }

        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }

        public string DstIP
        {
            get
            {
                return this._dstip;
            }
            set
            {
                this._dstip = value;
            }
        }

        public string DstPort
        {
            get
            {
                return this._dstport;
            }
            set
            {
                this._dstport = value;
            }
        }

        public string EventMsg
        {
            get
            {
                return this._eventmsg;
            }
            set
            {
                this._eventmsg = value;
            }
        }

        public string GenID
        {
            get
            {
                return this._genID;
            }
            set
            {
                this._genID = value;
            }
        }

        public int Priority
        {
            get
            {
                return this._priority;
            }
            set
            {
                this._priority = value;
            }
        }

        public int Proto
        {
            get
            {
                return this._proto;
            }
            set
            {
                this._proto = value;
            }
        }

        public string Sensor
        {
            get
            {
                return this._sensor;
            }
            set
            {
                this._sensor = value;
            }
        }

        public string SigID
        {
            get
            {
                return this._sigID;
            }
            set
            {
                this._sigID = value;
            }
        }

        public string SigRev
        {
            get
            {
                return this._sigRev;
            }
            set
            {
                this._sigRev = value;
            }
        }

        public string SrcIP
        {
            get
            {
                return this._srcip;
            }
            set
            {
                this._srcip = value;
            }
        }

        public string SrcPort
        {
            get
            {
                return this._srcport;
            }
            set
            {
                this._srcport = value;
            }
        }

        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public string Timestamp
        {
            get
            {
                return this._timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }
    }
}

