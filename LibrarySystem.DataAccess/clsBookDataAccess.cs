using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Pure ADO.NET Data Access Layer for Books table operations.
    /// Manages core catalog metadata with parameterized queries and safe DBNull conversions.
    /// </summary>
    public static class clsBookDataAccess
    {
        /// <summary>
        /// Retrieves a book record by its primary key.
        /// </summary>
        public static bool GetBookInfoByID(int bookID, ref string title, ref string isbn,
            ref int publicationYear, ref int authorID, ref int genreID, ref string imagePath)
        {
            bool isFound = false;

            const string query = @"
                SELECT Title, ISBN, PublicationYear, AuthorID, GenreID, ImagePath
                FROM Books
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
                            genreID = reader.SafeGetInt("GenreID");
                            imagePath = reader.SafeGetString("ImagePath");
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
        /// Retrieves a book record by its unique ISBN.
        /// </summary>
        public static bool GetBookInfoByISBN(string isbn, ref int bookID, ref string title,
            ref int publicationYear, ref int authorID, ref int genreID, ref string imagePath)
        {
            bool isFound = false;

            const string query = @"
                SELECT BookID, Title, PublicationYear, AuthorID, GenreID, ImagePath
                FROM Books
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
                            genreID = reader.SafeGetInt("GenreID");
                            imagePath = reader.SafeGetString("ImagePath");
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
        /// Inserts a new book into the database and returns the generated BookID.
        /// </summary>
        public static int AddNewBook(string title, string isbn, int publicationYear,
            int authorID, int genreID, string imagePath)
        {
            int bookID = -1;

            const string query = @"
                INSERT INTO Books (Title, ISBN, PublicationYear, AuthorID, GenreID, ImagePath)
                VALUES (@Title, @ISBN, @PublicationYear, @AuthorID, @GenreID, @ImagePath);
                SELECT SCOPE_IDENTITY();";

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
