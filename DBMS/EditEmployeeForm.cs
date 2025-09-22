using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class EditEmployeeForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        private int employeeId;
        private DataRow employeeData;

        public EditEmployeeForm(int nhanVienId)
        {
            InitializeComponent();
            employeeId = nhanVienId;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_GetEmployeeById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NhanVienID", employeeId);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        if (dataTable.Rows.Count > 0)
                        {
                            employeeData = dataTable.Rows[0];
                            PopulateForm();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Lỗi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void PopulateForm()
        {
            if (employeeData != null)
            {
                txtHoTen.Text = employeeData["HoTen"].ToString();
                txtDiaChi.Text = employeeData["DiaChi"].ToString();
                txtSoDienThoai.Text = employeeData["SoDienThoai"].ToString();
                txtEmail.Text = employeeData["Email"].ToString();
                txtChucVu.Text = employeeData["ChucVu"].ToString();
                
                // Ngày sinh
                if (employeeData["NgaySinh"] != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(employeeData["NgaySinh"]);
                    dtpNgaySinh.Checked = true;
                }
                else
                {
                    dtpNgaySinh.Checked = false;
                }
                
                // Giới tính
                string gioiTinh = employeeData["GioiTinh"].ToString();
                if (gioiTinh == "Nam")
                    rdbNam.Checked = true;
                else if (gioiTinh == "Nữ")
                    rdbNu.Checked = true;
                else
                    rdbKhong.Checked = true;
                
                // Trạng thái
                string trangThai = employeeData["TrangThai"].ToString();
                if (trangThai == "Đang làm")
                    rdbDangLam.Checked = true;
                else if (trangThai == "Đã nghỉ việc")
                    rdbNghiViec.Checked = true;
                else
                    rdbTamNghi.Checked = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                UpdateEmployee();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi nhập liệu", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập chức vụ!", "Lỗi nhập liệu", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChucVu.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi nhập liệu", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void UpdateEmployee()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@NhanVienID", employeeId);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Checked ? (object)dtpNgaySinh.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", GetSelectedGender());
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? DBNull.Value : (object)txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? DBNull.Value : (object)txtSoDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : (object)txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@ChucVu", txtChucVu.Text.Trim());
                        cmd.Parameters.AddWithValue("@TrangThai", GetSelectedStatus());
                        
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
                                this.DialogResult = DialogResult.OK;
                                this.Close();
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
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedGender()
        {
            if (rdbNam.Checked) return "Nam";
            if (rdbNu.Checked) return "Nữ";
            return "Không xác định";
        }

        private string GetSelectedStatus()
        {
            if (rdbDangLam.Checked) return "Đang làm";
            if (rdbNghiViec.Checked) return "Đã nghỉ việc";
            return "Tạm nghỉ";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
