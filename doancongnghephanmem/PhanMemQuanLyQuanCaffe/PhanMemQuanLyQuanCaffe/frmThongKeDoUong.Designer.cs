using System.Windows.Forms.DataVisualization.Charting;

namespace PhanMemQuanLyQuanCaffe
{
    partial class frmThongKeDoUong
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTu = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDen = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtgvData = new System.Windows.Forms.DataGridView();
            this.lblTongSL = new System.Windows.Forms.Label();
            this.lblTongDT = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).BeginInit();
            this.SuspendLayout();

            // panel1 - header
            this.panel1.BackColor = System.Drawing.Color.FromArgb(26, 35, 50);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Size = new System.Drawing.Size(1100, 55);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Text = "📊 Thống Kê Đồ Uống Bán Chạy";

            // panel2 - filter
            this.panel2.BackColor = System.Drawing.Color.FromArgb(236, 240, 244);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpTu);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpDen);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.nudTop);
            this.panel2.Controls.Add(this.btnXem);
            this.panel2.Controls.Add(this.btnXuatExcel);
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Size = new System.Drawing.Size(1100, 55);

            this.label1.AutoSize = true; this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.label1.Location = new System.Drawing.Point(10, 18); this.label1.Text = "Từ ngày:";
            this.dtpTu.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpTu.Font = new System.Drawing.Font("Segoe UI", 10F); this.dtpTu.Location = new System.Drawing.Point(70, 14); this.dtpTu.Size = new System.Drawing.Size(115, 26);

            this.label2.AutoSize = true; this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.label2.Location = new System.Drawing.Point(200, 18); this.label2.Text = "Đến ngày:";
            this.dtpDen.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpDen.Font = new System.Drawing.Font("Segoe UI", 10F); this.dtpDen.Location = new System.Drawing.Point(270, 14); this.dtpDen.Size = new System.Drawing.Size(115, 26);

            this.label3.AutoSize = true; this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold); this.label3.Location = new System.Drawing.Point(400, 18); this.label3.Text = "Top:";
            this.nudTop.Font = new System.Drawing.Font("Segoe UI", 10F); this.nudTop.Location = new System.Drawing.Point(435, 14); this.nudTop.Size = new System.Drawing.Size(55, 26); this.nudTop.Minimum = 3; this.nudTop.Maximum = 20; this.nudTop.Value = 10;

            this.btnXem.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(510, 12);
            this.btnXem.Size = new System.Drawing.Size(120, 32);
            this.btnXem.Text = "🔍 Xem";
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);

            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(645, 12);
            this.btnXuatExcel.Size = new System.Drawing.Size(140, 32);
            this.btnXuatExcel.Text = "📋 Xuất CSV";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);

            // chart1 - biểu đồ
            chartArea.Name = "ChartArea1";
            chartArea.BackColor = System.Drawing.Color.White;
            chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea.AxisX.LabelStyle.Angle = -35;
            chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(220, 220, 220);
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.chart1.ChartAreas.Add(chartArea);

            legend.Name = "Legend1";
            legend.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chart1.Legends.Add(legend);

            series.Name = "SoLuong";
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series.Color = System.Drawing.Color.FromArgb(52, 152, 219);
            series.IsValueShownAsLabel = true;
            series.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chart1.Series.Add(series);

            this.chart1.BackColor = System.Drawing.Color.White;
            this.chart1.Location = new System.Drawing.Point(0, 110);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(680, 480);

            // dtgvData - bảng số liệu
            this.dtgvData.AllowUserToAddRows = false;
            this.dtgvData.ReadOnly = true;
            this.dtgvData.RowHeadersVisible = false;
            this.dtgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvData.ColumnHeadersHeight = 35;
            this.dtgvData.RowTemplate.Height = 32;
            this.dtgvData.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(26, 35, 50);
            this.dtgvData.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dtgvData.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dtgvData.EnableHeadersVisualStyles = false;
            this.dtgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvData.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.dtgvData.BackgroundColor = System.Drawing.Color.White;
            this.dtgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgvData.Location = new System.Drawing.Point(685, 110);
            this.dtgvData.Name = "dtgvData";
            this.dtgvData.Size = new System.Drawing.Size(415, 400);

            // lblTongSL
            this.lblTongSL.AutoSize = true;
            this.lblTongSL.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongSL.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblTongSL.Location = new System.Drawing.Point(685, 520);
            this.lblTongSL.Name = "lblTongSL";
            this.lblTongSL.Text = "Tổng số lượng: 0";

            // lblTongDT
            this.lblTongDT.AutoSize = true;
            this.lblTongDT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongDT.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.lblTongDT.Location = new System.Drawing.Point(685, 545);
            this.lblTongDT.Name = "lblTongDT";
            this.lblTongDT.Text = "Tổng doanh thu: 0 đ";

            // btnThoat
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(970, 545);
            this.btnThoat.Size = new System.Drawing.Size(120, 36);
            this.btnThoat.Text = "✕ Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 244);
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dtgvData);
            this.Controls.Add(this.lblTongSL);
            this.Controls.Add(this.lblTongDT);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmThongKeDoUong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống Kê Đồ Uống Bán Chạy";
            this.Load += new System.EventHandler(this.frmThongKeDoUong_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvData)).EndInit();
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
        private System.Windows.Forms.NumericUpDown nudTop;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dtgvData;
        private System.Windows.Forms.Label lblTongSL;
        private System.Windows.Forms.Label lblTongDT;
        private System.Windows.Forms.Button btnThoat;
    }
}