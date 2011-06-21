namespace PT_Sguil
{
    partial class ToolEncoderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolEncoderForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnURLEncode = new System.Windows.Forms.Button();
            this.btnBase64Encode = new System.Windows.Forms.Button();
            this.tbPlainText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnURLDecode = new System.Windows.Forms.Button();
            this.btnBase64Decode = new System.Windows.Forms.Button();
            this.tbEncodedText = new System.Windows.Forms.TextBox();
            this.btnSwapText = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnURLEncode);
            this.groupBox1.Controls.Add(this.btnBase64Encode);
            this.groupBox1.Controls.Add(this.tbPlainText);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plain Text";
            // 
            // btnURLEncode
            // 
            this.btnURLEncode.Location = new System.Drawing.Point(212, 50);
            this.btnURLEncode.Name = "btnURLEncode";
            this.btnURLEncode.Size = new System.Drawing.Size(93, 23);
            this.btnURLEncode.TabIndex = 2;
            this.btnURLEncode.Text = "URL Encode";
            this.btnURLEncode.UseVisualStyleBackColor = true;
            this.btnURLEncode.Click += new System.EventHandler(this.btnURLEncode_Click);
            // 
            // btnBase64Encode
            // 
            this.btnBase64Encode.Location = new System.Drawing.Point(212, 20);
            this.btnBase64Encode.Name = "btnBase64Encode";
            this.btnBase64Encode.Size = new System.Drawing.Size(93, 23);
            this.btnBase64Encode.TabIndex = 1;
            this.btnBase64Encode.Text = "Base64 Encode";
            this.btnBase64Encode.UseVisualStyleBackColor = true;
            this.btnBase64Encode.Click += new System.EventHandler(this.btnBase64Encode_Click);
            // 
            // tbPlainText
            // 
            this.tbPlainText.Location = new System.Drawing.Point(7, 20);
            this.tbPlainText.Multiline = true;
            this.tbPlainText.Name = "tbPlainText";
            this.tbPlainText.Size = new System.Drawing.Size(198, 74);
            this.tbPlainText.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnURLDecode);
            this.groupBox2.Controls.Add(this.btnBase64Decode);
            this.groupBox2.Controls.Add(this.tbEncodedText);
            this.groupBox2.Location = new System.Drawing.Point(13, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Encoded Text";
            // 
            // btnURLDecode
            // 
            this.btnURLDecode.Location = new System.Drawing.Point(212, 50);
            this.btnURLDecode.Name = "btnURLDecode";
            this.btnURLDecode.Size = new System.Drawing.Size(93, 23);
            this.btnURLDecode.TabIndex = 2;
            this.btnURLDecode.Text = "URL Decode";
            this.btnURLDecode.UseVisualStyleBackColor = true;
            this.btnURLDecode.Click += new System.EventHandler(this.btnURLDecode_Click);
            // 
            // btnBase64Decode
            // 
            this.btnBase64Decode.Location = new System.Drawing.Point(212, 20);
            this.btnBase64Decode.Name = "btnBase64Decode";
            this.btnBase64Decode.Size = new System.Drawing.Size(93, 23);
            this.btnBase64Decode.TabIndex = 1;
            this.btnBase64Decode.Text = "Base64 Decode";
            this.btnBase64Decode.UseVisualStyleBackColor = true;
            this.btnBase64Decode.Click += new System.EventHandler(this.btnBase64Decode_Click);
            // 
            // tbEncodedText
            // 
            this.tbEncodedText.Location = new System.Drawing.Point(7, 20);
            this.tbEncodedText.Multiline = true;
            this.tbEncodedText.Name = "tbEncodedText";
            this.tbEncodedText.Size = new System.Drawing.Size(198, 74);
            this.tbEncodedText.TabIndex = 0;
            // 
            // btnSwapText
            // 
            this.btnSwapText.Image = ((System.Drawing.Image)(resources.GetObject("btnSwapText.Image")));
            this.btnSwapText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSwapText.Location = new System.Drawing.Point(94, 117);
            this.btnSwapText.Name = "btnSwapText";
            this.btnSwapText.Size = new System.Drawing.Size(59, 23);
            this.btnSwapText.TabIndex = 2;
            this.btnSwapText.Text = "Swap";
            this.btnSwapText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSwapText.UseVisualStyleBackColor = true;
            this.btnSwapText.Click += new System.EventHandler(this.btnSwapText_Click);
            // 
            // frmToolEncoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 259);
            this.Controls.Add(this.btnSwapText);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmToolEncoder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Encoding Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPlainText;
        private System.Windows.Forms.Button btnBase64Encode;
        private System.Windows.Forms.Button btnBase64Decode;
        private System.Windows.Forms.TextBox tbEncodedText;
        private System.Windows.Forms.Button btnURLEncode;
        private System.Windows.Forms.Button btnURLDecode;
        private System.Windows.Forms.Button btnSwapText;
    }
}