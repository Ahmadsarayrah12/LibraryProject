namespace LibrarySystem.UI
{
    partial class frmBorrowBook
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
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.tcBorrow = new System.Windows.Forms.TabControl();
            this.tpMember = new System.Windows.Forms.TabPage();
            this.btnNextToBook = new System.Windows.Forms.Button();
            this.gbMemberStatus = new System.Windows.Forms.GroupBox();
            this.lblEligibility = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblActiveLoans = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSubscriptionDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlPersonCardWithFilter1 = new LibrarySystem.UI.ctrlPersonCardWithFilter();
            this.tpBookAndLoan = new System.Windows.Forms.TabPage();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.btnBackToMember = new System.Windows.Forms.Button();
            this.gbLoanDetails = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.lblBorrowDate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbAvailableCopies = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ctrlBookCardWithFilter1 = new LibrarySystem.UI.ctrlBookCardWithFilter();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tcBorrow.SuspendLayout();
            this.tpMember.SuspendLayout();
            this.gbMemberStatus.SuspendLayout();
            this.tpBookAndLoan.SuspendLayout();
            this.gbLoanDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblHeaderTitle.Location = new System.Drawing.Point(12, 9);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(700, 38);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Issue Book Loan / تسجيل إعارة كتاب";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tcBorrow
            // 
            this.tcBorrow.Controls.Add(this.tpMember);
            this.tcBorrow.Controls.Add(this.tpBookAndLoan);
            this.tcBorrow.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcBorrow.Location = new System.Drawing.Point(12, 50);
            this.tcBorrow.Name = "tcBorrow";
            this.tcBorrow.SelectedIndex = 0;
            this.tcBorrow.Size = new System.Drawing.Size(700, 535);
            this.tcBorrow.TabIndex = 1;
            // 
            // tpMember
            // 
            this.tpMember.Controls.Add(this.btnNextToBook);
            this.tpMember.Controls.Add(this.gbMemberStatus);
            this.tpMember.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tpMember.Location = new System.Drawing.Point(4, 26);
            this.tpMember.Name = "tpMember";
            this.tpMember.Padding = new System.Windows.Forms.Padding(3);
            this.tpMember.Size = new System.Drawing.Size(692, 505);
            this.tpMember.TabIndex = 0;
            this.tpMember.Text = "1. Member Info (بيانات العضو)";
            this.tpMember.UseVisualStyleBackColor = true;
            // 
            // btnNextToBook
            // 
            this.btnNextToBook.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnNextToBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextToBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextToBook.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextToBook.ForeColor = System.Drawing.Color.White;
            this.btnNextToBook.Location = new System.Drawing.Point(542, 458);
            this.btnNextToBook.Name = "btnNextToBook";
            this.btnNextToBook.Size = new System.Drawing.Size(140, 36);
            this.btnNextToBook.TabIndex = 2;
            this.btnNextToBook.Text = "Next: Book >>";
            this.btnNextToBook.UseVisualStyleBackColor = false;
            this.btnNextToBook.Click += new System.EventHandler(this.btnNextToBook_Click);
            // 
            // gbMemberStatus
            // 
            this.gbMemberStatus.Controls.Add(this.lblEligibility);
            this.gbMemberStatus.Controls.Add(this.label6);
            this.gbMemberStatus.Controls.Add(this.lblActiveLoans);
            this.gbMemberStatus.Controls.Add(this.label4);
            this.gbMemberStatus.Controls.Add(this.lblSubscriptionDate);
            this.gbMemberStatus.Controls.Add(this.label3);
            this.gbMemberStatus.Controls.Add(this.lblMemberID);
            this.gbMemberStatus.Controls.Add(this.label2);
            this.gbMemberStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.gbMemberStatus.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.gbMemberStatus.Location = new System.Drawing.Point(10, 375);
            this.gbMemberStatus.Name = "gbMemberStatus";
            this.gbMemberStatus.Size = new System.Drawing.Size(672, 75);
            this.gbMemberStatus.TabIndex = 1;
            this.gbMemberStatus.TabStop = false;
            this.gbMemberStatus.Text = "Membership & Circulation Status";
            // 
            // lblEligibility
            // 
            this.lblEligibility.AutoSize = true;
            this.lblEligibility.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEligibility.ForeColor = System.Drawing.Color.Gray;
            this.lblEligibility.Location = new System.Drawing.Point(520, 30);
            this.lblEligibility.Name = "lblEligibility";
            this.lblEligibility.Size = new System.Drawing.Size(126, 17);
            this.lblEligibility.TabIndex = 7;
            this.lblEligibility.Text = "Select a member...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(445, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Eligibility:";
            // 
            // lblActiveLoans
            // 
            this.lblActiveLoans.AutoSize = true;
            this.lblActiveLoans.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblActiveLoans.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblActiveLoans.Location = new System.Drawing.Point(380, 30);
            this.lblActiveLoans.Name = "lblActiveLoans";
            this.lblActiveLoans.Size = new System.Drawing.Size(35, 17);
            this.lblActiveLoans.TabIndex = 5;
            this.lblActiveLoans.Text = "[???]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(285, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Active Loans:";
            // 
            // lblSubscriptionDate
            // 
            this.lblSubscriptionDate.AutoSize = true;
            this.lblSubscriptionDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSubscriptionDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSubscriptionDate.Location = new System.Drawing.Point(195, 30);
            this.lblSubscriptionDate.Name = "lblSubscriptionDate";
            this.lblSubscriptionDate.Size = new System.Drawing.Size(35, 17);
            this.lblSubscriptionDate.TabIndex = 3;
            this.lblSubscriptionDate.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(145, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Since:";
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMemberID.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblMemberID.Location = new System.Drawing.Point(90, 30);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(35, 17);
            this.lblMemberID.TabIndex = 1;
            this.lblMemberID.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Member ID:";
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(10, 6);
            this.ctrlPersonCardWithFilter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(672, 365);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            // 
            // tpBookAndLoan
            // 
            this.tpBookAndLoan.Controls.Add(this.btnBorrow);
            this.tpBookAndLoan.Controls.Add(this.btnBackToMember);
            this.tpBookAndLoan.Controls.Add(this.gbLoanDetails);
            this.tpBookAndLoan.Controls.Add(this.ctrlBookCardWithFilter1);
            this.tpBookAndLoan.Location = new System.Drawing.Point(4, 26);
            this.tpBookAndLoan.Name = "tpBookAndLoan";
            this.tpBookAndLoan.Padding = new System.Windows.Forms.Padding(3);
            this.tpBookAndLoan.Size = new System.Drawing.Size(692, 505);
            this.tpBookAndLoan.TabIndex = 1;
            this.tpBookAndLoan.Text = "2. Book & Loan Terms (الكتاب وتفاصيل الإعارة)";
            this.tpBookAndLoan.UseVisualStyleBackColor = true;
            // 
            // btnBorrow
            // 
            this.btnBorrow.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnBorrow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrow.Enabled = false;
            this.btnBorrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBorrow.ForeColor = System.Drawing.Color.White;
            this.btnBorrow.Location = new System.Drawing.Point(512, 458);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(170, 36);
            this.btnBorrow.TabIndex = 3;
            this.btnBorrow.Text = "Issue Loan / استعارة";
            this.btnBorrow.UseVisualStyleBackColor = false;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnBackToMember
            // 
            this.btnBackToMember.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnBackToMember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToMember.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackToMember.ForeColor = System.Drawing.Color.White;
            this.btnBackToMember.Location = new System.Drawing.Point(10, 458);
            this.btnBackToMember.Name = "btnBackToMember";
            this.btnBackToMember.Size = new System.Drawing.Size(140, 36);
            this.btnBackToMember.TabIndex = 2;
            this.btnBackToMember.Text = "<< Back to Member";
            this.btnBackToMember.UseVisualStyleBackColor = false;
            this.btnBackToMember.Click += new System.EventHandler(this.btnBackToMember_Click);
            // 
            // gbLoanDetails
            // 
            this.gbLoanDetails.Controls.Add(this.lblCreatedBy);
            this.gbLoanDetails.Controls.Add(this.label9);
            this.gbLoanDetails.Controls.Add(this.dtpDueDate);
            this.gbLoanDetails.Controls.Add(this.label8);
            this.gbLoanDetails.Controls.Add(this.lblBorrowDate);
            this.gbLoanDetails.Controls.Add(this.label7);
            this.gbLoanDetails.Controls.Add(this.cbAvailableCopies);
            this.gbLoanDetails.Controls.Add(this.label5);
            this.gbLoanDetails.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.gbLoanDetails.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.gbLoanDetails.Location = new System.Drawing.Point(10, 375);
            this.gbLoanDetails.Name = "gbLoanDetails";
            this.gbLoanDetails.Size = new System.Drawing.Size(672, 75);
            this.gbLoanDetails.TabIndex = 1;
            this.gbLoanDetails.TabStop = false;
            this.gbLoanDetails.Text = "Circulation Parameters";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCreatedBy.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCreatedBy.Location = new System.Drawing.Point(585, 30);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(35, 17);
            this.lblCreatedBy.TabIndex = 7;
            this.lblCreatedBy.Text = "[???]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(515, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Issued By:";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(395, 27);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(105, 24);
            this.dtpDueDate.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(325, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Due Date:";
            // 
            // lblBorrowDate
            // 
            this.lblBorrowDate.AutoSize = true;
            this.lblBorrowDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBorrowDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblBorrowDate.Location = new System.Drawing.Point(235, 30);
            this.lblBorrowDate.Name = "lblBorrowDate";
            this.lblBorrowDate.Size = new System.Drawing.Size(78, 17);
            this.lblBorrowDate.TabIndex = 3;
            this.lblBorrowDate.Text = "01/01/2026";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(175, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Loaned:";
            // 
            // cbAvailableCopies
            // 
            this.cbAvailableCopies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAvailableCopies.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbAvailableCopies.FormattingEnabled = true;
            this.cbAvailableCopies.Location = new System.Drawing.Point(75, 27);
            this.cbAvailableCopies.Name = "cbAvailableCopies";
            this.cbAvailableCopies.Size = new System.Drawing.Size(90, 24);
            this.cbAvailableCopies.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Copy ID:";
            // 
            // ctrlBookCardWithFilter1
            // 
            this.ctrlBookCardWithFilter1.FilterEnabled = true;
            this.ctrlBookCardWithFilter1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlBookCardWithFilter1.Location = new System.Drawing.Point(10, 6);
            this.ctrlBookCardWithFilter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrlBookCardWithFilter1.Name = "ctrlBookCardWithFilter1";
            this.ctrlBookCardWithFilter1.Size = new System.Drawing.Size(672, 365);
            this.ctrlBookCardWithFilter1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(582, 591);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 36);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmBorrowBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(724, 636);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tcBorrow);
            this.Controls.Add(this.lblHeaderTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBorrowBook";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Issue Book Loan";
            this.Load += new System.EventHandler(this.frmBorrowBook_Load);
            this.tcBorrow.ResumeLayout(false);
            this.tpMember.ResumeLayout(false);
            this.gbMemberStatus.ResumeLayout(false);
            this.gbMemberStatus.PerformLayout();
            this.tpBookAndLoan.ResumeLayout(false);
            this.gbLoanDetails.ResumeLayout(false);
            this.gbLoanDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.TabControl tcBorrow;
        private System.Windows.Forms.TabPage tpMember;
        private System.Windows.Forms.TabPage tpBookAndLoan;
        private ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private ctrlBookCardWithFilter ctrlBookCardWithFilter1;
        private System.Windows.Forms.GroupBox gbMemberStatus;
        private System.Windows.Forms.Label lblEligibility;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblActiveLoans;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSubscriptionDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNextToBook;
        private System.Windows.Forms.GroupBox gbLoanDetails;
        private System.Windows.Forms.ComboBox cbAvailableCopies;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBorrowDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnBackToMember;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
