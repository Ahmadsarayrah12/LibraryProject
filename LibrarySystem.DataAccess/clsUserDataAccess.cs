using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Data access layer for User entities.
    /// Provides low-level database operations executing against the SQL Server database.
    /// </summary>
    public static class clsUserDataAccess
    {
        public static bool GetUserInfoByUserID(int userID, ref int personID, ref string username,
            ref string password, ref int permissions, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT PersonID, Username, Password, Permissions, IsActive 
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
                                personID = (int)reader["PersonID"];
                                username = (string)reader["Username"];
                                password = (string)reader["Password"];
                                permissions = reader["Permissions"] != DBNull.Value ? Convert.ToInt32(reader["Permissions"]) : 0;
                                isActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool GetUserByID(int userID, ref int personID, ref string username,
            ref string password, ref bool isActive, ref int permissions)
        {
            return GetUserInfoByUserID(userID, ref personID, ref username, ref password, ref permissions, ref isActive);
        }

        public static bool GetUserInfoByPersonID(int personID, ref int userID, ref string username,
            ref string password, ref int permissions, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT UserID, Username, Password, Permissions, IsActive 
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
                                userID = (int)reader["UserID"];
                                username = (string)reader["Username"];
                                password = (string)reader["Password"];
                                permissions = reader["Permissions"] != DBNull.Value ? Convert.ToInt32(reader["Permissions"]) : 0;
                                isActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool GetUserInfoByPersonID(int personID, ref int userID, ref string username,
            ref string password, ref bool isActive, ref int permissions)
        {
            return GetUserInfoByPersonID(personID, ref userID, ref username, ref password, ref permissions, ref isActive);
        }

        public static bool GetUserInfoByUsernameAndPassword(string username, string password,
            ref int userID, ref int personID, ref int permissions, ref bool isActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT UserID, PersonID, Permissions, IsActive 
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
                                userID = (int)reader["UserID"];
                                personID = (int)reader["PersonID"];
                                permissions = reader["Permissions"] != DBNull.Value ? Convert.ToInt32(reader["Permissions"]) : 0;
                                isActive = (bool)reader["IsActive"];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(string username, string password,
            ref int userID, ref int personID, ref bool isActive, ref int permissions)
        {
            return GetUserInfoByUsernameAndPassword(username, password, ref userID, ref personID, ref permissions, ref isActive);
        }

        public static int AddNewUser(int personID, string username, string password, int permissions, bool isActive)
        {
            int userID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Users (PersonID, Username, Password, Permissions, IsActive)
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
                    catch (Exception)
                    {
                        userID = -1;
                    }
                }
            }

            return userID;
        }

        public static int AddNewUser(int personID, string username, string password, bool isActive, int permissions)
        {
            return AddNewUser(personID, username, password, permissions, isActive);
        }

        public static bool UpdateUser(int userID, int personID, string username, string password, int permissions, bool isActive)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Users 
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
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static bool UpdateUser(int userID, string username, string password, bool isActive, int permissions)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Users 
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
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static bool ChangePassword(int userID, string newPassword)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Users 
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
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeactivateUser(int userID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Users 
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
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM Users WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 
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
                    catch (Exception)
                    {
                    }
                }
            }

            return dt;
        }

        public static bool IsUserExist(int userID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 1 FROM Users WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool IsUserExist(string username)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 1 FROM Users WHERE Username = @Username;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 1 FROM Users WHERE PersonID = @PersonID;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isFound = (result != null);
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
    }
}