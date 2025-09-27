namespace DBMS
{
    partial class UpdateMaintenanceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblBaoTriID = new System.Windows.Forms.Label();
            this.lblTenCSVC = new System.Windows.Forms.Label();
            this.lblNgayYeuCau = new System.Windows.Forms.Label();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.lblChiPhiCu = new System.Windows.Forms.Label();
            this.lblTrangThaiCu = new System.Windows.Forms.Label();
            this.lblTrangThaiMoi = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblNgayHoanThanh = new System.Windows.Forms.Label();
            this.dtpNgayHoanThanh = new System.Windows.Forms.DateTimePicker();
            this.lblChiPhiThucTe = new System.Windows.Forms.Label();
            this.txtChiPhiThucTe = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(534, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔧 CẬP NHẬT TRẠNG THÁI BẢO TRÌ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.lblTrangThaiCu);
            this.panelInfo.Controls.Add(this.lblChiPhiCu);
            this.panelInfo.Controls.Add(this.lblNoiDung);
            this.panelInfo.Controls.Add(this.lblNgayYeuCau);
            this.panelInfo.Controls.Add(this.lblTenCSVC);
            this.panelInfo.Controls.Add(this.lblBaoTriID);
            this.panelInfo.Location = new System.Drawing.Point(20, 70);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(494, 150);
            this.panelInfo.TabIndex = 1;
            // 
            // lblBaoTriID
            // 
            this.lblBaoTriID.AutoSize = true;
            this.lblBaoTriID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBaoTriID.Location = new System.Drawing.Point(10, 10);
            this.lblBaoTriID.Name = "lblBaoTriID";
            this.lblBaoTriID.Size = new System.Drawing.Size(24, 17);
            this.lblBaoTriID.TabIndex = 0;
            this.lblBaoTriID.Text = "ID";
            // 
            // lblTenCSVC
            // 
            this.lblTenCSVC.AutoSize = true;
            this.lblTenCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTenCSVC.Location = new System.Drawing.Point(10, 35);
            this.lblTenCSVC.Name = "lblTenCSVC";
            this.lblTenCSVC.Size = new System.Drawing.Size(45, 17);
            this.lblTenCSVC.TabIndex = 1;
            this.lblTenCSVC.Text = "CSVC";
            // 
            // lblNgayYeuCau
            // 
            this.lblNgayYeuCau.AutoSize = true;
            this.lblNgayYeuCau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNgayYeuCau.Location = new System.Drawing.Point(10, 60);
            this.lblNgayYeuCau.Name = "lblNgayYeuCau";
            this.lblNgayYeuCau.Size = new System.Drawing.Size(94, 17);
            this.lblNgayYeuCau.TabIndex = 2;
            this.lblNgayYeuCau.Text = "Ngày yêu cầu";
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNoiDung.Location = new System.Drawing.Point(10, 85);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(68, 17);
            this.lblNoiDung.TabIndex = 3;
            this.lblNoiDung.Text = "Nội dung";
            // 
            // lblChiPhiCu
            // 
            this.lblChiPhiCu.AutoSize = true;
            this.lblChiPhiCu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblChiPhiCu.Location = new System.Drawing.Point(10, 110);
            this.lblChiPhiCu.Name = "lblChiPhiCu";
            this.lblChiPhiCu.Size = new System.Drawing.Size(53, 17);
            this.lblChiPhiCu.TabIndex = 4;
            this.lblChiPhiCu.Text = "Chi phí";
            // 
            // lblTrangThaiCu
            // 
            this.lblTrangThaiCu.AutoSize = true;
            this.lblTrangThaiCu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiCu.Location = new System.Drawing.Point(250, 10);
            this.lblTrangThaiCu.Name = "lblTrangThaiCu";
            this.lblTrangThaiCu.Size = new System.Drawing.Size(84, 17);
            this.lblTrangThaiCu.TabIndex = 5;
            this.lblTrangThaiCu.Text = "Trạng thái";
            // 
            // lblTrangThaiMoi
            // 
            this.lblTrangThaiMoi.AutoSize = true;
            this.lblTrangThaiMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiMoi.Location = new System.Drawing.Point(30, 240);
            this.lblTrangThaiMoi.Name = "lblTrangThaiMoi";
            this.lblTrangThaiMoi.Size = new System.Drawing.Size(124, 17);
            this.lblTrangThaiMoi.TabIndex = 2;
            this.lblTrangThaiMoi.Text = "Trạng thái mới: *";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboTrangThai.Location = new System.Drawing.Point(33, 260);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(200, 24);
            this.cboTrangThai.TabIndex = 3;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);
            // 
            // lblNgayHoanThanh
            // 
            this.lblNgayHoanThanh.AutoSize = true;
            this.lblNgayHoanThanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNgayHoanThanh.Location = new System.Drawing.Point(250, 240);
            this.lblNgayHoanThanh.Name = "lblNgayHoanThanh";
            this.lblNgayHoanThanh.Size = new System.Drawing.Size(120, 17);
            this.lblNgayHoanThanh.TabIndex = 4;
            this.lblNgayHoanThanh.Text = "Ngày hoàn thành:";
            this.lblNgayHoanThanh.Visible = false;
            // 
            // dtpNgayHoanThanh
            // 
            this.dtpNgayHoanThanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpNgayHoanThanh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHoanThanh.Location = new System.Drawing.Point(253, 260);
            this.dtpNgayHoanThanh.Name = "dtpNgayHoanThanh";
            this.dtpNgayHoanThanh.Size = new System.Drawing.Size(150, 23);
            this.dtpNgayHoanThanh.TabIndex = 5;
            this.dtpNgayHoanThanh.Visible = false;
            // 
            // lblChiPhiThucTe
            // 
            this.lblChiPhiThucTe.AutoSize = true;
            this.lblChiPhiThucTe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblChiPhiThucTe.Location = new System.Drawing.Point(30, 300);
            this.lblChiPhiThucTe.Name = "lblChiPhiThucTe";
            this.lblChiPhiThucTe.Size = new System.Drawing.Size(105, 17);
            this.lblChiPhiThucTe.TabIndex = 6;
            this.lblChiPhiThucTe.Text = "Chi phí thực tế:";
            this.lblChiPhiThucTe.Visible = false;
            // 
            // txtChiPhiThucTe
            // 
            this.txtChiPhiThucTe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtChiPhiThucTe.Location = new System.Drawing.Point(33, 320);
            this.txtChiPhiThucTe.Name = "txtChiPhiThucTe";
            this.txtChiPhiThucTe.Size = new System.Drawing.Size(200, 23);
            this.txtChiPhiThucTe.TabIndex = 7;
            this.txtChiPhiThucTe.Visible = false;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(30, 360);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(107, 17);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú thêm:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(33, 380);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(470, 60);
            this.txtGhiChu.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(293, 460);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Cập nhật";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(405, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UpdateMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(534, 517);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtChiPhiThucTe);
            this.Controls.Add(this.lblChiPhiThucTe);
            this.Controls.Add(this.dtpNgayHoanThanh);
            this.Controls.Add(this.lblNgayHoanThanh);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThaiMoi);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateMaintenanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật trạng thái bảo trì";
            this.Load += new System.EventHandler(this.UpdateMaintenanceForm_Load);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblBaoTriID;
        private System.Windows.Forms.Label lblTenCSVC;
        private System.Windows.Forms.Label lblNgayYeuCau;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.Label lblChiPhiCu;
        private System.Windows.Forms.Label lblTrangThaiCu;
        private System.Windows.Forms.Label lblTrangThaiMoi;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblNgayHoanThanh;
        private System.Windows.Forms.DateTimePicker dtpNgayHoanThanh;
        private System.Windows.Forms.Label lblChiPhiThucTe;
        private System.Windows.Forms.TextBox txtChiPhiThucTe;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}