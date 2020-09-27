using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class SingleInputForm : Form
    {
        public string ResultString => tbInput.Text;
        public SingleInputForm(string title, string val, string buttonText)
        {
            InitializeComponent();
            lblTitle.Text = title;
            tbInput.Text = val;
            btnOk.Text = buttonText;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbInput.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender, e);
            }
        }

        private void SingleInputForm_Load(object sender, EventArgs e)
        {
            tbInput.Select();
        }
    }
}
