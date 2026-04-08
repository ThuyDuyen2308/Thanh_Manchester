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
        public static string Quyen = "";  // ← THÊM DÒNG NÀY
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDangNhap.Text))
            {
                MessageBox.Show("Chưa nhập mã nhân viên");
                txtDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Chưa nhập mật khẩu");
                txtMatKhau.Focus();
                return;
            }

            // ÉP mật khẩu sang INT
            if (!int.TryParse(txtMatKhau.Text.Trim(), out int matKhau))
            {
                MessageBox.Show("Mật khẩu phải là số!");
                txtMatKhau.Focus();
                return;
            }

            string sql = @"
        SELECT COUNT(*) 
        FROM NhanVien
        WHERE MaNV = @ma
          AND MatKhau = @mk";

            bool check = ConnectSQL.ExecuteScalarBool(
                sql,
                new SqlParameter("@ma", txtDangNhap.Text.Trim()),
                new SqlParameter("@mk", matKhau)
            );
            if (check)
            {
                MaNV = txtDangNhap.Text.Trim();
                MatKhau = txtMatKhau.Text;

                // ← THÊM ĐOẠN NÀY ĐỂ LẤY QUYỀN TỪ DB
                string sqlQuyen = "SELECT Quyen FROM NhanVien WHERE MaNV = @ma";
                object q = ConnectSQL.ExecuteScalar(sqlQuyen,
                    new SqlParameter("@ma", MaNV));
                Quyen = (q != null && q != DBNull.Value) ? q.ToString() : "";

                MessageBox.Show("Đăng nhập thành công ✅");
                this.Hide();
                new frmManHinhChinh().Show();
            }
            else
            {
                MessageBox.Show("Sai mã nhân viên hoặc mật khẩu ❌");
                txtDangNhap.Focus();
            }

        }
    
    }
}


