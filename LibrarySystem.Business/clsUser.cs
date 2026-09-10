using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;

        [Flags]
        public enum enPermissions
        {
            eAll = -1,
            pManagePeople = 1,
            pManageUsers = 2,
            pManageBooks = 4,
            pBorrowing = 8,
            pFines = 16
        }

        public int UserID { get; private set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permissions { get; set; }

         public clsPerson PersonInfo { get; set; }


        public string FullName => $"{PersonInfo.FirstName} {PersonInfo.LastName}";
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.IsActive = true;
            this.Permissions = -1;
            this.PersonInfo = null;

            this.Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string username, string password, bool isActive, int permissions)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
            this.Permissions = permissions;
            this.PersonInfo = clsPerson.FindPerson(personID);

            this.Mode = enMode.Update;
        }

        public static clsUser Find(int userID)
        {
            int personID = -1;
            string username = "";
            string password = "";
            bool isActive = false;
            int permissions = -1;

            bool isFound = clsDataAccessLayerUsers.GetUserByID(
                userID, ref personID, ref username, ref password, ref isActive, ref permissions);

            if (isFound)
            {
                return new clsUser(userID, personID, username, password, isActive, permissions);
            }

            return null;
        }

        public static clsUser FindByPersonID(int personID)
        {
            int userID = -1;
            string username = "";
            string password = "";
            bool isActive = false;
            int permissions = -1;

            bool isFound = clsDataAccessLayerUsers.GetUserInfoByPersonID(
                personID, ref userID, ref username, ref password, ref isActive, ref permissions);

            if (isFound)
            {
                return new clsUser(userID, personID, username, password, isActive, permissions);
            }

            return null;
        }

        public static clsUser FindByUsernameAndPassword(string username, string password)
        {
            int userID = -1;
            int personID = -1;
            bool isActive = false;
            int permissions = -1;

            bool isFound = clsDataAccessLayerUsers.GetUserInfoByUsernameAndPassword(
                username, password, ref userID, ref personID, ref isActive, ref permissions);

            if (isFound)
            {
                return new clsUser(userID, personID, username, password, isActive, permissions);
            }

            return null;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsDataAccessLayerUsers.AddNewUser(
                this.PersonID, this.Username, this.Password, this.IsActive, this.Permissions);

            return (this.UserID > 0);
        }

        private bool _UpdateUser()
        {
            return clsDataAccessLayerUsers.UpdateUser(
                this.UserID, this.Username, this.Password, this.IsActive, this.Permissions);
        }

        public bool Save()
        {
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

        public bool ChangePassword(string newPassword)
        {
            bool isUpdated = clsDataAccessLayerUsers.ChangePassword(this.UserID, newPassword);

            if (isUpdated)
            {
                this.Password = newPassword;
                return true;
            }

            return false;
        }

        public bool CheckAccessPermission(enPermissions permission)
        {
            if (this.Permissions == (int)enPermissions.eAll)
                return true;

            return ((this.Permissions & (int)permission) == (int)permission);
        }

        public static bool Delete(int userID)
        {
            return clsDataAccessLayerUsers.DeleteUser(userID);
        }

        public static bool Deactivate(int userID)
        {
            return clsDataAccessLayerUsers.DeactivateUser(userID);
        }

        public static DataTable GetAllUsers()
        {
            return clsDataAccessLayerUsers.GetAllUsers();
        }

        public static bool IsUserExist(int userID)
        {
            return clsDataAccessLayerUsers.IsUserExist(userID);
        }

        public static bool IsUserExist(string username)
        {
            return clsDataAccessLayerUsers.IsUserExist(username);
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            return clsDataAccessLayerUsers.IsUserExistForPersonID(personID);
        }
    }
}