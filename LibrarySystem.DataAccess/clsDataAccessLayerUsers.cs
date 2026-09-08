using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public class clsDataAccessLayerUsers
    {
        // 1. Read / Authentication
        public static bool GetUserInfoByUsernameAndPassword(string username, string password,
            ref int userID, ref int personID, ref bool isActive, ref int permissions)
        {
            bool isFound = false;

            string query = @"SELECT UserID, PersonID, IsActive, Permissions
                             FROM Users 
                             WHERE Username = @Username AND Password = @Password;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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
                            permissions = (int)reader["Permissions"];
                            isActive = (reader["IsActive"] != DBNull.Value) && (bool)reader["IsActive"];
                        }
                    }
                }
                catch (Exception)
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        // 2. Read by UserID
        public static bool GetUserByID(int userID, ref int personID, ref string username,
            ref string password, ref bool isActive, ref int permissions)
        {
            bool isFound = false;

            string query = @"SELECT PersonID, Username, Password, IsActive, Permissions
                             FROM Users 
                             WHERE UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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
                            permissions = (int)reader["Permissions"];
                            isActive = (reader["IsActive"] != DBNull.Value) && (bool)reader["IsActive"];
                        }
                    }
                }
                catch (Exception)
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        // 3. Read by PersonID
        public static bool GetUserInfoByPersonID(int personID, ref int userID, ref string username,
            ref string password, ref bool isActive, ref int permissions)
        {
            bool isFound = false;

            string query = @"SELECT UserID, Username, Password, IsActive, Permissions
                             FROM Users 
                             WHERE PersonID = @PersonID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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
                            permissions = (int)reader["Permissions"];
                            isActive = (reader["IsActive"] != DBNull.Value) && (bool)reader["IsActive"];
                        }
                    }
                }
                catch (Exception)
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        // 4. Create (Add New User)
        public static int AddNewUser(int personID, string username, string password, bool isActive, int permissions)
        {
            int userID = -1;

            string query = @"INSERT INTO Users (PersonID, Username, Password, IsActive, Permissions)
                             VALUES (@PersonID, @Username, @Password, @IsActive, @Permissions);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@Permissions", permissions);

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

            return userID;
        }

        // 5. Update Full Profile
        public static bool UpdateUser(int userID, string username, string password, bool isActive, int permissions)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Users
                             SET Username    = @Username,
                                 Password    = @Password,
                                 IsActive    = @IsActive,
                                 Permissions = @Permissions
                             WHERE UserID    = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@Permissions", permissions);

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

            return (rowsAffected > 0);
        }

        // 6. Change Password Only
        public static bool ChangePassword(int userID, string newPassword)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Users
                             SET Password = @NewPassword
                             WHERE UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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

            return (rowsAffected > 0);
        }

        // 7. Soft Delete (Deactivate)
        public static bool DeactivateUser(int userID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Users
                             SET IsActive = 0
                             WHERE UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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

            return (rowsAffected > 0);
        }

        // 8. Hard Delete
        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;

            string query = @"DELETE FROM Users WHERE UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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

            return (rowsAffected > 0);
        }

        // 9. Check Existence by UserID
        public static bool IsUserExist(int userID)
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM Users WHERE UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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

            return isFound;
        }

        // 10. Check Existence by Username
        public static bool IsUserExist(string username)
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM Users WHERE Username = @Username;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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

            return isFound;
        }

        // 11. Check Existence by PersonID
        public static bool IsUserExistForPersonID(int personID)
        {
            bool isFound = false;

            string query = @"SELECT 1 FROM Users WHERE PersonID = @PersonID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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

            return isFound;
        }

        // 12. Read All Users (Joined with People)
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT 
                                Users.UserID,
                                Users.PersonID,
                                (People.FirstName + ' ' + People.LastName) AS FullName,
                                Users.Username,
                                Users.Permissions,
                                Users.IsActive,
                                People.Phone,
                                People.Email
                             FROM People 
                             INNER JOIN Users ON People.PersonID = Users.PersonID
                             ORDER BY Users.UserID DESC;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
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
                    // Log error or leave dt empty
                }
            }

            return dt;
        }
    }
}