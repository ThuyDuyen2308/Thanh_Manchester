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

            txtMaNV.Text = r.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = r.Cells["TenNV"].Value.ToString();
            txtMatKhau.Text = r.Cells["MatKhau"].Value.ToString();
            txtSDT.Text = r.Cells["SDT"].Value.ToString();
            txtDiaChi.Text = r.Cells["DiaChi"].Value.ToString();
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" || txtTenNV.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
                return;
            }

            if (!int.TryParse(txtMatKhau.Text, out int mk))
            {
                MessageBox.Show("Mật khẩu phải là số");
                return;
            }

            string sql = @"INSERT INTO NhanVien
                           VALUES (@ma,@ten,@mk,@sdt,@dc)";

            int kq = ConnectSQL.RunQuery(sql,
                new SqlParameter("@ma", txtMaNV.Text),
                new SqlParameter("@ten", txtTenNV.Text),
                new SqlParameter("@mk", mk),
                new SqlParameter("@sdt", txtSDT.Text),
                new SqlParameter("@dc", txtDiaChi.Text));

            if (kq > 0)
            {
                MessageBox.Show("Thêm nhân viên thành công");
                LoadData();
            }
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE NhanVien SET
                           TenNV=@ten, MatKhau=@mk, SDT=@sdt, DiaChi=@dc
                           WHERE MaNV=@ma";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@ten", txtTenNV.Text),
                new SqlParameter("@mk", int.Parse(txtMatKhau.Text)),
                new SqlParameter("@sdt", txtSDT.Text),
                new SqlParameter("@dc", txtDiaChi.Text),
                new SqlParameter("@ma", txtMaNV.Text));

            MessageBox.Show("Cập nhật thành công");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa nhân viên này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            string sql = "DELETE FROM NhanVien WHERE MaNV=@ma";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@ma", txtMaNV.Text));

            MessageBox.Show("Xóa thành công");
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
            txtMaNV.Focus();
        }

        // ================= TÌM KIẾM =================
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM NhanVien WHERE TenNV LIKE @ten";
            DataTable dt = ConnectSQL.Load(
                sql.Replace("@ten", "N'%" + txtSearch.Text + "%'"));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy!");
                LoadData();
                return;
            }
            dtgvData.DataSource = dt;
        }
        private void menuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quay về màn hình chính?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new frmManHinhChinh().Show();
                this.Close(); // đóng form nhân viên
            }
        }

    }
}
