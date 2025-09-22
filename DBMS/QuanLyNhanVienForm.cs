using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class QuanLyNhanVienForm : Form
    {
        // Connection string - bạn đã kết nối thành công với database vc
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public QuanLyNhanVienForm()
        {
            InitializeComponent();
        }

        private void QuanLyNhanVienForm_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Cấu hình DataGridView
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.AllowUserToDeleteRows = false;
            dgvNhanVien.ReadOnly = true;
            
            // Đặt màu cho header
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 44, 51);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            
            // Màu xen kẽ cho rows
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Gọi stored procedure sp_GetAllEmployees
                    using (SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        // Gán dữ liệu vào DataGridView
                        dgvNhanVien.DataSource = dataTable;
                        
                        // Đặt tên cột tiếng Việt và định dạng
                        if (dgvNhanVien.Columns.Count > 0)
                        {
                            dgvNhanVien.Columns["NhanVienID"].HeaderText = "Mã NV";
                            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ và Tên";
                            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
                            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                            dgvNhanVien.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                            dgvNhanVien.Columns["Email"].HeaderText = "Email";
                            dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái";
                            dgvNhanVien.Columns["ChucVu"].HeaderText = "Chức Vụ";
                            dgvNhanVien.Columns["Tuoi"].HeaderText = "Tuổi";
                            
                            // Hiển thị cột ID và điều chỉnh độ rộng
                            dgvNhanVien.Columns["NhanVienID"].Visible = true;
                            dgvNhanVien.Columns["NhanVienID"].Width = 80;
                            dgvNhanVien.Columns["Tuoi"].Width = 80;
                            dgvNhanVien.Columns["GioiTinh"].Width = 100;
                            dgvNhanVien.Columns["TrangThai"].Width = 120;
                            dgvNhanVien.Columns["ChucVu"].Width = 150;
                            
                            // Định dạng ngày sinh
                            dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                            dgvNhanVien.Columns["NgaySinh"].Width = 120;
                            
                            // Căn giữa cho cột ID và Tuổi
                            dgvNhanVien.Columns["NhanVienID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvNhanVien.Columns["Tuoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (AddEmployeeForm addForm = new AddEmployeeForm())
                {
                    if (addForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Refresh dữ liệu sau khi thêm thành công
                        LoadEmployeeData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form thêm nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int nhanVienId = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["NhanVienID"].Value);
                
                using (EditEmployeeForm editForm = new EditEmployeeForm(nhanVienId))
                {
                    if (editForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Refresh dữ liệu sau khi sửa thành công
                        LoadEmployeeData();
                        MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form sửa nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int nhanVienId = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["NhanVienID"].Value);
                string hoTen = dgvNhanVien.SelectedRows[0].Cells["HoTen"].Value.ToString();
                
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhân viên '{hoTen}'?\n\n" +
                    "Lưu ý: Đây là thao tác xóa mềm (soft delete), nhân viên sẽ được chuyển sang trạng thái 'Đã nghỉ việc'.",
                    "Xác nhận xóa nhân viên",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteEmployee(nhanVienId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteEmployee(int nhanVienId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        
                        if (resultTable.Rows.Count > 0)
                        {
                            string result = resultTable.Rows[0]["Result"].ToString();
                            string message = resultTable.Rows[0]["Message"].ToString();
                            
                            if (result == "SUCCESS")
                            {
                                MessageBox.Show(message, "Thành công", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadEmployeeData(); // Refresh dữ liệu
                            }
                            else
                            {
                                MessageBox.Show(message, "Lỗi", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SearchEmployeeForm searchForm = new SearchEmployeeForm())
                {
                    if (searchForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Hiển thị kết quả tìm kiếm
                        if (searchForm.SearchResults != null && searchForm.SearchResults.Rows.Count > 0)
                        {
                            dgvNhanVien.DataSource = searchForm.SearchResults;
                            SetupDataGridView();
                            
                            MessageBox.Show($"Tìm thấy {searchForm.SearchResults.Rows.Count} nhân viên phù hợp!", 
                                "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên nào phù hợp với điều kiện tìm kiếm!", 
                                "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form tìm kiếm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemLuong_Click(object sender, EventArgs e)
        {
            try
            {
                int? nhanVienId = null;
                
                // Nếu có nhân viên được chọn, mở form xem lương cho nhân viên đó
                if (dgvNhanVien.SelectedRows.Count > 0)
                {
                    nhanVienId = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["NhanVienID"].Value);
                }
                
                using (ViewSalaryForm salaryForm = new ViewSalaryForm(nhanVienId ?? 0))
                {
                    salaryForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form xem lương: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
            MessageBox.Show("Đã làm mới dữ liệu thành công!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            try
            {
                using (EmployeeOverviewForm overviewForm = new EmployeeOverviewForm())
                {
                    overviewForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form tổng quan: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method để test kết nối database
        private void TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Kết nối database thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
