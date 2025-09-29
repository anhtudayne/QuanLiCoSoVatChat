using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class AccountManagementForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";

        public AccountManagementForm()
        {
            InitializeComponent();
        }

        private void AccountManagementForm_Load(object sender, EventArgs e)
        {
            LoadNhanVienToComboBox();
            LoadAccountsList();
        }

        private string ConvertChucVuToRole(string chucVu)
        {
            if (string.IsNullOrEmpty(chucVu))
                return "Nhân Viên Trực";

            string chucVuLower = chucVu.ToLower().Trim();
            
            // Mapping chức vụ sang vai trò
            if (chucVuLower.Contains("kỹ thuật") || chucVuLower.Contains("kỷ thuật") || 
                chucVuLower.Contains("technical") || chucVuLower.Contains("technician") ||
                chucVuLower.Contains("bảo trì") || chucVuLower.Contains("maintenance"))
            {
                return "Nhân Viên Kỹ Thuật";
            }
            
            // Mặc định là Nhân viên trực
            return "Nhân Viên Trực";
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedIndex >= 0)
            {
                DataRowView selectedRow = (DataRowView)cboNhanVien.SelectedItem;
                string chucVu = selectedRow["ChucVu"].ToString();
                string role = ConvertChucVuToRole(chucVu);
                
                lblRoleDisplay.Text = role;
                lblRoleDisplay.Tag = role; // Lưu vai trò để sử dụng khi tạo tài khoản
            }
            else
            {
                lblRoleDisplay.Text = "Chưa chọn";
                lblRoleDisplay.Tag = null;
            }
        }

        private void LoadNhanVienToComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT NhanVienID, HoTen, ChucVu FROM NhanVien WHERE TrangThai = N'Đang làm' ORDER BY HoTen";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    cboNhanVien.DisplayMember = "HoTen";
                    cboNhanVien.ValueMember = "NhanVienID";
                    cboNhanVien.DataSource = dt;
                    cboNhanVien.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAccountsList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            t.TaiKhoanID,
                            t.Username,
                            t.Role,
                            ISNULL(n.HoTen, N'Không có') AS TenNhanVien
                        FROM TaiKhoan t
                        LEFT JOIN NhanVien n ON t.NhanVienID = n.NhanVienID
                        ORDER BY t.Role, t.Username";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    dgvAccounts.DataSource = dt;
                    SetupDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài khoản: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvAccounts.Columns.Count > 0)
            {
                dgvAccounts.Columns["TaiKhoanID"].Visible = false;
                dgvAccounts.Columns["Username"].HeaderText = "Tên đăng nhập";
                dgvAccounts.Columns["Role"].HeaderText = "Vai trò";
                dgvAccounts.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
                
                dgvAccounts.Columns["Username"].Width = 150;
                dgvAccounts.Columns["Role"].Width = 200;
                dgvAccounts.Columns["TenNhanVien"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lblRoleDisplay.Tag == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xác định vai trò!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CreateAccount();
        }

        private void CreateAccount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_AddTaiKhoan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@Role", lblRoleDisplay.Tag.ToString());
                        cmd.Parameters.AddWithValue("@NhanVienID", cboNhanVien.SelectedValue);
                        
                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultParam);
                        
                        SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(messageParam);
                        
                        cmd.ExecuteNonQuery();
                        
                        int result = (int)resultParam.Value;
                        string message = messageParam.Value?.ToString() ?? "";
                        
                        if (result == 1)
                        {
                            MessageBox.Show(message, "Thành công", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearInputs();
                            LoadAccountsList();
                        }
                        else
                        {
                            MessageBox.Show(message, "Lỗi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo tài khoản: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần reset mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dgvAccounts.SelectedRows[0].Cells["Username"].Value.ToString();
            ResetPassword(username);
        }

        private void ResetPassword(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ResetPassword", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@NewPassword", txtNewPassword.Text);
                        
                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultParam);
                        
                        SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(messageParam);
                        
                        cmd.ExecuteNonQuery();
                        
                        int result = (int)resultParam.Value;
                        string message = messageParam.Value?.ToString() ?? "";
                        
                        MessageBox.Show(message, result == 1 ? "Thành công" : "Lỗi", 
                            MessageBoxButtons.OK, 
                            result == 1 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                        
                        if (result == 1)
                        {
                            txtNewPassword.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset mật khẩu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cboNhanVien.SelectedIndex = -1;
            lblRoleDisplay.Text = "Chưa chọn";
            lblRoleDisplay.Tag = null;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAccountsList();
        }
    }
}