using System;
using System.Drawing;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Circulation Form for processing book returns, calculating overdue durations,
    /// and restoring physical copy inventory to Available status.
    /// </summary>
    public partial class frmReturnBook : Form
    {
        

        private int _initialBorrowingID = -1;
        private clsBorrowingRecord _currentBorrowing = null;

        /// <summary>
        /// Daily fine rate in dollars for overdue loans.
        /// </summary>
        public const decimal DailyFineRate = 1.00m;

        public frmReturnBook()
        {
            InitializeComponent();
        }

        public frmReturnBook(int borrowingID) : this()
        {
            _initialBorrowingID = borrowingID;
        }

        private void frmReturnBook_Load(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0; // Default: Borrowing ID
            lblReturnDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (_initialBorrowingID > 0)
            {
                txtSearchValue.Text = _initialBorrowingID.ToString();
                _SearchBorrowing();
            }
            else
            {
                _ResetForm();
            }
        }

        private void _ResetForm()
        {
            _currentBorrowing = null;

            lblBorrowingID.Text = "[???]";
            lblBookTitle.Text = "[???]";
            lblISBN.Text = "[???]";
            lblCopyID.Text = "[???]";
            lblMemberID.Text = "[???]";
            lblMemberName.Text = "[???]";
            lblBorrowDate.Text = "[???]";
            lblDueDate.Text = "[???]";
            lblStatus.Text = "[???]";
            lblStatus.ForeColor = Color.Gray;

            lblOverdueDays.Text = "On Time";
            lblOverdueDays.ForeColor = Color.ForestGreen;
            lblEstimatedFine.Text = "$0.00";
            lblEstimatedFine.ForeColor = Color.ForestGreen;

            btnReturn.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _SearchBorrowing();
        }

        private void txtSearchValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _SearchBorrowing();
            }
        }

        private void _SearchBorrowing()
        {
            string query = txtSearchValue.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                errorProvider1.SetError(txtSearchValue, "Please enter an ID to search.");
                txtSearchValue.Focus();
                return;
            }
            errorProvider1.SetError(txtSearchValue, "");

            if (!int.TryParse(query, out int id))
            {
                MessageBox.Show("Please enter a valid numeric ID.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearchValue.Focus();
                return;
            }

            clsBorrowingRecord record = null;

            if (cbSearchBy.SelectedIndex == 0) // Borrowing ID
            {
                record = clsBorrowingRecord.Find(id);
            }
            else // Book Copy ID
            {
                record = clsBorrowingRecord.FindActiveByCopyID(id);
                if (record == null)
                {
                    MessageBox.Show($"This book copy (ID: {id}) is not currently loaned out.", "No Active Loan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (record == null)
            {
                MessageBox.Show($"No borrowing record found matching [{query}].", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetForm();
                return;
            }

            _currentBorrowing = record;
            _DisplayBorrowingDetails();
        }

        private void _DisplayBorrowingDetails()
        {
            if (_currentBorrowing == null)
                return;

            lblBorrowingID.Text = _currentBorrowing.BorrowingID.ToString();
            lblCopyID.Text = _currentBorrowing.BookCopyID.ToString();
            lblMemberID.Text = _currentBorrowing.MemberID.ToString();

            // Hydrate domain entities
            clsBook book = _currentBorrowing.BookInfo;
            if (book != null)
            {
                lblBookTitle.Text = book.Title;
                lblISBN.Text = book.ISBN;
            }
            else
            {
                lblBookTitle.Text = "[Book record missing]";
                lblISBN.Text = "N/A";
            }

            clsMember member = _currentBorrowing.MemberInfo;
            if (member != null)
            {
                lblMemberName.Text = member.FullName;
            }
            else
            {
                lblMemberName.Text = "[Member missing]";
            }

            lblBorrowDate.Text = _currentBorrowing.BorrowDate.ToString("dd/MM/yyyy");
            lblDueDate.Text = _currentBorrowing.DueDate.ToString("dd/MM/yyyy");

            // Evaluate Overdue Status & Fines
            int lateDays = 0;
            if (_currentBorrowing.IsActive)
            {
                if (DateTime.Now.Date > _currentBorrowing.DueDate.Date)
                {
                    lateDays = (int)(DateTime.Now.Date - _currentBorrowing.DueDate.Date).TotalDays;
                }
            }
            else
            {
                lateDays = _currentBorrowing.OverdueDays;
            }

            if (lateDays > 0)
            {
                decimal fine = lateDays * DailyFineRate;
                lblOverdueDays.Text = $"{lateDays} day(s) OVERDUE";
                lblOverdueDays.ForeColor = Color.Crimson;

                lblEstimatedFine.Text = $"${fine:F2}";
                lblEstimatedFine.ForeColor = Color.Crimson;
            }
            else
            {
                lblOverdueDays.Text = "0 days (On Time)";
                lblOverdueDays.ForeColor = Color.ForestGreen;

                lblEstimatedFine.Text = "$0.00";
                lblEstimatedFine.ForeColor = Color.ForestGreen;
            }

            // Status Badge & Action Control
            if (_currentBorrowing.IsActive)
            {
                if (_currentBorrowing.IsOverdue)
                {
                    lblStatus.Text = "Active (Overdue)";
                    lblStatus.ForeColor = Color.Crimson;
                }
                else
                {
                    lblStatus.Text = "Active";
                    lblStatus.ForeColor = Color.ForestGreen;
                }
                btnReturn.Enabled = true;
            }
            else
            {
                lblStatus.Text = $"Returned on {_currentBorrowing.ActualReturnDate:dd/MM/yyyy}";
                lblStatus.ForeColor = Color.Gray;
                btnReturn.Enabled = false;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (_currentBorrowing == null || !_currentBorrowing.IsActive)
            {
                MessageBox.Show("No active borrowing record is currently loaded.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int lateDays = _currentBorrowing.OverdueDays;
            string lateNotice = lateDays > 0
                ? $"\n\nWARNING: This loan is {lateDays} day(s) overdue! Estimated fine: ${(lateDays * DailyFineRate):F2}."
                : "";

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to process the return for Book Copy [{_currentBorrowing.BookCopyID}]?{lateNotice}",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                if (_currentBorrowing.ReturnBook(DateTime.Now))
                {
                    MessageBox.Show(
                        $"Book successfully returned!\n\nInventory for Copy ID [{_currentBorrowing.BookCopyID}] restored to Available.",
                        "Return Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    
                    _DisplayBorrowingDetails();
                }
                else
                {
                    MessageBox.Show("Failed to process book return. Please verify database connection.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Return operation failed: {ex.Message}", "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

