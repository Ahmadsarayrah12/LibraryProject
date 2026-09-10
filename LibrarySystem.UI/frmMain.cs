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
                frmUsers frm = new frmUsers();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to manage users. Contact your admin.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            // Opens the user info dialog passing the active session's UserID
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword();
            frm.ShowDialog();
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
            // Permission check: ensure user has rights to view and manage members
            if (clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageMembers))
            {
                frmMembersList frm = new frmMembersList();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to access members management.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary>
        /// Direct shortcut to enroll a new library member.
        /// </summary>
        private void tsmiAddNewMember_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageMembers))
            {
                frmAddUpdateMember frm = new frmAddUpdateMember();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Access Denied! You do not have permission to add new members.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #endregion
    }
}