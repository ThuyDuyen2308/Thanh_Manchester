using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmChuyenBan : Form
    {
        public frmChuyenBan()
        {
            InitializeComponent();
        }

        private void frmChuyenBan_Load(object sender, EventArgs e)
        {
            LoadBanNguon();
            LoadBanDich();
        }

        private void LoadBanNguon()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaBan FROM Ban WHERE TrangThai = 1 ORDER BY MaBan");// Chỉ lấy bàn đang có người (TrangThai=1)
            cboBanNguon.Items.Clear();// Xóa hết item cũ
            foreach (DataRow r in dt.Rows)//    Thêm từng bàn vào cboBanNguon
                cboBanNguon.Items.Add(r["MaBan"].ToString().Trim());// Trim() để loại bỏ khoảng trắng thừa
            if (cboBanNguon.Items.Count > 0)// Nếu có bàn nào thì chọn bàn đầu tiên làm mặc định
                cboBanNguon.SelectedIndex = 0;// Chọn bàn đầu tiên trong danh sách làm mặc định
        }

        private void LoadBanDich()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaBan FROM Ban WHERE TrangThai = 0 ORDER BY MaBan");
            cboBanDich.Items.Clear();
            foreach (DataRow r in dt.Rows)
                cboBanDich.Items.Add(r["MaBan"].ToString().Trim());
            if (cboBanDich.Items.Count > 0)
                cboBanDich.SelectedIndex = 0;
        }

        private void cboBanNguon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBanNguon.SelectedItem == null) return;
            string maBan = cboBanNguon.SelectedItem.ToString();// Lấy mã bàn nguồn đã chọn

            string sql = @"SELECT du.TenDU, ct.SoLuong, ct.DonGia, ct.ThanhTien
                           FROM ChiTietHoaDon ct
                           INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                           INNER JOIN DoUong du ON ct.MaDU = du.MaDU
                           WHERE hd.TrangThai = 0 AND hd.MaBan = @MaBan";

            DataTable dt = ConnectSQL.Load(sql, new SqlParameter("@MaBan", maBan));// Lấy chi tiết hóa đơn của bàn nguồn đang chọn
            dtgvHoaDon.DataSource = dt;
            dtgvHoaDon.AllowUserToAddRows = false;
            dtgvHoaDon.ReadOnly = true;
            dtgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            long tong = 0;
            foreach (DataRow r in dt.Rows)
                tong += Convert.ToInt64(r["ThanhTien"]);
            lblTong.Text = "Tổng tiền: " + tong.ToString("N0") + " đ";
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            if (cboBanNguon.SelectedItem == null || cboBanDich.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đủ bàn nguồn và bàn đích!"); return;
            }

            string maNguon = cboBanNguon.SelectedItem.ToString();
            string maDich = cboBanDich.SelectedItem.ToString();

            if (maNguon == maDich)
            {
                MessageBox.Show("Bàn nguồn và bàn đích không được trùng!"); return;
            }

            if (MessageBox.Show($"Chuyển bàn [{maNguon}] → [{maDich}]?", "Xác nhận",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            using (SqlConnection cnn = new SqlConnection(ConnectSQL.connectionString))
            {
                cnn.Open();
                SqlTransaction tran = cnn.BeginTransaction();
                try
                {
                    // Lấy MaHD bàn nguồn
                    SqlCommand cmd1 = new SqlCommand(
                        "SELECT MaHD FROM HoaDon WHERE MaBan=@mb AND TrangThai=0", cnn, tran);
                    cmd1.Parameters.AddWithValue("@mb", maNguon);
                    object maHD = cmd1.ExecuteScalar();
                    if (maHD == null) { MessageBox.Show("Bàn nguồn chưa có hóa đơn!"); tran.Rollback(); return; }

                    // Cập nhật HoaDon sang bàn đích
                    SqlCommand cmd2 = new SqlCommand(
                        "UPDATE HoaDon SET MaBan=@maDich WHERE MaHD=@maHD", cnn, tran);
                    cmd2.Parameters.AddWithValue("@maDich", maDich);
                    cmd2.Parameters.AddWithValue("@maHD", maHD.ToString());
                    cmd2.ExecuteNonQuery();

                    // Bàn nguồn → trống
                    SqlCommand cmd3 = new SqlCommand(
                        "UPDATE Ban SET TrangThai=0 WHERE MaBan=@mb", cnn, tran);
                    cmd3.Parameters.AddWithValue("@mb", maNguon);
                    cmd3.ExecuteNonQuery();

                    // Bàn đích → có người
                    SqlCommand cmd4 = new SqlCommand(
                        "UPDATE Ban SET TrangThai=1 WHERE MaBan=@mb", cnn, tran);
                    cmd4.Parameters.AddWithValue("@mb", maDich);
                    cmd4.ExecuteNonQuery();

                    tran.Commit();
                    MessageBox.Show($"Chuyển bàn thành công!\n[{maNguon}] → [{maDich}]");
                    LoadBanNguon(); LoadBanDich();
                    dtgvHoaDon.DataSource = null;
                    lblTong.Text = "Tổng tiền: 0 đ";
                }
                catch (Exception ex) { tran.Rollback(); MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e) { this.Close(); }
    }
}