namespace DBMS
{
    partial class BaoCaoForm
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
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.btnBaoCaoBaoTri = new System.Windows.Forms.Button();
            this.btnThongKeNhanVien = new System.Windows.Forms.Button();
            this.btnThongKeCSVC = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.lblChartPlaceholder = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelChart.SuspendLayout();
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
            this.lblTitle.Text = "📊 BÁO CÁO - THỐNG KÊ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelTop.Controls.Add(this.btnXuatBaoCao);
            this.panelTop.Controls.Add(this.btnBaoCaoBaoTri);
            this.panelTop.Controls.Add(this.btnThongKeNhanVien);
            this.panelTop.Controls.Add(this.btnThongKeCSVC);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 60);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(950, 80);
            this.panelTop.TabIndex = 1;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXuatBaoCao.FlatAppearance.BorderSize = 0;
            this.btnXuatBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(720, 20);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(150, 40);
            this.btnXuatBaoCao.TabIndex = 3;
            this.btnXuatBaoCao.Text = "📄 Xuất báo cáo";
            this.btnXuatBaoCao.UseVisualStyleBackColor = false;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // btnBaoCaoBaoTri
            // 
            this.btnBaoCaoBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnBaoCaoBaoTri.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCaoBaoTri.ForeColor = System.Drawing.Color.Black;
            this.btnBaoCaoBaoTri.Location = new System.Drawing.Point(500, 20);
            this.btnBaoCaoBaoTri.Name = "btnBaoCaoBaoTri";
            this.btnBaoCaoBaoTri.Size = new System.Drawing.Size(150, 40);
            this.btnBaoCaoBaoTri.TabIndex = 2;
            this.btnBaoCaoBaoTri.Text = "🔧 BC Bảo trì";
            this.btnBaoCaoBaoTri.UseVisualStyleBackColor = false;
            this.btnBaoCaoBaoTri.Click += new System.EventHandler(this.btnBaoCaoBaoTri_Click);
            // 
            // btnThongKeNhanVien
            // 
            this.btnThongKeNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnThongKeNhanVien.FlatAppearance.BorderSize = 0;
            this.btnThongKeNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKeNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnThongKeNhanVien.Location = new System.Drawing.Point(280, 20);
            this.btnThongKeNhanVien.Name = "btnThongKeNhanVien";
            this.btnThongKeNhanVien.Size = new System.Drawing.Size(150, 40);
            this.btnThongKeNhanVien.TabIndex = 1;
            this.btnThongKeNhanVien.Text = "👥 TK Nhân viên";
            this.btnThongKeNhanVien.UseVisualStyleBackColor = false;
            this.btnThongKeNhanVien.Click += new System.EventHandler(this.btnThongKeNhanVien_Click);
            // 
            // btnThongKeCSVC
            // 
            this.btnThongKeCSVC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnThongKeCSVC.FlatAppearance.BorderSize = 0;
            this.btnThongKeCSVC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKeCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeCSVC.ForeColor = System.Drawing.Color.White;
            this.btnThongKeCSVC.Location = new System.Drawing.Point(60, 20);
            this.btnThongKeCSVC.Name = "btnThongKeCSVC";
            this.btnThongKeCSVC.Size = new System.Drawing.Size(150, 40);
            this.btnThongKeCSVC.TabIndex = 0;
            this.btnThongKeCSVC.Text = "🏢 TK CSVC";
            this.btnThongKeCSVC.UseVisualStyleBackColor = false;
            this.btnThongKeCSVC.Click += new System.EventHandler(this.btnThongKeCSVC_Click);
            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChart.Controls.Add(this.lblChartPlaceholder);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 140);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(950, 460);
            this.panelChart.TabIndex = 2;
            // 
            // lblChartPlaceholder
            // 
            this.lblChartPlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblChartPlaceholder.AutoSize = true;
            this.lblChartPlaceholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartPlaceholder.ForeColor = System.Drawing.Color.Gray;
            this.lblChartPlaceholder.Location = new System.Drawing.Point(350, 200);
            this.lblChartPlaceholder.Name = "lblChartPlaceholder";
            this.lblChartPlaceholder.Size = new System.Drawing.Size(250, 58);
            this.lblChartPlaceholder.TabIndex = 0;
            this.lblChartPlaceholder.Text = "Khu vực hiển thị biểu đồ\r\nthống kê sẽ được phát triển";
            this.lblChartPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BaoCaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaoCaoForm";
            this.Text = "Báo cáo - Thống kê";
            this.Load += new System.EventHandler(this.BaoCaoForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelChart.ResumeLayout(false);
            this.panelChart.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnThongKeCSVC;
        private System.Windows.Forms.Button btnThongKeNhanVien;
        private System.Windows.Forms.Button btnBaoCaoBaoTri;
        private System.Windows.Forms.Button btnXuatBaoCao;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Label lblChartPlaceholder;
    }
}
