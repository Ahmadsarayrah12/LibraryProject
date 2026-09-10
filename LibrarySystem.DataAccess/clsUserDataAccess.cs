using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Data Access Layer providing parameterized operations against the Users table in SQL Server.
    /// Handles credential validation, role and permission mappings, and account lifecycle states.
    /// </summary>
    public static class clsUserDataAccess
    {
        /// <summary>
        /// Retrieves user details by primary key (UserID).
        /// </summary>
        public static bool GetUserInfoByUserID(int userID, ref int personID, ref string username,
            ref string password, ref int permissions, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT PersonID, Username, Password, Permissions, IsActive 
                                       FROM Users 
                                       WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                personID = reader.SafeGetInt("PersonID");
                                username = reader.SafeGetString("Username");
                                password = reader.SafeGetString("Password");
                                permissions = reader.SafeGetInt("Permissions", 0);
                                isActive = reader.SafeGetBool("IsActive");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetUserInfoByUserID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Backward-compatible overload for retrieving user details by UserID.
        /// </summary>
        public static bool GetUserByID(int userID, ref int personID, ref string username,
            ref string password, ref bool isActive, ref int permissions)
        {
            return GetUserInfoByUserID(userID, ref personID, ref username, ref password, ref permissions, ref isActive);
        }

        /// <summary>
        /// Retrieves user details by linked PersonID.
        /// </summary>
        public static bool GetUserInfoByPersonID(int personID, ref int userID, ref string username,
            ref string password, ref int permissions, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT UserID, Username, Password, Permissions, IsActive 
                                       FROM Users 
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
                                userID = reader.SafeGetInt("UserID");
                                username = reader.SafeGetString("Username");
                                password = reader.SafeGetString("Password");
                                permissions = reader.SafeGetInt("Permissions", 0);
                                isActive = reader.SafeGetBool("IsActive");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetUserInfoByPersonID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Backward-compatible overload for retrieving user details by PersonID.
        /// </summary>
        public static bool GetUserInfoByPersonID(int personID, ref int userID, ref string username,
            ref string password, ref bool isActive, ref int permissions)
        {
            return GetUserInfoByPersonID(personID, ref userID, ref username, ref password, ref permissions, ref isActive);
        }

        /// <summary>
        /// Validates login credentials and returns user details upon matching Username and Password.
        /// </summary>
        public static bool GetUserInfoByUsernameAndPassword(string username, string password,
            ref int userID, ref int personID, ref int permissions, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT UserID, PersonID, Permissions, IsActive 
                                       FROM Users 
                                       WHERE Username = @Username AND Password = @Password;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                userID = reader.SafeGetInt("UserID");
                                personID = reader.SafeGetInt("PersonID");
                                permissions = reader.SafeGetInt("Permissions", 0);
                                isActive = reader.SafeGetBool("IsActive");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetUserInfoByUsernameAndPassword));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Backward-compatible overload for credential validation.
        /// </summary>
        public static bool GetUserInfoByUsernameAndPassword(string username, string password,
            ref int userID, ref int personID, ref bool isActive, ref int permissions)
        {
            return GetUserInfoByUsernameAndPassword(username, password, ref userID, ref personID, ref permissions, ref isActive);
        }

        /// <summary>
        /// Inserts a new user record and returns the auto-generated UserID.
        /// </summary>
        public static int AddNewUser(int personID, string username, string password, int permissions, bool isActive)
        {
            int userID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"INSERT INTO Users (PersonID, Username, Password, Permissions, IsActive)
                                       VALUES (@PersonID, @Username, @Password, @Permissions, @IsActive);
                                       SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Permissions", permissions);
                    command.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            userID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(AddNewUser));
                        userID = -1;
                    }
                }
            }

            return userID;
        }

        /// <summary>
        /// Backward-compatible overload for inserting a new user.
        /// </summary>
        public static int AddNewUser(int personID, string username, string password, bool isActive, int permissions)
        {
            return AddNewUser(personID, username, password, permissions, isActive);
        }

        /// <summary>
        /// Updates an existing user record including PersonID linkage.
        /// </summary>
        public static bool UpdateUser(int userID, int personID, string username, string password, int permissions, bool isActive)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"UPDATE Users 
                                       SET PersonID    = @PersonID,
                                           Username    = @Username,
                                           Password    = @Password,
                                           Permissions = @Permissions,
                                           IsActive    = @IsActive
                                       WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Permissions", permissions);
                    command.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(UpdateUser));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Backward-compatible overload for updating an existing user.
        /// </summary>
        public static bool UpdateUser(int userID, string username, string password, bool isActive, int permissions)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"UPDATE Users 
                                       SET Username    = @Username,
                                           Password    = @Password,
                                           Permissions = @Permissions,
                                           IsActive    = @IsActive
                                       WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Permissions", permissions);
                    command.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(UpdateUser));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Updates a user's password securely by UserID.
        /// </summary>
        public static bool ChangePassword(int userID, string newPassword)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"UPDATE Users 
                                       SET Password = @NewPassword 
                                       WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(ChangePassword));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Deactivates a user account (soft disable).
        /// </summary>
        public static bool DeactivateUser(int userID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"UPDATE Users 
                                       SET IsActive = 0 
                                       WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(DeactivateUser));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Permanently deletes a user record by primary key.
        /// </summary>
        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"DELETE FROM Users WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(DeleteUser));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Retrieves all users joined with person contact information.
        /// </summary>
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 
                                        Users.UserID, 
                                        Users.PersonID, 
                                        (People.FirstName + ' ' + People.LastName) AS FullName, 
                                        Users.Username, 
                                        Users.Permissions, 
                                        Users.IsActive, 
                                        People.Phone, 
                                        People.Email 
                                     FROM Users 
                                     INNER JOIN People ON Users.PersonID = People.PersonID 
                                     ORDER BY Users.UserID DESC;";

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
                        clsDataLogger.LogError(ex, nameof(GetAllUsers));
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Checks whether a user exists by primary key ID.
        /// </summary>
        public static bool IsUserExist(int userID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM Users WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(IsUserExist));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Checks whether a user exists with the specified username.
        /// </summary>
        public static bool IsUserExist(string username)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM Users WHERE Username = @Username;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, $"{nameof(IsUserExist)}_Username");
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Checks whether a user exists for a given PersonID.
        /// </summary>
        public static bool IsUserExistForPersonID(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM Users WHERE PersonID = @PersonID;";

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
                        clsDataLogger.LogError(ex, nameof(IsUserExistForPersonID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
    }
}