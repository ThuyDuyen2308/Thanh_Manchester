namespace PhanMemQuanLyQuanCaffe
{
    partial class frmChuyenBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBanNguon = new System.Windows.Forms.ComboBox();
            this.cboBanDich = new System.Windows.Forms.ComboBox();
            this.dtgvHoaDon = new System.Windows.Forms.DataGridView();
            this.lblTong = new System.Windows.Forms.Label();
            this.btnChuyenBan = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvHoaDon)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // panel1 - header
            this.panel1.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Size = new System.Drawing.Size(620, 55);

            // label3 - title
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Text = "🔄 Chuyển Bàn";

            // label1 - Bàn nguồn
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.label1.Location = new System.Drawing.Point(20, 75);
            this.label1.Text = "Bàn đang phục vụ (nguồn):";

            // cboBanNguon
            this.cboBanNguon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBanNguon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboBanNguon.Location = new System.Drawing.Point(20, 100);
            this.cboBanNguon.Size = new System.Drawing.Size(200, 32);
     

            // label2 - Bàn đích
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.label2.Location = new System.Drawing.Point(280, 75);
            this.label2.Text = "Chuyển sang bàn (đích trống):";

            // cboBanDich
            this.cboBanDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBanDich.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboBanDich.Location = new System.Drawing.Point(280, 100);
            this.cboBanDich.Size = new System.Drawing.Size(200, 32);

            // dtgvHoaDon
            this.dtgvHoaDon.AllowUserToAddRows = false;
            this.dtgvHoaDon.ColumnHeadersHeight = 30;
            this.dtgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.dtgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dtgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dtgvHoaDon.EnableHeadersVisualStyles = false;
            this.dtgvHoaDon.Location = new System.Drawing.Point(20, 150);
            this.dtgvHoaDon.RowHeadersVisible = false;
            this.dtgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvHoaDon.Size = new System.Drawing.Size(580, 230);
            this.dtgvHoaDon.ReadOnly = true;
            this.dtgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // lblTong
            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTong.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblTong.Location = new System.Drawing.Point(20, 393);
            this.lblTong.Text = "Tổng tiền: 0 đ";

            // btnChuyenBan
            this.btnChuyenBan.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnChuyenBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuyenBan.FlatAppearance.BorderSize = 0;
            this.btnChuyenBan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnChuyenBan.ForeColor = System.Drawing.Color.White;
            this.btnChuyenBan.Location = new System.Drawing.Point(300, 420);
            this.btnChuyenBan.Size = new System.Drawing.Size(180, 42);
            this.btnChuyenBan.Text = "✔ Chuyển Bàn";
      

            // btnThoat
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(490, 420);
            this.btnThoat.Size = new System.Drawing.Size(110, 42);
            this.btnThoat.Text = "✕ Thoát";
           
            // frmChuyenBan
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboBanNguon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboBanDich);
            this.Controls.Add(this.dtgvHoaDon);
            this.Controls.Add(this.lblTong);
            this.Controls.Add(this.btnChuyenBan);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChuyenBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chuyển Bàn";
            // ✅ Thêm 3 dòng event VÀO ĐÂY - trước ResumeLayout
            this.Load += new System.EventHandler(this.frmChuyenBan_Load);
            this.cboBanNguon.SelectedIndexChanged += new System.EventHandler(this.cboBanNguon_SelectedIndexChanged);
            this.btnChuyenBan.Click += new System.EventHandler(this.btnChuyenBan_Click);
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvHoaDon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBanNguon;
        private System.Windows.Forms.ComboBox cboBanDich;
        private System.Windows.Forms.DataGridView dtgvHoaDon;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Button btnChuyenBan;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel panel1;
    }
}

