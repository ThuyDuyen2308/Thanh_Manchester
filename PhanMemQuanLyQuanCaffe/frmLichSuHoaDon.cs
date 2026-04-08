using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmLichSuHoaDon : Form
    {
        public frmLichSuHoaDon()
        {
            InitializeComponent();
        }

        private void frmLichSuHoaDon_Load(object sender, EventArgs e)
        {
            if (frmDangNhap.Quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền xem chức năng này!",
                    "Từ Chối Truy Cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // ← Đóng sau khi Load hoàn tất
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

            dtpTu.Value = DateTime.Today;
            dtpDen.Value = DateTime.Today;
            XemLichSu();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            XemLichSu();
        }

        private void XemLichSu()
        {
            if (dtpTu.Value > dtpDen.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                return;
            }

            string sql = @"
                SELECT 
                    hd.MaHD                 AS [Mã HD],
                    hd.MaBan                AS [Bàn],
                    nv.TenNV                AS [Nhân Viên],
                    hd.NgayLap              AS [Ngày Lập],
                    SUM(ct.ThanhTien)       AS [Tổng Tiền],
                    CASE hd.TrangThai 
                        WHEN 1 THEN N'Đã thanh toán'
                        ELSE N'Chưa thanh toán'
                    END                     AS [Trạng Thái]
                FROM HoaDon hd
                INNER JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD
                WHERE CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                GROUP BY hd.MaHD, hd.MaBan, nv.TenNV, hd.NgayLap, hd.TrangThai
                ORDER BY hd.NgayLap DESC";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@TuNgay", dtpTu.Value.Date),
                new SqlParameter("@DenNgay", dtpDen.Value.Date)
            );

            dtgvLichSu.DataSource = dt;
            FormatGrid();

            long tong = 0;
            foreach (DataRow row in dt.Rows)
                tong += Convert.ToInt64(row["Tổng Tiền"]);

            lblTong.Text = "Tổng: " + tong.ToString("N0") + " đ  |  Số hóa đơn: " + dt.Rows.Count;
        }

        private void FormatGrid()
        {
            dtgvLichSu.AllowUserToAddRows = false;
            dtgvLichSu.ReadOnly = true;
            dtgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dtgvLichSu.Columns["Tổng Tiền"] != null)
                dtgvLichSu.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";

            if (dtgvLichSu.Columns["Ngày Lập"] != null)
                dtgvLichSu.Columns["Ngày Lập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }
    }
}