using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public static class clsBorrowingDataAccess
    {
        public static int AddNewBorrowing(int memberID, int bookID, int bookCopyID, DateTime borrowDate, DateTime dueDate, int createdByUserID)
        {
            int borrowingID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                BEGIN TRY
                    BEGIN TRANSACTION;

                    -- 1. Check if copy is available
                    DECLARE @Status INT;
                    SELECT @Status = Status FROM BookCopies WITH (UPDLOCK) WHERE CopyID = @CopyID;

                    IF @Status = 1
                    BEGIN
                        -- 2. Insert Borrowing
                        INSERT INTO Borrowings (MemberID, BookID, BookCopyID, BorrowDate, DueDate, CreatedByUserID)
                        VALUES (@MemberID, @BookID, @CopyID, @BorrowDate, @DueDate, @CreatedByUserID);
                        
                        DECLARE @NewID INT = SCOPE_IDENTITY();

                        -- 3. Update Copy Status
                        UPDATE BookCopies SET Status = 2 WHERE CopyID = @CopyID;

                        COMMIT TRANSACTION;
                        SELECT @NewID;
                    END
                    ELSE
                    BEGIN
                        ROLLBACK TRANSACTION;
                        SELECT -1;
                    END
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK TRANSACTION;
                    SELECT -1;
                END CATCH;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MemberID", memberID);
            command.Parameters.AddWithValue("@BookID", bookID);
            command.Parameters.AddWithValue("@CopyID", bookCopyID);
            command.Parameters.AddWithValue("@BorrowDate", borrowDate);
            command.Parameters.AddWithValue("@DueDate", dueDate);
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    borrowingID = id;
                }
            }
            catch (Exception ex)
            {
                borrowingID = -1;
            }
            finally
            {
                connection.Close();
            }

            return borrowingID;
        }

        public static bool ReturnBook(int borrowingID, DateTime actualReturnDate)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                BEGIN TRY
                    BEGIN TRANSACTION;
                    
                    DECLARE @CopyID INT;
                    SELECT @CopyID = BookCopyID FROM Borrowings WITH (UPDLOCK) WHERE BorrowingID = @BorrowingID AND ActualReturnDate IS NULL;

                    IF @CopyID IS NOT NULL
                    BEGIN
                        UPDATE Borrowings SET ActualReturnDate = @ActualReturnDate WHERE BorrowingID = @BorrowingID;
                        UPDATE BookCopies SET Status = 1 WHERE CopyID = @CopyID;
                        COMMIT TRANSACTION;
                        SELECT 1;
                    END
                    ELSE
                    BEGIN
                        ROLLBACK TRANSACTION;
                        SELECT 0;
                    END
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK TRANSACTION;
                    SELECT 0;
                END CATCH;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BorrowingID", borrowingID);
            command.Parameters.AddWithValue("@ActualReturnDate", actualReturnDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int success))
                {
                    rowsAffected = success;
                }
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

        public static bool GetBorrowingInfoByID(int borrowingID, ref int memberID, ref int bookID, ref int bookCopyID,
            ref DateTime borrowDate, ref DateTime dueDate, ref DateTime? actualReturnDate, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Borrowings WHERE BorrowingID = @BorrowingID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BorrowingID", borrowingID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    memberID = (int)reader["MemberID"];
                    bookID = (int)reader["BookID"];
                    bookCopyID = (int)reader["BookCopyID"];
                    borrowDate = (DateTime)reader["BorrowDate"];
                    dueDate = (DateTime)reader["DueDate"];

                    if (reader["ActualReturnDate"] != DBNull.Value)
                        actualReturnDate = (DateTime)reader["ActualReturnDate"];
                    else
                        actualReturnDate = null;

                    createdByUserID = (int)reader["CreatedByUserID"];
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

        public static bool GetActiveBorrowingByCopyID(int bookCopyID, ref int borrowingID, ref int memberID,
            ref int bookID, ref DateTime borrowDate, ref DateTime dueDate, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT TOP 1 * FROM Borrowings WHERE BookCopyID = @BookCopyID AND ActualReturnDate IS NULL ORDER BY BorrowingID DESC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookCopyID", bookCopyID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    borrowingID = (int)reader["BorrowingID"];
                    memberID = (int)reader["MemberID"];
                    bookID = (int)reader["BookID"];
                    borrowDate = (DateTime)reader["BorrowDate"];
                    dueDate = (DateTime)reader["DueDate"];
                    createdByUserID = (int)reader["CreatedByUserID"];
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

        public static bool GetActiveBorrowingByBookAndMember(int bookID, int memberID, ref int borrowingID,
            ref int bookCopyID, ref DateTime borrowDate, ref DateTime dueDate, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT TOP 1 * FROM Borrowings WHERE BookID = @BookID AND MemberID = @MemberID AND ActualReturnDate IS NULL ORDER BY BorrowingID DESC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookID", bookID);
            command.Parameters.AddWithValue("@MemberID", memberID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    borrowingID = (int)reader["BorrowingID"];
                    bookCopyID = (int)reader["BookCopyID"];
                    borrowDate = (DateTime)reader["BorrowDate"];
                    dueDate = (DateTime)reader["DueDate"];
                    createdByUserID = (int)reader["CreatedByUserID"];
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

        public static int GetActiveBorrowingCountForMember(int memberID)
        {
            int count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(1) FROM Borrowings WHERE MemberID = @MemberID AND ActualReturnDate IS NULL";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MemberID", memberID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int val))
                {
                    count = val;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return count;
        }

        public static int GetFirstAvailableCopyID(int bookID)
        {
            int copyID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT TOP 1 CopyID FROM BookCopies WHERE BookID = @BookID AND Status = 1 ORDER BY CopyID ASC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookID", bookID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    copyID = id;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return copyID;
        }

        public static bool IsCopyAvailable(int copyID)
        {
            bool isAvailable = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT 1 FROM BookCopies WHERE CopyID = @CopyID AND Status = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CopyID", copyID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isAvailable = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return isAvailable;
        }

        public static DataTable GetAllBorrowings()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM v_BorrowingsInfo ORDER BY BorrowingID DESC";
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

        public static DataTable GetActiveBorrowings()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM v_BorrowingsInfo WHERE Status IN ('Active', 'Overdue') ORDER BY DueDate ASC";
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

        public static DataTable GetOverdueBorrowings()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM v_BorrowingsInfo WHERE Status = 'Overdue' ORDER BY DueDate ASC";
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

        public static bool DeleteBorrowing(int borrowingID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Borrowings WHERE BorrowingID = @BorrowingID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BorrowingID", borrowingID);

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
    }
}

