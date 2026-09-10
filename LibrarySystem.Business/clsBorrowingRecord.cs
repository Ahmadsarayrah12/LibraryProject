using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    /// <summary>
    /// Core domain entity encapsulating the lifecycle of a book borrowing transaction.
    /// Enforces lending invariants: member eligibility, physical copy availability,
    /// concurrent active loan ceilings, and atomic inventory state transitions.
    /// </summary>
    public class clsBorrowingRecord
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _mode = enMode.AddNew;

        /// <summary>
        /// Maximum allowed concurrent unreturned book loans per member.
        /// </summary>
        public const int MaxConcurrentLoansPerMember = 5;

        /// <summary>
        /// Default loan period in days.
        /// </summary>
        public const int DefaultLoanDurationDays = 14;

        public int BorrowingID { get; private set; } = -1;

        /// <summary>
        /// Alias matching RecordID from specification.
        /// </summary>
        public int RecordID => this.BorrowingID;

        public int MemberID { get; set; } = -1;
        public int BookID { get; set; } = -1;
        public int BookCopyID { get; set; } = -1;
        public DateTime BorrowDate { get; set; } = DateTime.Now.Date;
        public DateTime DueDate { get; set; } = DateTime.Now.Date.AddDays(DefaultLoanDurationDays);
        public DateTime? ActualReturnDate { get; set; } = null;
        public int CreatedByUserID { get; set; } = -1;

        public enMode Mode => _mode;

        #region Domain Computed Properties

        /// <summary>
        /// Indicates whether the borrowing is actively unreturned.
        /// </summary>
        public bool IsActive => !this.ActualReturnDate.HasValue;

        /// <summary>
        /// Indicates whether the loan is active and past its due date.
        /// </summary>
        public bool IsOverdue => this.IsActive && DateTime.Now.Date > this.DueDate.Date;

        /// <summary>
        /// Computes the number of days the loan is overdue.
        /// Returns 0 if returned on time or not yet overdue.
        /// </summary>
        public int OverdueDays
        {
            get
            {
                if (this.IsActive && DateTime.Now.Date > this.DueDate.Date)
                {
                    return (int)(DateTime.Now.Date - this.DueDate.Date).TotalDays;
                }
                else if (this.ActualReturnDate.HasValue && this.ActualReturnDate.Value.Date > this.DueDate.Date)
                {
                    return (int)(this.ActualReturnDate.Value.Date - this.DueDate.Date).TotalDays;
                }
                return 0;
            }
        }

        /// <summary>
        /// Returns the textual circulation status badge representation.
        /// </summary>
        public string StatusString
        {
            get
            {
                if (this.ActualReturnDate.HasValue)
                    return "Returned";
                if (DateTime.Now.Date > this.DueDate.Date)
                    return "Overdue";
                return "Active";
            }
        }

        #endregion

        #region Lazy Navigation Properties

        private clsMember _memberInfo;
        public clsMember MemberInfo
        {
            get
            {
                if (_memberInfo == null && this.MemberID > 0)
                {
                    _memberInfo = clsMember.Find(this.MemberID);
                }
                return _memberInfo;
            }
            set => _memberInfo = value;
        }

        private clsBook _bookInfo;
        public clsBook BookInfo
        {
            get
            {
                if (_bookInfo == null && this.BookID > 0)
                {
                    _bookInfo = clsBook.Find(this.BookID);
                }
                return _bookInfo;
            }
            set => _bookInfo = value;
        }

        private clsBookCopy _bookCopyInfo;
        public clsBookCopy BookCopyInfo
        {
            get
            {
                if (_bookCopyInfo == null && this.BookCopyID > 0)
                {
                    _bookCopyInfo = clsBookCopy.Find(this.BookCopyID);
                }
                return _bookCopyInfo;
            }
            set => _bookCopyInfo = value;
        }

        private clsUser _createdByUserInfo;
        public clsUser CreatedByUserInfo
        {
            get
            {
                if (_createdByUserInfo == null && this.CreatedByUserID > 0)
                {
                    _createdByUserInfo = clsUser.Find(this.CreatedByUserID);
                }
                return _createdByUserInfo;
            }
            set => _createdByUserInfo = value;
        }

        #endregion

        #region Constructors

        public clsBorrowingRecord()
        {
            this.BorrowingID = -1;
            this.MemberID = -1;
            this.BookID = -1;
            this.BookCopyID = -1;
            this.BorrowDate = DateTime.Now.Date;
            this.DueDate = DateTime.Now.Date.AddDays(DefaultLoanDurationDays);
            this.ActualReturnDate = null;
            this.CreatedByUserID = -1;
            this._mode = enMode.AddNew;
        }

        private clsBorrowingRecord(int borrowingID, int memberID, int bookID, int bookCopyID,
            DateTime borrowDate, DateTime dueDate, DateTime? actualReturnDate, int createdByUserID)
        {
            this.BorrowingID = borrowingID;
            this.MemberID = memberID;
            this.BookID = bookID;
            this.BookCopyID = bookCopyID;
            this.BorrowDate = borrowDate;
            this.DueDate = dueDate;
            this.ActualReturnDate = actualReturnDate;
            this.CreatedByUserID = createdByUserID;
            this._mode = enMode.Update;
        }

        #endregion

        #region Validation Invariants

        /// <summary>
        /// Evaluates whether a member is currently eligible to borrow additional books.
        /// </summary>
        public static bool IsMemberEligibleForBorrowing(int memberID, out string reason)
        {
            reason = string.Empty;

            if (memberID <= 0)
            {
                reason = "Invalid member identifier.";
                return false;
            }

            if (!clsMember.IsMemberExist(memberID))
            {
                reason = "Member record does not exist.";
                return false;
            }

            int activeLoans = clsBorrowingDataAccess.GetActiveBorrowingCountForMember(memberID);
            if (activeLoans >= MaxConcurrentLoansPerMember)
            {
                reason = $"Member has reached the maximum limit of {MaxConcurrentLoansPerMember} active borrowings. Return existing books before borrowing new ones.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Evaluates whether a physical book copy is valid and available for borrowing.
        /// </summary>
        public static bool IsCopyAvailableForBorrowing(int copyID, out string reason)
        {
            reason = string.Empty;

            if (copyID <= 0)
            {
                reason = "Invalid copy identifier.";
                return false;
            }

            clsBookCopy copy = clsBookCopy.Find(copyID);
            if (copy == null)
            {
                reason = "Book copy does not exist.";
                return false;
            }

            if (copy.Status != clsBookCopy.enCopyStatus.Available)
            {
                reason = $"Book copy is not available (Current Status: {copy.Status}).";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates invariants prior to executing persistence operations.
        /// </summary>
        private void _ValidateInvariants()
        {
            if (this.MemberID <= 0)
                throw new InvalidOperationException("A valid MemberID must be specified for a borrowing transaction.");

            if (this.DueDate <= this.BorrowDate)
                throw new InvalidOperationException("DueDate must be set strictly after BorrowDate.");

            if (_mode == enMode.AddNew)
            {
                if (!IsMemberEligibleForBorrowing(this.MemberID, out string memberReason))
                    throw new InvalidOperationException(memberReason);

                // Auto-resolve BookCopyID from BookID if needed
                if (this.BookCopyID <= 0 && this.BookID > 0)
                {
                    int autoCopyID = clsBorrowingDataAccess.GetFirstAvailableCopyID(this.BookID);
                    if (autoCopyID <= 0)
                        throw new InvalidOperationException("No available physical copies exist for the selected book.");
                    this.BookCopyID = autoCopyID;
                }

                // If BookID is not set, resolve from Copy
                if (this.BookID <= 0 && this.BookCopyID > 0)
                {
                    clsBookCopy copy = clsBookCopy.Find(this.BookCopyID);
                    if (copy != null)
                        this.BookID = copy.BookID;
                }

                if (this.BookCopyID <= 0)
                    throw new InvalidOperationException("A physical copy must be assigned for borrowing.");

                if (!IsCopyAvailableForBorrowing(this.BookCopyID, out string copyReason))
                    throw new InvalidOperationException(copyReason);
            }
        }

        #endregion

        #region Operations & Persistence

        /// <summary>
        /// Issues the borrowing record atomically and updates copy inventory to Borrowed.
        /// </summary>
        public bool Borrow()
        {
            if (_mode != enMode.AddNew)
                throw new InvalidOperationException("Cannot issue an existing borrowing record.");

            _ValidateInvariants();

            this.BorrowingID = clsBorrowingDataAccess.AddNewBorrowing(
                this.MemberID,
                this.BookID,
                this.BookCopyID,
                this.BorrowDate,
                this.DueDate,
                this.CreatedByUserID
            );

            if (this.BorrowingID != -1)
            {
                this._mode = enMode.Update;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unified Save persistence entry point.
        /// </summary>
        public bool Save()
        {
            if (_mode == enMode.AddNew)
            {
                return Borrow();
            }
            else
            {
                if (this.ActualReturnDate.HasValue)
                {
                    return ReturnBook(this.ActualReturnDate.Value);
                }
                return true;
            }
        }

        /// <summary>
        /// Returns the borrowed book copy, updates ActualReturnDate, and resets copy to Available status.
        /// </summary>
        public bool ReturnBook(DateTime? returnDate = null)
        {
            if (this.BorrowingID <= 0)
                throw new InvalidOperationException("Cannot return an unsaved borrowing record.");

            if (this.ActualReturnDate.HasValue)
                throw new InvalidOperationException("This book copy has already been returned.");

            DateTime actualDate = returnDate ?? DateTime.Now;

            bool success = clsBorrowingDataAccess.ReturnBook(this.BorrowingID, actualDate);
            if (success)
            {
                this.ActualReturnDate = actualDate;
                // Invalidate cached copy info to reflect Available status
                this._bookCopyInfo = null;
                return true;
            }

            return false;
        }

        #endregion

        #region Static Query Methods

        /// <summary>
        /// Finds and constructs a clsBorrowingRecord by primary key (BorrowingID).
        /// </summary>
        public static clsBorrowingRecord Find(int borrowingID)
        {
            if (borrowingID <= 0)
                return null;

            int memberID = -1;
            int bookID = -1;
            int bookCopyID = -1;
            DateTime borrowDate = DateTime.MinValue;
            DateTime dueDate = DateTime.MinValue;
            DateTime? actualReturnDate = null;
            int createdByUserID = -1;

            if (clsBorrowingDataAccess.GetBorrowingInfoByID(borrowingID, ref memberID, ref bookID,
                ref bookCopyID, ref borrowDate, ref dueDate, ref actualReturnDate, ref createdByUserID))
            {
                return new clsBorrowingRecord(borrowingID, memberID, bookID, bookCopyID,
                    borrowDate, dueDate, actualReturnDate, createdByUserID);
            }

            return null;
        }

        /// <summary>
        /// Finds the current active borrowing for a specific physical book copy.
        /// </summary>
        public static clsBorrowingRecord FindActiveByCopyID(int copyID)
        {
            if (copyID <= 0)
                return null;

            int borrowingID = -1;
            int memberID = -1;
            int bookID = -1;
            DateTime borrowDate = DateTime.MinValue;
            DateTime dueDate = DateTime.MinValue;
            int createdByUserID = -1;

            if (clsBorrowingDataAccess.GetActiveBorrowingByCopyID(copyID, ref borrowingID, ref memberID,
                ref bookID, ref borrowDate, ref dueDate, ref createdByUserID))
            {
                return new clsBorrowingRecord(borrowingID, memberID, bookID, copyID,
                    borrowDate, dueDate, null, createdByUserID);
            }

            return null;
        }

        /// <summary>
        /// Finds the active borrowing for a book by a specific member.
        /// </summary>
        public static clsBorrowingRecord FindActiveByBookAndMember(int bookID, int memberID)
        {
            if (bookID <= 0 || memberID <= 0)
                return null;

            int borrowingID = -1;
            int bookCopyID = -1;
            DateTime borrowDate = DateTime.MinValue;
            DateTime dueDate = DateTime.MinValue;
            int createdByUserID = -1;

            if (clsBorrowingDataAccess.GetActiveBorrowingByBookAndMember(bookID, memberID,
                ref borrowingID, ref bookCopyID, ref borrowDate, ref dueDate, ref createdByUserID))
            {
                return new clsBorrowingRecord(borrowingID, memberID, bookID, bookCopyID,
                    borrowDate, dueDate, null, createdByUserID);
            }

            return null;
        }

        /// <summary>
        /// Retrieves all borrowing records from v_BorrowingsInfo view.
        /// </summary>
        public static DataTable GetAllBorrowings()
        {
            return clsBorrowingDataAccess.GetAllBorrowings();
        }

        /// <summary>
        /// Retrieves active and overdue borrowing records from v_BorrowingsInfo view.
        /// </summary>
        public static DataTable GetActiveBorrowings()
        {
            return clsBorrowingDataAccess.GetActiveBorrowings();
        }

        /// <summary>
        /// Retrieves exclusively overdue borrowing records from v_BorrowingsInfo view.
        /// </summary>
        public static DataTable GetOverdueBorrowings()
        {
            return clsBorrowingDataAccess.GetOverdueBorrowings();
        }

        /// <summary>
        /// Returns the count of current unreturned active loans for a member.
        /// </summary>
        public static int GetActiveBorrowingCountForMember(int memberID)
        {
            return clsBorrowingDataAccess.GetActiveBorrowingCountForMember(memberID);
        }

        /// <summary>
        /// Locates the first available copy ID for a given book.
        /// </summary>
        public static int GetFirstAvailableCopyID(int bookID)
        {
            return clsBorrowingDataAccess.GetFirstAvailableCopyID(bookID);
        }

        #endregion
    }
}
