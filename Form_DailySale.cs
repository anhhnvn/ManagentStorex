using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT1
{
    public partial class Form_DailySale : Form
    {
        // Khởi tạo đối tượng kết nối cơ sở dữ liệu
        KetNoi kn = new KetNoi();

        public Form_DailySale()
        {
            InitializeComponent();
        }

        private void Form_DailySale_Load(object sender, EventArgs e)
        {
            btnLoad.PerformClick(); // Tự động load dữ liệu khi Form mở
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int productID = int.Parse(txtProductID.Text);
                int employeeID = int.Parse(txtEmployeeID.Text);
                int quantitySold = int.Parse(txtQuantitySold.Text);
                decimal totalRevenue = decimal.Parse(txtTotalRevenue.Text);

                // Chèn vào cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("INSERT INTO DailySales (ProductID, EmployeeID, QuantitySold, TotalRevenue, SalesDate) VALUES (@ProductID, @EmployeeID, @QuantitySold, @TotalRevenue, @SalesDate)");
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);
                cmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);
                cmd.Parameters.AddWithValue("@SalesDate", dtpSalesDate.Value);

                kn.Execute(cmd); // Thực thi câu lệnh SQL

                MessageBox.Show("Add successfully!");
                btnLoad.PerformClick(); // Load lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int salesID = int.Parse(txtSalesID.Text);
                int productID = int.Parse(txtProductID.Text);
                int employeeID = int.Parse(txtEmployeeID.Text);
                int quantitySold = int.Parse(txtQuantitySold.Text);
                decimal totalRevenue = decimal.Parse(txtTotalRevenue.Text);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("UPDATE DailySales SET ProductID = @ProductID, EmployeeID = @EmployeeID, QuantitySold = @QuantitySold, TotalRevenue = @TotalRevenue, SalesDate = @SalesDate WHERE SalesID = @SalesID");
                cmd.Parameters.AddWithValue("@SalesID", salesID);
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);
                cmd.Parameters.AddWithValue("@TotalRevenue", totalRevenue);
                cmd.Parameters.AddWithValue("@SalesDate", dtpSalesDate.Value);

                kn.Execute(cmd); // Thực thi câu lệnh SQL

                MessageBox.Show("Update successfully!");
                btnLoad.PerformClick(); // Load lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int salesID = int.Parse(txtSalesID.Text);

                // Xóa dữ liệu từ cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("DELETE FROM DailySales WHERE SalesID = @SalesID");
                cmd.Parameters.AddWithValue("@SalesID", salesID);

                kn.Execute(cmd); // Thực thi câu lệnh SQL

                MessageBox.Show("Delete successfully!");
                btnLoad.PerformClick(); // Load lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM DailySales";
                dgvDailySales.DataSource = kn.Load(sql); // Lấy dữ liệu và hiển thị lên DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void dgvDailySales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDailySales.Rows[e.RowIndex];
                txtSalesID.Text = row.Cells["SalesID"].Value.ToString();
                txtProductID.Text = row.Cells["ProductID"].Value.ToString();
                txtEmployeeID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtQuantitySold.Text = row.Cells["QuantitySold"].Value.ToString();
                txtTotalRevenue.Text = row.Cells["TotalRevenue"].Value.ToString();
                dtpSalesDate.Value = Convert.ToDateTime(row.Cells["SalesDate"].Value);
            }
        }
    }
}
