using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Core domain model representing a book catalog entry.
    /// Enforces business invariants, domain composition with Author/Genre,
    /// atomic transactional batch copy provisioning, and single-roundtrip projections.
    /// </summary>
    public class clsBook
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        private int _totalCopies = 0;
        private int _availableCopies = 0;

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
        public int TotalCopies => _totalCopies;

        /// <summary>
        /// Count of physical copies currently available for loan.
        /// </summary>
        public int AvailableCopies => _availableCopies;

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
            this._totalCopies = 0;
            this._availableCopies = 0;
            this._mode = enMode.AddNew;
        }

        private clsBook(int bookID, string title, string isbn, int publicationYear,
            int authorID, string authorName, int genreID, string genreName,
            string imagePath, int totalCopies, int availableCopies)
        {
            this.BookID = bookID;
            this.Title = title;
            this.ISBN = isbn;
            this.PublicationYear = publicationYear;
            this.AuthorID = authorID;
            this.GenreID = genreID;
            this.ImagePath = imagePath;
            this.InitialCopies = 0;
            this._totalCopies = totalCopies;
            this._availableCopies = availableCopies;

            // Pre-hydrate navigation objects directly to eliminate N+1 database queries
            if (authorID > 0)
            {
                this._authorInfo = new clsAuthor();
                typeof(clsAuthor).GetProperty("AuthorID")?.SetValue(this._authorInfo, authorID);
                this._authorInfo.FullName = authorName;
            }

            if (genreID > 0)
            {
                this._genreInfo = new clsGenre();
                typeof(clsGenre).GetProperty("GenreID")?.SetValue(this._genreInfo, genreID);
                this._genreInfo.GenreName = genreName;
            }

            this._mode = enMode.Update;
        }

        /// <summary>
        /// Finds a book by primary key BookID in a single high-performance roundtrip.
        /// </summary>
        public static clsBook Find(int bookID)
        {
            string title = string.Empty;
            string isbn = string.Empty;
            int publicationYear = 0;
            int authorID = -1;
            string authorName = string.Empty;
            int genreID = -1;
            string genreName = string.Empty;
            string imagePath = string.Empty;
            int totalCopies = 0;
            int availableCopies = 0;

            if (clsBookDataAccess.GetBookInfoByID(bookID, ref title, ref isbn,
                ref publicationYear, ref authorID, ref authorName, ref genreID, ref genreName,
                ref imagePath, ref totalCopies, ref availableCopies))
            {
                return new clsBook(bookID, title, isbn, publicationYear, authorID, authorName,
                    genreID, genreName, imagePath, totalCopies, availableCopies);
            }

            return null;
        }

        /// <summary>
        /// Finds a book by unique ISBN in a single high-performance roundtrip.
        /// </summary>
        public static clsBook Find(string isbn)
        {
            int bookID = -1;
            string title = string.Empty;
            int publicationYear = 0;
            int authorID = -1;
            string authorName = string.Empty;
            int genreID = -1;
            string genreName = string.Empty;
            string imagePath = string.Empty;
            int totalCopies = 0;
            int availableCopies = 0;

            if (clsBookDataAccess.GetBookInfoByISBN(isbn, ref bookID, ref title,
                ref publicationYear, ref authorID, ref authorName, ref genreID, ref genreName,
                ref imagePath, ref totalCopies, ref availableCopies))
            {
                return new clsBook(bookID, title, isbn, publicationYear, authorID, authorName,
                    genreID, genreName, imagePath, totalCopies, availableCopies);
            }

            return null;
        }

        private bool _AddNewBook()
        {
            // Atomically inserts book and initial copies within a single SQL transaction
            this.BookID = clsBookDataAccess.AddNewBook(
                this.Title,
                this.ISBN,
                this.PublicationYear,
                this.AuthorID,
                this.GenreID,
                this.ImagePath,
                this.InitialCopies);

            if (this.BookID != -1)
            {
                this._totalCopies = Math.Max(1, this.InitialCopies);
                this._availableCopies = this._totalCopies;
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
        /// Adds additional physical inventory copies for this book in a single batch roundtrip.
        /// </summary>
        public bool AddCopies(int count)
        {
            if (this.BookID <= 0 || count <= 0)
                return false;

            if (clsBookCopy.AddCopies(this.BookID, count))
            {
                this._totalCopies += count;
                this._availableCopies += count;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Refreshes the copy counts from storage.
        /// </summary>
        public void RefreshCopiesCount()
        {
            if (this.BookID <= 0) return;
            int total = 0, available = 0;
            if (clsBookCopy.GetCopiesCount(this.BookID, ref total, ref available))
            {
                this._totalCopies = total;
                this._availableCopies = available;
            }
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
