using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmAddUpdatePerson : Form
    {
        private int _PersonID = -1;
        private clsPerson _Person;

        // Public property to expose the newly created person ID
        public int PersonID => _PersonID;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _PersonID = -1;
        }

        public frmAddUpdatePerson(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void FrmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                _Person = new clsPerson();
                lblTitle.Text = "Add New Person";
            }
            else
            {
                _Person = clsPerson.Find(_PersonID);
                if (_Person == null)
                {
                    MessageBox.Show("Person not found.");
                    this.Close();
                    return;
                }
                lblTitle.Text = "Update Person";
                txtFirstName.Text = _Person.FirstName;
                txtLastName.Text = _Person.LastName;
                txtPhone.Text = _Person.Phone;
                txtEmail.Text = _Person.Email;
            }
        }

        private void TxtRequired_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(tb, "This field is required.");
            }
            else
            {
                errorProvider.SetError(tb, "");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill all required fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();

            if (_Person.Save())
            {
                _PersonID = _Person.PersonID;
                MessageBox.Show("Person Saved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save Person details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
