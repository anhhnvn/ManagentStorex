using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace BT1
{
    internal class Class_Products
    {
        private string ProductName;
        private string ProductCode;
        private decimal SellingPrice;
        private int InventoryQuantity;
        private string ImagePath;

        KetNoi ob = new KetNoi();

        // Constructor mặc định
        public Class_Products() { }

        // Constructor có tham số
        public Class_Products(string productCode, string productName, decimal sellingPrice, int inventoryQuantity, string imagePath = "")
        {
            ProductCode = productCode;
            ProductName = productName;
            SellingPrice = sellingPrice;
            InventoryQuantity = inventoryQuantity;
            ImagePath = imagePath;
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

            string sql = "INSERT INTO Products (ProductCode, ProductName, SellingPrice, InventoryQuantity, ImagePath) " +
                         "VALUES (@ProductCode, @ProductName, @SellingPrice, @InventoryQuantity, @ImagePath)";

            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", ob1.ProductCode);
            cmd.Parameters.AddWithValue("@ProductName", ob1.ProductName);
            cmd.Parameters.AddWithValue("@SellingPrice", ob1.SellingPrice);
            cmd.Parameters.AddWithValue("@InventoryQuantity", ob1.InventoryQuantity);
            cmd.Parameters.AddWithValue("@ImagePath", ob1.ImagePath);

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
                         "InventoryQuantity = @InventoryQuantity, ImagePath = @ImagePath WHERE ProductCode = @ProductCode";

            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ProductCode", ob1.ProductCode);
            cmd.Parameters.AddWithValue("@ProductName", ob1.ProductName);
            cmd.Parameters.AddWithValue("@SellingPrice", ob1.SellingPrice);
            cmd.Parameters.AddWithValue("@InventoryQuantity", ob1.InventoryQuantity);
            cmd.Parameters.AddWithValue("@ImagePath", ob1.ImagePath);

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

        // Lưu hình ảnh vào cơ sở dữ liệu
        public void SaveImage(string productCode, string imagePath)
        {
            string sql = "UPDATE Products SET ImagePath = @ImagePath WHERE ProductCode = @ProductCode";
            SqlCommand cmd = new SqlCommand(sql, ob.conn);
            cmd.Parameters.AddWithValue("@ImagePath", imagePath);
            cmd.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                ob.Execute(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving image path: " + ex.Message);
            }
        }
    }
}




