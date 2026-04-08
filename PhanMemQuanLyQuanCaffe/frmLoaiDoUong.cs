using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmLoaiDoUong : Form
    {
        public frmLoaiDoUong()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void frmLoaiDoUong_Load(object sender, EventArgs e)
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
            string sql = "SELECT * FROM LoaiDoUong";
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
            txtMaLoai.Text = r.Cells["MaLoai"].Value.ToString();
            txtTenLoai.Text = r.Cells["TenLoai"].Value.ToString();
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            // Check rỗng
            if (string.IsNullOrWhiteSpace(txtMaLoai.Text))
            {
                MessageBox.Show("Chưa nhập mã loại!");
                txtMaLoai.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Chưa nhập tên loại!");
                txtTenLoai.Focus();
                return;
            }

            // Check số
            if (!int.TryParse(txtMaLoai.Text, out int maLoai))
            {
                MessageBox.Show("Mã loại phải là số!");
                txtMaLoai.Focus();
                return;
            }

            // Check trùng mã
            string sqlCheck = @"SELECT COUNT(*) 
                                FROM LoaiDoUong 
                                WHERE MaLoai = @maloai";

            int tonTai = Convert.ToInt32(
                ConnectSQL.ExecuteScalar(sqlCheck,
                    new SqlParameter("@maloai", maLoai))
            );

            if (tonTai > 0)
            {
                MessageBox.Show("Mã loại đã tồn tại, vui lòng nhập mã khác!");
                txtMaLoai.Focus();
                return;
            }

            // Insert
            string sqlInsert = @"INSERT INTO LoaiDoUong (MaLoai, TenLoai)
                                 VALUES (@maloai, @tenloai)";

            int kq = ConnectSQL.RunQuery(sqlInsert,
                new SqlParameter("@maloai", maLoai),
                new SqlParameter("@tenloai", txtTenLoai.Text.Trim())
            );

            if (kq > 0)
            {
                MessageBox.Show("Thêm loại đồ uống thành công!");
                LoadData();
                txtMaLoai.Clear();
                txtTenLoai.Clear();
            }
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE LoaiDoUong
                           SET TenLoai = @tenloai
                           WHERE MaLoai = @maloai";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@tenloai", txtTenLoai.Text.Trim()),
                new SqlParameter("@maloai", txtMaLoai.Text)
            );

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa loại đồ uống này?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            string sql = @"DELETE FROM LoaiDoUong 
                           WHERE MaLoai = @maloai";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@maloai", txtMaLoai.Text)
            );

            MessageBox.Show("Xóa thành công!");
            LoadData();
        }

        // ================= XÓA TRẮNG =================
        private void menuXoaTrang_Click_1(object sender, EventArgs e)
        { 
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMaLoai.Focus();
        }

        // ================= TÌM KIẾM =================
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadData();
                return;
            }

            string sql = @"SELECT * FROM LoaiDoUong
                           WHERE TenLoai LIKE N'%" + txtSearch.Text + "%'";

            DataTable dt = ConnectSQL.Load(sql);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy loại đồ uống!");
                LoadData();
                return;
            }

            dtgvData.DataSource = dt;
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

        private void btntimkiem_Click_1(object sender, EventArgs e)
        {    
            string sql = "SELECT * FROM LoaiDoUong WHERE TenLoai LIKE @tenloai";

            DataTable dt = ConnectSQL.Load(
                sql.Replace("@tenloai", "N'%" + txtSearch.Text + "%'"));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy loại đồ uống!");
                LoadData();
                return;
            }

            dtgvData.DataSource = dt;
        }
    }
}

