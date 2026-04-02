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

        // ================= LOAD BÀN =================
        private void LoadTable()
        {
            lstBan.View = View.LargeIcon;
            lstBan.LargeImageList = imageList1;

            string strSQL = "SELECT * FROM Ban";

            if (rbYes.Checked)
                strSQL += " WHERE TrangThai = 0";
            else if (rbNo.Checked)
                strSQL += " WHERE TrangThai = 1";

            DataTable dt = ConnectSQL.Load(strSQL);

            lstBan.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem("BAN " + dt.Rows[i]["MaBan"]);

                bool trangThai = Convert.ToBoolean(dt.Rows[i]["TrangThai"]);

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
            {
                Name = "MaDU",
                HeaderText = "Mã Đồ Uống",
                DataPropertyName = "MaDU"
            });

            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TenDU",
                HeaderText = "Tên Đồ Uống",
                DataPropertyName = "TenDU"
            });

            dtgvDoUong.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DonGia",
                HeaderText = "Giá Tiền",
                DataPropertyName = "DonGia"
            });

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
                                 ? drv["HinhAnh"].ToString()
                                 : "";

                if (!string.IsNullOrEmpty(tenHinh))
                {
                    string path = folderHinh + tenHinh;

                    if (File.Exists(path))
                    {
                        row.Cells["colHinh"].Value = Image.FromFile(path);
                    }
                }
            }
        }

        private void frmManHinhChinh_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadDoUong_Main();
            dtgvDoUong.AllowUserToAddRows = false;
            FormatDtgvDoUong_CellOnly();
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            LoadTable();
        }

        // ================= TÌM KIẾM =================
        private void btnkiem_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                LoadDoUong_Main();
                return;
            }

            string sql = @"SELECT MaDU, TenDU, DonGia, HinhAnh
                           FROM DoUong
                           WHERE TenDU LIKE @key";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@key", "%" + key + "%"));

            dtgvDoUong.DataSource = dt;

            LoadImageForGrid();
        }

        // ================= FORMAT GRID =================
        private void FormatDtgvDoUong_CellOnly()
        {
            dtgvDoUong.AllowUserToAddRows = false;

            dtgvDoUong.RowTemplate.Height = 100;

            dtgvDoUong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgvDoUong.ColumnHeadersDefaultCellStyle.WrapMode =
                DataGridViewTriState.True;

            dtgvDoUong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dtgvDoUong.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dtgvDoUong.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            if (dtgvDoUong.Columns["DonGia"] != null)
            {
                dtgvDoUong.Columns["DonGia"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dtgvDoUong.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }

            dtgvDoUong.AllowUserToResizeRows = false;
            dtgvDoUong.AllowUserToResizeColumns = false;
        }

        // ================= CHỌN BÀN =================
        private void lstBan_Click(object sender, EventArgs e)
        {
            btnBanDaChon.Text = lstBan.SelectedItems[0].Text.Replace("BAN ", "");

            LoadDoUongDaGoi();
        }

        // ================= LOAD HÓA ĐƠN =================
        private void LoadDoUongDaGoi()
        {
            string sql = @"
            SELECT 
                hd.MaBan,
                du.TenDU,
                ct.SoLuong,
                ct.DonGia,
                ct.ThanhTien
            FROM ChiTietHoaDon ct
            INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
            INNER JOIN DoUong du ON ct.MaDU = du.MaDU
            WHERE hd.TrangThai = 0
            AND hd.MaBan = @MaBan";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@MaBan", btnBanDaChon.Text));

            dtgvHoaDon.DataSource = dt;

            SetupGridChiTietHoaDon();
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
            dtgvHoaDon.Columns[1].DefaultCellStyle.WrapMode =
               DataGridViewTriState.True;

            dtgvHoaDon.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.AllCells;
        }

        // ================= THÊM MÓN =================
        private void button2_Click(object sender, EventArgs e)
        {
            if (lstBan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn!");
                return;
            }

            if (dtgvDoUong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đồ uống!");
                return;
            }

            string MaBan = lstBan.SelectedItems[0].Text.Replace("BAN ", "");
            string MaDU = dtgvDoUong.CurrentRow.Cells["MaDU"].Value.ToString();
            int DonGia = Convert.ToInt32(dtgvDoUong.CurrentRow.Cells["DonGia"].Value);
            int SoLuong = (int)nmsoluong.Value;

            int ThanhTien = DonGia * SoLuong;

            string sql = @"SELECT MaHD FROM HoaDon 
                           WHERE MaBan=@MaBan AND TrangThai=0";

            object result = ConnectSQL.ExecuteScalar(
                sql,
                new SqlParameter("@MaBan", MaBan)
            );

            string MaHD;

            if (result == null)
            {
                MaHD = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");

                string insertHD = @"INSERT INTO HoaDon
                        (MaHD,NgayLap,MaNV,MaKH,MaBan,TongTien,TrangThai)
                        VALUES
                        (@MaHD,GETDATE(),@MaNV,NULL,@MaBan,0,0)";

                ConnectSQL.RunQuery(
                    insertHD,
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaNV", frmDangNhap.MaNV),
                    new SqlParameter("@MaBan", MaBan)
                );
            }
            else
            {
                MaHD = result.ToString();
            }

            // kiểm tra món đã có chưa
            string check = @"SELECT SoLuong FROM ChiTietHoaDon 
                             WHERE MaHD=@MaHD AND MaDU=@MaDU";

            object sl = ConnectSQL.ExecuteScalar(
                check,
                new SqlParameter("@MaHD", MaHD),
                new SqlParameter("@MaDU", MaDU)
            );

            if (sl != null)
            {
                string update = @"UPDATE ChiTietHoaDon
                                  SET SoLuong = SoLuong + @SoLuong,
                                      ThanhTien = (SoLuong + @SoLuong) * DonGia
                                  WHERE MaHD=@MaHD AND MaDU=@MaDU";

                ConnectSQL.RunQuery(
                    update,
                    new SqlParameter("@SoLuong", SoLuong),
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaDU", MaDU)
                );
            }
            else
            {
                string insertCT = @"INSERT INTO ChiTietHoaDon
                        (MaHD,MaDU,SoLuong,DonGia,ThanhTien)
                        VALUES
                        (@MaHD,@MaDU,@SoLuong,@DonGia,@ThanhTien)";

                ConnectSQL.RunQuery(
                    insertCT,
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaDU", MaDU),
                    new SqlParameter("@SoLuong", SoLuong),
                    new SqlParameter("@DonGia", DonGia),
                    new SqlParameter("@ThanhTien", ThanhTien)
                );
            }
            // cập nhật bàn có người
            string updateBan = "UPDATE Ban SET TrangThai = 1 WHERE MaBan = @MaBan";

            ConnectSQL.RunQuery(
                updateBan,
                new SqlParameter("@MaBan", MaBan)
            );
            LoadDoUongDaGoi();
            LoadTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dtgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đồ uống cần xóa!");
                return;
            }

            string MaBan = btnBanDaChon.Text;
            string TenDU = dtgvHoaDon.CurrentRow.Cells[1].Value.ToString();
            int SoLuong = Convert.ToInt32(dtgvHoaDon.CurrentRow.Cells[2].Value);

            // lấy MaHD
            string sqlHD = "SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan AND TrangThai=0";

            object MaHD = ConnectSQL.ExecuteScalar(sqlHD,
                new SqlParameter("@MaBan", MaBan));

            if (MaHD == null) return;

            // lấy MaDU từ TenDU
            string sqlMaDU = "SELECT MaDU FROM DoUong WHERE TenDU=@TenDU";

            object MaDU = ConnectSQL.ExecuteScalar(sqlMaDU,
                new SqlParameter("@TenDU", TenDU));

            if (MaDU == null) return;

            if (SoLuong > 1)
            {
                string update = @"UPDATE ChiTietHoaDon
                          SET SoLuong = SoLuong - 1,
                              ThanhTien = (SoLuong - 1) * DonGia
                          WHERE MaHD=@MaHD AND MaDU=@MaDU";

                ConnectSQL.RunQuery(update,
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaDU", MaDU));
            }
            else
            {
                string delete = @"DELETE FROM ChiTietHoaDon
                          WHERE MaHD=@MaHD AND MaDU=@MaDU";

                ConnectSQL.RunQuery(delete,
                    new SqlParameter("@MaHD", MaHD),
                    new SqlParameter("@MaDU", MaDU));
            }

            // kiểm tra hóa đơn còn món không
            string check = @"SELECT COUNT(*) FROM ChiTietHoaDon WHERE MaHD=@MaHD";

            int count = Convert.ToInt32(ConnectSQL.ExecuteScalar(
                check,
                new SqlParameter("@MaHD", MaHD)
            ));

            if (count == 0)
            {
                // xóa luôn hóa đơn
                string deleteHD = "DELETE FROM HoaDon WHERE MaHD=@MaHD";

                ConnectSQL.RunQuery(deleteHD,
                    new SqlParameter("@MaHD", MaHD));

                // cập nhật bàn về trống
                string updateBan = "UPDATE Ban SET TrangThai = 0 WHERE MaBan=@MaBan";

                ConnectSQL.RunQuery(updateBan,
                    new SqlParameter("@MaBan", MaBan));
            }

            // load lại dữ liệu
            LoadDoUongDaGoi();
            LoadTable();
        }
    }
}