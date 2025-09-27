namespace DBMS
{
    partial class LichBaoTriDinhKyForm
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
            this.btnLamMoi = new System.Windows.Forms.Button();

            this.btnXoaLichBaoTri = new System.Windows.Forms.Button();
            this.btnTaoYeuCauBaoTri = new System.Windows.Forms.Button();
            this.dgvLichBaoTri = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichBaoTri)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(1000, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📅 LỊCH BẢO TRÌ ĐỊNH KỲ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelTop.Controls.Add(this.btnLamMoi);

            this.panelTop.Controls.Add(this.btnXoaLichBaoTri);
            this.panelTop.Controls.Add(this.btnTaoYeuCauBaoTri);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 60);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 80);
            this.panelTop.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(780, 20);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(140, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // 
            // btnXoaLichBaoTri
            // 
            this.btnXoaLichBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoaLichBaoTri.FlatAppearance.BorderSize = 0;
            this.btnXoaLichBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaLichBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaLichBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnXoaLichBaoTri.Location = new System.Drawing.Point(300, 20);
            this.btnXoaLichBaoTri.Name = "btnXoaLichBaoTri";
            this.btnXoaLichBaoTri.Size = new System.Drawing.Size(180, 40);
            this.btnXoaLichBaoTri.TabIndex = 1;
            this.btnXoaLichBaoTri.Text = "🗑️ Xóa lịch bảo trì";
            this.btnXoaLichBaoTri.UseVisualStyleBackColor = false;
            this.btnXoaLichBaoTri.Click += new System.EventHandler(this.btnXoaLichBaoTri_Click);
            // 
            // btnTaoYeuCauBaoTri
            // 
            this.btnTaoYeuCauBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnTaoYeuCauBaoTri.FlatAppearance.BorderSize = 0;
            this.btnTaoYeuCauBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoYeuCauBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoYeuCauBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnTaoYeuCauBaoTri.Location = new System.Drawing.Point(40, 20);
            this.btnTaoYeuCauBaoTri.Name = "btnTaoYeuCauBaoTri";
            this.btnTaoYeuCauBaoTri.Size = new System.Drawing.Size(200, 40);
            this.btnTaoYeuCauBaoTri.TabIndex = 0;
            this.btnTaoYeuCauBaoTri.Text = "✨ Tạo yêu cầu bảo trì";
            this.btnTaoYeuCauBaoTri.UseVisualStyleBackColor = false;
            this.btnTaoYeuCauBaoTri.Click += new System.EventHandler(this.btnTaoYeuCauBaoTri_Click);
            // 
            // dgvLichBaoTri
            // 
            this.dgvLichBaoTri.AllowUserToAddRows = false;
            this.dgvLichBaoTri.AllowUserToDeleteRows = false;
            this.dgvLichBaoTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichBaoTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichBaoTri.Location = new System.Drawing.Point(0, 140);
            this.dgvLichBaoTri.Name = "dgvLichBaoTri";
            this.dgvLichBaoTri.ReadOnly = true;
            this.dgvLichBaoTri.RowHeadersWidth = 51;
            this.dgvLichBaoTri.RowTemplate.Height = 24;
            this.dgvLichBaoTri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichBaoTri.Size = new System.Drawing.Size(1000, 460);
            this.dgvLichBaoTri.TabIndex = 2;
            // 
            // LichBaoTriDinhKyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvLichBaoTri);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LichBaoTriDinhKyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lịch Bảo Trì Định Kỳ";
            this.Load += new System.EventHandler(this.LichBaoTriDinhKyForm_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichBaoTri)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnTaoYeuCauBaoTri;
        private System.Windows.Forms.Button btnXoaLichBaoTri;

        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvLichBaoTri;
    }
}