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
                MessageBox.Show("Vui lòng nhập tên vai trò.");
                return;
            }

            string roleName = txtRoleName.Text;

            if (_classRoles.AddRole(roleName))
            {
                MessageBox.Show("Thêm vai trò thành công.");
                LoadRoles(); // Cập nhật danh sách vai trò sau khi thêm
                txtRoleName.Clear(); // Xóa TextBox
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm vai trò.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vai trò cần sửa.");
                return;
            }

            int roleID = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["RoleID"].Value);
            string newRoleName = txtRoleName.Text;

            if (string.IsNullOrEmpty(newRoleName))
            {
                MessageBox.Show("Vui lòng nhập tên vai trò.");
                return;
            }

            if (_classRoles.EditRole(roleID, newRoleName))
            {
                MessageBox.Show("Cập nhật vai trò thành công.");
                LoadRoles(); // Cập nhật danh sách vai trò sau khi sửa
                txtRoleName.Clear(); // Xóa TextBox
            }
            else
            {
                MessageBox.Show("Lỗi khi sửa vai trò.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vai trò cần xóa.");
                return;
            }

            int roleID = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["RoleID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa vai trò này?", "Xóa vai trò", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (_classRoles.DeleteRole(roleID))
                {
                    MessageBox.Show("Xóa vai trò thành công.");
                    LoadRoles(); // Cập nhật danh sách vai trò sau khi xóa
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa vai trò.");
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
