using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    public partial class MainForm : Form
    {
        private Form currentChildForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Khởi tạo form với màn hình welcome
            ShowWelcomeScreen();
        }

        private void ShowWelcomeScreen()
        {
            // Đóng form con hiện tại nếu có
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm = null;
            }
            
            // Hiển thị lại welcome screen
            lblWelcome.Visible = true;
        }

        private void OpenChildForm(Form childForm)
        {
            // Đóng form con hiện tại nếu có
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            // Ẩn welcome screen
            lblWelcome.Visible = false;

            // Thiết lập form con mới
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ResetMenuButtonColors()
        {
            // Reset tất cả button về màu mặc định
            btnQuanLyNhanVien.BackColor = Color.FromArgb(51, 51, 76);
            btnPhanCongCa.BackColor = Color.FromArgb(51, 51, 76);
            btnQuanLyCSVC.BackColor = Color.FromArgb(51, 51, 76);
            btnBaoTri.BackColor = Color.FromArgb(51, 51, 76);
            btnThanhLy.BackColor = Color.FromArgb(51, 51, 76);
            btnBaoCao.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetMenuButtonColors();
            activeButton.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQuanLyNhanVien);
            OpenChildForm(new QuanLyNhanVienForm());
        }

        private void btnPhanCongCa_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnPhanCongCa);
            OpenChildForm(new PhanCongCaForm());
        }

        private void btnQuanLyCSVC_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQuanLyCSVC);
            OpenChildForm(new QuanLyCSVCForm());
        }

        private void btnBaoTri_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnBaoTri);
            OpenChildForm(new BaoTriForm());
        }

        private void btnThanhLy_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnThanhLy);
            OpenChildForm(new ThanhLyForm());
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnBaoCao);
            OpenChildForm(new BaoCaoForm());
        }
    }
}
