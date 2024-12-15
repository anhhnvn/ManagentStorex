using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    internal class Class_Users
    {
        private readonly KetNoi kn = new KetNoi();

        // Kiểm tra xem Username đã tồn tại chưa
        public bool CheckUsernameExists(string username)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@Username", username);
            kn.OpenConnection();
            int count = (int)cmd.ExecuteScalar();
            kn.CloseConnection();
            return count > 0;
        }

        // Thêm người dùng
        public void Add_User(string username, string passwordHash, int roleId)
        {
            string sql = "INSERT INTO Users (Username, PasswordHash, RoleID, FirstLogin) VALUES (@Username, @PasswordHash, @RoleID, 1)";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@RoleID", roleId);
            kn.Execute(cmd);
        }

        public void Update_User(int userId, string username, string password, int roleId)
        {
            string sql = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, RoleID = @RoleID WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", password); // Lưu mật khẩu trực tiếp
            cmd.Parameters.AddWithValue("@RoleID", roleId);
            cmd.Parameters.AddWithValue("@UserID", userId);
            kn.Execute(cmd);  // Gọi phương thức Execute để thực thi câu lệnh
        }



        // Xóa người dùng
        public void Delete_User(int userId)
        {
            string sql = "DELETE FROM Users WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@UserID", userId);
            kn.Execute(cmd);
        }

        // Load danh sách người dùng
        public DataTable Load_Users()
        {
            string sql = "SELECT * FROM Users";
            return kn.Load(sql);
        }

        // Tìm kiếm người dùng
        public DataTable Search_User(string username)
        {
            string sql = "SELECT * FROM Users WHERE Username LIKE @Username";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@Username", "%" + username + "%");
            return kn.Load(cmd);
        }
    }
}

