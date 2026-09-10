using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Represents an authenticated system operator.
    /// Manages user credentials, active states, and bitwise access control permissions.
    /// </summary>
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; set; }

        /// <summary>
        /// Bitwise permission mask flags for fine-grained authorization control.
        /// </summary>
        [Flags]
        public enum enPermissions
        {
            eAll = -1,
            pManagePeople = 1,       // 2^0 = 1
            pManageUsers = 2,        // 2^1 = 2
            pManageBooks = 4,        // 2^2 = 4
            pBorrowing = 8,          // 2^3 = 8
            pFines = 16,             // 2^4 = 16
            pManageMembers = 32      // 2^5 = 32
        }

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }

        // Compatibility alias for UserName
        public string UserName
        {
            get { return Username; }
            set { Username = value; }
        }

        public string Password { get; set; }
        public int Permissions { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo { get; set; }

        public string FullName
        {
            get { return PersonInfo != null ? PersonInfo.FullName : string.Empty; }
        }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Permissions = 0;
            this.IsActive = true;
            this.PersonInfo = null;

            Mode = enMode.AddNew;
        }

        public clsUser(int userID, int personID, string username, string password, int permissions, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.Username = username;
            this.Password = password;
            this.Permissions = permissions;
            this.IsActive = isActive;
            this.PersonInfo = clsPerson.FindPerson(personID);

            Mode = enMode.Update;
        }

        public clsUser(int userID, int personID, string username, string password, bool isActive, int permissions)
            : this(userID, personID, username, password, permissions, isActive)
        {
        }

        /// <summary>
        /// Evaluates whether the user holds a specific bitwise permission mask.
        /// Returns true if the user has full access (eAll = -1) or contains the specified flag.
        /// </summary>
        /// <param name="permission">The permission flag to test.</param>
        public bool CheckAccessPermission(enPermissions permission)
        {
            if (this.Permissions == (int)enPermissions.eAll)
                return true;

            return ((this.Permissions & (int)permission) == (int)permission);
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserDataAccess.AddNewUser(this.PersonID, this.Username, this.Password, this.Permissions, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserDataAccess.UpdateUser(this.UserID, this.PersonID, this.Username, this.Password, this.Permissions, this.IsActive);
        }

        public static clsUser Find(int userID)
        {
            return FindByUserID(userID);
        }

        public static clsUser FindByUserID(int userID)
        {
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

        public static clsUser FindByPersonID(int personID)
        {
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

        public static clsUser FindByUsernameAndPassword(string username, string password)
        {
            return FindByUserNameAndPassword(username, password);
        }

        public static clsUser FindByUserNameAndPassword(string userName, string password)
        {
            int userID = -1;
            int personID = -1;
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByUsernameAndPassword(userName, password, ref userID, ref personID, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, userName, password, permissions, isActive);
            }

            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public bool ChangePassword(string newPassword)
        {
            bool isUpdated = clsUserDataAccess.ChangePassword(this.UserID, newPassword);

            if (isUpdated)
            {
                this.Password = newPassword;
                return true;
            }

            return false;
        }

        public static bool Delete(int userID)
        {
            return DeleteUser(userID);
        }

        public static bool DeleteUser(int userID)
        {
            return clsUserDataAccess.DeleteUser(userID);
        }

        public static bool Deactivate(int userID)
        {
            return clsUserDataAccess.DeactivateUser(userID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }

        public static bool IsUserExist(int userID)
        {
            return clsUserDataAccess.IsUserExist(userID);
        }

        public static bool IsUserExist(string username)
        {
            return clsUserDataAccess.IsUserExist(username);
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            return clsUserDataAccess.IsUserExistForPersonID(personID);
        }
    }
}