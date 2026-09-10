using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Management screen displaying the complete book catalog with live filtering,
    /// copy provisioning, and contextual actions.
    /// </summary>
    public partial class frmBooksList : Form
    {
        private DataTable _dtBooks;

        public frmBooksList()
        {
            InitializeComponent();
        }

        private void _RefreshBooksList()
        {
            _dtBooks = clsBook.GetAllBooks();
            dgvBooks.DataSource = _dtBooks;
            lblRecordsCount.Text = $"# Records: {_dtBooks.Rows.Count}";

            if (dgvBooks.Rows.Count > 0)
            {
                if (dgvBooks.Columns["ImagePath"] != null)
                    dgvBooks.Columns["ImagePath"].Visible = false;

                if (dgvBooks.Columns["AuthorID"] != null)
                    dgvBooks.Columns["AuthorID"].Visible = false;

                if (dgvBooks.Columns["GenreID"] != null)
                    dgvBooks.Columns["GenreID"].Visible = false;

                dgvBooks.Columns["BookID"].HeaderText = "Book ID";
                dgvBooks.Columns["BookID"].Width = 80;

                dgvBooks.Columns["Title"].HeaderText = "Title";
                dgvBooks.Columns["Title"].Width = 240;

                dgvBooks.Columns["ISBN"].HeaderText = "ISBN";
                dgvBooks.Columns["ISBN"].Width = 120;

                dgvBooks.Columns["PublicationYear"].HeaderText = "Year";
                dgvBooks.Columns["PublicationYear"].Width = 70;

                dgvBooks.Columns["AuthorName"].HeaderText = "Author";
                dgvBooks.Columns["AuthorName"].Width = 150;

                dgvBooks.Columns["GenreName"].HeaderText = "Genre";
                dgvBooks.Columns["GenreName"].Width = 120;

                dgvBooks.Columns["TotalCopies"].HeaderText = "Total Copies";
                dgvBooks.Columns["TotalCopies"].Width = 90;

                dgvBooks.Columns["AvailableCopies"].HeaderText = "Available";
                dgvBooks.Columns["AvailableCopies"].Width = 90;
            }
        }

        private void frmBooksList_Load(object sender, EventArgs e)
        {
            _RefreshBooksList();
            cbFilterBy.SelectedIndex = 0; // "None"
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = string.Empty;
                txtFilterValue.Focus();
            }
            else
            {
                if (_dtBooks != null)
                {
                    _dtBooks.DefaultView.RowFilter = string.Empty;
                    lblRecordsCount.Text = $"# Records: {_dtBooks.DefaultView.Count}";
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_dtBooks == null) return;

            string filterColumn = string.Empty;
            switch (cbFilterBy.Text)
            {
                case "Book ID":
                    filterColumn = "BookID";
                    break;
                case "Title":
                    filterColumn = "Title";
                    break;
                case "ISBN":
                    filterColumn = "ISBN";
                    break;
                case "Author":
                    filterColumn = "AuthorName";
                    break;
                case "Genre":
                    filterColumn = "GenreName";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || filterColumn == "None")
            {
                _dtBooks.DefaultView.RowFilter = string.Empty;
                lblRecordsCount.Text = $"# Records: {_dtBooks.DefaultView.Count}";
                return;
            }

            string filterVal = txtFilterValue.Text.Trim().Replace("'", "''");

            if (filterColumn == "BookID")
            {
                if (int.TryParse(filterVal, out int id))
                    _dtBooks.DefaultView.RowFilter = $"[{filterColumn}] = {id}";
                else
                    _dtBooks.DefaultView.RowFilter = "1 = 0";
            }
            else
            {
                _dtBooks.DefaultView.RowFilter = $"[{filterColumn}] LIKE '%{filterVal}%'";
            }

            lblRecordsCount.Text = $"# Records: {_dtBooks.DefaultView.Count}";
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
            using (frmAddUpdateBook frm = new frmAddUpdateBook())
            {
                frm.DataBack += (bookID) =>
                {
                    _RefreshBooksList();
                };
                frm.ShowDialog();
            }
        }

        private int _GetSelectedBookID()
        {
            if (dgvBooks.SelectedRows.Count == 0)
                return -1;

            return (int)dgvBooks.SelectedRows[0].Cells["BookID"].Value;
        }

        private void bookDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            using (frmBookDetails frm = new frmBookDetails(bookID))
            {
                frm.ShowDialog();
                _RefreshBooksList();
            }
        }

        private void editBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            using (frmAddUpdateBook frm = new frmAddUpdateBook(bookID))
            {
                frm.DataBack += (id) =>
                {
                    _RefreshBooksList();
                };
                frm.ShowDialog();
            }
        }

        private void addCopiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            using (frmBookDetails frm = new frmBookDetails(bookID))
            {
                frm.ShowDialog();
                _RefreshBooksList();
            }
        }

        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = _GetSelectedBookID();
            if (bookID <= 0) return;

            if (MessageBox.Show($"Are you sure you want to delete Book [{bookID}]?", "Confirm Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (clsBook.Delete(bookID))
                {
                    MessageBox.Show("Book and associated copies deleted successfully!", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshBooksList();
                }
                else
                {
                    MessageBox.Show("Failed to delete book.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deletion Blocked",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
