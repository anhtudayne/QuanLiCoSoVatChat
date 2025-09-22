using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class ViewSalaryForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        private int? selectedEmployeeId = null;

        public ViewSalaryForm()
        {
            InitializeComponent();
            LoadYearList();
            LoadEmployeeList();
            LoadSalaryData();
        }

        public ViewSalaryForm(int nhanVienId)
        {
            InitializeComponent();
            selectedEmployeeId = nhanVienId;
            LoadYearList();
            LoadEmployeeList();
            LoadSalaryData();
            
            // Chọn nhân viên được chỉ định
            cboNhanVien.SelectedValue = nhanVienId;
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

        private void LoadEmployeeList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = "SELECT NhanVienID, HoTen FROM NhanVien WHERE TrangThai = N'Đang làm' ORDER BY HoTen";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // Thêm option "Tất cả nhân viên"
                        DataRow allRow = dt.NewRow();
                        allRow["NhanVienID"] = 0;
                        allRow["HoTen"] = "Tất cả nhân viên";
                        dt.Rows.InsertAt(allRow, 0);
                        
                        cboNhanVien.DataSource = dt;
                        cboNhanVien.DisplayMember = "HoTen";
                        cboNhanVien.ValueMember = "NhanVienID";
                        
                        if (selectedEmployeeId.HasValue)
                        {
                            cboNhanVien.SelectedValue = selectedEmployeeId.Value;
                        }
                        else
                        {
                            cboNhanVien.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalaryData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    int selectedEmployeeId = Convert.ToInt32(cboNhanVien.SelectedValue);
                    int? selectedMonth = cboThang.SelectedIndex == -1 ? DateTime.Now.Month : (cboThang.SelectedIndex + 1);
                    int? selectedYear = cboNam.SelectedItem == null ? DateTime.Now.Year : Convert.ToInt32(cboNam.SelectedItem);
                    
                    DataTable dataTable = new DataTable();
                    
                    if (selectedEmployeeId == 0) // Tất cả nhân viên
                    {
                        // Sử dụng function fn_GetAllEmployeeSalariesInMonth
                        string query = "SELECT * FROM dbo.fn_GetAllEmployeeSalariesInMonth(@Thang, @Nam) ORDER BY TongLuong DESC";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Thang", selectedMonth.Value);
                            cmd.Parameters.AddWithValue("@Nam", selectedYear.Value);
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dataTable);
                        }
                    }
                    else // Nhân viên cụ thể
                    {
                        // Sử dụng function fn_GetEmployeeSalaryDetails
                        string query = "SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(@NhanVienID, @Thang, @Nam)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@NhanVienID", selectedEmployeeId);
                            cmd.Parameters.AddWithValue("@Thang", selectedMonth.Value);
                            cmd.Parameters.AddWithValue("@Nam", selectedYear.Value);
                            
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dataTable);
                        }
                    }
                    
                    // Hiển thị kết quả
                    dgvLuong.DataSource = dataTable;
                    SetupDataGridView();
                    
                    // Cập nhật thông tin tổng quan
                    UpdateSummaryInfo(dataTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu lương: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvLuong.Columns.Count > 0)
            {
                // Đặt tên cột tiếng Việt cho cấu trúc mới
                if (dgvLuong.Columns.Contains("NhanVienID"))
                    dgvLuong.Columns["NhanVienID"].HeaderText = "Mã NV";
                if (dgvLuong.Columns.Contains("HoTen"))
                    dgvLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
                if (dgvLuong.Columns.Contains("ChucVu"))
                    dgvLuong.Columns["ChucVu"].HeaderText = "Chức Vụ";
                if (dgvLuong.Columns.Contains("TrangThai"))
                    dgvLuong.Columns["TrangThai"].HeaderText = "Trạng Thái";
                if (dgvLuong.Columns.Contains("LuongCoBanTheoGio"))
                    dgvLuong.Columns["LuongCoBanTheoGio"].HeaderText = "Lương cơ bản/giờ";
                if (dgvLuong.Columns.Contains("TongGioLamViec"))
                    dgvLuong.Columns["TongGioLamViec"].HeaderText = "Tổng giờ làm việc";
                if (dgvLuong.Columns.Contains("TongLuong"))
                    dgvLuong.Columns["TongLuong"].HeaderText = "Tổng lương";
                if (dgvLuong.Columns.Contains("Thang"))
                    dgvLuong.Columns["Thang"].HeaderText = "Tháng";
                if (dgvLuong.Columns.Contains("Nam"))
                    dgvLuong.Columns["Nam"].HeaderText = "Năm";
                
                // Điều chỉnh độ rộng cột
                if (dgvLuong.Columns.Contains("NhanVienID"))
                    dgvLuong.Columns["NhanVienID"].Width = 80;
                if (dgvLuong.Columns.Contains("HoTen"))
                    dgvLuong.Columns["HoTen"].Width = 180;
                if (dgvLuong.Columns.Contains("ChucVu"))
                    dgvLuong.Columns["ChucVu"].Width = 120;
                if (dgvLuong.Columns.Contains("TrangThai"))
                    dgvLuong.Columns["TrangThai"].Width = 100;
                if (dgvLuong.Columns.Contains("LuongCoBanTheoGio"))
                    dgvLuong.Columns["LuongCoBanTheoGio"].Width = 150;
                if (dgvLuong.Columns.Contains("TongGioLamViec"))
                    dgvLuong.Columns["TongGioLamViec"].Width = 140;
                if (dgvLuong.Columns.Contains("TongLuong"))
                    dgvLuong.Columns["TongLuong"].Width = 150;
                if (dgvLuong.Columns.Contains("Thang"))
                    dgvLuong.Columns["Thang"].Width = 80;
                if (dgvLuong.Columns.Contains("Nam"))
                    dgvLuong.Columns["Nam"].Width = 80;
                
                // Định dạng tiền tệ và số
                if (dgvLuong.Columns.Contains("LuongCoBanTheoGio"))
                {
                    dgvLuong.Columns["LuongCoBanTheoGio"].DefaultCellStyle.Format = "N0";
                    dgvLuong.Columns["LuongCoBanTheoGio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvLuong.Columns.Contains("TongGioLamViec"))
                {
                    dgvLuong.Columns["TongGioLamViec"].DefaultCellStyle.Format = "N2";
                    dgvLuong.Columns["TongGioLamViec"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvLuong.Columns.Contains("TongLuong"))
                {
                    dgvLuong.Columns["TongLuong"].DefaultCellStyle.Format = "N0";
                    dgvLuong.Columns["TongLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                // Căn giữa cho các cột ID và thời gian
                if (dgvLuong.Columns.Contains("NhanVienID"))
                    dgvLuong.Columns["NhanVienID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvLuong.Columns.Contains("Thang"))
                    dgvLuong.Columns["Thang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvLuong.Columns.Contains("Nam"))
                    dgvLuong.Columns["Nam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                // Ẩn cột TrangThai nếu tất cả nhân viên đều "Đang làm"
                if (dgvLuong.Columns.Contains("TrangThai"))
                    dgvLuong.Columns["TrangThai"].Visible = false;
            }
        }

        private void UpdateSummaryInfo(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                decimal totalSalary = 0;
                decimal totalHours = 0;
                int totalEmployees = 0;
                
                foreach (DataRow row in dataTable.Rows)
                {
                    // Sử dụng cột TongLuong từ function mới
                    if (row["TongLuong"] != DBNull.Value)
                    {
                        totalSalary += Convert.ToDecimal(row["TongLuong"]);
                    }
                    
                    // Tính tổng giờ làm việc
                    if (row["TongGioLamViec"] != DBNull.Value)
                    {
                        totalHours += Convert.ToDecimal(row["TongGioLamViec"]);
                    }
                    
                    totalEmployees++;
                }
                
                lblTongNhanVien.Text = $"Tổng số nhân viên: {totalEmployees}";
                lblTongLuong.Text = $"Tổng lương: {totalSalary:N0} VNĐ";
                lblKetQua.Text = $"Hiển thị {dataTable.Rows.Count} bản ghi - Tổng giờ: {totalHours:N2}h";
            }
            else
            {
                lblTongNhanVien.Text = "Tổng số nhân viên: 0";
                lblTongLuong.Text = "Tổng lương: 0 VNĐ";
                lblKetQua.Text = "Không có dữ liệu";
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadSalaryData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboNhanVien.SelectedIndex = 0;
            cboThang.SelectedIndex = -1;
            cboNam.SelectedIndex = -1;
            LoadSalaryData();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedIndex > 0)
            {
                LoadSalaryData();
            }
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalaryData();
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalaryData();
        }
    }
}
