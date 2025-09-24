namespace DBMS
{
    partial class SearchCSVCForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxTimKiem = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblTenCSVC = new System.Windows.Forms.Label();
            this.txtTenCSVC = new System.Windows.Forms.TextBox();
            this.groupBoxKetQua = new System.Windows.Forms.GroupBox();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.btnChon = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.groupBoxTimKiem.SuspendLayout();
            this.groupBoxKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1067, 74);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔍 TÌM KIẾM CƠ SỞ VẬT CHẤT";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxTimKiem
            // 
            this.groupBoxTimKiem.Controls.Add(this.btnLamMoi);
            this.groupBoxTimKiem.Controls.Add(this.btnTimKiem);
            this.groupBoxTimKiem.Controls.Add(this.lblTinhTrang);
            this.groupBoxTimKiem.Controls.Add(this.cboTinhTrang);
            this.groupBoxTimKiem.Controls.Add(this.lblLoai);
            this.groupBoxTimKiem.Controls.Add(this.cboLoai);
            this.groupBoxTimKiem.Controls.Add(this.lblTenCSVC);
            this.groupBoxTimKiem.Controls.Add(this.txtTenCSVC);
            this.groupBoxTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxTimKiem.Location = new System.Drawing.Point(27, 98);
            this.groupBoxTimKiem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxTimKiem.Name = "groupBoxTimKiem";
            this.groupBoxTimKiem.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxTimKiem.Size = new System.Drawing.Size(1013, 148);
            this.groupBoxTimKiem.TabIndex = 1;
            this.groupBoxTimKiem.TabStop = false;
            this.groupBoxTimKiem.Text = "Thông tin tìm kiếm";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(773, 80);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(133, 37);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(773, 31);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(133, 37);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTinhTrang.Location = new System.Drawing.Point(20, 71);
            this.lblTinhTrang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Size = new System.Drawing.Size(77, 18);
            this.lblTinhTrang.TabIndex = 4;
            this.lblTinhTrang.Text = "Tình trạng:";
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboTinhTrang.FormattingEnabled = true;
            this.cboTinhTrang.Location = new System.Drawing.Point(133, 68);
            this.cboTinhTrang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(265, 26);
            this.cboTinhTrang.TabIndex = 5;
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLoai.Location = new System.Drawing.Point(467, 34);
            this.lblLoai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(40, 18);
            this.lblLoai.TabIndex = 2;
            this.lblLoai.Text = "Loại:";
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(533, 31);
            this.cboLoai.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(199, 26);
            this.cboLoai.TabIndex = 3;
            // 
            // lblTenCSVC
            // 
            this.lblTenCSVC.AutoSize = true;
            this.lblTenCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTenCSVC.Location = new System.Drawing.Point(20, 34);
            this.lblTenCSVC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenCSVC.Name = "lblTenCSVC";
            this.lblTenCSVC.Size = new System.Drawing.Size(82, 18);
            this.lblTenCSVC.TabIndex = 0;
            this.lblTenCSVC.Text = "Tên CSVC:";
            // 
            // txtTenCSVC
            // 
            this.txtTenCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTenCSVC.Location = new System.Drawing.Point(133, 31);
            this.txtTenCSVC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTenCSVC.Name = "txtTenCSVC";
            this.txtTenCSVC.Size = new System.Drawing.Size(265, 24);
            this.txtTenCSVC.TabIndex = 1;
            // 
            // groupBoxKetQua
            // 
            this.groupBoxKetQua.Controls.Add(this.lblKetQua);
            this.groupBoxKetQua.Controls.Add(this.dgvKetQua);
            this.groupBoxKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxKetQua.Location = new System.Drawing.Point(27, 271);
            this.groupBoxKetQua.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxKetQua.Name = "groupBoxKetQua";
            this.groupBoxKetQua.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxKetQua.Size = new System.Drawing.Size(1013, 431);
            this.groupBoxKetQua.TabIndex = 2;
            this.groupBoxKetQua.TabStop = false;
            this.groupBoxKetQua.Text = "Kết quả tìm kiếm";
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblKetQua.Location = new System.Drawing.Point(20, 31);
            this.lblKetQua.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(307, 18);
            this.lblKetQua.TabIndex = 1;
            this.lblKetQua.Text = "👆 Nhập thông tin tìm kiếm và nhấn \'Tìm kiếm\'";
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            this.dgvKetQua.BackgroundColor = System.Drawing.Color.White;
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(20, 62);
            this.dgvKetQua.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvKetQua.MultiSelect = false;
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.RowHeadersWidth = 51;
            this.dgvKetQua.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKetQua.Size = new System.Drawing.Size(973, 345);
            this.dgvKetQua.TabIndex = 0;
            this.dgvKetQua.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKetQua_CellDoubleClick);
            // 
            // btnChon
            // 
            this.btnChon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChon.Enabled = false;
            this.btnChon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnChon.ForeColor = System.Drawing.Color.White;
            this.btnChon.Location = new System.Drawing.Point(733, 726);
            this.btnChon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(133, 43);
            this.btnChon.TabIndex = 3;
            this.btnChon.Text = "✅ Chọn";
            this.btnChon.UseVisualStyleBackColor = false;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(893, 726);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(133, 43);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "❌ Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // SearchCSVCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1067, 788);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.groupBoxKetQua);
            this.Controls.Add(this.groupBoxTimKiem);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchCSVCForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm kiếm CSVC";
            this.Load += new System.EventHandler(this.SearchCSVCForm_Load);
            this.groupBoxTimKiem.ResumeLayout(false);
            this.groupBoxTimKiem.PerformLayout();
            this.groupBoxKetQua.ResumeLayout(false);
            this.groupBoxKetQua.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxTimKiem;
        private System.Windows.Forms.TextBox txtTenCSVC;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.ComboBox cboTinhTrang;
        private System.Windows.Forms.Label lblTenCSVC;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.GroupBox groupBoxKetQua;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Button btnChon;
        private System.Windows.Forms.Button btnHuy;
    }
}