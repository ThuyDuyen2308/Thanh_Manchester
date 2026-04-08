using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmMenuTheoNgay : Form
    {
        string folderHinh = Application.StartupPath + @"\luuanh\";

        public frmMenuTheoNgay()
        {
            InitializeComponent();
        }

        private void frmMenuTheoNgay_Load(object sender, EventArgs e)
        {
            clbDoUong.CheckOnClick = true;  // ← THÊM DÒNG NÀY
            dtpNgay.Value = DateTime.Today;
            LoadDoUongVaoList();
            XemMenu();
        }

        // Load tất cả đồ uống vào CheckedListBox
        private void LoadDoUongVaoList()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaDU, TenDU FROM DoUong");
            clbDoUong.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                clbDoUong.Items.Add(new ComboboxItem
                {
                    Text = row["TenDU"].ToString(),
                    Value = row["MaDU"].ToString()
                });
            }
        }

        // Xem menu theo ngày đã chọn
        private void XemMenu()
        {
            string sql = @"
                SELECT du.MaDU      AS [Mã Đồ Uống],
                       du.TenDU     AS [Tên Đồ Uống],
                       du.DonGia    AS [Giá Tiền],
                       du.HinhAnh
                FROM MenuTheoNgay m
                INNER JOIN DoUong du ON m.MaDU = du.MaDU
                WHERE m.Ngay = @Ngay
                ORDER BY du.TenDU";

            DataTable dt = ConnectSQL.Load(sql,
                new SqlParameter("@Ngay", dtpNgay.Value.Date));

            // Setup grid
            dtgvMenu.AutoGenerateColumns = false;
            dtgvMenu.Columns.Clear();
            dtgvMenu.RowTemplate.Height = 90;

            // Cột hình ảnh
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "colHinh";
            imgCol.HeaderText = "Hình Ảnh";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imgCol.Width = 120;
            dtgvMenu.Columns.Add(imgCol);

            dtgvMenu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colMaDU",
                HeaderText = "Mã Đồ Uống",
                DataPropertyName = "Mã Đồ Uống",
                Width = 100
            });
            dtgvMenu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colTenDU",
                HeaderText = "Tên Đồ Uống",
                DataPropertyName = "Tên Đồ Uống",
                Width = 150
            });
            dtgvMenu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colGia",
                HeaderText = "Giá Tiền",
                DataPropertyName = "Giá Tiền",
                Width = 80,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dtgvMenu.DataSource = dt;
            dtgvMenu.AllowUserToAddRows = false;

            // Load hình ảnh
            foreach (DataGridViewRow row in dtgvMenu.Rows)
            {
                if (row.IsNewRow) continue;
                string tenHinh = (row.DataBoundItem as DataRowView)?["HinhAnh"]?.ToString();
                if (!string.IsNullOrEmpty(tenHinh))
                {
                    string path = folderHinh + tenHinh;
                    if (File.Exists(path))
                        row.Cells["colHinh"].Value = Image.FromFile(path);
                }
            }

            // Tích vào CheckedListBox những món đã có trong menu ngày này
            for (int i = 0; i < clbDoUong.Items.Count; i++)
            {
                string maDU = ((ComboboxItem)clbDoUong.Items[i]).Value;
                string checkSql = "SELECT COUNT(*) FROM MenuTheoNgay WHERE Ngay=@Ngay AND MaDU=@MaDU";
                bool coMon = ConnectSQL.ExecuteScalarBool(checkSql,
                    new SqlParameter("@Ngay", dtpNgay.Value.Date),
                    new SqlParameter("@MaDU", maDU));
                clbDoUong.SetItemChecked(i, coMon);
            }
        }

        // Lưu menu theo ngày
        private void btnLuu_Click(object sender, EventArgs e)
        {
            DateTime ngay = dtpNgay.Value.Date;

            // Xóa menu cũ của ngày này
            ConnectSQL.RunQuery("DELETE FROM MenuTheoNgay WHERE Ngay=@Ngay",
                new SqlParameter("@Ngay", ngay));

            // Thêm các món được tích chọn
            int count = 0;
            foreach (int i in clbDoUong.CheckedIndices)
            {
                string maDU = ((ComboboxItem)clbDoUong.Items[i]).Value;
                ConnectSQL.RunQuery(
                    "INSERT INTO MenuTheoNgay (Ngay, MaDU) VALUES (@Ngay, @MaDU)",
                    new SqlParameter("@Ngay", ngay),
                    new SqlParameter("@MaDU", maDU));
                count++;
            }

            MessageBox.Show($"Đã lưu menu {count} món cho ngày {ngay:dd/MM/yyyy}!");
            XemMenu();
        }

        // Xem menu khi đổi ngày
        private void btnXem_Click(object sender, EventArgs e)
        {
            XemMenu();
        }

        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {
            XemMenu();
        }
    }

    // Helper class cho CheckedListBox
    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public override string ToString() => Text;
    }
}