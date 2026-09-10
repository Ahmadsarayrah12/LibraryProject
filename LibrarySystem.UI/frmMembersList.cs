using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    /// <summary>
    /// Presentation form for managing library members.
    /// Provides listing, live filtering, and access to member enrollment and management operations.
    /// </summary>
    public partial class frmMembersList : Form
    {
        private DataTable _dtAllMembers;

        /// <summary>
        /// Initializes a new instance of frmMembersList.
        /// </summary>
        public frmMembersList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retrieves the latest member records and binds them to the data grid view.
        /// </summary>
        private void _RefreshMembersList()
        {
            _dtAllMembers = clsMember.GetAllMembers();
            dgvMembers.DataSource = _dtAllMembers;

            lblRecordsCount.Text = "Count Of Records: " + dgvMembers.Rows.Count.ToString();

            if (dgvMembers.Rows.Count > 0)
            {
                dgvMembers.Columns["MemberID"].HeaderText = "Member ID";
                dgvMembers.Columns["MemberID"].Width = 90;

                dgvMembers.Columns["PersonID"].HeaderText = "Person ID";
                dgvMembers.Columns["PersonID"].Width = 90;

                dgvMembers.Columns["FullName"].HeaderText = "Full Name";
                dgvMembers.Columns["FullName"].Width = 200;

                dgvMembers.Columns["Phone"].HeaderText = "Phone";
                dgvMembers.Columns["Phone"].Width = 120;

                dgvMembers.Columns["Email"].HeaderText = "Email";
                dgvMembers.Columns["Email"].Width = 180;

                dgvMembers.Columns["SubscriptionDate"].HeaderText = "Subscription Date";
                dgvMembers.Columns["SubscriptionDate"].Width = 150;
            }
        }

        private void frmMembersList_Load(object sender, EventArgs e)
        {
            _RefreshMembersList();
            cbFilterBy.SelectedIndex = 0; // "None"
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (cbFilterBy.Text == "None")
            {
                if (_dtAllMembers != null)
                {
                    _dtAllMembers.DefaultView.RowFilter = string.Empty;
                    lblRecordsCount.Text = "Count Of Records: " + dgvMembers.Rows.Count.ToString();
                }
            }
            else
            {
                txtFilterValue.Clear();
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_dtAllMembers == null)
                return;

            string filterColumn = string.Empty;

            switch (cbFilterBy.Text)
            {
                case "Member ID":
                    filterColumn = "MemberID";
                    break;

                case "Person ID":
                    filterColumn = "PersonID";
                    break;

                case "Full Name":
                    filterColumn = "FullName";
                    break;

                case "Phone":
                    filterColumn = "Phone";
                    break;

                case "Email":
                    filterColumn = "Email";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || filterColumn == "None")
            {
                _dtAllMembers.DefaultView.RowFilter = string.Empty;
                lblRecordsCount.Text = "Count Of Records: " + dgvMembers.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "MemberID" || filterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    _dtAllMembers.DefaultView.RowFilter = $"{filterColumn} = {value}";
                }
                else
                {
                    _dtAllMembers.DefaultView.RowFilter = $"{filterColumn} = -1";
                }
            }
            else
            {
                _dtAllMembers.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = "Count Of Records: " + dgvMembers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Member ID" || cbFilterBy.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateMember frm = new frmAddUpdateMember())
            {
                frm.ShowDialog();
            }
            _RefreshMembersList();
        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddMember.PerformClick();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow != null)
            {
                int selectedMemberID = (int)dgvMembers.CurrentRow.Cells["MemberID"].Value;
                using (frmAddUpdateMember frm = new frmAddUpdateMember(selectedMemberID))
                {
                    frm.ShowDialog();
                }
                _RefreshMembersList();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow != null)
            {
                int selectedMemberID = (int)dgvMembers.CurrentRow.Cells["MemberID"].Value;

                if (MessageBox.Show($"Are you sure you want to delete Member [{selectedMemberID}]?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (clsMember.DeleteMember(selectedMemberID))
                    {
                        MessageBox.Show("Member deleted successfully.", "Deleted",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshMembersList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete member. Member might be linked to active borrowings or records.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvMembers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvMembers.ClearSelection();
                dgvMembers.Rows[e.RowIndex].Selected = true;
            }
        }

        private void dgvMembers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow != null)
            {
                int selectedMemberID = (int)dgvMembers.CurrentRow.Cells["MemberID"].Value;
                using (frmAddUpdateMember frm = new frmAddUpdateMember(selectedMemberID))
                {
                    frm.ShowDialog();
                }
                _RefreshMembersList();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
