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

        // ================= LOAD DATA =================
        private void LoadData()
        {
            string sql = @"
            SELECT 
                MaBan,
                LoaiBan,
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

            string loai = r.Cells["LoaiBan"].Value.ToString();

            if (loai == "VIP")
            {
                chkVip.Checked = true;
                chkThuong.Checked = false;
            }
            else
            {
                chkVip.Checked = false;
                chkThuong.Checked = true;
            }
        } // 🔥 thiếu dấu này nên lỗi toàn bộ phía dưới

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

            // 👉 LẤY LOẠI BÀN (CHECKBOX)
            string loaiBan = "";

            if (chkVip.Checked)
                loaiBan = "VIP";
            else if (chkThuong.Checked)
                loaiBan = "Thường";
            else
            {
                MessageBox.Show("Vui lòng chọn loại bàn!");
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
                INSERT INTO Ban (MaBan, LoaiBan, SucChua, TrangThai)
                VALUES (@maban, @loaiban, @succhua, 0)";

            ConnectSQL.RunQuery(sqlInsert,
                new SqlParameter("@maban", txtMaBan.Text.Trim()),
                new SqlParameter("@loaiban", loaiBan),
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

            string loaiBan = chkVip.Checked ? "VIP" : "Thường";

            string sql = @"
                UPDATE Ban
                SET SucChua = @succhua,
                    LoaiBan = @loaiban
                WHERE MaBan = @maban";

            ConnectSQL.RunQuery(sql,
                new SqlParameter("@succhua", sucChua),
                new SqlParameter("@loaiban", loaiBan),
                new SqlParameter("@maban", txtMaBan.Text.Trim())
            );

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra bàn có hóa đơn không
            string checkHD = "SELECT COUNT(*) FROM HoaDon WHERE MaBan = @MaBan";
            bool coHoaDon = ConnectSQL.ExecuteScalarBool(checkHD,
                new SqlParameter("@MaBan", txtMaBan.Text));

            if (coHoaDon)
            {
                MessageBox.Show(
                    "Không thể xóa bàn này vì đang có hóa đơn liên kết!\nVui lòng xóa hóa đơn trước.",
                    "Không Thể Xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xóa bàn này?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.No) return;

            string sql = "DELETE FROM Ban WHERE MaBan = @MaBan";
            ConnectSQL.RunQuery(sql,
                new SqlParameter("@MaBan", txtMaBan.Text));

            MessageBox.Show("Xóa thành công!");
            LoadData();
        }

        // ================= KHÁC =================
        private void ClearText()
        {
            txtMaBan.Clear();
            txtSucChua.Clear();

            chkVip.Checked = false;
            chkThuong.Checked = false;

            txtMaBan.Focus();
        }

        private void chkVip_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVip.Checked)
                chkThuong.Checked = false;
        }

        private void chkThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThuong.Checked)
                chkVip.Checked = false;
        }

        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?",
        "Xác nhận",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

    }
}