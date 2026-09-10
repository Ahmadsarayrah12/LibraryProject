using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public delegate void PersonSelectedEventHandler(int personID);
        public event PersonSelectedEventHandler OnPersonSelected;

        public bool FilterEnabled
        {
            get { return gbFilter.Enabled; }
            set { gbFilter.Enabled = value; }
        }

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

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
            cbFilterBy.SelectedIndex = 0; // "Person ID"
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void _FindNow()
        {
            string query = txtFilterValue.Text.Trim();

            if (query == "")
            {
                MessageBox.Show("Please provide a valid filter query value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbFilterBy.Text == "Person ID")
            {
                if (int.TryParse(query, out int personID))
                {
                    ctrlPersonCard1.LoadPersonInfo(personID);
                }
                else
                {
                    MessageBox.Show("Invalid Person ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (cbFilterBy.Text == "Phone")
            {
                clsPerson person = clsPerson.Find(query);
                if (person != null)
                {
                    ctrlPersonCard1.LoadPersonInfo(person.PersonID);
                }
                else
                {
                    ctrlPersonCard1.ResetPersonInfo();
                    MessageBox.Show("No person found with the specified phone number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (ctrlPersonCard1.PersonID != -1 && OnPersonSelected != null)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        public void LoadPersonInfo(int personID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = personID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);

            if (ctrlPersonCard1.PersonID != -1 && OnPersonSelected != null)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _FindNow();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
                e.Handled = true;
            }

            if (cbFilterBy.Text == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }
    }
}
