namespace PhanMemQuanLyQuanCaffe
{
    partial class frmLichSuNgayLam
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.btnXem = new System.Windows.Forms.Button();
            this.dtgvLichSu = new System.Windows.Forms.DataGridView();
            this.lblTongCa = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLichSu)).BeginInit();
            this.SuspendLayout();

            // panel1
            this.panel1.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Size = new System.Drawing.Size(900, 55);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Text = "Lich Su Ngay Lam Nhan Vien";

            // panel2
            this.panel2.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpTu);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpDen);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cboNhanVien);
            this.panel2.Controls.Add(this.btnXem);
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Size = new System.Drawing.Size(900, 60);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Text = "Tu ngay:";

            // dtpTu
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTu.Location = new System.Drawing.Point(80, 16);
            this.dtpTu.Size = new System.Drawing.Size(120, 30);

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(215, 20);
            this.label2.Text = "Den ngay:";

            // dtpDen
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDen.Location = new System.Drawing.Point(290, 16);
            this.dtpDen.Size = new System.Drawing.Size(120, 30);

            // label3
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(425, 20);
            this.label3.Text = "Nhan vien:";

            // cboNhanVien
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhanVien.Location = new System.Drawing.Point(505, 16);
            this.cboNhanVien.Size = new System.Drawing.Size(200, 30);

            // btnXem
            this.btnXem.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(720, 14);
            this.btnXem.Size = new System.Drawing.Size(130, 34);
            this.btnXem.Text = "Xem";
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);

            // dtgvLichSu
            this.dtgvLichSu.AllowUserToAddRows = false;
            this.dtgvLichSu.ReadOnly = true;
            this.dtgvLichSu.RowHeadersVisible = false;
            this.dtgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvLichSu.ColumnHeadersHeight = 35;
            this.dtgvLichSu.RowTemplate.Height = 30;
            this.dtgvLichSu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.dtgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dtgvLichSu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtgvLichSu.EnableHeadersVisualStyles = false;
            this.dtgvLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvLichSu.Location = new System.Drawing.Point(0, 115);
            this.dtgvLichSu.Size = new System.Drawing.Size(900, 390);
            this.dtgvLichSu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgvLichSu.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // lblTongCa
            this.lblTongCa.AutoSize = true;
            this.lblTongCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongCa.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTongCa.Location = new System.Drawing.Point(15, 518);
            this.lblTongCa.Text = "Tong ca: 0  |  Tong gio lam: 0 gio 0 phut";

            // btnThoat
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(775, 510);
            this.btnThoat.Size = new System.Drawing.Size(110, 36);
            this.btnThoat.Text = "Thoat";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dtgvLichSu);
            this.Controls.Add(this.lblTongCa);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLichSuNgayLam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lich Su Ngay Lam Nhan Vien";
            this.Load += new System.EventHandler(this.frmLichSuNgayLam_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvLichSu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTu;
        private System.Windows.Forms.DateTimePicker dtpDen;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.DataGridView dtgvLichSu;
        private System.Windows.Forms.Label lblTongCa;
        private System.Windows.Forms.Button btnThoat;
    }
}