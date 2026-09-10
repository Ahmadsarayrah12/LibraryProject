using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Pure ADO.NET Data Access Layer for Authors table operations.
    /// Provides parameterized CRUD methods with safe DBNull handling.
    /// </summary>
    public static class clsAuthorDataAccess
    {
        /// <summary>
        /// Retrieves an author record by its primary key.
        /// </summary>
        public static bool GetAuthorInfoByID(int authorID, ref string fullName, ref string biography)
        {
            bool isFound = false;

            const string query = @"
                SELECT FullName, Biography 
                FROM Authors 
                WHERE AuthorID = @AuthorID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@AuthorID", SqlDbType.Int).Value = authorID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            fullName = reader.SafeGetString("FullName");
                            biography = reader.SafeGetString("Biography");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetAuthorInfoByID));
            }

            return isFound;
        }

        /// <summary>
        /// Inserts a new author and returns the generated AuthorID.
        /// </summary>
        public static int AddNewAuthor(string fullName, string biography)
        {
            int authorID = -1;

            const string query = @"
                INSERT INTO Authors (FullName, Biography)
                VALUES (@FullName, @Biography);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@FullName", SqlDbType.NVarChar, 150).Value = fullName;
                    command.Parameters.AddWithNullableString("@Biography", biography);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        authorID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(AddNewAuthor));
            }

            return authorID;
        }

        /// <summary>
        /// Updates an existing author record.
        /// </summary>
        public static bool UpdateAuthor(int authorID, string fullName, string biography)
        {
            int rowsAffected = 0;

            const string query = @"
                UPDATE Authors
                SET FullName = @FullName,
                    Biography = @Biography
                WHERE AuthorID = @AuthorID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@AuthorID", SqlDbType.Int).Value = authorID;
                    command.Parameters.Add("@FullName", SqlDbType.NVarChar, 150).Value = fullName;
                    command.Parameters.AddWithNullableString("@Biography", biography);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(UpdateAuthor));
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes an author by ID.
        /// </summary>
        public static bool DeleteAuthor(int authorID)
        {
            int rowsAffected = 0;

            const string query = "DELETE FROM Authors WHERE AuthorID = @AuthorID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@AuthorID", SqlDbType.Int).Value = authorID;
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(DeleteAuthor));
            }

            return rowsAffected > 0;
        }

        /// <summary>
        /// Checks if an author exists by AuthorID.
        /// </summary>
        public static bool IsAuthorExist(int authorID)
        {
            bool isFound = false;

            const string query = "SELECT 1 FROM Authors WHERE AuthorID = @AuthorID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@AuthorID", SqlDbType.Int).Value = authorID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(IsAuthorExist));
            }

            return isFound;
        }

        /// <summary>
        /// Returns all authors as a DataTable.
        /// </summary>
        public static DataTable GetAllAuthors()
        {
            DataTable dt = new DataTable();

            const string query = "SELECT AuthorID, FullName, Biography FROM Authors ORDER BY FullName ASC;";

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
                clsDataLogger.LogError(ex, nameof(GetAllAuthors));
            }

            return dt;
        }
    }
}
