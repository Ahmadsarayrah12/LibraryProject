namespace LibrarySystem.UI.Fines
{
    partial class frmManageFines
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvFines = new System.Windows.Forms.DataGridView();
            this.cmsFines = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPayFine = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).BeginInit();
            this.cmsFines.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manage Fines";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvFines
            // 
            this.dgvFines.AllowUserToAddRows = false;
            this.dgvFines.AllowUserToDeleteRows = false;
            this.dgvFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFines.BackgroundColor = System.Drawing.Color.White;
            this.dgvFines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFines.ContextMenuStrip = this.cmsFines;
            this.dgvFines.Location = new System.Drawing.Point(12, 63);
            this.dgvFines.MultiSelect = false;
            this.dgvFines.Name = "dgvFines";
            this.dgvFines.ReadOnly = true;
            this.dgvFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFines.Size = new System.Drawing.Size(876, 385);
            this.dgvFines.TabIndex = 1;
            // 
            // cmsFines
            // 
            this.cmsFines.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPayFine});
            this.cmsFines.Name = "cmsFines";
            this.cmsFines.Size = new System.Drawing.Size(120, 26);
            this.cmsFines.Opening += new System.ComponentModel.CancelEventHandler(this.cmsFines_Opening);
            // 
            // tsmiPayFine
            // 
            this.tsmiPayFine.Name = "tsmiPayFine";
            this.tsmiPayFine.Size = new System.Drawing.Size(119, 22);
            this.tsmiPayFine.Text = "Pay Fine";
            this.tsmiPayFine.Click += new System.EventHandler(this.tsmiPayFine_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(788, 463);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmManageFines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 510);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvFines);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmManageFines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Fines";
            this.Load += new System.EventHandler(this.frmManageFines_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFines)).EndInit();
            this.cmsFines.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvFines;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsFines;
        private System.Windows.Forms.ToolStripMenuItem tsmiPayFine;
    }
}
