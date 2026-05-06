using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmThongKeNgay : Form
    {
        public frmThongKeNgay()
        {
            InitializeComponent();
        }

        private void frmThongKeNgay_Load(object sender, EventArgs e)
        {
            string quyen = frmDangNhap.Quyen?.Trim().ToLower();

            if (!string.Equals(frmDangNhap.Quyen, "admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("❌ Bạn không có quyền truy cập!");
                this.Close();
                return;
            }

            // 🔥 LOAD DỮ LIỆU GIỐNG BÁN HÀNG
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
                // 🔥 check ngày
                if (dtpTu.Value.Date > dtpDen.Value.Date)
                {
                    MessageBox.Show("❌ Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                    return;
                }

                string sql = @"
                SELECT 
                    du.TenDU AS [Tên đồ uống],
                    SUM(ct.SoLuong) AS [Số lượng],
                    MAX(ct.DonGia) AS [Đơn giá],
                    SUM(ct.ThanhTien) AS [Thành tiền]
                FROM ChiTietHoaDon ct
                JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                JOIN DoUong du ON ct.MaDU = du.MaDU
                WHERE CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY du.MaDU, du.TenDU
                ORDER BY SUM(ct.ThanhTien) DESC";

                DataTable dt = ConnectSQL.Load(sql,
                    new SqlParameter("@TuNgay", dtpTu.Value.Date),
                    new SqlParameter("@DenNgay", dtpDen.Value.Date)
                );

                dtgvThongKe.DataSource = dt;

                FormatGrid();

                // 🔥 tính tổng an toàn
                long tong = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Thành tiền"] != DBNull.Value)
                        tong += Convert.ToInt64(row["Thành tiền"]);
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

            if (dtgvThongKe.Columns["Thành tiền"] != null)
            {
                dtgvThongKe.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
                dtgvThongKe.Columns["Thành tiền"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dtgvThongKe.Columns["Đơn giá"] != null)
            {
                dtgvThongKe.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
                dtgvThongKe.Columns["Đơn giá"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }
    }
}