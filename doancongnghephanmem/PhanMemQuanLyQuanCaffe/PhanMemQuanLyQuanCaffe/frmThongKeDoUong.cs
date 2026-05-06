using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmThongKeDoUong : Form
    {
        public frmThongKeDoUong()
        {
            InitializeComponent();
        }

        private void frmThongKeDoUong_Load(object sender, EventArgs e)
        {
            dtpTu.Value = DateTime.Today.AddDays(-30);
            dtpDen.Value = DateTime.Today;
            ThongKe();
        }

        private void ThongKe()
        {
            if (dtpTu.Value > dtpDen.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                return;
            }

            int top = (int)nudTop.Value;

            string sql = $@"
                SELECT TOP {top}
                    du.TenDU                        AS [Tên Đồ Uống],
                    SUM(ct.SoLuong)                 AS [Số Lượng],
                    du.DonGia                       AS [Đơn Giá],
                    SUM(ct.ThanhTien)               AS [Doanh Thu]
                FROM ChiTietHoaDon ct
                INNER JOIN DoUong du ON ct.MaDU = du.MaDU
                INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                WHERE hd.TrangThai = 1
                AND CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY du.TenDU, du.DonGia
                ORDER BY SUM(ct.SoLuong) DESC";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@TuNgay", dtpTu.Value.Date),
                new SqlParameter("@DenNgay", dtpDen.Value.Date));

            // ===== GRID =====
            dtgvData.DataSource = dt;
            FormatGrid();

            // ===== BIỂU ĐỒ =====
            VeChart(dt);

            // ===== TỔNG =====
            long tongSL = 0, tongDT = 0;
            foreach (DataRow r in dt.Rows)
            {
                tongSL += Convert.ToInt64(r["Số Lượng"]);
                tongDT += Convert.ToInt64(r["Doanh Thu"]);
            }

            lblTongSL.Text = $"Tổng số lượng bán: {tongSL:N0} ly";
            lblTongDT.Text = $"Tổng doanh thu: {tongDT:N0} đ";
        }

        private void VeChart(DataTable dt)
        {
            chart1.Series["SoLuong"].Points.Clear();
            chart1.Series["SoLuong"].Name = "Số Lượng";

            // Thêm series Doanh Thu nếu chưa có
            if (!chart1.Series.IsUniqueName("DoanhThu"))
                chart1.Series.RemoveAt(chart1.Series.IndexOf("DoanhThu"));

            // Màu sắc gradient cho từng cột
            Color[] colors = new Color[]
            {
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(231, 76, 60),
                Color.FromArgb(243, 156, 18),
                Color.FromArgb(26, 188, 156),
                Color.FromArgb(41, 128, 185),
                Color.FromArgb(39, 174, 96),
                Color.FromArgb(142, 68, 173),
                Color.FromArgb(192, 57, 43)
            };

            // Vẽ từ dưới lên (đảo ngược để cái cao nhất ở trên)
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                string ten = dt.Rows[i]["Tên Đồ Uống"].ToString();
                int sl = Convert.ToInt32(dt.Rows[i]["Số Lượng"]);

                DataPoint pt = new DataPoint();
                pt.SetValueY(sl);
                pt.AxisLabel = ten.Length > 15 ? ten.Substring(0, 13) + ".." : ten;
                pt.Label = sl.ToString("N0");
                pt.Color = colors[i % colors.Length];
                pt.ToolTip = $"{ten}\nSố lượng: {sl:N0}";

                chart1.Series["Số Lượng"].Points.Add(pt);
            }

            // Format chart
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Đồ Uống";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Số Lượng (ly)";
            chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            chart1.Titles.Clear();
            chart1.Titles.Add(new Title(
                $"Top {(int)nudTop.Value} Đồ Uống Bán Chạy ({dtpTu.Value:dd/MM/yyyy} - {dtpDen.Value:dd/MM/yyyy})",
                Docking.Top,
                new Font("Segoe UI", 11F, FontStyle.Bold),
                Color.FromArgb(26, 35, 50)));
        }

        private void FormatGrid()
        {
            dtgvData.AllowUserToAddRows = false;
            dtgvData.ReadOnly = true;
            dtgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dtgvData.Columns.Count >= 4)
            {
                dtgvData.Columns[0].FillWeight = 140; // Tên
                dtgvData.Columns[1].FillWeight = 70;  // SL
                dtgvData.Columns[2].FillWeight = 80;  // Đơn giá
                dtgvData.Columns[3].FillWeight = 100; // Doanh thu

                dtgvData.Columns[2].DefaultCellStyle.Format = "N0";
                dtgvData.Columns[3].DefaultCellStyle.Format = "N0";

                dtgvData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgvData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dtgvData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Tô màu top 3
            for (int i = 0; i < dtgvData.Rows.Count && i < 3; i++)
            {
                Color[] topColors = {
                    Color.FromArgb(255, 215, 0),   // vàng - top 1
                    Color.FromArgb(192, 192, 192), // bạc - top 2
                    Color.FromArgb(205, 127, 50)   // đồng - top 3
                };
                dtgvData.Rows[i].DefaultCellStyle.BackColor = topColors[i];
                dtgvData.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            ThongKe();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = $"ThongKeDoUong_{DateTime.Now:yyyyMMdd}";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            StringBuilder sb = new StringBuilder();

            // Header
            sb.AppendLine("STT,Tên Đồ Uống,Số Lượng,Đơn Giá,Doanh Thu");

            // Data
            int stt = 1;
            foreach (DataGridViewRow row in dtgvData.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine($"{stt++}," +
                    $"{row.Cells[0].Value}," +
                    $"{row.Cells[1].Value}," +
                    $"{row.Cells[2].Value}," +
                    $"{row.Cells[3].Value}");
            }

            // Tổng
            sb.AppendLine($",,{lblTongSL.Text},,{lblTongDT.Text}");

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Xuất file thành công!\n" + sfd.FileName,
                "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}