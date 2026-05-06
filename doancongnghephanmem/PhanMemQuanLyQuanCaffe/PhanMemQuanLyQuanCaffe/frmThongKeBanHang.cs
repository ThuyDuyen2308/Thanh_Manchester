using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmThongKeBanHang : Form
    {
        public frmThongKeBanHang()
        {
            InitializeComponent();
        }

        private void frmThongKeBanHang_Load(object sender, EventArgs e)
        {
            // 🔥 FIX QUYỀN CHUẨN (KHÔNG LỖI HOA THƯỜNG + NULL)
            if (string.IsNullOrEmpty(frmDangNhap.Quyen) ||
                frmDangNhap.Quyen.ToLower() != "admin")
            {
                MessageBox.Show("❌ Bạn không có quyền truy cập!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            dtpTu.Value = DateTime.Today;
            dtpDen.Value = DateTime.Today;

            ThongKe();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ThongKe();
        }

        private void ThongKe()
        {
            try
            {
                if (dtpTu.Value.Date > dtpDen.Value.Date)
                {
                    MessageBox.Show("❌ Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                    return;
                }

                string sql = @"
                    SELECT 
                        b.MaBan                 AS [Mã Bàn],
                        COUNT(DISTINCT hd.MaHD) AS [Số Hóa Đơn],
                        ISNULL(SUM(ct.ThanhTien),0) AS [Doanh Thu]
                    FROM HoaDon hd
                    INNER JOIN Ban b ON hd.MaBan = b.MaBan
                    INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD
                    WHERE CAST(hd.NgayLap AS DATE) 
                          BETWEEN @TuNgay AND @DenNgay
                    GROUP BY b.MaBan
                    ORDER BY SUM(ct.ThanhTien) DESC";

                DataTable dt = ConnectSQL.Load(sql,
                    new SqlParameter("@TuNgay", dtpTu.Value.Date),
                    new SqlParameter("@DenNgay", dtpDen.Value.Date)
                );

                dtgvThongKe.DataSource = dt;

                FormatGrid();

                // 🔥 TÍNH TỔNG AN TOÀN
                long tong = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Doanh Thu"] != DBNull.Value)
                        tong += Convert.ToInt64(row["Doanh Thu"]);
                }

                lblTongTien.Text = "Tổng doanh thu: " + tong.ToString("N0") + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê:\n" + ex.Message);
            }
        }

        private void FormatGrid()
        {
            dtgvThongKe.AllowUserToAddRows = false;
            dtgvThongKe.ReadOnly = true;
            dtgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dtgvThongKe.Columns["Doanh Thu"] != null)
            {
                dtgvThongKe.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";
                dtgvThongKe.Columns["Doanh Thu"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }
    }
}