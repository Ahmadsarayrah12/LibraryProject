using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Represents the main presentation interface for managing system users.
    /// Handles user listing, in-memory dynamic filtering, record operations (CRUD),
    /// and contextual navigation to specialized child dialogs.
    /// </summary>
    public partial class frmUsers : Form
    {
        // Holds an in-memory cached copy of the users table retrieved from the Business Logic Layer.
        // This avoids redundant round-trips to SQL Server when filtering or searching.
        private DataTable _dtAllUsers;

        public frmUsers()
        {
            InitializeComponent();
            _SetupResizing();
        }

        private void _SetupResizing()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAddUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRecordsCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        }

        /// <summary>
        /// Retrieves the latest user records from the Business Layer, rebinds the DataGridView,
        /// recalculates the record counter, and configures human-readable column headers.
        /// </summary>
        private void _RefreshUsersList()
        {
            // Fetch fresh records via the static BLL method (which communicates through DAL to SQL Server)
            _dtAllUsers = clsUser.GetAllUsers();

            // Re-assign the DataSource to force the DataGridView to render updated rows
            dgvUsers.DataSource = _dtAllUsers;

            // Update the live record counter label
            lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();

            // Format grid columns safely only if data exists
            if (dgvUsers.Rows.Count > 0)
            {
                // Explicitly naming columns by their DataTable alias ensures resilient layout configuration
                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 80;

                dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
                dgvUsers.Columns["PersonID"].Width = 80;

                dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                dgvUsers.Columns["FullName"].Width = 180;

                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Username"].Width = 120;

                dgvUsers.Columns["Permissions"].HeaderText = "Permissions";
                dgvUsers.Columns["Permissions"].Width = 90;

                dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
                dgvUsers.Columns["IsActive"].Width = 80;

                dgvUsers.Columns["Phone"].HeaderText = "Phone";
                dgvUsers.Columns["Phone"].Width = 120;

                dgvUsers.Columns["Email"].HeaderText = "Email";
                dgvUsers.Columns["Email"].Width = 160;
            }
        }

        /// <summary>
        /// Triggered when the form is initialized and displayed on the screen.
        /// Loads initial data and sets default filter states.
        /// </summary>
        private void frmUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            // Set "None" as the default selection in the filter combo box
            cbFilterBy.SelectedIndex = 0;
        }

        /// <summary>
        /// Adjusts dynamic UI input controls based on the filter criteria selected by the user.
        /// </summary>
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If the user chooses to filter by account status, display the Boolean ComboBox
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0; // Default to "All"
            }
            else
            {
                // For textual or numeric fields, display the TextBox; hide it if "None" is chosen
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    // Clear the DataView filter expression to show all available rows
                    _dtAllUsers.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
                }
                else
                {
                    // Reset search input and position the cursor inside the textbox
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
        }

        /// <summary>
        /// Applies real-time filtering to the in-memory DataTable's DefaultView as the user types.
        /// </summary>
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            // Map user-friendly dropdown display strings to actual database/DataTable column names
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    filterColumn = "UserID";
                    break;

                case "Person ID":
                    filterColumn = "PersonID";
                    break;

                case "Full Name":
                    filterColumn = "FullName";
                    break;

                case "Username":
                    filterColumn = "Username";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            // If the search input is cleared or no filter is selected, reset and display all rows
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || filterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
                return;
            }

            // Numeric comparisons require direct equality syntax without quotes (e.g., UserID = 10)
            if (filterColumn == "UserID" || filterColumn == "PersonID")
            {

                // Safe parsing prevents syntax exceptions in the RowFilter expression
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    _dtAllUsers.DefaultView.RowFilter = $"{filterColumn} = {value}";
                }
                else
                {
                    // If parsing fails during transition typing, assign an impossible value to clear results safely
                    _dtAllUsers.DefaultView.RowFilter = $"{filterColumn} = -1";
                }
                  
            }
            else
            {
                // String comparisons utilize SQL-like 'LIKE' syntax with wildcard prefix matching
                _dtAllUsers.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            // Keep the record count synchronized with the visible filtered rows
            lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
        }

        /// <summary>
        /// Filters the grid rows based on the user's active/inactive status selection.
        /// </summary>
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = cbIsActive.Text;

            switch (filterValue)
            {
                case "All":
                    _dtAllUsers.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = 1";
                    break;

                case "No":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = 0";
                    break;
            }

            lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
        }

        /// <summary>
        /// Restricts keystrokes in the search box to numeric digits only when searching numeric columns.
        /// </summary>
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
            {
                // Allow control keys (like Backspace) and numeric digits; suppress all alphabetic/symbol inputs
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Opens the unified Add/Update User dialog in AddNew mode modally.
        /// Automatically refreshes the grid once the modal dialog closes.
        /// </summary>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateUser frm = new frmAddUpdateUser())
            {
                frm.ShowDialog();
            }

            _RefreshUsersList();
        }

        /// <summary>
        /// Context menu shortcut delegating to the primary Add User button click handler.
        /// </summary>
        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddUser.PerformClick();
        }

        /// <summary>
        /// Reads the selected UserID from the active row and opens the editor dialog in Update mode.
        /// </summary>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                int selectedUserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

                using (frmAddUpdateUser frm = new frmAddUpdateUser(selectedUserID))
                {
                    frm.ShowDialog();
                }

                _RefreshUsersList();
            }
        }

        /// <summary>
        /// Prompts the user for deletion confirmation and executes a cascade/record delete via BLL.
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                int selectedUserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
               
                if (selectedUserID == clsGlobal.CurrentUser.UserID)
                {
                    MessageBox.Show("You cannot delete your own account while you are logged in!",
                        "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mandatory confirmation prompt before executing irreversible delete operations
                if (MessageBox.Show($"Are you sure you want to delete User [{selectedUserID}]?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Execute deletion through the Business Logic Layer
                    if (clsUser.Delete(selectedUserID))
                    {
                        MessageBox.Show("User deleted successfully.", "Deleted",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _RefreshUsersList();
                    }
                    else
                    {
                        // Failure typically indicates active foreign key constraints (e.g., active borrowing transactions)
                        MessageBox.Show("Failed to delete user. User might be linked to active operations.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Closes the manage users form.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvUsers.ClearSelection();
                dgvUsers.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                int selectedUserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
                using (frmAddUpdateUser frm = new frmAddUpdateUser(selectedUserID))
                {
                    frm.ShowDialog();
                }
                _RefreshUsersList();
            }
        }

        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            using (frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID))
            {
                frm.ShowDialog();
            }
        }
    }
}