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
    public partial class Form_Login : Form
    {
        private Class_Login classLogin = new Class_Login();

        public Form_Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Lấy tên đăng nhập và mật khẩu
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra nếu các trường trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra đăng nhập
            bool isLoginSuccessful = classLogin.CheckLogin(username, password);
            if (isLoginSuccessful)
            {
                MessageBox.Show("Login Successfully.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Trả về DialogResult.OK để cho phép form chính mở
                this.DialogResult = DialogResult.OK;
                this.Close(); // Đóng form đăng nhập
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Nếu nhấn hủy, trả về Cancel
            this.Close();
        }
    }
}
