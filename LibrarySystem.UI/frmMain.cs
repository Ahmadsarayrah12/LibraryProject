using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

           
          if ( clsGlobal.CurrentUser.CheckAccessPermission(Business.clsUser.enPermissions.pManageUsers))
          {
              
            frmUsers users = new frmUsers();
            users.ShowDialog();
          }
            else
            {

                MessageBox.Show("You Dont have Permission To Enter User screen", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
             }

        }


    }
}
