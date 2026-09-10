using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Pure ADO.NET Data Access Layer for Borrowings and Circulation operations.
    /// Executes transactional state transitions (issuing loans and returning copies),
    /// safe schema hydration, and optimized single-roundtrip projections.
    /// </summary>
    public static class clsBorrowingDataAccess
    {
        /// <summary>
        /// Atomically records a book borrowing and marks the corresponding copy as Borrowed (Status = 2).
        /// Returns the newly generated BorrowingID, or -1 if the transaction fails or copy is unavailable.
        /// </summary>
        public static int AddNewBorrowing(int memberID, int bookID, int bookCopyID, DateTime borrowDate, DateTime dueDate, int createdByUserID)
        {
            int borrowingID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            // 1. Verify copy availability with row lock
                            const string checkCopyQuery = @"
                                SELECT Status 
                                FROM BookCopies WITH (UPDLOCK, ROWLOCK) 
                                WHERE CopyID = @CopyID;";

                            using (SqlCommand checkCmd = new SqlCommand(checkCopyQuery, connection, transaction))
                            {
                                checkCmd.Parameters.Add("@CopyID", SqlDbType.Int).Value = bookCopyID;
                                object statusResult = checkCmd.ExecuteScalar();

                                if (statusResult == null || Convert.ToByte(statusResult) != 1) // 1 = Available
                                {
                                    transaction.Rollback();
                                    return -1;
                                }
                            }

                            // 2. Insert borrowing record
                            const string insertQuery = @"
                                INSERT INTO Borrowings (MemberID, BookID, BookCopyID, BorrowDate, DueDate, CreatedByUserID)
                                VALUES (@MemberID, @BookID, @BookCopyID, @BorrowDate, @DueDate, @CreatedByUserID);
                                SELECT SCOPE_IDENTITY();";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID;
                                insertCmd.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                                insertCmd.Parameters.Add("@BookCopyID", SqlDbType.Int).Value = bookCopyID;
                                insertCmd.Parameters.Add("@BorrowDate", SqlDbType.Date).Value = borrowDate.Date;
                                insertCmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = dueDate;
                                insertCmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = createdByUserID > 0 ? (object)createdByUserID : DBNull.Value;

                                object result = insertCmd.ExecuteScalar();
                                if (result != null && int.TryParse(result.ToString(), out int id))
                                {
                                    borrowingID = id;
                                }
                                else
                                {
                                    transaction.Rollback();
                                    return -1;
                                }
                            }

                            // 3. Transition physical copy status to 2 (Borrowed)
                            const string updateCopyQuery = @"
                                UPDATE BookCopies 
                                SET Status = 2 
                                WHERE CopyID = @CopyID;";

                            using (SqlCommand updateCmd = new SqlCommand(updateCopyQuery, connection, transaction))
                            {
                                updateCmd.Parameters.Add("@CopyID", SqlDbType.Int).Value = bookCopyID;
                                int rows = updateCmd.ExecuteNonQuery();
                                if (rows <= 0)
                                {
                                    transaction.Rollback();
                                    return -1;
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            clsDataLogger.LogError(ex, $"{nameof(AddNewBorrowing)} - Transaction");
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsDataLogger.LogError(ex, nameof(AddNewBorrowing));
                    return -1;
                }
            }

            return borrowingID;
        }

        /// <summary>
        /// Atomically returns a borrowed copy, updates ActualReturnDate, and resets copy Status = 1 (Available).
        /// </summary>
        public static bool ReturnBook(int borrowingID, DateTime actualReturnDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            // 1. Locate active borrowing record and extract BookCopyID
                            int bookCopyID = -1;
                            const string findQuery = @"
                                SELECT BookCopyID 
                                FROM Borrowings WITH (UPDLOCK, ROWLOCK) 
                                WHERE BorrowingID = @BorrowingID AND ActualReturnDate IS NULL;";

                            using (SqlCommand findCmd = new SqlCommand(findQuery, connection, transaction))
                            {
                                findCmd.Parameters.Add("@BorrowingID", SqlDbType.Int).Value = borrowingID;
                                object copyResult = findCmd.ExecuteScalar();

                                if (copyResult == null || copyResult == DBNull.Value || !int.TryParse(copyResult.ToString(), out bookCopyID))
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }

                            // 2. Mark borrowing record as returned
                            const string updateBorrowingQuery = @"
                                UPDATE Borrowings 
                                SET ActualReturnDate = @ActualReturnDate 
                                WHERE BorrowingID = @BorrowingID;";

                            using (SqlCommand updateBorrowCmd = new SqlCommand(updateBorrowingQuery, connection, transaction))
                            {
                                updateBorrowCmd.Parameters.Add("@BorrowingID", SqlDbType.Int).Value = borrowingID;
                                updateBorrowCmd.Parameters.Add("@ActualReturnDate", SqlDbType.DateTime).Value = actualReturnDate;
                                int rows = updateBorrowCmd.ExecuteNonQuery();
                                if (rows <= 0)
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }

                            // 3. Mark physical copy as 1 (Available)
                            const string updateCopyQuery = @"
                                UPDATE BookCopies 
                                SET Status = 1 
                                WHERE CopyID = @CopyID;";

                            using (SqlCommand updateCopyCmd = new SqlCommand(updateCopyQuery, connection, transaction))
                            {
                                updateCopyCmd.Parameters.Add("@CopyID", SqlDbType.Int).Value = bookCopyID;
                                updateCopyCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            clsDataLogger.LogError(ex, $"{nameof(ReturnBook)} - Transaction");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsDataLogger.LogError(ex, nameof(ReturnBook));
                    return false;
                }
            }
        }

        /// <summary>
        /// Retrieves borrowing record details by unique identifier.
        /// </summary>
        public static bool GetBorrowingInfoByID(int borrowingID, ref int memberID, ref int bookID, ref int bookCopyID,
            ref DateTime borrowDate, ref DateTime dueDate, ref DateTime? actualReturnDate, ref int createdByUserID)
        {
            bool isFound = false;

            const string query = @"
                SELECT MemberID, BookID, BookCopyID, BorrowDate, DueDate, ActualReturnDate, CreatedByUserID
                FROM Borrowings
                WHERE BorrowingID = @BorrowingID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BorrowingID", SqlDbType.Int).Value = borrowingID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            memberID = reader.SafeGetInt("MemberID");
                            bookID = reader.SafeGetInt("BookID");
                            bookCopyID = reader.SafeGetInt("BookCopyID");
                            borrowDate = reader.SafeGetDateTime("BorrowDate");
                            dueDate = reader.SafeGetDateTime("DueDate");

                            int returnOrdinal = reader.GetOrdinal("ActualReturnDate");
                            actualReturnDate = reader.IsDBNull(returnOrdinal) ? (DateTime?)null : reader.GetDateTime(returnOrdinal);

                            createdByUserID = reader.SafeGetInt("CreatedByUserID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetBorrowingInfoByID));
            }

            return isFound;
        }

        /// <summary>
        /// Finds active borrowing details for a specific physical book copy.
        /// </summary>
        public static bool GetActiveBorrowingByCopyID(int bookCopyID, ref int borrowingID, ref int memberID,
            ref int bookID, ref DateTime borrowDate, ref DateTime dueDate, ref int createdByUserID)
        {
            bool isFound = false;

            const string query = @"
                SELECT TOP 1 BorrowingID, MemberID, BookID, BorrowDate, DueDate, CreatedByUserID
                FROM Borrowings
                WHERE BookCopyID = @BookCopyID AND ActualReturnDate IS NULL
                ORDER BY BorrowingID DESC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookCopyID", SqlDbType.Int).Value = bookCopyID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            borrowingID = reader.SafeGetInt("BorrowingID");
                            memberID = reader.SafeGetInt("MemberID");
                            bookID = reader.SafeGetInt("BookID");
                            borrowDate = reader.SafeGetDateTime("BorrowDate");
                            dueDate = reader.SafeGetDateTime("DueDate");
                            createdByUserID = reader.SafeGetInt("CreatedByUserID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetActiveBorrowingByCopyID));
            }

            return isFound;
        }

        /// <summary>
        /// Finds active borrowing details for a specific book and member combination.
        /// </summary>
        public static bool GetActiveBorrowingByBookAndMember(int bookID, int memberID, ref int borrowingID,
            ref int bookCopyID, ref DateTime borrowDate, ref DateTime dueDate, ref int createdByUserID)
        {
            bool isFound = false;

            const string query = @"
                SELECT TOP 1 BorrowingID, BookCopyID, BorrowDate, DueDate, CreatedByUserID
                FROM Borrowings
                WHERE BookID = @BookID AND MemberID = @MemberID AND ActualReturnDate IS NULL
                ORDER BY BorrowingID DESC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = bookID;
                    command.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            borrowingID = reader.SafeGetInt("BorrowingID");
                            bookCopyID = reader.SafeGetInt("BookCopyID");
                            borrowDate = reader.SafeGetDateTime("BorrowDate");
                            dueDate = reader.SafeGetDateTime("DueDate");
                            createdByUserID = reader.SafeGetInt("CreatedByUserID");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetActiveBorrowingByBookAndMember));
            }

            return isFound;
        }

        /// <summary>
        /// Counts current unreturned active loans for a specific member.
        /// Used to enforce the maximum allowed active borrowing threshold.
        /// </summary>
        public static int GetActiveBorrowingCountForMember(int memberID)
        {
            int count = 0;

            const string query = @"
                SELECT COUNT(1) 
                FROM Borrowings 
                WHERE MemberID = @MemberID AND ActualReturnDate IS NULL;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID;
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int val))
                    {
                        count = val;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetActiveBorrowingCountForMember));
            }

            return count;
        }

        /// <summary>
        /// Locates the first available copy ID for a given book catalog entry.
        /// Returns -1 if no physical copies are currently in Available status.
        /// </summary>
        public static int GetFirstAvailableCopyID(int bookID)
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
                clsDataLogger.LogError(ex, nameof(GetFirstAvailableCopyID));
            }

            return copyID;
        }

        /// <summary>
        /// Checks whether a specific copy ID exists and is currently Available.
        /// </summary>
        public static bool IsCopyAvailable(int copyID)
        {
            bool isAvailable = false;

            const string query = "SELECT 1 FROM BookCopies WHERE CopyID = @CopyID AND Status = 1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CopyID", SqlDbType.Int).Value = copyID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isAvailable = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(IsCopyAvailable));
            }

            return isAvailable;
        }

        /// <summary>
        /// Returns all borrowing records with full denormalized projections from v_BorrowingsInfo.
        /// Unconditionally populates DataTable schema to prevent UI binding exceptions.
        /// </summary>
        public static DataTable GetAllBorrowings()
        {
            DataTable dt = new DataTable();

            const string query = @"
                SELECT * 
                FROM v_BorrowingsInfo 
                ORDER BY BorrowingID DESC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetAllBorrowings));
            }

            return dt;
        }

        /// <summary>
        /// Returns active and overdue borrowing records from v_BorrowingsInfo.
        /// </summary>
        public static DataTable GetActiveBorrowings()
        {
            DataTable dt = new DataTable();

            const string query = @"
                SELECT * 
                FROM v_BorrowingsInfo 
                WHERE Status IN ('Active', 'Overdue')
                ORDER BY DueDate ASC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetActiveBorrowings));
            }

            return dt;
        }

        /// <summary>
        /// Returns exclusively overdue borrowing records from v_BorrowingsInfo.
        /// </summary>
        public static DataTable GetOverdueBorrowings()
        {
            DataTable dt = new DataTable();

            const string query = @"
                SELECT * 
                FROM v_BorrowingsInfo 
                WHERE Status = 'Overdue'
                ORDER BY DueDate ASC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetOverdueBorrowings));
            }

            return dt;
        }

        /// <summary>
        /// Deletes a borrowing record by primary key (primarily for benchmark and administrative test cleanup).
        /// </summary>
        public static bool DeleteBorrowing(int borrowingID)
        {
            int rowsAffected = 0;
            const string query = "DELETE FROM Borrowings WHERE BorrowingID = @BorrowingID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BorrowingID", SqlDbType.Int).Value = borrowingID;
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(DeleteBorrowing));
            }

            return rowsAffected > 0;
        }
    }
}
