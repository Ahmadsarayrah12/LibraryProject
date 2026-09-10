using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmManageAuthors : Form
    {
        private DataTable _dtAuthors;
        private int _editingAuthorID = -1;

        public frmManageAuthors()
        {
            InitializeComponent();
            _SetupResizing();
        }

        private void _SetupResizing()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            dgvAuthors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void _RefreshAuthorsList()
        {
            _dtAuthors = clsAuthor.GetAllAuthors();
            dgvAuthors.DataSource = _dtAuthors;

            if (_dtAuthors != null)
                lblRecordsCount.Text = "# Records: " + _dtAuthors.Rows.Count;
            else
                lblRecordsCount.Text = "# Records: 0";
        }

        private void _ResetForm()
        {
            _editingAuthorID = -1;
            txtFullName.Clear();
            txtBiography.Clear();
            gbAddAuthor.Text = "Add New Author";
            btnSave.Text = "Save Author";
            btnCancel.Visible = false;
        }

        private void frmManageAuthors_Load(object sender, EventArgs e)
        {
            _RefreshAuthorsList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the full name before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (_editingAuthorID > 0)
                    MessageBox.Show("Author updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Author added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _ResetForm();
                _RefreshAuthorsList();
            }
            else
            {
                MessageBox.Show("Failed to save author. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Author [" + authorID + "] not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _editingAuthorID = author.AuthorID;
            txtFullName.Text = author.FullName;
            txtBiography.Text = author.Biography;
            gbAddAuthor.Text = "Update Author [ID: " + author.AuthorID + "]";
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

        private void deleteAuthorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int authorID = _GetSelectedAuthorID();
            if (authorID <= 0) return;

            if (MessageBox.Show("Are you sure you want to delete Author [" + authorID + "]?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsAuthor.Delete(authorID))
                {
                    MessageBox.Show("Author deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshAuthorsList();
                }
                else
                {
                    MessageBox.Show("Failed to delete author. They might be linked to books.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void dgvAuthors_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvAuthors.ClearSelection();
                dgvAuthors.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dgvAuthors_DoubleClick(object sender, EventArgs e)
        {
            int authorID = _GetSelectedAuthorID();
            if (authorID > 0)
            {
                _StartEditingAuthor(authorID);
            }
        }
    }
}




