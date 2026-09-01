using System;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public class clsDataAccessLayerUsers
    {
        public static bool Login(string username, string password,
            ref int userID, ref int personID, ref bool isActive)
        {
            bool isFound = false;

            string query = @"SELECT UserID, PersonID, IsActive 
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

                            // التعامل الآمن مع احتمالية وجود قيم NULL
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
    }
}