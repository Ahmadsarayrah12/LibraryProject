using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Pure domain entity representing a literary author in the library system.
    /// Encapsulates biographical data, invariant validation, and mode management.
    /// </summary>
    public class clsAuthor
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        public int AuthorID { get; private set; } = -1;
        public string FullName { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;

        public enMode Mode => _mode;

        /// <summary>
        /// Initializes a new author entity in AddNew mode.
        /// </summary>
        public clsAuthor()
        {
            this.AuthorID = -1;
            this.FullName = string.Empty;
            this.Biography = string.Empty;
            this._mode = enMode.AddNew;
        }

        private clsAuthor(int authorID, string fullName, string biography)
        {
            this.AuthorID = authorID;
            this.FullName = fullName;
            this.Biography = biography;
            this._mode = enMode.Update;
        }

        /// <summary>
        /// Finds and hydrates an author by primary key.
        /// </summary>
        public static clsAuthor Find(int authorID)
        {
            string fullName = string.Empty;
            string biography = string.Empty;

            if (clsAuthorDataAccess.GetAuthorInfoByID(authorID, ref fullName, ref biography))
            {
                return new clsAuthor(authorID, fullName, biography);
            }

            return null;
        }

        private bool _AddNewAuthor()
        {
            this.AuthorID = clsAuthorDataAccess.AddNewAuthor(this.FullName, this.Biography);
            return this.AuthorID != -1;
        }

        private bool _UpdateAuthor()
        {
            return clsAuthorDataAccess.UpdateAuthor(this.AuthorID, this.FullName, this.Biography);
        }

        /// <summary>
        /// Validates invariants and persists author state to storage.
        /// </summary>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.FullName))
                throw new InvalidOperationException("Author full name cannot be empty or whitespace.");

            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewAuthor())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateAuthor();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Deletes an author by ID.
        /// </summary>
        public static bool Delete(int authorID)
        {
            return clsAuthorDataAccess.DeleteAuthor(authorID);
        }

        /// <summary>
        /// Checks if an author exists by ID.
        /// </summary>
        public static bool IsAuthorExist(int authorID)
        {
            return clsAuthorDataAccess.IsAuthorExist(authorID);
        }

        /// <summary>
        /// Retrieves all authors for binding to UI selection controls.
        /// </summary>
        public static DataTable GetAllAuthors()
        {
            return clsAuthorDataAccess.GetAllAuthors();
        }
    }
}
