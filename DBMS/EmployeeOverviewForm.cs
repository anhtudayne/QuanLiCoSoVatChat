using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class EmployeeOverviewForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public EmployeeOverviewForm()
        {
            InitializeComponent();
        }

        private void EmployeeOverviewForm_Load(object sender, EventArgs e)
        {
            SetupDataGridViews();
            LoadEmployeeStatistics();
            LoadPositionStatistics();
            LoadBirthdayEmployees();
            
            // Set tháng hiện tại cho ComboBox
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void LoadEmployeeStatistics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Lấy thống kê nhân viên theo trạng thái
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM fn_GetEmployeeStatistics()", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvStatistics.DataSource = dt;
                        
                        if (dgvStatistics.Columns.Count > 0)
                        {
                            dgvStatistics.Columns["TrangThai"].HeaderText = "Trạng Thái";
                            dgvStatistics.Columns["SoLuong"].HeaderText = "Số Lượng";
                            dgvStatistics.Columns["TyLePhanTram"].HeaderText = "Tỷ Lệ (%)";
                            
                            dgvStatistics.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvStatistics.Columns["TyLePhanTram"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvStatistics.Columns["TyLePhanTram"].DefaultCellStyle.Format = "0.00";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPositionStatistics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Lấy danh sách chức vụ và đếm số lượng
                    string query = @"
                        SELECT DISTINCT ChucVu, 
                               dbo.fn_CountEmployeesByPosition(ChucVu) AS SoLuong
                        FROM NhanVien 
                        WHERE ChucVu IS NOT NULL
                        ORDER BY SoLuong DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvPositions.DataSource = dt;
                        
                        if (dgvPositions.Columns.Count > 0)
                        {
                            dgvPositions.Columns["ChucVu"].HeaderText = "Chức Vụ";
                            dgvPositions.Columns["SoLuong"].HeaderText = "Số Lượng";
                            
                            dgvPositions.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê chức vụ: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBirthdayEmployees()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Lấy danh sách nhân viên có sinh nhật trong tháng hiện tại
                    int currentMonth = DateTime.Now.Month;
                    string query = $"SELECT * FROM fn_GetBirthdayEmployees({currentMonth}) ORDER BY NgaySinhNhat";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvBirthdays.DataSource = dt;
                        
                        if (dgvBirthdays.Columns.Count > 0)
                        {
                            dgvBirthdays.Columns["NhanVienID"].HeaderText = "Mã NV";
                            dgvBirthdays.Columns["HoTen"].HeaderText = "Họ Tên";
                            dgvBirthdays.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                            dgvBirthdays.Columns["ChucVu"].HeaderText = "Chức Vụ";
                            dgvBirthdays.Columns["SoDienThoai"].HeaderText = "Số ĐT";
                            dgvBirthdays.Columns["Email"].HeaderText = "Email";
                            dgvBirthdays.Columns["NgaySinhNhat"].HeaderText = "Ngày";
                            dgvBirthdays.Columns["TuoiSapToi"].HeaderText = "Tuổi";
                            
                            dgvBirthdays.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                            dgvBirthdays.Columns["NhanVienID"].Width = 80;
                            dgvBirthdays.Columns["NgaySinhNhat"].Width = 60;
                            dgvBirthdays.Columns["TuoiSapToi"].Width = 60;
                            
                            dgvBirthdays.Columns["NhanVienID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvBirthdays.Columns["NgaySinhNhat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvBirthdays.Columns["TuoiSapToi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        
                        lblBirthdayCount.Text = $"Có {dt.Rows.Count} nhân viên sinh nhật tháng {currentMonth}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sinh nhật: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedIndex >= 0)
            {
                int selectedMonth = cmbMonth.SelectedIndex + 1;
                LoadBirthdayEmployeesByMonth(selectedMonth);
            }
        }

        private void LoadBirthdayEmployeesByMonth(int month)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = $"SELECT * FROM fn_GetBirthdayEmployees({month}) ORDER BY NgaySinhNhat";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvBirthdays.DataSource = dt;
                        lblBirthdayCount.Text = $"Có {dt.Rows.Count} nhân viên sinh nhật tháng {month}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sinh nhật: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployeeStatistics();
            LoadPositionStatistics();
            LoadBirthdayEmployees();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetupDataGridViews()
        {
            // Setup cho tất cả DataGridView
            DataGridView[] grids = { dgvStatistics, dgvPositions, dgvBirthdays };
            
            foreach (var grid in grids)
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.MultiSelect = false;
                grid.AllowUserToAddRows = false;
                grid.AllowUserToDeleteRows = false;
                grid.ReadOnly = true;
                
                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 44, 51);
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
                
                grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            }
        }
    }
}