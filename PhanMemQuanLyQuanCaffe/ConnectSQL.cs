using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhanMemQuanLyQuanCaffe
{
    public static class ConnectSQL
    {
        // Chuỗi kết nối SQL
        public static string connectionString =
 @"Data Source=ASUS\SQLEXPRESS01;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        private static SqlConnection cnn = new SqlConnection(connectionString);

        // ================== TEST CONNECTION ==================
        public static void TestConnection()
        {
            try
            {
                cnn.Open();
                MessageBox.Show("Kết nối SQL thành công ✅");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối:\n" + ex.Message);
            }
        }

        // ================== OPEN CONNECTION ==================
        private static void OpenConnection()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
        }

        // ================== CLOSE CONNECTION ==================
        private static void CloseConnection()
        {
            if (cnn.State == ConnectionState.Open)
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
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối:\n" + ex.ToString());
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        // ================== INSERT UPDATE DELETE ==================
        public static int RunQuery(string sql, params SqlParameter[] parameters)
        {
            int result = 0;

            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    result = cmd.ExecuteNonQuery();
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

        // ================== CHECK LOGIN ==================
        public static bool ExecuteScalarBool(string sql, params SqlParameter[] parameters)
        {
            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    if (parameters != null)
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
                    if (parameters != null)
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
    }
}