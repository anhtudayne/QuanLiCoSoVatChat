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
            this.btnLichBaoTri = new System.Windows.Forms.Button();
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
            this.panelTop.Controls.Add(this.btnLichBaoTri);
            this.panelTop.Controls.Add(this.btnCapNhat);
            this.panelTop.Controls.Add(this.btnThemBaoTri);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 60);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(950, 80);
            this.panelTop.TabIndex = 1;
            // 
            // btnLichBaoTri
            // 
            this.btnLichBaoTri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLichBaoTri.FlatAppearance.BorderSize = 0;
            this.btnLichBaoTri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLichBaoTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLichBaoTri.ForeColor = System.Drawing.Color.White;
            this.btnLichBaoTri.Location = new System.Drawing.Point(420, 20);
            this.btnLichBaoTri.Name = "btnLichBaoTri";
            this.btnLichBaoTri.Size = new System.Drawing.Size(139, 40);
            this.btnLichBaoTri.TabIndex = 2;
            this.btnLichBaoTri.Text = "📅 Lịch bảo trì";
            this.btnLichBaoTri.UseVisualStyleBackColor = false;
            this.btnLichBaoTri.Click += new System.EventHandler(this.btnLichBaoTri_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.ForeColor = System.Drawing.Color.Black;
            this.btnCapNhat.Location = new System.Drawing.Point(240, 20);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(150, 40);
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
            this.btnThemBaoTri.Location = new System.Drawing.Point(60, 20);
            this.btnThemBaoTri.Name = "btnThemBaoTri";
            this.btnThemBaoTri.Size = new System.Drawing.Size(150, 40);
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
        private System.Windows.Forms.Button btnLichBaoTri;
        private System.Windows.Forms.DataGridView dgvBaoTri;
    }
}
