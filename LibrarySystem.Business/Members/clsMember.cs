using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsMember
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int MemberID { get; set; }
        public int PersonID { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public clsPerson PersonInfo { get; set; }

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

        public clsMember()
        {
            this.MemberID = -1;
            this.PersonID = -1;
            this.SubscriptionDate = DateTime.Now;
            this.PersonInfo = null;

            this.Mode = enMode.AddNew;
        }

        private clsMember(int memberID, int personID, DateTime subscriptionDate)
        {
            this.MemberID = memberID;
            this.PersonID = personID;
            this.SubscriptionDate = subscriptionDate;
            this.PersonInfo = clsPerson.Find(personID);

            this.Mode = enMode.Update;
        }

        private bool _AddNewMember()
        {
            this.MemberID = clsMemberDataAccess.AddNewMember(this.PersonID, this.SubscriptionDate);
            return (this.MemberID != -1);
        }

        private bool _UpdateMember()
        {
            return clsMemberDataAccess.UpdateMember(this.MemberID, this.PersonID, this.SubscriptionDate);
        }

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

        public static DataTable GetAllMembers()
        {
            return clsMemberDataAccess.GetAllMembers();
        }

        public static bool DeleteMember(int memberID)
        {
            return clsMemberDataAccess.DeleteMember(memberID);
        }

        public static bool IsMemberExist(int memberID)
        {
            return clsMemberDataAccess.IsMemberExist(memberID);
        }

        public static bool IsMemberExistByPersonID(int personID)
        {
            return clsMemberDataAccess.IsMemberExistForPersonID(personID);
        }
    }
}