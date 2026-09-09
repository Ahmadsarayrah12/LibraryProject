using System;
using System.Data;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private DataTable _dtAllUsers;

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFilterBy.SelectedIndex = 0;
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;

            lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns["UserID"].HeaderText = "User ID";
                dgvUsers.Columns["UserID"].Width = 90;

                dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
                dgvUsers.Columns["PersonID"].Width = 90;

                dgvUsers.Columns["FullName"].HeaderText = "Full Name";
                dgvUsers.Columns["FullName"].Width = 180;

                dgvUsers.Columns["Username"].HeaderText = "Username";
                dgvUsers.Columns["Username"].Width = 120;

                dgvUsers.Columns["Permissions"].HeaderText = "Permissions";
                dgvUsers.Columns["Permissions"].Width = 100;

                dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
                dgvUsers.Columns["IsActive"].Width = 90;

                dgvUsers.Columns["Phone"].HeaderText = "Phone";
                dgvUsers.Columns["Phone"].Width = 120;

                dgvUsers.Columns["Email"].HeaderText = "Email";
                dgvUsers.Columns["Email"].Width = 160;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    _dtAllUsers.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
                }
                else
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    filterColumn = "UserID";
                    break;

                case "Person ID":
                    filterColumn = "PersonID";
                    break;

                case "Full Name":
                    filterColumn = "FullName";
                    break;

                case "Username":
                    filterColumn = "Username";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtFilterValue.Text) || filterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "UserID" || filterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int value))
                {
                    _dtAllUsers.DefaultView.RowFilter = $"{filterColumn} = {value}";
                }
                else
                {
                    _dtAllUsers.DefaultView.RowFilter = $"{filterColumn} = -1";
                }
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filterValue = cbIsActive.Text;

            switch (filterValue)
            {
                case "All":
                    _dtAllUsers.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = 1";
                    break;

                case "No":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = 0";
                    break;
            }

            lblRecordsCount.Text = "Count Of Records: " + dgvUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}