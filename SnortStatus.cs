namespace PT_Sguil
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class SnortStatus : INotifyPropertyChanged
    {
        private string _alerts;
        private string _averageBW;
        private string _bytes;
        private string _match;
        private string _maxSessions;
        private string _newSessions;
        private string _packetLoss;
        private string _packets;
        private string _sensor;
        private string _sid;
        private string _timestamp;
        private string _totalSessions;

        public event PropertyChangedEventHandler PropertyChanged;

        internal SnortStatus(List<string> snort)
        {
            this.UpdateSnortStats(snort);
        }

        private string FormatStat(string prop, string stat)
        {
            if (stat != "N/A")
            {
                switch (prop)
                {
                    case "PacketLoss":
                    case "Match":
                        stat = stat + "%";
                        return stat;

                    case "AverageBW":
                        stat = stat + " Mb/s";
                        return stat;

                    case "Packets":
                        stat = stat + " k/s";
                        return stat;

                    case "Bytes":
                        stat = stat + "/pckt";
                        return stat;

                    case "Alerts":
                    case "NewSessions":
                        stat = stat + "/sec";
                        return stat;
                }
            }
            return stat;
        }

        internal void UpdateSnortStats(List<string> snort)
        {
            this._sid = snort[0];
            this._sensor = snort[1];
            this._packetLoss = snort[2];
            this._averageBW = snort[3];
            this._alerts = snort[4];
            this._packets = snort[5];
            this._bytes = snort[6];
            this._match = snort[7];
            this._newSessions = snort[8];
            this._totalSessions = snort[9];
            this._maxSessions = snort[10];
            this._timestamp = snort[11];
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        public string Alerts
        {
            get
            {
                return this.FormatStat("Alerts", this._alerts);
            }
            set
            {
                this._alerts = value;
            }
        }

        public string AverageBW
        {
            get
            {
                return this.FormatStat("AverageBW", this._averageBW);
            }
            set
            {
                this._averageBW = value;
            }
        }

        public string Bytes
        {
            get
            {
                return this.FormatStat("Bytes", this._bytes);
            }
            set
            {
                this._bytes = value;
            }
        }

        public string Match
        {
            get
            {
                return this.FormatStat("Match", this._match);
            }
            set
            {
                this._match = value;
            }
        }

        public string MaxSessions
        {
            get
            {
                return this._maxSessions;
            }
            set
            {
                this._maxSessions = value;
            }
        }

        public string NewSessions
        {
            get
            {
                return this.FormatStat("NewSessions", this._newSessions);
            }
            set
            {
                this._newSessions = value;
            }
        }

        public string PacketLoss
        {
            get
            {
                return this.FormatStat("PacketLoss", this._packetLoss);
            }
            set
            {
                this._packetLoss = value;
            }
        }

        public string Packets
        {
            get
            {
                return this.FormatStat("Packets", this._packets);
            }
            set
            {
                this._packets = value;
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

        public string Sid
        {
            get
            {
                return this._sid;
            }
            set
            {
                this._sid = value;
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

        public string TotalSessions
        {
            get
            {
                return this._totalSessions;
            }
            set
            {
                this._totalSessions = value;
            }
        }
    }
}

