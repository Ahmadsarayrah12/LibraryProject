using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class ctrlBookCardWithFilter : UserControl
    {
        public delegate void BookSelectedEventHandler(int bookID);
        public event BookSelectedEventHandler OnBookSelected;

        public bool FilterEnabled
        {
            get { return gbFilter.Enabled; }
            set { gbFilter.Enabled = value; }
        }

        public int BookID
        {
            get { return ctrlBookCard1.BookID; }
        }

        public clsBook SelectedBook
        {
            get { return ctrlBookCard1.SelectedBook; }
        }

        public ctrlBookCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlBookCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0; // Book ID
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        public void LoadBookInfo(int bookID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = bookID.ToString();
            _FindNow();
        }

        public void LoadBookInfo(string isbn)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = isbn;
            _FindNow();
        }

        private void _FindNow()
        {
            string filterText = txtFilterValue.Text.Trim();

            if (filterText == "")
            {
                MessageBox.Show("Please enter a value to search.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbFilterBy.SelectedIndex == 0)
            {
                if (!int.TryParse(filterText, out int bookID))
                {
                    MessageBox.Show("Please enter a valid numeric Book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ctrlBookCard1.LoadBookInfo(bookID);
            }
            else
            {
                ctrlBookCard1.LoadBookInfo(filterText);
            }

            // Raise the event if a book was actually selected
            if (ctrlBookCard1.BookID != -1)
            {
                if (OnBookSelected != null)
                {
                    OnBookSelected(ctrlBookCard1.BookID);
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _FindNow();
        }

        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            frmAddUpdateBook frm = new frmAddUpdateBook();
            frm.ShowDialog();
            // In a simple pattern, we don't automatically load the added book. 
            // The user can type the ID and search it if they want, 
            // or we could add a public property in frmAddUpdateBook to get the saved ID.
            if (frm.SavedBookID != -1)
            {
                LoadBookInfo(frm.SavedBookID);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
                e.Handled = true;
            }

            if (cbFilterBy.SelectedIndex == 0)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
