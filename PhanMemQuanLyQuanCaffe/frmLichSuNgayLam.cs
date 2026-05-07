using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public partial class frmLichSuNgayLam : Form
    {
        public frmLichSuNgayLam()
        {
            InitializeComponent();
        }

        private void frmLichSuNgayLam_Load(object sender, EventArgs e)
        {
            dtpTu.Value = DateTime.Today.AddDays(-30);
            dtpDen.Value = DateTime.Today;
            LoadNhanVien();
            XemLichSu();
        }

        private void LoadNhanVien()
        {
            DataTable dt = ConnectSQL.Load("SELECT MaNV, TenNV FROM NhanVien ORDER BY TenNV");
            cboNhanVien.Items.Clear();
            cboNhanVien.Items.Add("-- Tất cả nhân viên --");

            foreach (DataRow r in dt.Rows)
                cboNhanVien.Items.Add(new ComboboxItem
                {
                    Text = r["TenNV"].ToString(),
                    Value = r["MaNV"].ToString()
                });

            cboNhanVien.SelectedIndex = 0;
        }

        private void XemLichSu()
        {
            string sqlWhere = @"
                WHERE CAST(c.GioVao AS DATE) BETWEEN @TuNgay AND @DenNgay";

            // Nếu chọn nhân viên cụ thể
            bool locNV = cboNhanVien.SelectedIndex > 0 &&
                         cboNhanVien.SelectedItem is ComboboxItem;

            if (locNV)
                sqlWhere += " AND c.MaNV = @MaNV";

            string sql = $@"
                SELECT
                    nv.MaNV                             AS [Mã NV],
                    nv.TenNV                            AS [Tên Nhân Viên],
                    CAST(c.GioVao AS DATE)              AS [Ngày Làm],
                    CONVERT(VARCHAR,c.GioVao,108)       AS [Giờ Vào],
                    CASE WHEN c.GioRa IS NULL
                         THEN N'Đang làm'
                         ELSE CONVERT(VARCHAR,c.GioRa,108)
                    END                                 AS [Giờ Ra],
                    CASE WHEN c.GioRa IS NULL
                         THEN N'Đang làm'
                         ELSE CAST(
                              DATEDIFF(MINUTE,c.GioVao,c.GioRa)/60
                              AS VARCHAR) + N' giờ '
                              + CAST(
                              DATEDIFF(MINUTE,c.GioVao,c.GioRa)%60
                              AS VARCHAR) + N' phút'
                    END                                 AS [Thời Gian],
                    CASE c.TrangThai
                         WHEN 0 THEN N'Đang làm việc'
                         ELSE N'Đã kết thúc'
                    END                                 AS [Trạng Thái]
                FROM CaLamViec c
                INNER JOIN NhanVien nv ON c.MaNV = nv.MaNV
                {sqlWhere}
                ORDER BY c.GioVao DESC";

            SqlParameter[] prms;

            if (locNV)
            {
                string maNV = ((ComboboxItem)cboNhanVien.SelectedItem).Value;
                prms = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", dtpTu.Value.Date),
                    new SqlParameter("@DenNgay", dtpDen.Value.Date),
                    new SqlParameter("@MaNV", maNV)
                };
            }
            else
            {
                prms = new SqlParameter[]
                {
                    new SqlParameter("@TuNgay", dtpTu.Value.Date),
                    new SqlParameter("@DenNgay", dtpDen.Value.Date)
                };
            }

            DataTable dt = ConnectSQL.Load(sql, prms);
            dtgvLichSu.DataSource = dt;
            FormatGrid();

            // Tính tổng ca và tổng giờ
            int tongCa = dt.Rows.Count;
            int tongPhut = 0;

            // Tính lại từ DB để chính xác
            string sqlTong = $@"
                SELECT ISNULL(SUM(DATEDIFF(MINUTE,GioVao,GioRa)),0)
                FROM CaLamViec c
                INNER JOIN NhanVien nv ON c.MaNV = nv.MaNV
                {sqlWhere.Replace("c.GioVao AS DATE", "GioVao AS DATE")}
                AND GioRa IS NOT NULL";

            object ketQua = locNV
                ? ConnectSQL.ExecuteScalar(sqlTong,
                    new SqlParameter("@TuNgay", dtpTu.Value.Date),
                    new SqlParameter("@DenNgay", dtpDen.Value.Date),
                    new SqlParameter("@MaNV", ((ComboboxItem)cboNhanVien.SelectedItem).Value))
                : ConnectSQL.ExecuteScalar(sqlTong,
                    new SqlParameter("@TuNgay", dtpTu.Value.Date),
                    new SqlParameter("@DenNgay", dtpDen.Value.Date));

            if (ketQua != null)
                tongPhut = Convert.ToInt32(ketQua);

            int gio = tongPhut / 60;
            int phut = tongPhut % 60;

            lblTongCa.Text = $"Tổng ca: {tongCa}  |  Tổng giờ làm: {gio} giờ {phut} phút";
        }

        private void FormatGrid()
        {
            dtgvLichSu.AllowUserToAddRows = false;
            dtgvLichSu.ReadOnly = true;
            dtgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvLichSu.RowHeadersVisible = false;
            dtgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tô màu dòng "Đang làm việc"
            foreach (DataGridViewRow row in dtgvLichSu.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Trạng Thái"].Value?.ToString() == "Đang làm việc")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(212, 239, 223);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(39, 174, 96);
                }
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dtpTu.Value > dtpDen.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                return;
            }
            XemLichSu();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void frmLichSuNgayLam_Load_1(object sender, EventArgs e)
        {

        }
    }
}