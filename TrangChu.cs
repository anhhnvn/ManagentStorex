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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_products_Click(object sender, EventArgs e)
        {
            Product prouduct = new Product();
            prouduct.Show();
        }

       

        private void btn_employees_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Roles form_Roles = new Form_Roles();
            form_Roles.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form_Customers form_Customers = new Form_Customers();
            form_Customers.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_PurchaseHistorys form_PurchaseHistorys = new Form_PurchaseHistorys();
            form_PurchaseHistorys.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void Form_DailySale_Click(object sender, EventArgs e)
        {
            Form_DailySale form_DailySale = new Form_DailySale();
            form_DailySale.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form_MonthlyProfit form_MonthlyProfit = new Form_MonthlyProfit();
            form_MonthlyProfit.Show();
        }
    }
}
