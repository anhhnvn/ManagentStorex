using System;
using System.Windows.Forms;

namespace BT1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi tạo Form_Login và hiển thị
            Form_Login loginForm = new Form_Login();
            // Chạy cửa sổ đăng nhập và kiểm tra nếu đăng nhập thành công
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Nếu đăng nhập thành công, mở Form Main
                Application.Run(new Main());
            }
            else
            {
                // Nếu không đăng nhập thành công, thoát ứng dụng
                Application.Exit();
            }
        }
    }
}
