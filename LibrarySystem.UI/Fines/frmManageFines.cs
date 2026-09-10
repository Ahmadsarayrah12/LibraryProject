using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI.Fines
{
    public partial class frmManageFines : Form
    {
        private DataTable _dtAllFines;

        public frmManageFines()
        {
            InitializeComponent();
        }

        private void _RefreshFinesList()
        {
            _dtAllFines = clsFine.GetAllFines();
            dgvFines.DataSource = _dtAllFines;

            if (dgvFines.Rows.Count > 0)
            {
                dgvFines.Columns[0].HeaderText = "Fine ID";
                dgvFines.Columns[0].Width = 80;

                dgvFines.Columns[1].HeaderText = "Member ID";
                dgvFines.Columns[1].Width = 80;

                dgvFines.Columns[2].HeaderText = "Member Name";
                dgvFines.Columns[2].Width = 200;

                dgvFines.Columns[3].HeaderText = "Borrowing ID";
                dgvFines.Columns[3].Width = 100;

                dgvFines.Columns[4].HeaderText = "Late Days";
                dgvFines.Columns[4].Width = 80;

                dgvFines.Columns[5].HeaderText = "Amount";
                dgvFines.Columns[5].Width = 80;
                dgvFines.Columns[5].DefaultCellStyle.Format = "C2";

                dgvFines.Columns[6].HeaderText = "Is Paid?";
                dgvFines.Columns[6].Width = 80;

                dgvFines.Columns[7].HeaderText = "Payment Date";
                dgvFines.Columns[7].Width = 150;
            }
        }

        private void frmManageFines_Load(object sender, EventArgs e)
        {
            _RefreshFinesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsFines_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dgvFines.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            bool isPaid = (bool)dgvFines.SelectedRows[0].Cells["PaymentStatus"].Value;
            tsmiPayFine.Enabled = !isPaid; // Only allow paying if it is not paid yet
        }

        private void tsmiPayFine_Click(object sender, EventArgs e)
        {
            if (dgvFines.SelectedRows.Count == 0) return;

            int fineID = (int)dgvFines.SelectedRows[0].Cells[0].Value;
            decimal amount = (decimal)dgvFines.SelectedRows[0].Cells["FineAmount"].Value;

            if (MessageBox.Show($"Are you sure you want to collect payment of {amount:C2} for Fine ID [{fineID}]?", 
                                "Confirm Payment", 
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsFine fine = clsFine.Find(fineID);
                if (fine != null)
                {
                    if (fine.PayFine())
                    {
                        MessageBox.Show("Payment recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshFinesList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to record payment. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
