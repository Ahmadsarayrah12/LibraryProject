using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public class frmAddUpdatePerson : Form
    {
        private int _PersonID = -1;
        private clsPerson _Person;

        private Label lblTitle;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnSave;
        private Button btnClose;
        private ErrorProvider errorProvider;

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

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblFirstName = new Label();
            this.txtFirstName = new TextBox();
            this.lblLastName = new Label();
            this.txtLastName = new TextBox();
            this.lblPhone = new Label();
            this.txtPhone = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.btnSave = new Button();
            this.btnClose = new Button();
            this.errorProvider = new ErrorProvider();

            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.MidnightBlue;
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Text = "Add New Person";

            // FirstName
            this.lblFirstName.Location = new Point(20, 80);
            this.lblFirstName.Text = "First Name:";
            this.txtFirstName.Location = new Point(100, 77);
            this.txtFirstName.Size = new Size(200, 22);

            // LastName
            this.lblLastName.Location = new Point(320, 80);
            this.lblLastName.Text = "Last Name:";
            this.txtLastName.Location = new Point(400, 77);
            this.txtLastName.Size = new Size(200, 22);

            // Phone
            this.lblPhone.Location = new Point(20, 130);
            this.lblPhone.Text = "Phone:";
            this.txtPhone.Location = new Point(100, 127);
            this.txtPhone.Size = new Size(200, 22);

            // Email
            this.lblEmail.Location = new Point(320, 130);
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new Point(400, 127);
            this.txtEmail.Size = new Size(200, 22);

            // btnSave
            this.btnSave.Location = new Point(420, 190);
            this.btnSave.Size = new Size(80, 30);
            this.btnSave.Text = "Save";
            this.btnSave.BackColor = Color.MidnightBlue;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Click += BtnSave_Click;

            // btnClose
            this.btnClose.Location = new Point(520, 190);
            this.btnClose.Size = new Size(80, 30);
            this.btnClose.Text = "Close";
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            // Validation
            this.txtFirstName.Validating += TxtRequired_Validating;
            this.txtLastName.Validating += TxtRequired_Validating;
            this.txtPhone.Validating += TxtRequired_Validating;

            // Form
            this.ClientSize = new Size(630, 250);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblFirstName);
            this.Controls.Add(txtFirstName);
            this.Controls.Add(lblLastName);
            this.Controls.Add(txtLastName);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Add/Update Person";
            this.Load += FrmAddUpdatePerson_Load;

            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
}
