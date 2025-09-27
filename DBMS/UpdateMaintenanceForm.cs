using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class UpdateMaintenanceForm : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=vc;Integrated Security=True";
        private int baoTriID;
        private string currentStatus = "";

        public UpdateMaintenanceForm(int baoTriID)
        {
            InitializeComponent();
            this.baoTriID = baoTriID;
        }

        private void UpdateMaintenanceForm_Load(object sender, EventArgs e)
        {
            LoadMaintenanceInfo();
            LoadStatusOptions();
        }

        private void LoadMaintenanceInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT bt.BaoTriID, c.TenCSVC, bt.NgayYeuCau, bt.NoiDung, 
                                    bt.ChiPhi, bt.TrangThai
                                    FROM LichSuBaoTri bt 
                                    INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID 
                                    WHERE bt.BaoTriID = @BaoTriID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BaoTriID", baoTriID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblBaoTriID.Text = $"ID: {reader["BaoTriID"]}";
                                lblTenCSVC.Text = $"CSVC: {reader["TenCSVC"]}";
                                lblNgayYeuCau.Text = $"Ngày yêu cầu: {Convert.ToDateTime(reader["NgayYeuCau"]):dd/MM/yyyy}";
                                lblNoiDung.Text = $"Nội dung: {reader["NoiDung"]}";
                                lblChiPhiCu.Text = $"Chi phí hiện tại: {(reader["ChiPhi"] == DBNull.Value ? "Chưa có" : Convert.ToDecimal(reader["ChiPhi"]).ToString("N0"))}";
                                
                                currentStatus = reader["TrangThai"].ToString();
                                lblTrangThaiCu.Text = $"Trạng thái hiện tại: {currentStatus}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin bảo trì: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatusOptions()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Chờ xử lý");
            cboTrangThai.Items.Add("Đang xử lý");
            cboTrangThai.Items.Add("Hoàn thành");
            cboTrangThai.Items.Add("Hủy bỏ");
            
            // Set trạng thái hiện tại nếu có, nếu không thì mặc định "Chờ xử lý"
            if (!string.IsNullOrEmpty(currentStatus))
            {
                cboTrangThai.SelectedItem = currentStatus;
            }
            else
            {
                cboTrangThai.SelectedIndex = 0;
            }
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chọn "Hoàn thành", hiển thị các trường bổ sung
            bool isCompleted = cboTrangThai.SelectedItem?.ToString() == "Hoàn thành";
            
            lblNgayHoanThanh.Visible = isCompleted;
            dtpNgayHoanThanh.Visible = isCompleted;
            lblChiPhiThucTe.Visible = isCompleted;
            txtChiPhiThucTe.Visible = isCompleted;
            
            if (isCompleted)
            {
                dtpNgayHoanThanh.Value = DateTime.Now;
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
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateMaintenanceStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@BaoTriID", baoTriID);
                        cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem.ToString());
                        
                        // Ngày hoàn thành chỉ cần khi trạng thái là "Hoàn thành"
                        if (cboTrangThai.SelectedItem.ToString() == "Hoàn thành")
                        {
                            cmd.Parameters.AddWithValue("@NgayHoanThanh", dtpNgayHoanThanh.Value.Date);
                            
                            if (!string.IsNullOrEmpty(txtChiPhiThucTe.Text))
                            {
                                cmd.Parameters.AddWithValue("@ChiPhiThucTe", decimal.Parse(txtChiPhiThucTe.Text));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ChiPhiThucTe", DBNull.Value);
                            }
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@NgayHoanThanh", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ChiPhiThucTe", DBNull.Value);
                        }

                        if (!string.IsNullOrEmpty(txtGhiChu.Text))
                        {
                            cmd.Parameters.AddWithValue("@GhiChuCapNhat", txtGhiChu.Text.Trim());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@GhiChuCapNhat", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật trạng thái bảo trì thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật trạng thái bảo trì: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cboTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThai.Focus();
                return false;
            }

            if (cboTrangThai.SelectedItem.ToString() == "Hoàn thành" && !string.IsNullOrEmpty(txtChiPhiThucTe.Text))
            {
                if (!decimal.TryParse(txtChiPhiThucTe.Text, out decimal result) || result < 0)
                {
                    MessageBox.Show("Chi phí thực tế phải là số và không âm!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtChiPhiThucTe.Focus();
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