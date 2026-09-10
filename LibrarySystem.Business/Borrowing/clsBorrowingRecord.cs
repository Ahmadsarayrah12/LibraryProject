using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsBorrowingRecord
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public const int MaxConcurrentLoansPerMember = 5;
        public const int DefaultLoanDurationDays = 14;

        public int BorrowingID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public int BookCopyID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public int CreatedByUserID { get; set; }

        public clsMember MemberInfo { get; set; }
        public clsBook BookInfo { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public bool IsActive
        {
            get { return !ActualReturnDate.HasValue; }
        }

        public bool IsOverdue
        {
            get { return IsActive && DateTime.Now.Date > DueDate.Date; }
        }

        public int OverdueDays
        {
            get
            {
                if (IsActive && DateTime.Now.Date > DueDate.Date)
                    return (int)(DateTime.Now.Date - DueDate.Date).TotalDays;
                else if (ActualReturnDate.HasValue && ActualReturnDate.Value.Date > DueDate.Date)
                    return (int)(ActualReturnDate.Value.Date - DueDate.Date).TotalDays;
                return 0;
            }
        }

        public string StatusString
        {
            get
            {
                if (ActualReturnDate.HasValue) return "Returned";
                if (DateTime.Now.Date > DueDate.Date) return "Overdue";
                return "Active";
            }
        }

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
            Mode = enMode.AddNew;
        }

        private clsBorrowingRecord(int borrowingID, int memberID, int bookID, int bookCopyID, DateTime borrowDate, DateTime dueDate, DateTime? actualReturnDate, int createdByUserID)
        {
            this.BorrowingID = borrowingID;
            this.MemberID = memberID;
            this.BookID = bookID;
            this.BookCopyID = bookCopyID;
            this.BorrowDate = borrowDate;
            this.DueDate = dueDate;
            this.ActualReturnDate = actualReturnDate;
            this.CreatedByUserID = createdByUserID;

            this.MemberInfo = clsMember.Find(memberID);
            this.BookInfo = clsBook.Find(bookID);
            this.CreatedByUserInfo = clsUser.FindByUserID(createdByUserID);

            Mode = enMode.Update;
        }

        public static clsBorrowingRecord Find(int borrowingID)
        {
            int memberID = -1, bookID = -1, bookCopyID = -1, createdByUserID = -1;
            DateTime borrowDate = DateTime.Now, dueDate = DateTime.Now;
            DateTime? actualReturnDate = null;

            if (clsBorrowingDataAccess.GetBorrowingInfoByID(borrowingID, ref memberID, ref bookID, ref bookCopyID, ref borrowDate, ref dueDate, ref actualReturnDate, ref createdByUserID))
            {
                return new clsBorrowingRecord(borrowingID, memberID, bookID, bookCopyID, borrowDate, dueDate, actualReturnDate, createdByUserID);
            }
            return null;
        }

        public static clsBorrowingRecord FindActiveByCopyID(int copyID)
        {
            int borrowingID = -1, memberID = -1, bookID = -1, createdByUserID = -1;
            DateTime borrowDate = DateTime.Now, dueDate = DateTime.Now;
            
            if (clsBorrowingDataAccess.GetActiveBorrowingByCopyID(copyID, ref borrowingID, ref memberID, ref bookID, ref borrowDate, ref dueDate, ref createdByUserID))
            {
                return new clsBorrowingRecord(borrowingID, memberID, bookID, copyID, borrowDate, dueDate, null, createdByUserID);
            }
            return null;
        }

        public static int GetActiveBorrowingCountForMember(int memberID)
        {
            return clsBorrowingDataAccess.GetActiveBorrowingCountForMember(memberID);
        }

        public static bool IsMemberEligibleForBorrowing(int memberID, out string reason)
        {
            reason = "";
            if (!clsMember.IsMemberExist(memberID))
            {
                reason = "Member does not exist.";
                return false;
            }
            if (GetActiveBorrowingCountForMember(memberID) >= MaxConcurrentLoansPerMember)
            {
                reason = "Member has reached the maximum allowed concurrent loans.";
                return false;
            }
            return true;
        }

        public static bool IsMemberEligibleForBorrowing(int memberID)
        {
            string reason;
            return IsMemberEligibleForBorrowing(memberID, out reason);
        }

        public bool Borrow()
        {
            return Save();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (!IsMemberEligibleForBorrowing(this.MemberID)) return false;

                    int copyID = clsBorrowingDataAccess.GetFirstAvailableCopyID(this.BookID);
                    if (copyID == -1) return false;
                    this.BookCopyID = copyID;

                    this.BorrowingID = clsBorrowingDataAccess.AddNewBorrowing(this.MemberID, this.BookID, this.BookCopyID, this.BorrowDate, this.DueDate, this.CreatedByUserID);
                    
                    if (this.BorrowingID != -1)
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return false;
            }
            return false;
        }

        public bool ReturnBook(DateTime returnDate)
        {
            if (this.Mode == enMode.AddNew || !this.IsActive) return false;

            this.ActualReturnDate = returnDate.Date;
            return clsBorrowingDataAccess.ReturnBook(this.BorrowingID, this.ActualReturnDate.Value);
        }

        public static DataTable GetAllBorrowings()
        {
            return clsBorrowingDataAccess.GetAllBorrowings();
        }

        public static bool DeleteBorrowing(int borrowingID)
        {
            return clsBorrowingDataAccess.DeleteBorrowing(borrowingID);
        }
    }
}

