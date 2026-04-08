using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmDoUong : Form
    {
        string folderHinh = Application.StartupPath + @"\luuanh\";
        string tenHinh = "";

        public frmDoUong()
        {
            InitializeComponent();
            this.Load += frmDoUong_Load;
        }

        // ================= FORM LOAD =================
        private void frmDoUong_Load(object sender, EventArgs e)
        {
            dtgvData.AllowUserToAddRows = false;
            dtgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!Directory.Exists(folderHinh))
                Directory.CreateDirectory(folderHinh);

            LoadLoai();
            LoadData();
        }

        // ================= LOAD DATA =================
        private void LoadData()
        {
            string sql = "SELECT MaDU, TenDU, MaLoai, DonGia, HinhAnh FROM DoUong";
            dtgvData.DataSource = ConnectSQL.Load(sql);

            if (dtgvData.Rows.Count > 0)
                ShowRow(0);
        }

        // ================= LOAD LOAI =================
        private void LoadLoai()
        {
            string sql = "SELECT MaLoai, TenLoai FROM LoaiDoUong";
            DataTable dt = ConnectSQL.Load(sql);

            cboMaLoai.DataSource = dt;
            cboMaLoai.DisplayMember = "TenLoai";
            cboMaLoai.ValueMember = "MaLoai";
            cboMaLoai.SelectedIndex = -1;
        }

        // ================= SHOW ROW =================
        private void ShowRow(int index)
        {
            if (index < 0 || index >= dtgvData.Rows.Count) return;

            DataGridViewRow r = dtgvData.Rows[index];
            if (r.IsNewRow) return;

            txtMaDU.Text = r.Cells["MaDU"].Value?.ToString() ?? "";
            txtTenDU.Text = r.Cells["TenDU"].Value?.ToString() ?? "";
            txtDonGia.Text = r.Cells["DonGia"].Value?.ToString() ?? "";

            if (r.Cells["MaLoai"].Value != null)
                cboMaLoai.SelectedValue = r.Cells["MaLoai"].Value;

            picHinhAnh.Image = null;
            tenHinh = r.Cells["HinhAnh"].Value?.ToString() ?? "";

            if (!string.IsNullOrEmpty(tenHinh))
            {
                string path = folderHinh + tenHinh;
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        picHinhAnh.Image = Image.FromStream(fs);
                    }
                }
            }
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        // ================= CHỌN HÌNH =================
        private void picHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Image|*.jpg;*.png";

            if (f.ShowDialog() == DialogResult.OK)
            {
                tenHinh = Path.GetFileName(f.FileName);
                string dest = folderHinh + tenHinh;

                // Copy ảnh trước
                File.Copy(f.FileName, dest, true);

                // Giải phóng ảnh cũ nếu có
                if (picHinhAnh.Image != null)
                {
                    picHinhAnh.Image.Dispose();
                    picHinhAnh.Image = null;
                }

                // Load ảnh KHÔNG LOCK FILE
                using (FileStream fs = new FileStream(dest, FileMode.Open, FileAccess.Read))
                {
                    picHinhAnh.Image = Image.FromStream(fs);
                }
            }
        }


        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO DoUong
                           (MaDU, TenDU, MaLoai, DonGia, HinhAnh)
                           VALUES (@madu,@tendu,@maloai,@dongia,@hinhanh)";

            int kq = ConnectSQL.RunQuery(sql,
                new SqlParameter("@madu", txtMaDU.Text.Trim()),
                new SqlParameter("@tendu", txtTenDU.Text.Trim()),
                new SqlParameter("@maloai", cboMaLoai.SelectedValue),
                new SqlParameter("@dongia", txtDonGia.Text.Trim()),
                new SqlParameter("@hinhanh", tenHinh)
            );

            if (kq > 0)
            {
                MessageBox.Show("Thêm đồ uống thành công!");
                LoadData();
            }
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE DoUong SET
                           TenDU=@tendu,
                           MaLoai=@maloai,
                           DonGia=@dongia,
                           HinhAnh=@hinhanh
                           WHERE MaDU=@madu";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@madu", txtMaDU.Text.Trim()),
                new SqlParameter("@tendu", txtTenDU.Text.Trim()),
                new SqlParameter("@maloai", cboMaLoai.SelectedValue),
                new SqlParameter("@dongia", txtDonGia.Text.Trim()),
                new SqlParameter("@hinhanh", tenHinh)
            );

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa đồ uống này?",
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string sql = "DELETE FROM DoUong WHERE MaDU=@madu";
            ConnectSQL.RunQuery(sql,
                new SqlParameter("@madu", txtMaDU.Text.Trim()));

            LoadData();
        }

        // ================= XÓA TRẮNG =================
        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            txtMaDU.Clear();
            txtTenDU.Clear();
            txtDonGia.Clear();
            cboMaLoai.SelectedIndex = -1;
            picHinhAnh.Image = null;
            tenHinh = "";
            dtgvData.ClearSelection();
            txtMaDU.Focus();
        }

        // ================= TÌM KIẾM =================
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string key = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                LoadData();
                return;
            }

            string sql = @"SELECT * FROM DoUong
                   WHERE MaDU LIKE @key
                      OR TenDU LIKE @key";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@key", "%" + key + "%"));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy đồ uống!");
                LoadData();
                return;
            }

            dtgvData.DataSource = dt;
        }


        private void btnXoaHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (picHinhAnh.Image != null)
            {
                picHinhAnh.Image.Dispose();
                picHinhAnh.Image = null;
            }
            tenHinh = "";
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quay về màn hình chính?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new frmManHinhChinh().Show();
                this.Close();
            }
        }
    }
}
