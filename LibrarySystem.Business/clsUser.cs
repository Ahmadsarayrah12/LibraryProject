using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Represents an authenticated system operator in the Library Management System.
    /// Manages user credentials, bitwise authorization permissions, active states,
    /// and personal identity linkage.
    /// </summary>
    public class clsUser
    {
        /// <summary>
        /// Defines operational modes for user entity persistence.
        /// </summary>
        public enum enMode { AddNew = 0, Update = 1 }

        /// <summary>
        /// Gets the current operational persistence mode.
        /// </summary>
        public enMode Mode { get; private set; }

        /// <summary>
        /// Bitwise permission mask flags for fine-grained authorization control.
        /// </summary>
        [Flags]
        public enum enPermissions
        {
            /// <summary>
            /// Unrestricted administrative access across all system modules.
            /// </summary>
            eAll = -1,

            /// <summary>
            /// Permission to manage people records (2^0 = 1).
            /// </summary>
            pManagePeople = 1,

            /// <summary>
            /// Permission to manage system operator users (2^1 = 2).
            /// </summary>
            pManageUsers = 2,

            /// <summary>
            /// Permission to manage books and catalog inventory (2^2 = 4).
            /// </summary>
            pManageBooks = 4,

            /// <summary>
            /// Permission to issue and manage book borrowings (2^3 = 8).
            /// </summary>
            pManageBorrowing = 8,

            /// <summary>
            /// Alias for borrowing management permissions.
            /// </summary>
            pBorrowing = 8,

            /// <summary>
            /// Permission to assess and collect fines (2^4 = 16).
            /// </summary>
            pFines = 16,

            /// <summary>
            /// Permission to manage library members (2^5 = 32).
            /// </summary>
            pManageMembers = 32
        }

        /// <summary>
        /// Gets the unique identifier for the user account (-1 if unsaved).
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// Gets or sets the linked PersonID primary key.
        /// </summary>
        public int PersonID { get; set; }

        /// <summary>
        /// Gets or sets the login username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Compatibility alias property mirroring Username.
        /// </summary>
        public string UserName
        {
            get { return Username; }
            set { Username = value; }
        }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the accumulated bitwise permissions integer.
        /// </summary>
        public int Permissions { get; set; }

        /// <summary>
        /// Gets or sets whether the account is currently active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Composition reference to the personal details of the user.
        /// </summary>
        public clsPerson PersonInfo { get; set; }

        /// <summary>
        /// Gets the full name of the associated person.
        /// </summary>
        public string FullName
        {
            get { return PersonInfo != null ? PersonInfo.FullName : string.Empty; }
        }

        /// <summary>
        /// Parameterless constructor initializing a new User in AddNew mode.
        /// </summary>
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Permissions = 0;
            this.IsActive = true;
            this.PersonInfo = null;

            this.Mode = enMode.AddNew;
        }

        /// <summary>
        /// Parameterized constructor initializing an existing user in Update mode.
        /// </summary>
        public clsUser(int userID, int personID, string username, string password, int permissions, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.Username = username ?? string.Empty;
            this.Password = password ?? string.Empty;
            this.Permissions = permissions;
            this.IsActive = isActive;
            this.PersonInfo = clsPerson.FindPerson(personID);

            this.Mode = enMode.Update;
        }

        /// <summary>
        /// Overloaded constructor supporting alternate parameter ordering for backward compatibility.
        /// </summary>
        public clsUser(int userID, int personID, string username, string password, bool isActive, int permissions)
            : this(userID, personID, username, password, permissions, isActive)
        {
        }

        /// <summary>
        /// Evaluates whether the user holds a specific bitwise permission flag.
        /// Returns true if the user holds full access (eAll = -1) or contains the requested bit flag.
        /// </summary>
        /// <param name="permission">The permission flag to test.</param>
        /// <returns>True if access is permitted; otherwise, false.</returns>
        public bool CheckAccessPermission(enPermissions permission)
        {
            if (this.Permissions == (int)enPermissions.eAll)
                return true;

            return ((this.Permissions & (int)permission) == (int)permission);
        }

        /// <summary>
        /// Validates user invariants prior to persistence.
        /// </summary>
        /// <returns>True if invariants pass; otherwise, false.</returns>
        private bool _ValidateInvariants()
        {
            if (this.PersonID <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(this.Username))
                return false;

            if (string.IsNullOrWhiteSpace(this.Password))
                return false;

            return true;
        }

        /// <summary>
        /// Inserts a new user record via the Data Access Layer.
        /// </summary>
        private bool _AddNewUser()
        {
            this.UserID = clsUserDataAccess.AddNewUser(
                this.PersonID,
                this.Username.Trim(),
                this.Password.Trim(),
                this.Permissions,
                this.IsActive);

            return (this.UserID != -1);
        }

        /// <summary>
        /// Updates the current user record via the Data Access Layer.
        /// </summary>
        private bool _UpdateUser()
        {
            return clsUserDataAccess.UpdateUser(
                this.UserID,
                this.PersonID,
                this.Username.Trim(),
                this.Password.Trim(),
                this.Permissions,
                this.IsActive);
        }

        /// <summary>
        /// Finds and hydrates a clsUser entity by UserID.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A clsUser instance if found; otherwise, null.</returns>
        public static clsUser FindByUserID(int userID)
        {
            if (userID <= 0)
                return null;

            int personID = -1;
            string username = string.Empty;
            string password = string.Empty;
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByUserID(userID, ref personID, ref username, ref password, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, username, password, permissions, isActive);
            }

            return null;
        }

        /// <summary>
        /// Standard lookup alias by UserID.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A clsUser instance if found; otherwise, null.</returns>
        public static clsUser Find(int userID)
        {
            return FindByUserID(userID);
        }

        /// <summary>
        /// Finds and hydrates a clsUser entity by linked PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>A clsUser instance if found; otherwise, null.</returns>
        public static clsUser FindByPersonID(int personID)
        {
            if (personID <= 0)
                return null;

            int userID = -1;
            string username = string.Empty;
            string password = string.Empty;
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByPersonID(personID, ref userID, ref username, ref password, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, username, password, permissions, isActive);
            }

            return null;
        }

        /// <summary>
        /// Authenticates user credentials and returns the hydrated user upon match.
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="password">Login password.</param>
        /// <returns>A clsUser instance if authenticated; otherwise, null.</returns>
        public static clsUser FindByUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            int userID = -1;
            int personID = -1;
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByUsernameAndPassword(username.Trim(), password.Trim(), ref userID, ref personID, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, username.Trim(), password.Trim(), permissions, isActive);
            }

            return null;
        }

        /// <summary>
        /// Compatibility alias for credential authentication.
        /// </summary>
        public static clsUser FindByUserNameAndPassword(string userName, string password)
        {
            return FindByUsernameAndPassword(userName, password);
        }

        /// <summary>
        /// Commits user changes to the database based on the active Mode.
        /// </summary>
        /// <returns>True if save succeeded; otherwise, false.</returns>
        public bool Save()
        {
            if (!_ValidateInvariants())
                return false;

            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateUser();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Changes the user's password and updates the database immediately.
        /// </summary>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>True if password was updated; otherwise, false.</returns>
        public bool ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || this.UserID <= 0)
                return false;

            bool isUpdated = clsUserDataAccess.ChangePassword(this.UserID, newPassword.Trim());

            if (isUpdated)
            {
                this.Password = newPassword.Trim();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Permanently deletes a user from the system by UserID.
        /// </summary>
        /// <param name="userID">The unique identifier of the user to delete.</param>
        /// <returns>True if deletion succeeded; otherwise, false.</returns>
        public static bool Delete(int userID)
        {
            if (userID <= 0)
                return false;

            return clsUserDataAccess.DeleteUser(userID);
        }

        /// <summary>
        /// Alias method for deleting a user.
        /// </summary>
        public static bool DeleteUser(int userID)
        {
            return Delete(userID);
        }

        /// <summary>
        /// Deactivates a user account (soft disable) preventing further logins.
        /// </summary>
        /// <param name="userID">The unique identifier of the user to deactivate.</param>
        /// <returns>True if successfully deactivated; otherwise, false.</returns>
        public static bool Deactivate(int userID)
        {
            if (userID <= 0)
                return false;

            return clsUserDataAccess.DeactivateUser(userID);
        }

        /// <summary>
        /// Retrieves all user records joined with person information.
        /// </summary>
        /// <returns>A DataTable of users with full identity details.</returns>
        public static DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }

        /// <summary>
        /// Checks whether a user exists by UserID.
        /// </summary>
        public static bool IsUserExist(int userID)
        {
            if (userID <= 0)
                return false;

            return clsUserDataAccess.IsUserExist(userID);
        }

        /// <summary>
        /// Checks whether a user exists by username.
        /// </summary>
        public static bool IsUserExist(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            return clsUserDataAccess.IsUserExist(username.Trim());
        }

        /// <summary>
        /// Checks whether a user exists for the specified PersonID.
        /// </summary>
        public static bool IsUserExistForPersonID(int personID)
        {
            if (personID <= 0)
                return false;

            return clsUserDataAccess.IsUserExistForPersonID(personID);
        }
    }
}