using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class SearchThanhLyForm : Form
    {
        private string connectionString = "Server=.;Database=vc;Integrated Security=true;";

        public SearchThanhLyForm()
        {
            InitializeComponent();
        }

        private void SearchThanhLyForm_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void LoadAllData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SearchAssetDisposal", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvKetQua.DataSource = dt;
                        SetupDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvKetQua.Columns.Count > 0)
            {
                dgvKetQua.Columns["ThanhLyID"].HeaderText = "ID";
                dgvKetQua.Columns["ThanhLyID"].Width = 60;
                dgvKetQua.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                dgvKetQua.Columns["TenCSVC"].Width = 200;
                dgvKetQua.Columns["NgayThanhLy"].HeaderText = "Ngày thanh lý";
                dgvKetQua.Columns["NgayThanhLy"].Width = 120;
                dgvKetQua.Columns["NgayThanhLy"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvKetQua.Columns["LyDoThanhLy"].HeaderText = "Lý do";
                dgvKetQua.Columns["LyDoThanhLy"].Width = 250;
                dgvKetQua.Columns["GiaTriThanhLy"].HeaderText = "Giá trị thanh lý";
                dgvKetQua.Columns["GiaTriThanhLy"].Width = 120;
                dgvKetQua.Columns["GiaTriThanhLy"].DefaultCellStyle.Format = "N0";
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SearchAssetDisposal", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        // Add search parameters
                        if (!string.IsNullOrWhiteSpace(txtCSVCID.Text))
                        {
                            if (int.TryParse(txtCSVCID.Text, out int csvcId))
                                cmd.Parameters.AddWithValue("@CSVCID", csvcId);
                        }
                        
                        if (nudGiaTriThanhLy.Value > 0)
                        {
                            cmd.Parameters.AddWithValue("@GiaTriThanhLy", nudGiaTriThanhLy.Value);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvKetQua.DataSource = dt;
                        
                        lblKetQua.Text = $"Tìm thấy {dt.Rows.Count} kết quả";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtCSVCID.Clear();
            nudGiaTriThanhLy.Value = 0;
            lblKetQua.Text = "";
            LoadAllData();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}