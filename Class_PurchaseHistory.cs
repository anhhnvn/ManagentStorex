using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    public class Class_PurchaseHistory
    {
        KetNoi kn = new KetNoi();

        // Thêm giao dịch mua hàng vào cơ sở dữ liệu
        public void AddPurchaseHistory(int customerID, int productID, int employeeID, int quantity, decimal totalPrice)
        {
            string query = "INSERT INTO PurchaseHistorys (CustomerID, ProductID, EmployeeID, Quantity, TotalPrice) " +
                           "VALUES (@CustomerID, @ProductID, @EmployeeID, @Quantity, @TotalPrice)";
            SqlCommand cmd = new SqlCommand(query, kn.conn);
            cmd.Parameters.AddWithValue("@CustomerID", customerID);
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

            kn.Execute(cmd); // Thực thi câu lệnh SQL
        }

        // Lấy lịch sử giao dịch của khách hàng
        public DataTable GetPurchaseHistoryByCustomer(int customerID)
        {
            string query = @"
                SELECT ph.PurchaseID, ph.PurchaseDate, p.ProductName, ph.Quantity, ph.TotalPrice, e.EmployeeName
                FROM PurchaseHistorys ph
                JOIN Products p ON ph.ProductID = p.ProductID
                JOIN Employees e ON ph.EmployeeID = e.EmployeeID
                WHERE ph.CustomerID = @CustomerID
                ORDER BY ph.PurchaseDate DESC"; // Sắp xếp theo PurchaseDate (mới nhất lên trước)

            SqlCommand cmd = new SqlCommand(query, kn.conn);
            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            return kn.Load(cmd); // Trả về DataTable chứa lịch sử mua hàng
        }
    }
}

