using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class ThanhLyForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";
        
        public ThanhLyForm()
        {
            InitializeComponent();
        }

        private void ThanhLyForm_Load(object sender, EventArgs e)
        {
            LoadThanhLyData();
            SetupDataGridView();
        }

        private void LoadThanhLyData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM vw_ThanhLy ORDER BY ThanhLyID DESC";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    dgvThanhLy.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu thanh lý: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SetupDataGridView()
        {
            if (dgvThanhLy.Columns.Count > 0)
            {
                dgvThanhLy.Columns["ThanhLyID"].HeaderText = "ID";
                dgvThanhLy.Columns["ThanhLyID"].Width = 50;
                dgvThanhLy.Columns["CSVCID"].HeaderText = "CSVC ID";
                dgvThanhLy.Columns["CSVCID"].Width = 80;
                dgvThanhLy.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                dgvThanhLy.Columns["TenCSVC"].Width = 200;
                dgvThanhLy.Columns["NgayThanhLy"].HeaderText = "Ngày thanh lý";
                dgvThanhLy.Columns["NgayThanhLy"].Width = 120;
                dgvThanhLy.Columns["LyDoThanhLy"].HeaderText = "Lý do thanh lý";
                dgvThanhLy.Columns["LyDoThanhLy"].Width = 200;
                dgvThanhLy.Columns["GiaTriThanhLy"].HeaderText = "Giá trị thanh lý";
                dgvThanhLy.Columns["GiaTriThanhLy"].Width = 120;
                dgvThanhLy.Columns["GiaTriGoc"].HeaderText = "Giá trị gốc";
                dgvThanhLy.Columns["GiaTriGoc"].Width = 120;
                
                // Format currency columns
                dgvThanhLy.Columns["GiaTriThanhLy"].DefaultCellStyle.Format = "N0";
                dgvThanhLy.Columns["GiaTriGoc"].DefaultCellStyle.Format = "N0";
                dgvThanhLy.Columns["NgayThanhLy"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void btnThanhLy_Click(object sender, EventArgs e)
        {
            // Mở form chọn CSVC để thanh lý
            using (var selectForm = new SelectCSVCForDisposalForm())
            {
                if (selectForm.ShowDialog() == DialogResult.OK)
                {
                    LoadThanhLyData(); // Refresh data
                }
            }
        }

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            // Mở form tìm kiếm lịch sử thanh lý
            using (var searchForm = new SearchThanhLyForm())
            {
                searchForm.ShowDialog();
            }
        }
        
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvThanhLy.SelectedRows.Count > 0)
            {
                int thanhLyID = Convert.ToInt32(dgvThanhLy.SelectedRows[0].Cells["ThanhLyID"].Value);
                
                using (var updateForm = new UpdateThanhLyForm(thanhLyID))
                {
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadThanhLyData(); // Refresh data
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadThanhLyData();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            ShowDisposalStatistics();
        }

        private void ShowDisposalStatistics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Gọi các function để lấy thống kê
                    string queryTotalValue = "SELECT dbo.fn_GetTotalDisposalValue()";
                    string queryCount = "SELECT dbo.fn_GetDisposalCount()";
                    
                    SqlCommand cmdTotalValue = new SqlCommand(queryTotalValue, conn);
                    SqlCommand cmdCount = new SqlCommand(queryCount, conn);
                    
                    decimal totalValue = (decimal)cmdTotalValue.ExecuteScalar();
                    int count = (int)cmdCount.ExecuteScalar();
                    
                    // Hiển thị thông tin trong MessageBox
                    string message = $"THỐNG KÊ THANH LÝ CSVC\n\n" +
                                   $"📋 Tổng số CSVC đã thanh lý: {count:N0} cái\n" +
                                   $"💰 Tổng giá trị thanh lý: {totalValue:N0} VNĐ";
                    
                    MessageBox.Show(message, "Chi tiết thanh lý", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
