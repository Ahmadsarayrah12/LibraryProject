using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Reusable presentation control displaying personal identity details for a clsPerson entity.
    /// Provides properties for entity access, programmatic loading, and DesignMode safety guards.
    /// </summary>
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _person;
        private int _personID = -1;

        /// <summary>
        /// Gets the primary key ID of the currently displayed person (-1 if unassigned).
        /// </summary>
        public int PersonID
        {
            get { return _personID; }
        }

        /// <summary>
        /// Gets the active clsPerson business entity currently bound to this control.
        /// </summary>
        public clsPerson PersonInfo
        {
            get { return _person; }
        }

        /// <summary>
        /// Initializes a new instance of ctrlPersonCard.
        /// </summary>
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Resets all visual labels back to their default placeholder states.
        /// </summary>
        public void ResetPersonInfo()
        {
            _personID = -1;
            _person = null;

            lblPersonID.Text = "[???]";
            lblFullName.Text = "[???]";
            lblPhone.Text = "[???]";
            lblEmail.Text = "[???]";
        }

        /// <summary>
        /// Binds the attributes of the current person object to visual controls.
        /// </summary>
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
            lblPhone.Text = string.IsNullOrEmpty(_person.Phone) ? "N/A" : _person.Phone;
            lblEmail.Text = string.IsNullOrEmpty(_person.Email) ? "N/A" : _person.Email;
        }

        /// <summary>
        /// Loads personal identity details by querying BLL for the specified PersonID.
        /// </summary>
        /// <param name="personID">The unique identifier of the person.</param>
        /// <returns>True if the record was successfully loaded; otherwise, false.</returns>
        public bool LoadPersonInfo(int personID)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return false;

            if (personID <= 0)
            {
                ResetPersonInfo();
                return false;
            }

            _person = clsPerson.Find(personID);

            if (_person == null)
            {
                ResetPersonInfo();
                return false;
            }

            _FillPersonInfo();
            return true;
        }

        /// <summary>
        /// Binds a pre-hydrated clsPerson instance directly without triggering a database roundtrip.
        /// </summary>
        /// <param name="person">The pre-loaded person entity.</param>
        public void LoadPersonInfo(clsPerson person)
        {
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _person = person;

            if (_person == null)
            {
                ResetPersonInfo();
                return;
            }

            _FillPersonInfo();
        }
    }
}