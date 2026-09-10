namespace LibrarySystem.UI
{
    partial class frmReturnBook
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
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.cbSearchBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbBorrowingInfo = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBorrowDate = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMemberName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMemberID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCopyID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblISBN = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBorrowingID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbReturnInfo = new System.Windows.Forms.GroupBox();
            this.lblEstimatedFine = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblOverdueDays = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbSearch.SuspendLayout();
            this.gbBorrowingInfo.SuspendLayout();
            this.gbReturnInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblHeaderTitle.Location = new System.Drawing.Point(12, 9);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(650, 38);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Return Book / استرجاع كتاب";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtSearchValue);
            this.gbSearch.Controls.Add(this.cbSearchBy);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.gbSearch.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.gbSearch.Location = new System.Drawing.Point(12, 50);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(650, 70);
            this.gbSearch.TabIndex = 1;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Find Active Loan Record";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(465, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 28);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchValue.Location = new System.Drawing.Point(260, 26);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(185, 24);
            this.txtSearchValue.TabIndex = 2;
            this.txtSearchValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchValue_KeyDown);
            // 
            // cbSearchBy
            // 
            this.cbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchBy.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cbSearchBy.FormattingEnabled = true;
            this.cbSearchBy.Items.AddRange(new object[] {
            "Borrowing ID",
            "Book Copy ID"});
            this.cbSearchBy.Location = new System.Drawing.Point(95, 26);
            this.cbSearchBy.Name = "cbSearchBy";
            this.cbSearchBy.Size = new System.Drawing.Size(150, 24);
            this.cbSearchBy.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search By:";
            // 
            // gbBorrowingInfo
            // 
            this.gbBorrowingInfo.Controls.Add(this.lblStatus);
            this.gbBorrowingInfo.Controls.Add(this.label11);
            this.gbBorrowingInfo.Controls.Add(this.lblDueDate);
            this.gbBorrowingInfo.Controls.Add(this.label9);
            this.gbBorrowingInfo.Controls.Add(this.lblBorrowDate);
            this.gbBorrowingInfo.Controls.Add(this.label8);
            this.gbBorrowingInfo.Controls.Add(this.lblMemberName);
            this.gbBorrowingInfo.Controls.Add(this.label7);
            this.gbBorrowingInfo.Controls.Add(this.lblMemberID);
            this.gbBorrowingInfo.Controls.Add(this.label6);
            this.gbBorrowingInfo.Controls.Add(this.lblCopyID);
            this.gbBorrowingInfo.Controls.Add(this.label5);
            this.gbBorrowingInfo.Controls.Add(this.lblISBN);
            this.gbBorrowingInfo.Controls.Add(this.label4);
            this.gbBorrowingInfo.Controls.Add(this.lblBookTitle);
            this.gbBorrowingInfo.Controls.Add(this.label3);
            this.gbBorrowingInfo.Controls.Add(this.lblBorrowingID);
            this.gbBorrowingInfo.Controls.Add(this.label2);
            this.gbBorrowingInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.gbBorrowingInfo.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.gbBorrowingInfo.Location = new System.Drawing.Point(12, 130);
            this.gbBorrowingInfo.Name = "gbBorrowingInfo";
            this.gbBorrowingInfo.Size = new System.Drawing.Size(650, 235);
            this.gbBorrowingInfo.TabIndex = 2;
            this.gbBorrowingInfo.TabStop = false;
            this.gbBorrowingInfo.Text = "Loan Record Information";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(440, 190);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 19);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "[???]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(340, 192);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "Loan Status:";
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDueDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblDueDate.Location = new System.Drawing.Point(440, 150);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(35, 17);
            this.lblDueDate.TabIndex = 15;
            this.lblDueDate.Text = "[???]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(340, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Due Date:";
            // 
            // lblBorrowDate
            // 
            this.lblBorrowDate.AutoSize = true;
            this.lblBorrowDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBorrowDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblBorrowDate.Location = new System.Drawing.Point(440, 110);
            this.lblBorrowDate.Name = "lblBorrowDate";
            this.lblBorrowDate.Size = new System.Drawing.Size(35, 17);
            this.lblBorrowDate.TabIndex = 13;
            this.lblBorrowDate.Text = "[???]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(340, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Borrow Date:";
            // 
            // lblMemberName
            // 
            this.lblMemberName.AutoSize = true;
            this.lblMemberName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMemberName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblMemberName.Location = new System.Drawing.Point(440, 70);
            this.lblMemberName.Name = "lblMemberName";
            this.lblMemberName.Size = new System.Drawing.Size(35, 17);
            this.lblMemberName.TabIndex = 11;
            this.lblMemberName.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(340, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Member:";
            // 
            // lblMemberID
            // 
            this.lblMemberID.AutoSize = true;
            this.lblMemberID.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMemberID.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblMemberID.Location = new System.Drawing.Point(440, 30);
            this.lblMemberID.Name = "lblMemberID";
            this.lblMemberID.Size = new System.Drawing.Size(35, 17);
            this.lblMemberID.TabIndex = 9;
            this.lblMemberID.Text = "[???]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(340, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Member ID:";
            // 
            // lblCopyID
            // 
            this.lblCopyID.AutoSize = true;
            this.lblCopyID.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCopyID.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCopyID.Location = new System.Drawing.Point(120, 150);
            this.lblCopyID.Name = "lblCopyID";
            this.lblCopyID.Size = new System.Drawing.Size(35, 17);
            this.lblCopyID.TabIndex = 7;
            this.lblCopyID.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(15, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Copy ID:";
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblISBN.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblISBN.Location = new System.Drawing.Point(120, 110);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(35, 17);
            this.lblISBN.TabIndex = 5;
            this.lblISBN.Text = "[???]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(15, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "ISBN:";
            // 
            // lblBookTitle
            // 
            this.lblBookTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBookTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblBookTitle.Location = new System.Drawing.Point(120, 70);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(200, 35);
            this.lblBookTitle.TabIndex = 3;
            this.lblBookTitle.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(15, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Book Title:";
            // 
            // lblBorrowingID
            // 
            this.lblBorrowingID.AutoSize = true;
            this.lblBorrowingID.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBorrowingID.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblBorrowingID.Location = new System.Drawing.Point(120, 30);
            this.lblBorrowingID.Name = "lblBorrowingID";
            this.lblBorrowingID.Size = new System.Drawing.Size(35, 17);
            this.lblBorrowingID.TabIndex = 1;
            this.lblBorrowingID.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Borrowing ID:";
            // 
            // gbReturnInfo
            // 
            this.gbReturnInfo.Controls.Add(this.lblEstimatedFine);
            this.gbReturnInfo.Controls.Add(this.label14);
            this.gbReturnInfo.Controls.Add(this.lblOverdueDays);
            this.gbReturnInfo.Controls.Add(this.label13);
            this.gbReturnInfo.Controls.Add(this.lblReturnDate);
            this.gbReturnInfo.Controls.Add(this.label12);
            this.gbReturnInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.gbReturnInfo.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.gbReturnInfo.Location = new System.Drawing.Point(12, 380);
            this.gbReturnInfo.Name = "gbReturnInfo";
            this.gbReturnInfo.Size = new System.Drawing.Size(650, 75);
            this.gbReturnInfo.TabIndex = 3;
            this.gbReturnInfo.TabStop = false;
            this.gbReturnInfo.Text = "Return Assessment & Delay Evaluation";
            // 
            // lblEstimatedFine
            // 
            this.lblEstimatedFine.AutoSize = true;
            this.lblEstimatedFine.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstimatedFine.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblEstimatedFine.Location = new System.Drawing.Point(540, 30);
            this.lblEstimatedFine.Name = "lblEstimatedFine";
            this.lblEstimatedFine.Size = new System.Drawing.Size(45, 19);
            this.lblEstimatedFine.TabIndex = 5;
            this.lblEstimatedFine.Text = "$0.00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(445, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 17);
            this.label14.TabIndex = 4;
            this.label14.Text = "Accrued Fine:";
            // 
            // lblOverdueDays
            // 
            this.lblOverdueDays.AutoSize = true;
            this.lblOverdueDays.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOverdueDays.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblOverdueDays.Location = new System.Drawing.Point(325, 30);
            this.lblOverdueDays.Name = "lblOverdueDays";
            this.lblOverdueDays.Size = new System.Drawing.Size(65, 19);
            this.lblOverdueDays.TabIndex = 3;
            this.lblOverdueDays.Text = "On Time";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(220, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Late Duration:";
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblReturnDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblReturnDate.Location = new System.Drawing.Point(105, 30);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(78, 17);
            this.lblReturnDate.TabIndex = 1;
            this.lblReturnDate.Text = "01/01/2026";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(15, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Return Date:";
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.Enabled = false;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(442, 475);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(220, 38);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Confirm Return / استرجاع";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(310, 475);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmReturnBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(674, 530);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.gbReturnInfo);
            this.Controls.Add(this.gbBorrowingInfo);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.lblHeaderTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReturnBook";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Return Borrowed Book";
            this.Load += new System.EventHandler(this.frmReturnBook_Load);
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbBorrowingInfo.ResumeLayout(false);
            this.gbBorrowingInfo.PerformLayout();
            this.gbReturnInfo.ResumeLayout(false);
            this.gbReturnInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSearchBy;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gbBorrowingInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBorrowingID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCopyID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMemberID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMemberName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblBorrowDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox gbReturnInfo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblOverdueDays;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblEstimatedFine;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
