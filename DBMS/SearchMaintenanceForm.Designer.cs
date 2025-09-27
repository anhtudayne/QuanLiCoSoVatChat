namespace DBMS
{
    partial class SearchMaintenanceForm
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
            this.groupBoxCriteria = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.cboCSVC = new System.Windows.Forms.ComboBox();
            this.lblCSVC = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.lblResultCount = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxCriteria.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1312, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔍 TÌM KIẾM LỊCH SỬ BẢO TRÌ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxCriteria
            // 
            this.groupBoxCriteria.Controls.Add(this.btnClear);
            this.groupBoxCriteria.Controls.Add(this.btnSearch);
            this.groupBoxCriteria.Controls.Add(this.dtpDenNgay);
            this.groupBoxCriteria.Controls.Add(this.lblDenNgay);
            this.groupBoxCriteria.Controls.Add(this.dtpTuNgay);
            this.groupBoxCriteria.Controls.Add(this.lblTuNgay);
            this.groupBoxCriteria.Controls.Add(this.cboCSVC);
            this.groupBoxCriteria.Controls.Add(this.lblCSVC);
            this.groupBoxCriteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxCriteria.Location = new System.Drawing.Point(27, 86);
            this.groupBoxCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxCriteria.Name = "groupBoxCriteria";
            this.groupBoxCriteria.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxCriteria.Size = new System.Drawing.Size(1259, 185);
            this.groupBoxCriteria.TabIndex = 1;
            this.groupBoxCriteria.TabStop = false;
            this.groupBoxCriteria.Text = "Tiêu chí tìm kiếm";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(977, 117);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(133, 43);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "🔄 Xóa";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(977, 62);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(133, 43);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(777, 62);
            this.dtpDenNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(159, 26);
            this.dtpDenNgay.TabIndex = 7;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDenNgay.Location = new System.Drawing.Point(773, 37);
            this.lblDenNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(84, 20);
            this.lblDenNgay.TabIndex = 6;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(578, 62);
            this.dtpTuNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(159, 26);
            this.dtpTuNgay.TabIndex = 5;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTuNgay.Location = new System.Drawing.Point(573, 37);
            this.lblTuNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(73, 20);
            this.lblTuNgay.TabIndex = 4;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // cboCSVC
            // 
            this.cboCSVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboCSVC.Location = new System.Drawing.Point(31, 62);
            this.cboCSVC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboCSVC.Name = "cboCSVC";
            this.cboCSVC.Size = new System.Drawing.Size(265, 28);
            this.cboCSVC.TabIndex = 1;
            // 
            // lblCSVC
            // 
            this.lblCSVC.AutoSize = true;
            this.lblCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCSVC.Location = new System.Drawing.Point(27, 37);
            this.lblCSVC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCSVC.Name = "lblCSVC";
            this.lblCSVC.Size = new System.Drawing.Size(60, 20);
            this.lblCSVC.TabIndex = 0;
            this.lblCSVC.Text = "CSVC:";
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.lblResultCount);
            this.groupBoxResults.Controls.Add(this.dgvResults);
            this.groupBoxResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxResults.Location = new System.Drawing.Point(27, 295);
            this.groupBoxResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxResults.Size = new System.Drawing.Size(1259, 431);
            this.groupBoxResults.TabIndex = 2;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Kết quả tìm kiếm";
            // 
            // lblResultCount
            // 
            this.lblResultCount.AutoSize = true;
            this.lblResultCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblResultCount.Location = new System.Drawing.Point(20, 31);
            this.lblResultCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(0, 20);
            this.lblResultCount.TabIndex = 1;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(20, 68);
            this.dgvResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(1219, 345);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1152, 751);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 43);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SearchMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1312, 814);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBoxCriteria);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchMaintenanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm kiếm lịch sử bảo trì";
            this.Load += new System.EventHandler(this.SearchMaintenanceForm_Load);
            this.groupBoxCriteria.ResumeLayout(false);
            this.groupBoxCriteria.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxCriteria;
        private System.Windows.Forms.Label lblCSVC;
        private System.Windows.Forms.ComboBox cboCSVC;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblResultCount;
        private System.Windows.Forms.Button btnClose;
    }
}