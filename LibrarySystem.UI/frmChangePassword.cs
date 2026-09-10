using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Dialog for changing a user's password securely with verification of the current password.
    /// Supports changing the password for the active session user or any specified UserID.
    /// </summary>
    public partial class frmChangePassword : Form
    {
        private int _UserID = -1;
        private clsUser _User;

        /// <summary>
        /// Default constructor: changes password for the currently logged-in user.
        /// </summary>
        public frmChangePassword()
        {
            InitializeComponent();

            if (clsGlobal.CurrentUser != null)
                _UserID = clsGlobal.CurrentUser.UserID;
        }

        /// <summary>
        /// Overloaded constructor: changes password for a specific user identified by UserID.
        /// </summary>
        /// <param name="userID">Target user ID.</param>
        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"User with ID [{_UserID}] was not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblUsername.Text = _User.Username;
        }

        // ==================== Validations ====================

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password cannot be blank!");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New password cannot be blank!");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password confirmation does not match!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        // ==================== Actions ====================

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate all form controls before proceeding
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fix the validation errors before submitting.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Verify that the entered current password matches the database record
            if (txtCurrentPassword.Text.Trim() != _User.Password)
            {
                errorProvider1.SetError(txtCurrentPassword, "Current password is incorrect!");
                MessageBox.Show("The current password you entered is incorrect.", "Wrong Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPassword.Focus();
                return;
            }

            // 2. Prevent setting the same old password as the new one
            if (txtNewPassword.Text.Trim() == _User.Password)
            {
                MessageBox.Show("The new password cannot be the same as the current password.", "Invalid Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            // 3. Update password in the database via Business Layer
            _User.Password = txtNewPassword.Text.Trim();

            if (_User.Save())
            {
                MessageBox.Show("Password has been changed successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update the password in the database.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}