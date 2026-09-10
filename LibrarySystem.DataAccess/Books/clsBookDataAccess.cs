using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public static class clsBookDataAccess
    {
        public static bool GetBookInfoByID(int bookID, ref string title, ref string isbn,
            ref int publicationYear, ref int authorID, ref string authorName,
            ref int genreID, ref string genreName, ref string imagePath,
            ref int totalCopies, ref int availableCopies)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM v_BooksInfo WHERE BookID = @BookID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookID", bookID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    title = (string)reader["Title"];
                    isbn = (string)reader["ISBN"];
                    publicationYear = (int)reader["PublicationYear"];
                    authorID = (int)reader["AuthorID"];
                    authorName = (string)reader["AuthorName"];
                    genreID = (int)reader["GenreID"];
                    genreName = (string)reader["GenreName"];
                    
                    if (reader["ImagePath"] != DBNull.Value)
                        imagePath = (string)reader["ImagePath"];
                    else
                        imagePath = "";

                    totalCopies = (int)reader["TotalCopies"];
                    availableCopies = (int)reader["AvailableCopies"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetBookInfoByID(int bookID, ref string title, ref string isbn,
            ref int publicationYear, ref int authorID, ref int genreID, ref string imagePath)
        {
            string authorName = "";
            string genreName = "";
            int total = 0, available = 0;

            return GetBookInfoByID(bookID, ref title, ref isbn, ref publicationYear,
                ref authorID, ref authorName, ref genreID, ref genreName, ref imagePath,
                ref total, ref available);
        }

        public static bool GetBookInfoByISBN(string isbn, ref int bookID, ref string title,
            ref int publicationYear, ref int authorID, ref string authorName,
            ref int genreID, ref string genreName, ref string imagePath,
            ref int totalCopies, ref int availableCopies)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM v_BooksInfo WHERE ISBN = @ISBN";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ISBN", isbn);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    bookID = (int)reader["BookID"];
                    title = (string)reader["Title"];
                    publicationYear = (int)reader["PublicationYear"];
                    authorID = (int)reader["AuthorID"];
                    authorName = (string)reader["AuthorName"];
                    genreID = (int)reader["GenreID"];
                    genreName = (string)reader["GenreName"];
                    
                    if (reader["ImagePath"] != DBNull.Value)
                        imagePath = (string)reader["ImagePath"];
                    else
                        imagePath = "";

                    totalCopies = (int)reader["TotalCopies"];
                    availableCopies = (int)reader["AvailableCopies"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetBookInfoByISBN(string isbn, ref int bookID, ref string title,
            ref int publicationYear, ref int authorID, ref int genreID, ref string imagePath)
        {
            string authorName = "";
            string genreName = "";
            int total = 0, available = 0;

            return GetBookInfoByISBN(isbn, ref bookID, ref title, ref publicationYear,
                ref authorID, ref authorName, ref genreID, ref genreName, ref imagePath,
                ref total, ref available);
        }

        public static int AddNewBook(string title, string isbn, int publicationYear,
            int authorID, int genreID, string imagePath, int initialCopies = 1)
        {
            int bookID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            string query = @"
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

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@ISBN", isbn);
            command.Parameters.AddWithValue("@PublicationYear", publicationYear);
            command.Parameters.AddWithValue("@AuthorID", authorID);
            command.Parameters.AddWithValue("@GenreID", genreID);
            
            if (imagePath != "" && imagePath != null)
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                
            command.Parameters.AddWithValue("@InitialCopies", initialCopies);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    bookID = insertedID;
                }
            }
            catch (Exception ex)
            {
                bookID = -1;
            }
            finally
            {
                connection.Close();
            }

            return bookID;
        }

        public static bool UpdateBook(int bookID, string title, string isbn, int publicationYear,
            int authorID, int genreID, string imagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                UPDATE Books
                SET Title = @Title,
                    ISBN = @ISBN,
                    PublicationYear = @PublicationYear,
                    AuthorID = @AuthorID,
                    GenreID = @GenreID,
                    ImagePath = @ImagePath
                WHERE BookID = @BookID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BookID", bookID);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@ISBN", isbn);
            command.Parameters.AddWithValue("@PublicationYear", publicationYear);
            command.Parameters.AddWithValue("@AuthorID", authorID);
            command.Parameters.AddWithValue("@GenreID", genreID);
            
            if (imagePath != "" && imagePath != null)
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteBook(int bookID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Books WHERE BookID = @BookID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookID", bookID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllBooks()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM v_BooksInfo";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsBookExist(int bookID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Books WHERE BookID = @BookID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookID", bookID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsBookExist(string isbn)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Books WHERE ISBN = @ISBN";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ISBN", isbn);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        
        public static bool ProvisionBookCopies(int bookID, int additionalCopies)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                DECLARE @i INT = 0;
                WHILE @i < @AdditionalCopies
                BEGIN
                    INSERT INTO BookCopies (BookID, Status) VALUES (@BookID, 1);
                    SET @i = @i + 1;
                END;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookID", bookID);
            command.Parameters.AddWithValue("@AdditionalCopies", additionalCopies);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
    }
}

