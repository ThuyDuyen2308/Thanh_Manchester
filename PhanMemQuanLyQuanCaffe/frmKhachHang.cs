using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }
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
        private void LoadData()
        {
            string sql = "SELECT * FROM KhachHang";
            dtgvData.DataSource = ConnectSQL.Load(sql);
            SetupDataGridView();

            if (dtgvData.Rows.Count > 0)
                ShowRow(0);
        }
        private void ShowRow(int index)
        {
            if (index < 0) return;

            DataGridViewRow r = dtgvData.Rows[index];
            txtMaKH.Text = r.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = r.Cells["TenKH"].Value.ToString();
            txtSDT.Text = r.Cells["SDT"].Value.ToString();
            txtDiaChi.Text = r.Cells["DiaChi"].Value.ToString();
        }
        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        private void menuThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) ||
                string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (!int.TryParse(txtMaKH.Text, out int maKH))
            {
                MessageBox.Show("Mã KH phải là số!");
                txtMaKH.Focus();
                return;
            }

            string sqlCheck = @"SELECT COUNT(*) FROM KhachHang WHERE MaKH = @makh";
            int tonTai = Convert.ToInt32(
                ConnectSQL.ExecuteScalar(sqlCheck,
                    new SqlParameter("@makh", maKH)));

            if (tonTai > 0)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!");
                txtMaKH.Focus();
                return;
            }

            string sqlInsert = @"INSERT INTO KhachHang (MaKH, TenKH, SDT, DiaChi)
                                 VALUES (@makh, @tenkh, @sdt, @diachi)";

            int kq = ConnectSQL.RunQuery(sqlInsert,
                new SqlParameter("@makh", maKH),
                new SqlParameter("@tenkh", txtTenKH.Text.Trim()),
                new SqlParameter("@sdt", txtSDT.Text.Trim()),
                new SqlParameter("@diachi", txtDiaChi.Text.Trim())
            );

            if (kq > 0)
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                LoadData();
                XoaTrang();
            }
        }

        private void menuSua_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE KhachHang
                           SET TenKH = @tenkh,
                               SDT = @sdt,
                               DiaChi = @diachi
                           WHERE MaKH = @makh";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@tenkh", txtTenKH.Text.Trim()),
                new SqlParameter("@sdt", txtSDT.Text.Trim()),
                new SqlParameter("@diachi", txtDiaChi.Text.Trim()),
                new SqlParameter("@makh", txtMaKH.Text)
            );

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        private void menuXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa khách hàng này?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            string sql = @"DELETE FROM KhachHang WHERE MaKH = @makh";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@makh", txtMaKH.Text)
            );

            MessageBox.Show("Xóa thành công!");
            LoadData();
        }

        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }

        private void XoaTrang()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtMaKH.Focus();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadData();
                return;
            }

            string sql = @"SELECT * FROM KhachHang
                           WHERE TenKH LIKE N'%" + txtSearch.Text + "%'";

            DataTable dt = ConnectSQL.Load(sql);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng!");
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
                this.Close();
            }
        }    
    }
}
