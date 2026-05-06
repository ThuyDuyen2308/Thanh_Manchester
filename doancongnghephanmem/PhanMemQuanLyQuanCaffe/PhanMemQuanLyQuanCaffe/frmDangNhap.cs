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
    public partial class frmDangNhap : Form
    {     
        public frmDangNhap()
        {
            InitializeComponent();
        }
        public static string MatKhau;
        public static string MaNV;
        public static string Quyen = ""; 
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            string sql = @"
                SELECT MaNV, TenNV, Quyen 
                FROM NhanVien 
                WHERE TenNV = @TenDangNhap 
                  AND MatKhau = @MatKhau";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@TenDangNhap", txtDangNhap.Text.Trim()),
                new SqlParameter("@MatKhau", int.Parse(txtMatKhau.Text.Trim()))
            );

            if (dt.Rows.Count > 0)
            {
                // Lưu thông tin đăng nhập
                frmDangNhap.MaNV = dt.Rows[0]["MaNV"].ToString();
                frmDangNhap.MatKhau = txtMatKhau.Text.Trim();
                frmDangNhap.Quyen = dt.Rows[0]["Quyen"].ToString().Trim();

                // Chấm công vào
                ConnectSQL.RunQuery(
                    @"INSERT INTO CaLamViec (MaNV, NgayLam, GioVao, TrangThai)
                    VALUES (@MaNV, CAST(GETDATE() AS DATE), GETDATE(), 0)",
                    new SqlParameter("@MaNV", frmDangNhap.MaNV)
                );

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                frmManHinhChinh mainForm = new frmManHinhChinh();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}


