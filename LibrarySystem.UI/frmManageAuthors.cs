using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Management form for viewing, searching, adding, and editing authors in the library system.
    /// Exposes DataBack delegate event to automatically return newly created or selected authors.
    /// </summary>
    public partial class frmManageAuthors : Form
    {
        public delegate void DataBackHandler(int authorID, string fullName);
        public event DataBackHandler DataBack;

        private DataTable _dtAuthors;
        private int _editingAuthorID = -1;

        public frmManageAuthors()
        {
            InitializeComponent();
        }

        private void _RefreshAuthorsList()
        {
            _dtAuthors = clsAuthor.GetAllAuthors();
            dgvAuthors.DataSource = _dtAuthors;
            lblRecordsCount.Text = $"# Records: {_dtAuthors?.Rows.Count ?? 0}";

            if (dgvAuthors.Columns.Count > 0)
            {
                if (dgvAuthors.Columns["AuthorID"] != null)
                {
                    dgvAuthors.Columns["AuthorID"].HeaderText = "Author ID";
                    dgvAuthors.Columns["AuthorID"].Width = 80;
                }

                if (dgvAuthors.Columns["FullName"] != null)
                {
                    dgvAuthors.Columns["FullName"].HeaderText = "Full Name";
                    dgvAuthors.Columns["FullName"].Width = 220;
                }

                if (dgvAuthors.Columns["Biography"] != null)
                {
                    dgvAuthors.Columns["Biography"].HeaderText = "Biography";
                }
            }
        }

        private void _ResetForm()
        {
            _editingAuthorID = -1;
            txtFullName.Clear();
            txtBiography.Clear();
            gbAddAuthor.Text = "Add New Author";
            btnSave.Text = "Save Author";
            btnCancel.Visible = false;
            errorProvider1.SetError(txtFullName, "");
        }

        private void frmManageAuthors_Load(object sender, EventArgs e)
        {
            _RefreshAuthorsList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill required fields before saving.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsAuthor author;

            if (_editingAuthorID > 0)
            {
                author = clsAuthor.Find(_editingAuthorID);
                if (author == null)
                {
                    MessageBox.Show("Author not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _ResetForm();
                    return;
                }
            }
            else
            {
                author = new clsAuthor();
            }

            author.FullName = txtFullName.Text.Trim();
            author.Biography = txtBiography.Text.Trim();

            if (author.Save())
            {
                string message = _editingAuthorID > 0 ? "Author updated successfully!" : "Author added successfully!";
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int savedID = author.AuthorID;
                string savedName = author.FullName;

                _ResetForm();
                _RefreshAuthorsList();

                DataBack?.Invoke(savedID, savedName);
            }
            else
            {
                MessageBox.Show("Failed to save author. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int _GetSelectedAuthorID()
        {
            if (dgvAuthors.SelectedRows.Count == 0)
                return -1;

            return (int)dgvAuthors.SelectedRows[0].Cells["AuthorID"].Value;
        }

        private void _StartEditingAuthor(int authorID)
        {
            clsAuthor author = clsAuthor.Find(authorID);
            if (author == null)
            {
                MessageBox.Show($"Author [{authorID}] not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _editingAuthorID = author.AuthorID;
            txtFullName.Text = author.FullName;
            txtBiography.Text = author.Biography;
            gbAddAuthor.Text = $"Update Author [ID: {author.AuthorID}]";
            btnSave.Text = "Update";
            btnCancel.Visible = true;
            txtFullName.Focus();
        }

        private void editAuthorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int authorID = _GetSelectedAuthorID();
            if (authorID <= 0) return;

            _StartEditingAuthor(authorID);
        }

        private void dgvAuthors_DoubleClick(object sender, EventArgs e)
        {
            int authorID = _GetSelectedAuthorID();
            if (authorID <= 0) return;

            _StartEditingAuthor(authorID);
        }

        private void deleteAuthorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int authorID = _GetSelectedAuthorID();
            if (authorID <= 0) return;

            if (MessageBox.Show($"Are you sure you want to delete Author [{authorID}]?", "Confirm Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (clsAuthor.Delete(authorID))
                {
                    MessageBox.Show("Author deleted successfully.", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_editingAuthorID == authorID)
                        _ResetForm();
                    _RefreshAuthorsList();
                }
                else
                {
                    MessageBox.Show("Cannot delete author. This author is referenced by books in the catalog.",
                        "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAuthors_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvAuthors.ClearSelection();
                dgvAuthors.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _ResetForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFullName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                errorProvider1.SetError(txtFullName, "Author full name is required.");
            }
            else
            {
                errorProvider1.SetError(txtFullName, "");
            }
        }
    }
}
