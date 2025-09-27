using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class AddMaintenanceForm : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=vc;Integrated Security=True";

        public AddMaintenanceForm()
        {
            InitializeComponent();
        }

        private void AddMaintenanceForm_Load(object sender, EventArgs e)
        {
            LoadCSVCList();
            LoadNhanVienList();
        }

        private void LoadCSVCList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetAvailableCSVCForMaintenance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        cboCSVC.DataSource = dt;
                        cboCSVC.DisplayMember = "TenCSVC";
                        cboCSVC.ValueMember = "CSVCID";
                        cboCSVC.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNhanVienList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT NhanVienID, HoTen FROM NhanVien WHERE TrangThai = N'Đang làm'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Load cho ComboBox Giám sát
                    cboNhanVienGiamSat.DataSource = dt.Copy();
                    cboNhanVienGiamSat.DisplayMember = "HoTen";
                    cboNhanVienGiamSat.ValueMember = "NhanVienID";
                    cboNhanVienGiamSat.SelectedIndex = -1;

                    // Load cho ComboBox Kỹ thuật
                    cboNhanVienKyThuat.DataSource = dt.Copy();
                    cboNhanVienKyThuat.DisplayMember = "HoTen";
                    cboNhanVienKyThuat.ValueMember = "NhanVienID";
                    cboNhanVienKyThuat.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_AddMaintenanceRequest", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CSVCID", cboCSVC.SelectedValue);
                        cmd.Parameters.AddWithValue("@NoiDung", txtNoiDung.Text.Trim());
                        cmd.Parameters.AddWithValue("@NhanVienGiamSatID", 
                            cboNhanVienGiamSat.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NhanVienKyThuatID", 
                            cboNhanVienKyThuat.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ChiPhiDuKien", 
                            string.IsNullOrEmpty(txtChiPhiDuKien.Text) ? DBNull.Value : (object)decimal.Parse(txtChiPhiDuKien.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm yêu cầu bảo trì thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm yêu cầu bảo trì: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cboCSVC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn CSVC!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCSVC.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung bảo trì!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtChiPhiDuKien.Text))
            {
                if (!decimal.TryParse(txtChiPhiDuKien.Text, out decimal result) || result < 0)
                {
                    MessageBox.Show("Chi phí dự kiến phải là số và không âm!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtChiPhiDuKien.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}