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
    public partial class Employees : Form
    {
        private readonly Class_Employees employees = new Class_Employees();
        public Employees()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtCode.Text.Trim();
                string name = txtName.Text.Trim();
                string position = txtPosition.Text.Trim();
                string authority = txtAuthority.Text.Trim();
                int userId = int.Parse(txtUserID.Text.Trim());

                employees.Add_Employee(code, name, position, authority, userId);

                MessageBox.Show("Add employee succesfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployees();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                dataGridView2.DataSource = employees.Load_Employees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtCode.Clear();
            txtName.Clear();
            txtPosition.Clear();
            txtAuthority.Clear();
            txtUserID.Clear();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCode.Text = dataGridView2.Rows[e.RowIndex].Cells["EmployeeCode"].Value.ToString();
                txtName.Text = dataGridView2.Rows[e.RowIndex].Cells["EmployeeName"].Value.ToString();
                txtPosition.Text = dataGridView2.Rows[e.RowIndex].Cells["Position"].Value.ToString();
                txtAuthority.Text = dataGridView2.Rows[e.RowIndex].Cells["Authority"].Value.ToString();
                txtUserID.Text = dataGridView2.Rows[e.RowIndex].Cells["UserID"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dataGridView2.CurrentRow.Cells["EmployeeID"].Value.ToString());
                string code = txtCode.Text.Trim();
                string name = txtName.Text.Trim();
                string position = txtPosition.Text.Trim();
                string authority = txtAuthority.Text.Trim();
                int userId = int.Parse(txtUserID.Text.Trim());

                employees.Update_Employee(id, code, name, position, authority, userId);

                MessageBox.Show("Update information employee successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployees();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dataGridView2.CurrentRow.Cells["EmployeeID"].Value.ToString());

                DialogResult result = MessageBox.Show("Are you sure you want to delete this role??", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    employees.Delete_Employee(id);
                    MessageBox.Show("Delete employee successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployees();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting employee: " + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtSearch.Text.Trim();
                dataGridView2.DataSource = employees.Search_Employee(code);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

