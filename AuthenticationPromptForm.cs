using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PT_Sguil
{
    partial class AuthenticationPromptForm : Form
    {
        public AuthenticationPromptForm()
        {
            InitializeComponent();

            foreach (string host in ConfigurationSupport.CfgServerHost.Split(' '))
            {
                this.ServerHostCombinationBox.Items.Add(host);
            }

            //this.cbServerHost.SelectedIndex = this.cbServerHost.Items.IndexOf("Host string from registry");

            this.ServerHostCombinationBox.SelectedIndex = 0;

            this.ServerPortTextBox.Text = ConfigurationSupport.CfgServerPort.ToString();
            this.userNameTextBox.Text = "";
            this.passwordTextBox.Text = "";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if ((((this.ServerHostCombinationBox.Text != "") && (this.ServerPortTextBox.Text != "")) && (this.userNameTextBox.Text != "")) && (this.passwordTextBox.Text != ""))
            {
                ConfigurationSupport.currentHost = this.ServerHostCombinationBox.Text;
                ConfigurationSupport.currentPort = this.ServerPortTextBox.Text;
                ConfigurationSupport.currentUsername = this.userNameTextBox.Text;
                ConfigurationSupport.currentPassword = this.passwordTextBox.Text;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
            else
            {
                MessageBox.Show("All fields are required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

