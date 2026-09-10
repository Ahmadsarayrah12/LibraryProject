using System;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public static class clsDashboardDataAccess
    {
        public static int GetTotalBooks()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Books";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int c))
                    {
                        count = c;
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return count;
        }

        public static int GetTotalMembers()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Members";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int c))
                    {
                        count = c;
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return count;
        }

        public static int GetActiveBorrowings()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Borrowings WHERE ActualReturnDate IS NULL";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int c))
                    {
                        count = c;
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return count;
        }

        public static int GetOverdueBooks()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Borrowings WHERE ActualReturnDate IS NULL AND DueDate < @Today";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Today", DateTime.Today);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int c))
                    {
                        count = c;
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
                }
            }
            return count;
        }
    }
}
