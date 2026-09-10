using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Unified form responsible for creating new users or updating existing records.
    /// Manages the dual-entity persistence workflow (Person entity + User credentials/permissions)
    /// in a single coherent operation.
    /// </summary>
    public partial class frmAddUpdateUser : Form
    {
        // Enum to represent the runtime state of the form
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _UserID = -1;
        private clsUser _User;
        private clsPerson _Person;
        private bool _isSaved = false;

        /// <summary>
        /// Default parameterless constructor used for creating a new user record.
        /// </summary>
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        /// <summary>
        /// Overloaded constructor used for editing an existing user record identified by UserID.
        /// </summary>
        /// <param name="userID">The primary key ID of the target user record.</param>
        public frmAddUpdateUser(int userID)
        {
            InitializeComponent();
            _UserID = userID;
            _Mode = enMode.Update;
        }

        /// <summary>
        /// Initializes the UI input controls, labels, and entity instances to default states
        /// based on the active operational mode.
        /// </summary>
        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";

                // Initialize clean, blank business entity instances
                _User = new clsUser();
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
            }

            // Reset text and visual placeholders
            lblUserID.Text = "[???]";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
            chkAllPermissions.Checked = true;
        }

        /// <summary>
        /// Queries the Business Logic Layer to load both User and Person records,
        /// then unpacks their properties into the corresponding visual fields.
        /// </summary>
        private void _LoadUserData()
        {
            // Find the User record by primary key
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"No User with ID = {_UserID} was found!", "User Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            // Find the linked Person entity using the Foreign Key PersonID
            _Person = clsPerson.FindPerson(_User.PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person details for this user were not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Populate Person-related text controls
            lblUserID.Text = _User.UserID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;

            // Populate User-related authentication controls
            txtUsername.Text = _User.Username;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;

            // Unpack integer bitmask permissions into individual checkbox states
            if (_User.Permissions == -1)
            {
                // -1 represents unrestricted administrative permissions
                chkAllPermissions.Checked = true;
            }
            else
            {
                chkAllPermissions.Checked = false;

                // Check individual bit flags using the BLL helper method
                chkManagePeople.Checked = _User.CheckAccessPermission(clsUser.enPermissions.pManagePeople);
                chkManageUsers.Checked = _User.CheckAccessPermission(clsUser.enPermissions.pManageUsers);
                chkManageBooks.Checked = _User.CheckAccessPermission(clsUser.enPermissions.pManageBooks);
                chkBorrowing.Checked   = _User.CheckAccessPermission(clsUser.enPermissions.pBorrowing);
                chkFines.Checked =       _User.CheckAccessPermission(clsUser.enPermissions.pFines);


            }
        }

        /// <summary>
        /// Computes the composite integer permissions value using bitwise OR operations.
        /// </summary>
        /// <returns>Integer value representing accumulated permission flags, or -1 for full access.</returns>
        private int _CalculatePermissions()
        {
            // If full access is toggled, return the special system-wide constant -1
            if (chkAllPermissions.Checked)
                return -1;

            int permissions = 0;

            // Aggregate discrete permission bits using bitwise OR assignment (|=)
            if (chkManagePeople.Checked)
                permissions |= (int)clsUser.enPermissions.pManagePeople;

            if (chkManageUsers.Checked)
                permissions |= (int)clsUser.enPermissions.pManageUsers;

            if (chkManageBooks.Checked)
                permissions |= (int)clsUser.enPermissions.pManageBooks;

            if (chkBorrowing.Checked)
                permissions |= (int)clsUser.enPermissions.pBorrowing;

            if (chkFines.Checked)
                permissions |= (int)clsUser.enPermissions.pFines;

            return permissions;
        }

        /// <summary>
        /// Form load handler initializing defaults and loading data when in Update mode.
        /// </summary>
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update) 
                _LoadUserData();
        }

        /// <summary>
        /// Synchronizes the availability of individual permission checkboxes
        /// with the "All Permissions" master checkbox.
        /// </summary>
        private void chkAllPermissions_CheckedChanged(object sender, EventArgs e)
        {
            // Disable individual checkboxes when full access (-1) is selected
            pnlIndividualPermissions.Enabled = !chkAllPermissions.Checked;

            if (chkAllPermissions.Checked)
            {
                // Clear individual selections to prevent visual confusion
                chkManagePeople.Checked = false;
                chkManageUsers.Checked = false;
                chkManageBooks.Checked = false;
                chkBorrowing.Checked = false;
                chkFines.Checked = false;
            }
        }

        /// <summary>
        /// Validates that First Name is provided before saving.
        /// </summary>
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text.Trim()))
            {
                 
                errorProvider1.SetError(txtFirstName, "First Name is required!");
            }
            else
            {
                errorProvider1.SetError(txtFirstName, null);
            }
        }

        /// <summary>
        /// Validates that Last Name is provided before saving.
        /// </summary>
        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text.Trim()))
            {
            
                errorProvider1.SetError(txtLastName, "Last Name is required!");
            }
            else
            {
                errorProvider1.SetError(txtLastName, null);
            }
        }

        /// <summary>
        /// Validates that Username is supplied and checks for uniqueness in the database.
        /// </summary>
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
            {
                
                errorProvider1.SetError(txtUsername, "Username is required!");
                return;
            }

            // Check uniqueness if adding new OR if the username was altered during an update
            if (_Mode == enMode.AddNew || (_Mode == enMode.Update && _User.Username != txtUsername.Text.Trim()))
            {
                if (clsUser.IsUserExist(txtUsername.Text.Trim()))
                {
                   
                    errorProvider1.SetError(txtUsername, "Username is already used by another user!");
                    return;
                }
            }

            errorProvider1.SetError(txtUsername, null);
        }

        /// <summary>
        /// Validates that Password is not empty.
        /// </summary>
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
 
                errorProvider1.SetError(txtPassword, "Password cannot be blank!");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        /// <summary>
        /// Validates that Password Confirmation strictly matches the initial Password input.
        /// </summary>
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                
                errorProvider1.SetError(txtConfirmPassword, "Password confirmation does not match!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        /// <summary>
        /// Executes the two-step unified entity persistence pipeline:
        /// 1. Persists Person entity to SQL Server and acquires the generated/updated PersonID.
        /// 2. Injects PersonID into User entity and persists credentials, status, and permissions.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Triggers validating events on all child controls; aborts if any validation fails
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill all required fields correctly.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 1: Populate and persist Person entity
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();

            if (!_Person.Save())
            {
                MessageBox.Show("Failed to save Person details.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Step 2: Bind the resulting PersonID to the User entity and persist credentials
            _User.PersonID = _Person.PersonID;
            _User.Username = txtUsername.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;
            _User.Permissions = _CalculatePermissions();

            if (_User.Save())
            {
                _isSaved = true;
                MessageBox.Show("User Saved Successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close dialog immediately to unblock frmUsers.ShowDialog() and trigger data grid refresh
                lblUserID.Text = _User.PersonID.ToString();
            }
            else
            {
                MessageBox.Show("Failed to save User credentials.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Closes the form without saving changes.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!_isSaved && (!string.IsNullOrWhiteSpace(txtFirstName.Text) || !string.IsNullOrWhiteSpace(txtUsername.Text)))
            {
                if (MessageBox.Show("You have unsaved changes. Are you sure you want to close this window?", 
                    "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}