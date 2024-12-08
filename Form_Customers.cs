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
    public partial class Form_Customers : Form
    {
        private Class_Customers customers;

        public Form_Customers()
        {
            InitializeComponent();
            customers = new Class_Customers();
            LoadCustomerData();
        }
        private void LoadCustomerData()
        {
            try
            {
                DataTable dt = customers.GetAllCustomers();
                dgvCustomers.DataSource = dt;  // Hiển thị danh sách khách hàng lên DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tải dữ liệu khách hàng: " + ex.Message);
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerCode = txtCustomerCode.Text;
            string customerName = txtCusName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string address = txtAddress.Text;

            // Kiểm tra các trường dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(customerCode) || string.IsNullOrWhiteSpace(customerName) ||
                string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khách hàng.");
                return;
            }

            customers.AddCustomer(customerCode, customerName, phoneNumber, address);
            LoadCustomerData();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                txtCustomerCode.Text = dgvCustomers.SelectedRows[0].Cells["CustomerCode"].Value.ToString();
                txtCusName.Text = dgvCustomers.SelectedRows[0].Cells["CustomerName"].Value.ToString();
                txtPhoneNumber.Text = dgvCustomers.SelectedRows[0].Cells["PhoneNumber"].Value.ToString();
                txtAddress.Text = dgvCustomers.SelectedRows[0].Cells["Address"].Value.ToString();
                cmbUserID.SelectedItem = dgvCustomers.SelectedRows[0].Cells["UserID"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int customerId;
            if (int.TryParse(txtCustomerID.Text, out customerId))
            {
                string customerCode = txtCustomerCode.Text;
                string customerName = txtCusName.Text;
                string phoneNumber = txtPhoneNumber.Text;
                string address = txtAddress.Text;

                customers.UpdateCustomer(customerId, customerCode, customerName, phoneNumber, address);
                LoadCustomerData();  // Tải lại danh sách khách hàng sau khi cập nhật
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để cập nhật.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int customerId;
            if (int.TryParse(txtCustomerID.Text, out customerId))
            {
                customers.DeleteCustomer(customerId);
                LoadCustomerData();  // Tải lại danh sách khách hàng sau khi xóa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.");
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string customerName = txtSearchName.Text;

            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng cần tìm.");
                return;
            }

            DataTable dt = customers.SearchCustomerByName(customerName);
            dgvCustomers.DataSource = dt;
        }
    }

    }
    
