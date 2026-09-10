using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsBook
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AuthorID { get; set; }
        public int GenreID { get; set; }
        public string ImagePath { get; set; }
        public int InitialCopies { get; set; }

        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }

        public clsAuthor AuthorInfo { get; set; }
        public clsGenre GenreInfo { get; set; }

        public clsBook()
        {
            this.BookID = -1;
            this.Title = "";
            this.ISBN = "";
            this.PublicationYear = DateTime.Now.Year;
            this.AuthorID = -1;
            this.GenreID = -1;
            this.ImagePath = "";
            this.InitialCopies = 1;
            this.TotalCopies = 0;
            this.AvailableCopies = 0;
            
            Mode = enMode.AddNew;
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
            this.TotalCopies = totalCopies;
            this.AvailableCopies = availableCopies;

            this.AuthorInfo = clsAuthor.Find(authorID);
            this.GenreInfo = clsGenre.Find(genreID);

            Mode = enMode.Update;
        }

        private bool _AddNewBook()
        {
            this.BookID = clsBookDataAccess.AddNewBook(this.Title, this.ISBN, this.PublicationYear, this.AuthorID, this.GenreID, this.ImagePath, this.InitialCopies);
            return (this.BookID != -1);
        }

        private bool _UpdateBook()
        {
            return clsBookDataAccess.UpdateBook(this.BookID, this.Title, this.ISBN, this.PublicationYear, this.AuthorID, this.GenreID, this.ImagePath);
        }

        public static clsBook Find(int bookID)
        {
            string title = "";
            string isbn = "";
            int publicationYear = 0;
            int authorID = -1;
            string authorName = "";
            int genreID = -1;
            string genreName = "";
            string imagePath = "";
            int totalCopies = 0;
            int availableCopies = 0;

            if (clsBookDataAccess.GetBookInfoByID(bookID, ref title, ref isbn, ref publicationYear, ref authorID, ref authorName, ref genreID, ref genreName, ref imagePath, ref totalCopies, ref availableCopies))
            {
                return new clsBook(bookID, title, isbn, publicationYear, authorID, authorName, genreID, genreName, imagePath, totalCopies, availableCopies);
            }
            else
            {
                return null;
            }
        }

        public static clsBook FindByISBN(string isbn)
        {
            int bookID = -1;
            string title = "";
            int publicationYear = 0;
            int authorID = -1;
            string authorName = "";
            int genreID = -1;
            string genreName = "";
            string imagePath = "";
            int totalCopies = 0;
            int availableCopies = 0;

            if (clsBookDataAccess.GetBookInfoByISBN(isbn, ref bookID, ref title, ref publicationYear, ref authorID, ref authorName, ref genreID, ref genreName, ref imagePath, ref totalCopies, ref availableCopies))
            {
                return new clsBook(bookID, title, isbn, publicationYear, authorID, authorName, genreID, genreName, imagePath, totalCopies, availableCopies);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBook())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateBook();
            }
            return false;
        }

        public static DataTable GetAllBooks()
        {
            return clsBookDataAccess.GetAllBooks();
        }

        public static bool DeleteBook(int bookID)
        {
            return clsBookDataAccess.DeleteBook(bookID);
        }

        public static bool isBookExist(int bookID)
        {
            return clsBookDataAccess.IsBookExist(bookID);
        }

        public static bool isBookExist(string isbn)
        {
            return clsBookDataAccess.IsBookExist(isbn);
        }

        public bool ProvisionCopies(int additionalCopies)
        {
            if (additionalCopies <= 0) return false;
            
            bool result = clsBookDataAccess.ProvisionBookCopies(this.BookID, additionalCopies);
            
            if (result)
            {
                this.TotalCopies += additionalCopies;
                this.AvailableCopies += additionalCopies;
            }
            
            return result;
        }
    }
}
