using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Management form for viewing, searching, and adding authors to the library system.
    /// Exposes DataBack delegate event to automatically return newly added authors to caller forms.
    /// </summary>
    public partial class frmManageAuthors : Form
    {
        public delegate void DataBackHandler(int authorID, string fullName);
        public event DataBackHandler DataBack;

        private DataTable _dtAuthors;

        public frmManageAuthors()
        {
            InitializeComponent();
        }

        private void _RefreshAuthorsList()
        {
            _dtAuthors = clsAuthor.GetAllAuthors();
            dgvAuthors.DataSource = _dtAuthors;
            lblRecordsCount.Text = $"# Records: {_dtAuthors.Rows.Count}";
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

            clsAuthor author = new clsAuthor();
            author.FullName = txtFullName.Text.Trim();
            author.Biography = txtBiography.Text.Trim();

            if (author.Save())
            {
                MessageBox.Show("Author saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                int newID = author.AuthorID;
                string newName = author.FullName;

                txtFullName.Clear();
                txtBiography.Clear();
                _RefreshAuthorsList();

                DataBack?.Invoke(newID, newName);
            }
            else
            {
                MessageBox.Show("Failed to save author. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
