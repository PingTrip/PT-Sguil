using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PT_Sguil
{
    partial class SensorSelectForm : Form
    {
        public SensorSelectForm(Dictionary<string, List<string>> sensorList)
        {
            InitializeComponent();

            foreach (KeyValuePair<string, List<string>> pair in sensorList)
            {
                string item = string.Format("{0} ({1})", pair.Key, string.Join(" ", pair.Value.ToArray()));
                this.clbSensorList.Items.Add(item);
            }
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            if (this.clbSensorList.CheckedItems.Count > 0)
            {
                foreach (string str in this.clbSensorList.CheckedItems)
                {
                    ConfigurationSupport.monitoredSensors.Add(str.Remove(str.IndexOf(" (")));
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
            else
            {
                MessageBox.Show("You must select at least one sensor to monitor.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

