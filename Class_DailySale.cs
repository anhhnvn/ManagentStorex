using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    internal class DailySales
    {
        private KetNoi kn = new KetNoi();

        // Thêm dữ liệu vào bảng DailySales
        public void Insert(int productID, int employeeID, int quantitySold, decimal totalRevenue)
        {
            string query = @"INSERT INTO DailySales (ProductID, EmployeeID, SalesDate, QuantitySold, TotalRevenue) 
                             VALUES (@ProductID, @EmployeeID, GETDATE(), @QuantitySold, @TotalRevenue)";
            SqlCommand cmd = new SqlCommand(query, kn.conn);
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);
            cmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);

            kn.Execute(cmd);
        }

        // Sửa dữ liệu trong bảng DailySales
        public void Update(int salesID, int productID, int employeeID, int quantitySold, decimal totalRevenue)
        {
            string query = @"UPDATE DailySales 
                             SET ProductID = @ProductID, EmployeeID = @EmployeeID, QuantitySold = @QuantitySold, TotalRevenue = @TotalRevenue
                             WHERE SalesID = @SalesID";
            SqlCommand cmd = new SqlCommand(query, kn.conn);
            cmd.Parameters.AddWithValue("@SalesID", salesID);
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);
            cmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);

            kn.Execute(cmd);
        }

        // Xóa dữ liệu trong bảng DailySales
        public void Delete(int salesID)
        {
            string query = @"DELETE FROM DailySales WHERE SalesID = @SalesID";
            SqlCommand cmd = new SqlCommand(query, kn.conn);
            cmd.Parameters.AddWithValue("@SalesID", salesID);

            kn.Execute(cmd);
        }

        // Load toàn bộ dữ liệu
        public DataTable LoadAll()
        {
            string query = "SELECT * FROM DailySales";
            return kn.Load(query);
        }
    }
}
