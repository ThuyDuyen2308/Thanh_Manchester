namespace PhanMemQuanLyQuanCaffe
{
    partial class frmMenuTheoNgay
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
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.dtgvMenu = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.themlistbox = new System.Windows.Forms.ListBox();
            this.lichsu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(531, 109);
            this.dtpNgay.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(265, 22);
            this.dtpNgay.TabIndex = 0;
            this.dtpNgay.ValueChanged += new System.EventHandler(this.dtpNgay_ValueChanged);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(833, 139);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(4);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 28);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(833, 175);
            this.btnXem.Margin = new System.Windows.Forms.Padding(4);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(100, 28);
            this.btnXem.TabIndex = 3;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // dtgvMenu
            // 
            this.dtgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvMenu.Location = new System.Drawing.Point(337, 139);
            this.dtgvMenu.Margin = new System.Windows.Forms.Padding(4);
            this.dtgvMenu.Name = "dtgvMenu";
            this.dtgvMenu.RowHeadersWidth = 51;
            this.dtgvMenu.Size = new System.Drawing.Size(459, 412);
            this.dtgvMenu.TabIndex = 4;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.OrangeRed;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(833, 210);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 30);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "Xóa Menu";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Gray;
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(833, 246);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // themlistbox
            // 
            this.themlistbox.FormattingEnabled = true;
            this.themlistbox.ItemHeight = 16;
            this.themlistbox.Location = new System.Drawing.Point(1, 131);
            this.themlistbox.Name = "themlistbox";
            this.themlistbox.Size = new System.Drawing.Size(329, 420);
            this.themlistbox.TabIndex = 7;
            this.themlistbox.DoubleClick += new System.EventHandler(this.themlistbox_DoubleClick);
            // 
            // lichsu
            // 
            this.lichsu.BackColor = System.Drawing.Color.Gray;
            this.lichsu.ForeColor = System.Drawing.Color.White;
            this.lichsu.Location = new System.Drawing.Point(833, 282);
            this.lichsu.Name = "lichsu";
            this.lichsu.Size = new System.Drawing.Size(100, 30);
            this.lichsu.TabIndex = 8;
            this.lichsu.Text = "Xem Lịch Sử";
            this.lichsu.UseVisualStyleBackColor = false;
            this.lichsu.Click += new System.EventHandler(this.lichsu_Click);
            // 
            // frmMenuTheoNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.lichsu);
            this.Controls.Add(this.themlistbox);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.dtgvMenu);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dtpNgay);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMenuTheoNgay";
            this.Text = "Menu Theo Ngày";
            this.Load += new System.EventHandler(this.frmMenuTheoNgay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.DataGridView dtgvMenu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ListBox themlistbox;
        private System.Windows.Forms.Button lichsu;
    }
}