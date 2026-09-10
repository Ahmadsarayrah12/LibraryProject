using System;
using System.Data;
using System.Data.SqlClient;

namespace LibrarySystem.DataAccess
{
    /// <summary>
    /// Handles low-level parameterized database operations against the Members table in SQL Server.
    /// Provides CRUD operations, relational joins with the People table, and membership checks.
    /// </summary>
    public static class clsMemberDataAccess
    {
        /// <summary>
        /// Retrieves member details by primary key (MemberID).
        /// </summary>
        /// <param name="memberID">The unique identifier of the member.</param>
        /// <param name="personID">Output parameter receiving the linked PersonID.</param>
        /// <param name="subscriptionDate">Output parameter receiving the subscription date.</param>
        /// <returns>True if the member record was found; otherwise, false.</returns>
        public static bool GetMemberInfoByID(int memberID, ref int personID, ref DateTime subscriptionDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT PersonID, SubscriptionDate 
                                       FROM Members 
                                       WHERE MemberID = @MemberID;";

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
                                personID = reader.SafeGetInt("PersonID");
                                subscriptionDate = reader.SafeGetDateTime("SubscriptionDate");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetMemberInfoByID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Retrieves member details using the associated PersonID foreign key.
        /// </summary>
        /// <param name="personID">The unique identifier of the associated person.</param>
        /// <param name="memberID">Output parameter receiving the MemberID.</param>
        /// <param name="subscriptionDate">Output parameter receiving the subscription date.</param>
        /// <returns>True if a member record exists for the given person; otherwise, false.</returns>
        public static bool GetMemberInfoByPersonID(int personID, ref int memberID, ref DateTime subscriptionDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT MemberID, SubscriptionDate 
                                       FROM Members 
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
                                memberID = reader.SafeGetInt("MemberID");
                                subscriptionDate = reader.SafeGetDateTime("SubscriptionDate");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsDataLogger.LogError(ex, nameof(GetMemberInfoByPersonID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Inserts a new member record linked to an existing Person entity and returns the generated MemberID.
        /// </summary>
        /// <param name="personID">The primary key ID of the associated person.</param>
        /// <param name="subscriptionDate">The date of membership enrollment.</param>
        /// <returns>The newly generated MemberID upon success; otherwise, -1.</returns>
        public static int AddNewMember(int personID, DateTime subscriptionDate)
        {
            int memberID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"INSERT INTO Members (PersonID, SubscriptionDate)
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
                        clsDataLogger.LogError(ex, nameof(AddNewMember));
                        memberID = -1;
                    }
                }
            }

            return memberID;
        }

        /// <summary>
        /// Updates an existing member record in the Members table.
        /// </summary>
        /// <param name="memberID">The primary key ID of the member to update.</param>
        /// <param name="personID">The associated PersonID.</param>
        /// <param name="subscriptionDate">The updated subscription date.</param>
        /// <returns>True if rows were affected; otherwise, false.</returns>
        public static bool UpdateMember(int memberID, int personID, DateTime subscriptionDate)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"UPDATE Members
                                       SET PersonID = @PersonID,
                                           SubscriptionDate = @SubscriptionDate
                                       WHERE MemberID = @MemberID;";

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
                        clsDataLogger.LogError(ex, nameof(UpdateMember));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Deletes a member record from the Members table by MemberID.
        /// </summary>
        /// <param name="memberID">The primary key ID of the member to delete.</param>
        /// <returns>True if deleted; false if constraints prevent deletion or an error occurs.</returns>
        public static bool DeleteMember(int memberID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"DELETE FROM Members WHERE MemberID = @MemberID;";

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
                        clsDataLogger.LogError(ex, nameof(DeleteMember));
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// Retrieves all members joined with their personal identity details from the People table.
        /// Formats full names, contact info, and subscription dates for presentation binding.
        /// </summary>
        /// <returns>A DataTable containing all member records with personal identity details.</returns>
        public static DataTable GetAllMembers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 
                                        Members.MemberID, 
                                        Members.PersonID, 
                                        (People.FirstName + ' ' + People.LastName) AS FullName,
                                        People.Phone, 
                                        People.Email, 
                                        Members.SubscriptionDate
                                     FROM Members 
                                     INNER JOIN People ON Members.PersonID = People.PersonID
                                     ORDER BY Members.MemberID DESC;";

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
                        clsDataLogger.LogError(ex, nameof(GetAllMembers));
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Checks whether a member exists by MemberID.
        /// </summary>
        /// <param name="memberID">The unique identifier of the member.</param>
        /// <returns>True if the member exists; otherwise, false.</returns>
        public static bool IsMemberExist(int memberID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM Members WHERE MemberID = @MemberID;";

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
                        clsDataLogger.LogError(ex, nameof(IsMemberExist));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        /// <summary>
        /// Checks if a Person is already registered as a library member.
        /// </summary>
        /// <param name="personID">The unique identifier of the person to test.</param>
        /// <returns>True if a membership already exists for the given person; otherwise, false.</returns>
        public static bool IsMemberExistByPersonID(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                const string query = @"SELECT 1 FROM Members WHERE PersonID = @PersonID;";

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
                        clsDataLogger.LogError(ex, nameof(IsMemberExistByPersonID));
                        isFound = false;
                    }
                }
            }

            return isFound;
        }
    }
}