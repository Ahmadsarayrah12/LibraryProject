using System;
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
        //Login Proccess
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

             if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
             {
                MessageBox.Show("Please Enter Username and password", "Required fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
             }

             clsUser user = clsUser.FindByUserNameAndPassword(username, password);

             if (user == null)
             {
                MessageBox.Show("Incorrect username or password.", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
             }

             if (!user.IsActive)
             {
                MessageBox.Show("This account is suspended, please contact the administrator.", "account is suspended", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
             }
            clsGlobal.CurrentUser = user;
            DialogResult = DialogResult.OK;
            this.Close();
 
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}