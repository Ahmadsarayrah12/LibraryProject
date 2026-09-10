using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmAddUpdateBook : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        private int _bookID = -1;
        private clsBook _book;
        private string _tempSourceImagePath = null;
        private bool _imageRemoved = false;

        public int SavedBookID { get; private set; } = -1;

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
                txtTitle.Text = "";
                txtISBN.Text = "";
                nudInitialCopies.Value = 1;
                nudInitialCopies.Visible = true;
                lblInitialCopies.Visible = true;
                _tempSourceImagePath = null;
                _imageRemoved = false;

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
                MessageBox.Show("Book with ID [" + _bookID + "] was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblBookID.Text = _book.BookID.ToString();
            txtTitle.Text = _book.Title;
            txtISBN.Text = _book.ISBN;
            
            if (_book.PublicationYear >= nudPublicationYear.Minimum && _book.PublicationYear <= nudPublicationYear.Maximum)
                nudPublicationYear.Value = _book.PublicationYear;

            cbAuthors.SelectedValue = _book.AuthorID;
            cbGenres.SelectedValue = _book.GenreID;

            _LoadCoverImage(_book.ImagePath);
        }

        private void _LoadCoverImage(string imagePath)
        {
            if (imagePath != "")
            {
                if (File.Exists(imagePath))
                {
                    pbCover.Load(imagePath);
                }
                else
                {
                    pbCover.Image = null;
                }
            }
            else
            {
                pbCover.Image = null;
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
                string sourceFile = openFileDialog1.FileName;
                pbCover.Load(sourceFile);
                _tempSourceImagePath = sourceFile;
                _imageRemoved = false;
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            pbCover.Image = null;
            _tempSourceImagePath = null;
            _imageRemoved = true;
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            frmManageAuthors frm = new frmManageAuthors();
            frm.ShowDialog();
            _FillAuthorsComboBox();
        }

        private string _ProcessImageStorage()
        {
            if (_imageRemoved)
            {
                return "";
            }

            if (_tempSourceImagePath != null && File.Exists(_tempSourceImagePath))
            {
                string destDirectory = @"C:\Library_Images\Books";
                if (!Directory.Exists(destDirectory))
                    Directory.CreateDirectory(destDirectory);

                string fileExt = Path.GetExtension(_tempSourceImagePath);
                string targetFileName = Guid.NewGuid().ToString() + fileExt;
                string destFile = Path.Combine(destDirectory, targetFileName);

                File.Copy(_tempSourceImagePath, destFile, true);
                return destFile;
            }

            return _book.ImagePath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Simple Explicit Validations
            if (txtTitle.Text.Trim() == "")
            {
                MessageBox.Show("Book title cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtISBN.Text.Trim() == "")
            {
                MessageBox.Show("ISBN cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbAuthors.SelectedValue == null)
            {
                MessageBox.Show("Please select an author.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbGenres.SelectedValue == null)
            {
                MessageBox.Show("Please select a genre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ISBN Uniqueness Validation
            if (_mode == enMode.AddNew || (_mode == enMode.Update && _book.ISBN != txtISBN.Text.Trim()))
            {
                if (clsBook.isBookExist(txtISBN.Text.Trim()))
                {
                    MessageBox.Show("This ISBN is already assigned to another book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string savedImagePath = _ProcessImageStorage();

            _book.Title = txtTitle.Text.Trim();
            _book.ISBN = txtISBN.Text.Trim();
            _book.AuthorID = (int)cbAuthors.SelectedValue;
            _book.GenreID = (int)cbGenres.SelectedValue;
            _book.PublicationYear = (int)nudPublicationYear.Value;
            _book.ImagePath = savedImagePath;

            if (_mode == enMode.AddNew)
            {
                _book.InitialCopies = (int)nudInitialCopies.Value;
            }

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
                
                this.SavedBookID = _book.BookID;

                MessageBox.Show("Book saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save book record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
