namespace LibrarySystem.UI.Dashboard
{
    partial class frmDashboard
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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTotalBooks = new System.Windows.Forms.Panel();
            this.lblTotalBooksVal = new System.Windows.Forms.Label();
            this.lblTotalBooksTitle = new System.Windows.Forms.Label();
            this.pnlTotalMembers = new System.Windows.Forms.Panel();
            this.lblTotalMembersVal = new System.Windows.Forms.Label();
            this.lblTotalMembersTitle = new System.Windows.Forms.Label();
            this.pnlActiveBorrowings = new System.Windows.Forms.Panel();
            this.lblActiveBorrowingsVal = new System.Windows.Forms.Label();
            this.lblActiveBorrowingsTitle = new System.Windows.Forms.Label();
            this.pnlOverdue = new System.Windows.Forms.Panel();
            this.lblOverdueVal = new System.Windows.Forms.Label();
            this.lblOverdueTitle = new System.Windows.Forms.Label();
            this.pnlTotalBooks.SuspendLayout();
            this.pnlTotalMembers.SuspendLayout();
            this.pnlActiveBorrowings.SuspendLayout();
            this.pnlOverdue.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(884, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Library System Dashboard";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotalBooks
            // 
            this.pnlTotalBooks.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlTotalBooks.Controls.Add(this.lblTotalBooksVal);
            this.pnlTotalBooks.Controls.Add(this.lblTotalBooksTitle);
            this.pnlTotalBooks.Location = new System.Drawing.Point(50, 100);
            this.pnlTotalBooks.Name = "pnlTotalBooks";
            this.pnlTotalBooks.Size = new System.Drawing.Size(350, 150);
            this.pnlTotalBooks.TabIndex = 1;
            // 
            // lblTotalBooksVal
            // 
            this.lblTotalBooksVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBooksVal.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBooksVal.ForeColor = System.Drawing.Color.White;
            this.lblTotalBooksVal.Location = new System.Drawing.Point(0, 50);
            this.lblTotalBooksVal.Name = "lblTotalBooksVal";
            this.lblTotalBooksVal.Size = new System.Drawing.Size(350, 100);
            this.lblTotalBooksVal.TabIndex = 1;
            this.lblTotalBooksVal.Text = "0";
            this.lblTotalBooksVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalBooksTitle
            // 
            this.lblTotalBooksTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalBooksTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBooksTitle.ForeColor = System.Drawing.Color.White;
            this.lblTotalBooksTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTotalBooksTitle.Name = "lblTotalBooksTitle";
            this.lblTotalBooksTitle.Size = new System.Drawing.Size(350, 50);
            this.lblTotalBooksTitle.TabIndex = 0;
            this.lblTotalBooksTitle.Text = "Total Books";
            this.lblTotalBooksTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotalMembers
            // 
            this.pnlTotalMembers.BackColor = System.Drawing.Color.MediumAquamarine;
            this.pnlTotalMembers.Controls.Add(this.lblTotalMembersVal);
            this.pnlTotalMembers.Controls.Add(this.lblTotalMembersTitle);
            this.pnlTotalMembers.Location = new System.Drawing.Point(450, 100);
            this.pnlTotalMembers.Name = "pnlTotalMembers";
            this.pnlTotalMembers.Size = new System.Drawing.Size(350, 150);
            this.pnlTotalMembers.TabIndex = 2;
            // 
            // lblTotalMembersVal
            // 
            this.lblTotalMembersVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalMembersVal.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMembersVal.ForeColor = System.Drawing.Color.White;
            this.lblTotalMembersVal.Location = new System.Drawing.Point(0, 50);
            this.lblTotalMembersVal.Name = "lblTotalMembersVal";
            this.lblTotalMembersVal.Size = new System.Drawing.Size(350, 100);
            this.lblTotalMembersVal.TabIndex = 1;
            this.lblTotalMembersVal.Text = "0";
            this.lblTotalMembersVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalMembersTitle
            // 
            this.lblTotalMembersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalMembersTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMembersTitle.ForeColor = System.Drawing.Color.White;
            this.lblTotalMembersTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTotalMembersTitle.Name = "lblTotalMembersTitle";
            this.lblTotalMembersTitle.Size = new System.Drawing.Size(350, 50);
            this.lblTotalMembersTitle.TabIndex = 0;
            this.lblTotalMembersTitle.Text = "Total Members";
            this.lblTotalMembersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlActiveBorrowings
            // 
            this.pnlActiveBorrowings.BackColor = System.Drawing.Color.SandyBrown;
            this.pnlActiveBorrowings.Controls.Add(this.lblActiveBorrowingsVal);
            this.pnlActiveBorrowings.Controls.Add(this.lblActiveBorrowingsTitle);
            this.pnlActiveBorrowings.Location = new System.Drawing.Point(50, 280);
            this.pnlActiveBorrowings.Name = "pnlActiveBorrowings";
            this.pnlActiveBorrowings.Size = new System.Drawing.Size(350, 150);
            this.pnlActiveBorrowings.TabIndex = 3;
            // 
            // lblActiveBorrowingsVal
            // 
            this.lblActiveBorrowingsVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActiveBorrowingsVal.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveBorrowingsVal.ForeColor = System.Drawing.Color.White;
            this.lblActiveBorrowingsVal.Location = new System.Drawing.Point(0, 50);
            this.lblActiveBorrowingsVal.Name = "lblActiveBorrowingsVal";
            this.lblActiveBorrowingsVal.Size = new System.Drawing.Size(350, 100);
            this.lblActiveBorrowingsVal.TabIndex = 1;
            this.lblActiveBorrowingsVal.Text = "0";
            this.lblActiveBorrowingsVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblActiveBorrowingsTitle
            // 
            this.lblActiveBorrowingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblActiveBorrowingsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveBorrowingsTitle.ForeColor = System.Drawing.Color.White;
            this.lblActiveBorrowingsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblActiveBorrowingsTitle.Name = "lblActiveBorrowingsTitle";
            this.lblActiveBorrowingsTitle.Size = new System.Drawing.Size(350, 50);
            this.lblActiveBorrowingsTitle.TabIndex = 0;
            this.lblActiveBorrowingsTitle.Text = "Active Borrowings";
            this.lblActiveBorrowingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlOverdue
            // 
            this.pnlOverdue.BackColor = System.Drawing.Color.Crimson;
            this.pnlOverdue.Controls.Add(this.lblOverdueVal);
            this.pnlOverdue.Controls.Add(this.lblOverdueTitle);
            this.pnlOverdue.Location = new System.Drawing.Point(450, 280);
            this.pnlOverdue.Name = "pnlOverdue";
            this.pnlOverdue.Size = new System.Drawing.Size(350, 150);
            this.pnlOverdue.TabIndex = 4;
            // 
            // lblOverdueVal
            // 
            this.lblOverdueVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOverdueVal.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueVal.ForeColor = System.Drawing.Color.White;
            this.lblOverdueVal.Location = new System.Drawing.Point(0, 50);
            this.lblOverdueVal.Name = "lblOverdueVal";
            this.lblOverdueVal.Size = new System.Drawing.Size(350, 100);
            this.lblOverdueVal.TabIndex = 1;
            this.lblOverdueVal.Text = "0";
            this.lblOverdueVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOverdueTitle
            // 
            this.lblOverdueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOverdueTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueTitle.ForeColor = System.Drawing.Color.White;
            this.lblOverdueTitle.Location = new System.Drawing.Point(0, 0);
            this.lblOverdueTitle.Name = "lblOverdueTitle";
            this.lblOverdueTitle.Size = new System.Drawing.Size(350, 50);
            this.lblOverdueTitle.TabIndex = 0;
            this.lblOverdueTitle.Text = "Overdue Books";
            this.lblOverdueTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.pnlOverdue);
            this.Controls.Add(this.pnlActiveBorrowings);
            this.Controls.Add(this.pnlTotalMembers);
            this.Controls.Add(this.pnlTotalBooks);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.pnlTotalBooks.ResumeLayout(false);
            this.pnlTotalMembers.ResumeLayout(false);
            this.pnlActiveBorrowings.ResumeLayout(false);
            this.pnlOverdue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTotalBooks;
        private System.Windows.Forms.Label lblTotalBooksVal;
        private System.Windows.Forms.Label lblTotalBooksTitle;
        private System.Windows.Forms.Panel pnlTotalMembers;
        private System.Windows.Forms.Label lblTotalMembersVal;
        private System.Windows.Forms.Label lblTotalMembersTitle;
        private System.Windows.Forms.Panel pnlActiveBorrowings;
        private System.Windows.Forms.Label lblActiveBorrowingsVal;
        private System.Windows.Forms.Label lblActiveBorrowingsTitle;
        private System.Windows.Forms.Panel pnlOverdue;
        private System.Windows.Forms.Label lblOverdueVal;
        private System.Windows.Forms.Label lblOverdueTitle;
    }
}
