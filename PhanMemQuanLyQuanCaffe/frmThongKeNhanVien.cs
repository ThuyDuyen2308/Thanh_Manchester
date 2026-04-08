using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmThongKeNhanVien : Form
    {
        public frmThongKeNhanVien()
        {
            InitializeComponent();
        }

        private void frmThongKeNhanVien_Load(object sender, EventArgs e)
        {
            // ===== KIỂM TRA QUYỀN ADMIN =====
            if (frmDangNhap.Quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền xem chức năng này!",
                    "Từ Chối Truy Cập",
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
            if (dtpTu.Value > dtpDen.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                return;
            }

            string sql = @"
                SELECT 
                    nv.MaNV                     AS [Mã NV],
                    nv.TenNV                    AS [Tên Nhân Viên],
                    COUNT(DISTINCT hd.MaHD)     AS [Số Hóa Đơn],
                    SUM(ct.ThanhTien)           AS [Doanh Thu]
                FROM HoaDon hd
                INNER JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD
                WHERE CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY nv.MaNV, nv.TenNV
                ORDER BY SUM(ct.ThanhTien) DESC";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@TuNgay", dtpTu.Value.Date),
                new SqlParameter("@DenNgay", dtpDen.Value.Date)
            );

            dtgvThongKe.DataSource = dt;
            FormatGrid();

            // Tính tổng
            long tong = 0;
            foreach (DataRow row in dt.Rows)
                tong += Convert.ToInt64(row["Doanh Thu"]);

            lblTongTien.Text = "Tổng doanh thu: " + tong.ToString("N0") + " đ";
        }

        private void FormatGrid()
        {
            dtgvThongKe.AllowUserToAddRows = false;
            dtgvThongKe.ReadOnly = true;
            dtgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dtgvThongKe.Columns["Doanh Thu"] != null)
                dtgvThongKe.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";
        }

        private void dtgvThongKe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}