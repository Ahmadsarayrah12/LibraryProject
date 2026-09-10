using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmAddUpdateMember : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _MemberID = -1;
        private clsMember _Member;
        private clsPerson _Person;

        public frmAddUpdateMember()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateMember(int memberID)
        {
            InitializeComponent();
            _MemberID = memberID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Member";
                this.Text = "Add New Member";
                _Member = new clsMember();
                _Person = new clsPerson();

                txtFirstName.Clear();
                txtLastName.Clear();
                txtPhone.Clear();
                txtEmail.Clear();

                lblMemberID.Text = "[???]";
                dtpSubscriptionDate.Value = DateTime.Now;
            }
            else
            {
                lblTitle.Text = "Update Member";
                this.Text = "Update Member";
            }
        }

        private void _LoadData()
        {
            _Member = clsMember.Find(_MemberID);

            if (_Member == null)
            {
                MessageBox.Show("Member with ID [" + _MemberID + "] was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblMemberID.Text = _Member.MemberID.ToString();
            dtpSubscriptionDate.Value = _Member.SubscriptionDate;

            _Person = clsPerson.Find(_Member.PersonID);
            if (_Person != null)
            {
                txtFirstName.Text = _Person.FirstName;
                txtLastName.Text = _Person.LastName;
                txtPhone.Text = _Person.Phone;
                txtEmail.Text = _Person.Email;
            }
        }

        private void frmAddUpdateMember_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text.Trim() == "" || txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("First name and last name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();

            if (!_Person.Save())
            {
                MessageBox.Show("Failed to save Person details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Member.PersonID = _Person.PersonID;
            _Member.SubscriptionDate = dtpSubscriptionDate.Value;

            if (_Member.Save())
            {
                lblMemberID.Text = _Member.MemberID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Member";
                this.Text = "Update Member";

                MessageBox.Show("Member details saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save member details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
