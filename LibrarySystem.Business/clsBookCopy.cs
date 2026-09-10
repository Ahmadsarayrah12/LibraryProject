using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Pure domain entity representing a physical copy of a book in the library inventory.
    /// Manages physical tracking and lifecycle availability states.
    /// </summary>
    public class clsBookCopy
    {
        public enum enCopyStatus { Available = 1, Borrowed = 2, Lost = 3 }
        public enum enMode { AddNew = 0, Update = 1 }

        private enMode _mode = enMode.AddNew;

        public int CopyID { get; private set; } = -1;
        public int BookID { get; set; } = -1;
        public enCopyStatus Status { get; set; } = enCopyStatus.Available;

        public enMode Mode => _mode;

        public clsBookCopy()
        {
            this.CopyID = -1;
            this.BookID = -1;
            this.Status = enCopyStatus.Available;
            this._mode = enMode.AddNew;
        }

        public clsBookCopy(int copyID, int bookID, enCopyStatus status)
        {
            this.CopyID = copyID;
            this.BookID = bookID;
            this.Status = status;
            this._mode = enMode.Update;
        }

        /// <summary>
        /// Finds and constructs a clsBookCopy instance by CopyID.
        /// </summary>
        public static clsBookCopy Find(int copyID)
        {
            if (copyID <= 0)
                return null;

            int bookID = -1;
            byte status = 1;

            if (clsBookCopyDataAccess.GetCopyInfoByID(copyID, ref bookID, ref status))
            {
                return new clsBookCopy(copyID, bookID, (enCopyStatus)status);
            }

            return null;
        }

        private bool _AddNewCopy()
        {
            this.CopyID = clsBookCopyDataAccess.AddNewCopy(this.BookID, (byte)this.Status);
            return this.CopyID != -1;
        }

        private bool _UpdateCopy()
        {
            return clsBookCopyDataAccess.UpdateCopyStatus(this.CopyID, (byte)this.Status);
        }

        /// <summary>
        /// Persists copy state to the database.
        /// </summary>
        public bool Save()
        {
            if (this.BookID <= 0)
                throw new InvalidOperationException("Valid BookID must be specified for a book copy.");

            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewCopy())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateCopy();

                default:
                    return false;
            }
        }

        /// <summary>
        /// Inserts multiple copies in a single high-speed database roundtrip.
        /// </summary>
        public static bool AddCopies(int bookID, int count, enCopyStatus status = enCopyStatus.Available)
        {
            return clsBookCopyDataAccess.AddCopies(bookID, count, (byte)status);
        }

        /// <summary>
        /// Directly updates the status of an existing copy.
        /// </summary>
        public static bool UpdateStatus(int copyID, enCopyStatus newStatus)
        {
            return clsBookCopyDataAccess.UpdateCopyStatus(copyID, (byte)newStatus);
        }

        /// <summary>
        /// Retrieves the primary key of the first available copy for a book.
        /// </summary>
        public static int GetFirstAvailableCopy(int bookID)
        {
            return clsBookCopyDataAccess.GetAvailableCopyID(bookID);
        }

        /// <summary>
        /// Retrieves total and available copy counts for a specified book.
        /// </summary>
        public static bool GetCopiesCount(int bookID, ref int total, ref int available)
        {
            return clsBookCopyDataAccess.GetCopiesCountByBookID(bookID, ref total, ref available);
        }

        /// <summary>
        /// Returns all physical copies of a book.
        /// </summary>
        public static DataTable GetBookCopies(int bookID)
        {
            return clsBookCopyDataAccess.GetCopiesByBookID(bookID);
        }

        /// <summary>
        /// Checks if a book has active unreturned borrowings.
        /// </summary>
        public static bool HasActiveBorrowings(int bookID)
        {
            return clsBookCopyDataAccess.HasActiveBorrowings(bookID);
        }
    }
}
