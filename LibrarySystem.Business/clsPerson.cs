using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Represents a person domain entity within the library management domain.
    /// Encapsulates personal identification attributes, validation invariants,
    /// persistence workflows (AddNew and Update modes), and data integrity rules.
    /// </summary>
    public class clsPerson
    {
        /// <summary>
        /// Defines the operational mode for persistence management.
        /// </summary>
        public enum enMode { AddNew = 0, Update = 1 }

        /// <summary>
        /// Gets the active operational persistence mode.
        /// </summary>
        public enMode Mode { get; private set; }

        /// <summary>
        /// Gets the unique identifier for the person (-1 if unsaved).
        /// </summary>
        public int PersonID { get; private set; }

        /// <summary>
        /// Gets or sets the person's given name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the person's family or surname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the primary contact telephone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the electronic mail address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Computed property returning the composite full name.
        /// </summary>
        public string FullName
        {
            get { return $"{FirstName} {LastName}".Trim(); }
        }

        /// <summary>
        /// Parameterless constructor initializing a new Person entity in AddNew mode.
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
        /// Private constructor for initializing an existing person entity in Update mode.
        /// </summary>
        private clsPerson(int personID, string firstName, string lastName, string phone, string email)
        {
            this.PersonID = personID;
            this.FirstName = firstName ?? string.Empty;
            this.LastName = lastName ?? string.Empty;
            this.Phone = phone ?? string.Empty;
            this.Email = email ?? string.Empty;

            this.Mode = enMode.Update;
        }

        /// <summary>
        /// Validates domain invariants before committing persistence operations.
        /// </summary>
        /// <returns>True if invariants are satisfied; otherwise, false.</returns>
        private bool _ValidateInvariants()
        {
            if (string.IsNullOrWhiteSpace(this.FirstName))
                return false;

            if (string.IsNullOrWhiteSpace(this.LastName))
                return false;

            return true;
        }

        /// <summary>
        /// Persists a new person record via the Data Access Layer.
        /// </summary>
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonDataAccess.AddNewPerson(
                this.FirstName.Trim(),
                this.LastName.Trim(),
                this.Phone != null ? this.Phone.Trim() : string.Empty,
                this.Email != null ? this.Email.Trim() : string.Empty);

            return (this.PersonID != -1);
        }

        /// <summary>
        /// Commits updates to an existing person record via the Data Access Layer.
        /// </summary>
        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(
                this.PersonID,
                this.FirstName.Trim(),
                this.LastName.Trim(),
                this.Phone != null ? this.Phone.Trim() : string.Empty,
                this.Email != null ? this.Email.Trim() : string.Empty);
        }

        /// <summary>
        /// Finds and hydrates a clsPerson entity by primary key ID.
        /// </summary>
        /// <param name="personID">The unique identifier of the target person.</param>
        /// <returns>A clsPerson instance if located; otherwise, null.</returns>
        public static clsPerson FindPerson(int personID)
        {
            if (personID <= 0)
                return null;

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
        /// Alias method providing standard Find convention by ID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>A clsPerson instance if found; otherwise, null.</returns>
        public static clsPerson Find(int personID)
        {
            return FindPerson(personID);
        }

        /// <summary>
        /// Finds and hydrates a clsPerson entity by unique phone number.
        /// </summary>
        /// <param name="phone">The phone number of the target person.</param>
        /// <returns>A clsPerson instance if located; otherwise, null.</returns>
        public static clsPerson FindPersonByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return null;

            int personID = -1;
            string firstName = string.Empty;
            string lastName = string.Empty;
            string email = string.Empty;

            if (clsPersonDataAccess.GetPersonInfoByPhone(phone.Trim(), ref personID, ref firstName, ref lastName, ref email))
            {
                return new clsPerson(personID, firstName, lastName, phone.Trim(), email);
            }

            return null;
        }

        /// <summary>
        /// Alias method for phone-based lookup.
        /// </summary>
        public static clsPerson FindByPhone(string phone)
        {
            return FindPersonByPhone(phone);
        }

        /// <summary>
        /// Validates invariants and persists the entity according to the active Mode.
        /// </summary>
        /// <returns>True if persistence was successful; otherwise, false.</returns>
        public bool Save()
        {
            if (!_ValidateInvariants())
                return false;

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
        /// Deletes a person record from the system by PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person to delete.</param>
        /// <returns>True if deletion succeeded; otherwise, false.</returns>
        public static bool Delete(int personID)
        {
            if (personID <= 0)
                return false;

            return clsPersonDataAccess.DeletePerson(personID);
        }

        /// <summary>
        /// Retrieves all people records for presentation grid binding.
        /// </summary>
        /// <returns>A DataTable containing all people records.</returns>
        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();
        }

        /// <summary>
        /// Verifies whether a person exists with the specified PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>True if found; otherwise, false.</returns>
        public static bool IsPersonExist(int personID)
        {
            if (personID <= 0)
                return false;

            return clsPersonDataAccess.IsPersonExist(personID);
        }

        /// <summary>
        /// Verifies whether a person exists with the specified phone number.
        /// </summary>
        /// <param name="phone">The telephone number to check.</param>
        /// <returns>True if found; otherwise, false.</returns>
        public static bool IsPersonExist(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return clsPersonDataAccess.IsPersonExist(phone.Trim());
        }
    }
}