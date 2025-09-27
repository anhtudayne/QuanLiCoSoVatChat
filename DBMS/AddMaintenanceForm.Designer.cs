namespace DBMS
{
    partial class AddMaintenanceForm
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
            this.lblCSVC = new System.Windows.Forms.Label();
            this.cboCSVC = new System.Windows.Forms.ComboBox();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.lblNhanVienGiamSat = new System.Windows.Forms.Label();
            this.cboNhanVienGiamSat = new System.Windows.Forms.ComboBox();
            this.lblNhanVienKyThuat = new System.Windows.Forms.Label();
            this.cboNhanVienKyThuat = new System.Windows.Forms.ComboBox();
            this.lblChiPhiDuKien = new System.Windows.Forms.Label();
            this.txtChiPhiDuKien = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.lblTitle.Size = new System.Drawing.Size(484, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "➕ THÊM YÊU CẦU BẢO TRÌ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSVC
            // 
            this.lblCSVC.AutoSize = true;
            this.lblCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCSVC.Location = new System.Drawing.Point(30, 70);
            this.lblCSVC.Name = "lblCSVC";
            this.lblCSVC.Size = new System.Drawing.Size(94, 17);
            this.lblCSVC.TabIndex = 1;
            this.lblCSVC.Text = "Chọn CSVC: *";
            // 
            // cboCSVC
            // 
            this.cboCSVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboCSVC.Location = new System.Drawing.Point(33, 90);
            this.cboCSVC.Name = "cboCSVC";
            this.cboCSVC.Size = new System.Drawing.Size(420, 24);
            this.cboCSVC.TabIndex = 2;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNoiDung.Location = new System.Drawing.Point(30, 130);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(138, 17);
            this.lblNoiDung.TabIndex = 3;
            this.lblNoiDung.Text = "Nội dung bảo trì: *";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNoiDung.Location = new System.Drawing.Point(33, 150);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(420, 60);
            this.txtNoiDung.TabIndex = 4;
            // 
            // lblNhanVienGiamSat
            // 
            this.lblNhanVienGiamSat.AutoSize = true;
            this.lblNhanVienGiamSat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNhanVienGiamSat.Location = new System.Drawing.Point(30, 230);
            this.lblNhanVienGiamSat.Name = "lblNhanVienGiamSat";
            this.lblNhanVienGiamSat.Size = new System.Drawing.Size(134, 17);
            this.lblNhanVienGiamSat.TabIndex = 5;
            this.lblNhanVienGiamSat.Text = "Nhân viên giám sát:";
            // 
            // cboNhanVienGiamSat
            // 
            this.cboNhanVienGiamSat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVienGiamSat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboNhanVienGiamSat.Location = new System.Drawing.Point(33, 250);
            this.cboNhanVienGiamSat.Name = "cboNhanVienGiamSat";
            this.cboNhanVienGiamSat.Size = new System.Drawing.Size(200, 24);
            this.cboNhanVienGiamSat.TabIndex = 6;
            // 
            // lblNhanVienKyThuat
            // 
            this.lblNhanVienKyThuat.AutoSize = true;
            this.lblNhanVienKyThuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNhanVienKyThuat.Location = new System.Drawing.Point(250, 230);
            this.lblNhanVienKyThuat.Name = "lblNhanVienKyThuat";
            this.lblNhanVienKyThuat.Size = new System.Drawing.Size(129, 17);
            this.lblNhanVienKyThuat.TabIndex = 7;
            this.lblNhanVienKyThuat.Text = "Nhân viên kỹ thuật:";
            // 
            // cboNhanVienKyThuat
            // 
            this.cboNhanVienKyThuat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVienKyThuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboNhanVienKyThuat.Location = new System.Drawing.Point(253, 250);
            this.cboNhanVienKyThuat.Name = "cboNhanVienKyThuat";
            this.cboNhanVienKyThuat.Size = new System.Drawing.Size(200, 24);
            this.cboNhanVienKyThuat.TabIndex = 8;
            // 
            // lblChiPhiDuKien
            // 
            this.lblChiPhiDuKien.AutoSize = true;
            this.lblChiPhiDuKien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblChiPhiDuKien.Location = new System.Drawing.Point(30, 290);
            this.lblChiPhiDuKien.Name = "lblChiPhiDuKien";
            this.lblChiPhiDuKien.Size = new System.Drawing.Size(107, 17);
            this.lblChiPhiDuKien.TabIndex = 9;
            this.lblChiPhiDuKien.Text = "Chi phí dự kiến:";
            // 
            // txtChiPhiDuKien
            // 
            this.txtChiPhiDuKien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtChiPhiDuKien.Location = new System.Drawing.Point(33, 310);
            this.txtChiPhiDuKien.Name = "txtChiPhiDuKien";
            this.txtChiPhiDuKien.Size = new System.Drawing.Size(200, 23);
            this.txtChiPhiDuKien.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(253, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Lưu";
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
            this.btnCancel.Location = new System.Drawing.Point(365, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(484, 421);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtChiPhiDuKien);
            this.Controls.Add(this.lblChiPhiDuKien);
            this.Controls.Add(this.cboNhanVienKyThuat);
            this.Controls.Add(this.lblNhanVienKyThuat);
            this.Controls.Add(this.cboNhanVienGiamSat);
            this.Controls.Add(this.lblNhanVienGiamSat);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.lblNoiDung);
            this.Controls.Add(this.cboCSVC);
            this.Controls.Add(this.lblCSVC);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddMaintenanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm yêu cầu bảo trì";
            this.Load += new System.EventHandler(this.AddMaintenanceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCSVC;
        private System.Windows.Forms.ComboBox cboCSVC;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Label lblNhanVienGiamSat;
        private System.Windows.Forms.ComboBox cboNhanVienGiamSat;
        private System.Windows.Forms.Label lblNhanVienKyThuat;
        private System.Windows.Forms.ComboBox cboNhanVienKyThuat;
        private System.Windows.Forms.Label lblChiPhiDuKien;
        private System.Windows.Forms.TextBox txtChiPhiDuKien;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}