using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT1
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường không được trống
            if (string.IsNullOrEmpty(txtProductCode.Text) || string.IsNullOrEmpty(txtProductName.Text) ||
                string.IsNullOrEmpty(txtsellingprice.Text) || string.IsNullOrEmpty(txtInventoryQuantity.Text))
            {
                MessageBox.Show("Please fill in all information.");
                return;
            }

            try
            {
                // Chuyển các giá trị sang kiểu số
                int sellingPrice = int.Parse(txtsellingprice.Text);
                int inventoryQuantity = int.Parse(txtInventoryQuantity.Text);

                // Tạo đối tượng sản phẩm mới
                Class_Products newProduct = new Class_Products(txtProductCode.Text, txtProductName.Text, sellingPrice, inventoryQuantity);

                // Thêm sản phẩm vào cơ sở dữ liệu
                newProduct.Insert(newProduct);

                // Làm mới DataGridView sau khi thêm sản phẩm
                LoadData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for the selling price and quantity.");
            }
        }

        private void Product_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            Class_Products cd = new Class_Products();
            dataGridView1.DataSource = cd.Load_Products();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường không được trống
            if (string.IsNullOrEmpty(txtProductCode.Text) || string.IsNullOrEmpty(txtProductName.Text) ||
                string.IsNullOrEmpty(txtsellingprice.Text) || string.IsNullOrEmpty(txtInventoryQuantity.Text))
            {
                MessageBox.Show("Please fill in all information.");
                return;
            }

            try
            {
                int sellingPrice = int.Parse(txtsellingprice.Text);
                int inventoryQuantity = int.Parse(txtInventoryQuantity.Text);

                // Cập nhật thông tin sản phẩm
                Class_Products updatedProduct = new Class_Products(txtProductCode.Text, txtProductName.Text, sellingPrice, inventoryQuantity);
                updatedProduct.Update(updatedProduct);

                // Làm mới DataGridView sau khi cập nhật
                LoadData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid entry for the selling price and quantity that exists.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu mã sản phẩm không trống
            if (string.IsNullOrEmpty(txtProductCode.Text))
            {
                MessageBox.Show("Please enter the product code to be deleted.");
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Class_Products product = new Class_Products();
                    product.Delete(txtProductCode.Text);  // Xóa sản phẩm theo mã
                    LoadData();  // Làm mới DataGridView sau khi xóa
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting product: " + ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô TextBox (txtSearch) trống
            if (string.IsNullOrEmpty(txtSearchProduct.Text.Trim()))
            {
                MessageBox.Show("Please enter the Product Code to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo một instance của class xử lý sản phẩm
                Class_Products product = new Class_Products();

                // Gọi phương thức tìm kiếm dựa trên mã sản phẩm nhập vào
                var result = product.Search(txtSearchProduct.Text.Trim());

                // Kiểm tra nếu không có kết quả
                if (result == null || result.Rows.Count == 0) // Nếu `result` là DataTable
                {
                    MessageBox.Show("No product found for the entered Product Code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null; // Xóa dữ liệu hiện tại trên DataGridView
                }
                else
                {
                    // Hiển thị kết quả tìm kiếm lên DataGridView
                    dataGridView1.DataSource = result;
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu xảy ra ngoại lệ
                MessageBox.Show("An error occurred while searching for the product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    

