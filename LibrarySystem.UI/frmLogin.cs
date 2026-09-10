using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Presentation layer form for authenticating system users.
    /// Handles credential verification, active account status checks,
    /// input validation via ErrorProvider, and session initialization.
    /// </summary>
    public partial class frmLogin : Form
    {
        private CheckBox _chkShowPassword;
        private Label _lblCapsLock;

        public frmLogin()
        {
            InitializeComponent();
            _SetupExtraUI();
        }

        private void _SetupExtraUI()
        {
            // Show Password Toggle
            _chkShowPassword = new CheckBox
            {
                Text = "👁",
                Font = new System.Drawing.Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new System.Drawing.Point(txtPassword.Right + 5, txtPassword.Top + 2),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat
            };
            _chkShowPassword.Appearance = Appearance.Button;
            _chkShowPassword.FlatAppearance.BorderSize = 0;
            _chkShowPassword.CheckedChanged += (s, e) => 
            {
                txtPassword.PasswordChar = _chkShowPassword.Checked ? '\0' : '*';
            };
            this.Controls.Add(_chkShowPassword);

            // Caps Lock Warning
            _lblCapsLock = new Label
            {
                Text = "⚠️ Caps Lock is ON",
                ForeColor = System.Drawing.Color.DarkOrange,
                AutoSize = true,
                Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(txtPassword.Left, txtPassword.Bottom + 2),
                Visible = Control.IsKeyLocked(Keys.CapsLock)
            };
            this.Controls.Add(_lblCapsLock);

            this.KeyPreview = true;
            this.KeyDown += (s, e) => _lblCapsLock.Visible = Control.IsKeyLocked(Keys.CapsLock);
        }

        /// <summary>
        /// Form load event handler.
        /// Checks the Windows Registry for cached credentials and pre-populates input fields.
        /// </summary>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = "";
            string password = "";

            // Attempt to retrieve cached credentials from the registry
            if (clsGlobal.GetStoredCredential(ref username, ref password))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        // ==================== Control Validations ====================

        /// <summary>
        /// Validates that the Username field is not empty or composed solely of whitespace.
        /// </summary>
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
            {
                errorProvider1.SetError(txtUsername, "Username is required!");
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }
        }

        /// <summary>
        /// Validates that the Password field is not left blank.
        /// </summary>
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password is required!");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        // ==================== Authentication Workflow ====================

        /// <summary>
        /// Handles the primary authentication execution pipeline:
        /// 1. Runs control validation across all child inputs.
        /// 2. Queries the Business Logic Layer for matching user credentials.
        /// 3. Validates account status (IsActive flag).
        /// 4. Handles registry persistence for the "Remember Me" toggle.
        /// 5. Injects the authenticated user into clsGlobal.CurrentUser and sets DialogResult.OK.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Manual validation since we don't use e.Cancel (to avoid focus trap)
            bool hasErrors = false;

            if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
            {
                errorProvider1.SetError(txtUsername, "Username is required!");
                hasErrors = true;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password is required!");
                hasErrors = true;
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }

            if (hasErrors)
            {
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Step 1: Query the BLL (which calls DAL/SQL Server) for user matching username and password
            clsUser user = clsUser.FindByUsernameAndPassword(username, password);

            // If no record is returned, authentication fails
            if (user == null)
            {
                MessageBox.Show("Invalid Username or Password.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtUsername.Focus();
                return;
            }

            // Step 2: Check account activation status to prevent locked/deactivated users from logging in
            if (!user.IsActive)
            {
                MessageBox.Show("Your account is currently deactivated. Please contact your administrator.",
                    "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Step 3: Handle credential persistence based on the Remember Me checkbox state
            if (chkRememberMe.Checked)
            {
                clsGlobal.RememberUsernameAndPassword(username, password);
            }
            else
            {
                clsGlobal.ClearStoredCredentials();
            }

            // Step 4: Store authenticated user instance in the global session context
            clsGlobal.CurrentUser = user;

            // Step 5: Mark dialog result as OK and close. This signals Program.cs to launch frmMain
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Cancels authentication and signals the application entry point to terminate gracefully.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}