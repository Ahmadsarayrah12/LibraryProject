using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Phone = "";
            this.Email = "";
            Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string firstName, string lastName, string phone, string email)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;
            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonDataAccess.AddNewPerson(this.FirstName, this.LastName, this.Phone, this.Email);
            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.Phone, this.Email);
        }

        public static clsPerson Find(int personID)
        {
            string firstName = "";
            string lastName = "";
            string phone = "";
            string email = "";

            if (clsPersonDataAccess.GetPersonInfoByID(personID, ref firstName, ref lastName, ref phone, ref email))
            {
                return new clsPerson(personID, firstName, lastName, phone, email);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string phone)
        {
            int personID = -1;
            string firstName = "";
            string lastName = "";
            string email = "";

            if (clsPersonDataAccess.GetPersonInfoByPhone(phone, ref personID, ref firstName, ref lastName, ref email))
            {
                return new clsPerson(personID, firstName, lastName, phone, email);
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
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();
        }

        public static bool DeletePerson(int personID)
        {
            return clsPersonDataAccess.DeletePerson(personID);
        }

        public static bool isPersonExist(int personID)
        {
            return clsPersonDataAccess.IsPersonExist(personID);
        }

        public static bool isPersonExist(string phone)
        {
            return clsPersonDataAccess.IsPersonExist(phone);
        }
    }
}