using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class ThanhLyForm : Form
    {
        public ThanhLyForm()
        {
            InitializeComponent();
        }

        private void ThanhLyForm_Load(object sender, EventArgs e)
        {
            // TODO: Load dữ liệu thanh lý
        }

        private void btnThanhLy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thanh lý CSVC sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Xem lịch sử thanh lý sẽ được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
