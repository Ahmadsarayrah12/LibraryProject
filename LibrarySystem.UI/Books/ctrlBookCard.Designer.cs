namespace LibrarySystem.UI
{
    partial class ctrlBookCard
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.gbBookDetails = new System.Windows.Forms.GroupBox();
            this.pbCover = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBookID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGenre = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblISBN = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPublicationYear = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalCopies = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAvailableCopies = new System.Windows.Forms.Label();
            this.gbBookDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).BeginInit();
            this.SuspendLayout();
            // 
            // gbBookDetails
            // 
            this.gbBookDetails.Controls.Add(this.lblAvailableCopies);
            this.gbBookDetails.Controls.Add(this.label8);
            this.gbBookDetails.Controls.Add(this.lblTotalCopies);
            this.gbBookDetails.Controls.Add(this.label7);
            this.gbBookDetails.Controls.Add(this.lblPublicationYear);
            this.gbBookDetails.Controls.Add(this.label6);
            this.gbBookDetails.Controls.Add(this.lblISBN);
            this.gbBookDetails.Controls.Add(this.label5);
            this.gbBookDetails.Controls.Add(this.lblGenre);
            this.gbBookDetails.Controls.Add(this.label4);
            this.gbBookDetails.Controls.Add(this.lblAuthor);
            this.gbBookDetails.Controls.Add(this.label3);
            this.gbBookDetails.Controls.Add(this.lblTitle);
            this.gbBookDetails.Controls.Add(this.label2);
            this.gbBookDetails.Controls.Add(this.lblBookID);
            this.gbBookDetails.Controls.Add(this.label1);
            this.gbBookDetails.Controls.Add(this.pbCover);
            this.gbBookDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBookDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBookDetails.Location = new System.Drawing.Point(0, 0);
            this.gbBookDetails.Name = "gbBookDetails";
            this.gbBookDetails.Size = new System.Drawing.Size(642, 235);
            this.gbBookDetails.TabIndex = 0;
            this.gbBookDetails.TabStop = false;
            this.gbBookDetails.Text = "Book Information";
            // 
            // pbCover
            // 
            this.pbCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCover.Location = new System.Drawing.Point(15, 25);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new System.Drawing.Size(140, 190);
            this.pbCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCover.TabIndex = 0;
            this.pbCover.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Book ID:";
            // 
            // lblBookID
            // 
            this.lblBookID.AutoSize = true;
            this.lblBookID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookID.Location = new System.Drawing.Point(270, 30);
            this.lblBookID.Name = "lblBookID";
            this.lblBookID.Size = new System.Drawing.Size(35, 14);
            this.lblBookID.TabIndex = 2;
            this.lblBookID.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Title:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitle.Location = new System.Drawing.Point(270, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(38, 14);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Author:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(270, 90);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(35, 14);
            this.lblAuthor.TabIndex = 6;
            this.lblAuthor.Text = "[???]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Genre:";
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenre.Location = new System.Drawing.Point(270, 120);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(35, 14);
            this.lblGenre.TabIndex = 8;
            this.lblGenre.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "ISBN:";
            // 
            // lblISBN
            // 
            this.lblISBN.AutoSize = true;
            this.lblISBN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblISBN.Location = new System.Drawing.Point(270, 150);
            this.lblISBN.Name = "lblISBN";
            this.lblISBN.Size = new System.Drawing.Size(35, 14);
            this.lblISBN.TabIndex = 10;
            this.lblISBN.Text = "[???]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "Year:";
            // 
            // lblPublicationYear
            // 
            this.lblPublicationYear.AutoSize = true;
            this.lblPublicationYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPublicationYear.Location = new System.Drawing.Point(270, 180);
            this.lblPublicationYear.Name = "lblPublicationYear";
            this.lblPublicationYear.Size = new System.Drawing.Size(35, 14);
            this.lblPublicationYear.TabIndex = 12;
            this.lblPublicationYear.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(450, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 14);
            this.label7.TabIndex = 13;
            this.label7.Text = "Total Copies:";
            // 
            // lblTotalCopies
            // 
            this.lblTotalCopies.AutoSize = true;
            this.lblTotalCopies.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCopies.Location = new System.Drawing.Point(555, 30);
            this.lblTotalCopies.Name = "lblTotalCopies";
            this.lblTotalCopies.Size = new System.Drawing.Size(38, 14);
            this.lblTotalCopies.TabIndex = 14;
            this.lblTotalCopies.Text = "[???]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(450, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "Available Copies:";
            // 
            // lblAvailableCopies
            // 
            this.lblAvailableCopies.AutoSize = true;
            this.lblAvailableCopies.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCopies.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblAvailableCopies.Location = new System.Drawing.Point(555, 60);
            this.lblAvailableCopies.Name = "lblAvailableCopies";
            this.lblAvailableCopies.Size = new System.Drawing.Size(38, 14);
            this.lblAvailableCopies.TabIndex = 16;
            this.lblAvailableCopies.Text = "[???]";
            // 
            // ctrlBookCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbBookDetails);
            this.Name = "ctrlBookCard";
            this.Size = new System.Drawing.Size(642, 235);
            this.gbBookDetails.ResumeLayout(false);
            this.gbBookDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBookDetails;
        private System.Windows.Forms.PictureBox pbCover;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBookID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPublicationYear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalCopies;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAvailableCopies;
    }
}
