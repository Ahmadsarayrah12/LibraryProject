using System;
using System.ComponentModel;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Composite UserControl coupling real-time lookup filters with ctrlBookCard.
    /// Exposes decoupled OnBookSelected event for integration into loan and return forms.
    /// </summary>
    public partial class ctrlBookCardWithFilter : UserControl
    {
        public event Action<int> OnBookSelected;

        private bool _filterEnabled = true;

        public bool FilterEnabled
        {
            get => _filterEnabled;
            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = _filterEnabled;
            }
        }

        public int BookID => ctrlBookCard1.BookID;
        public clsBook SelectedBook => ctrlBookCard1.SelectedBook;

        public ctrlBookCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlBookCardWithFilter_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            cbFilterBy.SelectedIndex = 0; // Default: Book ID
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        /// <summary>
        /// Loads book information by ID into the inner card and raises OnBookSelected.
        /// </summary>
        public void LoadBookInfo(int bookID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = bookID.ToString();
            _FindNow();
        }

        /// <summary>
        /// Loads book information by ISBN into the inner card and raises OnBookSelected.
        /// </summary>
        public void LoadBookInfo(string isbn)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = isbn;
            _FindNow();
        }

        private void _FindNow()
        {
            string filterText = txtFilterValue.Text.Trim();

            if (string.IsNullOrWhiteSpace(filterText))
                return;

            bool found = false;

            if (cbFilterBy.SelectedIndex == 0) // Book ID
            {
                if (int.TryParse(filterText, out int bookID))
                {
                    found = ctrlBookCard1.LoadBookInfo(bookID);
                }
            }
            else // ISBN
            {
                found = ctrlBookCard1.LoadBookInfo(filterText);
            }

            if (!found)
            {
                MessageBox.Show("No book found with the specified search criteria.", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OnBookSelected?.Invoke(ctrlBookCard1.BookID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please correct validation errors before searching.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _FindNow();
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateBook frm = new frmAddUpdateBook())
            {
                frm.DataBack += (bookID) =>
                {
                    if (bookID > 0)
                    {
                        LoadBookInfo(bookID);
                    }
                };
                frm.ShowDialog();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
            errorProvider1.SetError(txtFilterValue, "");
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter key
            {
                btnFind.PerformClick();
                return;
            }

            if (cbFilterBy.SelectedIndex == 0) // Book ID must be numeric
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                errorProvider1.SetError(txtFilterValue, "Filter value cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, "");
            }
        }
    }
}
