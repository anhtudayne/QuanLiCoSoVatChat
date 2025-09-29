using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class LoginForm : Form
    {
        // Connection string cho việc kiểm tra đăng nhập (dùng admin quyền)
        private string adminConnectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Kiểm tra thông tin đăng nhập bằng SQL function
            try
            {
                string userRole = CheckLoginAndGetRole(username, password);
                
                if (userRole == "Sai username hoặc password")
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                    return;
                }
                
                if (userRole == "User này không có role nào")
                {
                    MessageBox.Show("Tài khoản của bạn chưa được phân quyền!", "Lỗi phân quyền", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đăng nhập thành công - mở form theo vai trò
                this.Hide();
                OpenFormByRole(userRole, username);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CheckLoginAndGetRole(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(adminConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.fn_LoginAndGetRole(@username, @password)", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "Lỗi không xác định";
                }
            }
        }

        private void OpenFormByRole(string role, string username)
        {
            // Tạo connection string riêng cho user với SQL Server Authentication
            string userConnectionString = CreateUserConnectionString(username);
            Form targetForm = null;
            
            switch (role)
            {
                case "QuanLyRole":
                    targetForm = new MainForm(userConnectionString);
                    break;
                    
                case "KyThuatRole":
                    targetForm = new TechnicalMainForm(userConnectionString, username);
                    break;
                    
                case "NhanVienTrucRole":
                    targetForm = new StaffMainForm(userConnectionString, username);
                    break;
                    
                default:
                    MessageBox.Show($"Vai trò không được hỗ trợ: {role}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            if (targetForm != null)
            {
                // Có thể truyền thông tin user vào form nếu cần
                targetForm.Text += $" - Đăng nhập bởi: {username}";
                targetForm.ShowDialog();
            }
        }

        private string CreateUserConnectionString(string username)
        {
            // Lấy password từ database để tạo connection string
            string password = GetUserPassword(username);
            
            // Tạo connection string với SQL Server Authentication
            return $"Data Source=.;Initial Catalog=vc;User ID={username};Password={password};";
        }

        private string GetUserPassword(string username)
        {
            using (SqlConnection conn = new SqlConnection(adminConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Password FROM TaiKhoan WHERE Username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, e);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {   
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}