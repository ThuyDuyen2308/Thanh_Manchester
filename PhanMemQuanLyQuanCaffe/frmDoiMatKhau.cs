using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            {
                // Kiểm tra mật khẩu cũ có trùng khớp không
                if (txtNhapMatKhauCu.Text != frmDangNhap.MatKhau)
                {
                    MessageBox.Show("Mật khẩu cũ không trùng khớp");
                    txtNhapMatKhauCu.Focus();
                    return;
                }

                // Kiểm tra mật khẩu mới bắt buộc nhập
                if (string.IsNullOrEmpty(txtNhapMatKhauMoi.Text))
                {
                    MessageBox.Show("Mật khẩu mới không được trống");
                    txtNhapMatKhauMoi.Focus();
                    return;
                }

                // Kiểm tra nhập lại mật khẩu có khớp không
                if (txtXacNhatLaiMatKhau.Text != txtXacNhatLaiMatKhau.Text)
                {
                    MessageBox.Show("Nhập lại mật khẩu không khớp");
                    txtXacNhatLaiMatKhau.Focus();
                    return;
                }
                string strSQL = $"UPDATE NhanVien SET MatKhau = '{txtNhapMatKhauMoi.Text}' WHERE MaNV = '{frmDangNhap.MaNV}'";
                ConnectSQL.RunQuery(strSQL);

                MessageBox.Show("Đổi mật khẩu thành công");
                this.Close();

            }
        }
    }
}
