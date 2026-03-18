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
            @"Data Source=LAPTOP-702JQGV7\THANH;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        private static SqlConnection cnn;

        // ================== CONNECTION ==================
        private static SqlConnection GetConnection()
        {
            if (cnn == null)
                cnn = new SqlConnection(connectionString);

            return cnn;
        }

        private static void OpenConnection()
        {
            if (GetConnection().State == ConnectionState.Closed)
                GetConnection().Open();
        }

        private static void CloseConnection()
        {
            if (cnn != null && cnn.State == ConnectionState.Open)
                cnn.Close();
        }

        // ================== SELECT ==================
        public static DataTable Load(string sql, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        // ================== INSERT / UPDATE / DELETE ==================
        public static int RunQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }

        // ================== CHECK EXIST ==================
        public static bool ExecuteScalarBool(string sql, params SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        // ================== EXECUTE SCALAR ==================
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            object result = null;

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    result = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL:\n" + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        // ================== GET DATATABLE (AN TOÀN) ==================
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            return Load(sql, parameters);
        }
    }
}
