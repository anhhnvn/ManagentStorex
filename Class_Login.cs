using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BT1
{
    public class Class_Login
    {
        private KetNoi ketNoi = new KetNoi();

        // Hàm kiểm tra đăng nhập (Không băm mật khẩu)
        public bool CheckLogin(string username, string password)
        {
            try
            {
                // SQL để kiểm tra thông tin đăng nhập (Không sử dụng băm)
                string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", password);  // Sử dụng mật khẩu thô

                // Thực thi câu lệnh và kiểm tra kết quả
                DataTable dt = ketNoi.Load(cmd);
                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    return true; // Đăng nhập thành công
                }
                return false; // Đăng nhập thất bại
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra đăng nhập: " + ex.Message);
                return false;
            }
        }

        // Hàm đăng ký người dùng (Lưu mật khẩu thô vào cơ sở dữ liệu)
        public bool RegisterUser(string username, string password)
        {
            try
            {
                // Câu lệnh SQL để đăng ký người dùng (Lưu mật khẩu thô)
                string sql = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", password); // Lưu mật khẩu thô

                ketNoi.Execute(cmd); // Thực thi câu lệnh
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký người dùng: " + ex.Message);
                return false;
            }
        }
    }
}
