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
    public partial class EventCommentForm : Form
    {
        public EventCommentForm()
        {
            InitializeComponent();
        }

        private string _commentText = string.Empty;


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.CommentTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please provide a comment for the event(s) or cancel the validation.", "Comment Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                base.DialogResult = DialogResult.OK;
                this.CommentText = this.CommentTextBox.Text;
                base.Close();
            }
        }

        private void frmEventComment_Shown(object sender, EventArgs e)
        {
            this.CommentTextBox.Focus();
        }

        public string CommentText
        {
            get
            {
                return this._commentText;
            }
            private set
            {
                this._commentText = value;
            }
        }
    }
}
