using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Pure ADO.NET Data Access Layer for Books table operations.
    /// Manages core catalog metadata with parameterized queries, single-roundtrip projections,
    /// and transactional batch copy provisioning for enterprise-grade performance.
    /// </summary>
    public static class clsBookDataAccess
    {
        /// <summary>
        /// Retrieves complete aggregated book metadata (including AuthorName, GenreName, and live copy counts)
        /// in a single database roundtrip.
        /// </summary>
        public static bool GetBookInfoByID(int bookID, ref string title, ref string isbn,
            ref int publicationYear, ref int authorID, ref string authorName,
            ref int genreID, ref string genreName, ref string imagePath,
            ref int totalCopies, ref int availableCopies)
        {
            bool isFound = false;

            const string query = @"
                SELECT 
                    Title, ISBN, PublicationYear, AuthorID, AuthorName,
                    GenreID, GenreName, ImagePath, TotalCopies, AvailableCopies
                FROM v_BooksInfo
                WHERE BookID = @BookID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            title = reader.SafeGetString("Title");
                            isbn = reader.SafeGetString("ISBN");
                            publicationYear = reader.SafeGetInt("PublicationYear");
                            authorID = reader.SafeGetInt("AuthorID");
                            authorName = reader.SafeGetString("AuthorName");
                            genreID = reader.SafeGetInt("GenreID");
                            genreName = reader.SafeGetString("GenreName");
                            imagePath = reader.SafeGetString("ImagePath");
                            totalCopies = reader.SafeGetInt("TotalCopies");
                            availableCopies = reader.SafeGetInt("AvailableCopies");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetBookInfoByID));
            }

            return isFound;
        }

        /// <summary>
        /// Backward-compatible overload for retrieving core book columns.
        /// </summary>
        public static bool GetBookInfoByID(int bookID, ref string title, ref string isbn,
            ref int publicationYear, ref int authorID, ref int genreID, ref string imagePath)
        {
            string authorName = string.Empty;
            string genreName = string.Empty;
            int total = 0, available = 0;

            return GetBookInfoByID(bookID, ref title, ref isbn, ref publicationYear,
                ref authorID, ref authorName, ref genreID, ref genreName, ref imagePath,
                ref total, ref available);
        }

        /// <summary>
        /// Retrieves complete aggregated book metadata by unique ISBN in a single database roundtrip.
        /// </summary>
        public static bool GetBookInfoByISBN(string isbn, ref int bookID, ref string title,
            ref int publicationYear, ref int authorID, ref string authorName,
            ref int genreID, ref string genreName, ref string imagePath,
            ref int totalCopies, ref int availableCopies)
        {
            bool isFound = false;

            const string query = @"
                SELECT 
                    BookID, Title, PublicationYear, AuthorID, AuthorName,
                    GenreID, GenreName, ImagePath, TotalCopies, AvailableCopies
                FROM v_BooksInfo
                WHERE ISBN = @ISBN;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ISBN", SqlDbType.NVarChar, 50).Value = isbn;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            bookID = reader.SafeGetInt("BookID");
                            title = reader.SafeGetString("Title");
                            publicationYear = reader.SafeGetInt("PublicationYear");
                            authorID = reader.SafeGetInt("AuthorID");
                            authorName = reader.SafeGetString("AuthorName");
                            genreID = reader.SafeGetInt("GenreID");
                            genreName = reader.SafeGetString("GenreName");
                            imagePath = reader.SafeGetString("ImagePath");
                            totalCopies = reader.SafeGetInt("TotalCopies");
                            availableCopies = reader.SafeGetInt("AvailableCopies");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetBookInfoByISBN));
            }

            return isFound;
        }

        /// <summary>
        /// Backward-compatible overload for retrieving book by ISBN.
        /// </summary>
        public static bool GetBookInfoByISBN(string isbn, ref int bookID, ref string title,
            ref int publicationYear, ref int authorID, ref int genreID, ref string imagePath)
        {
            string authorName = string.Empty;
            string genreName = string.Empty;
            int total = 0, available = 0;

            return GetBookInfoByISBN(isbn, ref bookID, ref title, ref publicationYear,
                ref authorID, ref authorName, ref genreID, ref genreName, ref imagePath,
                ref total, ref available);
        }

        /// <summary>
        /// Inserts a new book into the database and provisions initial copies atomically inside a single transaction.
        /// </summary>
        public static int AddNewBook(string title, string isbn, int publicationYear,
            int authorID, int genreID, string imagePath, int initialCopies = 1)
        {
            int bookID = -1;

            const string query = @"
                BEGIN TRANSACTION;
                INSERT INTO Books (Title, ISBN, PublicationYear, AuthorID, GenreID, ImagePath)
                VALUES (@Title, @ISBN, @PublicationYear, @AuthorID, @GenreID, @ImagePath);
                DECLARE @NewBookID INT = SCOPE_IDENTITY();

                DECLARE @i INT = 0;
                WHILE @i < @InitialCopies
                BEGIN
                    INSERT INTO BookCopies (BookID, Status) VALUES (@NewBookID, 1);
                    SET @i = @i + 1;
                END;
                COMMIT TRANSACTION;
                SELECT @NewBookID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 250).Value = title;
                    command.Parameters.Add("@ISBN", SqlDbType.NVarChar, 50).Value = isbn;
                    command.Parameters.Add("@PublicationYear", SqlDbType.Int).Value = publicationYear;
                    command.Parameters.Add("@AuthorID", SqlDbType.Int).Value = authorID;
                    command.Parameters.Add("@GenreID", SqlDbType.Int).Value = genreID;
                    command.Parameters.AddWithNullableString("@ImagePath", imagePath);
                    command.Parameters.Add("@InitialCopies", SqlDbType.Int).Value = Math.Max(1, initialCopies);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        bookID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(AddNewBook));
            }

            return bookID;
        }

        /// <summary>
        /// Updates an existing book catalog record.
        /// </summary>
        public static bool UpdateBook(int bookID, string title, string isbn, int publicationYear,
            int authorID, int genreID, string imagePath)
        {
            int rowsAffected = 0;

            const string query = @"
                UPDATE Books
                SET Title = @Title,
                    ISBN = @ISBN,
                    PublicationYear = @PublicationYear,
                    AuthorID = @AuthorID,
                    GenreID = @GenreID,
                    ImagePath = @ImagePath
                WHERE BookID = @BookID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 250).Value = title;
                    command.Parameters.Add("@ISBN", SqlDbType.NVarChar, 50).Value = isbn;
                    command.Parameters.Add("@PublicationYear", SqlDbType.Int).Value = publicationYear;
                    command.Parameters.Add("@AuthorID", SqlDbType.Int).Value = authorID;
                    command.Parameters.Add("@GenreID", SqlDbType.Int).Value = genreID;
                    command.Parameters.AddWithNullableString("@ImagePath", imagePath);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(UpdateBook));
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a book by ID (cascades to BookCopies).
        /// </summary>
        public static bool DeleteBook(int bookID)
        {
            int rowsAffected = 0;

            const string query = "DELETE FROM Books WHERE BookID = @BookID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(DeleteBook));
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Checks if a book exists with the specified ISBN.
        /// </summary>
        public static bool IsBookExistByISBN(string isbn)
        {
            bool isFound = false;

            const string query = "SELECT 1 FROM Books WHERE ISBN = @ISBN;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ISBN", SqlDbType.NVarChar, 50).Value = isbn;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(IsBookExistByISBN));
            }

            return isFound;
        }

        /// <summary>
        /// Checks if a book exists with the specified BookID.
        /// </summary>
        public static bool IsBookExistByID(int bookID)
        {
            bool isFound = false;

            const string query = "SELECT 1 FROM Books WHERE BookID = @BookID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(IsBookExistByID));
            }

            return isFound;
        }

        /// <summary>
        /// Returns all books aggregated from v_BooksInfo view.
        /// </summary>
        public static DataTable GetAllBooks()
        {
            DataTable dt = new DataTable();

            const string query = @"
                SELECT 
                    BookID,
                    Title,
                    ISBN,
                    PublicationYear,
                    AuthorID,
                    AuthorName,
                    GenreID,
                    GenreName,
                    ImagePath,
                    TotalCopies,
                    AvailableCopies
                FROM v_BooksInfo
                ORDER BY BookID DESC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetAllBooks));
            }

            return dt;
        }
    }
}
