using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class BaoTriForm : Form
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=vc;Integrated Security=True";

        public BaoTriForm()
        {
            InitializeComponent();
        }

        private void BaoTriForm_Load(object sender, EventArgs e)
        {
            LoadBaoTriData();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvBaoTri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBaoTri.MultiSelect = false;
            dgvBaoTri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBaoTri.RowHeadersVisible = false;
            
            // Định dạng cột
            if (dgvBaoTri.Columns.Count > 0)
            {
                dgvBaoTri.Columns["BaoTriID"].HeaderText = "ID";
                dgvBaoTri.Columns["CSVCID"].HeaderText = "CSVC ID";
                dgvBaoTri.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                dgvBaoTri.Columns["NgayYeuCau"].HeaderText = "Ngày yêu cầu";
                dgvBaoTri.Columns["NgayHoanThanh"].HeaderText = "Ngày hoàn thành";
                dgvBaoTri.Columns["NoiDung"].HeaderText = "Nội dung";
                dgvBaoTri.Columns["ChiPhi"].HeaderText = "Chi phí";
                dgvBaoTri.Columns["TrangThai"].HeaderText = "Trạng thái";
            }
        }

        private void LoadBaoTriData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM vw_BaoTri ORDER BY BaoTriID DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBaoTri.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemBaoTri_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddMaintenanceForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBaoTriData();
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvBaoTri.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi để cập nhật!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int baoTriID = Convert.ToInt32(dgvBaoTri.SelectedRows[0].Cells["BaoTriID"].Value);

            using (var updateForm = new UpdateMaintenanceForm(baoTriID))
            {
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBaoTriData();
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (var searchForm = new SearchMaintenanceForm())
            {
                searchForm.ShowDialog();
                // Form tìm kiếm sẽ tự xử lý việc hiển thị kết quả
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (var thongKeForm = new MaintenanceStatisticsForm())
            {
                thongKeForm.ShowDialog();
            }
        }

        private void btnLichDinhKy_Click(object sender, EventArgs e)
        {
            using (var lichDinhKyForm = new LichBaoTriDinhKyForm())
            {
                lichDinhKyForm.ShowDialog();
            }
        }

        // Nút làm mới để hiển thị lại tất cả dữ liệu
        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadBaoTriData();
        }
    }
}
