using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class SearchMaintenanceForm : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=vc;Integrated Security=True";

        public SearchMaintenanceForm()
        {
            InitializeComponent();
        }

        private void SearchMaintenanceForm_Load(object sender, EventArgs e)
        {
            LoadCSVCList();
            // Thiết lập giá trị mặc định
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
        }

        private void LoadCSVCList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetCSVCInMaintenanceHistory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Thêm option "Tất cả"
                        DataRow allRow = dt.NewRow();
                        allRow["CSVCID"] = 0;
                        allRow["TenCSVC"] = "-- Tất cả CSVC --";
                        dt.Rows.InsertAt(allRow, 0);

                        cboCSVC.DataSource = dt;
                        cboCSVC.DisplayMember = "TenCSVC";
                        cboCSVC.ValueMember = "CSVCID";
                        cboCSVC.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách CSVC: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SearchMaintenanceHistory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameter TenCSVC (từ CSVCID được chọn)
                        if (cboCSVC.SelectedValue != null && Convert.ToInt32(cboCSVC.SelectedValue) > 0)
                        {
                            string tenCSVC = cboCSVC.Text;
                            if (tenCSVC != "-- Tất cả CSVC --")
                            {
                                cmd.Parameters.AddWithValue("@TenCSVC", tenCSVC);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TenCSVC", DBNull.Value);
                            }
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TenCSVC", DBNull.Value);
                        }

                        // Parameter ngày
                        cmd.Parameters.AddWithValue("@TuNgay", dtpTuNgay.Value.Date);
                        cmd.Parameters.AddWithValue("@DenNgay", dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1));

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvResults.DataSource = dt;
                        FormatDataGridView();

                        lblResultCount.Text = $"Tìm thấy {dt.Rows.Count} kết quả";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvResults.Columns.Count > 0)
            {
                dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                // Đặt tên tiêu đề cột
                if (dgvResults.Columns.Contains("TenCSVC"))
                    dgvResults.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                
                if (dgvResults.Columns.Contains("TrangThai"))
                    dgvResults.Columns["TrangThai"].HeaderText = "Trạng thái";

                if (dgvResults.Columns.Contains("SoNgayXuLy"))
                    dgvResults.Columns["SoNgayXuLy"].HeaderText = "Số ngày xử lý";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Reset các control về giá trị mặc định
            cboCSVC.SelectedIndex = 0;
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
            dgvResults.DataSource = null;
            lblResultCount.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không có BaoTriID trong kết quả trả về từ stored procedure hiện tại
            // Chức năng này tạm thời bị vô hiệu hóa
            MessageBox.Show("Chức năng chỉnh sửa từ đây chưa được hỗ trợ.\nVui lòng sử dụng form bảo trì chính để cập nhật.", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}