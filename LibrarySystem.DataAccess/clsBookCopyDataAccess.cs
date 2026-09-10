using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Pure ADO.NET Data Access Layer for BookCopies table operations.
    /// Tracks individual physical inventory copies and their statuses.
    /// </summary>
    public static class clsBookCopyDataAccess
    {
        /// <summary>
        /// Inserts a new copy for a book and returns the generated CopyID.
        /// </summary>
        public static int AddNewCopy(int bookID, byte status)
        {
            int copyID = -1;

            const string query = @"
                INSERT INTO BookCopies (BookID, Status)
                VALUES (@BookID, @Status);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = status;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        copyID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(AddNewCopy));
            }

            return copyID;
        }

        /// <summary>
        /// Inserts multiple copies in a single high-speed database roundtrip.
        /// </summary>
        public static bool AddCopies(int bookID, int count, byte status = 1)
        {
            if (bookID <= 0 || count <= 0)
                return false;

            int rowsAffected = 0;

            const string query = @"
                DECLARE @i INT = 0;
                WHILE @i < @Count
                BEGIN
                    INSERT INTO BookCopies (BookID, Status) VALUES (@BookID, @Status);
                    SET @i = @i + 1;
                END;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    command.Parameters.Add("@Count", SqlDbType.Int).Value = count;
                    command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = status;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(AddCopies));
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Updates the status of a specific book copy.
        /// </summary>
        public static bool UpdateCopyStatus(int copyID, byte newStatus)
        {
            int rowsAffected = 0;

            const string query = @"
                UPDATE BookCopies
                SET Status = @Status
                WHERE CopyID = @CopyID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CopyID", SqlDbType.Int).Value = copyID;
                    command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = newStatus;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(UpdateCopyStatus));
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Retrieves physical copy information by CopyID.
        /// </summary>
        public static bool GetCopyInfoByID(int copyID, ref int bookID, ref byte status)
        {
            bool isFound = false;

            const string query = "SELECT BookID, Status FROM BookCopies WHERE CopyID = @CopyID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CopyID", SqlDbType.Int).Value = copyID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            bookID = reader.SafeGetInt("BookID");
                            status = Convert.ToByte(reader["Status"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetCopyInfoByID));
            }

            return isFound;
        }

        /// <summary>
        /// Retrieves the primary key of the first available copy for a book (Status = 1).
        /// Returns -1 if no copy is available.
        /// </summary>
        public static int GetAvailableCopyID(int bookID)
        {
            int copyID = -1;

            const string query = @"
                SELECT TOP 1 CopyID
                FROM BookCopies
                WHERE BookID = @BookID AND Status = 1
                ORDER BY CopyID ASC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        copyID = id;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetAvailableCopyID));
            }

            return copyID;
        }

        /// <summary>
        /// Retrieves total and available copy counts for a specified book.
        /// </summary>
        public static bool GetCopiesCountByBookID(int bookID, ref int total, ref int available)
        {
            bool success = false;

            const string query = @"
                SELECT 
                    COUNT(*) AS TotalCount,
                    ISNULL(SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END), 0) AS AvailableCount
                FROM BookCopies
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
                            success = true;
                            total = reader.SafeGetInt("TotalCount");
                            available = reader.SafeGetInt("AvailableCount");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetCopiesCountByBookID));
            }

            return success;
        }

        /// <summary>
        /// Returns all physical copies of a book.
        /// </summary>
        public static DataTable GetCopiesByBookID(int bookID)
        {
            DataTable dt = new DataTable();

            const string query = @"
                SELECT 
                    CopyID,
                    BookID,
                    Status,
                    CASE Status
                        WHEN 1 THEN 'Available'
                        WHEN 2 THEN 'Borrowed'
                        WHEN 3 THEN 'Lost/Damaged'
                        ELSE 'Unknown'
                    END AS StatusName
                FROM BookCopies
                WHERE BookID = @BookID
                ORDER BY CopyID ASC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetCopiesByBookID));
            }

            return dt;
        }

        /// <summary>
        /// Checks if a book has any currently active borrowings.
        /// </summary>
        public static bool HasActiveBorrowings(int bookID)
        {
            bool hasActive = false;

            const string query = @"
                SELECT 1 
                FROM Borrowings 
                WHERE BookID = @BookID AND ActualReturnDate IS NULL;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        hasActive = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(HasActiveBorrowings));
            }

            return hasActive;
        }

        /// <summary>
        /// Deletes all copies for a given book.
        /// </summary>
        public static bool DeleteCopiesByBookID(int bookID)
        {
            int rowsAffected = 0;

            const string query = "DELETE FROM BookCopies WHERE BookID = @BookID;";

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
                clsDataLogger.LogError(ex, nameof(DeleteCopiesByBookID));
            }

            return rowsAffected >= 0;
        }
    }
}
