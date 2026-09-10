using System;
using System.Windows.Forms;
using LibrarySystem.Business;

namespace LibrarySystem.UI.Dashboard
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        public void _LoadData()
        {
            lblTotalBooksVal.Text = clsDashboard.GetTotalBooks().ToString();
            lblTotalMembersVal.Text = clsDashboard.GetTotalMembers().ToString();
            lblActiveBorrowingsVal.Text = clsDashboard.GetActiveBorrowings().ToString();
            lblOverdueVal.Text = clsDashboard.GetOverdueBooks().ToString();
        }
    }
}
