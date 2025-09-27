namespace DBMS
{
    partial class ThanhLyForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnChiTiet = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXemLichSu = new System.Windows.Forms.Button();
            this.btnThanhLy = new System.Windows.Forms.Button();
            this.dgvThanhLy = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhLy)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(950, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🗑️ THANH LÝ CSVC";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelTop.Controls.Add(this.btnChiTiet);
            this.panelTop.Controls.Add(this.btnLamMoi);
            this.panelTop.Controls.Add(this.btnCapNhat);
            this.panelTop.Controls.Add(this.btnXemLichSu);
            this.panelTop.Controls.Add(this.btnThanhLy);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 60);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(950, 80);
            this.panelTop.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(703, 20);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnChiTiet.FlatAppearance.BorderSize = 0;
            this.btnChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnChiTiet.Location = new System.Drawing.Point(557, 20);
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Size = new System.Drawing.Size(120, 40);
            this.btnChiTiet.TabIndex = 4;
            this.btnChiTiet.Text = "📊 Chi tiết";
            this.btnChiTiet.UseVisualStyleBackColor = false;
            this.btnChiTiet.Click += new System.EventHandler(this.btnChiTiet_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.Black;
            this.btnCapNhat.Location = new System.Drawing.Point(386, 20);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(150, 40);
            this.btnCapNhat.TabIndex = 2;
            this.btnCapNhat.Text = "✏️ Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnXemLichSu
            // 
            this.btnXemLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnXemLichSu.FlatAppearance.BorderSize = 0;
            this.btnXemLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemLichSu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemLichSu.ForeColor = System.Drawing.Color.White;
            this.btnXemLichSu.Location = new System.Drawing.Point(217, 20);
            this.btnXemLichSu.Name = "btnXemLichSu";
            this.btnXemLichSu.Size = new System.Drawing.Size(150, 40);
            this.btnXemLichSu.TabIndex = 1;
            this.btnXemLichSu.Text = "📋 Xem lịch sử";
            this.btnXemLichSu.UseVisualStyleBackColor = false;
            this.btnXemLichSu.Click += new System.EventHandler(this.btnXemLichSu_Click);
            // 
            // btnThanhLy
            // 
            this.btnThanhLy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnThanhLy.FlatAppearance.BorderSize = 0;
            this.btnThanhLy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhLy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhLy.ForeColor = System.Drawing.Color.White;
            this.btnThanhLy.Location = new System.Drawing.Point(50, 20);
            this.btnThanhLy.Name = "btnThanhLy";
            this.btnThanhLy.Size = new System.Drawing.Size(150, 40);
            this.btnThanhLy.TabIndex = 0;
            this.btnThanhLy.Text = "🗑️ Thanh lý";
            this.btnThanhLy.UseVisualStyleBackColor = false;
            this.btnThanhLy.Click += new System.EventHandler(this.btnThanhLy_Click);
            // 
            // dgvThanhLy
            // 
            this.dgvThanhLy.AllowUserToAddRows = false;
            this.dgvThanhLy.AllowUserToDeleteRows = false;
            this.dgvThanhLy.BackgroundColor = System.Drawing.Color.White;
            this.dgvThanhLy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThanhLy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThanhLy.Location = new System.Drawing.Point(0, 140);
            this.dgvThanhLy.Name = "dgvThanhLy";
            this.dgvThanhLy.ReadOnly = true;
            this.dgvThanhLy.RowHeadersWidth = 51;
            this.dgvThanhLy.RowTemplate.Height = 24;
            this.dgvThanhLy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThanhLy.Size = new System.Drawing.Size(950, 460);
            this.dgvThanhLy.TabIndex = 2;
            // 
            // ThanhLyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.dgvThanhLy);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThanhLyForm";
            this.Text = "Thanh lý CSVC";
            this.Load += new System.EventHandler(this.ThanhLyForm_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhLy)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnThanhLy;
        private System.Windows.Forms.Button btnXemLichSu;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnChiTiet;
        private System.Windows.Forms.DataGridView dgvThanhLy;
    }
}
