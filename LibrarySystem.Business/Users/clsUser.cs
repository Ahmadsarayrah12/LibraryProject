using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enPermissions
        {
            eAll = -1,
            pManageBooks = 1,
            pManageMembers = 2,
            pManageBorrowing = 4,
            pManageUsers = 8,
            pManagePeople = 16,
            pFines = 32
        }

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }
        public bool IsActive { get; set; }

        public string FullName
        {
            get
            {
                if (PersonInfo != null)
                    return PersonInfo.FullName;
                else
                    return "";
            }
        }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.Permissions = 0;
            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string username, string password, int permissions, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.Username = username;
            this.Password = password;
            this.Permissions = permissions;
            this.IsActive = isActive;
            Mode = enMode.Update;
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

        public static clsUser FindByUserID(int userID)
        {
            int personID = -1;
            string username = "";
            string password = "";
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByUserID(userID, ref personID, ref username, ref password, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, username, password, permissions, isActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByPersonID(int personID)
        {
            int userID = -1;
            string username = "";
            string password = "";
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByPersonID(personID, ref userID, ref username, ref password, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, username, password, permissions, isActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByUsernameAndPassword(string username, string password)
        {
            int userID = -1;
            int personID = -1;
            int permissions = 0;
            bool isActive = false;

            if (clsUserDataAccess.GetUserInfoByUsernameAndPassword(username, password, ref userID, ref personID, ref permissions, ref isActive))
            {
                return new clsUser(userID, personID, username, password, permissions, isActive);
            }
            else
            {
                return null;
            }
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
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }

        public static bool DeleteUser(int userID)
        {
            return clsUserDataAccess.DeleteUser(userID);
        }

        public static bool isUserExist(int userID)
        {
            return clsUserDataAccess.IsUserExist(userID);
        }

        public static bool isUserExist(string username)
        {
            return clsUserDataAccess.IsUserExist(username);
        }

        public static bool isUserExistForPersonID(int personID)
        {
            return clsUserDataAccess.IsUserExistForPersonID(personID);
        }

        public bool ChangePassword(string newPassword)
        {
            return clsUserDataAccess.ChangePassword(this.UserID, newPassword);
        }

        public bool CheckAccessPermission(enPermissions permission)
        {
            if (this.Permissions == (int)enPermissions.eAll)
                return true;

            if ((this.Permissions & (int)permission) == (int)permission)
                return true;
            else
                return false;
        }
    }
}