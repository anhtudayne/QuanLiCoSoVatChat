namespace DBMS
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnThanhLy = new System.Windows.Forms.Button();
            this.btnBaoTri = new System.Windows.Forms.Button();
            this.btnQuanLyCSVC = new System.Windows.Forms.Button();
            this.btnPhanCongCa = new System.Windows.Forms.Button();
            this.btnQuanLyNhanVien = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.panelMenu.Controls.Add(this.btnBaoCao);
            this.panelMenu.Controls.Add(this.btnThanhLy);
            this.panelMenu.Controls.Add(this.btnBaoTri);
            this.panelMenu.Controls.Add(this.btnQuanLyCSVC);
            this.panelMenu.Controls.Add(this.btnPhanCongCa);
            this.panelMenu.Controls.Add(this.btnQuanLyNhanVien);
            this.panelMenu.Controls.Add(this.lblTitle);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 600);
            this.panelMenu.TabIndex = 0;
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnBaoCao.Location = new System.Drawing.Point(0, 420);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnBaoCao.Size = new System.Drawing.Size(250, 60);
            this.btnBaoCao.TabIndex = 6;
            this.btnBaoCao.Text = "📊 Báo cáo - Thống kê";
            this.btnBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCao.UseVisualStyleBackColor = false;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnThanhLy
            // 
            this.btnThanhLy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnThanhLy.FlatAppearance.BorderSize = 0;
            this.btnThanhLy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhLy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhLy.ForeColor = System.Drawing.Color.White;
            this.btnThanhLy.Location = new System.Drawing.Point(0, 360);
            this.btnThanhLy.Name = "btnThanhLy";
            this.btnThanhLy.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnThanhLy.Size = new System.Drawing.Size(250, 60);
            this.btnThanhLy.TabIndex = 5;
            this.btnThanhLy.Text = "🗑️ Thanh lý CSVC";
            this.btnThanhLy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThanhLy.UseVisualStyleBackColor = false;
            this.btnThanhLy.Click += new System.EventHandler(this.btnThanhLy_Click);
            // 
            // btnBaoTri
            // 
            this.btnBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnBaoTri.FlatAppearance.BorderSize = 0;
            this.btnBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnBaoTri.Location = new System.Drawing.Point(0, 300);
            this.btnBaoTri.Name = "btnBaoTri";
            this.btnBaoTri.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnBaoTri.Size = new System.Drawing.Size(250, 60);
            this.btnBaoTri.TabIndex = 4;
            this.btnBaoTri.Text = "🔧 Bảo trì - Lịch bảo trì";
            this.btnBaoTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoTri.UseVisualStyleBackColor = false;
            this.btnBaoTri.Click += new System.EventHandler(this.btnBaoTri_Click);
            // 
            // btnQuanLyCSVC
            // 
            this.btnQuanLyCSVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnQuanLyCSVC.FlatAppearance.BorderSize = 0;
            this.btnQuanLyCSVC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyCSVC.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyCSVC.Location = new System.Drawing.Point(0, 240);
            this.btnQuanLyCSVC.Name = "btnQuanLyCSVC";
            this.btnQuanLyCSVC.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnQuanLyCSVC.Size = new System.Drawing.Size(250, 60);
            this.btnQuanLyCSVC.TabIndex = 3;
            this.btnQuanLyCSVC.Text = "🏢 Quản lý CSVC";
            this.btnQuanLyCSVC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyCSVC.UseVisualStyleBackColor = false;
            this.btnQuanLyCSVC.Click += new System.EventHandler(this.btnQuanLyCSVC_Click);
            // 
            // btnPhanCongCa
            // 
            this.btnPhanCongCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnPhanCongCa.FlatAppearance.BorderSize = 0;
            this.btnPhanCongCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhanCongCa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanCongCa.ForeColor = System.Drawing.Color.White;
            this.btnPhanCongCa.Location = new System.Drawing.Point(0, 180);
            this.btnPhanCongCa.Name = "btnPhanCongCa";
            this.btnPhanCongCa.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnPhanCongCa.Size = new System.Drawing.Size(250, 60);
            this.btnPhanCongCa.TabIndex = 2;
            this.btnPhanCongCa.Text = "⏰ Phân công ca trực";
            this.btnPhanCongCa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhanCongCa.UseVisualStyleBackColor = false;
            this.btnPhanCongCa.Click += new System.EventHandler(this.btnPhanCongCa_Click);
            // 
            // btnQuanLyNhanVien
            // 
            this.btnQuanLyNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnQuanLyNhanVien.FlatAppearance.BorderSize = 0;
            this.btnQuanLyNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyNhanVien.Location = new System.Drawing.Point(0, 120);
            this.btnQuanLyNhanVien.Name = "btnQuanLyNhanVien";
            this.btnQuanLyNhanVien.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnQuanLyNhanVien.Size = new System.Drawing.Size(250, 60);
            this.btnQuanLyNhanVien.TabIndex = 1;
            this.btnQuanLyNhanVien.Text = "👥 Quản lý Nhân viên";
            this.btnQuanLyNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyNhanVien.UseVisualStyleBackColor = false;
            this.btnQuanLyNhanVien.Click += new System.EventHandler(this.btnQuanLyNhanVien_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🏪 COOPMART\r\nQuản lý CSVC";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.lblWelcome);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(250, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(950, 600);
            this.panelContent.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblWelcome.Location = new System.Drawing.Point(350, 280);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(250, 62);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Chào mừng đến với\r\nHệ thống quản lý CSVC";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý CSVC - Coopmart";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnQuanLyNhanVien;
        private System.Windows.Forms.Button btnPhanCongCa;
        private System.Windows.Forms.Button btnQuanLyCSVC;
        private System.Windows.Forms.Button btnBaoTri;
        private System.Windows.Forms.Button btnThanhLy;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Label lblWelcome;
    }
}

