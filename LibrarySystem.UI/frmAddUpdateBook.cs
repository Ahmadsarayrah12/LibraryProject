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
    /// Handles cover image persistence to local storage and auto-provisions initial copies.
    /// </summary>
    public partial class frmAddUpdateBook : Form
    {
        public delegate void DataBackHandler(int bookID);
        public event DataBackHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        private int _bookID = -1;
        private clsBook _book;
        private string _selectedImagePath = string.Empty;

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
            cbAuthors.DataSource = dt;
            cbAuthors.DisplayMember = "FullName";
            cbAuthors.ValueMember = "AuthorID";

            if (cbAuthors.Items.Count > 0)
                cbAuthors.SelectedIndex = 0;
        }

        private void _FillGenresComboBox()
        {
            DataTable dt = clsGenre.GetAllGenres();
            cbGenres.DataSource = dt;
            cbGenres.DisplayMember = "GenreName";
            cbGenres.ValueMember = "GenreID";

            if (cbGenres.Items.Count > 0)
                cbGenres.SelectedIndex = 0;
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
                _selectedImagePath = string.Empty;
                pbCover.Image = null;
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
            nudPublicationYear.Value = _book.PublicationYear;

            cbAuthors.SelectedValue = _book.AuthorID;
            cbGenres.SelectedValue = _book.GenreID;

            _selectedImagePath = _book.ImagePath;

            _LoadCoverImage();
        }

        private void _LoadCoverImage()
        {
            if (pbCover.Image != null)
            {
                pbCover.Image.Dispose();
                pbCover.Image = null;
            }

            if (!string.IsNullOrWhiteSpace(_selectedImagePath))
            {
                string resolvedPath = Path.IsPathRooted(_selectedImagePath)
                    ? _selectedImagePath
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _selectedImagePath);

                if (File.Exists(resolvedPath))
                {
                    try
                    {
                        using (var stream = new FileStream(resolvedPath, FileMode.Open, FileAccess.Read))
                        {
                            pbCover.Image = Image.FromStream(stream);
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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string sourceFile = openFileDialog1.FileName;
                    string destDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Library_Images", "Books");

                    if (!Directory.Exists(destDirectory))
                        Directory.CreateDirectory(destDirectory);

                    string fileExt = Path.GetExtension(sourceFile);
                    string targetFileName = Guid.NewGuid().ToString() + fileExt;
                    string destFile = Path.Combine(destDirectory, targetFileName);

                    File.Copy(sourceFile, destFile, true);

                    // Store relative path
                    _selectedImagePath = Path.Combine("Library_Images", "Books", targetFileName);
                    _LoadCoverImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to copy image: {ex.Message}", "Image Error",
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
            _selectedImagePath = string.Empty;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct validation errors before proceeding.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _book.Title = txtTitle.Text.Trim();
            _book.ISBN = txtISBN.Text.Trim();
            _book.AuthorID = (int)cbAuthors.SelectedValue;
            _book.GenreID = (int)cbGenres.SelectedValue;
            _book.PublicationYear = (int)nudPublicationYear.Value;
            _book.ImagePath = _selectedImagePath;

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

            if (_mode == enMode.AddNew || (_mode == enMode.Update && _book.ISBN != isbn))
            {
                if (clsBook.IsBookExist(isbn))
                {
                    errorProvider1.SetError(txtISBN, "This ISBN is already assigned to another book.");
                    return;
                }
            }

            errorProvider1.SetError(txtISBN, "");
        }
    }
}
