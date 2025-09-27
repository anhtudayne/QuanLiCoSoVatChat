namespace DBMS
{
    partial class UpdateThanhLyForm
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
            this.lblCSVCInfo = new System.Windows.Forms.Label();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDoThanhLy = new System.Windows.Forms.TextBox();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.nudGiaTriThanhLy = new System.Windows.Forms.NumericUpDown();
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgayThanhLy = new System.Windows.Forms.DateTimePicker();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaTriThanhLy)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(500, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "✏️ CẬP NHẬT THANH LÝ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCSVCInfo
            // 
            this.lblCSVCInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSVCInfo.Location = new System.Drawing.Point(30, 70);
            this.lblCSVCInfo.Name = "lblCSVCInfo";
            this.lblCSVCInfo.Size = new System.Drawing.Size(440, 30);
            this.lblCSVCInfo.TabIndex = 1;
            this.lblCSVCInfo.Text = "CSVC: ";
            // 
            // lblLyDo
            // 
            this.lblLyDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLyDo.Location = new System.Drawing.Point(30, 120);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(150, 25);
            this.lblLyDo.TabIndex = 2;
            this.lblLyDo.Text = "Lý do thanh lý:";
            // 
            // txtLyDoThanhLy
            // 
            this.txtLyDoThanhLy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyDoThanhLy.Location = new System.Drawing.Point(30, 150);
            this.txtLyDoThanhLy.Multiline = true;
            this.txtLyDoThanhLy.Name = "txtLyDoThanhLy";
            this.txtLyDoThanhLy.Size = new System.Drawing.Size(440, 80);
            this.txtLyDoThanhLy.TabIndex = 3;
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaTri.Location = new System.Drawing.Point(30, 250);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(150, 25);
            this.lblGiaTri.TabIndex = 4;
            this.lblGiaTri.Text = "Giá trị thanh lý:";
            // 
            // nudGiaTriThanhLy
            // 
            this.nudGiaTriThanhLy.DecimalPlaces = 0;
            this.nudGiaTriThanhLy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudGiaTriThanhLy.Location = new System.Drawing.Point(30, 280);
            this.nudGiaTriThanhLy.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudGiaTriThanhLy.Name = "nudGiaTriThanhLy";
            this.nudGiaTriThanhLy.Size = new System.Drawing.Size(200, 26);
            this.nudGiaTriThanhLy.TabIndex = 5;
            this.nudGiaTriThanhLy.ThousandsSeparator = true;
            // 
            // lblNgay
            // 
            this.lblNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgay.Location = new System.Drawing.Point(270, 250);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(150, 25);
            this.lblNgay.TabIndex = 6;
            this.lblNgay.Text = "Ngày thanh lý:";
            // 
            // dtpNgayThanhLy
            // 
            this.dtpNgayThanhLy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayThanhLy.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayThanhLy.Location = new System.Drawing.Point(270, 280);
            this.dtpNgayThanhLy.Name = "dtpNgayThanhLy";
            this.dtpNgayThanhLy.Size = new System.Drawing.Size(200, 26);
            this.dtpNgayThanhLy.TabIndex = 7;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelBottom.Controls.Add(this.btnHuy);
            this.panelBottom.Controls.Add(this.btnCapNhat);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 350);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(500, 80);
            this.panelBottom.TabIndex = 8;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(280, 20);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 40);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "❌ Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.Black;
            this.btnCapNhat.Location = new System.Drawing.Point(130, 20);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(120, 40);
            this.btnCapNhat.TabIndex = 0;
            this.btnCapNhat.Text = "💾 Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // UpdateThanhLyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(500, 430);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.dtpNgayThanhLy);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.nudGiaTriThanhLy);
            this.Controls.Add(this.lblGiaTri);
            this.Controls.Add(this.txtLyDoThanhLy);
            this.Controls.Add(this.lblLyDo);
            this.Controls.Add(this.lblCSVCInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateThanhLyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật thanh lý CSVC";
            this.Load += new System.EventHandler(this.UpdateThanhLyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaTriThanhLy)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCSVCInfo;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDoThanhLy;
        private System.Windows.Forms.Label lblGiaTri;
        private System.Windows.Forms.NumericUpDown nudGiaTriThanhLy;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgayThanhLy;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnHuy;
    }
}