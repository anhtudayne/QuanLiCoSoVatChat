using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class LichBaoTriDinhKyForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public LichBaoTriDinhKyForm()
        {
            InitializeComponent();
        }

        private void LichBaoTriDinhKyForm_Load(object sender, EventArgs e)
        {
            LoadLichBaoTriDinhKyData();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvLichBaoTri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichBaoTri.MultiSelect = false;
            dgvLichBaoTri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLichBaoTri.RowHeadersVisible = false;
            dgvLichBaoTri.AllowUserToAddRows = false;
            dgvLichBaoTri.AllowUserToDeleteRows = false;
            dgvLichBaoTri.ReadOnly = true;
            
            // Đặt màu cho header
            dgvLichBaoTri.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 44, 51);
            dgvLichBaoTri.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichBaoTri.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            
            // Màu xen kẽ cho rows
            dgvLichBaoTri.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvLichBaoTri.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            
            // Định dạng cột
            if (dgvLichBaoTri.Columns.Count > 0)
            {
                dgvLichBaoTri.Columns["LichID"].HeaderText = "ID";
                dgvLichBaoTri.Columns["LichID"].Width = 60;
                dgvLichBaoTri.Columns["CSVCID"].HeaderText = "CSVC ID";
                dgvLichBaoTri.Columns["CSVCID"].Width = 80;
                dgvLichBaoTri.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                dgvLichBaoTri.Columns["TenCSVC"].Width = 250;
                dgvLichBaoTri.Columns["ChuKyBaoTri_Thang"].HeaderText = "Chu kỳ (tháng)";
                dgvLichBaoTri.Columns["ChuKyBaoTri_Thang"].Width = 130;
                dgvLichBaoTri.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
                dgvLichBaoTri.Columns["NgayBatDau"].Width = 130;
                dgvLichBaoTri.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvLichBaoTri.Columns["TrangThai"].Width = 130;

                // Định dạng ngày
                dgvLichBaoTri.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
                
                // Căn giữa
                dgvLichBaoTri.Columns["LichID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvLichBaoTri.Columns["CSVCID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvLichBaoTri.Columns["ChuKyBaoTri_Thang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                // Màu cho trạng thái
                dgvLichBaoTri.CellFormatting += DgvLichBaoTri_CellFormatting;
            }
        }

        private void DgvLichBaoTri_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLichBaoTri.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                string trangThai = e.Value?.ToString();
                switch (trangThai)
                {
                    case "Quá hạn":
                        e.CellStyle.BackColor = Color.FromArgb(255, 205, 210);
                        e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                        break;
                    case "Sắp đến hạn":
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 224);
                        e.CellStyle.ForeColor = Color.FromArgb(239, 108, 0);
                        break;
                    case "Chưa đến hạn":
                        e.CellStyle.BackColor = Color.FromArgb(200, 230, 201);
                        e.CellStyle.ForeColor = Color.FromArgb(46, 125, 50);
                        break;
                }
            }
        }

        private void LoadLichBaoTriDinhKyData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM vw_LichBaoTriDinhKy ORDER BY CSVCID DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvLichBaoTri.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaoYeuCauBaoTri_Click(object sender, EventArgs e)
        {
            if (dgvLichBaoTri.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lịch bảo trì để tạo yêu cầu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int lichID = Convert.ToInt32(dgvLichBaoTri.SelectedRows[0].Cells["LichID"].Value);
            string tenCSVC = dgvLichBaoTri.SelectedRows[0].Cells["TenCSVC"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn tạo yêu cầu bảo trì định kỳ cho:\n{tenCSVC}?", 
                "Xác nhận tạo yêu cầu", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_CreateMaintenanceFromSchedule", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@LichID", lichID);
                            
                            cmd.ExecuteNonQuery();
                            
                            MessageBox.Show("Đã tạo yêu cầu bảo trì định kỳ thành công!", "Thành công", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tạo yêu cầu bảo trì: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoaLichBaoTri_Click(object sender, EventArgs e)
        {
            if (dgvLichBaoTri.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lịch bảo trì để xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int lichID = Convert.ToInt32(dgvLichBaoTri.SelectedRows[0].Cells["LichID"].Value);
            string tenCSVC = dgvLichBaoTri.SelectedRows[0].Cells["TenCSVC"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa lịch bảo trì định kỳ cho:\n{tenCSVC}?\n\nHành động này không thể hoàn tác!", 
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteMaintenanceSchedule", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@LichID", lichID);
                            
                            cmd.ExecuteNonQuery();
                            
                            MessageBox.Show("Đã xóa lịch bảo trì thành công!", "Thành công", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            LoadLichBaoTriDinhKyData(); // Refresh data
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa lịch bảo trì: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadLichBaoTriDinhKyData();
        }
    }
}