using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Composite UserControl that encapsulates a search/filter toolbar over ctrlPersonCard.
    /// Exposes custom event Action&lt;int&gt; OnPersonSelected for parent form notification.
    /// Includes design-mode safety guards to avoid Visual Studio designer initialization crashes.
    /// </summary>
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        // Custom delegate event fired when a person is loaded or selected
        public event Action<int> OnPersonSelected;

        /// <summary>
        /// Raises the OnPersonSelected event safely.
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

        private bool _FilterEnabled = true;

        /// <summary>
        /// Controls whether the search toolbar is enabled or locked down.
        /// </summary>
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        /// <summary>
        /// Exposes the PersonID from the underlying ctrlPersonCard instance.
        /// </summary>
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        /// <summary>
        /// Exposes the clsPerson entity from the underlying ctrlPersonCard instance.
        /// </summary>
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.PersonInfo; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            // Guard: prevent designer runtime execution when rendering inside Visual Studio Form Designer
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            cbFilterBy.SelectedIndex = 0; // Default filter criterion to "Person ID"
            txtFilterValue.Focus();
        }

        /// <summary>
        /// Executes search query based on the selected filter type (Person ID or Phone).
        /// </summary>
        private void _FindNow()
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text.Trim()))
                return;

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    if (int.TryParse(txtFilterValue.Text.Trim(), out int personID))
                    {
                        ctrlPersonCard1.LoadPersonInfo(personID);
                    }
                    break;

                case "Phone":
                    clsPerson person = clsPerson.FindPersonByPhone(txtFilterValue.Text.Trim());
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

            // Raise the selection event to notify parent containers
            if (OnPersonSelected != null && ctrlPersonCard1.PersonID != -1)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        /// <summary>
        /// Programmatically loads a person by ID and updates the filter search textbox.
        /// </summary>
        /// <param name="personID">Target person primary key ID.</param>
        public void LoadPersonInfo(int personID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = personID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);

            if (OnPersonSelected != null && ctrlPersonCard1.PersonID != -1)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        /// <summary>
        /// Focuses the search input textbox.
        /// </summary>
        public void FilterFocus()
        {
            txtFilterValue.Focus();
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
            // Trigger search when pressing Enter
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            // Restrict input to digits only when searching by Person ID
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