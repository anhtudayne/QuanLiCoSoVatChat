namespace DBMS
{
    partial class StaffMainForm
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
            this.btnThongTinCaNhan = new System.Windows.Forms.Button();
            this.btnXemCaTruc = new System.Windows.Forms.Button();
            this.btnYeuCauBaoTri = new System.Windows.Forms.Button();
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
            this.panelMenu.Controls.Add(this.btnThongTinCaNhan);
            this.panelMenu.Controls.Add(this.btnXemCaTruc);
            this.panelMenu.Controls.Add(this.btnYeuCauBaoTri);
            this.panelMenu.Controls.Add(this.btnXemCSVC);
            this.panelMenu.Controls.Add(this.lblTitle);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 600);
            this.panelMenu.TabIndex = 0;
            // 
            // btnXemCaTruc
            // 
            this.btnXemCaTruc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnXemCaTruc.FlatAppearance.BorderSize = 0;
            this.btnXemCaTruc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemCaTruc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemCaTruc.ForeColor = System.Drawing.Color.White;
            this.btnXemCaTruc.Location = new System.Drawing.Point(0, 240);
            this.btnXemCaTruc.Name = "btnXemCaTruc";
            this.btnXemCaTruc.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnXemCaTruc.Size = new System.Drawing.Size(250, 60);
            this.btnXemCaTruc.TabIndex = 3;
            this.btnXemCaTruc.Text = "🕐 Xem lịch ca trực";
            this.btnXemCaTruc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemCaTruc.UseVisualStyleBackColor = false;
            this.btnXemCaTruc.Click += new System.EventHandler(this.btnXemCaTruc_Click);
            // 
            // btnThongTinCaNhan
            // 
            this.btnThongTinCaNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnThongTinCaNhan.FlatAppearance.BorderSize = 0;
            this.btnThongTinCaNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongTinCaNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongTinCaNhan.ForeColor = System.Drawing.Color.White;
            this.btnThongTinCaNhan.Location = new System.Drawing.Point(0, 520);
            this.btnThongTinCaNhan.Name = "btnThongTinCaNhan";
            this.btnThongTinCaNhan.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnThongTinCaNhan.Size = new System.Drawing.Size(250, 60);
            this.btnThongTinCaNhan.TabIndex = 4;
            this.btnThongTinCaNhan.Text = "👤 Thông tin cá nhân";
            this.btnThongTinCaNhan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongTinCaNhan.UseVisualStyleBackColor = false;
            this.btnThongTinCaNhan.Click += new System.EventHandler(this.btnThongTinCaNhan_Click);
            // 
            // btnYeuCauBaoTri
            // 
            this.btnYeuCauBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnYeuCauBaoTri.FlatAppearance.BorderSize = 0;
            this.btnYeuCauBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYeuCauBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYeuCauBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnYeuCauBaoTri.Location = new System.Drawing.Point(0, 180);
            this.btnYeuCauBaoTri.Name = "btnYeuCauBaoTri";
            this.btnYeuCauBaoTri.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnYeuCauBaoTri.Size = new System.Drawing.Size(250, 60);
            this.btnYeuCauBaoTri.TabIndex = 2;
            this.btnYeuCauBaoTri.Text = "🔧 Yêu cầu bảo trì";
            this.btnYeuCauBaoTri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYeuCauBaoTri.UseVisualStyleBackColor = false;
            this.btnYeuCauBaoTri.Click += new System.EventHandler(this.btnYeuCauBaoTri_Click);
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
            this.lblTitle.Text = "👥 NHÂN VIÊN\r\nTRỰC";
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
            this.lblWelcome.Location = new System.Drawing.Point(220, 280);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(310, 62);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "👥 Chào mừng\r\nNhân viên trực!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StaffMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StaffMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý CSVC - Nhân viên trực";
            this.Load += new System.EventHandler(this.StaffMainForm_Load);
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
        private System.Windows.Forms.Button btnYeuCauBaoTri;
        private System.Windows.Forms.Button btnXemCaTruc;
        private System.Windows.Forms.Button btnThongTinCaNhan;
        private System.Windows.Forms.Label lblWelcome;
    }
}