using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmGopBan : Form
    {
        public frmGopBan()
        {
            InitializeComponent();
        }

        private void frmGopBan_Load(object sender, EventArgs e)
        {
            LoadBanNguon();
            LoadBanDich();
        }

        private void LoadBanNguon()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaBan FROM Ban WHERE TrangThai=1 ORDER BY MaBan");
            clbBanNguon.Items.Clear();
            foreach (DataRow r in dt.Rows)
                clbBanNguon.Items.Add(r["MaBan"].ToString().Trim());
            lblSoLuongChon.Text = "Đã chọn: 0 bàn";
        }

        private void LoadBanDich()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaBan FROM Ban WHERE TrangThai=1 ORDER BY MaBan");
            cboBanDich.Items.Clear();
            foreach (DataRow r in dt.Rows)
                cboBanDich.Items.Add(r["MaBan"].ToString().Trim());
            if (cboBanDich.Items.Count > 0)
                cboBanDich.SelectedIndex = 0;
        }

        private void clbBanNguon_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                lblSoLuongChon.Text = "Đã chọn: " + clbBanNguon.CheckedItems.Count + " bàn";
                HienThiChiTiet();
            }));
        }

        private void HienThiChiTiet()
        {
            dtgvChiTiet.Rows.Clear();
            long tongAll = 0;

            foreach (var item in clbBanNguon.CheckedItems)
            {
                string maBan = item.ToString();
                string sql = @"SELECT du.TenDU, ct.SoLuong, ct.DonGia, ct.ThanhTien
                               FROM ChiTietHoaDon ct
                               INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                               INNER JOIN DoUong du ON ct.MaDU = du.MaDU
                               WHERE hd.TrangThai=0 AND hd.MaBan=@MaBan";

                DataTable dt = ConnectSQL.Load(sql, new SqlParameter("@MaBan", maBan));
                foreach (DataRow r in dt.Rows)
                {
                    long tt = Convert.ToInt64(r["ThanhTien"]);
                    dtgvChiTiet.Rows.Add(maBan, r["TenDU"], r["SoLuong"],
                        Convert.ToInt32(r["DonGia"]).ToString("N0"), tt.ToString("N0"));
                    tongAll += tt;
                }
            }
            lblTongGop.Text = "Tổng tiền gộp: " + tongAll.ToString("N0") + " đ";
        }

        private void btnGopBan_Click(object sender, EventArgs e)
        {
            if (clbBanNguon.CheckedItems.Count == 0)
            { MessageBox.Show("Chọn ít nhất 1 bàn cần gộp!"); return; }

            if (cboBanDich.SelectedItem == null)
            { MessageBox.Show("Chọn bàn đích!"); return; }

            string maDich = cboBanDich.SelectedItem.ToString();

            foreach (var item in clbBanNguon.CheckedItems)
                if (item.ToString() == maDich)
                { MessageBox.Show("Bàn đích không được nằm trong danh sách gộp!"); return; }

            List<string> banNguon = new List<string>();
            foreach (var item in clbBanNguon.CheckedItems)
                banNguon.Add(item.ToString());

            if (MessageBox.Show($"Gộp [{string.Join(", ", banNguon)}] → [{maDich}]?",
                "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            using (SqlConnection cnn = new SqlConnection(ConnectSQL.connectionString))
            {
                cnn.Open();
                SqlTransaction tran = cnn.BeginTransaction();
                try
                {
                    // Lấy hoặc tạo HoaDon bàn đích
                    SqlCommand cmdHDDich = new SqlCommand(
                        "SELECT MaHD FROM HoaDon WHERE MaBan=@mb AND TrangThai=0", cnn, tran);
                    cmdHDDich.Parameters.AddWithValue("@mb", maDich);
                    object maHDDich = cmdHDDich.ExecuteScalar();

                    if (maHDDich == null)
                    {
                        string newHD = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        SqlCommand cmdNew = new SqlCommand(
                            "INSERT INTO HoaDon(MaHD,NgayLap,MaNV,MaKH,MaBan,TongTien,TrangThai) VALUES(@hd,GETDATE(),@nv,NULL,@mb,0,0)",
                            cnn, tran);
                        cmdNew.Parameters.AddWithValue("@hd", newHD);
                        cmdNew.Parameters.AddWithValue("@nv", frmDangNhap.MaNV);
                        cmdNew.Parameters.AddWithValue("@mb", maDich);
                        cmdNew.ExecuteNonQuery();
                        maHDDich = newHD;
                    }

                    foreach (string maNguon in banNguon)
                    {
                        // Lấy MaHD bàn nguồn
                        SqlCommand cmdHDNguon = new SqlCommand(
                            "SELECT MaHD FROM HoaDon WHERE MaBan=@mb AND TrangThai=0", cnn, tran);
                        cmdHDNguon.Parameters.AddWithValue("@mb", maNguon);
                        object maHDNguon = cmdHDNguon.ExecuteScalar();
                        if (maHDNguon == null) continue;

                        // Lấy chi tiết bàn nguồn
                        SqlCommand cmdCT = new SqlCommand(
                            "SELECT MaDU,SoLuong,DonGia FROM ChiTietHoaDon WHERE MaHD=@hd", cnn, tran);
                        cmdCT.Parameters.AddWithValue("@hd", maHDNguon);
                        var reader = cmdCT.ExecuteReader();
                        var items = new List<(string MaDU, int SL, int DG)>();
                        while (reader.Read())
                            items.Add((reader["MaDU"].ToString(), Convert.ToInt32(reader["SoLuong"]), Convert.ToInt32(reader["DonGia"])));
                        reader.Close();

                        // Gộp từng món vào bàn đích
                        foreach (var it in items)
                        {
                            SqlCommand cmdCheck = new SqlCommand(
                                "SELECT SoLuong FROM ChiTietHoaDon WHERE MaHD=@hd AND MaDU=@du", cnn, tran);
                            cmdCheck.Parameters.AddWithValue("@hd", maHDDich);
                            cmdCheck.Parameters.AddWithValue("@du", it.MaDU);
                            object slCu = cmdCheck.ExecuteScalar();

                            if (slCu != null)
                            {
                                int slMoi = Convert.ToInt32(slCu) + it.SL;
                                SqlCommand cmdUpd = new SqlCommand(
                                    "UPDATE ChiTietHoaDon SET SoLuong=@sl, ThanhTien=@tt WHERE MaHD=@hd AND MaDU=@du",
                                    cnn, tran);
                                cmdUpd.Parameters.AddWithValue("@sl", slMoi);
                                cmdUpd.Parameters.AddWithValue("@tt", (long)slMoi * it.DG);
                                cmdUpd.Parameters.AddWithValue("@hd", maHDDich);
                                cmdUpd.Parameters.AddWithValue("@du", it.MaDU);
                                cmdUpd.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmdIns = new SqlCommand(
                                    "INSERT INTO ChiTietHoaDon(MaHD,MaDU,SoLuong,DonGia,ThanhTien) VALUES(@hd,@du,@sl,@dg,@tt)",
                                    cnn, tran);
                                cmdIns.Parameters.AddWithValue("@hd", maHDDich);
                                cmdIns.Parameters.AddWithValue("@du", it.MaDU);
                                cmdIns.Parameters.AddWithValue("@sl", it.SL);
                                cmdIns.Parameters.AddWithValue("@dg", it.DG);
                                cmdIns.Parameters.AddWithValue("@tt", (long)it.SL * it.DG);
                                cmdIns.ExecuteNonQuery();
                            }
                        }

                        // Xóa chi tiết + hóa đơn bàn nguồn
                        new SqlCommand("DELETE FROM ChiTietHoaDon WHERE MaHD=@hd", cnn, tran)
                        { Parameters = { new SqlParameter("@hd", maHDNguon) } }.ExecuteNonQuery();
                        new SqlCommand("DELETE FROM HoaDon WHERE MaHD=@hd", cnn, tran)
                        { Parameters = { new SqlParameter("@hd", maHDNguon) } }.ExecuteNonQuery();

                        // Bàn nguồn → trống
                        new SqlCommand("UPDATE Ban SET TrangThai=0 WHERE MaBan=@mb", cnn, tran)
                        { Parameters = { new SqlParameter("@mb", maNguon) } }.ExecuteNonQuery();
                    }

                    // Đảm bảo bàn đích = có người
                    new SqlCommand("UPDATE Ban SET TrangThai=1 WHERE MaBan=@mb", cnn, tran)
                    { Parameters = { new SqlParameter("@mb", maDich) } }.ExecuteNonQuery();

                    tran.Commit();
                    MessageBox.Show("Gộp bàn thành công!\nTất cả đồ uống đã vào bàn [" + maDich + "]");
                    LoadBanNguon(); LoadBanDich();
                    dtgvChiTiet.Rows.Clear();
                    lblTongGop.Text = "Tổng tiền gộp: 0 đ";
                }
                catch (Exception ex) { tran.Rollback(); MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e) { this.Close(); }
    }
}