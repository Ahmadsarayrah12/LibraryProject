namespace LibrarySystem.UI
{
    partial class frmAddUpdateMember
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
            
            // Person Info GroupBox and Controls
            this.gbPersonInfo = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            
            this.gbMembershipInfo = new System.Windows.Forms.GroupBox();
            this.dtpSubscriptionDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            
            this.gbPersonInfo.SuspendLayout();
            this.gbMembershipInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(760, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Member";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // gbPersonInfo
            this.gbPersonInfo.Controls.Add(this.txtEmail);
            this.gbPersonInfo.Controls.Add(this.txtPhone);
            this.gbPersonInfo.Controls.Add(this.txtLastName);
            this.gbPersonInfo.Controls.Add(this.txtFirstName);
            this.gbPersonInfo.Controls.Add(this.label7);
            this.gbPersonInfo.Controls.Add(this.label6);
            this.gbPersonInfo.Controls.Add(this.label5);
            this.gbPersonInfo.Controls.Add(this.label4);
            this.gbPersonInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPersonInfo.Location = new System.Drawing.Point(15, 60);
            this.gbPersonInfo.Name = "gbPersonInfo";
            this.gbPersonInfo.Size = new System.Drawing.Size(760, 120);
            this.gbPersonInfo.TabIndex = 1;
            this.gbPersonInfo.TabStop = false;
            this.gbPersonInfo.Text = "Personal Information";
            
            // txtEmail
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(490, 75);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(240, 22);
            this.txtEmail.TabIndex = 7;
            
            // txtPhone
            this.txtPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(115, 75);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(240, 22);
            this.txtPhone.TabIndex = 5;

            
            // txtLastName
            this.txtLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(490, 30);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(240, 22);
            this.txtLastName.TabIndex = 3;

            
            // txtFirstName
            this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(115, 30);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(240, 22);
            this.txtFirstName.TabIndex = 1;

            
            // label7
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(405, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "Email:";
            
            // label6
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "Phone:";
            
            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "Last Name:";
            
            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "First Name:";
            
            // gbMembershipInfo
            this.gbMembershipInfo.Controls.Add(this.dtpSubscriptionDate);
            this.gbMembershipInfo.Controls.Add(this.label2);
            this.gbMembershipInfo.Controls.Add(this.lblMemberID);
            this.gbMembershipInfo.Controls.Add(this.label1);
            this.gbMembershipInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMembershipInfo.Location = new System.Drawing.Point(15, 200);
            this.gbMembershipInfo.Name = "gbMembershipInfo";
            this.gbMembershipInfo.Size = new System.Drawing.Size(760, 75);
            this.gbMembershipInfo.TabIndex = 2;
            this.gbMembershipInfo.TabStop = false;
            this.gbMembershipInfo.Text = "Membership Details";
            
            // dtpSubscriptionDate
            this.dtpSubscriptionDate.CustomFormat = "dd/MM/yyyy";
            this.dtpSubscriptionDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSubscriptionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSubscriptionDate.Location = new System.Drawing.Point(490, 30);
            this.dtpSubscriptionDate.Name = "dtpSubscriptionDate";
            this.dtpSubscriptionDate.Size = new System.Drawing.Size(240, 22);
            this.dtpSubscriptionDate.TabIndex = 3;
            
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Subscription Date:";
            
            // lblMemberID
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberID.Location = new System.Drawing.Point(110, 34);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(35, 14);
            this.lblMemberID.TabIndex = 1;
            this.lblMemberID.Text = "[???]";
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Member ID:";
            
            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(675, 295);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(565, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // errorProvider1
            this.errorProvider1.ContainerControl = this;
            
            // frmAddUpdateMember
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(790, 345);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbMembershipInfo);
            this.Controls.Add(this.gbPersonInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "frmAddUpdateMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Edit Member";
            this.Load += new System.EventHandler(this.frmAddUpdateMember_Load);
            
            this.gbPersonInfo.ResumeLayout(false);
            this.gbPersonInfo.PerformLayout();
            this.gbMembershipInfo.ResumeLayout(false);
            this.gbMembershipInfo.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbMembershipInfo;
        private System.Windows.Forms.DateTimePicker dtpSubscriptionDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

