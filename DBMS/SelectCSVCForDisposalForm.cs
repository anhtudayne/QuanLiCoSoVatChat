using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class SelectCSVCForDisposalForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";
        public int SelectedCSVCID { get; private set; }

        public SelectCSVCForDisposalForm()
        {
            InitializeComponent();
        }

        private void SelectCSVCForDisposalForm_Load(object sender, EventArgs e)
        {
            LoadCSVCData();
        }

        private void LoadCSVCData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Sử dụng view vw_CSVCForDisposal
                    string query = "SELECT * FROM vw_CSVCForDisposal ORDER BY CSVCID ASC";
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    dgvCSVC.DataSource = dt;
                    SetupDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvCSVC.Columns.Count > 0)
            {
                dgvCSVC.Columns["CSVCID"].HeaderText = "ID";
                dgvCSVC.Columns["CSVCID"].Width = 50;
                dgvCSVC.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                dgvCSVC.Columns["TenCSVC"].Width = 200;
                dgvCSVC.Columns["MaCSVC"].HeaderText = "Mã CSVC";
                dgvCSVC.Columns["MaCSVC"].Width = 100;
                dgvCSVC.Columns["TenLoai"].HeaderText = "Loại";
                dgvCSVC.Columns["TenLoai"].Width = 130;
                dgvCSVC.Columns["GiaTri"].HeaderText = "Giá trị";
                dgvCSVC.Columns["GiaTri"].Width = 100;
                dgvCSVC.Columns["GiaTri"].DefaultCellStyle.Format = "N0";
                dgvCSVC.Columns["TinhTrang"].HeaderText = "Tình trạng";
                dgvCSVC.Columns["TinhTrang"].Width = 120;
                dgvCSVC.Columns["TenViTri"].HeaderText = "Vị trí";
                dgvCSVC.Columns["TenViTri"].Width = 120;
                dgvCSVC.Columns["NgayMua"].HeaderText = "Ngày mua";
                dgvCSVC.Columns["NgayMua"].Width = 100;
                dgvCSVC.Columns["NgayMua"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dgvCSVC.SelectedRows.Count > 0)
            {
                SelectedCSVCID = Convert.ToInt32(dgvCSVC.SelectedRows[0].Cells["CSVCID"].Value);
                string tenCSVC = dgvCSVC.SelectedRows[0].Cells["TenCSVC"].Value.ToString();
                
                // Mở form nhập thông tin thanh lý
                using (var disposalForm = new AddDisposalForm(SelectedCSVCID, tenCSVC))
                {
                    if (disposalForm.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn CSVC cần thanh lý!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvCSVC_DoubleClick(object sender, EventArgs e)
        {
            btnChon_Click(sender, e);
        }
    }
}