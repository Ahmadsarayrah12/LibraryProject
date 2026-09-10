using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _person;
        private int _personID = -1;

        public int PersonID
        {
            get { return _personID; }
        }

        public clsPerson PersonInfo
        {
            get { return _person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void ResetPersonInfo()
        {
            _personID = -1;
            _person = null;

            lblPersonID.Text = "[???]";
            lblFullName.Text = "[???]";
            lblPhone.Text = "[???]";
            lblEmail.Text = "[???]";
        }

        private void _FillPersonInfo()
        {
            if (_person == null)
            {
                ResetPersonInfo();
                return;
            }

            _personID = _person.PersonID;

            lblPersonID.Text = _person.PersonID.ToString();
            lblFullName.Text = _person.FullName;

            if (_person.Phone != "")
                lblPhone.Text = _person.Phone;
            else
                lblPhone.Text = "N/A";

            if (_person.Email != "")
                lblEmail.Text = _person.Email;
            else
                lblEmail.Text = "N/A";
        }

        public void LoadPersonInfo(int personID)
        {
            _person = clsPerson.Find(personID);

            if (_person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + personID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
    }
}