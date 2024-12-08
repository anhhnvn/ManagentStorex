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
    public partial class Form_MonthlyProfit : Form
    {
        // Khởi tạo đối tượng kết nối cơ sở dữ liệu
        KetNoi kn = new KetNoi();

        public Form_MonthlyProfit()
        {
            InitializeComponent();
        }

        private void Form_MonthlyProfit_Load(object sender, EventArgs e)
        {
            btnLoad.PerformClick(); // Tự động load dữ liệu khi Form mở
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int employeeID = int.Parse(txtEmployeeID.Text);
                int salesID = int.Parse(txtSalesID.Text);
                decimal totalProfit = decimal.Parse(txtTotalProfit.Text);

                // Chèn vào cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("INSERT INTO MonthlyProfit (EmployeeID, SalesID, TotalProfit, ProfitDate) VALUES (@EmployeeID, @SalesID, @TotalProfit, @ProfitDate)");
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalesID", salesID);
                cmd.Parameters.AddWithValue("@TotalProfit", totalProfit);
                cmd.Parameters.AddWithValue("@ProfitDate", dtpProfitDate.Value);

                kn.Execute(cmd); // Thực thi câu lệnh SQL

                MessageBox.Show("Thêm thành công!");
                btnLoad.PerformClick(); // Load lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int profitID = int.Parse(txtProfitID.Text);
                int employeeID = int.Parse(txtEmployeeID.Text);
                int salesID = int.Parse(txtSalesID.Text);
                decimal totalProfit = decimal.Parse(txtTotalProfit.Text);

                // Cập nhật dữ liệu vào cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("UPDATE MonthlyProfit SET EmployeeID = @EmployeeID, SalesID = @SalesID, TotalProfit = @TotalProfit, ProfitDate = @ProfitDate WHERE ProfitID = @ProfitID");
                cmd.Parameters.AddWithValue("@ProfitID", profitID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalesID", salesID);
                cmd.Parameters.AddWithValue("@TotalProfit", totalProfit);
                cmd.Parameters.AddWithValue("@ProfitDate", dtpProfitDate.Value);

                kn.Execute(cmd); // Thực thi câu lệnh SQL

                MessageBox.Show("Cập nhật thành công!");
                btnLoad.PerformClick(); // Load lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int profitID = int.Parse(txtProfitID.Text);

                // Xóa dữ liệu từ cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("DELETE FROM MonthlyProfit WHERE ProfitID = @ProfitID");
                cmd.Parameters.AddWithValue("@ProfitID", profitID);

                kn.Execute(cmd); // Thực thi câu lệnh SQL

                MessageBox.Show("Xóa thành công!");
                btnLoad.PerformClick(); // Load lại dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM MonthlyProfit";
                dgvMonthlyProfit.DataSource = kn.Load(sql); // Lấy dữ liệu và hiển thị lên DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvMonthlyProfit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMonthlyProfit.Rows[e.RowIndex];
                txtProfitID.Text = row.Cells["ProfitID"].Value.ToString();
                txtEmployeeID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtSalesID.Text = row.Cells["SalesID"].Value.ToString();
                txtTotalProfit.Text = row.Cells["TotalProfit"].Value.ToString();
                dtpProfitDate.Value = Convert.ToDateTime(row.Cells["ProfitDate"].Value);
            }
        }
    }
}
