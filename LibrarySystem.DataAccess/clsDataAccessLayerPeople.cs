using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.DataAccess
{
    public class clsDataAccessLayerPeople
    {
 
 

         enum enMode  {AddNew=1,Update=2};
       public int PersonID {  get; private set; }
       public string FirstName { get;  set; }

        public string LastName { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }


        private enMode _Mode=enMode.AddNew;
       public clsDataAccessLayerPeople()
        {
            this.PersonID = -1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this._Mode = enMode.AddNew;
 
        }
        public clsDataAccessLayerPeople(string FirstName,string LastName,string Phone,string Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Email = Email;
            this._Mode = enMode.Update;


        }

 
 
         
        public static bool GetPersonInfoByID(int personID, ref string firstName,
            ref string lastName, ref string phone, ref string email)
        {
            bool isFound = false;

            string query = @"SELECT [FirstName]
                                ,[LastName]
                                ,[Phone]
                                ,[Email]
                            FROM [dbo].[People]
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

        public static int AddNewPerson(string firstName ,string lastName,string phone,string email)
        {
            int personID = -1;

            string Query = @"INSERT INTO [dbo].[People]
                            ( FirstName 
                           , LastName 
                           , Phone 
                           , Email )
                            VALUES
                           (@FirstName,@LastName,@Phone,@Email) 
                           SELECT SCOPE_IDENTITY()  ";

            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, connection))
            {

                command.Parameters.AddWithValue("@FirstName", firstName);

                command.Parameters.AddWithValue("@LastName", lastName);
                
            

                if(string.IsNullOrEmpty( phone))
                    command.Parameters.AddWithValue("@Phone", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Phone", phone);

                if(string.IsNullOrEmpty( email))
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Email", email);



                try
                {
                    connection.Open();
                    
                    Object result = command.ExecuteScalar();

                     

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        personID = insertedID;  
                    }

                }
                catch
                {
                    return -1;
                }


            }
            return personID;

        }


        public static bool UpdatePerson(int personID,string firstName,string lastName,string phone,string email)
        {

             int rowAffected = -1;

            string Query = @"UPDATE [dbo].[People]
                            SET [FirstName] =  @FirstName 
                            ,[LastName] =  @LastName 
                            ,[Phone] =  @Phone 
                            ,[Email] =  @Email 
                            WHERE PersonID =@PersonID";


            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, connection)) {
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@FirstName", firstName);

               command.Parameters.AddWithValue("@LastName", lastName);


                if(string.IsNullOrEmpty(phone))
                    command.Parameters.AddWithValue("@Phone", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Phone", phone);

                if(string.IsNullOrEmpty(email))
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Email", email);

                try
                {
                    connection.Open();

                     rowAffected= command.ExecuteNonQuery();

                    

                }
                catch
                {
                    return false;

                }
            
            
            }
            return (rowAffected > 0);

        }

        public static bool DeletePerson(int personID )
        {
            int rowAffected = -1;


            string query = @"DELETE FROM [dbo].[People]
                       WHERE PersonID= @PersonID";


            using (SqlConnection connection = new SqlConnection(clsSettingsDataAccessLayer.ConnectionString)) 
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", personID);

                try
                {
                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();

                }
                catch
                {
                    rowAffected = -1;

                }


            }
            return (rowAffected > 0);
     


        } 


    }


}




 
