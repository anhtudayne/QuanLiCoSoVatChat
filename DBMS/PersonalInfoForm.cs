using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class PersonalInfoForm : Form
    {
        private string connectionString;
        private string username;

        public PersonalInfoForm(string connectionString, string username)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.username = username;
        }

        private void PersonalInfoForm_Load(object sender, EventArgs e)
        {
            LoadPersonalInfo();
        }

        private void LoadPersonalInfo()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // First get NhanVienID
                    string getNhanVienIdQuery = "SELECT dbo.fn_GetNhanVienIDByUsername(@Username)";
                    int nhanVienId;
                    
                    using (SqlCommand cmd = new SqlCommand(getNhanVienIdQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        object result = cmd.ExecuteScalar();
                        nhanVienId = Convert.ToInt32(result);
                    }

                    if (nhanVienId == 0)
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Get detailed employee information
                    string getInfoQuery = "SELECT * FROM dbo.fn_GetNhanVienInfo(@NhanVienID)";
                    using (SqlCommand cmd = new SqlCommand(getInfoQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                PopulateForm(reader);
                            }
                            else
                            {
                                MessageBox.Show("Không thể tải thông tin nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateForm(SqlDataReader reader)
        {
            txtNhanVienID.Text = reader["NhanVienID"].ToString();
            txtTenNhanVien.Text = reader["TenNhanVien"].ToString();
            txtUsername.Text = reader["Username"]?.ToString() ?? "";
            txtVaiTro.Text = reader["VaiTro"]?.ToString() ?? "";
            
            if (reader["NgaySinh"] != DBNull.Value)
                dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]);
            else
                dtpNgaySinh.Value = DateTime.Now; // Giá trị mặc định
            
            txtGioiTinh.Text = reader["GioiTinh"]?.ToString() ?? "";
            txtSoDienThoai.Text = reader["SoDienThoai"]?.ToString() ?? "";
            txtEmail.Text = reader["Email"]?.ToString() ?? "";
            txtDiaChi.Text = reader["DiaChi"]?.ToString() ?? "";
            txtChucVu.Text = reader["ChucVu"]?.ToString() ?? "";
            txtTrangThai.Text = reader["TrangThai"]?.ToString() ?? "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}