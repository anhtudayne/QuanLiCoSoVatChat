using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class BaoCaoForm : Form
    {
        public BaoCaoForm()
        {
            InitializeComponent();
        }

        private void BaoCaoForm_Load(object sender, EventArgs e)
        {
            // TODO: Load dữ liệu báo cáo
        }

        private void btnThongKeCSVC_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thống kê CSVC sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThongKeNhanVien_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thống kê Nhân viên sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBaoCaoBaoTri_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Báo cáo Bảo trì sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Xuất báo cáo sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
