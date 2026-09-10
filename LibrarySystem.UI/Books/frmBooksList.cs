using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmBooksList : Form
    {
        private DataTable _dtBooks;

        public frmBooksList()
        {
            InitializeComponent();
            _SetupResizing();
        }

        private void _SetupResizing()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            dgvBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAddBook.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRecordsCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        }

        private void _FormatGridColumns()
        {
            if (dgvBooks.Columns.Count > 0)
            {
                if (dgvBooks.Columns["ImagePath"] != null) dgvBooks.Columns["ImagePath"].Visible = false;
                if (dgvBooks.Columns["AuthorID"] != null) dgvBooks.Columns["AuthorID"].Visible = false;
                if (dgvBooks.Columns["GenreID"] != null) dgvBooks.Columns["GenreID"].Visible = false;

                if (dgvBooks.Columns["BookID"] != null) dgvBooks.Columns["BookID"].Width = 80;
                if (dgvBooks.Columns["Title"] != null) dgvBooks.Columns["Title"].Width = 240;
                if (dgvBooks.Columns["ISBN"] != null) dgvBooks.Columns["ISBN"].Width = 120;
                if (dgvBooks.Columns["PublicationYear"] != null) dgvBooks.Columns["PublicationYear"].Width = 70;
                if (dgvBooks.Columns["AuthorName"] != null) dgvBooks.Columns["AuthorName"].Width = 150;
                if (dgvBooks.Columns["GenreName"] != null) dgvBooks.Columns["GenreName"].Width = 120;
                if (dgvBooks.Columns["TotalCopies"] != null) dgvBooks.Columns["TotalCopies"].Width = 90;
                if (dgvBooks.Columns["AvailableCopies"] != null) dgvBooks.Columns["AvailableCopies"].Width = 90;
            }
        }

        private void _RefreshBooksList()
        {
            _dtBooks = clsBook.GetAllBooks();
            dgvBooks.DataSource = _dtBooks;
            if (_dtBooks != null)
                lblRecordsCount.Text = "# Records: " + _dtBooks.Rows.Count;
            else
                lblRecordsCount.Text = "# Records: 0";
            
            _FormatGridColumns();
        }

        private void frmBooksList_Load(object sender, EventArgs e)
        {
            _RefreshBooksList();
            cbFilterBy.SelectedIndex = 0; // "None"
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                if (_dtBooks != null)
                {
                    _dtBooks.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = "# Records: " + _dtBooks.DefaultView.Count;
                }
            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_dtBooks == null) return;

            string filterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Book ID": filterColumn = "BookID"; break;
                case "Title": filterColumn = "Title"; break;
                case "ISBN": filterColumn = "ISBN"; break;
                case "Author": filterColumn = "AuthorName"; break;
                case "Genre": filterColumn = "GenreName"; break;
                default: filterColumn = "None"; break;
            }

            if (txtFilterValue.Text.Trim() == "" || filterColumn == "None")
            {
                _dtBooks.DefaultView.RowFilter = "";
                lblRecordsCount.Text = "# Records: " + _dtBooks.DefaultView.Count;
                return;
            }

            string filterVal = txtFilterValue.Text.Trim().Replace("'", "''");

            if (filterColumn == "BookID")
            {
                if (int.TryParse(filterVal, out int id))
                    _dtBooks.DefaultView.RowFilter = "[" + filterColumn + "] = " + id;
                else
                    _dtBooks.DefaultView.RowFilter = "1 = 0";
            }
            else
            {
                _dtBooks.DefaultView.RowFilter = "[" + filterColumn + "] LIKE '%" + filterVal + "%'";
            }

            lblRecordsCount.Text = "# Records: " + _dtBooks.DefaultView.Count;
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Book ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            frmAddUpdateBook frm = new frmAddUpdateBook();
            frm.ShowDialog();
            _RefreshBooksList();
        }

        private int _GetSelectedBookID()
        {
            if (dgvBooks.SelectedRows.Count == 0)
                return -1;

            return (int)dgvBooks.SelectedRows[0].Cells["BookID"].Value;
        }

        private void dgvBooks_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvBooks.ClearSelection();
                dgvBooks.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dgvBooks_DoubleClick(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            frmBookDetails frm = new frmBookDetails(bookID);
            frm.ShowDialog();
            _RefreshBooksList();
        }

        private void bookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            frmBookDetails frm = new frmBookDetails(bookID);
            frm.ShowDialog();
            _RefreshBooksList();
        }

        private void editBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            frmAddUpdateBook frm = new frmAddUpdateBook(bookID);
            frm.ShowDialog();
            _RefreshBooksList();
        }

        private void addCopiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            frmBookDetails frm = new frmBookDetails(bookID);
            frm.ShowDialog();
            _RefreshBooksList();
        }

        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            if (MessageBox.Show("Are you sure you want to delete Book [" + bookID + "]?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsBook.DeleteBook(bookID))
                {
                    MessageBox.Show("Book and associated copies deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshBooksList();
                }
                else
                {
                    MessageBox.Show("Failed to delete book.\n\nThis usually happens because the book has active physical copies or borrowing history.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
