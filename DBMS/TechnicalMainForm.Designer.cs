namespace DBMS
{
    partial class TechnicalMainForm
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
            this.btnLichBaoTriDinhKy = new System.Windows.Forms.Button();
            this.btnBaoTri = new System.Windows.Forms.Button();
            this.btnXemCSVC = new System.Windows.Forms.Button();
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
            this.panelMenu.Controls.Add(this.btnLichBaoTriDinhKy);
            this.panelMenu.Controls.Add(this.btnBaoTri);
            this.panelMenu.Controls.Add(this.btnXemCSVC);
            this.panelMenu.Controls.Add(this.lblTitle);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 600);
            this.panelMenu.TabIndex = 0;
            // 
            // btnLichBaoTriDinhKy
            // 
            this.btnLichBaoTriDinhKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnLichBaoTriDinhKy.FlatAppearance.BorderSize = 0;
            this.btnLichBaoTriDinhKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLichBaoTriDinhKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLichBaoTriDinhKy.ForeColor = System.Drawing.Color.White;
            this.btnLichBaoTriDinhKy.Location = new System.Drawing.Point(0, 240);
            this.btnLichBaoTriDinhKy.Name = "btnLichBaoTriDinhKy";
            this.btnLichBaoTriDinhKy.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLichBaoTriDinhKy.Size = new System.Drawing.Size(250, 60);
            this.btnLichBaoTriDinhKy.TabIndex = 3;
            this.btnLichBaoTriDinhKy.Text = "📅 Lịch bảo trì định kỳ";
            this.btnLichBaoTriDinhKy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLichBaoTriDinhKy.UseVisualStyleBackColor = false;
            this.btnLichBaoTriDinhKy.Click += new System.EventHandler(this.btnLichBaoTriDinhKy_Click);
            // 
            // btnBaoTri
            // 
            this.btnBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnBaoTri.FlatAppearance.BorderSize = 0;
            this.btnBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnBaoTri.Location = new System.Drawing.Point(0, 180);
            this.btnBaoTri.Name = "btnBaoTri";
            this.btnBaoTri.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnBaoTri.Size = new System.Drawing.Size(250, 60);
            this.btnBaoTri.TabIndex = 2;
            this.btnBaoTri.Text = "🔧 Bảo trì CSVC";
            this.btnBaoTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoTri.UseVisualStyleBackColor = false;
            this.btnBaoTri.Click += new System.EventHandler(this.btnBaoTri_Click);
            // 
            // btnXemCSVC
            // 
            this.btnXemCSVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnXemCSVC.FlatAppearance.BorderSize = 0;
            this.btnXemCSVC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemCSVC.ForeColor = System.Drawing.Color.White;
            this.btnXemCSVC.Location = new System.Drawing.Point(0, 120);
            this.btnXemCSVC.Name = "btnXemCSVC";
            this.btnXemCSVC.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnXemCSVC.Size = new System.Drawing.Size(250, 60);
            this.btnXemCSVC.TabIndex = 1;
            this.btnXemCSVC.Text = "📋 Xem danh sách CSVC";
            this.btnXemCSVC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemCSVC.UseVisualStyleBackColor = false;
            this.btnXemCSVC.Click += new System.EventHandler(this.btnXemCSVC_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔧 NHÂN VIÊN\r\nKỸ THUẬT";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.lblWelcome);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(250, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(750, 600);
            this.panelContent.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWelcome.Location = new System.Drawing.Point(200, 280);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(350, 62);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "🔧 Chào mừng\r\nNhân viên Kỹ thuật!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TechnicalMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TechnicalMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý CSVC - Nhân viên Kỹ thuật";
            this.Load += new System.EventHandler(this.TechnicalMainForm_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnXemCSVC;
        private System.Windows.Forms.Button btnBaoTri;
        private System.Windows.Forms.Button btnLichBaoTriDinhKy;
        private System.Windows.Forms.Label lblWelcome;
    }
}