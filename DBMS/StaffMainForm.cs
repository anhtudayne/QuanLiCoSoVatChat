using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class StaffMainForm : Form
    {
        private Form currentChildForm;

        public StaffMainForm()
        {
            InitializeComponent();
        }

        private void StaffMainForm_Load(object sender, EventArgs e)
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
            btnXemCSVC.BackColor = Color.FromArgb(51, 51, 76);
            btnYeuCauBaoTri.BackColor = Color.FromArgb(51, 51, 76);
            btnXemCaTruc.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetMenuButtonColors();
            activeButton.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnXemCSVC_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnXemCSVC);
            // Mở form xem CSVC với quyền hạn chế cho nhân viên trực
            OpenChildForm(new QuanLyCSVCForm());
        }

        private void btnYeuCauBaoTri_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnYeuCauBaoTri);
            // Chỉ cho phép tạo yêu cầu bảo trì, không quản lý
            OpenChildForm(new BaoTriForm());
        }

        private void btnXemCaTruc_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnXemCaTruc);
            // Hiển thị lịch ca trực của nhân viên
            OpenChildForm(new PhanCongCaForm());
        }
    }
}