using System;
using System.IO;
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
    public partial class Form_Product : Form
    {
        private Class_Products productManager = new Class_Products();

        public Form_Product()
        {
            InitializeComponent();
            LoadProducts();
        }
        // Load tất cả sản phẩm vào DataGridView
        private void LoadProducts()
        {
            DataTable dt = productManager.Load_Products();
            dgvProducts.DataSource = dt;
        }
        // Xóa trường nhập liệu
        private void ClearFields()
        {
            txtProductCode.Clear();
            txtProductName.Clear();
            txtSellingPrice.Clear();
            txtInventoryQuantity.Clear();
            picProductImage.Image = null;
        }

        private void Form_Product_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string productCode = txtProductCode.Text;
                string productName = txtProductName.Text;
                decimal sellingPrice = decimal.Parse(txtSellingPrice.Text);
                int inventoryQuantity = int.Parse(txtInventoryQuantity.Text);
                string imagePath = picProductImage.Text;

                var product = new Class_Products(productCode, productName, sellingPrice, inventoryQuantity, imagePath);
                productManager.Insert(product);
                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picProductImage.Image = Image.FromFile(openFileDialog.FileName);
                picProductImage.Text = openFileDialog.FileName;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string productCode = txtProductCode.Text;
                productManager.Delete(productCode);
                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string productCode = txtSearch.Text;
                DataTable dt = productManager.Search(productCode);
                dgvProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching product: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string productCode = txtProductCode.Text;
                string productName = txtProductName.Text;
                decimal sellingPrice = decimal.Parse(txtSellingPrice.Text);
                int inventoryQuantity = int.Parse(txtInventoryQuantity.Text);
                string imagePath = picProductImage.Text;

                var product = new Class_Products(productCode, productName, sellingPrice, inventoryQuantity, imagePath);
                productManager.Update(product);
                LoadProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                txtProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtSellingPrice.Text = row.Cells["SellingPrice"].Value.ToString();
                txtInventoryQuantity.Text = row.Cells["InventoryQuantity"].Value.ToString();

                string imagePath = row.Cells["ImagePath"].Value.ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picProductImage.Image = Image.FromFile(imagePath);
                    lblImagePath.Text = imagePath;
                }
                else
                {
                    picProductImage.Image = null;
                    lblImagePath.Text = "";
                }
            }
        }
    }
}
