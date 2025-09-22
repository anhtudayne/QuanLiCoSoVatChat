using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class BaoTriForm : Form
    {
        public BaoTriForm()
        {
            InitializeComponent();
        }

        private void BaoTriForm_Load(object sender, EventArgs e)
        {
            // TODO: Load dữ liệu bảo trì
        }

        private void btnThemBaoTri_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thêm yêu cầu bảo trì sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Cập nhật trạng thái bảo trì sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLichBaoTri_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Lịch bảo trì định kỳ sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
