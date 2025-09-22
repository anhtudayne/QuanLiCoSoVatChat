using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class SearchEmployeeForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        public DataTable SearchResults { get; private set; }

        public SearchEmployeeForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Load chức vụ
                    using (SqlCommand cmd = new SqlCommand("sp_GetPositions", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        cboChucVu.DataSource = dt;
                        cboChucVu.DisplayMember = "ChucVu";
                        cboChucVu.ValueMember = "ChucVu";
                        cboChucVu.SelectedIndex = -1;
                    }
                    
                    // Load trạng thái
                    using (SqlCommand cmd = new SqlCommand("sp_GetEmployeeStatuses", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        cboTrangThai.DataSource = dt;
                        cboTrangThai.DisplayMember = "TrangThai";
                        cboTrangThai.ValueMember = "TrangThai";
                        cboTrangThai.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SearchEmployees();
        }

        private void SearchEmployees()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_SearchEmployees", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        // Tham số tìm kiếm
                        string searchTerm = string.IsNullOrWhiteSpace(txtTimKiem.Text) ? null : txtTimKiem.Text.Trim();
                        string chucVu = cboChucVu.SelectedIndex == -1 ? null : cboChucVu.SelectedValue.ToString();
                        string trangThai = cboTrangThai.SelectedIndex == -1 ? null : cboTrangThai.SelectedValue.ToString();
                        
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        // Hiển thị kết quả
                        dgvKetQua.DataSource = dataTable;
                        SetupDataGridView();
                        
                        // Lưu kết quả để trả về
                        SearchResults = dataTable.Copy();
                        
                        lblKetQua.Text = $"Tìm thấy {dataTable.Rows.Count} nhân viên";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvKetQua.Columns.Count > 0)
            {
                // Đặt tên cột tiếng Việt
                dgvKetQua.Columns["NhanVienID"].HeaderText = "Mã NV";
                dgvKetQua.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvKetQua.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvKetQua.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvKetQua.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvKetQua.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dgvKetQua.Columns["Email"].HeaderText = "Email";
                dgvKetQua.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvKetQua.Columns["ChucVu"].HeaderText = "Chức Vụ";
                dgvKetQua.Columns["Tuoi"].HeaderText = "Tuổi";
                
                // Điều chỉnh độ rộng cột
                dgvKetQua.Columns["NhanVienID"].Width = 80;
                dgvKetQua.Columns["Tuoi"].Width = 80;
                dgvKetQua.Columns["GioiTinh"].Width = 100;
                dgvKetQua.Columns["TrangThai"].Width = 120;
                dgvKetQua.Columns["ChucVu"].Width = 150;
                dgvKetQua.Columns["NgaySinh"].Width = 120;
                
                // Định dạng ngày sinh
                dgvKetQua.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                
                // Căn giữa cho cột ID và Tuổi
                dgvKetQua.Columns["NhanVienID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvKetQua.Columns["Tuoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboChucVu.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            dgvKetQua.DataSource = null;
            lblKetQua.Text = "Nhập thông tin tìm kiếm";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.SelectedRows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên từ danh sách!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchEmployees();
            }
        }
    }
}
