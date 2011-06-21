using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PT_Sguil
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            this.tbServerHost.Text = ConfigurationSupport.CfgServerHost;
            this.tbServerPort.Text = ConfigurationSupport.CfgServerPort.ToString();
            this.tbWiresharkPath.Text = ConfigurationSupport.wiresharkPath;
            this.tbBowserPath.Text = ConfigurationSupport.browserPath;
        }

        private void btnWrshrkPth_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tbWiresharkPath.Text = openFileDialog1.FileName;
            }
        }

        private void tbServerPort_TextChanged(object sender, EventArgs e)
        {
            //TO-DO: Check if valid INT btween 1-65356
        }
    }
}
