using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class AddEmployeeForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public AddEmployeeForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Đặt giá trị mặc định
            dtpNgaySinh.Value = DateTime.Now.AddYears(-25); // Mặc định 25 tuổi
            cboGioiTinh.SelectedIndex = 0; // Nam
            cboChucVu.SelectedIndex = 0; // Nhân viên trực
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu đầu vào
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_InsertEmployee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        // Thêm parameters
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                        cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoDienThoai", string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? (object)DBNull.Value : txtSoDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@ChucVu", cboChucVu.Text);
                        
                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@NewEmployeeID", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);
                        
                        // Thực thi stored procedure
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable result = new DataTable();
                        adapter.Fill(result);
                        
                        if (result.Rows.Count > 0)
                        {
                            string status = result.Rows[0]["Result"].ToString();
                            string message = result.Rows[0]["Message"].ToString();
                            
                            if (status == "SUCCESS")
                            {
                                int newEmployeeID = Convert.ToInt32(result.Rows[0]["EmployeeID"]);
                                MessageBox.Show($"{message}\nMã nhân viên mới: {newEmployeeID}", "Thành công", 
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
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Kiểm tra họ tên (bắt buộc)
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Kiểm tra độ tuổi hợp lệ (16-65 tuổi)
            int age = DateTime.Now.Year - dtpNgaySinh.Value.Year;
            if (dtpNgaySinh.Value.Date > DateTime.Now.AddYears(-age)) age--;
            
            if (age < 16 || age > 65)
            {
                MessageBox.Show("Tuổi nhân viên phải từ 16 đến 65!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // Kiểm tra chức vụ (bắt buộc)
            if (cboChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChucVu.Focus();
                return false;
            }

            // Kiểm tra số điện thoại (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                string sdt = txtSoDienThoai.Text.Trim();
                if (sdt.Length < 10 || sdt.Length > 11 || !sdt.StartsWith("0") || !IsNumeric(sdt))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! (Phải có 10-11 số và bắt đầu bằng 0)", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoDienThoai.Focus();
                    return false;
                }
            }

            // Kiểm tra email (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string email = txtEmail.Text.Trim();
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsNumeric(string text)
        {
            return long.TryParse(text, out _);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Event để format số điện thoại khi nhập
        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Event để validate email khi rời khỏi textbox
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                }
            }
        }
    }
}
