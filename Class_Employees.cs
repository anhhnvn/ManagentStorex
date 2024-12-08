using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    internal class Class_Employees
    {
        private readonly KetNoi kn = new KetNoi();

        // Load danh sách nhân viên
        public DataTable Load_Employees()
        {
            string sql = "SELECT * FROM Employees";
            return kn.Load(sql);
        }

        // Thêm nhân viên mới
        public void Add_Employee(string code, string name, string position, string authority, int userId)
        {
            string sql = "INSERT INTO Employees (EmployeeCode, EmployeeName, Position, Authority, UserID) " +
                         "VALUES (@code, @name, @position, @authority, @userId)";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@position", position);
            cmd.Parameters.AddWithValue("@authority", authority);
            cmd.Parameters.AddWithValue("@userId", userId);
            kn.Execute(cmd);
        }

        // Cập nhật thông tin nhân viên
        public void Update_Employee(int id, string code, string name, string position, string authority, int userId)
        {
            string sql = "UPDATE Employees SET EmployeeCode = @code, EmployeeName = @name, " +
                         "Position = @position, Authority = @authority, UserID = @userId WHERE EmployeeID = @id";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@position", position);
            cmd.Parameters.AddWithValue("@authority", authority);
            cmd.Parameters.AddWithValue("@userId", userId);
            kn.Execute(cmd);
        }

        // Xóa nhân viên
        public void Delete_Employee(int id)
        {
            string sql = "DELETE FROM Employees WHERE EmployeeID = @id";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@id", id);
            kn.Execute(cmd);
        }

        // Tìm kiếm nhân viên theo mã
        public DataTable Search_Employee(string code)
        {
            string sql = "SELECT * FROM Employees WHERE EmployeeCode LIKE '%' + @code + '%'";
            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@code", code);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
    }
}

