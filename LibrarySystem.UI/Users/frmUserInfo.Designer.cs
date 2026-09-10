namespace LibrarySystem.UI
{
    partial class frmUserInfo
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbPersonInfo = new System.Windows.Forms.GroupBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPersonID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbUserInfo = new System.Windows.Forms.GroupBox();
            this.lblPermissions = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbPersonInfo.SuspendLayout();
            this.gbUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(640, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "User Details";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbPersonInfo
            // 
            this.gbPersonInfo.Controls.Add(this.lblEmail);
            this.gbPersonInfo.Controls.Add(this.label7);
            this.gbPersonInfo.Controls.Add(this.lblPhone);
            this.gbPersonInfo.Controls.Add(this.label5);
            this.gbPersonInfo.Controls.Add(this.lblFullName);
            this.gbPersonInfo.Controls.Add(this.label3);
            this.gbPersonInfo.Controls.Add(this.lblPersonID);
            this.gbPersonInfo.Controls.Add(this.label1);
            this.gbPersonInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPersonInfo.Location = new System.Drawing.Point(17, 60);
            this.gbPersonInfo.Name = "gbPersonInfo";
            this.gbPersonInfo.Size = new System.Drawing.Size(635, 125);
            this.gbPersonInfo.TabIndex = 1;
            this.gbPersonInfo.TabStop = false;
            this.gbPersonInfo.Text = "Personal Information";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(395, 78);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 14);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "Email:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(115, 78);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(35, 14);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phone:";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblFullName.Location = new System.Drawing.Point(395, 35);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(41, 14);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Full Name:";
            // 
            // lblPersonID
            // 
            this.lblPersonID.AutoSize = true;
            this.lblPersonID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonID.Location = new System.Drawing.Point(115, 35);
            this.lblPersonID.Name = "lblPersonID";
            this.lblPersonID.Size = new System.Drawing.Size(35, 14);
            this.lblPersonID.TabIndex = 1;
            this.lblPersonID.Text = "[???]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Person ID:";
            // 
            // gbUserInfo
            // 
            this.gbUserInfo.Controls.Add(this.lblPermissions);
            this.gbUserInfo.Controls.Add(this.label11);
            this.gbUserInfo.Controls.Add(this.lblIsActive);
            this.gbUserInfo.Controls.Add(this.label9);
            this.gbUserInfo.Controls.Add(this.lblUsername);
            this.gbUserInfo.Controls.Add(this.label6);
            this.gbUserInfo.Controls.Add(this.lblUserID);
            this.gbUserInfo.Controls.Add(this.label4);
            this.gbUserInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbUserInfo.Location = new System.Drawing.Point(17, 200);
            this.gbUserInfo.Name = "gbUserInfo";
            this.gbUserInfo.Size = new System.Drawing.Size(635, 150);
            this.gbUserInfo.TabIndex = 2;
            this.gbUserInfo.TabStop = false;
            this.gbUserInfo.Text = "User Account Information";
            // 
            // lblPermissions
            // 
            this.lblPermissions.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermissions.Location = new System.Drawing.Point(115, 78);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new System.Drawing.Size(500, 55);
            this.lblPermissions.TabIndex = 7;
            this.lblPermissions.Text = "[???]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 14);
            this.label11.TabIndex = 6;
            this.label11.Text = "Permissions:";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsActive.Location = new System.Drawing.Point(395, 35);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(41, 14);
            this.lblIsActive.TabIndex = 5;
            this.lblIsActive.Text = "[???]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "Is Active:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblUsername.Location = new System.Drawing.Point(195, 35);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(41, 14);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "[???]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(120, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "Username:";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(75, 35);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(35, 14);
            this.lblUserID.TabIndex = 1;
            this.lblUserID.Text = "[???]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "User ID:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(542, 365);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(669, 412);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbUserInfo);
            this.Controls.Add(this.gbPersonInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "frmUserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Information";
            this.Load += new System.EventHandler(this.frmUserInfo_Load);
            this.gbPersonInfo.ResumeLayout(false);
            this.gbPersonInfo.PerformLayout();
            this.gbUserInfo.ResumeLayout(false);
            this.gbUserInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbPersonInfo;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPersonID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbUserInfo;
        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
    }
}
