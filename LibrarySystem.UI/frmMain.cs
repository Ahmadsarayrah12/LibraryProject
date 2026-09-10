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
            _BuildDashboard();
        }

        private void _BuildDashboard()
        {
            pnlDashboard = new System.Windows.Forms.Panel();
            lblWelcome = new System.Windows.Forms.Label();
            lblDashboardSub = new System.Windows.Forms.Label();
            flpQuickActions = new System.Windows.Forms.FlowLayoutPanel();
            btnQuickBorrow = new System.Windows.Forms.Button();
            btnQuickReturn = new System.Windows.Forms.Button();
            btnQuickAddBook = new System.Windows.Forms.Button();
            btnQuickAddMember = new System.Windows.Forms.Button();

            // pnlDashboard
            pnlDashboard.BackColor = System.Drawing.Color.WhiteSmoke;
            pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlDashboard.Padding = new System.Windows.Forms.Padding(40);
            pnlDashboard.Controls.Add(flpQuickActions);
            pnlDashboard.Controls.Add(lblDashboardSub);
            pnlDashboard.Controls.Add(lblWelcome);

            // lblWelcome
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            lblWelcome.ForeColor = System.Drawing.Color.MidnightBlue;
            lblWelcome.Location = new System.Drawing.Point(40, 40);
            lblWelcome.Text = "Welcome to Library Management System";

            // lblDashboardSub
            lblDashboardSub.AutoSize = true;
            lblDashboardSub.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblDashboardSub.ForeColor = System.Drawing.Color.DimGray;
            lblDashboardSub.Location = new System.Drawing.Point(45, 90);
            lblDashboardSub.Text = "Manage books, members, and circulation efficiently from your dashboard.";

            // flpQuickActions
            flpQuickActions.Location = new System.Drawing.Point(45, 140);
            flpQuickActions.Size = new System.Drawing.Size(800, 200);
            flpQuickActions.WrapContents = true;
            flpQuickActions.Controls.Add(btnQuickBorrow);
            flpQuickActions.Controls.Add(btnQuickReturn);
            flpQuickActions.Controls.Add(btnQuickAddBook);
            flpQuickActions.Controls.Add(btnQuickAddMember);

            // Button Styles
            var btnFont = new System.Drawing.Font("Segoe UI Semibold", 11F);
            var btnSize = new System.Drawing.Size(160, 100);
            var btnBackColor = System.Drawing.Color.MidnightBlue;
            var btnForeColor = System.Drawing.Color.White;
            var margin = new System.Windows.Forms.Padding(0, 0, 20, 20);

            // ToolTip
            var toolTip = new System.Windows.Forms.ToolTip();
            
            // Setup Buttons
            btnQuickBorrow.Text = "Issue Loan\r\n(Ctrl+B)";
            btnQuickBorrow.Size = btnSize;
            btnQuickBorrow.Font = btnFont;
            btnQuickBorrow.BackColor = btnBackColor;
            btnQuickBorrow.ForeColor = btnForeColor;
            btnQuickBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuickBorrow.Margin = margin;
            btnQuickBorrow.Cursor = System.Windows.Forms.Cursors.Hand;
            btnQuickBorrow.Click += tsmiBorrowBook_Click;
            toolTip.SetToolTip(btnQuickBorrow, "Issue a new book loan to a member");

            btnQuickReturn.Text = "Return Book\r\n(Ctrl+R)";
            btnQuickReturn.Size = btnSize;
            btnQuickReturn.Font = btnFont;
            btnQuickReturn.BackColor = btnBackColor;
            btnQuickReturn.ForeColor = btnForeColor;
            btnQuickReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuickReturn.Margin = margin;
            btnQuickReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            btnQuickReturn.Click += tsmiReturnBook_Click;
            toolTip.SetToolTip(btnQuickReturn, "Process a book return and calculate fines");

            btnQuickAddBook.Text = "Add Book\r\n(Ctrl+N)";
            btnQuickAddBook.Size = btnSize;
            btnQuickAddBook.Font = btnFont;
            btnQuickAddBook.BackColor = System.Drawing.Color.ForestGreen;
            btnQuickAddBook.ForeColor = btnForeColor;
            btnQuickAddBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuickAddBook.Margin = margin;
            btnQuickAddBook.Cursor = System.Windows.Forms.Cursors.Hand;
            btnQuickAddBook.Click += tsmiAddNewBook_Click;
            toolTip.SetToolTip(btnQuickAddBook, "Add a new book to the library catalog");

            btnQuickAddMember.Text = "Add Member\r\n(Ctrl+M)";
            btnQuickAddMember.Size = btnSize;
            btnQuickAddMember.Font = btnFont;
            btnQuickAddMember.BackColor = System.Drawing.Color.Teal;
            btnQuickAddMember.ForeColor = btnForeColor;
            btnQuickAddMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuickAddMember.Margin = margin;
            btnQuickAddMember.Cursor = System.Windows.Forms.Cursors.Hand;
            btnQuickAddMember.Click += tsmiAddNewMember_Click;
            toolTip.SetToolTip(btnQuickAddMember, "Register a new library member");

            this.Controls.Add(pnlDashboard);
            pnlDashboard.BringToFront();
        }

        private Timer _clockTimer;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _ApplyPermissionStates();

            // Set up real-time clock
            _clockTimer = new Timer { Interval = 1000 };
            _clockTimer.Tick += (s, ev) => _UpdateStatusBar();
            _clockTimer.Start();
            
            _UpdateStatusBar();
        }

        private void _UpdateStatusBar()
        {
            if (clsGlobal.CurrentUser != null)
                lblUser.Text = $"Current User: {clsGlobal.CurrentUser.FullName} | Date: {DateTime.Now:dd/MM/yyyy hh:mm tt}";
        }

        private void _ApplyPermissionStates()
        {
            if (clsGlobal.CurrentUser == null) return;

            bool canManageBooks = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks);
            tsmiManageBooks.Enabled = canManageBooks;
            tsmiAddNewBook.Enabled = canManageBooks;
            tsmiManageAuthors.Enabled = canManageBooks;
            btnQuickAddBook.Enabled = canManageBooks;

            bool canManageMembers = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageMembers);
            tsmiManageMembers.Enabled = canManageMembers;
            tsmiAddNewMember.Enabled = canManageMembers;
            btnQuickAddMember.Enabled = canManageMembers;

            bool canManageBorrowing = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing);
            tsmiManageBorrowings.Enabled = canManageBorrowing;
            tsmiBorrowBook.Enabled = canManageBorrowing;
            tsmiReturnBook.Enabled = canManageBorrowing;
            btnQuickBorrow.Enabled = canManageBorrowing;
            btnQuickReturn.Enabled = canManageBorrowing;

            bool canManageUsers = clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageUsers);
            tsmiManageUsers.Enabled = canManageUsers;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.B))
            {
                if (btnQuickBorrow.Enabled) tsmiBorrowBook_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.R))
            {
                if (btnQuickReturn.Enabled) tsmiReturnBook_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.N))
            {
                if (btnQuickAddBook.Enabled) tsmiAddNewBook_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.M))
            {
                if (btnQuickAddMember.Enabled) tsmiAddNewMember_Click(null, null);
                return true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
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
            if (MessageBox.Show("Are you sure you want to sign out?", "Confirm Sign Out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsGlobal.CurrentUser = null;
                this.Close();
            }
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
            if (clsGlobal.CurrentUser != null && clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
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
            if (clsGlobal.CurrentUser != null && clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
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
            if (clsGlobal.CurrentUser != null && clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
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
            if (clsGlobal.CurrentUser != null && clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
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
            if (clsGlobal.CurrentUser != null && clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
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
            if (clsGlobal.CurrentUser != null && clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
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