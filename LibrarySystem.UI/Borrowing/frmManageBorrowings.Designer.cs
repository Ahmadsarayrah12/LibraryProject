namespace LibrarySystem.UI
{
    partial class frmManageBorrowings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlKpis = new System.Windows.Forms.Panel();
            this.pnlKpiReturned = new System.Windows.Forms.Panel();
            this.lblReturnedCount = new System.Windows.Forms.Label();
            this.lblReturnedTitle = new System.Windows.Forms.Label();
            this.pnlKpiOverdue = new System.Windows.Forms.Panel();
            this.lblOverdueCount = new System.Windows.Forms.Label();
            this.lblOverdueTitle = new System.Windows.Forms.Label();
            this.pnlKpiActive = new System.Windows.Forms.Panel();
            this.lblActiveCount = new System.Windows.Forms.Label();
            this.lblActiveTitle = new System.Windows.Forms.Label();
            this.pnlKpiTotal = new System.Windows.Forms.Panel();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.pnlFilterBar = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.btnBorrowBook = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbStatusFilter = new System.Windows.Forms.ComboBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.dgvBorrowings = new System.Windows.Forms.DataGridView();
            this.cmsBorrowings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiReturnBook = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiIssueLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiBookDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlKpis.SuspendLayout();
            this.pnlKpiReturned.SuspendLayout();
            this.pnlKpiOverdue.SuspendLayout();
            this.pnlKpiActive.SuspendLayout();
            this.pnlKpiTotal.SuspendLayout();
            this.pnlFilterBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowings)).BeginInit();
            this.cmsBorrowings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(437, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Circulation & Borrowings Management";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(23, 49);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(377, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Monitor real-time book circulation, track overdue loans, and process returns";
            // 
            // pnlKpis
            // 
            this.pnlKpis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKpis.Controls.Add(this.pnlKpiReturned);
            this.pnlKpis.Controls.Add(this.pnlKpiOverdue);
            this.pnlKpis.Controls.Add(this.pnlKpiActive);
            this.pnlKpis.Controls.Add(this.pnlKpiTotal);
            this.pnlKpis.Location = new System.Drawing.Point(20, 75);
            this.pnlKpis.Name = "pnlKpis";
            this.pnlKpis.Size = new System.Drawing.Size(1140, 65);
            this.pnlKpis.TabIndex = 2;
            // 
            // pnlKpiReturned
            // 
            this.pnlKpiReturned.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlKpiReturned.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiReturned.Controls.Add(this.lblReturnedCount);
            this.pnlKpiReturned.Controls.Add(this.lblReturnedTitle);
            this.pnlKpiReturned.Location = new System.Drawing.Point(858, 0);
            this.pnlKpiReturned.Name = "pnlKpiReturned";
            this.pnlKpiReturned.Size = new System.Drawing.Size(270, 62);
            this.pnlKpiReturned.TabIndex = 3;
            // 
            // lblReturnedCount
            // 
            this.lblReturnedCount.AutoSize = true;
            this.lblReturnedCount.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnedCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblReturnedCount.Location = new System.Drawing.Point(12, 24);
            this.lblReturnedCount.Name = "lblReturnedCount";
            this.lblReturnedCount.Size = new System.Drawing.Size(25, 30);
            this.lblReturnedCount.TabIndex = 1;
            this.lblReturnedCount.Text = "0";
            // 
            // lblReturnedTitle
            // 
            this.lblReturnedTitle.AutoSize = true;
            this.lblReturnedTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblReturnedTitle.Location = new System.Drawing.Point(12, 8);
            this.lblReturnedTitle.Name = "lblReturnedTitle";
            this.lblReturnedTitle.Size = new System.Drawing.Size(95, 15);
            this.lblReturnedTitle.TabIndex = 0;
            this.lblReturnedTitle.Text = "RETURNED LOANS";
            // 
            // pnlKpiOverdue
            // 
            this.pnlKpiOverdue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlKpiOverdue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiOverdue.Controls.Add(this.lblOverdueCount);
            this.pnlKpiOverdue.Controls.Add(this.lblOverdueTitle);
            this.pnlKpiOverdue.Location = new System.Drawing.Point(572, 0);
            this.pnlKpiOverdue.Name = "pnlKpiOverdue";
            this.pnlKpiOverdue.Size = new System.Drawing.Size(270, 62);
            this.pnlKpiOverdue.TabIndex = 2;
            // 
            // lblOverdueCount
            // 
            this.lblOverdueCount.AutoSize = true;
            this.lblOverdueCount.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblOverdueCount.Location = new System.Drawing.Point(12, 24);
            this.lblOverdueCount.Name = "lblOverdueCount";
            this.lblOverdueCount.Size = new System.Drawing.Size(25, 30);
            this.lblOverdueCount.TabIndex = 1;
            this.lblOverdueCount.Text = "0";
            // 
            // lblOverdueTitle
            // 
            this.lblOverdueTitle.AutoSize = true;
            this.lblOverdueTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblOverdueTitle.Location = new System.Drawing.Point(12, 8);
            this.lblOverdueTitle.Name = "lblOverdueTitle";
            this.lblOverdueTitle.Size = new System.Drawing.Size(95, 15);
            this.lblOverdueTitle.TabIndex = 0;
            this.lblOverdueTitle.Text = "OVERDUE LOANS";
            // 
            // pnlKpiActive
            // 
            this.pnlKpiActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlKpiActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiActive.Controls.Add(this.lblActiveCount);
            this.pnlKpiActive.Controls.Add(this.lblActiveTitle);
            this.pnlKpiActive.Location = new System.Drawing.Point(286, 0);
            this.pnlKpiActive.Name = "pnlKpiActive";
            this.pnlKpiActive.Size = new System.Drawing.Size(270, 62);
            this.pnlKpiActive.TabIndex = 1;
            // 
            // lblActiveCount
            // 
            this.lblActiveCount.AutoSize = true;
            this.lblActiveCount.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblActiveCount.Location = new System.Drawing.Point(12, 24);
            this.lblActiveCount.Name = "lblActiveCount";
            this.lblActiveCount.Size = new System.Drawing.Size(25, 30);
            this.lblActiveCount.TabIndex = 1;
            this.lblActiveCount.Text = "0";
            // 
            // lblActiveTitle
            // 
            this.lblActiveTitle.AutoSize = true;
            this.lblActiveTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblActiveTitle.Location = new System.Drawing.Point(12, 8);
            this.lblActiveTitle.Name = "lblActiveTitle";
            this.lblActiveTitle.Size = new System.Drawing.Size(83, 15);
            this.lblActiveTitle.TabIndex = 0;
            this.lblActiveTitle.Text = "ACTIVE LOANS";
            // 
            // pnlKpiTotal
            // 
            this.pnlKpiTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlKpiTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiTotal.Controls.Add(this.lblTotalCount);
            this.pnlKpiTotal.Controls.Add(this.lblTotalTitle);
            this.pnlKpiTotal.Location = new System.Drawing.Point(0, 0);
            this.pnlKpiTotal.Name = "pnlKpiTotal";
            this.pnlKpiTotal.Size = new System.Drawing.Size(270, 62);
            this.pnlKpiTotal.TabIndex = 0;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalCount.Location = new System.Drawing.Point(12, 24);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(25, 30);
            this.lblTotalCount.TabIndex = 1;
            this.lblTotalCount.Text = "0";
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTotalTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(78, 15);
            this.lblTotalTitle.TabIndex = 0;
            this.lblTotalTitle.Text = "TOTAL LOANS";
            // 
            // pnlFilterBar
            // 
            this.pnlFilterBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilterBar.Controls.Add(this.btnRefresh);
            this.pnlFilterBar.Controls.Add(this.btnReturnBook);
            this.pnlFilterBar.Controls.Add(this.btnBorrowBook);
            this.pnlFilterBar.Controls.Add(this.btnResetFilter);
            this.pnlFilterBar.Controls.Add(this.txtFilterValue);
            this.pnlFilterBar.Controls.Add(this.cbFilterBy);
            this.pnlFilterBar.Controls.Add(this.lblFilterBy);
            this.pnlFilterBar.Controls.Add(this.cbStatusFilter);
            this.pnlFilterBar.Controls.Add(this.lblStatusFilter);
            this.pnlFilterBar.Location = new System.Drawing.Point(20, 148);
            this.pnlFilterBar.Name = "pnlFilterBar";
            this.pnlFilterBar.Size = new System.Drawing.Size(1140, 42);
            this.pnlFilterBar.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.SlateGray;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(760, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(85, 30);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "↻ Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnReturnBook
            // 
            this.btnReturnBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturnBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnReturnBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnBook.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnBook.ForeColor = System.Drawing.Color.White;
            this.btnReturnBook.Location = new System.Drawing.Point(855, 5);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(135, 30);
            this.btnReturnBook.TabIndex = 7;
            this.btnReturnBook.Text = "↵ Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = false;
            this.btnReturnBook.Click += new System.EventHandler(this.btnReturnBook_Click);
            // 
            // btnBorrowBook
            // 
            this.btnBorrowBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrowBook.BackColor = System.Drawing.Color.ForestGreen;
            this.btnBorrowBook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrowBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowBook.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrowBook.ForeColor = System.Drawing.Color.White;
            this.btnBorrowBook.Location = new System.Drawing.Point(1000, 5);
            this.btnBorrowBook.Name = "btnBorrowBook";
            this.btnBorrowBook.Size = new System.Drawing.Size(135, 30);
            this.btnBorrowBook.TabIndex = 6;
            this.btnBorrowBook.Text = "+ Issue Loan";
            this.btnBorrowBook.UseVisualStyleBackColor = false;
            this.btnBorrowBook.Click += new System.EventHandler(this.btnBorrowBook_Click);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.BackColor = System.Drawing.Color.Gainsboro;
            this.btnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetFilter.ForeColor = System.Drawing.Color.Black;
            this.btnResetFilter.Location = new System.Drawing.Point(620, 7);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(65, 26);
            this.btnResetFilter.TabIndex = 5;
            this.btnResetFilter.Text = "Reset";
            this.btnResetFilter.UseVisualStyleBackColor = false;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(420, 8);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(190, 23);
            this.txtFilterValue.TabIndex = 4;
            this.txtFilterValue.Visible = false;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Borrowing ID",
            "Book Title",
            "ISBN",
            "Copy ID",
            "Member Name",
            "Member Phone"});
            this.cbFilterBy.Location = new System.Drawing.Point(270, 8);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(140, 23);
            this.cbFilterBy.TabIndex = 3;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(210, 12);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(56, 15);
            this.lblFilterBy.TabIndex = 2;
            this.lblFilterBy.Text = "Filter By:";
            // 
            // cbStatusFilter
            // 
            this.cbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatusFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatusFilter.FormattingEnabled = true;
            this.cbStatusFilter.Items.AddRange(new object[] {
            "All",
            "Active",
            "Overdue",
            "Returned"});
            this.cbStatusFilter.Location = new System.Drawing.Point(55, 8);
            this.cbStatusFilter.Name = "cbStatusFilter";
            this.cbStatusFilter.Size = new System.Drawing.Size(130, 23);
            this.cbStatusFilter.TabIndex = 1;
            this.cbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cbStatusFilter_SelectedIndexChanged);
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusFilter.Location = new System.Drawing.Point(3, 12);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(45, 15);
            this.lblStatusFilter.TabIndex = 0;
            this.lblStatusFilter.Text = "Status:";
            // 
            // dgvBorrowings
            // 
            this.dgvBorrowings.AllowUserToAddRows = false;
            this.dgvBorrowings.AllowUserToDeleteRows = false;
            this.dgvBorrowings.AllowUserToResizeRows = false;
            this.dgvBorrowings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBorrowings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBorrowings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBorrowings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBorrowings.ColumnHeadersHeight = 32;
            this.dgvBorrowings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBorrowings.ContextMenuStrip = this.cmsBorrowings;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBorrowings.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBorrowings.Location = new System.Drawing.Point(20, 198);
            this.dgvBorrowings.MultiSelect = false;
            this.dgvBorrowings.Name = "dgvBorrowings";
            this.dgvBorrowings.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBorrowings.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBorrowings.RowHeadersWidth = 30;
            this.dgvBorrowings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowings.Size = new System.Drawing.Size(1140, 420);
            this.dgvBorrowings.TabIndex = 4;
            this.dgvBorrowings.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBorrowings_CellFormatting);
            this.dgvBorrowings.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBorrowings_CellMouseDown);
            this.dgvBorrowings.DoubleClick += new System.EventHandler(this.dgvBorrowings_DoubleClick);
            // 
            // cmsBorrowings
            // 
            this.cmsBorrowings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReturnBook,
            this.toolStripSeparator1,
            this.tsmiIssueLoan,
            this.toolStripSeparator2,
            this.tsmiBookDetails,
            this.toolStripSeparator3,
            this.tsmiRefresh});
            this.cmsBorrowings.Name = "cmsBorrowings";
            this.cmsBorrowings.Size = new System.Drawing.Size(206, 110);
            this.cmsBorrowings.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBorrowings_Opening);
            // 
            // tsmiReturnBook
            // 
            this.tsmiReturnBook.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiReturnBook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.tsmiReturnBook.Name = "tsmiReturnBook";
            this.tsmiReturnBook.Size = new System.Drawing.Size(205, 22);
            this.tsmiReturnBook.Text = "↵ Process Book Return";
            this.tsmiReturnBook.Click += new System.EventHandler(this.tsmiReturnBook_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmiIssueLoan
            // 
            this.tsmiIssueLoan.Name = "tsmiIssueLoan";
            this.tsmiIssueLoan.Size = new System.Drawing.Size(205, 22);
            this.tsmiIssueLoan.Text = "+ Issue New Loan...";
            this.tsmiIssueLoan.Click += new System.EventHandler(this.tsmiIssueLoan_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmiBookDetails
            // 
            this.tsmiBookDetails.Name = "tsmiBookDetails";
            this.tsmiBookDetails.Size = new System.Drawing.Size(205, 22);
            this.tsmiBookDetails.Text = "📖 Show Book Details";
            this.tsmiBookDetails.Click += new System.EventHandler(this.tsmiBookDetails_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(205, 22);
            this.tsmiRefresh.Text = "↻ Refresh";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblRecordsCount.Location = new System.Drawing.Point(20, 632);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(89, 17);
            this.lblRecordsCount.TabIndex = 5;
            this.lblRecordsCount.Text = "# Records: 0";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1055, 626);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 32);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmManageBorrowings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1184, 671);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.dgvBorrowings);
            this.Controls.Add(this.pnlFilterBar);
            this.Controls.Add(this.pnlKpis);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "frmManageBorrowings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circulation & Borrowings Management";
            this.Load += new System.EventHandler(this.frmManageBorrowings_Load);
            this.pnlKpis.ResumeLayout(false);
            this.pnlKpiReturned.ResumeLayout(false);
            this.pnlKpiReturned.PerformLayout();
            this.pnlKpiOverdue.ResumeLayout(false);
            this.pnlKpiOverdue.PerformLayout();
            this.pnlKpiActive.ResumeLayout(false);
            this.pnlKpiActive.PerformLayout();
            this.pnlKpiTotal.ResumeLayout(false);
            this.pnlKpiTotal.PerformLayout();
            this.pnlFilterBar.ResumeLayout(false);
            this.pnlFilterBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowings)).EndInit();
            this.cmsBorrowings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlKpis;
        private System.Windows.Forms.Panel pnlKpiTotal;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.Panel pnlKpiActive;
        private System.Windows.Forms.Label lblActiveCount;
        private System.Windows.Forms.Label lblActiveTitle;
        private System.Windows.Forms.Panel pnlKpiOverdue;
        private System.Windows.Forms.Label lblOverdueCount;
        private System.Windows.Forms.Label lblOverdueTitle;
        private System.Windows.Forms.Panel pnlKpiReturned;
        private System.Windows.Forms.Label lblReturnedCount;
        private System.Windows.Forms.Label lblReturnedTitle;
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cbStatusFilter;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnBorrowBook;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvBorrowings;
        private System.Windows.Forms.ContextMenuStrip cmsBorrowings;
        private System.Windows.Forms.ToolStripMenuItem tsmiReturnBook;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiIssueLoan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiBookDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Button btnClose;
    }
}
