using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmMenuTheoNgay : Form
    {
        string folderHinh = Application.StartupPath + @"\luuanh\";

        // ✅ Flag để tránh XemMenu chạy trước khi SetupGrid xong
        private bool isInitialized = false;

        public frmMenuTheoNgay()
        {
            InitializeComponent();
        }

        private void frmMenuTheoNgay_Load(object sender, EventArgs e)
        {
            // ✅ Setup grid TRƯỚC
            SetupGrid();

            // ✅ Load listbox
            LoadDoUongVaoList();

            // ✅ Gán ngày SAU khi đã setup xong (tránh ValueChanged kích hoạt sớm)
            isInitialized = true;
            dtpNgay.Value = DateTime.Today;
        }

        // ================== LOAD LISTBOX ==================
        private void LoadDoUongVaoList()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaDU, TenDU FROM DoUong");

            themlistbox.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                themlistbox.Items.Add(new ComboboxItem
                {
                    Text = row["TenDU"].ToString(),
                    Value = row["MaDU"].ToString()
                });
            }
        }

        // ================== SETUP GRID ==================
        private void SetupGrid()
        {
            dtgvMenu.Columns.Clear();
            dtgvMenu.RowTemplate.Height = 80;
            dtgvMenu.AllowUserToAddRows = false;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "colHinh";
            imgCol.HeaderText = "Hình Ảnh";
            imgCol.Width = 120;
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dtgvMenu.Columns.Add(imgCol);

            dtgvMenu.Columns.Add("colMaDU", "Mã");
            dtgvMenu.Columns.Add("colTenDU", "Tên Đồ Uống");
            dtgvMenu.Columns.Add("colGia", "Giá");
        }

        // ================== XEM MENU ==================
        private void XemMenu()
        {
            dtgvMenu.Rows.Clear();

            string sql = @"
                SELECT du.MaDU, du.TenDU, du.DonGia, du.HinhAnh
                FROM MenuTheoNgay m
                INNER JOIN DoUong du ON m.MaDU = du.MaDU
                WHERE m.Ngay = @Ngay";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@Ngay", dtpNgay.Value.Date));

            foreach (DataRow r in dt.Rows)
            {
                Image img = LoadImage(r["HinhAnh"]?.ToString());

                dtgvMenu.Rows.Add(
                    img,
                    r["MaDU"].ToString(),
                    r["TenDU"].ToString(),
                    Convert.ToInt32(r["DonGia"])
                );
            }
        }

        // ================== HÀM LOAD ẢNH ==================
        private Image LoadImage(string tenHinh)
        {
            if (string.IsNullOrEmpty(tenHinh)) return null;

            try
            {
                string path = Path.Combine(Application.StartupPath, "luuanh", tenHinh);

                if (File.Exists(path))
                {
                    // ✅ Dùng FileStream giống frmDoUong, KHÔNG dispose sớm
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        // ✅ Copy ra Bitmap mới để không phụ thuộc vào stream
                        Image tmp = Image.FromStream(fs);
                        Bitmap bmp = new Bitmap(tmp);
                        tmp.Dispose();
                        return bmp;
                    }
                }
            }
            catch { }

            return null;
        }

        // ================== DOUBLE CLICK THÊM MÓN ==================
        private void themlistbox_DoubleClick(object sender, EventArgs e)
        {
            if (themlistbox.SelectedItem == null) return;

            ComboboxItem item = (ComboboxItem)themlistbox.SelectedItem;
            string maDU = item.Value;

            // Kiểm tra đã có trong grid chưa
            foreach (DataGridViewRow row in dtgvMenu.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colMaDU"].Value?.ToString() == maDU)
                {
                    MessageBox.Show("Món này đã có trong menu!", "Thông báo");
                    return;
                }
            }

            string sql = "SELECT TenDU, DonGia, HinhAnh FROM DoUong WHERE MaDU = @MaDU";
            DataTable dt = ConnectSQL.Load(sql, new SqlParameter("@MaDU", maDU));

            if (dt.Rows.Count == 0) return;

            DataRow r = dt.Rows[0];
            Image img = LoadImage(r["HinhAnh"]?.ToString());

            dtgvMenu.Rows.Add(
                img,
                maDU,
                r["TenDU"].ToString(),
                Convert.ToInt32(r["DonGia"])
            );
        }

        // ================== LƯU ==================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DateTime ngay = dtpNgay.Value.Date;

            ConnectSQL.RunQuery("DELETE FROM MenuTheoNgay WHERE Ngay=@Ngay",
                new SqlParameter("@Ngay", ngay));

            int count = 0;

            foreach (DataGridViewRow row in dtgvMenu.Rows)
            {
                if (row.IsNewRow) continue;

                string maDU = row.Cells["colMaDU"].Value?.ToString();
                if (string.IsNullOrEmpty(maDU)) continue;

                ConnectSQL.RunQuery(
                    "INSERT INTO MenuTheoNgay (Ngay, MaDU) VALUES (@Ngay, @MaDU)",
                    new SqlParameter("@Ngay", ngay),
                    new SqlParameter("@MaDU", maDU));

                count++;
            }

            MessageBox.Show($"Đã lưu {count} món cho ngày {ngay:dd/MM/yyyy}!");
        }

        // ================== XEM ==================
        private void btnXem_Click(object sender, EventArgs e)
        {
            XemMenu();
        }

        // ✅ Chỉ chạy XemMenu sau khi đã khởi tạo xong
        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialized) return;
            XemMenu();
        }

        // ================== XOÁ ==================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DateTime ngay = dtpNgay.Value.Date;

            if (MessageBox.Show($"Xóa toàn bộ menu ngày {ngay:dd/MM/yyyy}?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            ConnectSQL.RunQuery("DELETE FROM MenuTheoNgay WHERE Ngay=@Ngay",
                new SqlParameter("@Ngay", ngay));

            XemMenu();
            MessageBox.Show("Đã xóa menu!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lichsu_Click(object sender, EventArgs e)
        {
            int thang = dtpNgay.Value.Month;
            int nam = dtpNgay.Value.Year;
            int soNgay = DateTime.DaysInMonth(nam, thang);

            // Lấy các ngày có menu trong tháng
            string sql = @"SELECT DISTINCT CAST(Ngay AS DATE) as Ngay 
                   FROM MenuTheoNgay 
                   WHERE MONTH(Ngay)=@Thang AND YEAR(Ngay)=@Nam";
            DataTable dtCoMenu = ConnectSQL.Load(sql,
                new SqlParameter("@Thang", thang),
                new SqlParameter("@Nam", nam));

            var ngayCoMenu = new System.Collections.Generic.HashSet<string>();
            foreach (DataRow row in dtCoMenu.Rows)
                ngayCoMenu.Add(Convert.ToDateTime(row["Ngay"]).ToString("dd/MM/yyyy"));

            // Hiển thị kết quả
            string ketQua = $"=== LỊCH SỬ MENU THÁNG {thang}/{nam} ===\n\n";
            for (int i = 1; i <= soNgay; i++)
            {
                DateTime ngay = new DateTime(nam, thang, i);
                string ngayStr = ngay.ToString("dd/MM/yyyy");

                if (ngayCoMenu.Contains(ngayStr))
                {
                    string sqlMon = @"SELECT du.TenDU FROM MenuTheoNgay m
                              INNER JOIN DoUong du ON m.MaDU = du.MaDU
                              WHERE m.Ngay = @Ngay";
                    DataTable dtMon = ConnectSQL.Load(sqlMon,
                        new SqlParameter("@Ngay", ngay.Date));

                    string dsMon = "";
                    foreach (DataRow r in dtMon.Rows)
                        dsMon += r["TenDU"].ToString() + ", ";
                    dsMon = dsMon.TrimEnd(',', ' ');

                    ketQua += $"✅ {ngayStr}: {dsMon}\n";
                }
                else
                {
                    ketQua += $"⬜ {ngayStr}: Chưa có menu\n";
                }
            }

            MessageBox.Show(ketQua, $"Lịch Sử Tháng {thang}/{nam}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // ================== CLASS ==================
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public override string ToString() => Text;
    }
}

