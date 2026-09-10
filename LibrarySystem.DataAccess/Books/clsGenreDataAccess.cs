using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Pure ADO.NET Data Access Layer for Genres table operations.
    /// Provides parameterized retrieval methods with safe DBNull handling.
    /// </summary>
    public static class clsGenreDataAccess
    {
        /// <summary>
        /// Retrieves all book genres ordered alphabetically.
        /// </summary>
        public static DataTable GetAllGenres()
        {
            DataTable dt = new DataTable();

            const string query = "SELECT GenreID, GenreName FROM Genres ORDER BY GenreName ASC;";

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
                clsDataLogger.LogError(ex, nameof(GetAllGenres));
            }

            return dt;
        }

        /// <summary>
        /// Retrieves genre information by GenreID.
        /// </summary>
        public static bool GetGenreInfoByID(int genreID, ref string genreName)
        {
            bool isFound = false;

            const string query = "SELECT GenreName FROM Genres WHERE GenreID = @GenreID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@GenreID", SqlDbType.Int).Value = genreID;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            genreName = reader.SafeGetString("GenreName");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(GetGenreInfoByID));
            }

            return isFound;
        }

        /// <summary>
        /// Adds a new genre if it does not already exist.
        /// </summary>
        public static int AddNewGenre(string genreName)
        {
            int genreID = -1;

            const string query = @"
                IF NOT EXISTS (SELECT 1 FROM Genres WHERE GenreName = @GenreName)
                BEGIN
                    INSERT INTO Genres (GenreName) VALUES (@GenreName);
                    SELECT SCOPE_IDENTITY();
                END
                ELSE
                BEGIN
                    SELECT GenreID FROM Genres WHERE GenreName = @GenreName;
                END;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@GenreName", SqlDbType.NVarChar, 100).Value = genreName;
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        genreID = id;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataLogger.LogError(ex, nameof(AddNewGenre));
            }

            return genreID;
        }
    }
}
