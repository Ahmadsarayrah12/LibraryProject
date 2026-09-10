namespace LibrarySystem.UI
{
    partial class frmManageAuthors
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvAuthors = new System.Windows.Forms.DataGridView();
            this.cmsAuthors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editAuthorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAuthorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbAddAuthor = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtBiography = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).BeginInit();
            this.cmsAuthors.SuspendLayout();
            this.gbAddAuthor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(240, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(206, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manage Authors";
            // 
            // dgvAuthors
            // 
            this.dgvAuthors.AllowUserToAddRows = false;
            this.dgvAuthors.AllowUserToDeleteRows = false;
            this.dgvAuthors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuthors.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvAuthors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuthors.ContextMenuStrip = this.cmsAuthors;
            this.dgvAuthors.Location = new System.Drawing.Point(20, 60);
            this.dgvAuthors.MultiSelect = false;
            this.dgvAuthors.Name = "dgvAuthors";
            this.dgvAuthors.ReadOnly = true;
            this.dgvAuthors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuthors.Size = new System.Drawing.Size(650, 220);
            this.dgvAuthors.TabIndex = 1;
            this.dgvAuthors.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAuthors_CellMouseDown);
            this.dgvAuthors.DoubleClick += new System.EventHandler(this.dgvAuthors_DoubleClick);
            // 
            // cmsAuthors
            // 
            this.cmsAuthors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editAuthorToolStripMenuItem,
            this.deleteAuthorToolStripMenuItem});
            this.cmsAuthors.Name = "cmsAuthors";
            this.cmsAuthors.Size = new System.Drawing.Size(147, 48);
            // 
            // editAuthorToolStripMenuItem
            // 
            this.editAuthorToolStripMenuItem.Name = "editAuthorToolStripMenuItem";
            this.editAuthorToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.editAuthorToolStripMenuItem.Text = "Edit Author";
            this.editAuthorToolStripMenuItem.Click += new System.EventHandler(this.editAuthorToolStripMenuItem_Click);
            // 
            // deleteAuthorToolStripMenuItem
            // 
            this.deleteAuthorToolStripMenuItem.Name = "deleteAuthorToolStripMenuItem";
            this.deleteAuthorToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.deleteAuthorToolStripMenuItem.Text = "Delete Author";
            this.deleteAuthorToolStripMenuItem.Click += new System.EventHandler(this.deleteAuthorToolStripMenuItem_Click);
            // 
            // gbAddAuthor
            // 
            this.gbAddAuthor.Controls.Add(this.btnCancel);
            this.gbAddAuthor.Controls.Add(this.btnSave);
            this.gbAddAuthor.Controls.Add(this.txtBiography);
            this.gbAddAuthor.Controls.Add(this.label2);
            this.gbAddAuthor.Controls.Add(this.txtFullName);
            this.gbAddAuthor.Controls.Add(this.label1);
            this.gbAddAuthor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddAuthor.Location = new System.Drawing.Point(20, 290);
            this.gbAddAuthor.Name = "gbAddAuthor";
            this.gbAddAuthor.Size = new System.Drawing.Size(650, 150);
            this.gbAddAuthor.TabIndex = 2;
            this.gbAddAuthor.TabStop = false;
            this.gbAddAuthor.Text = "Add New Author";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(540, 65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(540, 105);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save Author";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtBiography
            // 
            this.txtBiography.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBiography.Location = new System.Drawing.Point(100, 60);
            this.txtBiography.Multiline = true;
            this.txtBiography.Name = "txtBiography";
            this.txtBiography.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBiography.Size = new System.Drawing.Size(425, 75);
            this.txtBiography.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Biography:";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(100, 25);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(425, 22);
            this.txtFullName.TabIndex = 1;
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Full Name:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(575, 450);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(20, 455);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(79, 14);
            this.lblRecordsCount.TabIndex = 4;
            this.lblRecordsCount.Text = "# Records: 0";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmManageAuthors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(690, 495);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbAddAuthor);
            this.Controls.Add(this.dgvAuthors);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageAuthors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Authors";
            this.Load += new System.EventHandler(this.frmManageAuthors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthors)).EndInit();
            this.cmsAuthors.ResumeLayout(false);
            this.gbAddAuthor.ResumeLayout(false);
            this.gbAddAuthor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvAuthors;
        private System.Windows.Forms.ContextMenuStrip cmsAuthors;
        private System.Windows.Forms.ToolStripMenuItem editAuthorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAuthorToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbAddAuthor;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtBiography;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

