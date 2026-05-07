using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmThongKeThang : Form
    {
        public frmThongKeThang()
        {
            InitializeComponent();
        }

        private void frmThongKeThang_Load(object sender, EventArgs e)
        {
            if (!frmDangNhap.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ Admin mới được xem thống kê!",
                    "Từ Chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            // Setup controls
            nudThang.Minimum = 1;
            nudThang.Maximum = 12;
            nudThang.Value = DateTime.Today.Month;

            nudNam.Minimum = 2020;
            nudNam.Maximum = DateTime.Today.Year;
            nudNam.Value = DateTime.Today.Year;

            SetupGrid();
            ThongKe();
        }

        private void SetupGrid()
        {
            dtgvThongKe.AllowUserToAddRows = false;
            dtgvThongKe.ReadOnly = true;
            dtgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvThongKe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvThongKe.RowHeadersVisible = false;

            dtgvThongKe.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 50);
            dtgvThongKe.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvThongKe.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            dtgvThongKe.EnableHeadersVisualStyles = false;
            dtgvThongKe.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 249, 252);
        }

        private void ThongKe()
        {
            int thang = (int)nudThang.Value;
            int nam = (int)nudNam.Value;

            // Thống kê theo từng ngày trong tháng
            string sql = @"
                SELECT 
                    CAST(hd.NgayLap AS DATE)        AS [Ngày],
                    COUNT(DISTINCT hd.MaHD)         AS [Số Hóa Đơn],
                    ISNULL(SUM(ct.ThanhTien), 0)    AS [Doanh Thu]
                FROM HoaDon hd
                INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD
                WHERE MONTH(hd.NgayLap) = @Thang
                  AND YEAR(hd.NgayLap) = @Nam
                  AND hd.TrangThai = 1
                GROUP BY CAST(hd.NgayLap AS DATE)
                ORDER BY CAST(hd.NgayLap AS DATE)";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam));

            dtgvThongKe.DataSource = dt;

            if (dtgvThongKe.Columns["Doanh Thu"] != null)
            {
                dtgvThongKe.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";
                dtgvThongKe.Columns["Doanh Thu"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dtgvThongKe.Columns["Ngày"] != null)
                dtgvThongKe.Columns["Ngày"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Tổng tháng
            long tongThang = 0;
            int tongHD = 0;
            foreach (DataRow row in dt.Rows)
            {
                tongThang += Convert.ToInt64(row["Doanh Thu"]);
                tongHD += Convert.ToInt32(row["Số Hóa Đơn"]);
            }

            lblTong.Text = $"Tháng {thang}/{nam}  |  " +
                           $"Tổng hóa đơn: {tongHD}  |  " +
                           $"Doanh thu: {tongThang:N0} đ";

            // Tô màu ngày có doanh thu cao nhất
            if (dt.Rows.Count == 0) return;

            long maxDT = 0;
            foreach (DataRow row in dt.Rows)
            {
                long dt2 = Convert.ToInt64(row["Doanh Thu"]);
                if (dt2 > maxDT) maxDT = dt2;
            }

            foreach (DataGridViewRow row in dtgvThongKe.Rows)
            {
                if (row.IsNewRow) continue;
                long val = Convert.ToInt64(row.Cells["Doanh Thu"].Value);
                if (val == maxDT && maxDT > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(212, 239, 223);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                    row.DefaultCellStyle.Font =
                        new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            ThongKe();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}