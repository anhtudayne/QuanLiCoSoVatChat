using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class SalaryReportForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public SalaryReportForm()
        {
            InitializeComponent();
            LoadYearList();
            
            // Đặt giá trị mặc định
            cboThang.SelectedIndex = DateTime.Now.Month - 1; // Tháng hiện tại
            cboQuy.SelectedIndex = (DateTime.Now.Month - 1) / 3; // Quý hiện tại
            
            LoadMonthlyReport();
        }

        private void LoadYearList()
        {
            // Tạo danh sách năm từ 2020 đến năm hiện tại + 1
            int currentYear = DateTime.Now.Year;
            for (int year = 2020; year <= currentYear + 1; year++)
            {
                cboNam.Items.Add(year);
            }
            cboNam.SelectedItem = currentYear; // Chọn năm hiện tại
        }

        private void LoadMonthlyReport()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    int selectedMonth = cboThang.SelectedIndex + 1;
                    int selectedYear = Convert.ToInt32(cboNam.SelectedItem);
                    
                    // Sử dụng function fn_GetAllEmployeeSalariesInMonth
                    string query = "SELECT * FROM dbo.fn_GetAllEmployeeSalariesInMonth(@Thang, @Nam) ORDER BY TongLuong DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Thang", selectedMonth);
                        cmd.Parameters.AddWithValue("@Nam", selectedYear);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        dgvBaoCaoLuong.DataSource = dataTable;
                        SetupDataGridView();
                        UpdateSummaryStats(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo lương: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuarterlyReport()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    int selectedQuarter = cboQuy.SelectedIndex + 1;
                    int selectedYear = Convert.ToInt32(cboNam.SelectedItem);
                    
                    // Sử dụng function fn_GetAllEmployeeSalariesInQuarter
                    string query = "SELECT * FROM dbo.fn_GetAllEmployeeSalariesInQuarter(@Quy, @Nam) ORDER BY TongLuongQuy DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Quy", selectedQuarter);
                        cmd.Parameters.AddWithValue("@Nam", selectedYear);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        dgvBaoCaoLuong.DataSource = dataTable;
                        SetupQuarterlyDataGridView();
                        UpdateQuarterlySummaryStats(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo lương theo quý: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadYearlyReport()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    int selectedYear = Convert.ToInt32(cboNam.SelectedItem);
                    
                    // Sử dụng function fn_GetAllEmployeeSalariesInYear
                    string query = "SELECT * FROM dbo.fn_GetAllEmployeeSalariesInYear(@Nam) ORDER BY TongLuongNam DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", selectedYear);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        dgvBaoCaoLuong.DataSource = dataTable;
                        SetupYearlyDataGridView();
                        UpdateYearlySummaryStats(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo lương theo năm: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvBaoCaoLuong.Columns.Count > 0)
            {
                dgvBaoCaoLuong.Columns["NhanVienID"].HeaderText = "Mã NV";
                dgvBaoCaoLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvBaoCaoLuong.Columns["ChucVu"].HeaderText = "Chức Vụ";
                dgvBaoCaoLuong.Columns["LuongCoBanTheoGio"].HeaderText = "Lương/giờ";
                dgvBaoCaoLuong.Columns["TongGioLamViec"].HeaderText = "Tổng giờ";
                dgvBaoCaoLuong.Columns["TongLuong"].HeaderText = "Tổng lương";
                dgvBaoCaoLuong.Columns["Thang"].HeaderText = "Tháng";
                dgvBaoCaoLuong.Columns["Nam"].HeaderText = "Năm";
                
                // Ẩn cột TrangThai
                if (dgvBaoCaoLuong.Columns.Contains("TrangThai"))
                    dgvBaoCaoLuong.Columns["TrangThai"].Visible = false;
                
                // Định dạng
                dgvBaoCaoLuong.Columns["LuongCoBanTheoGio"].DefaultCellStyle.Format = "N0";
                dgvBaoCaoLuong.Columns["TongGioLamViec"].DefaultCellStyle.Format = "N2";
                dgvBaoCaoLuong.Columns["TongLuong"].DefaultCellStyle.Format = "N0";
                
                // Căn chỉnh
                dgvBaoCaoLuong.Columns["NhanVienID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBaoCaoLuong.Columns["LuongCoBanTheoGio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBaoCaoLuong.Columns["TongGioLamViec"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBaoCaoLuong.Columns["TongLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
                // Độ rộng
                dgvBaoCaoLuong.Columns["NhanVienID"].Width = 80;
                dgvBaoCaoLuong.Columns["HoTen"].Width = 180;
                dgvBaoCaoLuong.Columns["ChucVu"].Width = 120;
                dgvBaoCaoLuong.Columns["LuongCoBanTheoGio"].Width = 120;
                dgvBaoCaoLuong.Columns["TongGioLamViec"].Width = 100;
                dgvBaoCaoLuong.Columns["TongLuong"].Width = 150;
            }
        }

        private void SetupQuarterlyDataGridView()
        {
            if (dgvBaoCaoLuong.Columns.Count > 0)
            {
                dgvBaoCaoLuong.Columns["NhanVienID"].HeaderText = "Mã NV";
                dgvBaoCaoLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvBaoCaoLuong.Columns["ChucVu"].HeaderText = "Chức Vụ";
                dgvBaoCaoLuong.Columns["TongGioLamViecQuy"].HeaderText = "Tổng giờ quý";
                dgvBaoCaoLuong.Columns["TongLuongQuy"].HeaderText = "Tổng lương quý";
                dgvBaoCaoLuong.Columns["Quy"].HeaderText = "Quý";
                dgvBaoCaoLuong.Columns["Nam"].HeaderText = "Năm";
                
                // Ẩn các cột không cần thiết
                foreach (DataGridViewColumn col in dgvBaoCaoLuong.Columns)
                {
                    if (col.Name.StartsWith("LuongThang"))
                        col.Visible = false;
                }
                
                // Định dạng
                dgvBaoCaoLuong.Columns["TongGioLamViecQuy"].DefaultCellStyle.Format = "N2";
                dgvBaoCaoLuong.Columns["TongLuongQuy"].DefaultCellStyle.Format = "N0";
                
                // Căn chỉnh
                dgvBaoCaoLuong.Columns["TongGioLamViecQuy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBaoCaoLuong.Columns["TongLuongQuy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void SetupYearlyDataGridView()
        {
            if (dgvBaoCaoLuong.Columns.Count > 0)
            {
                dgvBaoCaoLuong.Columns["NhanVienID"].HeaderText = "Mã NV";
                dgvBaoCaoLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvBaoCaoLuong.Columns["ChucVu"].HeaderText = "Chức Vụ";
                dgvBaoCaoLuong.Columns["TongGioLamViecNam"].HeaderText = "Tổng giờ năm";
                dgvBaoCaoLuong.Columns["TongLuongNam"].HeaderText = "Tổng lương năm";
                dgvBaoCaoLuong.Columns["Nam"].HeaderText = "Năm";
                
                // Định dạng
                dgvBaoCaoLuong.Columns["TongGioLamViecNam"].DefaultCellStyle.Format = "N2";
                dgvBaoCaoLuong.Columns["TongLuongNam"].DefaultCellStyle.Format = "N0";
                
                // Căn chỉnh
                dgvBaoCaoLuong.Columns["TongGioLamViecNam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBaoCaoLuong.Columns["TongLuongNam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void UpdateSummaryStats(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                decimal totalSalary = 0;
                decimal totalHours = 0;
                
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["TongLuong"] != DBNull.Value)
                        totalSalary += Convert.ToDecimal(row["TongLuong"]);
                    if (row["TongGioLamViec"] != DBNull.Value)
                        totalHours += Convert.ToDecimal(row["TongGioLamViec"]);
                }
                
                lblThongKe.Text = $"Tổng: {dataTable.Rows.Count} NV | {totalHours:N2} giờ | {totalSalary:N0} VNĐ";
            }
            else
            {
                lblThongKe.Text = "Không có dữ liệu";
            }
        }

        private void UpdateQuarterlySummaryStats(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                decimal totalSalary = 0;
                decimal totalHours = 0;
                
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["TongLuongQuy"] != DBNull.Value)
                        totalSalary += Convert.ToDecimal(row["TongLuongQuy"]);
                    if (row["TongGioLamViecQuy"] != DBNull.Value)
                        totalHours += Convert.ToDecimal(row["TongGioLamViecQuy"]);
                }
                
                lblThongKe.Text = $"Tổng quý: {dataTable.Rows.Count} NV | {totalHours:N2} giờ | {totalSalary:N0} VNĐ";
            }
            else
            {
                lblThongKe.Text = "Không có dữ liệu";
            }
        }

        private void UpdateYearlySummaryStats(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                decimal totalSalary = 0;
                decimal totalHours = 0;
                
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["TongLuongNam"] != DBNull.Value)
                        totalSalary += Convert.ToDecimal(row["TongLuongNam"]);
                    if (row["TongGioLamViecNam"] != DBNull.Value)
                        totalHours += Convert.ToDecimal(row["TongGioLamViecNam"]);
                }
                
                lblThongKe.Text = $"Tổng năm: {dataTable.Rows.Count} NV | {totalHours:N2} giờ | {totalSalary:N0} VNĐ";
            }
            else
            {
                lblThongKe.Text = "Không có dữ liệu";
            }
        }

        private void rdoThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoThang.Checked)
            {
                cboThang.Enabled = true;
                cboQuy.Enabled = false;
                LoadMonthlyReport();
            }
        }

        private void rdoQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoQuy.Checked)
            {
                cboThang.Enabled = false;
                cboQuy.Enabled = true;
                LoadQuarterlyReport();
            }
        }

        private void rdoNam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNam.Checked)
            {
                cboThang.Enabled = false;
                cboQuy.Enabled = false;
                LoadYearlyReport();
            }
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoThang.Checked)
                LoadMonthlyReport();
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoQuy.Checked)
                LoadQuarterlyReport();
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoThang.Checked)
                LoadMonthlyReport();
            else if (rdoQuy.Checked)
                LoadQuarterlyReport();
            else if (rdoNam.Checked)
                LoadYearlyReport();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}