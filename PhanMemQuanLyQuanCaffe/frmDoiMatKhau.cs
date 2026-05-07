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
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // Kiểm tra mật khẩu cũ
            if (txtNhapMatKhauCu.Text != frmDangNhap.MatKhau)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapMatKhauCu.Focus();
                return;
            }

            // Kiểm tra mật khẩu mới không được trống
            if (string.IsNullOrEmpty(txtNhapMatKhauMoi.Text))
            {
                MessageBox.Show("Mật khẩu mới không được trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapMatKhauMoi.Focus();
                return;
            }

            // Kiểm tra nhập lại có khớp không
            if (txtNhapMatKhauMoi.Text != txtXacNhatLaiMatKhau.Text)
            {
                MessageBox.Show("Nhập lại mật khẩu không khớp!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhatLaiMatKhau.Focus();
                return;
            }

            // Kiểm tra mật khẩu phải là số
            if (!int.TryParse(txtNhapMatKhauMoi.Text, out int mkMoi))
            {
                MessageBox.Show("Mật khẩu phải là số!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật database
            ConnectSQL.RunQuery(
                "UPDATE NhanVien SET MatKhau=@mk WHERE MaNV=@ma",
                new SqlParameter("@mk", mkMoi),
                new SqlParameter("@ma", frmDangNhap.MaNV)
            );

            // Cập nhật lại session
            frmDangNhap.MatKhau = txtNhapMatKhauMoi.Text;

            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
