using System;
using System.Windows.Forms;
using System.Web;

namespace PT_Sguil
{
    public partial class ToolEncoderForm : Form
    {
        public ToolEncoderForm(string inputData)
        {
            InitializeComponent();
            this.tbEncodedText.Text = inputData;
        }

        private void btnBase64Decode_Click(object sender, EventArgs e)
        {
            tbPlainText.Text = Tools.Base64Decode(tbEncodedText.Text);
        }

        private void btnBase64Encode_Click(object sender, EventArgs e)
        {
            tbEncodedText.Text = Tools.Base64Encode(tbPlainText.Text);
        }

        private void btnURLDecode_Click(object sender, EventArgs e)
        {
            tbPlainText.Text = HttpUtility.UrlDecode(tbEncodedText.Text);
        }

        private void btnURLEncode_Click(object sender, EventArgs e)
        {
            tbEncodedText.Text = HttpUtility.UrlEncode(tbPlainText.Text);
        }

        private void btnSwapText_Click(object sender, EventArgs e)
        {
            string tmpSwap = this.tbPlainText.Text;

            this.tbPlainText.Text = this.tbEncodedText.Text;
            this.tbEncodedText.Text = tmpSwap;
        }


    }
}
