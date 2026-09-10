using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Represents a library member domain entity within the system.
    /// Manages membership enrollment, subscription validity, relational composition
    /// with the underlying clsPerson entity, and duplicate enrollment invariants.
    /// </summary>
    public class clsMember
    {
        /// <summary>
        /// Defines operational modes for member entity persistence.
        /// </summary>
        public enum enMode { AddNew = 0, Update = 1 }

        /// <summary>
        /// Gets the current operational persistence mode.
        /// </summary>
        public enMode Mode { get; private set; }

        /// <summary>
        /// Gets the unique identifier for the member (-1 if unsaved).
        /// </summary>
        public int MemberID { get; private set; }

        /// <summary>
        /// Gets or sets the associated PersonID foreign key.
        /// </summary>
        public int PersonID { get; set; }

        /// <summary>
        /// Gets or sets the date of membership enrollment.
        /// </summary>
        public DateTime SubscriptionDate { get; set; }

        /// <summary>
        /// Composition property providing direct access to the member's personal identity details.
        /// </summary>
        public clsPerson PersonInfo { get; set; }

        /// <summary>
        /// Gets the composite full name of the member.
        /// </summary>
        public string FullName
        {
            get { return PersonInfo != null ? PersonInfo.FullName : string.Empty; }
        }

        /// <summary>
        /// Parameterless constructor initializing a new Member instance in AddNew mode.
        /// </summary>
        public clsMember()
        {
            this.MemberID = -1;
            this.PersonID = -1;
            this.SubscriptionDate = DateTime.Now;
            this.PersonInfo = null;

            this.Mode = enMode.AddNew;
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

            this.Mode = enMode.Update;
        }

        /// <summary>
        /// Validates member invariants before committing persistence operations.
        /// </summary>
        /// <returns>True if invariants are satisfied; otherwise, false.</returns>
        private bool _ValidateInvariants()
        {
            if (this.PersonID <= 0)
                return false;

            // In AddNew mode, enforce one membership per person
            if (this.Mode == enMode.AddNew && IsMemberExistByPersonID(this.PersonID))
                return false;

            return true;
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
        /// <param name="memberID">The unique identifier of the member.</param>
        /// <returns>A clsMember instance if found; otherwise, null.</returns>
        public static clsMember Find(int memberID)
        {
            if (memberID <= 0)
                return null;

            int personID = -1;
            DateTime subscriptionDate = DateTime.Now;

            if (clsMemberDataAccess.GetMemberInfoByID(memberID, ref personID, ref subscriptionDate))
            {
                return new clsMember(memberID, personID, subscriptionDate);
            }

            return null;
        }

        /// <summary>
        /// Finds and constructs a clsMember object by associated PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the associated person.</param>
        /// <returns>A clsMember instance if found; otherwise, null.</returns>
        public static clsMember FindByPersonID(int personID)
        {
            if (personID <= 0)
                return null;

            int memberID = -1;
            DateTime subscriptionDate = DateTime.Now;

            if (clsMemberDataAccess.GetMemberInfoByPersonID(personID, ref memberID, ref subscriptionDate))
            {
                return new clsMember(memberID, personID, subscriptionDate);
            }

            return null;
        }

        /// <summary>
        /// Unified persistence entry point. Validates invariants and commits based on the active Mode.
        /// </summary>
        /// <returns>True if save succeeded; otherwise, false.</returns>
        public bool Save()
        {
            if (!_ValidateInvariants())
                return false;

            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateMember();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Deletes a member record by primary key (MemberID).
        /// </summary>
        /// <param name="memberID">The unique identifier of the member to delete.</param>
        /// <returns>True if deletion succeeded; otherwise, false.</returns>
        public static bool DeleteMember(int memberID)
        {
            if (memberID <= 0)
                return false;

            return clsMemberDataAccess.DeleteMember(memberID);
        }

        /// <summary>
        /// Alias method for deleting a member.
        /// </summary>
        public static bool Delete(int memberID)
        {
            return DeleteMember(memberID);
        }

        /// <summary>
        /// Retrieves all library members along with personal details for grid presentation.
        /// </summary>
        /// <returns>A DataTable containing all members with joined personal details.</returns>
        public static DataTable GetAllMembers()
        {
            return clsMemberDataAccess.GetAllMembers();
        }

        /// <summary>
        /// Checks if a member exists by MemberID.
        /// </summary>
        /// <param name="memberID">The unique identifier of the member.</param>
        /// <returns>True if the member exists; otherwise, false.</returns>
        public static bool IsMemberExist(int memberID)
        {
            if (memberID <= 0)
                return false;

            return clsMemberDataAccess.IsMemberExist(memberID);
        }

        /// <summary>
        /// Checks if a specific person is already registered as a library member.
        /// Prevents duplicate member registration for the same PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>True if a membership exists for this person; otherwise, false.</returns>
        public static bool IsMemberExistByPersonID(int personID)
        {
            if (personID <= 0)
                return false;

            return clsMemberDataAccess.IsMemberExistByPersonID(personID);
        }
    }
}