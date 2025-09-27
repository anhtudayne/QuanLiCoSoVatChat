using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class UpdateThanhLyForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";
        private int thanhLyID;

        public UpdateThanhLyForm(int thanhLyID)
        {
            InitializeComponent();
            this.thanhLyID = thanhLyID;
        }

        private void UpdateThanhLyForm_Load(object sender, EventArgs e)
        {
            LoadCurrentData();
        }

        private void LoadCurrentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT tl.*, c.TenCSVC 
                        FROM ThanhLyCSVC tl 
                        INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID 
                        WHERE tl.ThanhLyID = @ThanhLyID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ThanhLyID", thanhLyID);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblCSVCInfo.Text = $"CSVC: {reader["TenCSVC"]} (ID: {reader["CSVCID"]})";
                                txtLyDoThanhLy.Text = reader["LyDoThanhLy"].ToString();
                                
                                if (reader["GiaTriThanhLy"] != DBNull.Value)
                                    nudGiaTriThanhLy.Value = Convert.ToDecimal(reader["GiaTriThanhLy"]);
                                
                                if (reader["NgayThanhLy"] != DBNull.Value)
                                    dtpNgayThanhLy.Value = Convert.ToDateTime(reader["NgayThanhLy"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateAssetDisposal", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@ThanhLyID", thanhLyID);
                        cmd.Parameters.AddWithValue("@LyDoThanhLy", txtLyDoThanhLy.Text.Trim());
                        cmd.Parameters.AddWithValue("@GiaTriThanhLy", 
                            nudGiaTriThanhLy.Value == 0 ? (object)DBNull.Value : nudGiaTriThanhLy.Value);
                        cmd.Parameters.AddWithValue("@NgayThanhLy", dtpNgayThanhLy.Value.Date);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Cập nhật thông tin thanh lý thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLyDoThanhLy.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do thanh lý!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLyDoThanhLy.Focus();
                return false;
            }
            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}