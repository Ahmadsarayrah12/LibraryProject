using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Represents a person domain entity in the library system.
    /// Encapsulates identity attributes, persistence workflows (AddNew / Update),
    /// and queries against the underlying People data storage.
    /// </summary>
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Computed property returning the concatenated full name.
        /// </summary>
        public string FullName
        {
            get { return $"{FirstName} {LastName}".Trim(); }
        }

        /// <summary>
        /// Default constructor initializing a blank person object in AddNew mode.
        /// </summary>
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;

            this.Mode = enMode.AddNew;
        }

        /// <summary>
        /// Parameterized constructor initializing a person instance in Update mode.
        /// </summary>
        private clsPerson(int personID, string firstName, string lastName, string phone, string email)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Phone = phone;
            this.Email = email;

            this.Mode = enMode.Update;
        }

        /// <summary>
        /// Persists a new person record to the database via DAL.
        /// </summary>
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonDataAccess.AddNewPerson(this.FirstName, this.LastName, this.Phone, this.Email);
            return (this.PersonID != -1);
        }

        /// <summary>
        /// Updates the current person details in the database via DAL.
        /// </summary>
        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.Phone, this.Email);
        }

        /// <summary>
        /// Finds and instantiates a clsPerson entity by primary key (PersonID).
        /// </summary>
        /// <param name="personID">The ID of the target person.</param>
        /// <returns>A clsPerson instance if found; otherwise, null.</returns>
        public static clsPerson FindPerson(int personID)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string phone = string.Empty;
            string email = string.Empty;

            if (clsPersonDataAccess.GetPersonInfoByID(personID, ref firstName, ref lastName, ref phone, ref email))
            {
                return new clsPerson(personID, firstName, lastName, phone, email);
            }

            return null;
        }

        /// <summary>
        /// Finds and instantiates a clsPerson entity by unique phone number.
        /// </summary>
        /// <param name="phone">The phone number of the target person.</param>
        /// <returns>A clsPerson instance if found; otherwise, null.</returns>
        public static clsPerson FindPersonByPhone(string phone)
        {
            int personID = -1;
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = string.Empty;

            if (clsPersonDataAccess.GetPersonInfoByPhone(phone, ref personID, ref firstName, ref lastName, ref email))
            {
                return new clsPerson(personID, firstName, lastName, phone, email);
            }

            return null;
        }

        /// <summary>
        /// Saves the entity state by delegating to AddNew or Update based on the active Mode.
        /// </summary>
        /// <returns>True if persistence succeeded; otherwise, false.</returns>
        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdatePerson();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Deletes a person record by primary key.
        /// </summary>
        public static bool Delete(int personID)
        {
            return clsPersonDataAccess.DeletePerson(personID);
        }

        /// <summary>
        /// Retrieves all people records as a DataTable for grid binding.
        /// </summary>
        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();
        }

        /// <summary>
        /// Checks if a person exists with the specified PersonID.
        /// </summary>
        public static bool IsPersonExist(int personID)
        {
            return clsPersonDataAccess.IsPersonExist(personID);
        }

        /// <summary>
        /// Checks if a person exists with the specified Phone number.
        /// </summary>
        public static bool IsPersonExist(string phone)
        {
            return clsPersonDataAccess.IsPersonExist(phone);
        }
    }
}