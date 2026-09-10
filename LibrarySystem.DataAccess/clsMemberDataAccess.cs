using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Handles low-level database operations against the Members table in SQL Server.
    /// Provides parameterized queries for CRUD operations, joins with the People table,
    /// and ensures secure data access with proper resource disposal.
    /// </summary>
    public static class clsMemberDataAccess
    {
        /// <summary>
        /// Retrieves member details by MemberID.
        /// </summary>
        public static bool GetMemberInfoByID(int memberID, ref int personID, ref DateTime subscriptionDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Members WHERE MemberID = @MemberID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", memberID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                personID = (int)reader["PersonID"];
                                subscriptionDate = (DateTime)reader["SubscriptionDate"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // In production, log error details to an audit log or Event Viewer
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Retrieves member details using the associated PersonID foreign key.
        /// </summary>
        public static bool GetMemberInfoByPersonID(int personID, ref int memberID, ref DateTime subscriptionDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Members WHERE PersonID = @PersonID";

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
                                memberID = (int)reader["MemberID"];
                                subscriptionDate = (DateTime)reader["SubscriptionDate"];
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
        /// Inserts a new member record linked to an existing Person entity and returns the generated MemberID.
        /// </summary>
        public static int AddNewMember(int personID, DateTime subscriptionDate)
        {
            int memberID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Members (PersonID, SubscriptionDate)
                                 VALUES (@PersonID, @SubscriptionDate);
                                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@SubscriptionDate", subscriptionDate);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            memberID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        memberID = -1;
                    }
                }
            }

            return memberID;
        }

        /// <summary>
        /// Updates an existing member record in the Members table.
        /// </summary>
        public static bool UpdateMember(int memberID, int personID, DateTime subscriptionDate)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Members
                                 SET PersonID = @PersonID,
                                     SubscriptionDate = @SubscriptionDate
                                 WHERE MemberID = @MemberID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", memberID);
                    command.Parameters.AddWithValue("@PersonID", personID);
                    command.Parameters.AddWithValue("@SubscriptionDate", subscriptionDate);

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
        /// Deletes a member record by MemberID.
        /// </summary>
        public static bool DeleteMember(int memberID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Members WHERE MemberID = @MemberID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", memberID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Will return false if foreign key constraints are violated (e.g., active borrowing records)
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Retrieves all members joined with their personal details from the People table.
        /// Formats full names, contact info, and subscription dates for grid rendering.
        /// </summary>
        public static DataTable GetAllMembers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 
                                    Members.MemberID, 
                                    Members.PersonID, 
                                    (People.FirstName + ' ' + People.LastName) AS FullName,
                                    People.Phone, 
                                    People.Email, 
                                    Members.SubscriptionDate
                                 FROM Members 
                                 INNER JOIN People ON Members.PersonID = People.PersonID
                                 ORDER BY Members.MemberID DESC";

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
                        // Return empty DataTable on error
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Checks whether a member exists by MemberID.
        /// </summary>
        public static bool IsMemberExist(int memberID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM Members WHERE MemberID = @MemberID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", memberID);

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
        /// Checks if a Person is already registered as a library member.
        /// </summary>
        public static bool IsMemberExistByPersonID(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM Members WHERE PersonID = @PersonID";

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
    }
}