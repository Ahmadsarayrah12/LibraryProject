using System;
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
            _ApplyPermissionStates();
            _UpdateStatusBar();
        }

        private void _UpdateStatusBar()
        {
            if (clsGlobal.CurrentUser != null)
            {
                lblUser.Text = "Current User: " + clsGlobal.CurrentUser.FullName + " | Date: " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        private void _ApplyPermissionStates()
        {
            if (clsGlobal.CurrentUser == null) return;

            bool canManageBooks = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks);
            tsmiManageBooks.Enabled = canManageBooks;
            tsmiAddNewBook.Enabled = canManageBooks;
            tsmiManageAuthors.Enabled = canManageBooks;

            bool canManageMembers = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageMembers);
            tsmiManageMembers.Enabled = canManageMembers;
            tsmiAddNewMember.Enabled = canManageMembers;

            bool canManageBorrowing = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing);
            tsmiManageBorrowings.Enabled = canManageBorrowing;
            tsmiBorrowBook.Enabled = canManageBorrowing;
            tsmiReturnBook.Enabled = canManageBorrowing;

            bool canManageFines = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pFines);
            tsmiManageFines.Enabled = canManageFines;

            bool canManageUsers = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageUsers);
            tsmiManageUsers.Enabled = canManageUsers;
        }

        private void tsmiManageBooks_Click(object sender, EventArgs e)
        {
            frmBooksList frm = new frmBooksList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiAddNewBook_Click(object sender, EventArgs e)
        {
            frmAddUpdateBook frm = new frmAddUpdateBook();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiManageAuthors_Click(object sender, EventArgs e)
        {
            frmManageAuthors frm = new frmManageAuthors();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiManageMembers_Click(object sender, EventArgs e)
        {
            frmMembersList frm = new frmMembersList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiAddNewMember_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frm = new frmAddUpdateMember();
            frm.MdiParent = this;
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiManageBorrowings_Click(object sender, EventArgs e)
        {
            frmManageBorrowings frm = new frmManageBorrowings();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiBorrowBook_Click(object sender, EventArgs e)
        {
            frmBorrowBook frm = new frmBorrowBook();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiReturnBook_Click(object sender, EventArgs e)
        {
            frmReturnBook frm = new frmReturnBook();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiManageFines_Click(object sender, EventArgs e)
        {
            Fines.frmManageFines frm = new Fines.frmManageFines();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiManageUsers_Click(object sender, EventArgs e)
        {
            frmUsers frm = new frmUsers();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser != null)
            {
                frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
                frm.MdiParent = this;
            frm.Show();
            }
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser != null)
            {
                frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
                frm.MdiParent = this;
            frm.Show();
            }
        }

        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            this.Close();
        }
    }
}
