using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Reusable presentation UserControl displaying catalog information and live inventory copy counts.
    /// Equipped with DesignMode guards and decoupled data binding.
    /// </summary>
    public partial class ctrlBookCard : UserControl
    {
        private int _bookID = -1;
        private clsBook _book;

        public int BookID => _bookID;
        public clsBook SelectedBook => _book;

        public ctrlBookCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Resets all visual labels and picture box to default placeholder states.
        /// </summary>
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

            if (pbCover.Image != null)
            {
                pbCover.Image.Dispose();
                pbCover.Image = null;
            }
        }

        private void _LoadBookData()
        {
            _bookID = _book.BookID;

            lblBookID.Text = _book.BookID.ToString();
            lblTitle.Text = _book.Title;
            lblAuthor.Text = _book.AuthorInfo?.FullName ?? $"Author #{_book.AuthorID}";
            lblGenre.Text = _book.GenreInfo?.GenreName ?? $"Genre #{_book.GenreID}";
            lblISBN.Text = _book.ISBN;
            lblPublicationYear.Text = _book.PublicationYear.ToString();
            lblTotalCopies.Text = _book.TotalCopies.ToString();
            lblAvailableCopies.Text = _book.AvailableCopies.ToString();

            _LoadCoverImage();
        }

        private void _LoadCoverImage()
        {
            if (pbCover.Image != null)
            {
                pbCover.Image.Dispose();
                pbCover.Image = null;
            }

            if (!string.IsNullOrWhiteSpace(_book.ImagePath) && File.Exists(_book.ImagePath))
            {
                try
                {
                    using (var stream = new FileStream(_book.ImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbCover.Image = Image.FromStream(stream);
                    }
                }
                catch
                {
                    pbCover.Image = null;
                }
            }
            else
            {
                pbCover.Image = null;
            }
        }

        /// <summary>
        /// Loads book metadata by BookID primary key.
        /// </summary>
        public bool LoadBookInfo(int bookID)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return false;

            _book = clsBook.Find(bookID);

            if (_book == null)
            {
                ResetBookInfo();
                return false;
            }

            _LoadBookData();
            return true;
        }

        /// <summary>
        /// Loads book metadata by ISBN.
        /// </summary>
        public bool LoadBookInfo(string isbn)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return false;

            _book = clsBook.Find(isbn);

            if (_book == null)
            {
                ResetBookInfo();
                return false;
            }

            _LoadBookData();
            return true;
        }
    }
}
