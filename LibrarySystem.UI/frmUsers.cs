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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private DataTable _dtAllUsers;


        

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;

            lblRecordsCount.Text =  "Count Of Records: "+  dgvUsers.Rows.Count.ToString()  ;

            // تنسيق الأعمدة واختيار العرض المناسب بعد تحميل البيانات
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 90;

                dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
                dgvUsers.Columns["PersonID"].Width = 90;

                dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                dgvUsers.Columns["FullName"].Width = 180;

                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Username"].Width = 120;

                dgvUsers.Columns["Permissions"].HeaderText = "Permissions";
                dgvUsers.Columns["Permissions"].Width = 100;

                dgvUsers.Columns["isActive"].HeaderText = "Is Active";
                dgvUsers.Columns["isActive"].Width = 90;

                dgvUsers.Columns["Phone"].HeaderText = "Phone";
                dgvUsers.Columns["Phone"].Width = 120;

                dgvUsers.Columns["Email"].HeaderText = "Email";
                dgvUsers.Columns["Email"].Width = 160;
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
