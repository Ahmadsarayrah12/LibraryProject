using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Main application dashboard providing centralized navigation, session status,
    /// and permission-controlled access to system modules.
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the frmMain dashboard form.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = $"Current User: {clsGlobal.CurrentUser.FullName} | Date: {DateTime.Now:dd/MM/yyyy}";
        }

        #region Users Management

        private void tsmiUser_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageUsers))
            {
                using (frmUsers frm = new frmUsers())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to manage users. Contact your admin.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            using (frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID))
            {
                frm.ShowDialog();
            }
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            using (frmChangePassword frm = new frmChangePassword())
            {
                frm.ShowDialog();
            }
        }

        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            Application.Restart();
        }

        #endregion

        #region Members Management

        /// <summary>
        /// Handles opening the Members List / Management screen with permission verification.
        /// </summary>
        private void tsmiManageMembers_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageMembers))
            {
                using (frmMembersList frm = new frmMembersList())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to access members management.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// Direct shortcut to enroll a new library member with permission verification.
        /// </summary>
        private void tsmiAddNewMember_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageMembers))
            {
                using (frmAddUpdateMember frm = new frmAddUpdateMember())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to add new members.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #endregion

        #region Books Management

        /// <summary>
        /// Handles opening the Books List / Management screen with permission verification.
        /// </summary>
        private void tsmiManageBooks_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
            {
                using (frmBooksList frm = new frmBooksList())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to manage books.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// Direct shortcut to create a new book catalog entry with permission verification.
        /// </summary>
        private void tsmiAddNewBook_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
            {
                using (frmAddUpdateBook frm = new frmAddUpdateBook())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to add books.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// Handles opening the Authors management screen with permission verification.
        /// </summary>
        private void tsmiManageAuthors_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
            {
                using (frmManageAuthors frm = new frmManageAuthors())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to manage authors.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #endregion

        #region Circulation Management

        private void tsmiBorrowBook_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
            {
                using (frmBorrowBook frm = new frmBorrowBook())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to issue book loans.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tsmiManageBorrowings_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
            {
                using (frmManageBorrowings frm = new frmManageBorrowings())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to manage borrowings.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tsmiReturnBook_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
            {
                using (frmReturnBook frm = new frmReturnBook())
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to return books.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #endregion
    }
}