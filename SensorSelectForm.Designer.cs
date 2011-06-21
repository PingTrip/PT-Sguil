namespace PT_Sguil
{
    partial class SensorSelectForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.clbSensorList = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnMonitor, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.clbSensorList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.7218F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.2782F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 266);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(181, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnMonitor
            // 
            this.btnMonitor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnMonitor.Location = new System.Drawing.Point(35, 238);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(75, 23);
            this.btnMonitor.TabIndex = 1;
            this.btnMonitor.Text = "Monitor";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // clbSensorList
            // 
            this.clbSensorList.CheckOnClick = true;
            this.tableLayoutPanel1.SetColumnSpan(this.clbSensorList, 2);
            this.clbSensorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbSensorList.FormattingEnabled = true;
            this.clbSensorList.Location = new System.Drawing.Point(3, 3);
            this.clbSensorList.MultiColumn = true;
            this.clbSensorList.Name = "clbSensorList";
            this.clbSensorList.Size = new System.Drawing.Size(286, 228);
            this.clbSensorList.Sorted = true;
            this.clbSensorList.TabIndex = 2;
            this.clbSensorList.ThreeDCheckBoxes = true;
            // 
            // frmSensorSelect
            // 
            this.AcceptButton = this.btnMonitor;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmSensorSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sensors to Monitor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.CheckedListBox clbSensorList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
