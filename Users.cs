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
    public partial class Users : Form
    {
        private readonly KetNoi kn = new KetNoi();
        private readonly Class_Users users = new Class_Users();

        public Users()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                int roleId = int.Parse(txtRoleID.Text.Trim());

                // Kiểm tra xem Username đã tồn tại chưa
                if (users.CheckUsernameExists(username))
                {
                    MessageBox.Show("Username đã tồn tại. Vui lòng chọn Username khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo mật khẩu mặc định cho người dùng mới
                string passwordHash = "defaultPassword"; // Bạn có thể thay đổi mật khẩu này

                // Thêm người dùng mới
                users.Add_User(username, passwordHash, roleId);
                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            dataGridView3.DataSource = users.Load_Users(); // Hiển thị danh sách người dùng
        }
        private void ClearInputFields()
        {
            txtUserID.Clear();
            txtUsername.Clear();
            txtRoleID.Clear();
            txtSearchUsername.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(txtUserID.Text.Trim());
                string username = txtUsername.Text.Trim();
                int roleId = int.Parse(txtRoleID.Text.Trim());

                // Tạo mật khẩu mới cho người dùng
                string passwordHash = "newPassword"; // Bạn có thể thay đổi mật khẩu này

                // Cập nhật thông tin người dùng
                users.Update_User(userId, username, passwordHash, roleId);
                MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string username = txtSearchUsername.Text.Trim();
            dataGridView3.DataSource = users.Search_User(username); // Tìm kiếm người dùng theo Username
        }

        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(txtUserID.Text.Trim());

                // Xóa người dùng
                users.Delete_User(userId);
                MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                txtUserID.Text = row.Cells["UserID"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtRoleID.Text = row.Cells["RoleID"].Value.ToString();
            }
        }
    }
}
