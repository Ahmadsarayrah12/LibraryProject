using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Provides low-level, parameterized database operations against the People table in SQL Server.
    /// Handles CRUD operations, unique phone lookups, and identity existence verification.
    /// </summary>
    public static class clsPersonDataAccess
    {
        /// <summary>
        /// Retrieves personal identity record from the database by PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <param name="firstName">Output parameter receiving the first name.</param>
        /// <param name="lastName">Output parameter receiving the last name.</param>
        /// <param name="phone">Output parameter receiving the phone number.</param>
        /// <param name="email">Output parameter receiving the email address.</param>
        /// <returns>True if the record was found; otherwise, false.</returns>
        public static bool GetPersonInfoByID(int personID, ref string firstName, ref string lastName, ref string phone, ref string email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT FirstName, LastName, Phone, Email 
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
                                firstName = reader.SafeGetString("FirstName");
                                lastName = reader.SafeGetString("LastName");
                                phone = reader.SafeGetString("Phone");
                                email = reader.SafeGetString("Email");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetPersonInfoByID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Retrieves personal identity record from the database by unique phone number.
        /// </summary>
        /// <param name="phone">The phone number of the person.</param>
        /// <param name="personID">Output parameter receiving the PersonID.</param>
        /// <param name="firstName">Output parameter receiving the first name.</param>
        /// <param name="lastName">Output parameter receiving the last name.</param>
        /// <param name="email">Output parameter receiving the email address.</param>
        /// <returns>True if the record was found; otherwise, false.</returns>
        public static bool GetPersonInfoByPhone(string phone, ref int personID, ref string firstName, ref string lastName, ref string email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT PersonID, FirstName, LastName, Email 
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
                                personID = reader.SafeGetInt("PersonID");
                                firstName = reader.SafeGetString("FirstName");
                                lastName = reader.SafeGetString("LastName");
                                email = reader.SafeGetString("Email");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetPersonInfoByPhone));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Inserts a new person record and returns the auto-generated PersonID.
        /// </summary>
        /// <param name="firstName">First name of the person.</param>
        /// <param name="lastName">Last name of the person.</param>
        /// <param name="phone">Phone number of the person (nullable).</param>
        /// <param name="email">Email address of the person (nullable).</param>
        /// <returns>The newly generated PersonID upon success; otherwise, -1.</returns>
        public static int AddNewPerson(string firstName, string lastName, string phone, string email)
        {
            int personID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"INSERT INTO People (FirstName, LastName, Phone, Email)
                                       VALUES (@FirstName, @LastName, @Phone, @Email);
                                       SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithNullableString("@Phone", phone);
                    command.Parameters.AddWithNullableString("@Email", email);

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
                        clsDataLogger.LogError(ex, nameof(AddNewPerson));
                        personID = -1;
                    }
                }
            }

            return personID;
        }

        /// <summary>
        /// Updates an existing person record in the database.
        /// </summary>
        /// <param name="personID">The primary key ID of the person.</param>
        /// <param name="firstName">Updated first name.</param>
        /// <param name="lastName">Updated last name.</param>
        /// <param name="phone">Updated phone number.</param>
        /// <param name="email">Updated email address.</param>
        /// <returns>True if rows were affected; otherwise, false.</returns>
        public static bool UpdatePerson(int personID, string firstName, string lastName, string phone, string email)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"UPDATE People 
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
                    command.Parameters.AddWithNullableString("@Phone", phone);
                    command.Parameters.AddWithNullableString("@Email", email);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(UpdatePerson));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Deletes a person record from the database by PersonID.
        /// </summary>
        /// <param name="personID">The primary key ID of the person to delete.</param>
        /// <returns>True if deleted; otherwise, false.</returns>
        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"DELETE FROM People WHERE PersonID = @PersonID;";

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
                        clsDataLogger.LogError(ex, nameof(DeletePerson));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Retrieves all people records ordered descending by PersonID.
        /// </summary>
        /// <returns>A DataTable containing all people records.</returns>
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT PersonID, FirstName, LastName, Phone, Email 
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
                        clsDataLogger.LogError(ex, nameof(GetAllPeople));
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Checks whether a person exists with the specified PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>True if the person exists; otherwise, false.</returns>
        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM People WHERE PersonID = @PersonID;";

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
                        clsDataLogger.LogError(ex, nameof(IsPersonExist));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Checks whether a person exists with the specified phone number.
        /// </summary>
        /// <param name="phone">The phone number to search.</param>
        /// <returns>True if a person exists with the given phone; otherwise, false.</returns>
        public static bool IsPersonExist(string phone)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM People WHERE Phone = @Phone;";

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
                        clsDataLogger.LogError(ex, $"{nameof(IsPersonExist)}_Phone");
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
    }
}