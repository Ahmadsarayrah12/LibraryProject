using System;
using System.Text;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Read-only dialog that displays detailed profile and credential data for a user.
    /// Combines Person entity details with User account metadata and bitwise permissions breakdown.
    /// </summary>
    public partial class frmUserInfo : Form
    {
        private int _UserID = -1;
        private clsUser _User;
        private clsPerson _Person;

        /// <summary>
        /// Default constructor: loads profile information for the active session user.
        /// </summary>
        public frmUserInfo()
        {
            InitializeComponent();

            if (clsGlobal.CurrentUser != null)
                _UserID = clsGlobal.CurrentUser.UserID;
        }

        /// <summary>
        /// Overloaded constructor: loads profile information for any target user by ID.
        /// </summary>
        /// <param name="userID">The primary key ID of the target user record.</param>
        public frmUserInfo(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }

        /// <summary>
        /// Formats and decodes bitwise permission flags into a human-readable list.
        /// </summary>
        /// <param name="permissions">Bitmask integer representing user permissions.</param>
        /// <returns>Formatted text listing all granted permissions.</returns>
        private string _GetPermissionsText(int permissions)
        {
            if (permissions == -1)
                return "Full Access (Administrator)";

            if (permissions == 0)
                return "No Permissions Assigned";

            StringBuilder sb = new StringBuilder();

            if (_User.CheckAccessPermission(clsUser.enPermissions.pManagePeople))
                sb.Append("Manage People, ");

            if (_User.CheckAccessPermission(clsUser.enPermissions.pManageUsers))
                sb.Append("Manage Users, ");

            if (_User.CheckAccessPermission(clsUser.enPermissions.pManageBooks))
                sb.Append("Manage Books, ");

            if (_User.CheckAccessPermission(clsUser.enPermissions.pBorrowing))
                sb.Append("Borrowing Records, ");

            if (_User.CheckAccessPermission(clsUser.enPermissions.pFines))
                sb.Append("Fines, ");

            // Remove the trailing comma and space if at least one permission matched
            string result = sb.ToString();
            return result.EndsWith(", ") ? result.Substring(0, result.Length - 2) : result;
        }

        /// <summary>
        /// Fetches user and person records via the Business Layer and populates the UI controls.
        /// </summary>
        private void _LoadUserData()
        {
            // Retrieve user credentials and permissions from BLL
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"User with ID [{_UserID}] was not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Retrieve associated personal information from BLL
            _Person = clsPerson.FindPerson(_User.PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Associated Person details were not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Populate Person info labels
            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = $"{_Person.FirstName} {_Person.LastName}";
            lblPhone.Text = string.IsNullOrEmpty(_Person.Phone) ? "N/A" : _Person.Phone;
            lblEmail.Text = string.IsNullOrEmpty(_Person.Email) ? "N/A" : _Person.Email;

            // Populate User account info labels
            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.Username;
            lblIsActive.Text = _User.IsActive ? "Yes (Active)" : "No (Deactivated)";
            lblIsActive.ForeColor = _User.IsActive ? System.Drawing.Color.DarkGreen : System.Drawing.Color.Red;
            lblPermissions.Text = _GetPermissionsText(_User.Permissions);
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            _LoadUserData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}