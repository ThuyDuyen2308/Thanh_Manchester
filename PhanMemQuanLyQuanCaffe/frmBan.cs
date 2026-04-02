using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmBan : Form
    {
        public frmBan()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void frmBan_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
        }

        // ================= SETUP GRID =================
        private void SetupDataGridView()
        {
            dtgvData.Columns.Clear();          // 🔥 FIX LỖI MẤT DỮ LIỆU
            dtgvData.AutoGenerateColumns = true;

            dtgvData.AllowUserToAddRows = false;
            dtgvData.ReadOnly = true;
            dtgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvData.EnableHeadersVisualStyles = false;
        }

        // ================= LOAD DATA =================
        private void LoadData()
        {
            string sql = @"
        SELECT 
            MaBan,
            SucChua,
            CASE 
                WHEN TrangThai = 1 THEN N'Có người'
                ELSE N'Trống'
            END AS TrangThai
        FROM Ban";

            dtgvData.DataSource = ConnectSQL.Load(sql);

            if (dtgvData.Rows.Count > 0)
                ShowRow(0);
        }


        // ================= HIỂN THỊ =================
        private void ShowRow(int index)
        {
            if (index < 0 || index >= dtgvData.Rows.Count) return;

            DataGridViewRow r = dtgvData.Rows[index];
            txtMaBan.Text = r.Cells["MaBan"].Value.ToString();
            txtSucChua.Text = r.Cells["SucChua"].Value.ToString();
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaBan.Text) ||
                string.IsNullOrWhiteSpace(txtSucChua.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (!int.TryParse(txtSucChua.Text, out int sucChua))
            {
                MessageBox.Show("Sức chứa phải là số!");
                return;
            }

            string sqlCheck = "SELECT COUNT(*) FROM Ban WHERE MaBan = @maban";
            int tonTai = Convert.ToInt32(
                ConnectSQL.ExecuteScalar(sqlCheck,
                    new SqlParameter("@maban", txtMaBan.Text.Trim()))
            );

            if (tonTai > 0)
            {
                MessageBox.Show("Mã bàn đã tồn tại!");
                return;
            }

            string sqlInsert = @"
                INSERT INTO Ban (MaBan, SucChua, TrangThai)
                VALUES (@maban, @succhua, 0)";

            ConnectSQL.RunQuery(sqlInsert,
                new SqlParameter("@maban", txtMaBan.Text.Trim()),
                new SqlParameter("@succhua", sucChua)
            );

            MessageBox.Show("Thêm bàn thành công!");
            LoadData();
            ClearText();
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSucChua.Text, out int sucChua))
            {
                MessageBox.Show("Sức chứa phải là số!");
                return;
            }

            string sql = @"
                UPDATE Ban
                SET SucChua = @succhua
                WHERE MaBan = @maban";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@succhua", sucChua),
                new SqlParameter("@maban", txtMaBan.Text.Trim())
            );

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaBan.Text))
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!");
                return;
            }

            if (MessageBox.Show("Xóa bàn này?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            string sql = "DELETE FROM Ban WHERE MaBan = @maban";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@maban", txtMaBan.Text.Trim())
            );

            MessageBox.Show("Xóa thành công!");
            LoadData();
            ClearText();
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

            string sql = @"
                SELECT 
                    MaBan,
                    SucChua,
                    CASE 
                        WHEN ISNULL(TrangThai, 0) = 1 THEN N'Có người'
                        ELSE N'Trống'
                    END AS TrangThai
                FROM Ban
                WHERE MaBan LIKE @key";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@key", "%" + key + "%")
            );

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy bàn!");
                LoadData();
                return;
            }

            dtgvData.DataSource = dt;
        }

        // ================= KHÁC =================
        private void txtSucChua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void ClearText()
        {
            txtMaBan.Clear();
            txtSucChua.Clear();
            txtMaBan.Focus();
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
