namespace PT_Sguil
{
    using System;
    using System.Drawing;

    internal class EventPriorityConfig
    {
        private Color _evntColor;
        private int _priority;
        private int _rtPane;

        internal EventPriorityConfig(int priority, int rtPane, Color evntColor)
        {
            this._priority = priority;
            this._rtPane = rtPane;
            this._evntColor = evntColor;
        }

        public Color EvntColor
        {
            get
            {
                return this._evntColor;
            }
            set
            {
                this._evntColor = value;
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

        public int RTPane
        {
            get
            {
                return this._rtPane;
            }
            set
            {
                this._rtPane = value;
            }
        }
    }
}

