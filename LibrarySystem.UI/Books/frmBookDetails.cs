using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Modal dialog providing comprehensive book details and a physical copy inventory manager.
    /// </summary>
    public partial class frmBookDetails : Form
    {
        private readonly int _bookID;

        public frmBookDetails(int bookID)
        {
            InitializeComponent();
            _bookID = bookID;
        }

        private void _RefreshCopiesList()
        {
            DataTable dt = clsBookCopy.GetBookCopies(_bookID);
            dgvCopies.DataSource = dt;
        }

        private void frmBookDetails_Load(object sender, EventArgs e)
        {
            if (!ctrlBookCard1.LoadBookInfo(_bookID))
            {
                MessageBox.Show($"Book with ID [{_bookID}] was not found.", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _RefreshCopiesList();
        }

        private void btnAddCopies_Click(object sender, EventArgs e)
        {
            int count = (int)nudAddCopies.Value;
            clsBook book = ctrlBookCard1.SelectedBook;

            if (book != null && book.ProvisionCopies(count))
            {
                MessageBox.Show($"{count} new copy/copies added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ctrlBookCard1.LoadBookInfo(_bookID);
                _RefreshCopiesList();
            }
            else
            {
                MessageBox.Show("Failed to add copies.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
