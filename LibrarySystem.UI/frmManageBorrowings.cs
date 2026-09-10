using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Central Circulation & Borrowings management screen.
    /// Provides real-time KPI metrics, combined status/attribute filtering,
    /// overdue visual indicators, and contextual return/loan operations.
    /// </summary>
    public partial class frmManageBorrowings : Form
    {
        private DataTable _dtBorrowings;
        private bool _isLoading = true;

        public frmManageBorrowings()
        {
            InitializeComponent();
        }

        private void frmManageBorrowings_Load(object sender, EventArgs e)
        {
            _isLoading = true;

            cbStatusFilter.SelectedIndex = 0; // "All"
            cbFilterBy.SelectedIndex = 0;     // "None"

            _isLoading = false;
            _RefreshBorrowingsList();
        }

        #region Data Retrieval & Grid Setup

        private void _RefreshBorrowingsList()
        {
            _dtBorrowings = clsBorrowingRecord.GetAllBorrowings();
            dgvBorrowings.DataSource = _dtBorrowings;

            _UpdateKpis();
            _FormatGridColumns();
            _ApplyCombinedFilter();
        }

        private void _UpdateKpis()
        {
            if (_dtBorrowings == null)
            {
                lblTotalCount.Text = "0";
                lblActiveCount.Text = "0";
                lblOverdueCount.Text = "0";
                lblReturnedCount.Text = "0";
                return;
            }

            int total = _dtBorrowings.Rows.Count;
            int active = 0;
            int overdue = 0;
            int returned = 0;

            foreach (DataRow row in _dtBorrowings.Rows)
            {
                string status = row["Status"]?.ToString() ?? string.Empty;
                if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
                    active++;
                else if (status.Equals("Overdue", StringComparison.OrdinalIgnoreCase))
                    overdue++;
                else if (status.Equals("Returned", StringComparison.OrdinalIgnoreCase))
                    returned++;
            }

            lblTotalCount.Text = total.ToString();
            lblActiveCount.Text = active.ToString();
            lblOverdueCount.Text = overdue.ToString();
            lblReturnedCount.Text = returned.ToString();
        }

        private void _FormatGridColumns()
        {
            if (dgvBorrowings.Columns.Count == 0)
                return;

            // Hide internal and foreign key columns
            if (dgvBorrowings.Columns["PersonID"] != null)
                dgvBorrowings.Columns["PersonID"].Visible = false;

            if (dgvBorrowings.Columns["MemberID"] != null)
                dgvBorrowings.Columns["MemberID"].Visible = false;

            if (dgvBorrowings.Columns["BookID"] != null)
                dgvBorrowings.Columns["BookID"].Visible = false;

            if (dgvBorrowings.Columns["CreatedByUserID"] != null)
                dgvBorrowings.Columns["CreatedByUserID"].Visible = false;

            // Configure visible columns
            if (dgvBorrowings.Columns["BorrowingID"] != null)
            {
                dgvBorrowings.Columns["BorrowingID"].HeaderText = "Loan ID";
                dgvBorrowings.Columns["BorrowingID"].Width = 75;
                dgvBorrowings.Columns["BorrowingID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["BookTitle"] != null)
            {
                dgvBorrowings.Columns["BookTitle"].HeaderText = "Book Title";
                dgvBorrowings.Columns["BookTitle"].Width = 220;
            }

            if (dgvBorrowings.Columns["ISBN"] != null)
            {
                dgvBorrowings.Columns["ISBN"].HeaderText = "ISBN";
                dgvBorrowings.Columns["ISBN"].Width = 110;
            }

            if (dgvBorrowings.Columns["BookCopyID"] != null)
            {
                dgvBorrowings.Columns["BookCopyID"].HeaderText = "Copy ID";
                dgvBorrowings.Columns["BookCopyID"].Width = 75;
                dgvBorrowings.Columns["BookCopyID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["MemberName"] != null)
            {
                dgvBorrowings.Columns["MemberName"].HeaderText = "Member Name";
                dgvBorrowings.Columns["MemberName"].Width = 160;
            }

            if (dgvBorrowings.Columns["MemberPhone"] != null)
            {
                dgvBorrowings.Columns["MemberPhone"].HeaderText = "Phone";
                dgvBorrowings.Columns["MemberPhone"].Width = 100;
            }

            if (dgvBorrowings.Columns["BorrowDate"] != null)
            {
                dgvBorrowings.Columns["BorrowDate"].HeaderText = "Borrow Date";
                dgvBorrowings.Columns["BorrowDate"].Width = 95;
                dgvBorrowings.Columns["BorrowDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvBorrowings.Columns["BorrowDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["DueDate"] != null)
            {
                dgvBorrowings.Columns["DueDate"].HeaderText = "Due Date";
                dgvBorrowings.Columns["DueDate"].Width = 95;
                dgvBorrowings.Columns["DueDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvBorrowings.Columns["DueDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["ActualReturnDate"] != null)
            {
                dgvBorrowings.Columns["ActualReturnDate"].HeaderText = "Return Date";
                dgvBorrowings.Columns["ActualReturnDate"].Width = 95;
                dgvBorrowings.Columns["ActualReturnDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvBorrowings.Columns["ActualReturnDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["Status"] != null)
            {
                dgvBorrowings.Columns["Status"].HeaderText = "Status";
                dgvBorrowings.Columns["Status"].Width = 85;
                dgvBorrowings.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["OverdueDays"] != null)
            {
                dgvBorrowings.Columns["OverdueDays"].HeaderText = "Late Days";
                dgvBorrowings.Columns["OverdueDays"].Width = 75;
                dgvBorrowings.Columns["OverdueDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvBorrowings.Columns["CreatedByUsername"] != null)
            {
                dgvBorrowings.Columns["CreatedByUsername"].HeaderText = "Issued By";
                dgvBorrowings.Columns["CreatedByUsername"].Width = 90;
            }
        }

        #endregion

        #region Filtering & Search

        private void _ApplyCombinedFilter()
        {
            if (_isLoading || _dtBorrowings == null)
                return;

            string statusFilter = string.Empty;
            string selectedStatus = cbStatusFilter.SelectedItem?.ToString() ?? "All";

            if (selectedStatus != "All")
            {
                statusFilter = $"[Status] = '{selectedStatus}'";
            }

            string columnFilter = string.Empty;
            string filterColumn = cbFilterBy.SelectedItem?.ToString() ?? "None";
            string filterValue = txtFilterValue.Text.Trim().Replace("'", "''");

            if (filterColumn != "None" && !string.IsNullOrWhiteSpace(filterValue))
            {
                string targetColumn = string.Empty;
                bool isNumeric = false;

                switch (filterColumn)
                {
                    case "Borrowing ID":
                        targetColumn = "BorrowingID";
                        isNumeric = true;
                        break;
                    case "Book Title":
                        targetColumn = "BookTitle";
                        break;
                    case "ISBN":
                        targetColumn = "ISBN";
                        break;
                    case "Copy ID":
                        targetColumn = "BookCopyID";
                        isNumeric = true;
                        break;
                    case "Member Name":
                        targetColumn = "MemberName";
                        break;
                    case "Member Phone":
                        targetColumn = "MemberPhone";
                        break;
                }

                if (!string.IsNullOrEmpty(targetColumn))
                {
                    if (isNumeric)
                    {
                        if (int.TryParse(filterValue, out int numVal))
                            columnFilter = $"[{targetColumn}] = {numVal}";
                        else
                            columnFilter = "1 = 0";
                    }
                    else
                    {
                        columnFilter = $"[{targetColumn}] LIKE '%{filterValue}%'";
                    }
                }
            }

            // Combine both filters
            string finalFilter;
            if (!string.IsNullOrEmpty(statusFilter) && !string.IsNullOrEmpty(columnFilter))
            {
                finalFilter = $"({statusFilter}) AND ({columnFilter})";
            }
            else if (!string.IsNullOrEmpty(statusFilter))
            {
                finalFilter = statusFilter;
            }
            else if (!string.IsNullOrEmpty(columnFilter))
            {
                finalFilter = columnFilter;
            }
            else
            {
                finalFilter = string.Empty;
            }

            _dtBorrowings.DefaultView.RowFilter = finalFilter;
            lblRecordsCount.Text = $"# Records: {_dtBorrowings.DefaultView.Count}";
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ApplyCombinedFilter();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.SelectedItem?.ToString() != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }

            _ApplyCombinedFilter();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            _ApplyCombinedFilter();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selected = cbFilterBy.SelectedItem?.ToString();
            if (selected == "Borrowing ID" || selected == "Copy ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            _isLoading = true;
            cbStatusFilter.SelectedIndex = 0; // "All"
            cbFilterBy.SelectedIndex = 0;     // "None"
            txtFilterValue.Text = string.Empty;
            txtFilterValue.Visible = false;
            _isLoading = false;

            _ApplyCombinedFilter();
        }

        #endregion

        #region Visual Highlighting & Context Menu

        private void dgvBorrowings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvBorrowings.Rows.Count)
                return;

            DataGridViewRow row = dgvBorrowings.Rows[e.RowIndex];
            object statusObj = row.Cells["Status"]?.Value;
            if (statusObj == null || statusObj == DBNull.Value)
                return;

            string status = statusObj.ToString();

            if (status.Equals("Overdue", StringComparison.OrdinalIgnoreCase))
            {
                // Soft light red highlight for overdue loans
                e.CellStyle.BackColor = Color.FromArgb(254, 242, 242);
                e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                e.CellStyle.SelectionBackColor = Color.FromArgb(254, 202, 202);
                e.CellStyle.SelectionForeColor = Color.FromArgb(153, 27, 27);
            }
            else if (status.Equals("Active", StringComparison.OrdinalIgnoreCase))
            {
                // Soft light green highlight for active in-progress loans
                e.CellStyle.BackColor = Color.FromArgb(240, 253, 244);
                e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61);
                e.CellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
                e.CellStyle.SelectionForeColor = Color.FromArgb(20, 83, 45);
            }
            else if (status.Equals("Returned", StringComparison.OrdinalIgnoreCase))
            {
                // Neutral gray for completed returns
                e.CellStyle.ForeColor = Color.FromArgb(75, 85, 99);
            }
        }

        private void dgvBorrowings_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvBorrowings.ClearSelection();
                dgvBorrowings.Rows[e.RowIndex].Selected = true;
            }
        }

        private void cmsBorrowings_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int borrowingID = _GetSelectedBorrowingID();
            if (borrowingID <= 0)
            {
                tsmiReturnBook.Enabled = false;
                tsmiBookDetails.Enabled = false;
                return;
            }

            string status = _GetSelectedStatus();
            bool isReturnable = status.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
                                status.Equals("Overdue", StringComparison.OrdinalIgnoreCase);

            tsmiReturnBook.Enabled = isReturnable;
            tsmiBookDetails.Enabled = (_GetSelectedBookID() > 0);
        }

        private void dgvBorrowings_DoubleClick(object sender, EventArgs e)
        {
            int borrowingID = _GetSelectedBorrowingID();
            if (borrowingID <= 0)
                return;

            string status = _GetSelectedStatus();
            if (status.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Overdue", StringComparison.OrdinalIgnoreCase))
            {
                _OpenReturnDialog(borrowingID);
            }
            else
            {
                int bookID = _GetSelectedBookID();
                if (bookID > 0)
                {
                    using (frmBookDetails frm = new frmBookDetails(bookID))
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }

        #endregion

        #region Selected Row Helpers

        private int _GetSelectedBorrowingID()
        {
            if (dgvBorrowings.SelectedRows.Count == 0)
                return -1;

            object val = dgvBorrowings.SelectedRows[0].Cells["BorrowingID"]?.Value;
            if (val != null && int.TryParse(val.ToString(), out int id))
                return id;

            return -1;
        }

        private string _GetSelectedStatus()
        {
            if (dgvBorrowings.SelectedRows.Count == 0)
                return string.Empty;

            return dgvBorrowings.SelectedRows[0].Cells["Status"]?.Value?.ToString() ?? string.Empty;
        }

        private int _GetSelectedBookID()
        {
            if (dgvBorrowings.SelectedRows.Count == 0)
                return -1;

            object val = dgvBorrowings.SelectedRows[0].Cells["BookID"]?.Value;
            if (val != null && int.TryParse(val.ToString(), out int id))
                return id;

            return -1;
        }

        #endregion

        #region Action Operations

        private void _OpenReturnDialog(int borrowingID = -1)
        {
            if (clsGlobal.CurrentUser != null && !clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
            {
                MessageBox.Show("Access Denied! You do not have permission to return books.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (frmReturnBook frm = borrowingID > 0 ? new frmReturnBook(borrowingID) : new frmReturnBook())
            {
                frm.DataBack += (retId) =>
                {
                    _RefreshBorrowingsList();
                };
                frm.ShowDialog();
                _RefreshBorrowingsList();
            }
        }

        private void _OpenBorrowDialog()
        {
            if (clsGlobal.CurrentUser != null && !clsGlobal.CurrentUser.CheckAccessPermission(clsUser.enPermissions.pManageBorrowing))
            {
                MessageBox.Show("Access Denied! You do not have permission to issue book loans.",
                    "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (frmBorrowBook frm = new frmBorrowBook())
            {
                frm.DataBack += (borrowId) =>
                {
                    _RefreshBorrowingsList();
                };
                frm.ShowDialog();
                _RefreshBorrowingsList();
            }
        }

        private void btnBorrowBook_Click(object sender, EventArgs e)
        {
            _OpenBorrowDialog();
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            int borrowingID = _GetSelectedBorrowingID();
            string status = _GetSelectedStatus();

            if (borrowingID > 0 && (status.Equals("Active", StringComparison.OrdinalIgnoreCase) ||
                                    status.Equals("Overdue", StringComparison.OrdinalIgnoreCase)))
            {
                _OpenReturnDialog(borrowingID);
            }
            else
            {
                _OpenReturnDialog(-1);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _RefreshBorrowingsList();
        }

        private void tsmiReturnBook_Click(object sender, EventArgs e)
        {
            int borrowingID = _GetSelectedBorrowingID();
            if (borrowingID > 0)
            {
                _OpenReturnDialog(borrowingID);
            }
        }

        private void tsmiIssueLoan_Click(object sender, EventArgs e)
        {
            _OpenBorrowDialog();
        }

        private void tsmiBookDetails_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID > 0)
            {
                using (frmBookDetails frm = new frmBookDetails(bookID))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            _RefreshBorrowingsList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
