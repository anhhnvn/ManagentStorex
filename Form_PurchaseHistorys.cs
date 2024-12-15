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
    public partial class Form_PurchaseHistorys : Form
    {
        Class_PurchaseHistory purchaseHistory = new Class_PurchaseHistory();
        public Form_PurchaseHistorys()
        {
            InitializeComponent();
        }
        // Hàm tải lịch sử giao dịch của khách hàng
        private void LoadCustomerHistory(int customerID)
        {
            DataTable dt = purchaseHistory.GetPurchaseHistoryByCustomer(customerID);
            dgvPurchaseHistory.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các TextBox
                int customerID = int.Parse(txtCustomerID.Text);
                int productID = int.Parse(txtProductID.Text);
                int employeeID = int.Parse(txtEmployeeID.Text);
                int quantity = int.Parse(txtQuantity.Text);
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text);

                // Thêm giao dịch mới vào cơ sở dữ liệu
                purchaseHistory.AddPurchaseHistory(customerID, productID, employeeID, quantity, totalPrice);
                MessageBox.Show("The transaction has been added successfully.!");

                // Tải lại lịch sử giao dịch của khách hàng
                LoadCustomerHistory(customerID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Form_PurchaseHistorys_Load(object sender, EventArgs e)
        {
            LoadCustomerHistory(1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int customerID = int.Parse(txtCustomerID.Text);
            LoadCustomerHistory(customerID);
        }
    }
}
