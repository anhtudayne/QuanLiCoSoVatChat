using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    public partial class AddShiftForm : Form
    {
        private string connectionString = @"Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        
        public AddShiftForm()
        {
            InitializeComponent();
            LoadFormData();
        }

        private void LoadFormData()
        {
            try
            {
                LoadEmployees();
                LoadRoles();
                
                // Set default values
                dtpWorkDate.Value = DateTime.Now.Date.AddDays(1); // Tomorrow
                dtpWorkDate.MinDate = DateTime.Now.Date.AddDays(1); // Cannot select past dates
                
                // Set default times
                dtpStartTime.Value = DateTime.Today.AddHours(8); // 8:00 AM
                dtpEndTime.Value = DateTime.Today.AddHours(16);   // 4:00 PM
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT NhanVienID, HoTen FROM NhanVien WHERE TrangThai = N'Đang làm' ORDER BY HoTen", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbEmployee.DataSource = dt;
                    cmbEmployee.DisplayMember = "HoTen";
                    cmbEmployee.ValueMember = "NhanVienID";
                    cmbEmployee.SelectedIndex = -1;
                }
            }
        }

        private void LoadRoles()
        {
            // Load common roles
            var roles = new List<string>
            {
                "Nhân viên trực",
                "Nhân viên kỹ thuật",
            };

            cmbRole.DataSource = roles;
            cmbRole.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (cmbEmployee.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbEmployee.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtShiftName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên ca!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtShiftName.Focus();
                    return;
                }

                if (cmbRole.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRole.Focus();
                    return;
                }

                // Validate time
                if (dtpStartTime.Value.TimeOfDay >= dtpEndTime.Value.TimeOfDay)
                {
                    MessageBox.Show("Giờ bắt đầu phải nhỏ hơn giờ kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpStartTime.Focus();
                    return;
                }

                // Call stored procedure to insert new shift
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertWorkShift", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        cmd.Parameters.AddWithValue("@NhanVienID", cmbEmployee.SelectedValue);
                        cmd.Parameters.AddWithValue("@NgayLamViec", dtpWorkDate.Value.Date);
                        cmd.Parameters.AddWithValue("@TenCa", txtShiftName.Text.Trim());
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpStartTime.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpEndTime.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@VaiTroTrongCa", cmbRole.SelectedValue.ToString());
                        
                        if (!string.IsNullOrWhiteSpace(txtNotes.Text))
                            cmd.Parameters.AddWithValue("@GhiChu", txtNotes.Text.Trim());
                        else
                            cmd.Parameters.AddWithValue("@GhiChu", DBNull.Value);

                        // Output parameters
                        SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                        SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                        
                        cmd.Parameters.Add(successParam);
                        cmd.Parameters.Add(messageParam);

                        cmd.ExecuteNonQuery();

                        bool success = (bool)successParam.Value;
                        string message = messageParam.Value.ToString();

                        if (success)
                        {
                            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phân ca: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}