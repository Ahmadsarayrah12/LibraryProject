using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public class clsDataAccessLayerPeople
    {
        // 1. Read (Get By ID)
        public static bool GetPersonInfoByID(int personID, ref string firstName,
            ref string lastName, ref string phone, ref string email)
        {
            bool isFound = false;
            string query = @"SELECT FirstName, LastName, Phone, Email 
                             FROM People 
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
                            firstName = (string)reader["FirstName"];
                            lastName = (string)reader["LastName"];
                            phone = (reader["Phone"] != DBNull.Value) ? (string)reader["Phone"] : "";
                            email = (reader["Email"] != DBNull.Value) ? (string)reader["Email"] : "";
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

        // 2. Create (AddNew)
        public static int AddNewPerson(string firstName, string lastName, string phone, string email)
        {
            int personID = -1;
            string query = @"INSERT INTO People (FirstName, LastName, Phone, Email)
                             VALUES (@FirstName, @LastName, @Phone, @Email);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);

                if (string.IsNullOrEmpty(phone))
                    command.Parameters.AddWithValue("@Phone", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Phone", phone);

                if (string.IsNullOrEmpty(email))
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        personID = insertedID;
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            return personID;
        }

        // 3. Update
        public static bool UpdatePerson(int personID, string firstName, string lastName, string phone, string email)
        {
            int rowsAffected = 0;
            string query = @"UPDATE People 
                             SET FirstName = @FirstName,
                                 LastName  = @LastName,
                                 Phone     = @Phone,
                                 Email     = @Email
                             WHERE PersonID = @PersonID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);

                if (string.IsNullOrEmpty(phone))
                    command.Parameters.AddWithValue("@Phone", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Phone", phone);

                if (string.IsNullOrEmpty(email))
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Email", email);

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

        // 4. Delete
        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM People WHERE PersonID = @PersonID;";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", personID);

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

        // 5. Read All (DataTable)
        

        // 6. Check Existence
        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;
            string query = @"SELECT 1 FROM People WHERE PersonID = @PersonID;";

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT 
                        Users.UserID,
                        Users.PersonID,
                        (People.FirstName + ' ' + People.LastName) AS FullName,
                        People.Phone,
                        People.Email,
                        Users.Username,
                        Users.Permissions,
                        Users.IsActive
                     FROM Users
                     INNER JOIN People ON Users.PersonID = People.PersonID
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

                }
            }
            return dt;
        }


        public static bool ChangePassword(int UserID ,string NewPassword)
        {
            bool isFound = false;

            string query = @"UPDATE [dbo].[Users]
                             SET  
       
                            [Password] =  @NewPassword
      
                             WHERE  UserID = @UserID;";


            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command =new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@NewPassword", NewPassword);
                command.Parameters.AddWithValue("@UserID", UserID);


                try
                {
                    connection.Open();
                  int RowEfferted= command.ExecuteNonQuery();


                    isFound = (RowEfferted > 0);
                }
                catch
                {
                    return false;
                }

            }
            return isFound;
        }


    }
}