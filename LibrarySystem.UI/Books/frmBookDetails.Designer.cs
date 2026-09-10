namespace LibrarySystem.UI
{
    partial class frmBookDetails
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctrlBookCard1 = new LibrarySystem.UI.ctrlBookCard();
            this.gbCopies = new System.Windows.Forms.GroupBox();
            this.btnAddCopies = new System.Windows.Forms.Button();
            this.nudAddCopies = new System.Windows.Forms.NumericUpDown();
            this.dgvCopies = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbCopies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAddCopies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(260, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(158, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Book Details";
            // 
            // ctrlBookCard1
            // 
            this.ctrlBookCard1.BackColor = System.Drawing.Color.Transparent;
            this.ctrlBookCard1.Location = new System.Drawing.Point(15, 55);
            this.ctrlBookCard1.Name = "ctrlBookCard1";
            this.ctrlBookCard1.Size = new System.Drawing.Size(650, 240);
            this.ctrlBookCard1.TabIndex = 1;
            // 
            // gbCopies
            // 
            this.gbCopies.Controls.Add(this.btnAddCopies);
            this.gbCopies.Controls.Add(this.nudAddCopies);
            this.gbCopies.Controls.Add(this.dgvCopies);
            this.gbCopies.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCopies.Location = new System.Drawing.Point(15, 300);
            this.gbCopies.Name = "gbCopies";
            this.gbCopies.Size = new System.Drawing.Size(650, 200);
            this.gbCopies.TabIndex = 2;
            this.gbCopies.TabStop = false;
            this.gbCopies.Text = "Physical Copies Inventory";
            // 
            // btnAddCopies
            // 
            this.btnAddCopies.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAddCopies.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCopies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCopies.ForeColor = System.Drawing.Color.White;
            this.btnAddCopies.Location = new System.Drawing.Point(535, 20);
            this.btnAddCopies.Name = "btnAddCopies";
            this.btnAddCopies.Size = new System.Drawing.Size(100, 26);
            this.btnAddCopies.TabIndex = 2;
            this.btnAddCopies.Text = "+ Add Copies";
            this.btnAddCopies.UseVisualStyleBackColor = false;
            this.btnAddCopies.Click += new System.EventHandler(this.btnAddCopies_Click);
            // 
            // nudAddCopies
            // 
            this.nudAddCopies.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudAddCopies.Location = new System.Drawing.Point(445, 23);
            this.nudAddCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAddCopies.Name = "nudAddCopies";
            this.nudAddCopies.Size = new System.Drawing.Size(80, 22);
            this.nudAddCopies.TabIndex = 1;
            this.nudAddCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dgvCopies
            // 
            this.dgvCopies.AllowUserToAddRows = false;
            this.dgvCopies.AllowUserToDeleteRows = false;
            this.dgvCopies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCopies.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCopies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCopies.Location = new System.Drawing.Point(15, 55);
            this.dgvCopies.MultiSelect = false;
            this.dgvCopies.Name = "dgvCopies";
            this.dgvCopies.ReadOnly = true;
            this.dgvCopies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCopies.Size = new System.Drawing.Size(620, 130);
            this.dgvCopies.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(570, 510);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmBookDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(680, 550);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbCopies);
            this.Controls.Add(this.ctrlBookCard1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBookDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Details";
            this.Load += new System.EventHandler(this.frmBookDetails_Load);
            this.gbCopies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAddCopies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private ctrlBookCard ctrlBookCard1;
        private System.Windows.Forms.GroupBox gbCopies;
        private System.Windows.Forms.DataGridView dgvCopies;
        private System.Windows.Forms.Button btnAddCopies;
        private System.Windows.Forms.NumericUpDown nudAddCopies;
        private System.Windows.Forms.Button btnClose;
    }
}

