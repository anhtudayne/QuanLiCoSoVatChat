using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class QuanLyCSVCForm : Form
    {
        // Connection string - kết nối tới database vc
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public QuanLyCSVCForm()
        {
            InitializeComponent();
        }

        private void QuanLyCSVCForm_Load(object sender, EventArgs e)
        {
            LoadCSVCData();
            SetupDataGridView();
        }

        #region Database Operations

        private void LoadCSVCData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Sử dụng view vw_CSVCOverview để hiển thị đầy đủ thông tin
                    string query = "SELECT * FROM vw_CSVCOverview ORDER BY CSVCID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    
                    dgvCSVC.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu CSVC: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvCSVC.DataSource == null) return;

            // Ẩn các cột không cần thiết
            string[] hiddenColumns = { "LoaiID", "ViTriID", "MoTaLoai", "Tang", "KhuVuc", 
                                     "MoTaViTri", "EmailNCC", "GhiChuSuDung", "SoNgayConBaoHanh",
                                     "TrangThaiThanhLy", "NgayThanhLy", "LyDoThanhLy", "GiaTriThanhLy" };
            
            foreach (string columnName in hiddenColumns)
            {
                if (dgvCSVC.Columns.Contains(columnName))
                {
                    dgvCSVC.Columns[columnName].Visible = false;
                }
            }

            // Đặt tên cột tiếng Việt
            if (dgvCSVC.Columns.Contains("CSVCID"))
                dgvCSVC.Columns["CSVCID"].HeaderText = "Mã CSVC";
            if (dgvCSVC.Columns.Contains("TenCSVC"))
                dgvCSVC.Columns["TenCSVC"].HeaderText = "Tên CSVC";
            if (dgvCSVC.Columns.Contains("MaCSVC"))
                dgvCSVC.Columns["MaCSVC"].HeaderText = "Mã số";
            if (dgvCSVC.Columns.Contains("TenLoai"))
                dgvCSVC.Columns["TenLoai"].HeaderText = "Loại";
            if (dgvCSVC.Columns.Contains("ViTri"))
                dgvCSVC.Columns["ViTri"].HeaderText = "Vị trí";
            if (dgvCSVC.Columns.Contains("GiaTri"))
                dgvCSVC.Columns["GiaTri"].HeaderText = "Giá trị";
            if (dgvCSVC.Columns.Contains("TinhTrang"))
                dgvCSVC.Columns["TinhTrang"].HeaderText = "Tình trạng";
            if (dgvCSVC.Columns.Contains("TenNhaCungCap"))
                dgvCSVC.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
            if (dgvCSVC.Columns.Contains("SoDienThoaiNCC"))
                dgvCSVC.Columns["SoDienThoaiNCC"].HeaderText = "SĐT NCC";
            if (dgvCSVC.Columns.Contains("NgayMua"))
                dgvCSVC.Columns["NgayMua"].HeaderText = "Ngày mua";
            if (dgvCSVC.Columns.Contains("NgayHetBaoHanh"))
                dgvCSVC.Columns["NgayHetBaoHanh"].HeaderText = "Hết bảo hành";
            if (dgvCSVC.Columns.Contains("TinhTrangBaoHanh"))
                dgvCSVC.Columns["TinhTrangBaoHanh"].HeaderText = "Bảo hành";
            if (dgvCSVC.Columns.Contains("GhiChu"))
                dgvCSVC.Columns["GhiChu"].HeaderText = "Ghi chú";

            // Thiết lập độ rộng cột
            if (dgvCSVC.Columns.Contains("CSVCID"))
                dgvCSVC.Columns["CSVCID"].Width = 80;
            if (dgvCSVC.Columns.Contains("TenCSVC"))
                dgvCSVC.Columns["TenCSVC"].Width = 200;
            if (dgvCSVC.Columns.Contains("MaCSVC"))
                dgvCSVC.Columns["MaCSVC"].Width = 100;
            if (dgvCSVC.Columns.Contains("TenLoai"))
                dgvCSVC.Columns["TenLoai"].Width = 120;
            if (dgvCSVC.Columns.Contains("ViTri"))
                dgvCSVC.Columns["ViTri"].Width = 150;
            if (dgvCSVC.Columns.Contains("GiaTri"))
            {
                dgvCSVC.Columns["GiaTri"].Width = 120;
                dgvCSVC.Columns["GiaTri"].DefaultCellStyle.Format = "N0";
                dgvCSVC.Columns["GiaTri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvCSVC.Columns.Contains("TinhTrang"))
                dgvCSVC.Columns["TinhTrang"].Width = 100;
            if (dgvCSVC.Columns.Contains("TinhTrangBaoHanh"))
                dgvCSVC.Columns["TinhTrangBaoHanh"].Width = 100;

            dgvCSVC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvCSVC.ReadOnly = true;
            dgvCSVC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCSVC.AllowUserToAddRows = false;
            dgvCSVC.RowHeadersVisible = false;
        }

        #endregion

        #region Button Events

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (AddCSVCForm addForm = new AddCSVCForm())
                {
                    if (addForm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadCSVCData(); // Refresh data
                        MessageBox.Show("Thêm CSVC thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvCSVC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn CSVC cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int csvcId = Convert.ToInt32(dgvCSVC.SelectedRows[0].Cells["CSVCID"].Value);
                
                using (EditCSVCForm editForm = new EditCSVCForm(csvcId))
                {
                    if (editForm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadCSVCData(); // Refresh data
                        MessageBox.Show("Cập nhật CSVC thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCSVC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn CSVC cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa CSVC này?", "Xác nhận xóa", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int csvcId = Convert.ToInt32(dgvCSVC.SelectedRows[0].Cells["CSVCID"].Value);
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteCSVC", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CSVCID", csvcId);
                        cmd.Parameters.AddWithValue("@ForceDelete", 0); // Không force delete
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                
                LoadCSVCData(); // Refresh data
                MessageBox.Show("Xóa CSVC thành công!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Không thể xóa CSVC: {sqlEx.Message}", "Lỗi ràng buộc", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SearchCSVCForm searchForm = new SearchCSVCForm())
                {
                    if (searchForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Apply search results to the current form
                        dgvCSVC.DataSource = searchForm.SearchResults;
                        SetupDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadCSVCData();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            ShowCSVCOverview();
        }

        #endregion

        #region Thống kê và Tổng quan

        private void ShowCSVCOverview()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string overview = "📊 TỔNG QUAN CƠ SỞ VẬT CHẤT\n";
                    overview += new string('=', 40) + "\n\n";
                    
                    // 1. fn_GetCSVCCountByType - Tổng số CSVC
                    SqlCommand cmd1 = new SqlCommand("SELECT dbo.fn_GetCSVCCountByType(NULL) AS TotalCSVC", conn);
                    int totalCSVC = Convert.ToInt32(cmd1.ExecuteScalar());
                    overview += $"📦 Tổng số CSVC: {totalCSVC:N0} thiết bị\n\n";
                    
                    // 2. fn_GetTotalAssetValue - Tổng giá trị tài sản
                    SqlCommand cmd2 = new SqlCommand("SELECT dbo.fn_GetTotalAssetValue(NULL, NULL, NULL, 1) AS TotalValue", conn);
                    decimal totalValue = Convert.ToDecimal(cmd2.ExecuteScalar());
                    overview += $"💰 Tổng giá trị: {totalValue:N0} VNĐ\n\n";
                    
                    // 3. fn_GetCSVCCountByStatus - Theo tình trạng
                    overview += "📊 Theo tình trạng:\n";
                    string[] statuses = { "Đang sử dụng", "Hỏng", "Đã thanh lý", "Bảo trì" };
                    foreach (string status in statuses)
                    {
                        SqlCommand cmdStatus = new SqlCommand($"SELECT dbo.fn_GetCSVCCountByStatus(N'{status}') AS Count", conn);
                        int count = Convert.ToInt32(cmdStatus.ExecuteScalar());
                        if (count > 0)
                        {
                            overview += $"   • {status}: {count:N0}\n";
                        }
                    }
                    overview += "\n";
                    
                    // 4. fn_GetCSVCCountByLocation - Theo vị trí
                    overview += "📍 Theo vị trí:\n";
                    SqlCommand cmdLocations = new SqlCommand("SELECT ViTriID, KhuVuc + N' - Tầng ' + CAST(Tang AS NVARCHAR(5)) AS TenViTri FROM ViTri ORDER BY KhuVuc, Tang", conn);
                    SqlDataAdapter locationAdapter = new SqlDataAdapter(cmdLocations);
                    DataTable locationTable = new DataTable();
                    locationAdapter.Fill(locationTable);
                    
                    foreach (DataRow row in locationTable.Rows)
                    {
                        int viTriID = Convert.ToInt32(row["ViTriID"]);
                        string tenViTri = row["TenViTri"].ToString();
                        
                        SqlCommand cmdLocationCount = new SqlCommand($"SELECT dbo.fn_GetCSVCCountByLocation({viTriID}) AS Count", conn);
                        int count = Convert.ToInt32(cmdLocationCount.ExecuteScalar());
                        if (count > 0)
                        {
                            overview += $"   • {tenViTri}: {count:N0}\n";
                        }
                    }
                    
                    // Hiển thị trong MessageBox
                    MessageBox.Show(overview, "📊 Tổng quan CSVC", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo tổng quan: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
