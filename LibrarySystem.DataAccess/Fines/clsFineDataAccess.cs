using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public static class clsFineDataAccess
    {
        public static bool GetFineInfoByID(int fineID, ref int memberID, ref int borrowingID, ref int numberOfLateDays, ref decimal fineAmount, ref bool paymentStatus, ref DateTime? paymentDate, ref int createdByUserID)
        {
            bool isFound = false;
            string query = "SELECT * FROM Fines WHERE FineID = @FineID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FineID", fineID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            memberID = (int)reader["MemberID"];
                            borrowingID = (int)reader["BorrowingID"];
                            numberOfLateDays = (int)reader["NumberOfLateDays"];
                            fineAmount = (decimal)reader["FineAmount"];
                            paymentStatus = (bool)reader["PaymentStatus"];
                            paymentDate = reader["PaymentDate"] != DBNull.Value ? (DateTime?)reader["PaymentDate"] : null;
                            createdByUserID = (int)reader["CreatedByUserID"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return isFound;
        }

        public static int AddNewFine(int memberID, int borrowingID, int numberOfLateDays, decimal fineAmount, bool paymentStatus, DateTime? paymentDate, int createdByUserID)
        {
            int fineID = -1;
            string query = @"INSERT INTO Fines (MemberID, BorrowingID, NumberOfLateDays, FineAmount, PaymentStatus, PaymentDate, CreatedByUserID)
                             VALUES (@MemberID, @BorrowingID, @NumberOfLateDays, @FineAmount, @PaymentStatus, @PaymentDate, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);
                command.Parameters.AddWithValue("@BorrowingID", borrowingID);
                command.Parameters.AddWithValue("@NumberOfLateDays", numberOfLateDays);
                command.Parameters.AddWithValue("@FineAmount", fineAmount);
                command.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                command.Parameters.AddWithValue("@PaymentDate", paymentDate.HasValue ? (object)paymentDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        fineID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return fineID;
        }

        public static bool UpdateFine(int fineID, int memberID, int borrowingID, int numberOfLateDays, decimal fineAmount, bool paymentStatus, DateTime? paymentDate, int createdByUserID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Fines 
                             SET MemberID = @MemberID,
                                 BorrowingID = @BorrowingID,
                                 NumberOfLateDays = @NumberOfLateDays,
                                 FineAmount = @FineAmount,
                                 PaymentStatus = @PaymentStatus,
                                 PaymentDate = @PaymentDate,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE FineID = @FineID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FineID", fineID);
                command.Parameters.AddWithValue("@MemberID", memberID);
                command.Parameters.AddWithValue("@BorrowingID", borrowingID);
                command.Parameters.AddWithValue("@NumberOfLateDays", numberOfLateDays);
                command.Parameters.AddWithValue("@FineAmount", fineAmount);
                command.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                command.Parameters.AddWithValue("@PaymentDate", paymentDate.HasValue ? (object)paymentDate.Value : DBNull.Value);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllFines()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM v_FinesInfo ORDER BY PaymentStatus ASC, FineID DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return dt;
        }

        public static DataTable GetMemberFines(int memberID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM v_FinesInfo WHERE MemberID = @MemberID ORDER BY PaymentStatus ASC, FineID DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return dt;
        }

        public static bool HasUnpaidFines(int memberID)
        {
            bool hasUnpaidFines = false;
            string query = "SELECT TOP 1 1 FROM Fines WHERE MemberID = @MemberID AND PaymentStatus = 0";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MemberID", memberID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        hasUnpaidFines = true;
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return hasUnpaidFines;
        }
    }
}
