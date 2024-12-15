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
    public partial class Form_Roles : Form
    {
        private Class_Roles _classRoles;
        public Form_Roles()
        {
            InitializeComponent();
            _classRoles = new Class_Roles();
            LoadRoles();
        }
        private void LoadRoles()
        {
            DataTable dt = _classRoles.LoadRoles();
            dgvRoles.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoleName.Text))
            {
                MessageBox.Show("Please enter a role name.");
                return;
            }

            string roleName = txtRoleName.Text;

            if (_classRoles.AddRole(roleName))
            {
                MessageBox.Show("Add role success.");
                LoadRoles(); // Cập nhật danh sách vai trò sau khi thêm
                txtRoleName.Clear(); // Xóa TextBox
            }
            else
            {
                MessageBox.Show("Error adding role.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select the role that needs to be edited.");
                return;
            }

            int roleID = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["RoleID"].Value);
            string newRoleName = txtRoleName.Text;

            if (string.IsNullOrEmpty(newRoleName))
            {
                MessageBox.Show("Please enter the role name.");
                return;
            }

            if (_classRoles.EditRole(roleID, newRoleName))
            {
                MessageBox.Show("Update role success.");
                LoadRoles(); // Cập nhật danh sách vai trò sau khi sửa
                txtRoleName.Clear(); // Xóa TextBox
            }
            else
            {
                MessageBox.Show("Error updating role.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select the role to delete.");
                return;
            }

            int roleID = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["RoleID"].Value);

            DialogResult result = MessageBox.Show("You definitely want to delete this role?", "Delete Role", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (_classRoles.DeleteRole(roleID))
                {
                    MessageBox.Show("Delete role successfully.");
                    LoadRoles(); // Cập nhật danh sách vai trò sau khi xóa
                }
                else
                {
                    MessageBox.Show("Error delete role.");
                }
            }
        }

        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                txtRoleName.Text = dgvRoles.SelectedRows[0].Cells["RoleName"].Value.ToString();
            }
        }
    }
}
