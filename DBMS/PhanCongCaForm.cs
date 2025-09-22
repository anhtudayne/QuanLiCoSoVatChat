using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class PhanCongCaForm : Form
    {
        // Connection string - kết nối tới database vc
        private string connectionString = "Data Source=.;Initial Catalog=vc;Integrated Security=True;";

        public PhanCongCaForm()
        {
            InitializeComponent();
        }

        private void PhanCongCaForm_Load(object sender, EventArgs e)
        {
            LoadWorkShiftData();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Cấu hình DataGridView
            dgvPhanCongCa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCongCa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhanCongCa.MultiSelect = false;
            dgvPhanCongCa.AllowUserToAddRows = false;
            dgvPhanCongCa.AllowUserToDeleteRows = false;
            dgvPhanCongCa.ReadOnly = true;
            
            // Đặt màu cho header
            dgvPhanCongCa.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 44, 51);
            dgvPhanCongCa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPhanCongCa.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            
            // Màu xen kẽ cho rows
            dgvPhanCongCa.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvPhanCongCa.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
        }

        private void LoadWorkShiftData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Gọi stored procedure sp_GetWorkShiftDetails
                    using (SqlCommand cmd = new SqlCommand("sp_GetWorkShiftDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        // Gán dữ liệu vào DataGridView
                        dgvPhanCongCa.DataSource = dataTable;
                        
                        // Đặt tên cột tiếng Việt và định dạng
                        if (dgvPhanCongCa.Columns.Count > 0)
                        {
                            dgvPhanCongCa.Columns["PhanCongID"].HeaderText = "Mã PC";
                            dgvPhanCongCa.Columns["NhanVienID"].HeaderText = "Mã NV";
                            dgvPhanCongCa.Columns["HoTen"].HeaderText = "Họ và Tên";
                            dgvPhanCongCa.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                            dgvPhanCongCa.Columns["Tuoi"].HeaderText = "Tuổi";
                            dgvPhanCongCa.Columns["TenCa"].HeaderText = "Tên Ca";
                            dgvPhanCongCa.Columns["GioBatDau"].HeaderText = "Giờ BĐ";
                            dgvPhanCongCa.Columns["GioKetThuc"].HeaderText = "Giờ KT";
                            dgvPhanCongCa.Columns["NgayLamViec"].HeaderText = "Ngày LV";
                            dgvPhanCongCa.Columns["Thang"].HeaderText = "Tháng";
                            dgvPhanCongCa.Columns["Nam"].HeaderText = "Năm";
                            dgvPhanCongCa.Columns["ThuTrongTuan"].HeaderText = "Thứ";
                            dgvPhanCongCa.Columns["VaiTroTrongCa"].HeaderText = "Vai Trò";
                            dgvPhanCongCa.Columns["GhiChu"].HeaderText = "Ghi Chú";
                            dgvPhanCongCa.Columns["SoGioLam"].HeaderText = "Số Giờ";
                            dgvPhanCongCa.Columns["TinhTrangCa"].HeaderText = "Tình Trạng";
                            
                            // Ẩn cột ID và điều chỉnh độ rộng
                            dgvPhanCongCa.Columns["PhanCongID"].Visible = false; // Ẩn để dùng cho Edit/Delete
                            dgvPhanCongCa.Columns["NhanVienID"].Width = 70;
                            dgvPhanCongCa.Columns["NgaySinh"].Visible = false; // Ẩn vì đã có tuổi
                            dgvPhanCongCa.Columns["Tuoi"].Width = 60;
                            dgvPhanCongCa.Columns["TenCa"].Width = 120;
                            dgvPhanCongCa.Columns["GioBatDau"].Width = 80;
                            dgvPhanCongCa.Columns["GioKetThuc"].Width = 80;
                            dgvPhanCongCa.Columns["NgayLamViec"].Width = 100;
                            dgvPhanCongCa.Columns["Thang"].Width = 60;
                            dgvPhanCongCa.Columns["Nam"].Width = 60;
                            dgvPhanCongCa.Columns["ThuTrongTuan"].Width = 80;
                            dgvPhanCongCa.Columns["VaiTroTrongCa"].Width = 120;
                            dgvPhanCongCa.Columns["SoGioLam"].Width = 80;
                            dgvPhanCongCa.Columns["TinhTrangCa"].Width = 100;
                            
                            // Định dạng cột số
                            dgvPhanCongCa.Columns["SoGioLam"].DefaultCellStyle.Format = "N1";
                            dgvPhanCongCa.Columns["NgayLamViec"].DefaultCellStyle.Format = "dd/MM/yyyy";
                            
                            // Căn giữa cho một số cột
                            dgvPhanCongCa.Columns["NhanVienID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvPhanCongCa.Columns["Tuoi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvPhanCongCa.Columns["Thang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvPhanCongCa.Columns["Nam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvPhanCongCa.Columns["SoGioLam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvPhanCongCa.Columns["TinhTrangCa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            
                            // Màu sắc cho cột tình trạng
                            foreach (DataGridViewRow row in dgvPhanCongCa.Rows)
                            {
                                if (row.Cells["TinhTrangCa"].Value != null)
                                {
                                    string tinhTrang = row.Cells["TinhTrangCa"].Value.ToString();
                                    switch (tinhTrang)
                                    {
                                        case "Hôm nay":
                                            row.Cells["TinhTrangCa"].Style.BackColor = Color.LightGreen;
                                            break;
                                        case "Tương lai":
                                            row.Cells["TinhTrangCa"].Style.BackColor = Color.LightBlue;
                                            break;
                                        case "Đã qua":
                                            row.Cells["TinhTrangCa"].Style.BackColor = Color.LightGray;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu phân công ca: {ex.Message}", "Lỗi Database", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở form thêm phân công ca mới
            AddShiftForm addForm = new AddShiftForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // Nếu thêm thành công, reload dữ liệu
                LoadWorkShiftData();
                MessageBox.Show("Đã thêm phân công ca thành công và làm mới dữ liệu!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra có dòng nào được chọn không
            if (dgvPhanCongCa.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một ca để sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy PhanCongID từ dòng được chọn
            int phanCongID = Convert.ToInt32(dgvPhanCongCa.CurrentRow.Cells["PhanCongID"].Value);
            
            // Mở form sửa
            EditShiftForm editForm = new EditShiftForm(phanCongID);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Nếu sửa thành công, reload dữ liệu
                LoadWorkShiftData();
                MessageBox.Show("Đã cập nhật phân công ca thành công và làm mới dữ liệu!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra có dòng nào được chọn không
            if (dgvPhanCongCa.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một ca để xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin ca được chọn
            int phanCongID = Convert.ToInt32(dgvPhanCongCa.CurrentRow.Cells["PhanCongID"].Value);
            string hoTen = dgvPhanCongCa.CurrentRow.Cells["HoTen"].Value.ToString();
            string tenCa = dgvPhanCongCa.CurrentRow.Cells["TenCa"].Value.ToString();
            string ngayLamViec = Convert.ToDateTime(dgvPhanCongCa.CurrentRow.Cells["NgayLamViec"].Value).ToString("dd/MM/yyyy");

            // Xác nhận xóa
            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa ca làm việc:\n\n" +
                $"Nhân viên: {hoTen}\n" +
                $"Ca: {tenCa}\n" +
                $"Ngày: {ngayLamViec}\n\n" +
                $"Thao tác này không thể hoàn tác!",
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteWorkShift", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PhanCongID", phanCongID);

                            // Output parameters
                            SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                            SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                            
                            cmd.Parameters.Add(successParam);
                            cmd.Parameters.Add(messageParam);

                            cmd.ExecuteNonQuery();

                            bool success = (bool)successParam.Value;
                            string message = messageParam.Value.ToString();

                            if (success)
                            {
                                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadWorkShiftData(); // Reload dữ liệu
                            }
                            else
                            {
                                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa phân ca: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Tìm kiếm phân công ca sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadWorkShiftData();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
