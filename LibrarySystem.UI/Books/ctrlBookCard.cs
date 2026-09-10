using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class ctrlBookCard : UserControl
    {
        private int _bookID = -1;
        private clsBook _book;

        public int BookID
        {
            get { return _bookID; }
        }

        public clsBook SelectedBook
        {
            get { return _book; }
        }

        public ctrlBookCard()
        {
            InitializeComponent();
        }

        public void ResetBookInfo()
        {
            _bookID = -1;
            _book = null;

            lblBookID.Text = "[???]";
            lblTitle.Text = "[???]";
            lblAuthor.Text = "[???]";
            lblGenre.Text = "[???]";
            lblISBN.Text = "[???]";
            lblPublicationYear.Text = "[???]";
            lblTotalCopies.Text = "[???]";
            lblAvailableCopies.Text = "[???]";

            pbCover.Image = null;
        }

        private void _LoadBookData()
        {
            _bookID = _book.BookID;

            lblBookID.Text = _book.BookID.ToString();
            lblTitle.Text = _book.Title;
            lblISBN.Text = _book.ISBN;
            lblPublicationYear.Text = _book.PublicationYear.ToString();
            lblTotalCopies.Text = _book.TotalCopies.ToString();
            lblAvailableCopies.Text = _book.AvailableCopies.ToString();

            if (_book.AuthorInfo != null)
                lblAuthor.Text = _book.AuthorInfo.FullName;
            else
                lblAuthor.Text = "Unknown";

            if (_book.GenreInfo != null)
                lblGenre.Text = _book.GenreInfo.GenreName;
            else
                lblGenre.Text = "Unknown";

            _LoadCoverImage();
        }

        private void _LoadCoverImage()
        {
            if (_book.ImagePath != "")
            {
                if (File.Exists(_book.ImagePath))
                {
                    pbCover.Load(_book.ImagePath);
                }
                else
                {
                    pbCover.Image = null; // Can put a default image here
                }
            }
            else
            {
                pbCover.Image = null;
            }
        }

        public void LoadBookInfo(int bookID)
        {
            _book = clsBook.Find(bookID);

            if (_book == null)
            {
                ResetBookInfo();
                MessageBox.Show("No Book with BookID = " + bookID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadBookData();
        }

        public void LoadBookInfo(string isbn)
        {
            _book = clsBook.FindByISBN(isbn);

            if (_book == null)
            {
                ResetBookInfo();
                MessageBox.Show("No Book with ISBN = " + isbn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadBookData();
        }
    }
}
