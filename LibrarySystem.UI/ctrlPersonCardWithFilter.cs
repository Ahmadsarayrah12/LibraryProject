using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Composite UserControl encapsulating a search/filter toolbar over ctrlPersonCard.
    /// Emits loose-coupled Action&lt;int&gt; events for parent form notification.
    /// Incorporates DesignMode safety guards and input validation via ErrorProvider.
    /// </summary>
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        /// <summary>
        /// Custom event fired when a person is successfully located and selected.
        /// Passes the selected PersonID to subscribers.
        /// </summary>
        public event Action<int> OnPersonSelected;

        /// <summary>
        /// Safely raises the OnPersonSelected event.
        /// </summary>
        /// <param name="personID">The ID of the newly selected person.</param>
        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(personID);
            }
        }

        private bool _filterEnabled = true;

        /// <summary>
        /// Controls whether the search toolbar is enabled for user interaction.
        /// </summary>
        public bool FilterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = _filterEnabled;
            }
        }

        /// <summary>
        /// Exposes the PersonID from the embedded ctrlPersonCard instance.
        /// </summary>
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        /// <summary>
        /// Exposes the clsPerson entity from the embedded ctrlPersonCard instance.
        /// </summary>
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.PersonInfo; }
        }

        /// <summary>
        /// Initializes a new instance of ctrlPersonCardWithFilter.
        /// </summary>
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            cbFilterBy.SelectedIndex = 0; // Default filter to "Person ID"
            txtFilterValue.Focus();

            _SetupAddPersonButton();
        }

        /// <summary>
        /// Executes search query based on the selected filter type (Person ID or Phone).
        /// </summary>
        private void _FindNow()
        {
            string query = txtFilterValue.Text.Trim();

            if (string.IsNullOrWhiteSpace(query))
                return;

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    if (int.TryParse(query, out int personID))
                    {
                        if (!ctrlPersonCard1.LoadPersonInfo(personID))
                        {
                            MessageBox.Show($"Person with ID [{personID}] was not found.", "Not Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;

                case "Phone":
                    clsPerson person = clsPerson.FindByPhone(query);
                    if (person != null)
                    {
                        ctrlPersonCard1.LoadPersonInfo(person);
                    }
                    else
                    {
                        ctrlPersonCard1.ResetPersonInfo();
                        MessageBox.Show("No person found with the specified phone number.", "Not Found",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }

            if (ctrlPersonCard1.PersonID != -1)
            {
                PersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        /// <summary>
        /// Programmatically loads a person by ID and updates the filter search controls.
        /// </summary>
        /// <param name="personID">Target person primary key ID.</param>
        public void LoadPersonInfo(int personID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = personID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);

            if (ctrlPersonCard1.PersonID != -1)
            {
                PersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        /// <summary>
        /// Sets focus to the filter query input textbox.
        /// </summary>
        private Button btnAddPerson;

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void _SetupAddPersonButton()
        {
            btnAddPerson = new Button();
            btnAddPerson.Text = "Add New Person";
            btnAddPerson.BackColor = System.Drawing.Color.Teal;
            btnAddPerson.ForeColor = System.Drawing.Color.White;
            btnAddPerson.FlatStyle = FlatStyle.Flat;
            btnAddPerson.Size = new System.Drawing.Size(120, 28);
            btnAddPerson.Location = new System.Drawing.Point(525, 23);
            btnAddPerson.Cursor = Cursors.Hand;
            btnAddPerson.Click += BtnAddPerson_Click;
            gbFilter.Controls.Add(btnAddPerson);
        }

        private void BtnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Load the newly created person automatically
                LoadPersonInfo(frm.PersonID);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please provide a valid filter query value.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _FindNow();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "Search value cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }
    }
}