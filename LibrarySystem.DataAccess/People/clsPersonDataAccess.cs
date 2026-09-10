using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    public static class clsPersonDataAccess
    {
        public static bool GetPersonInfoByID(int personID, ref string firstName, ref string lastName, ref string phone, ref string email)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT FirstName, LastName, Phone, Email FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    
                    if (reader["FirstName"] != DBNull.Value)
                        firstName = (string)reader["FirstName"];
                    else
                        firstName = "";

                    if (reader["LastName"] != DBNull.Value)
                        lastName = (string)reader["LastName"];
                    else
                        lastName = "";

                    if (reader["Phone"] != DBNull.Value)
                        phone = (string)reader["Phone"];
                    else
                        phone = "";

                    if (reader["Email"] != DBNull.Value)
                        email = (string)reader["Email"];
                    else
                        email = "";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPersonInfoByPhone(string phone, ref int personID, ref string firstName, ref string lastName, ref string email)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT PersonID, FirstName, LastName, Email FROM People WHERE Phone = @Phone";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", phone);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    personID = (int)reader["PersonID"];

                    if (reader["FirstName"] != DBNull.Value)
                        firstName = (string)reader["FirstName"];
                    else
                        firstName = "";

                    if (reader["LastName"] != DBNull.Value)
                        lastName = (string)reader["LastName"];
                    else
                        lastName = "";

                    if (reader["Email"] != DBNull.Value)
                        email = (string)reader["Email"];
                    else
                        email = "";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewPerson(string firstName, string lastName, string phone, string email)
        {
            int personID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO People (FirstName, LastName, Phone, Email) VALUES (@FirstName, @LastName, @Phone, @Email); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);

            if (phone != "" && phone != null)
                command.Parameters.AddWithValue("@Phone", phone);
            else
                command.Parameters.AddWithValue("@Phone", DBNull.Value);

            if (email != "" && email != null)
                command.Parameters.AddWithValue("@Email", email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

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
            finally
            {
                connection.Close();
            }

            return personID;
        }

        public static bool UpdatePerson(int personID, string firstName, string lastName, string phone, string email)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE People SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @Email WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);

            if (phone != "" && phone != null)
                command.Parameters.AddWithValue("@Phone", phone);
            else
                command.Parameters.AddWithValue("@Phone", DBNull.Value);

            if (email != "" && email != null)
                command.Parameters.AddWithValue("@Email", email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // return empty table
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPersonExist(string phone)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE Phone = @Phone";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", phone);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
