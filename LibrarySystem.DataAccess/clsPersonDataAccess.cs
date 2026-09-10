using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Provides low-level database operations against the People table in SQL Server.
    /// Handles parameterized CRUD operations, search filters, and identity resolution.
    /// </summary>
    public static class clsPersonDataAccess
    {
        /// <summary>
        /// Retrieves personal identity data by primary key (PersonID).
        /// </summary>
        public static bool GetPersonInfoByID(int personID, ref string firstName, ref string lastName, ref string phone, ref string email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT FirstName, LastName, Phone, Email 
                                 FROM People 
                                 WHERE PersonID = @PersonID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                firstName = (string)reader["FirstName"];
                                lastName = (string)reader["LastName"];
                                phone = (reader["Phone"] != DBNull.Value) ? (string)reader["Phone"] : "";
                                email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // In production, log exception details to an audit logger or Event Viewer
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Retrieves personal identity data by phone number.
        /// </summary>
        public static bool GetPersonInfoByPhone(string phone, ref int personID, ref string firstName, ref string lastName, ref string email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT PersonID, FirstName, LastName, Email 
                                 FROM People 
                                 WHERE Phone = @Phone;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Phone", phone);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                personID = (int)reader["PersonID"];
                                firstName = (string)reader["FirstName"];
                                lastName = (string)reader["LastName"];
                                email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Inserts a new person record and returns the auto-generated PersonID.
        /// </summary>
        public static int AddNewPerson(string firstName, string lastName, string phone, string email)
        {
            int personID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO People (FirstName, LastName, Phone, Email)
                                 VALUES (@FirstName, @LastName, @Phone, @Email);
                                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);

                    if (string.IsNullOrWhiteSpace(phone))
                        command.Parameters.AddWithValue("@Phone", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Phone", phone.Trim());

                    if (string.IsNullOrWhiteSpace(email))
                        command.Parameters.AddWithValue("@Email", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Email", email.Trim());

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            personID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        personID = -1;
                    }
                }
            }

            return personID;
        }

        /// <summary>
        /// Updates an existing person record in the People table.
        /// </summary>
        public static bool UpdatePerson(int personID, string firstName, string lastName, string phone, string email)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE People 
                                 SET FirstName = @FirstName,
                                     LastName  = @LastName,
                                     Phone     = @Phone,
                                     Email     = @Email
                                 WHERE PersonID = @PersonID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);

                    if (string.IsNullOrWhiteSpace(phone))
                        command.Parameters.AddWithValue("@Phone", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Phone", phone.Trim());

                    if (string.IsNullOrWhiteSpace(email))
                        command.Parameters.AddWithValue("@Email", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@Email", email.Trim());

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Deletes a person record by PersonID.
        /// </summary>
        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM People WHERE PersonID = @PersonID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Will fail if restricted by Foreign Key constraints (e.g. Users, Members)
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Retrieves all people records from the database ordered by PersonID descending.
        /// </summary>
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 
                                    PersonID,
                                    FirstName,
                                    LastName,
                                    (FirstName + ' ' + LastName) AS FullName,
                                    Phone,
                                    Email
                                 FROM People
                                 ORDER BY PersonID DESC;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
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
                    catch (Exception ex)
                    {
                        // Returns empty DataTable on error
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Checks whether a person exists by PersonID.
        /// </summary>
        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 1 FROM People WHERE PersonID = @PersonID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Checks whether a person exists with a specific phone number.
        /// </summary>
        public static bool IsPersonExist(string phone)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 1 FROM People WHERE Phone = @Phone;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Phone", phone);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
    }
}