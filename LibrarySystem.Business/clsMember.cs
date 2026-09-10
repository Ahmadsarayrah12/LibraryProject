using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Represents a library member entity in the business domain.
    /// Combines membership state with the linked clsPerson details via object composition.
    /// </summary>
    public class clsMember
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int MemberID { get; set; }
        public int PersonID { get; set; }
        public DateTime SubscriptionDate { get; set; }

        // Composition property: provides direct access to the member's personal identity details
        public clsPerson PersonInfo { get; set; }

        /// <summary>
        /// Parameterless constructor initializing a new Member instance in AddNew mode.
        /// </summary>
        public clsMember()
        {
            this.MemberID = -1;
            this.PersonID = -1;
            this.SubscriptionDate = DateTime.Now;
            this.PersonInfo = null;

            Mode = enMode.AddNew;
        }

        /// <summary>
        /// Private constructor for initializing an existing member record in Update mode.
        /// Automatically resolves and attaches the associated PersonInfo instance.
        /// </summary>
        private clsMember(int memberID, int personID, DateTime subscriptionDate)
        {
            this.MemberID = memberID;
            this.PersonID = personID;
            this.SubscriptionDate = subscriptionDate;
            this.PersonInfo = clsPerson.FindPerson(personID);

            Mode = enMode.Update;
        }

        /// <summary>
        /// Persists a new member record to the database via DAL.
        /// </summary>
        private bool _AddNewMember()
        {
            this.MemberID = clsMemberDataAccess.AddNewMember(this.PersonID, this.SubscriptionDate);
            return (this.MemberID != -1);
        }

        /// <summary>
        /// Updates current member details in the database via DAL.
        /// </summary>
        private bool _UpdateMember()
        {
            return clsMemberDataAccess.UpdateMember(this.MemberID, this.PersonID, this.SubscriptionDate);
        }

        /// <summary>
        /// Finds and constructs a clsMember object by MemberID.
        /// </summary>
        public static clsMember Find(int memberID)
        {
            int personID = -1;
            DateTime subscriptionDate = DateTime.Now;

            if (clsMemberDataAccess.GetMemberInfoByID(memberID, ref personID, ref subscriptionDate))
            {
                return new clsMember(memberID, personID, subscriptionDate);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Finds and constructs a clsMember object by PersonID.
        /// </summary>
        public static clsMember FindByPersonID(int personID)
        {
            int memberID = -1;
            DateTime subscriptionDate = DateTime.Now;

            if (clsMemberDataAccess.GetMemberInfoByPersonID(personID, ref memberID, ref subscriptionDate))
            {
                return new clsMember(memberID, personID, subscriptionDate);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Unified persistence entry point. Routes to AddNew or Update based on the active Mode.
        /// </summary>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateMember();
            }

            return false;
        }

        /// <summary>
        /// Deletes a member record by MemberID.
        /// </summary>
        public static bool DeleteMember(int memberID)
        {
            return clsMemberDataAccess.DeleteMember(memberID);
        }

        /// <summary>
        /// Retrieves all library members along with personal details for grid binding.
        /// </summary>
        public static DataTable GetAllMembers()
        {
            return clsMemberDataAccess.GetAllMembers();
        }

        /// <summary>
        /// Checks if a member exists by MemberID.
        /// </summary>
        public static bool IsMemberExist(int memberID)
        {
            return clsMemberDataAccess.IsMemberExist(memberID);
        }

        /// <summary>
        /// Checks if a specific person is already registered as a library member.
        /// Prevents duplicate member registration for the same PersonID.
        /// </summary>
        public static bool IsMemberExistByPersonID(int personID)
        {
            return clsMemberDataAccess.IsMemberExistByPersonID(personID);
        }
    }
}