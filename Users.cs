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
                    MessageBox.Show("already exists, please select another Username..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo mật khẩu mặc định cho người dùng mới
                string passwordHash = "defaultPassword"; // Bạn có thể thay đổi mật khẩu này

                // Thêm người dùng mới
                users.Add_User(username, passwordHash, roleId);
                MessageBox.Show("Add user successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"error while adding user: {ex.Message}", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Lấy thông tin người dùng từ các TextBox
                int userId = int.Parse(txtUserID.Text.Trim());
                string username = txtUsername.Text.Trim();
                int roleId = int.Parse(txtRoleID.Text.Trim());

                // Kiểm tra nếu người dùng nhập mật khẩu mới hay không
                string password = string.Empty;
                if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    // Nếu có mật khẩu mới, sử dụng mật khẩu mới nhập vào
                    password = txtPassword.Text.Trim();
                }
                else
                {
                    // Nếu không có mật khẩu mới, giữ nguyên mật khẩu cũ từ cơ sở dữ liệu
                    password = GetCurrentPassword(userId); // Phương thức này để lấy mật khẩu cũ
                }

                // Cập nhật thông tin người dùng (bao gồm tên, mật khẩu và quyền)
                users.Update_User(userId, username, password, roleId);

                // Thông báo thành công và cập nhật lại danh sách người dùng
                MessageBox.Show("Update information user successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUsers();  // Cập nhật lại danh sách người dùng (giả sử bạn đã có phương thức này)
                ClearInputFields(); // Xóa các trường nhập liệu sau khi cập nhật
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có vấn đề trong quá trình cập nhật
                MessageBox.Show($"An error occurred while updating users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Delete user successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public string GetCurrentPassword(int userId)
        {
            string password = string.Empty;
            string sql = "SELECT PasswordHash FROM Users WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(sql, kn.conn);
            cmd.Parameters.AddWithValue("@UserID", userId);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                password = reader["PasswordHash"].ToString(); // Lấy mật khẩu cũ từ cơ sở dữ liệu
            }
            reader.Close();

            return password;
        }

    }
}
