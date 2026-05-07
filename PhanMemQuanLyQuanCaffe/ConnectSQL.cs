using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public static class ConnectSQL
    {
        // 🔹 CHUỖI KẾT NỐI DUY NHẤT
        public static string connectionString =
    @"Data Source=localhost;Initial Catalog=QuanLyQuanCafe;Integrated Security=True;TrustServerCertificate=True";

        // ================== SELECT ==================
        public static DataTable Load(string sql, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                // ✅ Tạo connection MỚI mỗi lần gọi, tránh xung đột static connection
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        if (parameters != null && parameters.Length > 0)
                            cmd.Parameters.AddRange(parameters);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
            }

            return dt;
        }

        // ================== INSERT / UPDATE / DELETE ==================
        public static int RunQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                // ✅ Tạo connection MỚI mỗi lần gọi
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        if (parameters != null && parameters.Length > 0)
                            cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
                return -1;
            }
        }

        // ================== CHECK EXIST ==================
        public static bool ExecuteScalarBool(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        if (parameters != null && parameters.Length > 0)
                            cmd.Parameters.AddRange(parameters);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
                return false;
            }
        }

        // ================== EXECUTE SCALAR ==================
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        if (parameters != null && parameters.Length > 0)
                            cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
                return null;
            }
        }

        // ================== GET DATATABLE ==================
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            return Load(sql, parameters);
        }
    }
}
