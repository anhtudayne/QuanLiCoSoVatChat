using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class SearchCSVCForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        public DataTable SearchResults { get; private set; }

        public SearchCSVCForm()
        {
            InitializeComponent();
        }

        private void SearchCSVCForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Load Loại CSVC
                    using (SqlCommand cmd = new SqlCommand("SELECT LoaiID, TenLoai FROM LoaiCSVC ORDER BY TenLoai", conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dtLoai = new DataTable();
                        adapter.Fill(dtLoai);
                        
                        // Add empty row for "All" - Use -1 instead of DBNull
                        DataRow emptyRow = dtLoai.NewRow();
                        emptyRow["LoaiID"] = -1;
                        emptyRow["TenLoai"] = "-- Tất cả loại --";
                        dtLoai.Rows.InsertAt(emptyRow, 0);
                        
                        cboLoai.DataSource = dtLoai;
                        cboLoai.DisplayMember = "TenLoai";
                        cboLoai.ValueMember = "LoaiID";
                        cboLoai.SelectedIndex = 0;
                    }

                    // Load Tình trạng using function
                    SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT TinhTrang, DisplayText FROM dbo.fn_GetCSVCStatuses()", conn);
                    DataTable dt3 = new DataTable();
                    adapter3.Fill(dt3);

                    // Add "All" option at the beginning - Use empty string instead of DBNull
                    DataRow allRow = dt3.NewRow();
                    allRow["TinhTrang"] = "";
                    allRow["DisplayText"] = "-- Tất cả tình trạng --";
                    dt3.Rows.InsertAt(allRow, 0);

                    cboTinhTrang.DataSource = dt3;
                    cboTinhTrang.DisplayMember = "DisplayText";
                    cboTinhTrang.ValueMember = "TinhTrang";
                    cboTinhTrang.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu ComboBox: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SearchCSVC", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        // Tên CSVC
                        if (!string.IsNullOrWhiteSpace(txtTenCSVC.Text))
                            cmd.Parameters.AddWithValue("@TenCSVC", txtTenCSVC.Text.Trim());
                        else
                            cmd.Parameters.AddWithValue("@TenCSVC", DBNull.Value);
                        
                        // Loại
                        if (cboLoai.SelectedValue != null && Convert.ToInt32(cboLoai.SelectedValue) != -1)
                            cmd.Parameters.AddWithValue("@LoaiID", cboLoai.SelectedValue);
                        else
                            cmd.Parameters.AddWithValue("@LoaiID", DBNull.Value);
                        
                        // Tình trạng
                        if (cboTinhTrang.SelectedValue != null && cboTinhTrang.SelectedValue.ToString() != "")
                            cmd.Parameters.AddWithValue("@TinhTrang", cboTinhTrang.SelectedValue);
                        else
                            cmd.Parameters.AddWithValue("@TinhTrang", DBNull.Value);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        SearchResults = new DataTable();
                        adapter.Fill(SearchResults);
                        
                        if (SearchResults.Rows.Count > 0)
                        {
                            lblKetQua.Text = $"🔍 Tìm thấy {SearchResults.Rows.Count} kết quả";
                            lblKetQua.ForeColor = System.Drawing.Color.Green;
                            btnChon.Enabled = true;
                        }
                        else
                        {
                            lblKetQua.Text = "❌ Không tìm thấy kết quả nào";
                            lblKetQua.ForeColor = System.Drawing.Color.Red;
                            btnChon.Enabled = false;
                        }
                        
                        dgvKetQua.DataSource = SearchResults;
                        
                        // Format DataGridView
                        if (dgvKetQua.Columns.Count > 0)
                        {
                            if (dgvKetQua.Columns.Contains("CSVCID"))
                                dgvKetQua.Columns["CSVCID"].Visible = false;
                            if (dgvKetQua.Columns.Contains("TenCSVC"))
                                dgvKetQua.Columns["TenCSVC"].HeaderText = "Tên CSVC";
                            if (dgvKetQua.Columns.Contains("MaCSVC"))
                                dgvKetQua.Columns["MaCSVC"].HeaderText = "Mã số";
                            if (dgvKetQua.Columns.Contains("LoaiID"))
                                dgvKetQua.Columns["LoaiID"].Visible = false;
                            if (dgvKetQua.Columns.Contains("TenLoai"))
                                dgvKetQua.Columns["TenLoai"].HeaderText = "Loại";
                            if (dgvKetQua.Columns.Contains("ViTriID"))
                                dgvKetQua.Columns["ViTriID"].Visible = false;
                            if (dgvKetQua.Columns.Contains("ViTri"))
                                dgvKetQua.Columns["ViTri"].HeaderText = "Vị trí";
                            if (dgvKetQua.Columns.Contains("GiaTri"))
                                dgvKetQua.Columns["GiaTri"].HeaderText = "Giá trị";
                            if (dgvKetQua.Columns.Contains("TinhTrang"))
                                dgvKetQua.Columns["TinhTrang"].HeaderText = "Tình trạng";
                            if (dgvKetQua.Columns.Contains("TenNhaCungCap"))
                                dgvKetQua.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
                            
                            // Auto resize columns
                            dgvKetQua.AutoResizeColumns();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenCSVC.Clear();
            cboLoai.SelectedIndex = 0;
            cboTinhTrang.SelectedIndex = 0;
            dgvKetQua.DataSource = null;
            lblKetQua.Text = "👆 Nhập thông tin tìm kiếm và nhấn 'Tìm kiếm'";
            lblKetQua.ForeColor = System.Drawing.Color.Black;
            btnChon.Enabled = false;
            SearchResults = null;
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.SelectedRows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng từ kết quả tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public DataRow GetSelectedRow()
        {
            if (dgvKetQua.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvKetQua.SelectedRows[0].Index;
                return SearchResults.Rows[selectedIndex];
            }
            return null;
        }

        private void dgvKetQua_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && SearchResults != null && SearchResults.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}