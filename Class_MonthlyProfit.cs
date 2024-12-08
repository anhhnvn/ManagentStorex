using System;
using System.Data;
using System.Data.SqlClient;

namespace BT1
{
    public class Class_MonthlyProfit
    {
        KetNoi kn = new KetNoi();  // Kết nối cơ sở dữ liệu

        // Hàm thêm một bản ghi vào bảng MonthlyProfit
        public void AddProfit(int employeeID, int salesID, decimal totalProfit, DateTime profitDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO MonthlyProfit (EmployeeID, SalesID, TotalProfit, ProfitDate) VALUES (@EmployeeID, @SalesID, @TotalProfit, @ProfitDate)");
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalesID", salesID);
                cmd.Parameters.AddWithValue("@TotalProfit", totalProfit);
                cmd.Parameters.AddWithValue("@ProfitDate", profitDate);

                kn.Execute(cmd); // Thực thi câu lệnh SQL
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        // Hàm cập nhật một bản ghi trong bảng MonthlyProfit
        public void EditProfit(int profitID, int employeeID, int salesID, decimal totalProfit, DateTime profitDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE MonthlyProfit SET EmployeeID = @EmployeeID, SalesID = @SalesID, TotalProfit = @TotalProfit, ProfitDate = @ProfitDate WHERE ProfitID = @ProfitID");
                cmd.Parameters.AddWithValue("@ProfitID", profitID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalesID", salesID);
                cmd.Parameters.AddWithValue("@TotalProfit", totalProfit);
                cmd.Parameters.AddWithValue("@ProfitDate", profitDate);

                kn.Execute(cmd); // Thực thi câu lệnh SQL
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }

        // Hàm xóa một bản ghi trong bảng MonthlyProfit
        public void DeleteProfit(int profitID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM MonthlyProfit WHERE ProfitID = @ProfitID");
                cmd.Parameters.AddWithValue("@ProfitID", profitID);

                kn.Execute(cmd); // Thực thi câu lệnh SQL
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }

        // Hàm lấy tất cả dữ liệu từ bảng MonthlyProfit
        public DataTable GetAllProfits()
        {
            try
            {
                string sql = "SELECT * FROM MonthlyProfit";
                return kn.Load(sql); // Trả về DataTable chứa dữ liệu
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
        }

        // Hàm lấy một bản ghi từ bảng MonthlyProfit theo ProfitID
        public DataTable GetProfitByID(int profitID)
        {
            try
            {
                string sql = "SELECT * FROM MonthlyProfit WHERE ProfitID = @ProfitID";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@ProfitID", profitID);
                return kn.Load(cmd); // Trả về DataTable chứa dữ liệu tìm được
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
        }
    }
}
