using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Circulation Form for issuing new book borrowings to verified library members.
    /// Incorporates real-time member eligibility verification, physical copy availability checks,
    /// and atomic transaction execution.
    /// </summary>
    public partial class frmBorrowBook : Form
    {
        

        private clsMember _selectedMember = null;
        private int _preselectedBookID = -1;
        private int _preselectedMemberID = -1;

        public frmBorrowBook()
        {
            InitializeComponent();
        }

        public frmBorrowBook(int preselectedBookID, int preselectedMemberID = -1) : this()
        {
            _preselectedBookID = preselectedBookID;
            _preselectedMemberID = preselectedMemberID;
        }

        private void frmBorrowBook_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            ctrlPersonCardWithFilter1.OnPersonSelected += _OnPersonSelected;
            ctrlBookCardWithFilter1.OnBookSelected += _OnBookSelected;

            if (_preselectedMemberID > 0)
            {
                clsMember member = clsMember.Find(_preselectedMemberID);
                if (member != null)
                {
                    ctrlPersonCardWithFilter1.LoadPersonInfo(member.PersonID);
                }
            }

            if (_preselectedBookID > 0)
            {
                ctrlBookCardWithFilter1.LoadBookInfo(_preselectedBookID);
                tcBorrow.SelectedTab = tpBookAndLoan;
            }
        }

        private void _ResetDefaultValues()
        {
            lblBorrowDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtpDueDate.MinDate = DateTime.Now.AddDays(1);
            dtpDueDate.MaxDate = DateTime.Now.AddDays(90);
            dtpDueDate.Value = DateTime.Now.AddDays(clsBorrowingRecord.DefaultLoanDurationDays);

            lblCreatedBy.Text = clsGlobal.CurrentUser != null ? clsGlobal.CurrentUser.Username : "System";

            lblMemberID.Text = "[???]";
            lblSubscriptionDate.Text = "[???]";
            lblActiveLoans.Text = "[???]";
            lblEligibility.Text = "Please select a member first.";
            lblEligibility.ForeColor = Color.DimGray;

            btnNextToBook.Enabled = false;
            btnBorrow.Enabled = false;
        }

        private void _OnPersonSelected(int personID)
        {
            if (personID <= 0)
            {
                _selectedMember = null;
                _ResetMemberStatus(false, "No person selected.");
                return;
            }

            clsMember member = clsMember.FindByPersonID(personID);
            if (member == null)
            {
                _selectedMember = null;
                _ResetMemberStatus(false, "Person is not registered as a library member.");
                
                DialogResult dr = MessageBox.Show(
                    "This person is not currently enrolled as a library member. Would you like to enroll them now?",
                    "Not a Member", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    using (frmAddUpdateMember frm = new frmAddUpdateMember())
                    {
                        frm.ShowDialog();
                        _selectedMember = clsMember.FindByPersonID(personID);
                        if (_selectedMember != null)
                        {
                            _DisplayMemberStatus();
                        }
                    }
                }
                return;
            }

            _selectedMember = member;
            _DisplayMemberStatus();
        }

        private void _ResetMemberStatus(bool isEligible, string message)
        {
            lblMemberID.Text = "[???]";
            lblSubscriptionDate.Text = "[???]";
            lblActiveLoans.Text = "[???]";
            lblEligibility.Text = message;
            lblEligibility.ForeColor = isEligible ? Color.Green : Color.Red;
            btnNextToBook.Enabled = isEligible;
        }

        private void _DisplayMemberStatus()
        {
            if (_selectedMember == null)
                return;

            lblMemberID.Text = _selectedMember.MemberID.ToString();
            lblSubscriptionDate.Text = _selectedMember.SubscriptionDate.ToString("dd/MM/yyyy");

            int activeLoans = clsBorrowingRecord.GetActiveBorrowingCountForMember(_selectedMember.MemberID);
            lblActiveLoans.Text = $"{activeLoans} / {clsBorrowingRecord.MaxConcurrentLoansPerMember}";

            if (clsBorrowingRecord.IsMemberEligibleForBorrowing(_selectedMember.MemberID, out string reason))
            {
                lblEligibility.Text = "Eligible to borrow";
                lblEligibility.ForeColor = Color.ForestGreen;
                btnNextToBook.Enabled = true;
            }
            else
            {
                lblEligibility.Text = reason;
                lblEligibility.ForeColor = Color.Crimson;
                btnNextToBook.Enabled = false;
            }

            _ValidateCanBorrow();
        }

        private void _OnBookSelected(int bookID)
        {
            cbAvailableCopies.Items.Clear();

            if (bookID <= 0)
            {
                btnBorrow.Enabled = false;
                return;
            }

            DataTable dtCopies = clsBookCopy.GetBookCopies(bookID);
            if (dtCopies != null)
            {
                foreach (DataRow row in dtCopies.Rows)
                {
                    byte status = Convert.ToByte(row["Status"]);
                    if (status == 1) // 1 = Available
                    {
                        cbAvailableCopies.Items.Add(row["CopyID"].ToString());
                    }
                }
            }

            if (cbAvailableCopies.Items.Count > 0)
            {
                cbAvailableCopies.SelectedIndex = 0;
            }

            _ValidateCanBorrow();
        }

        private void _ValidateCanBorrow()
        {
            bool memberOk = _selectedMember != null &&
                            clsBorrowingRecord.IsMemberEligibleForBorrowing(_selectedMember.MemberID, out _);
            bool bookOk = ctrlBookCardWithFilter1.BookID > 0 && cbAvailableCopies.Items.Count > 0 && cbAvailableCopies.SelectedItem != null;

            btnBorrow.Enabled = memberOk && bookOk;
        }

        private void btnNextToBook_Click(object sender, EventArgs e)
        {
            if (_selectedMember == null)
            {
                MessageBox.Show("Please select a valid library member first.", "Member Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tcBorrow.SelectedTab = tpBookAndLoan;
            ctrlBookCardWithFilter1.FilterFocus();
        }

        private void btnBackToMember_Click(object sender, EventArgs e)
        {
            tcBorrow.SelectedTab = tpMember;
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (_selectedMember == null)
            {
                MessageBox.Show("Please select an eligible library member.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tcBorrow.SelectedTab = tpMember;
                return;
            }

            if (ctrlBookCardWithFilter1.BookID <= 0 || cbAvailableCopies.SelectedItem == null)
            {
                MessageBox.Show("Please select a book with an available physical copy.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int copyID = Convert.ToInt32(cbAvailableCopies.SelectedItem);
            int adminUserID = clsGlobal.CurrentUser != null ? clsGlobal.CurrentUser.UserID : 1;

            DialogResult confirm = MessageBox.Show(
                $"Confirm issuing loan:\n\nMember: {_selectedMember.FullName} (ID: {_selectedMember.MemberID})\nBook: {ctrlBookCardWithFilter1.SelectedBook?.Title}\nCopy ID: {copyID}\nDue Date: {dtpDueDate.Value:dd/MM/yyyy}",
                "Confirm Loan Issue",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                clsBorrowingRecord borrowing = new clsBorrowingRecord
                {
                    MemberID = _selectedMember.MemberID,
                    BookID = ctrlBookCardWithFilter1.BookID,
                    BookCopyID = copyID,
                    BorrowDate = DateTime.Now.Date,
                    DueDate = dtpDueDate.Value,
                    CreatedByUserID = adminUserID
                };

                if (borrowing.Borrow())
                {
                    MessageBox.Show(
                        $"Book loan issued successfully!\n\nBorrowing Record ID: [{borrowing.BorrowingID}]\nDue Date: {borrowing.DueDate:dd/MM/yyyy}",
                        "Loan Completed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    

                    // Refresh book and member view
                    ctrlBookCardWithFilter1.LoadBookInfo(ctrlBookCardWithFilter1.BookID);
                    _DisplayMemberStatus();
                    btnBorrow.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed to issue borrowing record. Please check copy availability.",
                        "Borrowing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Borrowing failed: {ex.Message}", "System Invariant Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

