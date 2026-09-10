namespace LibrarySystem.UI
{
    partial class frmAddUpdateUser
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbPersonInfo = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbLoginInfo = new System.Windows.Forms.GroupBox();
            this.gbPermissions = new System.Windows.Forms.GroupBox();
            this.pnlIndividualPermissions = new System.Windows.Forms.Panel();
            this.chkFines = new System.Windows.Forms.CheckBox();
            this.chkBorrowing = new System.Windows.Forms.CheckBox();
            this.chkManageBooks = new System.Windows.Forms.CheckBox();
            this.chkManageUsers = new System.Windows.Forms.CheckBox();
            this.chkManagePeople = new System.Windows.Forms.CheckBox();
            this.chkAllPermissions = new System.Windows.Forms.CheckBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbPersonInfo.SuspendLayout();
            this.gbLoginInfo.SuspendLayout();
            this.gbPermissions.SuspendLayout();
            this.pnlIndividualPermissions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(790, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New User";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbPersonInfo
            // 
            this.gbPersonInfo.Controls.Add(this.txtEmail);
            this.gbPersonInfo.Controls.Add(this.txtPhone);
            this.gbPersonInfo.Controls.Add(this.txtLastName);
            this.gbPersonInfo.Controls.Add(this.txtFirstName);
            this.gbPersonInfo.Controls.Add(this.label7);
            this.gbPersonInfo.Controls.Add(this.label6);
            this.gbPersonInfo.Controls.Add(this.label5);
            this.gbPersonInfo.Controls.Add(this.label4);
            this.gbPersonInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPersonInfo.Location = new System.Drawing.Point(17, 65);
            this.gbPersonInfo.Name = "gbPersonInfo";
            this.gbPersonInfo.Size = new System.Drawing.Size(785, 120);
            this.gbPersonInfo.TabIndex = 1;
            this.gbPersonInfo.TabStop = false;
            this.gbPersonInfo.Text = "Personal Information";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(490, 75);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(260, 22);
            this.txtEmail.TabIndex = 7;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(115, 75);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(260, 22);
            this.txtPhone.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(490, 30);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(260, 22);
            this.txtLastName.TabIndex = 3;
            this.txtLastName.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastName_Validating);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(115, 30);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(260, 22);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstName_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "Email:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "Phone:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "Last Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "First Name:";
            // 
            // gbLoginInfo
            // 
            this.gbLoginInfo.Controls.Add(this.gbPermissions);
            this.gbLoginInfo.Controls.Add(this.chkIsActive);
            this.gbLoginInfo.Controls.Add(this.txtConfirmPassword);
            this.gbLoginInfo.Controls.Add(this.label10);
            this.gbLoginInfo.Controls.Add(this.txtPassword);
            this.gbLoginInfo.Controls.Add(this.label9);
            this.gbLoginInfo.Controls.Add(this.txtUsername);
            this.gbLoginInfo.Controls.Add(this.label8);
            this.gbLoginInfo.Controls.Add(this.lblUserID);
            this.gbLoginInfo.Controls.Add(this.label3);
            this.gbLoginInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLoginInfo.Location = new System.Drawing.Point(17, 200);
            this.gbLoginInfo.Name = "gbLoginInfo";
            this.gbLoginInfo.Size = new System.Drawing.Size(785, 305);
            this.gbLoginInfo.TabIndex = 2;
            this.gbLoginInfo.TabStop = false;
            this.gbLoginInfo.Text = "Login Credentials & Permissions";
            // 
            // gbPermissions
            // 
            this.gbPermissions.Controls.Add(this.pnlIndividualPermissions);
            this.gbPermissions.Controls.Add(this.chkAllPermissions);
            this.gbPermissions.Location = new System.Drawing.Point(440, 25);
            this.gbPermissions.Name = "gbPermissions";
            this.gbPermissions.Size = new System.Drawing.Size(325, 265);
            this.gbPermissions.TabIndex = 9;
            this.gbPermissions.TabStop = false;
            this.gbPermissions.Text = "Permissions";
            // 
            // pnlIndividualPermissions
            // 
            this.pnlIndividualPermissions.Controls.Add(this.chkFines);
            this.pnlIndividualPermissions.Controls.Add(this.chkBorrowing);
            this.pnlIndividualPermissions.Controls.Add(this.chkManageBooks);
            this.pnlIndividualPermissions.Controls.Add(this.chkManageUsers);
            this.pnlIndividualPermissions.Controls.Add(this.chkManagePeople);
            this.pnlIndividualPermissions.Enabled = false;
            this.pnlIndividualPermissions.Location = new System.Drawing.Point(20, 60);
            this.pnlIndividualPermissions.Name = "pnlIndividualPermissions";
            this.pnlIndividualPermissions.Size = new System.Drawing.Size(285, 190);
            this.pnlIndividualPermissions.TabIndex = 1;
            // 
            // chkFines
            // 
            this.chkFines.AutoSize = true;
            this.chkFines.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFines.Location = new System.Drawing.Point(15, 150);
            this.chkFines.Name = "chkFines";
            this.chkFines.Size = new System.Drawing.Size(53, 18);
            this.chkFines.TabIndex = 4;
            this.chkFines.Text = "Fines";
            this.chkFines.UseVisualStyleBackColor = true;
            // 
            // chkBorrowing
            // 
            this.chkBorrowing.AutoSize = true;
            this.chkBorrowing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBorrowing.Location = new System.Drawing.Point(15, 115);
            this.chkBorrowing.Name = "chkBorrowing";
            this.chkBorrowing.Size = new System.Drawing.Size(126, 18);
            this.chkBorrowing.TabIndex = 3;
            this.chkBorrowing.Text = "Borrowing Records";
            this.chkBorrowing.UseVisualStyleBackColor = true;
            // 
            // chkManageBooks
            // 
            this.chkManageBooks.AutoSize = true;
            this.chkManageBooks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManageBooks.Location = new System.Drawing.Point(15, 80);
            this.chkManageBooks.Name = "chkManageBooks";
            this.chkManageBooks.Size = new System.Drawing.Size(104, 18);
            this.chkManageBooks.TabIndex = 2;
            this.chkManageBooks.Text = "Manage Books";
            this.chkManageBooks.UseVisualStyleBackColor = true;
            // 
            // chkManageUsers
            // 
            this.chkManageUsers.AutoSize = true;
            this.chkManageUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManageUsers.Location = new System.Drawing.Point(15, 45);
            this.chkManageUsers.Name = "chkManageUsers";
            this.chkManageUsers.Size = new System.Drawing.Size(101, 18);
            this.chkManageUsers.TabIndex = 1;
            this.chkManageUsers.Text = "Manage Users";
            this.chkManageUsers.UseVisualStyleBackColor = true;
            // 
            // chkManagePeople
            // 
            this.chkManagePeople.AutoSize = true;
            this.chkManagePeople.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManagePeople.Location = new System.Drawing.Point(15, 10);
            this.chkManagePeople.Name = "chkManagePeople";
            this.chkManagePeople.Size = new System.Drawing.Size(108, 18);
            this.chkManagePeople.TabIndex = 0;
            this.chkManagePeople.Text = "Manage People";
            this.chkManagePeople.UseVisualStyleBackColor = true;
            // 
            // chkAllPermissions
            // 
            this.chkAllPermissions.AutoSize = true;
            this.chkAllPermissions.Checked = true;
            this.chkAllPermissions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllPermissions.Location = new System.Drawing.Point(20, 30);
            this.chkAllPermissions.Name = "chkAllPermissions";
            this.chkAllPermissions.Size = new System.Drawing.Size(147, 18);
            this.chkAllPermissions.TabIndex = 0;
            this.chkAllPermissions.Text = "All Permissions (-1)";
            this.chkAllPermissions.UseVisualStyleBackColor = true;
            this.chkAllPermissions.CheckedChanged += new System.EventHandler(this.chkAllPermissions_CheckedChanged);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Location = new System.Drawing.Point(160, 245);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(79, 18);
            this.chkIsActive.TabIndex = 8;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(160, 190);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(215, 22);
            this.txtConfirmPassword.TabIndex = 7;
            this.txtConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtConfirmPassword_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 14);
            this.label10.TabIndex = 6;
            this.label10.Text = "Confirm Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(160, 140);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(215, 22);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(160, 90);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(215, 22);
            this.txtUsername.TabIndex = 3;
            this.txtUsername.Validating += new System.ComponentModel.CancelEventHandler(this.txtUsername_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 14);
            this.label8.TabIndex = 2;
            this.label8.Text = "Username:";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblUserID.Location = new System.Drawing.Point(160, 45);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(38, 14);
            this.lblUserID.TabIndex = 1;
            this.lblUserID.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "User ID:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(692, 518);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(570, 518);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddUpdateUser
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(819, 565);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbLoginInfo);
            this.Controls.Add(this.gbPersonInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "frmAddUpdateUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Update User";
            this.Load += new System.EventHandler(this.frmAddUpdateUser_Load);
            this.gbPersonInfo.ResumeLayout(false);
            this.gbPersonInfo.PerformLayout();
            this.gbLoginInfo.ResumeLayout(false);
            this.gbLoginInfo.PerformLayout();
            this.gbPermissions.ResumeLayout(false);
            this.gbPermissions.PerformLayout();
            this.pnlIndividualPermissions.ResumeLayout(false);
            this.pnlIndividualPermissions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbPersonInfo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbLoginInfo;
        private System.Windows.Forms.GroupBox gbPermissions;
        private System.Windows.Forms.Panel pnlIndividualPermissions;
        private System.Windows.Forms.CheckBox chkFines;
        private System.Windows.Forms.CheckBox chkBorrowing;
        private System.Windows.Forms.CheckBox chkManageBooks;
        private System.Windows.Forms.CheckBox chkManageUsers;
        private System.Windows.Forms.CheckBox chkManagePeople;
        private System.Windows.Forms.CheckBox chkAllPermissions;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
