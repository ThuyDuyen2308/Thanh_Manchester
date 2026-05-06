using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmDatBan : Form
    {
        public frmDatBan()
        {
            InitializeComponent();
        }

        private void frmDatBan_Load(object sender, EventArgs e)
        {
            dtpNgay.Value = DateTime.Today;
            dtpLocNgay.Value = DateTime.Today;
            dtpGio.Value = DateTime.Today.AddHours(DateTime.Now.Hour + 1);
            LoadBan();
            LoadDatBan();
        }

        // ================== LOAD BÀN ==================
        private void LoadBan()
        {
            DataTable dt = ConnectSQL.Load(
                "SELECT MaBan FROM Ban ORDER BY MaBan");
            cboBan.Items.Clear();
            foreach (DataRow r in dt.Rows)
                cboBan.Items.Add(r["MaBan"].ToString().Trim());
            if (cboBan.Items.Count > 0)
                cboBan.SelectedIndex = 0;
        }

        // ================== LOAD DANH SÁCH ==================
        private void LoadDatBan()
        {
            string sql = @"
                SELECT
                    MaDatBan        AS [Mã Đặt],
                    MaBan           AS [Bàn],
                    TenKhach        AS [Tên Khách],
                    SoDienThoai     AS [SĐT],
                    NgayDat         AS [Ngày],
                    CONVERT(VARCHAR,GioDat,108) AS [Giờ Đến],
                    SoNguoi         AS [Số Người],
                    GhiChu          AS [Ghi Chú],
                    CASE TrangThai
                        WHEN 0 THEN N'Chờ xác nhận'
                        WHEN 1 THEN N'Khách đã đến'
                        WHEN 2 THEN N'Đã hủy'
                    END             AS [Trạng Thái]
                FROM DatBan
                WHERE NgayDat = @Ngay
                ORDER BY GioDat ASC";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@Ngay", dtpLocNgay.Value.Date));

            dtgvDatBan.DataSource = dt;
            FormatGrid();
        }

        private void FormatGrid()
        {
            dtgvDatBan.AllowUserToAddRows = false;
            dtgvDatBan.ReadOnly = true;
            dtgvDatBan.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Tô màu theo trạng thái
            foreach (DataGridViewRow row in dtgvDatBan.Rows)
            {
                if (row.IsNewRow) continue;
                string tt = row.Cells["Trạng Thái"].Value?.ToString();

                if (tt == "Khách đã đến")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.FromArgb(212, 239, 223);
                    row.DefaultCellStyle.ForeColor =
                        Color.FromArgb(39, 174, 96);
                }
                else if (tt == "Đã hủy")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.FromArgb(250, 219, 216);
                    row.DefaultCellStyle.ForeColor =
                        Color.FromArgb(192, 57, 43);
                }
            }
        }

        // ================== ĐẶT BÀN ==================
        private void btnDatBan_Click(object sender, EventArgs e)
        {
            if (cboBan.SelectedItem == null)
            { MessageBox.Show("Vui lòng chọn bàn!"); return; }

            if (string.IsNullOrWhiteSpace(txtTenKhach.Text))
            { MessageBox.Show("Vui lòng nhập tên khách!"); return; }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            { MessageBox.Show("Vui lòng nhập số điện thoại!"); return; }

            string maBan = cboBan.SelectedItem.ToString();
            DateTime ngayDat = dtpNgay.Value.Date;
            TimeSpan gioDat = dtpGio.Value.TimeOfDay;

            // Kiểm tra bàn đã được đặt trong khung giờ đó chưa
            // (cách nhau dưới 2 tiếng)
            string sqlCheck = @"
                SELECT COUNT(*) FROM DatBan
                WHERE MaBan = @MaBan
                AND NgayDat = @Ngay
                AND TrangThai = 0
                AND ABS(DATEDIFF(MINUTE, GioDat, @Gio)) < 120";

            int soLuong = Convert.ToInt32(ConnectSQL.ExecuteScalar(sqlCheck,
                new SqlParameter("@MaBan", maBan),
                new SqlParameter("@Ngay", ngayDat),
                new SqlParameter("@Gio", gioDat)));

            if (soLuong > 0)
            {
                MessageBox.Show(
                    $"Bàn [{maBan}] đã được đặt trong khung giờ này!\n" +
                    "Vui lòng chọn bàn khác hoặc giờ khác.",
                    "Trùng Lịch", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string maDat = "DB" + DateTime.Now.ToString("yyyyMMddHHmmss");

            ConnectSQL.RunQuery(@"
                INSERT INTO DatBan
                (MaDatBan,MaBan,TenKhach,SoDienThoai,NgayDat,GioDat,SoNguoi,GhiChu,TrangThai)
                VALUES (@MaDat,@MaBan,@Ten,@SDT,@Ngay,@Gio,@SoNguoi,@GhiChu,0)",
                new SqlParameter("@MaDat", maDat),
                new SqlParameter("@MaBan", maBan),
                new SqlParameter("@Ten", txtTenKhach.Text.Trim()),
                new SqlParameter("@SDT", txtSDT.Text.Trim()),
                new SqlParameter("@Ngay", ngayDat),
                new SqlParameter("@Gio", gioDat),
                new SqlParameter("@SoNguoi", (int)nudSoNguoi.Value),
                new SqlParameter("@GhiChu", txtGhiChu.Text.Trim()));

            MessageBox.Show(
                $"Đặt bàn thành công!\n" +
                $"Bàn: {maBan}\n" +
                $"Khách: {txtTenKhach.Text.Trim()}\n" +
                $"Ngày: {ngayDat:dd/MM/yyyy}  Giờ: {gioDat:hh\\:mm}",
                "Thành Công", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
            dtpLocNgay.Value = ngayDat;
            LoadDatBan();
        }

        // ================== KHÁCH ĐÃ ĐẾN ==================
        private void btnDaDen_Click(object sender, EventArgs e)
        {
            if (dtgvDatBan.CurrentRow == null)
            { MessageBox.Show("Chọn lịch đặt bàn!"); return; }

            string maDat = dtgvDatBan.CurrentRow
                .Cells["Mã Đặt"].Value.ToString();
            string tt = dtgvDatBan.CurrentRow
                .Cells["Trạng Thái"].Value.ToString();

            if (tt != "Chờ xác nhận")
            {
                MessageBox.Show("Chỉ cập nhật được lịch đang chờ!");
                return;
            }

            ConnectSQL.RunQuery(
                "UPDATE DatBan SET TrangThai=1 WHERE MaDatBan=@MaDat",
                new SqlParameter("@MaDat", maDat));

            MessageBox.Show("Đã cập nhật khách đến!");
            LoadDatBan();
        }

        // ================== HỦY ĐẶT BÀN ==================
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (dtgvDatBan.CurrentRow == null)
            { MessageBox.Show("Chọn lịch đặt bàn cần hủy!"); return; }

            string maDat = dtgvDatBan.CurrentRow
                .Cells["Mã Đặt"].Value.ToString();
            string tt = dtgvDatBan.CurrentRow
                .Cells["Trạng Thái"].Value.ToString();

            if (tt == "Đã hủy")
            { MessageBox.Show("Lịch này đã bị hủy rồi!"); return; }

            if (MessageBox.Show("Xác nhận hủy lịch đặt bàn này?",
                "Xác Nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes) return;

            ConnectSQL.RunQuery(
                "UPDATE DatBan SET TrangThai=2 WHERE MaDatBan=@MaDat",
                new SqlParameter("@MaDat", maDat));

            MessageBox.Show("Đã hủy lịch đặt bàn!");
            LoadDatBan();
        }

        // ================== LỌC THEO NGÀY ==================
        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDatBan();
        }

        // ================== LÀM MỚI ==================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            if (cboBan.Items.Count > 0)
                cboBan.SelectedIndex = 0;
            txtTenKhach.Clear();
            txtSDT.Clear();
            txtGhiChu.Clear();
            dtpNgay.Value = DateTime.Today;
            dtpGio.Value = DateTime.Today.AddHours(
                DateTime.Now.Hour + 1);
            nudSoNguoi.Value = 2;
            txtTenKhach.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}