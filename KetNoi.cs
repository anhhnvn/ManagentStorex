using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    internal class KetNoi
    {
        public SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-M3420N9;Initial Catalog=FoodHeavenStorer;Integrated Security=True;");

        // Mở kết nối
        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        // Đóng kết nối
        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        // Thực thi câu lệnh SQL
        public void Execute(SqlCommand cmd)
        {
            try
            {
                OpenConnection();
                cmd.Connection = conn; // Gắn kết nối vào SqlCommand
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi câu lệnh SQL: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Load dữ liệu từ cơ sở dữ liệu
        public DataTable Load(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                OpenConnection();
                SqlCommand comSelect = new SqlCommand(sql, conn);
                SqlDataAdapter ad = new SqlDataAdapter(comSelect);
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Load dữ liệu từ cơ sở dữ liệu với câu lệnh SQL và các tham số
        public DataTable Load(SqlCommand cmd)
        {
            try
            {
                DataTable dt = new DataTable();
                cmd.Connection = conn; // Gắn kết nối vào SqlCommand
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải dữ liệu với tham số: " + ex.Message);
            }
        }
    }
}




