namespace DBMS
{
    partial class BaoTriForm
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
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnLichDinhKy = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnThemBaoTri = new System.Windows.Forms.Button();
            this.dgvBaoTri = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).BeginInit();
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
            this.lblTitle.Text = "🔧 BẢO TRÌ - LỊCH BẢO TRÌ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelTop.Controls.Add(this.btnThongKe);
            this.panelTop.Controls.Add(this.btnLichDinhKy);
            this.panelTop.Controls.Add(this.btnTimKiem);
            this.panelTop.Controls.Add(this.btnCapNhat);
            this.panelTop.Controls.Add(this.btnThemBaoTri);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 60);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(950, 80);
            this.panelTop.TabIndex = 1;
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btnThongKe.FlatAppearance.BorderSize = 0;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Location = new System.Drawing.Point(480, 20);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(120, 40);
            this.btnThongKe.TabIndex = 3;
            this.btnThongKe.Text = "📊 Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnLichDinhKy
            // 
            this.btnLichDinhKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnLichDinhKy.FlatAppearance.BorderSize = 0;
            this.btnLichDinhKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLichDinhKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLichDinhKy.ForeColor = System.Drawing.Color.White;
            this.btnLichDinhKy.Location = new System.Drawing.Point(630, 20);
            this.btnLichDinhKy.Name = "btnLichDinhKy";
            this.btnLichDinhKy.Size = new System.Drawing.Size(150, 40);
            this.btnLichDinhKy.TabIndex = 4;
            this.btnLichDinhKy.Text = "📅 Lịch định kỳ";
            this.btnLichDinhKy.UseVisualStyleBackColor = false;
            this.btnLichDinhKy.Click += new System.EventHandler(this.btnLichDinhKy_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(320, 20);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(120, 40);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "� Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.Black;
            this.btnCapNhat.Location = new System.Drawing.Point(170, 20);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(120, 40);
            this.btnCapNhat.TabIndex = 1;
            this.btnCapNhat.Text = "✏️ Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnThemBaoTri
            // 
            this.btnThemBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemBaoTri.FlatAppearance.BorderSize = 0;
            this.btnThemBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnThemBaoTri.Location = new System.Drawing.Point(20, 20);
            this.btnThemBaoTri.Name = "btnThemBaoTri";
            this.btnThemBaoTri.Size = new System.Drawing.Size(120, 40);
            this.btnThemBaoTri.TabIndex = 0;
            this.btnThemBaoTri.Text = "➕ Thêm bảo trì";
            this.btnThemBaoTri.UseVisualStyleBackColor = false;
            this.btnThemBaoTri.Click += new System.EventHandler(this.btnThemBaoTri_Click);
            // 
            // dgvBaoTri
            // 
            this.dgvBaoTri.AllowUserToAddRows = false;
            this.dgvBaoTri.AllowUserToDeleteRows = false;
            this.dgvBaoTri.BackgroundColor = System.Drawing.Color.White;
            this.dgvBaoTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBaoTri.Location = new System.Drawing.Point(0, 140);
            this.dgvBaoTri.Name = "dgvBaoTri";
            this.dgvBaoTri.ReadOnly = true;
            this.dgvBaoTri.RowHeadersWidth = 51;
            this.dgvBaoTri.RowTemplate.Height = 24;
            this.dgvBaoTri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoTri.Size = new System.Drawing.Size(950, 460);
            this.dgvBaoTri.TabIndex = 2;
            // 
            // BaoTriForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.dgvBaoTri);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaoTriForm";
            this.Text = "Bảo trì";
            this.Load += new System.EventHandler(this.BaoTriForm_Load);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnThemBaoTri;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnLichDinhKy;
        private System.Windows.Forms.DataGridView dgvBaoTri;
    }
}
