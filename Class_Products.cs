using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BT1
{
    internal class Class_Products
    {
        private string ProductName;
        private string ProductCode;
        private int SellingPrice;
        private int InventoryQuantity;

        KetNoi ob = new KetNoi();

        // Constructor mặc định
        public Class_Products() { }

        // Constructor có tham số
        public Class_Products(string productCode, string productName, int sellingPrice, int inventoryQuantity)
        {
            ProductCode = productCode;
            ProductName = productName;
            SellingPrice = sellingPrice;
            InventoryQuantity = inventoryQuantity;
        }

        // Load tất cả sản phẩm từ cơ sở dữ liệu
        public DataTable Load_Products()
        {
            string sql = "SELECT * FROM Products";
            return ob.Load(sql);
        }

        // Kiểm tra mã sản phẩm đã tồn tại hay chưa
        public bool CheckProductCodeExists(string productCode)
        {
            string sql = "SELECT COUNT(*) FROM Products WHERE ProductCode = @ProductCode";
            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", productCode);
            ob.conn.Open();
            int count = (int)cmd.ExecuteScalar();
            ob.conn.Close();
            return count > 0;
        }

        // Thêm sản phẩm mới vào cơ sở dữ liệu
        public void Insert(Class_Products ob1)
        {
            if (CheckProductCodeExists(ob1.ProductCode))
            {
                MessageBox.Show("Product code already exists.");
                return;
            }

            string sql = "INSERT INTO Products (ProductCode, ProductName, SellingPrice, InventoryQuantity) " +
                         "VALUES (@ProductCode, @ProductName, @SellingPrice, @InventoryQuantity)";

            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", ob1.ProductCode);
            cmd.Parameters.AddWithValue("@ProductName", ob1.ProductName);
            cmd.Parameters.AddWithValue("@SellingPrice", ob1.SellingPrice);
            cmd.Parameters.AddWithValue("@InventoryQuantity", ob1.InventoryQuantity);

            try
            {
                ob.Execute(cmd);
                MessageBox.Show("The product has been added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while adding product: " + ex.Message);
            }
        }

        // Cập nhật thông tin sản phẩm
        public void Update(Class_Products ob1)
        {
            string sql = "UPDATE Products SET ProductName = @ProductName, SellingPrice = @SellingPrice, " +
                         "InventoryQuantity = @InventoryQuantity WHERE ProductCode = @ProductCode";

            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", ob1.ProductCode);
            cmd.Parameters.AddWithValue("@ProductName", ob1.ProductName);
            cmd.Parameters.AddWithValue("@SellingPrice", ob1.SellingPrice);
            cmd.Parameters.AddWithValue("@InventoryQuantity", ob1.InventoryQuantity);

            try
            {
                ob.Execute(cmd);
                MessageBox.Show("The product has been updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
        }

        // Xóa sản phẩm khỏi cơ sở dữ liệu
        public void Delete(string productCode)
        {
            string sql = "DELETE FROM Products WHERE ProductCode = @ProductCode";

            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                ob.Execute(cmd);
                MessageBox.Show("The product has been successfully deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting product: " + ex.Message);
            }
        }

        // Tìm kiếm sản phẩm theo mã
        public DataTable Search(string productCode)
        {
            string sql = "SELECT * FROM Products WHERE ProductCode LIKE '%' + @ProductCode + '%'";
            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", productCode);
            return ob.Load(sql); // Dùng phương thức Load để lấy dữ liệu
        }
    }
}



