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

        private void frmBan_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
        }

        private void SetupDataGridView()
        {
            dtgvData.Columns.Clear();
            dtgvData.AutoGenerateColumns = true;
            dtgvData.AllowUserToAddRows = false;
            dtgvData.ReadOnly = true;
            dtgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvData.EnableHeadersVisualStyles = false;
        }

        private void LoadData()
        {
            string sql = @"SELECT MaBan, LoaiBan, SucChua, TrangThai FROM Ban";

            dtgvData.DataSource = ConnectSQL.Load(sql);

            if (dtgvData.Rows.Count > 0)
                ShowRow(0);
        }

        private void ShowRow(int index)
        {
            if (index < 0 || index >= dtgvData.Rows.Count) return;

            DataGridViewRow r = dtgvData.Rows[index];

            txtMaBan.Text = r.Cells["MaBan"].Value?.ToString().Trim();
            txtSucChua.Text = r.Cells["SucChua"].Value?.ToString();

            int loaiBan = Convert.ToInt32(r.Cells["LoaiBan"].Value);

            chkVip.Checked = (loaiBan == 1);
            chkThuong.Checked = (loaiBan == 0);

            // ❌ KHÔNG khóa nữa
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowRow(e.RowIndex);
        }

        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            txtMaBan.Enabled = true; // cho nhập mới

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

            if (!chkVip.Checked && !chkThuong.Checked)
            {
                MessageBox.Show("Chọn loại bàn!");
                return;
            }

            int loaiBan = chkVip.Checked ? 1 : 0;
            string maBan = txtMaBan.Text.Trim().ToUpper();

            int tonTai = Convert.ToInt32(
                ConnectSQL.ExecuteScalar(
                    "SELECT COUNT(*) FROM Ban WHERE MaBan=@maban",
                    new SqlParameter("@maban", maBan))
            );

            if (tonTai > 0)
            {
                MessageBox.Show("Mã bàn đã tồn tại!");
                return;
            }

            ConnectSQL.RunQuery(
                "INSERT INTO Ban (MaBan, LoaiBan, SucChua, TrangThai) VALUES (@maban,@loaiban,@succhua,0)",
                new SqlParameter("@maban", maBan),
                new SqlParameter("@loaiban", loaiBan),
                new SqlParameter("@succhua", sucChua)
            );

            MessageBox.Show("Thêm thành công!");
            LoadData();
            ClearText();
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            if (dtgvData.CurrentRow == null)
            {
                MessageBox.Show("Chọn bàn cần sửa!");
                return;
            }

            if (!int.TryParse(txtSucChua.Text, out int sucChua))
            {
                MessageBox.Show("Sức chứa phải là số!");
                return;
            }

            int loaiBan = chkVip.Checked ? 1 : 0;

            string maBanCu = dtgvData.CurrentRow.Cells["MaBan"].Value.ToString().Trim();
            string maBanMoi = txtMaBan.Text.Trim().ToUpper();

            try
            {
                using (SqlConnection cnn = new SqlConnection(ConnectSQL.connectionString))
                {
                    cnn.Open();
                    SqlTransaction tran = cnn.BeginTransaction();

                    try
                    {
                        // update Ban
                        SqlCommand cmd1 = new SqlCommand(
                            "UPDATE Ban SET MaBan=@moi,SucChua=@succhua,LoaiBan=@loaiban WHERE MaBan=@cu",
                            cnn, tran);

                        cmd1.Parameters.AddWithValue("@moi", maBanMoi);
                        cmd1.Parameters.AddWithValue("@cu", maBanCu);
                        cmd1.Parameters.AddWithValue("@succhua", sucChua);
                        cmd1.Parameters.AddWithValue("@loaiban", loaiBan);
                        cmd1.ExecuteNonQuery();

                        // update HoaDon
                        SqlCommand cmd2 = new SqlCommand(
                            "UPDATE HoaDon SET MaBan=@moi WHERE MaBan=@cu",
                            cnn, tran);

                        cmd2.Parameters.AddWithValue("@moi", maBanMoi);
                        cmd2.Parameters.AddWithValue("@cu", maBanCu);
                        cmd2.ExecuteNonQuery();

                        tran.Commit();

                        MessageBox.Show("Cập nhật thành công!");
                        LoadData();
                    }
                    catch
                    {
                        tran.Rollback();
                        MessageBox.Show("❌ Lỗi cập nhật!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaBan.Text))
            {
                MessageBox.Show("Chọn bàn cần xóa!");
                return;
            }

            string maBan = txtMaBan.Text.Trim();

            if (MessageBox.Show("Xóa bàn + toàn bộ dữ liệu?", "Cảnh báo",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // 🔥 xóa chi tiết trước
            ConnectSQL.RunQuery(@"
                DELETE FROM ChiTietHoaDon 
                WHERE MaHD IN (SELECT MaHD FROM HoaDon WHERE MaBan=@MaBan)",
                new SqlParameter("@MaBan", maBan));

            // 🔥 xóa hóa đơn
            ConnectSQL.RunQuery(
                "DELETE FROM HoaDon WHERE MaBan=@MaBan",
                new SqlParameter("@MaBan", maBan)
            );

            // 🔥 xóa bàn
            ConnectSQL.RunQuery(
                "DELETE FROM Ban WHERE MaBan=@MaBan",
                new SqlParameter("@MaBan", maBan)
            );

            MessageBox.Show("Xóa thành công!");
            LoadData();
            ClearText();
        }

        private void ClearText()
        {
            txtMaBan.Clear();
            txtSucChua.Clear();
            chkVip.Checked = false;
            chkThuong.Checked = false;

            txtMaBan.Enabled = true;
            txtMaBan.Focus();
        }

        private void chkVip_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVip.Checked) chkThuong.Checked = false;
        }

        private void chkThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThuong.Checked) chkVip.Checked = false;
        }

        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thoát?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }
    }
}