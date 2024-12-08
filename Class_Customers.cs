using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BT1
{
    public class Class_Customers
    {
        private KetNoi ketNoi = new KetNoi();

        // Lấy tất cả khách hàng
        public DataTable GetAllCustomers()
        {
            string sql = "SELECT * FROM Customers";
            return ketNoi.Load(sql);
        }

        // Thêm khách hàng
        public void AddCustomer(string customerCode, string customerName, string phoneNumber, string address)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (CustomerCode, CustomerName, PhoneNumber, Address) VALUES (@CustomerCode, @CustomerName, @PhoneNumber, @Address)");
                cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Address", address);

                ketNoi.Execute(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
            }
        }

        // Cập nhật khách hàng
        public void UpdateCustomer(int customerId, string customerCode, string customerName, string phoneNumber, string address)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Customers SET CustomerCode = @CustomerCode, CustomerName = @CustomerName, PhoneNumber = @PhoneNumber, Address = @Address WHERE CustomerID = @CustomerID");
                cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                ketNoi.Execute(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message);
            }
        }

        // Xóa khách hàng
        public void DeleteCustomer(int customerId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @CustomerID");
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                ketNoi.Execute(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
            }
        }

        // Tìm kiếm khách hàng theo tên
        public DataTable SearchCustomerByName(string customerName)
        {
            string sql = "SELECT * FROM Customers WHERE CustomerName LIKE '%' + @CustomerName + '%'";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@CustomerName", customerName);
            return ketNoi.Load(cmd);
        }
    }
}
