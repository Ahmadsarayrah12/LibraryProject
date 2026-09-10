using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Reusable presentation control that renders personal identity data for a clsPerson entity.
    /// Provides properties for entity access and methods for programmatic loading by ID or object reference.
    /// </summary>
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;
        private int _PersonID = -1;

        /// <summary>
        /// Gets the primary key ID of the currently displayed person (-1 if unassigned).
        /// </summary>
        public int PersonID
        {
            get { return _PersonID; }
        }

        /// <summary>
        /// Gets the active clsPerson business entity loaded in this control.
        /// </summary>
        public clsPerson PersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Resets all visual labels back to their default placeholder state.
        /// </summary>
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            _Person = null;

            lblPersonID.Text = "[???]";
            lblFullName.Text = "[???]";
            lblPhone.Text = "[???]";
            lblEmail.Text = "[???]";
        }

        /// <summary>
        /// Binds the attributes of the loaded _Person object to the corresponding UI labels.
        /// </summary>
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;

            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = $"{_Person.FirstName} {_Person.LastName}";
            lblPhone.Text = string.IsNullOrEmpty(_Person.Phone) ? "N/A" : _Person.Phone;
            lblEmail.Text = string.IsNullOrEmpty(_Person.Email) ? "N/A" : _Person.Email;
        }

        /// <summary>
        /// Queries the business layer for a person record by primary key and renders the details.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        public void LoadPersonInfo(int personID)
        {
            _Person = clsPerson.FindPerson(personID);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show($"Person with ID [{personID}] was not found!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        /// <summary>
        /// Binds an existing clsPerson object reference directly without issuing a database query.
        /// </summary>
        /// <param name="person">The pre-loaded clsPerson entity.</param>
        public void LoadPersonInfo(clsPerson person)
        {
            _Person = person;

            if (_Person == null)
            {
                ResetPersonInfo();
                return;
            }

            _FillPersonInfo();
        }
    }
}