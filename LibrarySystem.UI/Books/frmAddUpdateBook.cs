using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// User interface dialog for adding a new book catalog entry or updating an existing one.
    /// Handles cover image persistence with deferred disk commits and atomic copy provisioning.
    /// </summary>
    public partial class frmAddUpdateBook : Form
    {
        public delegate void DataBackHandler(int bookID);
        public event DataBackHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        private int _bookID = -1;
        private clsBook _book;
        private string _tempSourceImagePath = null;
        private bool _imageRemoved = false;
        private bool _isSaved = false;

        public frmAddUpdateBook()
        {
            InitializeComponent();
            _mode = enMode.AddNew;
        }

        public frmAddUpdateBook(int bookID)
        {
            InitializeComponent();
            _bookID = bookID;
            _mode = enMode.Update;
        }

        private void _FillAuthorsComboBox()
        {
            DataTable dt = clsAuthor.GetAllAuthors();
            cbAuthors.DataSource = null;
            cbAuthors.DisplayMember = "FullName";
            cbAuthors.ValueMember = "AuthorID";
            cbAuthors.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
                cbAuthors.SelectedIndex = 0;
            else
                cbAuthors.SelectedIndex = -1;
        }

        private void _FillGenresComboBox()
        {
            DataTable dt = clsGenre.GetAllGenres();
            cbGenres.DataSource = null;
            cbGenres.DisplayMember = "GenreName";
            cbGenres.ValueMember = "GenreID";
            cbGenres.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
                cbGenres.SelectedIndex = 0;
            else
                cbGenres.SelectedIndex = -1;
        }

        private void _ResetDefaultValues()
        {
            _FillAuthorsComboBox();
            _FillGenresComboBox();

            nudPublicationYear.Maximum = DateTime.Now.Year + 1;
            nudPublicationYear.Value = DateTime.Now.Year;

            if (_mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Book";
                this.Text = "Add New Book";
                _book = new clsBook();

                lblBookID.Text = "[???]";
                txtTitle.Text = string.Empty;
                txtISBN.Text = string.Empty;
                nudInitialCopies.Value = 1;
                nudInitialCopies.Visible = true;
                lblInitialCopies.Visible = true;
                _tempSourceImagePath = null;
                _imageRemoved = false;

                if (pbCover.Image != null)
                {
                    pbCover.Image.Dispose();
                    pbCover.Image = null;
                }
            }
            else
            {
                lblTitle.Text = "Update Book";
                this.Text = "Update Book";
                nudInitialCopies.Visible = false;
                lblInitialCopies.Visible = false;
            }
        }

        private void _LoadData()
        {
            _book = clsBook.Find(_bookID);

            if (_book == null)
            {
                MessageBox.Show($"Book with ID [{_bookID}] was not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblBookID.Text = _book.BookID.ToString();
            txtTitle.Text = _book.Title;
            txtISBN.Text = _book.ISBN;
            nudPublicationYear.Value = Math.Min(Math.Max(_book.PublicationYear, nudPublicationYear.Minimum), nudPublicationYear.Maximum);

            cbAuthors.SelectedValue = _book.AuthorID;
            cbGenres.SelectedValue = _book.GenreID;

            _LoadCoverImage(_book.ImagePath);
        }

        private void _LoadCoverImage(string imagePath)
        {
            if (pbCover.Image != null)
            {
                pbCover.Image.Dispose();
                pbCover.Image = null;
            }

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string resolvedPath = Path.IsPathRooted(imagePath)
                    ? imagePath
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

                if (File.Exists(resolvedPath))
                {
                    try
                    {
                        using (var bmp = new Bitmap(resolvedPath))
                        {
                            pbCover.Image = new Bitmap(bmp);
                        }
                    }
                    catch
                    {
                        pbCover.Image = null;
                    }
                }
            }
        }

        private void frmAddUpdateBook_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string sourceFile = openFileDialog1.FileName;

                    // Display preview safely without locking file
                    using (var bmp = new Bitmap(sourceFile))
                    {
                        if (pbCover.Image != null)
                        {
                            pbCover.Image.Dispose();
                        }
                        pbCover.Image = new Bitmap(bmp);
                    }

                    _tempSourceImagePath = sourceFile;
                    _imageRemoved = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image preview: {ex.Message}", "Image Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            if (pbCover.Image != null)
            {
                pbCover.Image.Dispose();
                pbCover.Image = null;
            }
            _tempSourceImagePath = null;
            _imageRemoved = true;
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            using (frmManageAuthors frm = new frmManageAuthors())
            {
                frm.DataBack += (authorID, fullName) =>
                {
                    _FillAuthorsComboBox();
                    cbAuthors.SelectedValue = authorID;
                };
                frm.ShowDialog();
            }
        }

        private bool _ProcessImageStorage(ref string finalImagePath)
        {
            if (_imageRemoved)
            {
                // If previous image exists on disk, clean it up
                if (!string.IsNullOrWhiteSpace(_book.ImagePath))
                {
                    string oldPath = Path.IsPathRooted(_book.ImagePath)
                        ? _book.ImagePath
                        : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _book.ImagePath);

                    if (File.Exists(oldPath))
                    {
                        try { File.Delete(oldPath); } catch { }
                    }
                }
                finalImagePath = string.Empty;
                return true;
            }

            // If a new image was chosen, copy to persistent directory
            if (!string.IsNullOrWhiteSpace(_tempSourceImagePath) && File.Exists(_tempSourceImagePath))
            {
                try
                {
                    string destDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Library_Images", "Books");
                    if (!Directory.Exists(destDirectory))
                        Directory.CreateDirectory(destDirectory);

                    string fileExt = Path.GetExtension(_tempSourceImagePath);
                    string targetFileName = Guid.NewGuid().ToString() + fileExt;
                    string destFile = Path.Combine(destDirectory, targetFileName);

                    File.Copy(_tempSourceImagePath, destFile, true);

                    // If replacing an existing old image, delete the old file
                    if (!string.IsNullOrWhiteSpace(_book.ImagePath))
                    {
                        string oldPath = Path.IsPathRooted(_book.ImagePath)
                            ? _book.ImagePath
                            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _book.ImagePath);

                        if (File.Exists(oldPath) && !string.Equals(oldPath, destFile, StringComparison.OrdinalIgnoreCase))
                        {
                            try { File.Delete(oldPath); } catch { }
                        }
                    }

                    finalImagePath = Path.Combine("Library_Images", "Books", targetFileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save image file: {ex.Message}", "Disk Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            finalImagePath = _book.ImagePath;
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct validation errors before proceeding.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbAuthors.SelectedValue == null || !int.TryParse(cbAuthors.SelectedValue.ToString(), out int authorID))
            {
                MessageBox.Show("Please select an author.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbAuthors.Focus();
                return;
            }

            if (cbGenres.SelectedValue == null || !int.TryParse(cbGenres.SelectedValue.ToString(), out int genreID))
            {
                MessageBox.Show("Please select a genre.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbGenres.Focus();
                return;
            }

            string savedImagePath = string.Empty;
            if (!_ProcessImageStorage(ref savedImagePath))
                return;

            _book.Title = txtTitle.Text.Trim();
            _book.ISBN = txtISBN.Text.Trim();
            _book.AuthorID = authorID;
            _book.GenreID = genreID;
            _book.PublicationYear = (int)nudPublicationYear.Value;
            _book.ImagePath = savedImagePath;

            if (_mode == enMode.AddNew)
            {
                _book.InitialCopies = (int)nudInitialCopies.Value;
            }

            try
            {
                if (_book.Save())
                {
                    lblBookID.Text = _book.BookID.ToString();
                    lblTitle.Text = "Update Book";
                    this.Text = "Update Book";
                    _mode = enMode.Update;
                    nudInitialCopies.Visible = false;
                    lblInitialCopies.Visible = false;
                    _tempSourceImagePath = null;
                    _imageRemoved = false;
                    _isSaved = true;

                    MessageBox.Show("Book saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataBack?.Invoke(_book.BookID);
                }
                else
                {
                    MessageBox.Show("Failed to save book record.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation / Business Rule Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // Only warn if they actually typed something and haven't saved
            if (!_isSaved && (!string.IsNullOrWhiteSpace(txtTitle.Text) || !string.IsNullOrWhiteSpace(txtISBN.Text)))
            {
                if (MessageBox.Show("You have unsaved changes. Are you sure you want to close this window?", 
                    "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Book title cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
            }
        }

        private void txtISBN_Validating(object sender, CancelEventArgs e)
        {
            string isbn = txtISBN.Text.Trim();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                errorProvider1.SetError(txtISBN, "ISBN cannot be empty.");
                return;
            }

            if (_mode == enMode.AddNew || (_mode == enMode.Update && _book != null && _book.ISBN != isbn))
            {
                if (clsBook.isBookExist(isbn))
                {
                    errorProvider1.SetError(txtISBN, "This ISBN is already assigned to another book.");
                    return;
                }
            }

            errorProvider1.SetError(txtISBN, "");
        }
    }
}
