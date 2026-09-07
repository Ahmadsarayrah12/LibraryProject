using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public class clsDataAccessLayerUsers
    {
        public static bool Login(string username, string password,
            ref int userID, ref int personID, ref bool isActive , ref int permissions )
        {
            bool isFound = false;
            // query 
            string query = @"SELECT UserID, PersonID, IsActive ,Permissions
                             FROM Users 
                             WHERE Username = @Username AND Password = @Password;";
            
            //(Using) is more safe than any thing ..

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))

            //Command set
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
                            permissions = (int)reader["[Permissions]"];
                            // deal with null case.
                            isActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
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


        public static bool GetUserByID(int userID,ref int personID,ref string username,ref  string password,ref bool isActive,ref int permissions) 
        {

            bool isFound = false;

            string query = @"SELECT 
                             [PersonID]
                            ,[Username]
                            ,[Password]
                            ,[IsActive]
                            ,[Permissions]
                            FROM [dbo].[Users] WHERE UserID =@userID";


            using(SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query , connection))
            {


                command.Parameters.AddWithValue("@userID",userID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            personID = (int)reader["PersonID"];
                            username = (string)reader["Username"];
                            password = (string)reader["Password"];
                            isActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
                            permissions = (int)reader["Permissions"];
                            isFound = true;
                        }
                    }
                }
                catch
                {
                    isFound = false;
                }


            }
            return isFound;
        
        }
        
        public static bool GetUserByPersonID(  int personID,ref int userID,ref string username,ref  string password,ref bool isActive,ref int permissions) 
        {

            bool isFound = false;

            string query = @"SELECT 
                             [UserID]
                            ,[Username]
                            ,[Password]
                            ,[IsActive]
                            ,[Permissions]
                            FROM [dbo].[Users] WHERE PersonID =@personID";


            using(SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query , connection))
            {


                command.Parameters.AddWithValue("@personID", personID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            userID = (int)reader["UserID"];
                            username = (string)reader["Username"];
                            password = (string)reader["Password"];
                            isActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
                            permissions = (int)reader["Permissions"];
                            isFound = true;
                        }
                    }
                    
                }
                catch
                {
                    isFound = false;
                }


            }
            return isFound;
        
        }


        public static int AddNewUser(int PersonID,string Username, string Password, bool IsActive,int Permissions )
        {

            int UserID = -1;

            string query = @"INSERT INTO [dbo].[Users]
                                           ([PersonID]
                                           ,[Username]
                                           ,[Password]
                                           ,[IsActive]
                                           ,[Permissions])
                                     VALUES
                                           (@PersonID
                                           ,@Username
                                           ,@Password
                                           ,@IsActive
                                           ,@Permissions);

                                SELECT SCOPE_IDENTITY();";


            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection)) 
            {
                  command.Parameters.AddWithValue("@PersonID", PersonID);

                 command.Parameters.AddWithValue("@Username", Username);

                 command.Parameters.AddWithValue("@Password", Password);

                 command.Parameters.AddWithValue("@IsActive", IsActive);

                 command.Parameters.AddWithValue("@Permissions", Permissions);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();


                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {

                        UserID =insertedID ;


                    }

                }
                catch
                {
                    UserID = -1;

                }

            }
            return UserID;

        }

        public static bool UpdateUser(int userID, string username, string password, bool isActive, int permissions)
        {
            int rowsAffected = 0;

            string query = @"UPDATE [dbo].[Users]
                     SET [Username]    = @Username,
                         [Password]    = @Password,
                         [IsActive]    = @IsActive,
                         [Permissions] = @Permissions
                     WHERE UserID = @UserID;";

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

        public static bool DeactivateUser(int userID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE [dbo].[Users]
                     SET [IsActive] = 0
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

        public static bool DeleteUser(int userID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM [dbo].[Users] 
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


        public static bool IsExistUser(int UserID)
        {


            bool isFound = false;
            string query = @"SELECT person= 1 FROM [dbo].[Users] 
                             WHERE UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))  

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    isFound = (result != null);
                       

                }
                catch
                {

                    return false;

                }

            }
            return isFound;
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            string query = @"SELECT person= 1 FROM [dbo].[Users] 
                             WHERE PersonID = @PersonID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    isFound = (result != null);
                }
                catch
                {
                    return false;
                }


            }

            return isFound;

        }

       public static DataTable GetAllUser()
       {
            DataTable dt = new DataTable();

            string query = @"SELECT        Users.UserID, Users.PersonID, People.FirstName +' '+ People.LastName as FullName, Users.Username, Users.Permissions, Users.IsActive, People.Phone, People.Email
                             FROM            People INNER JOIN
                             Users ON People.PersonID = Users.PersonID";


            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query,connection))
            {

                try
                {
                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }

                    }


                }
                catch
                {

                }

                return dt;
            }

       }

    }
}