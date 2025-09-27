using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class AddDisposalForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";
        private int csvcID;
        private string tenCSVC;

        public AddDisposalForm(int csvcID, string tenCSVC)
        {
            InitializeComponent();
            this.csvcID = csvcID;
            this.tenCSVC = tenCSVC;
        }

        private void AddDisposalForm_Load(object sender, EventArgs e)
        {
            lblCSVCInfo.Text = $"CSVC: {tenCSVC} (ID: {csvcID})";
            dtpNgayThanhLy.Value = DateTime.Now;
            LoadNhanVienData();
        }

        private void LoadNhanVienData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT NhanVienID, HoTen, ChucVu 
                        FROM NhanVien 
                        WHERE TrangThai = N'Đang làm'
                        ORDER BY HoTen";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // Thêm dòng mặc định
                        DataRow defaultRow = dt.NewRow();
                        defaultRow["NhanVienID"] = -1;
                        defaultRow["HoTen"] = "-- Chọn người thực hiện --";
                        defaultRow["ChucVu"] = "";
                        dt.Rows.InsertAt(defaultRow, 0);
                        
                        cboNguoiThucHien.DisplayMember = "HoTen";
                        cboNguoiThucHien.ValueMember = "NhanVienID";
                        cboNguoiThucHien.DataSource = dt;
                        cboNguoiThucHien.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_AddAssetDisposal", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@CSVCID", csvcID);
                        cmd.Parameters.AddWithValue("@LyDoThanhLy", txtLyDoThanhLy.Text.Trim());
                        cmd.Parameters.AddWithValue("@GiaTriThanhLy", 
                            nudGiaTriThanhLy.Value == 0 ? (object)DBNull.Value : nudGiaTriThanhLy.Value);
                        cmd.Parameters.AddWithValue("@NguoiThucHienID", (int)cboNguoiThucHien.SelectedValue);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Thanh lý CSVC thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh lý CSVC: {ex.Message}", "Lỗi", 
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

            if (cboNguoiThucHien.SelectedValue == null || (int)cboNguoiThucHien.SelectedValue == -1)
            {
                MessageBox.Show("Vui lòng chọn người thực hiện thanh lý!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNguoiThucHien.Focus();
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