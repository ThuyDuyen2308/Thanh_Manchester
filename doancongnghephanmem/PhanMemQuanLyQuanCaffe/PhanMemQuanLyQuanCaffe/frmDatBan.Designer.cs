namespace PhanMemQuanLyQuanCaffe
{
    partial class frmDatBan
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
            this.cboBan = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenKhach = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpGio = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnDatBan = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpLocNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dtgvDatBan = new System.Windows.Forms.DataGridView();
            this.btnDaDen = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDatBan)).BeginInit();
            this.SuspendLayout();

            // panel1 - header
            this.panel1.BackColor = System.Drawing.Color.FromArgb(26, 35, 50);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Size = new System.Drawing.Size(1050, 55);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Text = "Đặt Bàn Trước";

            // panel2 - form nhập
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cboBan);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtTenKhach);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtSDT);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtpNgay);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtpGio);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.nudSoNguoi);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtGhiChu);
            this.panel2.Controls.Add(this.btnDatBan);
            this.panel2.Controls.Add(this.btnLamMoi);
            this.panel2.Location = new System.Drawing.Point(10, 65);
            this.panel2.Size = new System.Drawing.Size(1030, 185);

            // label1 - Bàn
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Text = "Chọn Bàn:";

            this.cboBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboBan.Location = new System.Drawing.Point(15, 38);
            this.cboBan.Size = new System.Drawing.Size(130, 28);

            // label2 - Tên khách
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label2.Location = new System.Drawing.Point(165, 20);
            this.label2.Text = "Tên Khách:";

            this.txtTenKhach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenKhach.Location = new System.Drawing.Point(165, 38);
            this.txtTenKhach.Size = new System.Drawing.Size(180, 26);

            // label3 - SĐT
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label3.Location = new System.Drawing.Point(365, 20);
            this.label3.Text = "Số Điện Thoại:";

            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSDT.Location = new System.Drawing.Point(365, 38);
            this.txtSDT.Size = new System.Drawing.Size(140, 26);

            // label4 - Ngày
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label4.Location = new System.Drawing.Point(520, 20);
            this.label4.Text = "Ngày Đặt:";

            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgay.Location = new System.Drawing.Point(520, 38);
            this.dtpNgay.Size = new System.Drawing.Size(120, 26);

            // label5 - Giờ
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label5.Location = new System.Drawing.Point(655, 20);
            this.label5.Text = "Giờ Đến:";

            this.dtpGio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGio.ShowUpDown = true;
            this.dtpGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpGio.Location = new System.Drawing.Point(655, 38);
            this.dtpGio.Size = new System.Drawing.Size(100, 26);

            // label6 - Số người
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label6.Location = new System.Drawing.Point(770, 20);
            this.label6.Text = "Số Người:";

            this.nudSoNguoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSoNguoi.Location = new System.Drawing.Point(770, 38);
            this.nudSoNguoi.Size = new System.Drawing.Size(60, 26);
            this.nudSoNguoi.Minimum = 1;
            this.nudSoNguoi.Maximum = 50;
            this.nudSoNguoi.Value = 2;

            // label7 - Ghi chú
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label7.Location = new System.Drawing.Point(15, 80);
            this.label7.Text = "Ghi Chú:";

            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(15, 98);
            this.txtGhiChu.Size = new System.Drawing.Size(700, 26);

            // btnDatBan
            this.btnDatBan.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnDatBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatBan.FlatAppearance.BorderSize = 0;
            this.btnDatBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDatBan.ForeColor = System.Drawing.Color.White;
            this.btnDatBan.Location = new System.Drawing.Point(740, 90);
            this.btnDatBan.Size = new System.Drawing.Size(140, 38);
            this.btnDatBan.Text = "Đặt Bàn";
            this.btnDatBan.Click += new System.EventHandler(this.btnDatBan_Click);

            // btnLamMoi
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(890, 90);
            this.btnLamMoi.Size = new System.Drawing.Size(120, 38);
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // label8 - Lọc ngày
            this.label8 = new System.Windows.Forms.Label();
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.label8.Location = new System.Drawing.Point(10, 265);
            this.label8.Text = "Xem lịch đặt bàn ngày:";

            this.dtpLocNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpLocNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLocNgay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpLocNgay.Location = new System.Drawing.Point(170, 261);
            this.dtpLocNgay.Size = new System.Drawing.Size(120, 26);

            this.btnLoc = new System.Windows.Forms.Button();
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.FlatAppearance.BorderSize = 0;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(300, 259);
            this.btnLoc.Size = new System.Drawing.Size(100, 30);
            this.btnLoc.Text = "Lọc";
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);

            // dtgvDatBan
            this.dtgvDatBan.AllowUserToAddRows = false;
            this.dtgvDatBan.ReadOnly = true;
            this.dtgvDatBan.RowHeadersVisible = false;
            this.dtgvDatBan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvDatBan.ColumnHeadersHeight = 35;
            this.dtgvDatBan.RowTemplate.Height = 30;
            this.dtgvDatBan.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(26, 35, 50);
            this.dtgvDatBan.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dtgvDatBan.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtgvDatBan.EnableHeadersVisualStyles = false;
            this.dtgvDatBan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvDatBan.BackgroundColor = System.Drawing.Color.White;
            this.dtgvDatBan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgvDatBan.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.dtgvDatBan.Location = new System.Drawing.Point(10, 300);
            this.dtgvDatBan.Size = new System.Drawing.Size(1030, 280);

            // btnDaDen
            this.btnDaDen = new System.Windows.Forms.Button();
            this.btnDaDen.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnDaDen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDaDen.FlatAppearance.BorderSize = 0;
            this.btnDaDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDaDen.ForeColor = System.Drawing.Color.White;
            this.btnDaDen.Location = new System.Drawing.Point(10, 590);
            this.btnDaDen.Size = new System.Drawing.Size(150, 38);
            this.btnDaDen.Text = "Khách Đã Đến";
            this.btnDaDen.Click += new System.EventHandler(this.btnDaDen_Click);

            // btnHuy
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(170, 590);
            this.btnHuy.Size = new System.Drawing.Size(150, 38);
            this.btnHuy.Text = "Hủy Đặt Bàn";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // btnThoat
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(890, 590);
            this.btnThoat.Size = new System.Drawing.Size(150, 38);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 244);
            this.ClientSize = new System.Drawing.Size(1050, 645);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpLocNgay);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.dtgvDatBan);
            this.Controls.Add(this.btnDaDen);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmDatBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt Bàn Trước";
            this.Load += new System.EventHandler(this.frmDatBan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDatBan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboBan;
        private System.Windows.Forms.TextBox txtTenKhach;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.DateTimePicker dtpGio;
        private System.Windows.Forms.NumericUpDown nudSoNguoi;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnDatBan;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DateTimePicker dtpLocNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.DataGridView dtgvDatBan;
        private System.Windows.Forms.Button btnDaDen;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnThoat;
    }
}