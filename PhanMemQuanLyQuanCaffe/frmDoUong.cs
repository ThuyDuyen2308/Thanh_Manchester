using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmDoUong : Form
    {
        string folderHinh = Application.StartupPath + @"\luuanh\";// Đường dẫn thư mục lưu ảnh, nằm cùng cấp với file .exe của ứng dụng
        string tenHinh = "";// Biến lưu tên ảnh hiện tại, dùng để lưu vào database và quản lý file ảnh

        public frmDoUong() //khai báo biến from đồ uống
        {
            InitializeComponent();// Khai báo giao diện (tùy thuộc vào designer)
            this.Load += frmDoUong_Load;// Gán sự kiện Load cho form
        }

        // ================= FORM LOAD =================
        private void frmDoUong_Load(object sender, EventArgs e) // khai báo sự kiện load form đồ uống
        {
            dtgvData.AllowUserToAddRows = false;// không cho phép người giùng thêm dòng mới
            dtgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // tự động tăng chỉnh kính thước cột

            if (!Directory.Exists(folderHinh))// kiểm tra xem thư mục ảnh tồn tại chưa , nếu chưa thì tạo mới
                Directory.CreateDirectory(folderHinh);//tạo thư mục ảnh nếu chưa tồi tại

            LoadLoai();// tải dữ liệu đồ uống lên combo box loại đồ uống
            LoadData();// tải dữ liệu đồ uống lên datagridview
        }

        // ================= LOAD DATA =================
        private void LoadData()// hàm tải dữ liệu đồ uống lên datagridview
        {
            string sql = "SELECT MaDU, TenDU, MaLoai, DonGia, HinhAnh FROM DoUong";// lấy dữ liệu từ bảng đồ uống
            dtgvData.DataSource = ConnectSQL.Load(sql);//gán dữ liệ vào datagridview

            if (dtgvData.Rows.Count > 0)// kiểm tra nếu có dữ liệu thì hiển thị ở dong đầu tiên
                ShowRow(0);// hiển thị dòng đầu tiên của datagridview
        }

        // ================= LOAD LOAI =================
        private void LoadLoai()// hàm tải dữ liệu loại đồ uống lên combo box
        {
            string sql = "SELECT MaLoai, TenLoai FROM LoaiDoUong";//lấy dữ liệu từ bảng douong
            DataTable dt = ConnectSQL.Load(sql);// gán dữ liệu vào datatable

            cboMaLoai.DataSource = dt;// gán datatable làm nguồn dữ liệu cho combo box
            cboMaLoai.DisplayMember = "TenLoai";// hiển thị tên loại lên combo box
            cboMaLoai.ValueMember = "MaLoai";// giá trị thực tế của combo box là mã loại
            cboMaLoai.SelectedIndex = -1;// không chọn mục nào mặc định
        }

        // ================= SHOW ROW =================
        private void ShowRow(int index)// hàm hiển thị dữ liệu của dòng được chọn lên các control nhập liệu
        {
            if (index < 0 || index >= dtgvData.Rows.Count) return;// kiểm tra chỉ số dòng hợp lệ 

            DataGridViewRow r = dtgvData.Rows[index];// lấy dòng dữ liệu tại chỉ số đã chọn
            if (r.IsNewRow) return;

            txtMaDU.Text = r.Cells["MaDU"].Value?.ToString() ?? "";// hiển thị mã đồ uống, nếu giá trị null thì hiển thị chuỗi rỗng
            txtTenDU.Text = r.Cells["TenDU"].Value?.ToString() ?? "";// hiển thị tên đồ uống, nếu giá trị null thì hiển thị chuỗi rỗng
            txtDonGia.Text = r.Cells["DonGia"].Value?.ToString() ?? "";// hiển thị đơn giá, nếu giá trị null thì hiển thị chuỗi rỗng    

            if (r.Cells["MaLoai"].Value != null)// kiểm tra nếu mã loại không null thì gán giá trị cho combo box
                cboMaLoai.SelectedValue = r.Cells["MaLoai"].Value;// gán mã loại cho combo box để hiển thị tên loại tương ứng

            picHinhAnh.Image = null;// xóa ảnh cũ trên PictureBox trước khi hiển thị ảnh mới
            tenHinh = r.Cells["HinhAnh"].Value?.ToString() ?? "";// lấy tên ảnh từ cột HinhAnh, nếu giá trị null thì gán chuỗi rỗng

            if (!string.IsNullOrEmpty(tenHinh))// nếu có tên ảnh thì tải và hiển thị ảnh lên PictureBox
            {
                string path = folderHinh + tenHinh;// tạo đường dẫn đầy đủ đến file ảnh
                if (File.Exists(path))// kiểm tra nếu file ảnh tồn tại thì mới tải lên để tránh lỗi 
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))// mở file ảnh bằng FileStream để tránh lỗi lock file khi hiển thị ảnh
                    {
                        picHinhAnh.Image = Image.FromStream(fs);// tải ảnh từ FileStream và hiển thị lên PictureBox
                    }
                }
            }
        }

        private void dtgvData_CellClick(object sender, DataGridViewCellEventArgs e)//   sự kiện khi người dùng click vào một ô trong datagridview
        {
            if (e.RowIndex >= 0)// kiểm tra nếu chỉ số dòng hợp lệ (không phải header hoặc dòng mới)
                ShowRow(e.RowIndex);// hiển thị dữ liệu của dòng được chọn lên các control nhập liệu
        }

        // ================= CHỌN HÌNH =================
        private void picHinhAnh_Click(object sender, EventArgs e)// sự kiện khi người dùng click vào PictureBox để chọn hình ảnh cho đồ uống
        {
            OpenFileDialog f = new OpenFileDialog();// tạo hộp thoại chọn file
            f.Filter = "Image|*.jpg;*.png";// chỉ cho phép chọn file ảnh có định dạng jpg hoặc png

            if (f.ShowDialog() == DialogResult.OK)// nếu người dùng chọn file và nhấn OK
            {
                tenHinh = Path.GetFileName(f.FileName);//   lấy tên file ảnh từ đường dẫn đầy đủ của file đã chọn
                string dest = folderHinh + tenHinh;// tạo đường dẫn đích để lưu ảnh vào thư mục luuanh với tên file đã lấy

                // Copy ảnh trước
                File.Copy(f.FileName, dest, true);// sao chép file ảnh từ đường dẫn gốc đến đường dẫn đích, nếu đã tồn tại file cùng tên thì ghi đè

                // Giải phóng ảnh cũ nếu có
                if (picHinhAnh.Image != null)// nếu PictureBox đã có ảnh trước đó thì giải phóng tài nguyên của ảnh cũ để tránh lỗi lock file khi hiển thị ảnh mới
                {
                    picHinhAnh.Image.Dispose();// giải phóng tài nguyên của ảnh cũ
                    picHinhAnh.Image = null;// xóa ảnh cũ trên PictureBox
                }

                // Load ảnh KHÔNG LOCK FILE
                using (FileStream fs = new FileStream(dest, FileMode.Open, FileAccess.Read))// mở file ảnh mới bằng FileStream để tránh lỗi lock file khi hiển thị ảnh
                {
                    picHinhAnh.Image = Image.FromStream(fs);// tải ảnh từ FileStream và hiển thị lên PictureBox
                }
            }
        }


        // ================= THÊM =================
        private void menuThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDU.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đồ uống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDU.Focus();
                return;
            }

            if (cboMaLoai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại đồ uống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaLoai.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn giá!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số lớn hơn 0!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            int kq = ConnectSQL.RunQuery(// Thêm mới một đồ uống vào bảng DoUong, sử dụng tham số để tránh lỗi SQL Injection
                "INSERT INTO DoUong (TenDU,MaLoai,DonGia,HinhAnh) VALUES (@tendu,@maloai,@dongia,@hinhanh)",// Câu lệnh SQL để thêm mới một đồ uống vào bảng DoUong, sử dụng tham số để tránh lỗi SQL Injection
                new SqlParameter("@tendu", txtTenDU.Text.Trim()),// Lấy tên đồ uống từ textbox, loại bỏ khoảng trắng đầu cuối
                new SqlParameter("@maloai", cboMaLoai.SelectedValue),// Lấy mã loại từ combo box (giá trị thực tế)
                new SqlParameter("@dongia", donGia),// Lấy đơn giá đã được kiểm tra và chuyển đổi thành decimal
                new SqlParameter("@hinhanh", tenHinh)// Lấy tên ảnh đã được lưu trong biến tenHinh, có thể là chuỗi rỗng nếu không chọn ảnh nào
            );

            if (kq > 0)
            {
                MessageBox.Show("Thêm đồ uống thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();// Tải lại dữ liệu để hiển thị đồ uống mới thêm vào datagridview
            }
        }

        // ================= SỬA =================
        private void menuSua_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE DoUong SET // Câu lệnh SQL để cập nhật thông tin của một đồ uống trong bảng DoUong, sử dụng tham số để tránh lỗi SQL Injection
                           TenDU=@tendu,
                           MaLoai=@maloai,
                           DonGia=@dongia,
                           HinhAnh=@hinhanh
                           WHERE MaDU=@madu";

            ConnectSQL.RunQuery(sql,// Thực thi câu lệnh SQL để cập nhật thông tin của một đồ uống trong bảng DoUong, sử dụng tham số để tránh lỗi SQL Injection
                new SqlParameter("@madu", txtMaDU.Text.Trim()),// Lấy mã đồ uống từ textbox, loại bỏ khoảng trắng đầu cuối
                new SqlParameter("@tendu", txtTenDU.Text.Trim()),// Lấy tên đồ uống từ textbox, loại bỏ khoảng trắng đầu cuối
                new SqlParameter("@maloai", cboMaLoai.SelectedValue),// Lấy mã loại từ combo box (giá trị thực tế)
                new SqlParameter("@dongia", decimal.Parse(txtDonGia.Text.Trim())),// Lấy đơn giá từ textbox, loại bỏ khoảng trắng đầu cuối và chuyển đổi thành decimal
                new SqlParameter("@hinhanh", tenHinh)// Lấy tên ảnh đã được lưu trong biến tenHinh, có thể là chuỗi rỗng nếu không chọn ảnh nào
            );

            MessageBox.Show("Cập nhật thành công!");
            LoadData();
        }

        // ================= XÓA =================
        private void menuXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa đồ uống này?",
                "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string sql = "DELETE FROM DoUong WHERE MaDU=@madu";
            ConnectSQL.RunQuery(sql,
                new SqlParameter("@madu", txtMaDU.Text.Trim()));// Xóa dữ liệu trong database  

            LoadData();
        }

        // ================= XÓA TRẮNG =================
        private void menuXoaTrang_Click(object sender, EventArgs e)
        {
            txtMaDU.Clear();
            txtTenDU.Clear();
            txtDonGia.Clear();
            cboMaLoai.SelectedIndex = -1;
            picHinhAnh.Image = null;
            tenHinh = "";
            dtgvData.ClearSelection();
            txtMaDU.Focus();
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

            string sql = @"SELECT * FROM DoUong
                   WHERE MaDU LIKE @key
                      OR TenDU LIKE @key";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@key", "%" + key + "%"));

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy đồ uống!");
                LoadData();
                return;
            }

            dtgvData.DataSource = dt;
        }


        private void btnXoaHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (picHinhAnh.Image != null)
            {
                picHinhAnh.Image.Dispose();
                picHinhAnh.Image = null;
            }
            tenHinh = "";
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
