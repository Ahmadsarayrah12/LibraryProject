using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
              lblUser.Text = $"Current User: {clsGlobal.CurrentUser.FullName} | Date: {DateTime.Now:dd/MM/yyyy}";
        }

         

        private void tsmiUser_Click(object sender, EventArgs e)
        {


            if (clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageUsers))
            {
                frmUsers frm = new frmUsers();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Access Denied! Contact your admin.");
            }

        }

        
        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
             clsGlobal.CurrentUser = null;

             Application.Restart();
        }

         

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword();
            frm.ShowDialog();


        }
    }
}
