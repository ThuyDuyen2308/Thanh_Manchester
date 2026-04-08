using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            // Load danh sách quyền vào ComboBox
            cboQuyen.Items.Clear();
            cboQuyen.Items.Add("NhanVien");
            cboQuyen.Items.Add("Admin");
            cboQuyen.SelectedIndex = 0;

            LoadData();
        }

        // ================= SETUP GRID =================
        private void SetupDataGridView()
        {
            dtgvData.AllowUserToAddRows = false;
            dtgvData.ReadOnly = true;
            dtgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dtgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvData.EnableHeadersVisualStyles = false;
        }

        // ================= LOAD DATA =================
        private void LoadData()
        {
            string sql = "SELECT * FROM NhanVien";
            dtgvData.DataSource = ConnectSQL.Load(sql);
            SetupDataGridView();

            if (dtgvData.Rows.Count > 0)
                ShowRow(0);
        }

        // ================= HIỂN THỊ =================
        private void ShowRow(int index)
        {
            if (index < 0) return;
            DataGridViewRow r = dtgvData.Rows[index];

            txtMaNV.Text = r.Cells["MaNV"].Value?.ToString() ?? "";
            txtTenNV.Text = r.Cells["TenNV"].Value?.ToString() ?? "";
            txtMatKhau.Text = r.Cells["MatKhau"].Value?.ToString() ?? "";
            txtSDT.Text = r.Cells["SDT"].Value?.ToString() ?? "";
            txtDiaChi.Text = r.Cells["DiaChi"].Value?.ToString() ?? "";

            // Hiển thị quyền vào ComboBox
            string quyen = r.Cells["Quyen"].Value?.ToString() ?? "";
            cboQuyen.SelectedItem = cboQuyen.Items.Contains(quyen) ? quyen : "NhanVien";
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            // Chỉ Admin mới được thêm nhân viên
            if (frmDangNhap.Quyen != "Admin")
            {
                MessageBox.Show("Chỉ Admin mới được thêm nhân viên!",
                    "Từ Chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaNV.Text == "" || txtTenNV.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
                return;
            }

            if (!int.TryParse(txtMatKhau.Text, out int mk))
            {
                MessageBox.Show("Mật khẩu phải là số!");
                return;
            }

            string sql = @"INSERT INTO NhanVien (MaNV, TenNV, MatKhau, SDT, DiaChi, Quyen)
                           VALUES (@ma, @ten, @mk, @sdt, @dc, @quyen)";

            int kq = ConnectSQL.RunQuery(sql,
                new SqlParameter("@ma", txtMaNV.Text.Trim()),
                new SqlParameter("@ten", txtTenNV.Text.Trim()),
                new SqlParameter("@mk", mk),
                new SqlParameter("@sdt", txtSDT.Text.Trim()),
                new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                new SqlParameter("@quyen", cboQuyen.SelectedItem.ToString()));

            if (kq > 0)
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadData();
            }
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            // Chỉ Admin mới được sửa
            if (frmDangNhap.Quyen != "Admin")
            {
                MessageBox.Show("Chỉ Admin mới được sửa thông tin nhân viên!",
                    "Từ Chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMatKhau.Text, out int mk))
            {
                MessageBox.Show("Mật khẩu phải là số!");
                return;
            }

            string sql = @"UPDATE NhanVien 
                           SET TenNV=@ten, MatKhau=@mk, SDT=@sdt, DiaChi=@dc, Quyen=@quyen
                           WHERE MaNV=@ma";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@ten", txtTenNV.Text.Trim()),
                new SqlParameter("@mk", mk),
                new SqlParameter("@sdt", txtSDT.Text.Trim()),
                new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                new SqlParameter("@quyen", cboQuyen.SelectedItem.ToString()),
                new SqlParameter("@ma", txtMaNV.Text.Trim()));

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            // Chỉ Admin mới được xóa
            if (frmDangNhap.Quyen != "Admin")
            {
                MessageBox.Show("Chỉ Admin mới được xóa nhân viên!",
                    "Từ Chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xóa nhân viên này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            string sql = "DELETE FROM NhanVien WHERE MaNV=@ma";
            ConnectSQL.RunQuery(sql,
                new SqlParameter("@ma", txtMaNV.Text.Trim()));

            MessageBox.Show("Xóa thành công!");
            LoadData();
        }

        // ================= XÓA TRẮNG =================
        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtMatKhau.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            cboQuyen.SelectedIndex = 0;
            txtMaNV.Focus();
        }

        // ================= TÌM KIẾM =================
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM NhanVien WHERE TenNV LIKE @ten";
            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@ten", "%" + txtSearch.Text + "%"));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy!");
                LoadData();
                return;
            }
            dtgvData.DataSource = dt;
            SetupDataGridView();
        }

        // ================= THOÁT =================
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