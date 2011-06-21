namespace PT_Sguil
{
    partial class AuthenticationPromptForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ServerHostCombinationBox = new System.Windows.Forms.ComboBox();
            this.ServerPortTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sguid Host:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sguild Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(55, 129);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(136, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ServerHostCombinationBox
            // 
            this.ServerHostCombinationBox.FormattingEnabled = true;
            this.ServerHostCombinationBox.Location = new System.Drawing.Point(117, 13);
            this.ServerHostCombinationBox.Name = "ServerHostCombinationBox";
            this.ServerHostCombinationBox.Size = new System.Drawing.Size(120, 21);
            this.ServerHostCombinationBox.TabIndex = 6;
            // 
            // ServerPortTextBox
            // 
            this.ServerPortTextBox.Location = new System.Drawing.Point(117, 39);
            this.ServerPortTextBox.Name = "ServerPortTextBox";
            this.ServerPortTextBox.Size = new System.Drawing.Size(120, 20);
            this.ServerPortTextBox.TabIndex = 7;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(117, 64);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(120, 20);
            this.userNameTextBox.TabIndex = 8;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(117, 90);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(120, 20);
            this.passwordTextBox.TabIndex = 9;
            // 
            // AuthenticationPromptForm
            // 
            this.AcceptButton = this.btnConnect;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(262, 169);
            this.ControlBox = false;
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.ServerPortTextBox);
            this.Controls.Add(this.ServerHostCombinationBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AuthenticationPromptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to Sguild";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ComboBox ServerHostCombinationBox;
        private System.Windows.Forms.TextBox ServerPortTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
    }
}
