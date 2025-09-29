using System;
using System.Drawing;
using System.Windows.Forms;

namespace DBMS
{
    public partial class TechnicalMainForm : Form
    {
        private Form currentChildForm;
        private string connectionString;
        private string username;

        // Constructor mới nhận connection string và username
        public TechnicalMainForm(string connString, string user)
        {
            InitializeComponent();
            connectionString = connString;
            username = user;
        }

        // Constructor với chỉ connection string để backward compatibility
        public TechnicalMainForm(string connString) : this(connString, "")
        {
        }

        // Constructor cũ để backward compatibility
        public TechnicalMainForm() : this("Data Source=.;Initial Catalog=vc;Integrated Security=True;", "")
        {
        }

        private void TechnicalMainForm_Load(object sender, EventArgs e)
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
            btnBaoTri.BackColor = Color.FromArgb(51, 51, 76);
            btnLichBaoTriDinhKy.BackColor = Color.FromArgb(51, 51, 76);
            btnThongTinCaNhan.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetMenuButtonColors();
            activeButton.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnXemCSVC_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnXemCSVC);
            // Tạo form xem CSVC readonly cho kỹ thuật viên
            OpenChildForm(new QuanLyCSVCForm());
        }

        private void btnBaoTri_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnBaoTri);
            OpenChildForm(new BaoTriForm());
        }

        private void btnLichBaoTriDinhKy_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnLichBaoTriDinhKy);
            // Tạo form lịch bảo trì định kỳ
            // OpenChildForm(new LichBaoTriDinhKyForm());
            MessageBox.Show("Chức năng đang được phát triển!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                PersonalInfoForm personalForm = new PersonalInfoForm(connectionString, username);
                personalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở thông tin cá nhân: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}