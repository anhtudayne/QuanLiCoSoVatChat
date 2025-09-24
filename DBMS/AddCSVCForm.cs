using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBMS
{
    public partial class AddCSVCForm : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public AddCSVCForm()
        {
            InitializeComponent();
        }

        private void AddCSVCForm_Load(object sender, EventArgs e)
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
                    SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT LoaiID, TenLoai FROM LoaiCSVC", conn);
                    DataTable dt1 = new DataTable();
                    adapter1.Fill(dt1);

                    DataRow row1 = dt1.NewRow();
                    row1["LoaiID"] = DBNull.Value;
                    row1["TenLoai"] = "-- Chọn loại --";
                    dt1.Rows.InsertAt(row1, 0);

                    cboLoai.DataSource = dt1;
                    cboLoai.DisplayMember = "TenLoai";
                    cboLoai.ValueMember = "LoaiID";

                    // Load Vị trí
                    SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT ViTriID, KhuVuc + N' - Tầng ' + CAST(Tang AS NVARCHAR(5)) AS TenViTri FROM ViTri", conn);
                    DataTable dt2 = new DataTable();
                    adapter2.Fill(dt2);

                    DataRow row2 = dt2.NewRow();
                    row2["ViTriID"] = DBNull.Value;
                    row2["TenViTri"] = "-- Chọn vị trí --";
                    dt2.Rows.InsertAt(row2, 0);

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
                        cboTinhTrang.SelectedIndex = 0; // Chọn item đầu tiên
                    }
                    else
                    {
                        // Fallback nếu function không trả về data
                        cboTinhTrang.Items.Clear();
                        cboTinhTrang.Items.AddRange(new string[] {
                            "Đang sử dụng", "Bảo trì", "Hỏng", "Đã thanh lý"
                        });
                        cboTinhTrang.SelectedIndex = 0; // Mặc định "Đang sử dụng"
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
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
                    using (SqlCommand cmd = new SqlCommand("sp_AddCSVC", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parameters
                        cmd.Parameters.AddWithValue("@TenCSVC", txtTenCSVC.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaCSVC", string.IsNullOrWhiteSpace(txtMaCSVC.Text) ? (object)DBNull.Value : txtMaCSVC.Text.Trim());
                        cmd.Parameters.AddWithValue("@LoaiID", cboLoai.SelectedValue == DBNull.Value ? (object)DBNull.Value : cboLoai.SelectedValue);
                        cmd.Parameters.AddWithValue("@ViTriID", cboViTri.SelectedValue == DBNull.Value ? (object)DBNull.Value : cboViTri.SelectedValue);
                        cmd.Parameters.AddWithValue("@GiaTri", nudGiaTri.Value);
                        cmd.Parameters.AddWithValue("@TenNhaCungCap", string.IsNullOrWhiteSpace(txtNhaCungCap.Text) ? (object)DBNull.Value : txtNhaCungCap.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoDienThoaiNCC", string.IsNullOrWhiteSpace(txtSDTNCC.Text) ? (object)DBNull.Value : txtSDTNCC.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmailNCC", string.IsNullOrWhiteSpace(txtEmailNCC.Text) ? (object)DBNull.Value : txtEmailNCC.Text.Trim());
                        cmd.Parameters.AddWithValue("@TinhTrang", cboTinhTrang.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());

                        // Thông tin sử dụng
                        cmd.Parameters.AddWithValue("@NgayMua", dtpNgayMua.Checked ? (object)dtpNgayMua.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayHetBaoHanh", dtpNgayHetBaoHanh.Checked ? (object)dtpNgayHetBaoHanh.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ThoiGianSuDungDuKien_Thang", nudThoiGianSuDung.Value == 0 ? (object)DBNull.Value : (int)nudThoiGianSuDung.Value);
                        cmd.Parameters.AddWithValue("@GhiChuSuDung", string.IsNullOrWhiteSpace(txtGhiChuSuDung.Text) ? (object)DBNull.Value : txtGhiChuSuDung.Text.Trim());

                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@CSVCID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm CSVC: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenCSVC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên CSVC!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCSVC.Focus();
                return false;
            }

            if (cboTinhTrang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tình trạng!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTinhTrang.Focus();
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}