using System;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            cbxUsernames.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        private void AttemptLogin()
        {
            if (cbxUsernames.SelectedItem != null)
            {
                if (pbPassword.Text == Program.AppConfiguration.GetPasswordText())
                {
                    AllowLogin();
                }
                else
                {
                    lblPasswordValidation.Visible = true;
                }
            }
        }

        private void AllowLogin()
        {
            Hide();
            new Main().ShowDialog();
            Close();
        }

        private void pbPassword_TextChanged(object sender, EventArgs e)
        {
            lblPasswordValidation.Visible = false;
        }

        private void pbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AttemptLogin();
                //AllowLogin();
            }
        }
    }
}