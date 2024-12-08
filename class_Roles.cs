using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    public class Class_Roles
    {
        private KetNoi _ketNoi;

        public Class_Roles()
        {
            _ketNoi = new KetNoi();
        }

        // Hàm thêm vai trò mới
        public bool AddRole(string roleName)
        {
            string sql = "INSERT INTO Roles (RoleName) VALUES (@RoleName)";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@RoleName", roleName);

            try
            {
                _ketNoi.Execute(cmd);
                return true;
            }
            catch (Exception ex)
            {
                // Lỗi nếu có
                Console.WriteLine($"Lỗi khi thêm vai trò: {ex.Message}");
                return false;
            }
        }

        // Hàm sửa vai trò
        public bool EditRole(int roleID, string roleName)
        {
            string sql = "UPDATE Roles SET RoleName = @RoleName WHERE RoleID = @RoleID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@RoleName", roleName);
            cmd.Parameters.AddWithValue("@RoleID", roleID);

            try
            {
                _ketNoi.Execute(cmd);
                return true;
            }
            catch (Exception ex)
            {
                // Lỗi nếu có
                Console.WriteLine($"Lỗi khi sửa vai trò: {ex.Message}");
                return false;
            }
        }

        // Hàm xóa vai trò
        public bool DeleteRole(int roleID)
        {
            string sql = "DELETE FROM Roles WHERE RoleID = @RoleID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@RoleID", roleID);

            try
            {
                _ketNoi.Execute(cmd);
                return true;
            }
            catch (Exception ex)
            {
                // Lỗi nếu có
                Console.WriteLine($"Lỗi khi xóa vai trò: {ex.Message}");
                return false;
            }
        }

        // Hàm tải danh sách vai trò
        public DataTable LoadRoles()
        {
            string sql = "SELECT RoleID, RoleName FROM Roles";
            return _ketNoi.Load(sql);
        }
    }
}

