using LibrarySystem.DataAccess;

namespace LibrarySystem.Business
{
    public class clsBusinessLayerUsers
    {
        public int UserID { get; private set; }
        public int PersonID { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        private clsBusinessLayerUsers(int userID, int personID, string username, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
        }

        public static clsBusinessLayerUsers FindByUserNameAndPassword(string username, string password)
        {
            int userID = -1;
            int personID = -1;
            bool isActive = false;

            bool isFound = clsDataAccessLayerUsers.Login(username, password, ref userID, ref personID, ref isActive);

            if (isFound)
            {
                return new clsBusinessLayerUsers(userID, personID, username, password, isActive);
            }

            return null;
        }
    }
}