using System;
using System.Data;
using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsFine
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _mode = enMode.AddNew;

        public int FineID { get; private set; }
        public int MemberID { get; set; }
        public int BorrowingID { get; set; }
        public int NumberOfLateDays { get; set; }
        public decimal FineAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int CreatedByUserID { get; set; }

        public static decimal DefaultFinePerDay { get { return 0.5m; } }

        public clsFine()
        {
            this.FineID = -1;
            this.MemberID = -1;
            this.BorrowingID = -1;
            this.NumberOfLateDays = 0;
            this.FineAmount = 0;
            this.PaymentStatus = false;
            this.PaymentDate = null;
            this.CreatedByUserID = -1;

            _mode = enMode.AddNew;
        }

        private clsFine(int fineID, int memberID, int borrowingID, int numberOfLateDays, decimal fineAmount, bool paymentStatus, DateTime? paymentDate, int createdByUserID)
        {
            this.FineID = fineID;
            this.MemberID = memberID;
            this.BorrowingID = borrowingID;
            this.NumberOfLateDays = numberOfLateDays;
            this.FineAmount = fineAmount;
            this.PaymentStatus = paymentStatus;
            this.PaymentDate = paymentDate;
            this.CreatedByUserID = createdByUserID;

            _mode = enMode.Update;
        }

        public static clsFine Find(int fineID)
        {
            int memberID = -1, borrowingID = -1, numberOfLateDays = 0, createdByUserID = -1;
            decimal fineAmount = 0;
            bool paymentStatus = false;
            DateTime? paymentDate = null;

            if (clsFineDataAccess.GetFineInfoByID(fineID, ref memberID, ref borrowingID, ref numberOfLateDays, ref fineAmount, ref paymentStatus, ref paymentDate, ref createdByUserID))
            {
                return new clsFine(fineID, memberID, borrowingID, numberOfLateDays, fineAmount, paymentStatus, paymentDate, createdByUserID);
            }
            return null;
        }

        private bool _AddNewFine()
        {
            this.FineID = clsFineDataAccess.AddNewFine(this.MemberID, this.BorrowingID, this.NumberOfLateDays, this.FineAmount, this.PaymentStatus, this.PaymentDate, this.CreatedByUserID);
            return (this.FineID != -1);
        }

        private bool _UpdateFine()
        {
            return clsFineDataAccess.UpdateFine(this.FineID, this.MemberID, this.BorrowingID, this.NumberOfLateDays, this.FineAmount, this.PaymentStatus, this.PaymentDate, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewFine())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateFine();
            }

            return false;
        }

        public bool PayFine()
        {
            this.PaymentStatus = true;
            this.PaymentDate = DateTime.Now;
            return Save();
        }

        public static DataTable GetAllFines()
        {
            return clsFineDataAccess.GetAllFines();
        }

        public static DataTable GetMemberFines(int memberID)
        {
            return clsFineDataAccess.GetMemberFines(memberID);
        }

        public static bool HasUnpaidFines(int memberID)
        {
            return clsFineDataAccess.HasUnpaidFines(memberID);
        }
    }
}
