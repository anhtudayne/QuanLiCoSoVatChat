using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class EditCSVCForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";
        private int csvfId;

        public EditCSVCForm(int csvfId)
        {
            InitializeComponent();
            this.csvfId = csvfId;
        }

        private void EditCSVCForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadCSVCData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Load Loại CSVC
                    SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT LoaiID, TenLoai FROM LoaiCSVC", conn);
                    DataTable dt1 = new DataTable();
                    adapter1.Fill(dt1);

                    cboLoai.DataSource = dt1;
                    cboLoai.DisplayMember = "TenLoai";
                    cboLoai.ValueMember = "LoaiID";

                    // Load Vị trí
                    SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT ViTriID, KhuVuc + N' - Tầng ' + CAST(Tang AS NVARCHAR(5)) AS TenViTri FROM ViTri", conn);
                    DataTable dt2 = new DataTable();
                    adapter2.Fill(dt2);

                    cboViTri.DataSource = dt2;
                    cboViTri.DisplayMember = "TenViTri";
                    cboViTri.ValueMember = "ViTriID";

                    // Load Tình trạng using function
                    SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT TinhTrang, DisplayText FROM dbo.fn_GetCSVCStatuses()", conn);
                    DataTable dt3 = new DataTable();
                    adapter3.Fill(dt3);

                    if (dt3.Rows.Count > 0)
                    {
                        cboTinhTrang.DataSource = dt3;
                        cboTinhTrang.DisplayMember = "DisplayText";
                        cboTinhTrang.ValueMember = "TinhTrang";
                    }
                    else
                    {
                        // Fallback nếu function không trả về data
                        cboTinhTrang.Items.Clear();
                        cboTinhTrang.Items.AddRange(new string[] { "Đang sử dụng", "Bảo trì", "Hỏng", "Đã thanh lý" });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu ComboBox: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCSVCData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT c.*, ts.NgayMua, ts.NgayHetBaoHanh, ts.ThoiGianSuDungDuKien_Thang, ts.GhiChu as GhiChuSuDung
                        FROM CSVC c
                        LEFT JOIN ThongTinSuDung ts ON c.CSVCID = ts.CSVCID
                        WHERE c.CSVCID = @CSVCID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CSVCID", csvfId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        
                        if (reader.Read())
                        {
                            txtTenCSVC.Text = reader["TenCSVC"].ToString();
                            txtMaCSVC.Text = reader["MaCSVC"].ToString();
                            
                            if (reader["LoaiID"] != DBNull.Value)
                                cboLoai.SelectedValue = reader["LoaiID"];
                            if (reader["ViTriID"] != DBNull.Value)
                                cboViTri.SelectedValue = reader["ViTriID"];
                            
                            nudGiaTri.Value = Convert.ToDecimal(reader["GiaTri"] ?? 0);
                            if (!string.IsNullOrEmpty(reader["TinhTrang"].ToString()))
                                cboTinhTrang.SelectedValue = reader["TinhTrang"].ToString();
                            txtGhiChu.Text = reader["GhiChu"].ToString();
                            txtNhaCungCap.Text = reader["TenNhaCungCap"].ToString();
                            txtSDTNCC.Text = reader["SoDienThoaiNCC"].ToString();
                            txtEmailNCC.Text = reader["EmailNCC"].ToString();

                            // Thông tin sử dụng
                            if (reader["NgayMua"] != DBNull.Value)
                            {
                                dtpNgayMua.Value = Convert.ToDateTime(reader["NgayMua"]);
                                dtpNgayMua.Checked = true;
                            }
                            if (reader["NgayHetBaoHanh"] != DBNull.Value)
                            {
                                dtpNgayHetBaoHanh.Value = Convert.ToDateTime(reader["NgayHetBaoHanh"]);
                                dtpNgayHetBaoHanh.Checked = true;
                            }
                            if (reader["ThoiGianSuDungDuKien_Thang"] != DBNull.Value)
                                nudThoiGianSuDung.Value = Convert.ToDecimal(reader["ThoiGianSuDungDuKien_Thang"]);
                            txtGhiChuSuDung.Text = reader["GhiChuSuDung"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu CSVC: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtTenCSVC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên CSVC!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCSVC.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCSVC", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@CSVCID", csvfId);
                        cmd.Parameters.AddWithValue("@TenCSVC", txtTenCSVC.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaCSVC", string.IsNullOrWhiteSpace(txtMaCSVC.Text) ? (object)DBNull.Value : txtMaCSVC.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoaiID", cboLoai.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ViTriID", cboViTri.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GiaTri", nudGiaTri.Value);
                        cmd.Parameters.AddWithValue("@TinhTrang", cboTinhTrang.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@TenNhaCungCap", string.IsNullOrWhiteSpace(txtNhaCungCap.Text) ? (object)DBNull.Value : txtNhaCungCap.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoDienThoaiNCC", string.IsNullOrWhiteSpace(txtSDTNCC.Text) ? (object)DBNull.Value : txtSDTNCC.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmailNCC", string.IsNullOrWhiteSpace(txtEmailNCC.Text) ? (object)DBNull.Value : txtEmailNCC.Text.Trim());
                        
                        cmd.ExecuteNonQuery();
                    }
                    
                    // Cập nhật thông tin sử dụng riêng biệt
                    using (SqlCommand cmdUsage = new SqlCommand(@"
                        UPDATE ThongTinSuDung 
                        SET NgayMua = @NgayMua,
                            NgayHetBaoHanh = @NgayHetBaoHanh,
                            ThoiGianSuDungDuKien_Thang = @ThoiGianSuDungDuKien_Thang,
                            GhiChu = @GhiChuSuDung
                        WHERE CSVCID = @CSVCID;
                        
                        IF @@ROWCOUNT = 0 AND @NgayMua IS NOT NULL
                        BEGIN
                            INSERT INTO ThongTinSuDung (CSVCID, NgayMua, NgayHetBaoHanh, ThoiGianSuDungDuKien_Thang, GhiChu)
                            VALUES (@CSVCID, @NgayMua, @NgayHetBaoHanh, @ThoiGianSuDungDuKien_Thang, @GhiChuSuDung);
                        END", conn))
                    {
                        cmdUsage.Parameters.AddWithValue("@CSVCID", csvfId);
                        cmdUsage.Parameters.AddWithValue("@NgayMua", dtpNgayMua.Checked ? (object)dtpNgayMua.Value : DBNull.Value);
                        cmdUsage.Parameters.AddWithValue("@NgayHetBaoHanh", dtpNgayHetBaoHanh.Checked ? (object)dtpNgayHetBaoHanh.Value : DBNull.Value);
                        cmdUsage.Parameters.AddWithValue("@ThoiGianSuDungDuKien_Thang", nudThoiGianSuDung.Value > 0 ? (object)nudThoiGianSuDung.Value : DBNull.Value);
                        cmdUsage.Parameters.AddWithValue("@GhiChuSuDung", string.IsNullOrWhiteSpace(txtGhiChuSuDung.Text) ? (object)DBNull.Value : txtGhiChuSuDung.Text.Trim());
                        
                        cmdUsage.ExecuteNonQuery();
                    }
                        
                    MessageBox.Show("Cập nhật CSVC thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật CSVC: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}