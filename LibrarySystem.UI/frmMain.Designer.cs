namespace LibrarySystem.UI
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddNewBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageAuthors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddNewMember = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCirculation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageBorrowings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBorrowBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReturnBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManageUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMainMenu
            // 
            this.msMainMenu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBooks,
            this.tsmiMembers,
            this.tsmiCirculation,
            this.tsmiUsers});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(984, 25);
            this.msMainMenu.TabIndex = 0;
            this.msMainMenu.Text = "menuStrip1";
            // 
            // tsmiBooks
            // 
            this.tsmiBooks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiManageBooks,
            this.tsmiAddNewBook,
            this.tsmiManageAuthors});
            this.tsmiBooks.Name = "tsmiBooks";
            this.tsmiBooks.Size = new System.Drawing.Size(56, 21);
            this.tsmiBooks.Text = "Books";
            // 
            // tsmiManageBooks
            // 
            this.tsmiManageBooks.Name = "tsmiManageBooks";
            this.tsmiManageBooks.Size = new System.Drawing.Size(180, 22);
            this.tsmiManageBooks.Text = "Manage Books";
            this.tsmiManageBooks.Click += new System.EventHandler(this.tsmiManageBooks_Click);
            // 
            // tsmiAddNewBook
            // 
            this.tsmiAddNewBook.Name = "tsmiAddNewBook";
            this.tsmiAddNewBook.Size = new System.Drawing.Size(180, 22);
            this.tsmiAddNewBook.Text = "Add New Book";
            this.tsmiAddNewBook.Click += new System.EventHandler(this.tsmiAddNewBook_Click);
            // 
            // tsmiManageAuthors
            // 
            this.tsmiManageAuthors.Name = "tsmiManageAuthors";
            this.tsmiManageAuthors.Size = new System.Drawing.Size(180, 22);
            this.tsmiManageAuthors.Text = "Manage Authors";
            this.tsmiManageAuthors.Click += new System.EventHandler(this.tsmiManageAuthors_Click);
            // 
            // tsmiMembers
            // 
            this.tsmiMembers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiManageMembers,
            this.tsmiAddNewMember});
            this.tsmiMembers.Name = "tsmiMembers";
            this.tsmiMembers.Size = new System.Drawing.Size(77, 21);
            this.tsmiMembers.Text = "Members";
            // 
            // tsmiManageMembers
            // 
            this.tsmiManageMembers.Name = "tsmiManageMembers";
            this.tsmiManageMembers.Size = new System.Drawing.Size(187, 22);
            this.tsmiManageMembers.Text = "Manage Members";
            this.tsmiManageMembers.Click += new System.EventHandler(this.tsmiManageMembers_Click);
            // 
            // tsmiAddNewMember
            // 
            this.tsmiAddNewMember.Name = "tsmiAddNewMember";
            this.tsmiAddNewMember.Size = new System.Drawing.Size(187, 22);
            this.tsmiAddNewMember.Text = "Add New Member";
            this.tsmiAddNewMember.Click += new System.EventHandler(this.tsmiAddNewMember_Click);
            // 
            // tsmiCirculation
            // 
            this.tsmiCirculation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiManageBorrowings,
            this.tsmiBorrowBook,
            this.tsmiReturnBook});
            this.tsmiCirculation.Name = "tsmiCirculation";
            this.tsmiCirculation.Size = new System.Drawing.Size(84, 21);
            this.tsmiCirculation.Text = "Circulation";
            // 
            // tsmiManageBorrowings
            // 
            this.tsmiManageBorrowings.Name = "tsmiManageBorrowings";
            this.tsmiManageBorrowings.Size = new System.Drawing.Size(200, 22);
            this.tsmiManageBorrowings.Text = "Manage Borrowings";
            this.tsmiManageBorrowings.Click += new System.EventHandler(this.tsmiManageBorrowings_Click);
            // 
            // tsmiBorrowBook
            // 
            this.tsmiBorrowBook.Name = "tsmiBorrowBook";
            this.tsmiBorrowBook.Size = new System.Drawing.Size(200, 22);
            this.tsmiBorrowBook.Text = "Issue Book Loan";
            this.tsmiBorrowBook.Click += new System.EventHandler(this.tsmiBorrowBook_Click);
            // 
            // tsmiReturnBook
            // 
            this.tsmiReturnBook.Name = "tsmiReturnBook";
            this.tsmiReturnBook.Size = new System.Drawing.Size(200, 22);
            this.tsmiReturnBook.Text = "Return Book";
            this.tsmiReturnBook.Click += new System.EventHandler(this.tsmiReturnBook_Click);
            // 
            // tsmiUsers
            // 
            this.tsmiUsers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiManageUsers,
            this.tsmiCurrentUserInfo,
            this.tsmiChangePassword,
            this.toolStripSeparator1,
            this.tsmiSignOut});
            this.tsmiUsers.Name = "tsmiUsers";
            this.tsmiUsers.Size = new System.Drawing.Size(55, 21);
            this.tsmiUsers.Text = "Users";
            // 
            // tsmiManageUsers
            // 
            this.tsmiManageUsers.Name = "tsmiManageUsers";
            this.tsmiManageUsers.Size = new System.Drawing.Size(184, 22);
            this.tsmiManageUsers.Text = "Manage Users";
            this.tsmiManageUsers.Click += new System.EventHandler(this.tsmiUser_Click);
            // 
            // tsmiCurrentUserInfo
            // 
            this.tsmiCurrentUserInfo.Name = "tsmiCurrentUserInfo";
            this.tsmiCurrentUserInfo.Size = new System.Drawing.Size(184, 22);
            this.tsmiCurrentUserInfo.Text = "Current User Info";
            this.tsmiCurrentUserInfo.Click += new System.EventHandler(this.tsmiCurrentUserInfo_Click);
            // 
            // tsmiChangePassword
            // 
            this.tsmiChangePassword.Name = "tsmiChangePassword";
            this.tsmiChangePassword.Size = new System.Drawing.Size(184, 22);
            this.tsmiChangePassword.Text = "Change Password";
            this.tsmiChangePassword.Click += new System.EventHandler(this.tsmiChangePassword_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // tsmiSignOut
            // 
            this.tsmiSignOut.Name = "tsmiSignOut";
            this.tsmiSignOut.Size = new System.Drawing.Size(184, 22);
            this.tsmiSignOut.Text = "Sign Out";
            this.tsmiSignOut.Click += new System.EventHandler(this.tsmiSignOut_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(73, 17);
            this.lblUser.Text = "Current User";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.msMainMenu);
            this.IsMdiContainer = false;
            this.MainMenuStrip = this.msMainMenu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Library Management System - Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiBooks;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageBooks;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewBook;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageAuthors;
        private System.Windows.Forms.ToolStripMenuItem tsmiMembers;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageMembers;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewMember;
        private System.Windows.Forms.ToolStripMenuItem tsmiCirculation;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageBorrowings;
        private System.Windows.Forms.ToolStripMenuItem tsmiBorrowBook;
        private System.Windows.Forms.ToolStripMenuItem tsmiReturnBook;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiManageUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSignOut;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        
        // Dashboard controls
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDashboardSub;
        private System.Windows.Forms.FlowLayoutPanel flpQuickActions;
        private System.Windows.Forms.Button btnQuickBorrow;
        private System.Windows.Forms.Button btnQuickReturn;
        private System.Windows.Forms.Button btnQuickAddBook;
        private System.Windows.Forms.Button btnQuickAddMember;
    }
}