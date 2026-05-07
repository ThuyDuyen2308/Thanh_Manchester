namespace PhanMemQuanLyQuanCaffe
{
    partial class frmThongKeThang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.dtgvThongKe = new System.Windows.Forms.DataGridView();
            this.lblTong = new System.Windows.Forms.Label();
            this.lblThang = new System.Windows.Forms.Label();
            this.lblNam = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // lblThang
            this.lblThang.Text = "Tháng:";
            this.lblThang.Location = new System.Drawing.Point(15, 20);
            this.lblThang.AutoSize = true;

            // nudThang
            this.nudThang.Location = new System.Drawing.Point(70, 17);
            this.nudThang.Size = new System.Drawing.Size(60, 23);

            // lblNam
            this.lblNam.Text = "Năm:";
            this.lblNam.Location = new System.Drawing.Point(150, 20);
            this.lblNam.AutoSize = true;

            // nudNam
            this.nudNam.Location = new System.Drawing.Point(190, 17);
            this.nudNam.Size = new System.Drawing.Size(80, 23);

            // btnXem
            this.btnXem.Text = "Xem";
            this.btnXem.Location = new System.Drawing.Point(285, 15);
            this.btnXem.Size = new System.Drawing.Size(80, 28);
            this.btnXem.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);

            // btnThoat
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Location = new System.Drawing.Point(375, 15);
            this.btnThoat.Size = new System.Drawing.Size(80, 28);
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // dtgvThongKe
            this.dtgvThongKe.Location = new System.Drawing.Point(15, 55);
            this.dtgvThongKe.Size = new System.Drawing.Size(555, 350);

            // lblTong
            this.lblTong.Text = "";
            this.lblTong.Location = new System.Drawing.Point(15, 415);
            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 10F,
                System.Drawing.FontStyle.Bold);
            this.lblTong.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);

            // Form
            this.ClientSize = new System.Drawing.Size(590, 450);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblThang, this.nudThang, this.lblNam, this.nudNam,
                this.btnXem, this.btnThoat, this.dtgvThongKe, this.lblTong
            });
            this.Text = "Thống Kê Doanh Thu Theo Tháng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmThongKeThang_Load);

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.DataGridView dtgvThongKe;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.Label lblNam;
    }
}