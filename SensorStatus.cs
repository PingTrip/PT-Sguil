namespace PT_Sguil
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class SensorStatus : INotifyPropertyChanged
    {
        private string _Hostname;
        private string _Last;
        private string _Net;
        private string _Sid;
        private string _Status;
        private string _Type;

        public event PropertyChangedEventHandler PropertyChanged;

        internal SensorStatus(string sid, string net, string hostname, string type, string last, string status)
        {
            this._Sid = sid;
            this._Net = net;
            this._Hostname = hostname;
            this._Type = type;
            this._Last = last;
            this._Status = status;
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
            }
        }

        public string Hostname
        {
            get
            {
                return this._Hostname;
            }
            set
            {
                this._Hostname = value;
            }
        }

        public string Last
        {
            get
            {
                return this._Last;
            }
            set
            {
                this._Last = value;
            }
        }

        public string Net
        {
            get
            {
                return this._Net;
            }
            set
            {
                this._Net = value;
            }
        }

        public string Sid
        {
            get
            {
                return this._Sid;
            }
            set
            {
                this._Sid = value;
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
    }
}

