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
            InitializeComponent();// khai báo giao diện ( tuy thuộc vào deginer )
        }

        private void frmLichSuHoaDon_Load(object sender, EventArgs e) // sự kiện load form
        {
            if (!frmDangNhap.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase)) // Chỉ Admin mới được xem lịch sử
            {
                MessageBox.Show("Bạn không có quyền xem chức năng này!",
                    "Từ Chối Truy Cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // ← Đóng sau khi Load hoàn tất
                this.BeginInvoke(new Action(() => this.Close()));// Đóng form ngay sau khi load xong
                return;
            }

            dtpTu.Value = DateTime.Today;// Mặc định ngày bắt đầu là hôm nay
            dtpDen.Value = DateTime.Today;// Mặc định ngày kết thúc là hôm nay
            XemLichSu();
        }

        private void btnXem_Click(object sender, EventArgs e)// sự kiện click nút xem
        {
            XemLichSu();// Gọi hàm xem lịch sử khi nhấn nút
        }

        private void XemLichSu()// Hàm xem lịch sử hóa đơn
        {
            if (dtpTu.Value > dtpDen.Value)// Kiểm tra nếu ngày bắt đầu lớn hơn ngày kết thúc
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!");// Hiển thị thông báo lỗi
                return;
            }

            string sql = @"// Truy vấn lấy lịch sử hóa đơn trong khoảng thời gian đã chọn
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
                INNER JOIN NhanVien nv ON hd.MaNV = nv.MaNV// Kết nối bảng hóa đơn với bảng nhân viên để lấy tên nhân viên BẰNG phép nối
                INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD// Kết nối bảng hóa đơn với bảng chi tiết hóa đơn để tính tổng tiền
                WHERE CAST(hd.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay// Lọc hóa đơn theo ngày lập trong khoảng đã chọn
                GROUP BY hd.MaHD, hd.MaBan, nv.TenNV, hd.NgayLap, hd.TrangThai// Nhóm theo các trường cần thiết để tính tổng tiền và hiển thị thông tin
                ORDER BY hd.NgayLap DESC";// Sắp xếp kết quả theo ngày lập giảm dần

            DataTable dt = ConnectSQL.Load(sql,// Thực thi truy vấn với tham số ngày bắt đầu và ngày kết thúc
                new SqlParameter("@TuNgay", dtpTu.Value.Date),// Chuyển ngày bắt đầu về dạng Date để so sánh chính xác
                new SqlParameter("@DenNgay", dtpDen.Value.Date)// Chuyển ngày kết thúc về dạng Date để so sánh chính xác
            );

            dtgvLichSu.DataSource = dt;// Hiển thị kết quả lên DataGridView
            FormatGrid();// Định dạng lại DataGridView cho đẹp mắt

            long tong = 0;// Biến để tính tổng tiền của tất cả hóa đơn
            foreach (DataRow row in dt.Rows)// Duyệt qua từng dòng kết quả để tính tổng tiền
                tong += Convert.ToInt64(row["Tổng Tiền"]);// Cộng dồn tổng tiền từ mỗi hóa đơn

            lblTong.Text = "Tổng: " + tong.ToString("N0") + " đ  |  Số hóa đơn: " + dt.Rows.Count;// Hiển thị tổng tiền và số lượng hóa đơn lên label
        }

        private void FormatGrid()// Hàm định dạng DataGridView
        {
            dtgvLichSu.AllowUserToAddRows = false; // Không cho phép người dùng thêm dòng mới
            dtgvLichSu.ReadOnly = true;// Chỉ cho phép đọc, không chỉnh sửa dữ liệu
            dtgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// Tự động điều chỉnh kích thước cột để vừa với DataGridView

            if (dtgvLichSu.Columns["Tổng Tiền"] != null)// Kiểm tra nếu cột "Tổng Tiền" tồn tại
                dtgvLichSu.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";// Định dạng cột "Tổng Tiền" hiển thị số với dấu phân cách hàng nghìn và không có chữ "đ"

            if (dtgvLichSu.Columns["Ngày Lập"] != null)//   Kiểm tra nếu cột "Ngày Lập" tồn tại
                dtgvLichSu.Columns["Ngày Lập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";//  Định dạng cột "Ngày Lập" hiển thị ngày giờ theo định dạng ngày/tháng/năm giờ:phút
        }
    }
}