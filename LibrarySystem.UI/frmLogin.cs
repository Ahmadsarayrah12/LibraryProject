using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = "";
            string password = "";

            if (clsGlobal.GetStoredCredential(ref username, ref password))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // تحقق بسيط جداً للمبتدئين
            if (txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // البحث عن المستخدم
            clsUser user = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text);

            if (user != null)
            {
                if (!user.IsActive)
                {
                    MessageBox.Show("Your account is not active, Contact Admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // حفظ المستخدم في الذاكرة
                clsGlobal.CurrentUser = user;

                // التذكر
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text);
                }
                else
                {
                    clsGlobal.ClearStoredCredentials();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}