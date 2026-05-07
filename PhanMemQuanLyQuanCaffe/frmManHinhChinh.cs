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

        // ================= FORM LOAD =================
        private void frmManHinhChinh_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadDoUong_Main();
            PhanQuyen();
            CaiThienGiaoDien();
        }

        // ================= PHÂN QUYỀN =================
        private void PhanQuyen()
        {
            bool laAdmin = frmDangNhap.Quyen?.Trim()
                .Equals("Admin", StringComparison.OrdinalIgnoreCase) == true;

            thốngKêTheoNgàyToolStripMenuItem.Visible = laAdmin;
            thốngKêBánHàngToolStripMenuItem.Visible = laAdmin;
            thốngKêDoanhThuTheoTuầnToolStripMenuItem.Visible = laAdmin;
            xemLịchSửHóaĐơnToolStripMenuItem.Visible = laAdmin;
        }

        // ================= CẢI THIỆN GIAO DIỆN =================
        private void CaiThienGiaoDien()
        {
            this.BackColor = Color.FromArgb(236, 240, 244);

            // Menu
            menuStrip1.BackColor = Color.FromArgb(26, 35, 50);
            menuStrip1.ForeColor = Color.White;
            menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.ForeColor = Color.FromArgb(189, 195, 199);
                item.BackColor = Color.FromArgb(26, 35, 50);
                item.Padding = new Padding(10, 0, 10, 0);
            }

            // Nút bàn đang chọn
            btnBanDaChon.BackColor = Color.FromArgb(52, 152, 219);
            btnBanDaChon.ForeColor = Color.White;
            btnBanDaChon.FlatStyle = FlatStyle.Flat;
            btnBanDaChon.FlatAppearance.BorderSize = 0;
            btnBanDaChon.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);

            // Nút Thêm Món
            button2.BackColor = Color.FromArgb(39, 174, 96);
            button2.ForeColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            button2.Text = "Thêm Món";

            // Nút Xóa Món
            button4.BackColor = Color.FromArgb(231, 76, 60);
            button4.ForeColor = Color.White;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            button4.Text = "Xóa Món";

            // Nút Thanh Toán
           

            // Nút In Hóa Đơn
            button6.BackColor = Color.FromArgb(142, 68, 173);
            button6.ForeColor = Color.White;
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;
            button6.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            button6.Text = "In Hóa Đơn";
            button6.Size = new Size(130, 36);

            // Nút Tìm Kiếm
            btnkiem.BackColor = Color.FromArgb(52, 152, 219);
            btnkiem.ForeColor = Color.White;
            btnkiem.FlatStyle = FlatStyle.Flat;
            btnkiem.FlatAppearance.BorderSize = 0;
            btnkiem.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            btnkiem.Text = "Tìm Kiếm";

            // Labels
            label7.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(44, 62, 80);

            label8.Text = "0";
            label8.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(192, 57, 43);

            label9.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(44, 62, 80);
            label9.Text = "VNĐ";
            label9.BackColor = Color.Transparent;

            // Grid Đồ Uống
            dtgvDoUong.BackgroundColor = Color.White;
            dtgvDoUong.BorderStyle = BorderStyle.None;
            dtgvDoUong.GridColor = Color.FromArgb(220, 220, 220);
            dtgvDoUong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 50);
            dtgvDoUong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvDoUong.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            dtgvDoUong.EnableHeadersVisualStyles = false;
            dtgvDoUong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dtgvDoUong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(174, 214, 241);
            dtgvDoUong.DefaultCellStyle.SelectionForeColor = Color.FromArgb(26, 35, 50);

            // Grid Hóa Đơn
            dtgvHoaDon.BackgroundColor = Color.White;
            dtgvHoaDon.BorderStyle = BorderStyle.None;
            dtgvHoaDon.GridColor = Color.FromArgb(220, 220, 220);
            dtgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 35, 50);
            dtgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);
            dtgvHoaDon.EnableHeadersVisualStyles = false;
            dtgvHoaDon.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 252);
            dtgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(174, 214, 241);
            dtgvHoaDon.DefaultCellStyle.SelectionForeColor = Color.FromArgb(26, 35, 50);

            // ListView Bàn
            lstBan.BackColor = Color.FromArgb(245, 246, 250);
            lstBan.ForeColor = Color.FromArgb(44, 62, 80);
            lstBan.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            lstBan.BorderStyle = BorderStyle.None;
        }

        // ================= LOAD BÀN =================
        private void LoadTable()
        {
            lstBan.View = View.LargeIcon;
            lstBan.LargeImageList = imageList1;

            string strSQL = "SELECT * FROM Ban";
            if (rbYes.Checked) strSQL += " WHERE TrangThai = 0";
            else if (rbNo.Checked) strSQL += " WHERE TrangThai = 1";

            DataTable dt = ConnectSQL.Load(strSQL);
            lstBan.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string maBan = dt.Rows[i]["MaBan"].ToString();

                int trangThai = 0;
                if (dt.Rows[i]["TrangThai"] != DBNull.Value)
                    int.TryParse(dt.Rows[i]["TrangThai"].ToString(), out trangThai);

                int loaiBan = 0;
                if (dt.Rows[i]["LoaiBan"] != DBNull.Value)
                    int.TryParse(dt.Rows[i]["LoaiBan"].ToString(), out loaiBan);

                ListViewItem item = new ListViewItem("BAN " + maBan);

                if (loaiBan == 0)
                    item.ImageIndex = trangThai == 0 ? 0 : 1;
                else
                    item.ImageIndex = trangThai == 0 ? 2 : 3;

                lstBan.Items.Add(item);
            }
        }

        // ================= LOAD ĐỒ UỐNG =================
        private void LoadDoUong_Main()
        {
            string sql = @"SELECT du.MaDU, du.TenDU, du.DonGia, du.HinhAnh
                           FROM DoUong du
                           INNER JOIN MenuTheoNgay m ON du.MaDU = m.MaDU
                           WHERE m.Ngay = CAST(GETDATE() AS DATE)";
            DataTable dt = ConnectSQL.Load(sql);

            dtgvDoUong.AutoGenerateColumns = false;
            dtgvDoUong.Columns.Clear();
            dtgvDoUong.RowTemplate.Height = 90;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "colHinh";
            imgCol.HeaderText = "Hình Ảnh";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dtgvDoUong.Columns.Add(imgCol);

            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaDU", HeaderText = "Mã ĐU", DataPropertyName = "MaDU" });
            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenDU", HeaderText = "Tên Đồ Uống", DataPropertyName = "TenDU" });
            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Giá Tiền", DataPropertyName = "DonGia" });

            dtgvDoUong.DataSource = dt;
            dtgvDoUong.AllowUserToAddRows = false;
            LoadImageForGrid();
        }

        private void LoadImageForGrid()
        {
            foreach (DataGridViewRow row in dtgvDoUong.Rows)
            {
                if (row.IsNewRow) continue;
                string tenHinh = row.DataBoundItem is DataRowView drv ? drv["HinhAnh"].ToString() : "";
                if (!string.IsNullOrEmpty(tenHinh))
                {
                    string path = folderHinh + tenHinh;
                    if (File.Exists(path))
                        row.Cells["colHinh"].Value = Image.FromFile(path);
                }
            }
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
            string sql = @"SELECT hd.MaBan, du.TenDU, ct.SoLuong, ct.DonGia, ct.ThanhTien
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

            if (dtgvHoaDon.Columns.Count < 5) return;

            dtgvHoaDon.Columns[0].HeaderText = "Mã Bàn";
            dtgvHoaDon.Columns[1].HeaderText = "Tên Đồ Uống";
            dtgvHoaDon.Columns[2].HeaderText = "SL";
            dtgvHoaDon.Columns[3].HeaderText = "Đơn Giá";
            dtgvHoaDon.Columns[4].HeaderText = "Thành Tiền";

            dtgvHoaDon.Columns[3].DefaultCellStyle.Format = "N0";
            dtgvHoaDon.Columns[4].DefaultCellStyle.Format = "N0";

            dtgvHoaDon.Columns[0].FillWeight = 55;
            dtgvHoaDon.Columns[1].FillWeight = 150;
            dtgvHoaDon.Columns[2].FillWeight = 45;
            dtgvHoaDon.Columns[3].FillWeight = 80;
            dtgvHoaDon.Columns[4].FillWeight = 80;

            dtgvHoaDon.ColumnHeadersHeight = 32;
            dtgvHoaDon.RowTemplate.Height = 28;
            dtgvHoaDon.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

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
                    "INSERT INTO HoaDon(MaHD,NgayLap,MaNV,MaKH,MaBan,TongTien,TrangThai) VALUES(@MaHD,GETDATE(),@MaNV,NULL,@MaBan,0,0)",
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
                    "UPDATE ChiTietHoaDon SET SoLuong=SoLuong+@SL, ThanhTien=(SoLuong+@SL)*DonGia WHERE MaHD=@MaHD AND MaDU=@MaDU",
                    new SqlParameter("@SL", SoLuong), new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU));
            else
                ConnectSQL.RunQuery(
                    "INSERT INTO ChiTietHoaDon(MaHD,MaDU,SoLuong,DonGia,ThanhTien) VALUES(@MaHD,@MaDU,@SL,@DG,@TT)",
                    new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU),
                    new SqlParameter("@SL", SoLuong), new SqlParameter("@DG", DonGia), new SqlParameter("@TT", ThanhTien));

            ConnectSQL.RunQuery("UPDATE Ban SET TrangThai=1 WHERE MaBan=@MaBan", new SqlParameter("@MaBan", MaBan));
            LoadDoUongDaGoi();
            LoadTable();
        }

        // ================= XÓA MÓN =================
        private void button4_Click(object sender, EventArgs e)
        {
            if (dtgvHoaDon.CurrentRow == null) { MessageBox.Show("Vui lòng chọn món cần xóa!"); return; }

            string MaBan = btnBanDaChon.Text;
            string TenDU = dtgvHoaDon.CurrentRow.Cells[1].Value.ToString();
            int SoLuong = Convert.ToInt32(dtgvHoaDon.CurrentRow.Cells[2].Value);

            object MaHD = ConnectSQL.ExecuteScalar("SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan AND TrangThai=0", new SqlParameter("@MaBan", MaBan));
            if (MaHD == null) return;

            object MaDU = ConnectSQL.ExecuteScalar("SELECT MaDU FROM DoUong WHERE TenDU=@TenDU", new SqlParameter("@TenDU", TenDU));
            if (MaDU == null) return;

            if (SoLuong > 1)
                ConnectSQL.RunQuery("UPDATE ChiTietHoaDon SET SoLuong=SoLuong-1, ThanhTien=(SoLuong-1)*DonGia WHERE MaHD=@MaHD AND MaDU=@MaDU",
                    new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU));
            else
                ConnectSQL.RunQuery("DELETE FROM ChiTietHoaDon WHERE MaHD=@MaHD AND MaDU=@MaDU",
                    new SqlParameter("@MaHD", MaHD), new SqlParameter("@MaDU", MaDU));

            int count = Convert.ToInt32(ConnectSQL.ExecuteScalar("SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaHD=@MaHD", new SqlParameter("@MaHD", MaHD)));
            if (count == 0)
            {
                ConnectSQL.RunQuery("DELETE FROM HoaDon WHERE MaHD=@MaHD", new SqlParameter("@MaHD", MaHD));
                ConnectSQL.RunQuery("UPDATE Ban SET TrangThai=0 WHERE MaBan=@MaBan", new SqlParameter("@MaBan", MaBan));
            }

            LoadDoUongDaGoi();
            LoadTable();
        }

        // ================= THANH TOÁN =================
        private void button5_Click(object sender, EventArgs e)// Sự kiện click nút thanh toán
        {
            if (lstBan.SelectedItems.Count == 0)// Kiểm tra xem đã chọn bàn chưa
            {
                MessageBox.Show("Vui lòng chọn bàn cần thanh toán!");
                return;
            }

            string MaBan = btnBanDaChon.Text;// Lấy mã bàn từ nút hiển thị bàn đã chọn

            object MaHD = ConnectSQL.ExecuteScalar(// Truy vấn để lấy mã hóa đơn đang mở cho bàn này
                "SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan AND TrangThai=0",// Câu SQL tìm hóa đơn chưa thanh toán của bàn
                new SqlParameter("@MaBan", MaBan));// Tham số truyền vào câu SQL để tránh lỗi SQL Injection

            if (MaHD == null)// Nếu không tìm thấy hóa đơn nào đang mở cho bàn này
            {
                MessageBox.Show("Bàn này chưa có hóa đơn!");
                return;
            }

            object tongObj = ConnectSQL.ExecuteScalar(// Truy vấn để tính tổng tiền của hóa đơn
                "SELECT SUM(ThanhTien) FROM ChiTietHoaDon WHERE MaHD=@MaHD",// Câu SQL tính tổng tiền từ bảng chi tiết hóa đơn
                new SqlParameter("@MaHD", MaHD));// Tham số truyền vào câu SQL để tránh lỗi SQL Injection

            long tongTien = tongObj != null ? Convert.ToInt64(tongObj) : 0;// Chuyển kết quả tổng tiền sang kiểu long, nếu null thì gán 0

            // ====== TÌM KIẾM KHÁCH HÀNG ======
            string maKH = null;
            string tenKH = "Khách lẻ";

            DialogResult timKH = MessageBox.Show(
                $"Thanh toán bàn {MaBan}\nTổng tiền: {tongTien:N0} VNĐ\n\nBạn có muốn gán khách hàng vào hóa đơn không?",
                "Thanh Toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (timKH == DialogResult.Yes)
            {
                // Hiện form tìm kiếm khách hàng
                using (Form frmTimKH = new Form())
                {
                    frmTimKH.Text = "Tìm Khách Hàng";
                    frmTimKH.Size = new System.Drawing.Size(500, 350);
                    frmTimKH.StartPosition = FormStartPosition.CenterParent;
                    frmTimKH.FormBorderStyle = FormBorderStyle.FixedDialog;
                    frmTimKH.MaximizeBox = false;

                    Label lblHD = new Label()
                    {
                        Text = "Tìm theo tên hoặc SĐT:",
                        Location = new System.Drawing.Point(15, 15),
                        AutoSize = true
                    };

                    TextBox txtSearch = new TextBox()
                    {
                        Location = new System.Drawing.Point(15, 35),
                        Width = 300
                    };

                    Button btnSearch = new Button()
                    {
                        Text = "Tìm",
                        Location = new System.Drawing.Point(325, 33),
                        Width = 80
                    };

                    DataGridView grid = new DataGridView()
                    {
                        Location = new System.Drawing.Point(15, 70),
                        Size = new System.Drawing.Size(455, 180),
                        ReadOnly = true,
                        AllowUserToAddRows = false,
                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    };

                    Button btnChon = new Button()
                    {
                        Text = "Chọn",
                        Location = new System.Drawing.Point(310, 265),
                        Width = 80,
                        DialogResult = DialogResult.OK
                    };

                    Button btnBo = new Button()
                    {
                        Text = "Bỏ qua",
                        Location = new System.Drawing.Point(395, 265),
                        Width = 80,
                        DialogResult = DialogResult.Cancel
                    };

                    // Load tất cả khách hàng
                    Action loadKH = () =>
                    {
                        string key = txtSearch.Text.Trim();
                        string sql = string.IsNullOrEmpty(key)
                            ? "SELECT MaKH, TenKH, SDT, DiaChi FROM KhachHang"
                            : "SELECT MaKH, TenKH, SDT, DiaChi FROM KhachHang WHERE TenKH LIKE @key OR SDT LIKE @key";

                        DataTable dt = string.IsNullOrEmpty(key)
                            ? ConnectSQL.Load(sql)
                            : ConnectSQL.Load(sql, new SqlParameter("@key", "%" + key + "%"));

                        grid.DataSource = dt;
                    };

                    btnSearch.Click += (s, ev) => loadKH();
                    txtSearch.KeyDown += (s, ev) => { if (ev.KeyCode == Keys.Enter) loadKH(); };

                    frmTimKH.Controls.AddRange(new Control[] { lblHD, txtSearch, btnSearch, grid, btnChon, btnBo });
                    frmTimKH.AcceptButton = btnChon;

                    loadKH();

                    if (frmTimKH.ShowDialog() == DialogResult.OK && grid.CurrentRow != null)
                    {
                        maKH = grid.CurrentRow.Cells["MaKH"].Value.ToString();
                        tenKH = grid.CurrentRow.Cells["TenKH"].Value.ToString();
                    }
                }
            }

            // ====== XÁC NHẬN THANH TOÁN ======
            DialogResult confirm = MessageBox.Show(
                $"Xác nhận thanh toán bàn {MaBan}?\n" +
                $"Khách hàng: {tenKH}\n" +
                $"Tổng tiền: {tongTien:N0} VNĐ",
                "Xác Nhận Thanh Toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            // Cập nhật hóa đơn
            if (maKH != null)
            {
                ConnectSQL.RunQuery(
                    "UPDATE HoaDon SET TrangThai=1, TongTien=@TT, MaKH=@MaKH WHERE MaHD=@MaHD",
                    new SqlParameter("@TT", tongTien),
                    new SqlParameter("@MaKH", int.Parse(maKH)),
                    new SqlParameter("@MaHD", MaHD));
            }
            else
            {
                ConnectSQL.RunQuery(
                    "UPDATE HoaDon SET TrangThai=1, TongTien=@TT WHERE MaHD=@MaHD",
                    new SqlParameter("@TT", tongTien),
                    new SqlParameter("@MaHD", MaHD));
            }

            ConnectSQL.RunQuery(
                "UPDATE Ban SET TrangThai=0 WHERE MaBan=@MaBan",
                new SqlParameter("@MaBan", MaBan));

            MessageBox.Show($"Thanh toán thành công!\nKhách hàng: {tenKH}\nTổng tiền: {tongTien:N0} VNĐ",
                "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            // Chọn nơi lưu file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files|*.pdf";
            sfd.FileName = $"HoaDon_Ban{MaBan}_{DateTime.Now:ddMMyyyyHHmmss}";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (var fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create))
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document(
                        iTextSharp.text.PageSize.A5, 20, 20, 20, 20);
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Font hỗ trợ tiếng Việt
                    string fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\\arial.ttf";
                    iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(
                        fontPath, iTextSharp.text.pdf.BaseFont.IDENTITY_H, true);

                    var fontTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                    var fontHeader = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
                    var fontNormal = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL);
                    var fontBold = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
                    var fontTotal = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD,
                        iTextSharp.text.BaseColor.RED);

                    // Tiêu đề
                    var title = new iTextSharp.text.Paragraph("QUÁN CAFFE", fontTitle);
                    title.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(title);

                    var sub = new iTextSharp.text.Paragraph("HÓA ĐƠN THANH TOÁN", fontHeader);
                    sub.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(sub);

                    doc.Add(new iTextSharp.text.Paragraph("--------------------------------", fontNormal)
                    { Alignment = iTextSharp.text.Element.ALIGN_CENTER });

                    doc.Add(new iTextSharp.text.Paragraph($"Bàn: {MaBan}", fontNormal));
                    doc.Add(new iTextSharp.text.Paragraph(
                        $"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fontNormal));
                    doc.Add(new iTextSharp.text.Paragraph(
                        $"Thu ngân: {frmDangNhap.MaNV}", fontNormal));
                    doc.Add(new iTextSharp.text.Paragraph(" "));

                    // Bảng chi tiết
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(4);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 40f, 15f, 20f, 25f });

                    // Header bảng
                    string[] headers = { "Tên đồ uống", "SL", "Đơn giá", "Thành tiền" };
                    foreach (string h in headers)
                    {
                        var cell = new iTextSharp.text.pdf.PdfPCell(
                            new iTextSharp.text.Phrase(h, fontHeader));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(26, 35, 50);
                        cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        cell.Padding = 5;
                        var fc = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD,
                            iTextSharp.text.BaseColor.WHITE);
                        cell.Phrase = new iTextSharp.text.Phrase(h, fc);
                        table.AddCell(cell);
                    }

                    // Dữ liệu
                    long tong = 0;
                    foreach (DataGridViewRow row in dtgvHoaDon.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string ten = row.Cells[1].Value?.ToString();
                        int sl = Convert.ToInt32(row.Cells[2].Value);
                        long dg = Convert.ToInt64(row.Cells[3].Value);
                        long tt = Convert.ToInt64(row.Cells[4].Value);
                        tong += tt;

                        table.AddCell(new iTextSharp.text.pdf.PdfPCell(
                            new iTextSharp.text.Phrase(ten, fontNormal))
                        { Padding = 4 });
                        table.AddCell(new iTextSharp.text.pdf.PdfPCell(
                            new iTextSharp.text.Phrase(sl.ToString(), fontNormal))
                        { HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, Padding = 4 });
                        table.AddCell(new iTextSharp.text.pdf.PdfPCell(
                            new iTextSharp.text.Phrase(dg.ToString("N0"), fontNormal))
                        { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, Padding = 4 });
                        table.AddCell(new iTextSharp.text.pdf.PdfPCell(
                            new iTextSharp.text.Phrase(tt.ToString("N0"), fontNormal))
                        { HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, Padding = 4 });
                    }

                    doc.Add(table);
                    doc.Add(new iTextSharp.text.Paragraph(" "));
                    doc.Add(new iTextSharp.text.Paragraph("--------------------------------", fontNormal)
                    { Alignment = iTextSharp.text.Element.ALIGN_CENTER });

                    // Tổng tiền
                    var tongPara = new iTextSharp.text.Paragraph(
                        $"TỔNG TIỀN: {tong:N0} VNĐ", fontTotal);
                    tongPara.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    doc.Add(tongPara);

                    doc.Add(new iTextSharp.text.Paragraph(" "));
                    var thanks = new iTextSharp.text.Paragraph("Cảm ơn quý khách!", fontBold);
                    thanks.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    doc.Add(thanks);

                    doc.Close();
                }

                // Mở file PDF luôn
                System.Diagnostics.Process.Start(sfd.FileName);
                MessageBox.Show("Xuất hóa đơn PDF thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất PDF:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= RADIO BUTTONS =================
        private void rbYes_CheckedChanged(object sender, EventArgs e) => LoadTable();
        private void rbNo_CheckedChanged(object sender, EventArgs e) => LoadTable();

        // ================= MENU EVENTS =================
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e) { new frmNhanVien().Show(); }
        private void thôngTinCáNhânToolStripMenuItem1_Click(object sender, EventArgs e) { new frmDoiMatKhau().ShowDialog(); }
        private void loạiĐồUốngToolStripMenuItem_Click(object sender, EventArgs e) { new frmLoaiDoUong().ShowDialog(); }
        private void đồUốngToolStripMenuItem_Click(object sender, EventArgs e) { new frmDoUong().ShowDialog(); }
        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e) { new frmKhachHang().ShowDialog(); }

        private void bànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmBan().ShowDialog();
            LoadTable();
        }

        private void menuTheoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMenuTheoNgay().ShowDialog();
            LoadDoUong_Main();
        }

        private void chuyểnBànToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmChuyenBan().ShowDialog();
            LoadTable();
        }

        private void gộpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new frmGopBan().ShowDialog();
            LoadTable();
        }

        private void lịchSửNgàyLàmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.Quyen != "Admin") { MessageBox.Show("Chỉ Admin mới được xem!"); return; }
            new frmLichSuNgayLam().ShowDialog();
        }

        private void thốngKêTheoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDangNhap.Quyen != "Admin") { MessageBox.Show("Chỉ Admin mới được xem thống kê!"); return; }
            new frmThongKeNgay().ShowDialog();
        }

        private void thốngKêDoanhThuTheoTuầnToolStripMenuItem_Click(object sender, EventArgs e) { new frmThongKeNhanVien().ShowDialog(); }
        private void thốngKêBánHàngToolStripMenuItem_Click_1(object sender, EventArgs e) { new frmThongKeBanHang().ShowDialog(); }
        private void xemLịchSửHóaĐơnToolStripMenuItem_Click_1(object sender, EventArgs e) { new frmLichSuHoaDon().ShowDialog(); }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác Nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            ConnectSQL.RunQuery(
                "UPDATE CaLamViec SET GioRa=GETDATE(), TrangThai=1 WHERE MaNV=@MaNV AND TrangThai=0",
                new SqlParameter("@MaNV", frmDangNhap.MaNV));

            frmDangNhap.MaNV = "";
            frmDangNhap.MatKhau = "";
            frmDangNhap.Quyen = "";
            new frmDangNhap().Show();
            this.Close();
        }

        // ================= EMPTY HANDLERS =================
        private void dtgvDoUong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void gộpToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void FormatDtgvDoUong_CellOnly() { }
        //thong ke theo thang
        private void thốngKêTheoThángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!frmDangNhap.Quyen.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ Admin mới được xem thống kê!");
                return;
            }
            new frmThongKeThang().ShowDialog();
        }
     
    }
}