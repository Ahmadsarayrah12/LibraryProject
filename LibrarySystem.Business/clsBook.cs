using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Core domain model representing a book catalog entry.
    /// Enforces business invariants, domain composition with Author/Genre,
    /// and automatic provisioning of physical inventory copies.
    /// </summary>
    public class clsBook
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        public int BookID { get; private set; } = -1;
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; } = DateTime.Now.Year;
        public int AuthorID { get; set; } = -1;
        public int GenreID { get; set; } = -1;
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// Initial physical inventory copies to provision when creating a new book record.
        /// </summary>
        public int InitialCopies { get; set; } = 1;

        public enMode Mode => _mode;

        private clsAuthor _authorInfo;
        public clsAuthor AuthorInfo
        {
            get
            {
                if (_authorInfo == null && this.AuthorID > 0)
                {
                    _authorInfo = clsAuthor.Find(this.AuthorID);
                }
                return _authorInfo;
            }
            set => _authorInfo = value;
        }

        private clsGenre _genreInfo;
        public clsGenre GenreInfo
        {
            get
            {
                if (_genreInfo == null && this.GenreID > 0)
                {
                    _genreInfo = clsGenre.Find(this.GenreID);
                }
                return _genreInfo;
            }
            set => _genreInfo = value;
        }

        /// <summary>
        /// Total count of physical copies registered for this book.
        /// </summary>
        public int TotalCopies
        {
            get
            {
                if (this.BookID <= 0) return 0;
                int total = 0, available = 0;
                clsBookCopy.GetCopiesCount(this.BookID, ref total, ref available);
                return total;
            }
        }

        /// <summary>
        /// Count of physical copies currently available for loan.
        /// </summary>
        public int AvailableCopies
        {
            get
            {
                if (this.BookID <= 0) return 0;
                int total = 0, available = 0;
                clsBookCopy.GetCopiesCount(this.BookID, ref total, ref available);
                return available;
            }
        }

        /// <summary>
        /// Initializes a new book instance in AddNew mode.
        /// </summary>
        public clsBook()
        {
            this.BookID = -1;
            this.Title = string.Empty;
            this.ISBN = string.Empty;
            this.PublicationYear = DateTime.Now.Year;
            this.AuthorID = -1;
            this.GenreID = -1;
            this.ImagePath = string.Empty;
            this.InitialCopies = 1;
            this._mode = enMode.AddNew;
        }

        private clsBook(int bookID, string title, string isbn, int publicationYear,
            int authorID, int genreID, string imagePath)
        {
            this.BookID = bookID;
            this.Title = title;
            this.ISBN = isbn;
            this.PublicationYear = publicationYear;
            this.AuthorID = authorID;
            this.GenreID = genreID;
            this.ImagePath = imagePath;
            this.InitialCopies = 0;
            this._mode = enMode.Update;
        }

        /// <summary>
        /// Finds a book by primary key BookID.
        /// </summary>
        public static clsBook Find(int bookID)
        {
            string title = string.Empty;
            string isbn = string.Empty;
            int publicationYear = 0;
            int authorID = -1;
            int genreID = -1;
            string imagePath = string.Empty;

            if (clsBookDataAccess.GetBookInfoByID(bookID, ref title, ref isbn,
                ref publicationYear, ref authorID, ref genreID, ref imagePath))
            {
                return new clsBook(bookID, title, isbn, publicationYear, authorID, genreID, imagePath);
            }

            return null;
        }

        /// <summary>
        /// Finds a book by unique ISBN.
        /// </summary>
        public static clsBook Find(string isbn)
        {
            int bookID = -1;
            string title = string.Empty;
            int publicationYear = 0;
            int authorID = -1;
            int genreID = -1;
            string imagePath = string.Empty;

            if (clsBookDataAccess.GetBookInfoByISBN(isbn, ref bookID, ref title,
                ref publicationYear, ref authorID, ref genreID, ref imagePath))
            {
                return new clsBook(bookID, title, isbn, publicationYear, authorID, genreID, imagePath);
            }

            return null;
        }

        private bool _AddNewBook()
        {
            this.BookID = clsBookDataAccess.AddNewBook(
                this.Title,
                this.ISBN,
                this.PublicationYear,
                this.AuthorID,
                this.GenreID,
                this.ImagePath);

            if (this.BookID != -1)
            {
                // Auto-provision initial physical copies
                int countToProvision = Math.Max(1, this.InitialCopies);
                for (int i = 0; i < countToProvision; i++)
                {
                    clsBookCopyDataAccess.AddNewCopy(this.BookID, (byte)clsBookCopy.enCopyStatus.Available);
                }
                return true;
            }

            return false;
        }

        private bool _UpdateBook()
        {
            return clsBookDataAccess.UpdateBook(
                this.BookID,
                this.Title,
                this.ISBN,
                this.PublicationYear,
                this.AuthorID,
                this.GenreID,
                this.ImagePath);
        }

        /// <summary>
        /// Validates invariants and persists book catalog data.
        /// </summary>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new InvalidOperationException("Book title cannot be empty.");

            if (string.IsNullOrWhiteSpace(this.ISBN))
                throw new InvalidOperationException("Book ISBN cannot be empty.");

            if (this.AuthorID <= 0)
                throw new InvalidOperationException("A valid author must be selected.");

            if (this.GenreID <= 0)
                throw new InvalidOperationException("A valid genre must be selected.");

            if (this.PublicationYear < 1000 || this.PublicationYear > DateTime.Now.Year + 1)
                throw new InvalidOperationException($"Publication year must be between 1000 and {DateTime.Now.Year + 1}.");

            switch (_mode)
            {
                case enMode.AddNew:
                    if (IsBookExist(this.ISBN))
                        throw new InvalidOperationException($"A book with ISBN [{this.ISBN}] already exists in the catalog.");

                    if (_AddNewBook())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateBook();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Adds additional physical inventory copies for this book.
        /// </summary>
        public bool AddCopies(int count)
        {
            if (this.BookID <= 0 || count <= 0)
                return false;

            bool allSuccess = true;
            for (int i = 0; i < count; i++)
            {
                int copyID = clsBookCopyDataAccess.AddNewCopy(this.BookID, (byte)clsBookCopy.enCopyStatus.Available);
                if (copyID == -1)
                    allSuccess = false;
            }

            return allSuccess;
        }

        /// <summary>
        /// Deletes a book catalog entry. Prevents deletion if active loans exist.
        /// </summary>
        public static bool Delete(int bookID)
        {
            if (clsBookCopy.HasActiveBorrowings(bookID))
                throw new InvalidOperationException("Cannot delete book while active unreturned borrowings exist.");

            return clsBookDataAccess.DeleteBook(bookID);
        }

        /// <summary>
        /// Checks if a book exists with the given ISBN.
        /// </summary>
        public static bool IsBookExist(string isbn)
        {
            return clsBookDataAccess.IsBookExistByISBN(isbn);
        }

        /// <summary>
        /// Checks if a book exists with the given BookID.
        /// </summary>
        public static bool IsBookExist(int bookID)
        {
            return clsBookDataAccess.IsBookExistByID(bookID);
        }

        /// <summary>
        /// Returns all books aggregated with author name, genre name, and copy counts.
        /// </summary>
        public static DataTable GetAllBooks()
        {
            return clsBookDataAccess.GetAllBooks();
        }
    }
}
