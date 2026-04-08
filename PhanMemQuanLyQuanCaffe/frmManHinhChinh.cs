using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmManHinhChinh : Form
    {
        public frmManHinhChinh()
        {
            InitializeComponent();
        }

        string folderHinh = Application.StartupPath + @"\luuanh\";

        // ================= MENU HỆ THỐNG =================
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien();
            frm.Show();
        }

        private void thôngTinCáNhânToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.ShowDialog();
        }

        private void loạiĐồUốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoaiDoUong frm = new frmLoaiDoUong();
            frm.ShowDialog();
        }

        private void bànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBan frm = new frmBan();
            frm.ShowDialog();
        }

        private void đồUốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoUong frm = new frmDoUong();
            frm.ShowDialog();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        // ================= FORM LOAD =================
        private void frmManHinhChinh_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadDoUong_Main();
            dtgvDoUong.AllowUserToAddRows = false;
            FormatDtgvDoUong_CellOnly();
            ApplyTheme();

            // Hiển thị tên nhân viên đang đăng nhập
            string sqlTen = "SELECT TenNV FROM NhanVien WHERE MaNV = @ma";
            object tenNV = ConnectSQL.ExecuteScalar(sqlTen,
                new SqlParameter("@ma", frmDangNhap.MaNV));
            txtNhanVien.Text = (tenNV != null) ? tenNV.ToString() : frmDangNhap.MaNV;
            txtNhanVien.ReadOnly = true;
        }

        // ================= APPLY THEME =================
        private void ApplyTheme()
        {
            this.BackColor = Color.FromArgb(245, 235, 220);

            // Thanh menu
            menuStrip1.BackColor = Color.FromArgb(101, 67, 33);
            menuStrip1.ForeColor = Color.White;
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.ForeColor = Color.White;
                item.BackColor = Color.FromArgb(101, 67, 33);
            }

            // Các nút
            StyleButton(button2, Color.FromArgb(210, 105, 30));  // Thêm Mới
            StyleButton(btnkiem, Color.FromArgb(139, 90, 43));   // Tìm Kiếm
            StyleButton(button4, Color.FromArgb(180, 60, 60));   // Xóa Đồ Uống
            StyleButton(button5, Color.FromArgb(34, 139, 34));   // Thanh Toán
            StyleButton(button6, Color.FromArgb(30, 144, 255));  // In Hóa Đơn
            StyleButton(btnBanDaChon, Color.FromArgb(210, 105, 30));

            // Grid đồ uống
            StyleGrid(dtgvDoUong);

            // Grid hóa đơn
            StyleGrid(dtgvHoaDon);

            // ListView bàn
            lstBan.BackColor = Color.FromArgb(255, 248, 240);

            // Labels
            label1.ForeColor = Color.FromArgb(101, 67, 33);
            label1.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            label2.ForeColor = Color.FromArgb(101, 67, 33);
            label2.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            label4.ForeColor = Color.FromArgb(101, 67, 33);
            label4.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            label7.ForeColor = Color.FromArgb(101, 67, 33);
            label7.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            label8.ForeColor = Color.FromArgb(101, 67, 33);
            label8.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            label9.BackColor = Color.FromArgb(200, 60, 60);
            label9.ForeColor = Color.White;

            // Radio buttons
            foreach (RadioButton rb in new[] { rbAll, rbYes, rbNo })
            {
                rb.ForeColor = Color.FromArgb(101, 67, 33);
                rb.Font = new Font("Segoe UI", 9);
            }

            // TextBox tìm kiếm
            txtTimKiem.BackColor = Color.FromArgb(255, 248, 240);
            txtTimKiem.BorderStyle = BorderStyle.FixedSingle;

            this.Font = new Font("Segoe UI", 9);
        }

        // Helper: style nút
        private void StyleButton(Button btn, Color backColor)
        {
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        // Helper: style grid
        private void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.FromArgb(255, 248, 240);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(101, 67, 33);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            grid.EnableHeadersVisualStyles = false;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 220);
            grid.GridColor = Color.FromArgb(210, 180, 140);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 105, 30);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        // ================= LOAD BÀN =================
        private void LoadTable()
        {
            lstBan.View = View.LargeIcon;
            lstBan.LargeImageList = imageList1;

            string strSQL = "SELECT MaBan, LoaiBan, TrangThai FROM Ban";
            if (rbYes.Checked)
                strSQL += " WHERE TrangThai = 0";
            else if (rbNo.Checked)
                strSQL += " WHERE TrangThai = 1";

            DataTable dt = ConnectSQL.Load(strSQL);
            lstBan.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string maBan = dt.Rows[i]["MaBan"].ToString();
                string loaiBan = dt.Rows[i]["LoaiBan"].ToString();
                bool trangThai = Convert.ToBoolean(dt.Rows[i]["TrangThai"]);

                ListViewItem item = new ListViewItem("BAN " + maBan);

                // Chọn ảnh theo loại bàn + trạng thái
                // Index 0 = Thường trống, 1 = Thường có người
                // Index 2 = VIP trống,    3 = VIP có người
                if (loaiBan == "VIP")
                    item.ImageIndex = trangThai ? 3 : 2;
                else
                    item.ImageIndex = trangThai ? 1 : 0;

                lstBan.Items.Add(item);
            }
        }

        // ================= LOAD ĐỒ UỐNG =================
        private void LoadDoUong_Main()
        {
            string sql = "SELECT MaDU, TenDU, DonGia, HinhAnh FROM DoUong";
            DataTable dt = ConnectSQL.Load(sql);

            dtgvDoUong.AutoGenerateColumns = false;
            dtgvDoUong.Columns.Clear();
            dtgvDoUong.RowTemplate.Height = 90;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "colHinh";
            imgCol.HeaderText = "Hình Ảnh";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dtgvDoUong.Columns.Add(imgCol);

            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn()
            { Name = "MaDU", HeaderText = "Mã Đồ Uống", DataPropertyName = "MaDU" });
            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn()
            { Name = "TenDU", HeaderText = "Tên Đồ Uống", DataPropertyName = "TenDU" });
            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn()
            { Name = "DonGia", HeaderText = "Giá Tiền", DataPropertyName = "DonGia" });

            dtgvDoUong.DataSource = dt;
            LoadImageForGrid();
        }

        // ================= LOAD HÌNH =================
        private void LoadImageForGrid()
        {
            foreach (DataGridViewRow row in dtgvDoUong.Rows)
            {
                if (row.IsNewRow) continue;
                string tenHinh = row.DataBoundItem is DataRowView drv
                                 ? drv["HinhAnh"].ToString() : "";
                if (!string.IsNullOrEmpty(tenHinh))
                {
                    string path = folderHinh + tenHinh;
                    if (File.Exists(path))
                        row.Cells["colHinh"].Value = Image.FromFile(path);
                }
            }
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e) => LoadTable();
        private void rbNo_CheckedChanged(object sender, EventArgs e) => LoadTable();

        // ================= TÌM KIẾM =================
        private void btnkiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(key)) { LoadDoUong_Main(); return; }

            string sql = "SELECT MaDU, TenDU, DonGia, HinhAnh FROM DoUong WHERE TenDU LIKE @key";
            DataTable dt = ConnectSQL.Load(sql, new SqlParameter("@key", "%" + key + "%"));
            dtgvDoUong.DataSource = dt;
            LoadImageForGrid();
        }

        // ================= FORMAT GRID =================
        private void FormatDtgvDoUong_CellOnly()
        {
            dtgvDoUong.AllowUserToAddRows = false;
            dtgvDoUong.RowTemplate.Height = 100;
            dtgvDoUong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvDoUong.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvDoUong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtgvDoUong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvDoUong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dtgvDoUong.Columns["DonGia"] != null)
            {
                dtgvDoUong.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dtgvDoUong.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }

            dtgvDoUong.AllowUserToResizeRows = false;
            dtgvDoUong.AllowUserToResizeColumns = false;
        }

        // ================= CHỌN BÀN =================
        private void lstBan_Click(object sender, EventArgs e)
        {
            if (lstBan.SelectedItems.Count == 0) return;
            btnBanDaChon.Text = lstBan.SelectedItems[0].Text.Replace("BAN ", "");
            LoadDoUongDaGoi();
        }

        // ================= LOAD HÓA ĐƠN =================
        private void LoadDoUongDaGoi()
        {
            string sql = @"
                SELECT hd.MaBan, du.TenDU, ct.SoLuong, ct.DonGia, ct.ThanhTien
                FROM ChiTietHoaDon ct
                INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                INNER JOIN DoUong du ON ct.MaDU = du.MaDU
                WHERE hd.TrangThai = 0 AND hd.MaBan = @MaBan";

            DataTable dt = ConnectSQL.Load(sql, new SqlParameter("@MaBan", btnBanDaChon.Text));
            dtgvHoaDon.DataSource = dt;
            SetupGridChiTietHoaDon();

            long tong = 0;
            foreach (DataRow row in dt.Rows)
                tong += Convert.ToInt64(row["ThanhTien"]);
            label8.Text = tong.ToString("N0");
        }

        private void SetupGridChiTietHoaDon()
        {
            dtgvHoaDon.AllowUserToAddRows = false;
            dtgvHoaDon.ReadOnly = true;
            dtgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtgvHoaDon.Columns[0].HeaderText = "Mã Bàn";
            dtgvHoaDon.Columns[1].HeaderText = "Tên đồ uống";
            dtgvHoaDon.Columns[2].HeaderText = "Số lượng";
            dtgvHoaDon.Columns[3].HeaderText = "Đơn giá";
            dtgvHoaDon.Columns[4].HeaderText = "Thành tiền";

            dtgvHoaDon.Columns[3].DefaultCellStyle.Format = "N0";
            dtgvHoaDon.Columns[4].DefaultCellStyle.Format = "N0";
            dtgvHoaDon.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvHoaDon.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        // ================= THÊM MÓN =================
        private void button2_Click(object sender, EventArgs e)
        {
            if (lstBan.SelectedItems.Count == 0) { MessageBox.Show("Vui lòng chọn bàn!"); return; }
            if (dtgvDoUong.CurrentRow == null) { MessageBox.Show("Vui lòng chọn đồ uống!"); return; }

            string MaBan = lstBan.SelectedItems[0].Text.Replace("BAN ", "");
            string MaDU = dtgvDoUong.CurrentRow.Cells["MaDU"].Value.ToString();
            int DonGia = Convert.ToInt32(dtgvDoUong.CurrentRow.Cells["DonGia"].Value);
            int SoLuong = (int)nmsoluong.Value;
            int ThanhTien = DonGia * SoLuong;

            object result = ConnectSQL.ExecuteScalar(
                "SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan AND TrangThai=0",
                new SqlParameter("@MaBan", MaBan));

            string MaHD;
            if (result == null)
            {
                MaHD = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                ConnectSQL.RunQuery(
                    @"INSERT INTO HoaDon (MaHD,NgayLap,MaNV,MaKH,MaBan,TongTien,TrangThai)
                      VALUES (@MaHD,GETDATE(),@MaNV,NULL,@MaBan,0,0)",
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaNV", frmDangNhap.MaNV),
                    new SqlParameter("@MaBan", MaBan));
            }
            else MaHD = result.ToString();

            object sl = ConnectSQL.ExecuteScalar(
                "SELECT SoLuong FROM ChiTietHoaDon WHERE MaHD=@MaHD AND MaDU=@MaDU",
                new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU));

            if (sl != null)
                ConnectSQL.RunQuery(
                    @"UPDATE ChiTietHoaDon SET SoLuong=SoLuong+@SoLuong,
                      ThanhTien=(SoLuong+@SoLuong)*DonGia WHERE MaHD=@MaHD AND MaDU=@MaDU",
                    new SqlParameter("@SoLuong", SoLuong),
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaDU", MaDU));
            else
                ConnectSQL.RunQuery(
                    @"INSERT INTO ChiTietHoaDon (MaHD,MaDU,SoLuong,DonGia,ThanhTien)
                      VALUES (@MaHD,@MaDU,@SoLuong,@DonGia,@ThanhTien)",
                    new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU),
                    new SqlParameter("@SoLuong", SoLuong), new SqlParameter("@DonGia", DonGia),
                    new SqlParameter("@ThanhTien", ThanhTien));

            ConnectSQL.RunQuery("UPDATE Ban SET TrangThai=1 WHERE MaBan=@MaBan",
                new SqlParameter("@MaBan", MaBan));

            LoadDoUongDaGoi();
            LoadTable();
        }

        // ================= XÓA MÓN =================
        private void button4_Click(object sender, EventArgs e)
        {
            if (dtgvHoaDon.CurrentRow == null) { MessageBox.Show("Vui lòng chọn đồ uống cần xóa!"); return; }

            string MaBan = btnBanDaChon.Text;
            string TenDU = dtgvHoaDon.CurrentRow.Cells[1].Value.ToString();
            int SoLuong = Convert.ToInt32(dtgvHoaDon.CurrentRow.Cells[2].Value);

            object MaHD = ConnectSQL.ExecuteScalar(
                "SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan AND TrangThai=0",
                new SqlParameter("@MaBan", MaBan));
            if (MaHD == null) return;

            object MaDU = ConnectSQL.ExecuteScalar(
                "SELECT MaDU FROM DoUong WHERE TenDU=@TenDU",
                new SqlParameter("@TenDU", TenDU));
            if (MaDU == null) return;

            if (SoLuong > 1)
                ConnectSQL.RunQuery(
                    @"UPDATE ChiTietHoaDon SET SoLuong=SoLuong-1,
                      ThanhTien=(SoLuong-1)*DonGia WHERE MaHD=@MaHD AND MaDU=@MaDU",
                    new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU));
            else
                ConnectSQL.RunQuery(
                    "DELETE FROM ChiTietHoaDon WHERE MaHD=@MaHD AND MaDU=@MaDU",
                    new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU));

            int count = Convert.ToInt32(ConnectSQL.ExecuteScalar(
                "SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaHD=@MaHD",
                new SqlParameter("@MaHD", MaHD)));

            if (count == 0)
            {
                ConnectSQL.RunQuery("DELETE FROM HoaDon WHERE MaHD=@MaHD",
                    new SqlParameter("@MaHD", MaHD));
                ConnectSQL.RunQuery("UPDATE Ban SET TrangThai=0 WHERE MaBan=@MaBan",
                    new SqlParameter("@MaBan", MaBan));
            }

            LoadDoUongDaGoi();
            LoadTable();
        }

        // ================= THANH TOÁN =================
        private void button5_Click(object sender, EventArgs e)
        {
            if (lstBan.SelectedItems.Count == 0) { MessageBox.Show("Vui lòng chọn bàn cần thanh toán!"); return; }

            string MaBan = btnBanDaChon.Text;

            object MaHD = ConnectSQL.ExecuteScalar(
                "SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan AND TrangThai=0",
                new SqlParameter("@MaBan", MaBan));

            if (MaHD == null) { MessageBox.Show("Bàn này chưa có hóa đơn!"); return; }

            object tongObj = ConnectSQL.ExecuteScalar(
                "SELECT SUM(ThanhTien) FROM ChiTietHoaDon WHERE MaHD=@MaHD",
                new SqlParameter("@MaHD", MaHD));
            long tongTien = tongObj != null ? Convert.ToInt64(tongObj) : 0;

            if (MessageBox.Show(
                $"Xác nhận thanh toán bàn {MaBan}?\nTổng tiền: {tongTien:N0} VNĐ",
                "Xác Nhận Thanh Toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            ConnectSQL.RunQuery(
                "UPDATE HoaDon SET TrangThai=1, TongTien=@TongTien WHERE MaHD=@MaHD",
                new SqlParameter("@TongTien", tongTien), new SqlParameter("@MaHD", MaHD));
            ConnectSQL.RunQuery("UPDATE Ban SET TrangThai=0 WHERE MaBan=@MaBan",
                new SqlParameter("@MaBan", MaBan));

            MessageBox.Show("Thanh toán thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            label8.Text = "0";
            btnBanDaChon.Text = "Chưa Chọn Bàn";
            dtgvHoaDon.DataSource = null;
            LoadTable();
        }

        // ================= IN HÓA ĐƠN =================
        private void button6_Click(object sender, EventArgs e)
        {
            if (lstBan.SelectedItems.Count == 0) { MessageBox.Show("Vui lòng chọn bàn!"); return; }
            if (dtgvHoaDon.Rows.Count == 0) { MessageBox.Show("Bàn này chưa có đồ uống!"); return; }

            string MaBan = btnBanDaChon.Text;
            string hoaDon = $"===== HÓA ĐƠN BÀN {MaBan} =====\n";
            hoaDon += $"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}\n";
            hoaDon += $"Nhân viên: {txtNhanVien.Text}\n";
            hoaDon += "--------------------------------\n";

            long tong = 0;
            foreach (DataGridViewRow row in dtgvHoaDon.Rows)
            {
                if (row.IsNewRow) continue;
                string ten = row.Cells[1].Value?.ToString();
                int sl = Convert.ToInt32(row.Cells[2].Value);
                long tt = Convert.ToInt64(row.Cells[4].Value);
                hoaDon += $"{ten} x{sl} = {tt:N0} đ\n";
                tong += tt;
            }

            hoaDon += "--------------------------------\n";
            hoaDon += $"TỔNG TIỀN: {tong:N0} VNĐ\n";
            hoaDon += "================================";

            MessageBox.Show(hoaDon, "In Hóa Đơn");
        }

        // ================= ĐĂNG XUẤT =================
        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác Nhận Đăng Xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmDangNhap.MaNV = "";
                frmDangNhap.MatKhau = "";
                frmDangNhap.Quyen = "";

                new frmDangNhap().Show();
                this.Close();
            }
        }

        // ================= MENU THỐNG KÊ =================
        private void thốngKêTheoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKeNgay frm = new frmThongKeNgay();
            frm.ShowDialog();
        }

        private void thốngKêDoanhThuTheoTuầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKeNhanVien frm = new frmThongKeNhanVien();
            frm.ShowDialog();
        }

        private void thốngKêBánHàngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmThongKeBanHang frm = new frmThongKeBanHang();
            frm.ShowDialog();
        }

        private void xemLịchSửHóaĐơnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmLichSuHoaDon frm = new frmLichSuHoaDon();
            frm.ShowDialog();
        }
        // test git account
        // ================= MENU THEO NGÀY =================
        private void menuTheoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenuTheoNgay frm = new frmMenuTheoNgay();
            frm.ShowDialog();
        }

        private void dtgvDoUong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}