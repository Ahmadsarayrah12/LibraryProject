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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

             if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("يرجى إدخال اسم المستخدم وكلمة المرور.", "حقول مطلوبة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

             clsBusinessLayerUsers user = clsBusinessLayerUsers.FindByUserNameAndPassword(username, password);

             if (user == null)
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة.", "خطأ في تسجيل الدخول", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             if (!user.IsActive)
            {
                MessageBox.Show("هذا الحساب موقوف، يرجى مراجعة المسؤول.", "حساب غير مفعل", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

             MessageBox.Show("تم تسجيل الدخول بنجاح!", "مرحباً", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}