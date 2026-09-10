using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// User interface dialog for enrolling new library members or updating active subscriptions.
    /// Embeds ctrlPersonCardWithFilter to ensure decoupled identity management and validation.
    /// </summary>
    public partial class frmAddUpdateMember : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _MemberID = -1;
        private clsMember _Member;
        private bool _isSaved = false;

        /// <summary>
        /// Default constructor initializing the form in AddNew mode.
        /// </summary>
        public frmAddUpdateMember()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        /// <summary>
        /// Overloaded constructor initializing the form in Update mode for an existing MemberID.
        /// </summary>
        /// <param name="memberID">The target member primary key ID.</param>
        public frmAddUpdateMember(int memberID)
        {
            InitializeComponent();
            _MemberID = memberID;
            _Mode = enMode.Update;
        }

        /// <summary>
        /// Configures initial UI states based on active mode (AddNew / Update).
        /// </summary>
        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Member";
                this.Text = "Add New Member";
                _Member = new clsMember();

                ctrlPersonCardWithFilter1.FilterEnabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();

                lblMemberID.Text = "[???]";
                dtpSubscriptionDate.Value = DateTime.Now;
                btnSave.Enabled = false; // Disabled until a valid person is selected
            }
            else
            {
                lblTitle.Text = "Update Member";
                this.Text = "Update Member";

                ctrlPersonCardWithFilter1.FilterEnabled = false; // Lock person selection when editing
                btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// Loads existing member data from BLL and binds details to the UI controls.
        /// </summary>
        private void _LoadData()
        {
            _Member = clsMember.Find(_MemberID);

            if (_Member == null)
            {
                MessageBox.Show($"Member with ID [{_MemberID}] was not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblMemberID.Text = _Member.MemberID.ToString();
            dtpSubscriptionDate.Value = _Member.SubscriptionDate;

            ctrlPersonCardWithFilter1.LoadPersonInfo(_Member.PersonID);
        }

        private void frmAddUpdateMember_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        /// <summary>
        /// Event listener triggered when a valid person is selected via the filter control.
        /// Validates business rule: a person cannot have multiple active library memberships.
        /// </summary>
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int selectedPersonID)
        {
            if (selectedPersonID == -1)
            {
                btnSave.Enabled = false;
                return;
            }

            if (_Mode == enMode.AddNew && clsMember.IsMemberExistByPersonID(selectedPersonID))
            {
                MessageBox.Show("This person is already registered as a member! Please select another person.",
                    "Duplicate Member", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                btnSave.Enabled = false;
                return;
            }

            btnSave.Enabled = true;
        }

        /// <summary>
        /// Handles validation, object construction, and persistence into the database.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int selectedPersonID = ctrlPersonCardWithFilter1.PersonID;

            if (selectedPersonID == -1)
            {
                MessageBox.Show("Please select a person before proceeding.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_Mode == enMode.AddNew && clsMember.IsMemberExistByPersonID(selectedPersonID))
            {
                MessageBox.Show("This person is already registered as a member.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Member.PersonID = selectedPersonID;
            _Member.SubscriptionDate = dtpSubscriptionDate.Value;

            if (_Member.Save())
            {
                lblMemberID.Text = _Member.MemberID.ToString();

                // Switch mode to Update after successful creation
                _Mode = enMode.Update;
                lblTitle.Text = "Update Member";
                this.Text = "Update Member";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                _isSaved = true;

                MessageBox.Show("Member details saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save member details into the database.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            if (!_isSaved && ctrlPersonCardWithFilter1.PersonID != -1)
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