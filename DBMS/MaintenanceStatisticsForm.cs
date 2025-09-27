using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DBMS
{
    public partial class MaintenanceStatisticsForm : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=vc;Integrated Security=True";

        public MaintenanceStatisticsForm()
        {
            InitializeComponent();
        }

        private void MaintenanceStatisticsForm_Load(object sender, EventArgs e)
        {
            // Mặc định là từ đầu năm đến hiện tại
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value > dtpDenNgay.Value)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("=".PadLeft(60, '='));
                result.AppendLine($"📊 THỐNG KÊ BẢO TRÌ TỪ {dtpTuNgay.Value:dd/MM/yyyy} ĐẾN {dtpDenNgay.Value:dd/MM/yyyy}");
                result.AppendLine("=".PadLeft(60, '='));
                result.AppendLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy danh sách tất cả CSVC có trong lịch sử bảo trì
                    using (SqlCommand cmd = new SqlCommand("sp_GetCSVCInMaintenanceHistory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool hasData = false;
                            
                            while (reader.Read())
                            {
                                hasData = true;
                                int csvcId = Convert.ToInt32(reader["CSVCID"]);
                                string tenCSVC = reader["TenCSVC"].ToString();

                                // Tính tổng chi phí bảo trì cho CSVC này
                                decimal tongChiPhi = GetTotalMaintenanceCost(csvcId, dtpTuNgay.Value, dtpDenNgay.Value);
                                
                                // Đếm số lần bảo trì cho CSVC này
                                int soLanBaoTri = GetMaintenanceCount(csvcId, dtpTuNgay.Value, dtpDenNgay.Value);

                                result.AppendLine($"🔧 {tenCSVC}:");
                                result.AppendLine($"   • Số lần bảo trì: {soLanBaoTri} lần");
                                result.AppendLine($"   • Tổng chi phí: {tongChiPhi:N0} VNĐ");
                                result.AppendLine();
                            }
                            
                            if (!hasData)
                            {
                                result.AppendLine("❌ Không có dữ liệu bảo trì trong khoảng thời gian này.");
                                result.AppendLine();
                            }
                        }
                    }

                    // Tính tổng chung
                    decimal tongChiPhiTatCa = GetTotalMaintenanceCost(null, dtpTuNgay.Value, dtpDenNgay.Value);
                    int tongSoLanBaoTri = GetMaintenanceCount(null, dtpTuNgay.Value, dtpDenNgay.Value);

                    result.AppendLine("=".PadLeft(60, '='));
                    result.AppendLine("📈 TỔNG KẾT:");
                    result.AppendLine($"• Tổng số lần bảo trì trong kỳ: {tongSoLanBaoTri} lần");
                    result.AppendLine($"• Tổng chi phí bảo trì: {tongChiPhiTatCa:N0} VNĐ");
                    
                    result.AppendLine($"⏰ Thời gian tạo báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    result.AppendLine("=".PadLeft(60, '='));
                }

                txtResults.Text = result.ToString();
                txtResults.SelectionStart = 0;
                txtResults.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tính thống kê:\n{ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetTotalMaintenanceCost(int? csvcId, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.fn_GetTotalMaintenanceCost(@CSVCID, @TuNgay, @DenNgay)", conn))
                    {
                        cmd.Parameters.AddWithValue("@CSVCID", (object)csvcId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);
                        
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private int GetMaintenanceCount(int? csvcId, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.fn_GetMaintenanceCount(@CSVCID, @TuNgay, @DenNgay)", conn))
                    {
                        cmd.Parameters.AddWithValue("@CSVCID", (object)csvcId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);
                        
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}