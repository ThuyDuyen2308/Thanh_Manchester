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
            string sql = @"
                SELECT 
                    du.TenDU          AS [Tên đồ uống],
                    SUM(ct.SoLuong)   AS [Số lượng bán],
                    ct.DonGia         AS [Đơn giá],
                    SUM(ct.ThanhTien) AS [Thành tiền]
                FROM ChiTietHoaDon ct
                INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                INNER JOIN DoUong du ON ct.MaDU = du.MaDU
                WHERE CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY du.TenDU, ct.DonGia
                ORDER BY SUM(ct.ThanhTien) DESC";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@TuNgay", dtpTu.Value.Date),
                new SqlParameter("@DenNgay", dtpDen.Value.Date)
            );

            dtgvThongKe.DataSource = dt;

            long tong = 0;
            foreach (DataRow row in dt.Rows)
                tong += Convert.ToInt64(row["Thành tiền"]);

            lblTongTien.Text = "Tổng doanh thu: " + tong.ToString("N0") + " đ";
        }

        private void dtpTu_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}