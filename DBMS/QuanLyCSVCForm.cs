using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class QuanLyCSVCForm : Form
    {
        public QuanLyCSVCForm()
        {
            InitializeComponent();
        }

        private void QuanLyCSVCForm_Load(object sender, EventArgs e)
        {
            // TODO: Load dữ liệu CSVC
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thêm CSVC sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Sửa CSVC sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Xóa CSVC sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Tìm kiếm CSVC sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Làm mới sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
