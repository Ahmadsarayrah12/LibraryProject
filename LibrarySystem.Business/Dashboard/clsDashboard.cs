using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public static class clsDashboard
    {
        public static int GetTotalBooks()
        {
            return clsDashboardDataAccess.GetTotalBooks();
        }

        public static int GetTotalMembers()
        {
            return clsDashboardDataAccess.GetTotalMembers();
        }

        public static int GetActiveBorrowings()
        {
            return clsDashboardDataAccess.GetActiveBorrowings();
        }

        public static int GetOverdueBooks()
        {
            return clsDashboardDataAccess.GetOverdueBooks();
        }
    }
}
