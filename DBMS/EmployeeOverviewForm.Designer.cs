namespace DBMS
{
    partial class EmployeeOverviewForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.lblStatisticsTitle = new System.Windows.Forms.Label();
            this.tabPositions = new System.Windows.Forms.TabPage();
            this.dgvPositions = new System.Windows.Forms.DataGridView();
            this.lblPositionsTitle = new System.Windows.Forms.Label();
            this.tabBirthdays = new System.Windows.Forms.TabPage();
            this.panelBirthdayFilter = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.lblBirthdayCount = new System.Windows.Forms.Label();
            this.dgvBirthdays = new System.Windows.Forms.DataGridView();
            this.lblBirthdaysTitle = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            this.tabPositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).BeginInit();
            this.tabBirthdays.SuspendLayout();
            this.panelBirthdayFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBirthdays)).BeginInit();
            this.panelTop.SuspendLayout();
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
            this.lblTitle.Text = "📊 TỔNG QUAN NHÂN VIÊN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabStatistics);
            this.tabControl.Controls.Add(this.tabPositions);
            this.tabControl.Controls.Add(this.tabBirthdays);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 120);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 480);
            this.tabControl.TabIndex = 1;
            // 
            // tabStatistics
            // 
            this.tabStatistics.Controls.Add(this.dgvStatistics);
            this.tabStatistics.Controls.Add(this.lblStatisticsTitle);
            this.tabStatistics.Location = new System.Drawing.Point(4, 29);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(992, 447);
            this.tabStatistics.TabIndex = 0;
            this.tabStatistics.Text = "📈 Thống kê trạng thái";
            this.tabStatistics.UseVisualStyleBackColor = true;
            // 
            // dgvStatistics
            // 
            this.dgvStatistics.AllowUserToAddRows = false;
            this.dgvStatistics.AllowUserToDeleteRows = false;
            this.dgvStatistics.BackgroundColor = System.Drawing.Color.White;
            this.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatistics.Location = new System.Drawing.Point(3, 43);
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.ReadOnly = true;
            this.dgvStatistics.RowHeadersWidth = 51;
            this.dgvStatistics.RowTemplate.Height = 24;
            this.dgvStatistics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatistics.Size = new System.Drawing.Size(986, 401);
            this.dgvStatistics.TabIndex = 1;
            // 
            // lblStatisticsTitle
            // 
            this.lblStatisticsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.lblStatisticsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatisticsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatisticsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblStatisticsTitle.Location = new System.Drawing.Point(3, 3);
            this.lblStatisticsTitle.Name = "lblStatisticsTitle";
            this.lblStatisticsTitle.Size = new System.Drawing.Size(986, 40);
            this.lblStatisticsTitle.TabIndex = 0;
            this.lblStatisticsTitle.Text = "Thống kê nhân viên theo trạng thái làm việc";
            this.lblStatisticsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPositions
            // 
            this.tabPositions.Controls.Add(this.dgvPositions);
            this.tabPositions.Controls.Add(this.lblPositionsTitle);
            this.tabPositions.Location = new System.Drawing.Point(4, 29);
            this.tabPositions.Name = "tabPositions";
            this.tabPositions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPositions.Size = new System.Drawing.Size(992, 447);
            this.tabPositions.TabIndex = 1;
            this.tabPositions.Text = "💼 Thống kê chức vụ";
            this.tabPositions.UseVisualStyleBackColor = true;
            // 
            // dgvPositions
            // 
            this.dgvPositions.AllowUserToAddRows = false;
            this.dgvPositions.AllowUserToDeleteRows = false;
            this.dgvPositions.BackgroundColor = System.Drawing.Color.White;
            this.dgvPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPositions.Location = new System.Drawing.Point(3, 43);
            this.dgvPositions.Name = "dgvPositions";
            this.dgvPositions.ReadOnly = true;
            this.dgvPositions.RowHeadersWidth = 51;
            this.dgvPositions.RowTemplate.Height = 24;
            this.dgvPositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPositions.Size = new System.Drawing.Size(986, 401);
            this.dgvPositions.TabIndex = 1;
            // 
            // lblPositionsTitle
            // 
            this.lblPositionsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.lblPositionsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPositionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblPositionsTitle.Location = new System.Drawing.Point(3, 3);
            this.lblPositionsTitle.Name = "lblPositionsTitle";
            this.lblPositionsTitle.Size = new System.Drawing.Size(986, 40);
            this.lblPositionsTitle.TabIndex = 0;
            this.lblPositionsTitle.Text = "Số lượng nhân viên theo chức vụ (chỉ tính nhân viên đang làm)";
            this.lblPositionsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabBirthdays
            // 
            this.tabBirthdays.Controls.Add(this.dgvBirthdays);
            this.tabBirthdays.Controls.Add(this.panelBirthdayFilter);
            this.tabBirthdays.Location = new System.Drawing.Point(4, 29);
            this.tabBirthdays.Name = "tabBirthdays";
            this.tabBirthdays.Size = new System.Drawing.Size(992, 447);
            this.tabBirthdays.TabIndex = 2;
            this.tabBirthdays.Text = "🎂 Sinh nhật";
            this.tabBirthdays.UseVisualStyleBackColor = true;
            // 
            // panelBirthdayFilter
            // 
            this.panelBirthdayFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelBirthdayFilter.Controls.Add(this.lblMonth);
            this.panelBirthdayFilter.Controls.Add(this.cmbMonth);
            this.panelBirthdayFilter.Controls.Add(this.lblBirthdayCount);
            this.panelBirthdayFilter.Controls.Add(this.lblBirthdaysTitle);
            this.panelBirthdayFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBirthdayFilter.Location = new System.Drawing.Point(0, 0);
            this.panelBirthdayFilter.Name = "panelBirthdayFilter";
            this.panelBirthdayFilter.Size = new System.Drawing.Size(992, 80);
            this.panelBirthdayFilter.TabIndex = 0;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(20, 50);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(59, 20);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "Tháng:";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "Tháng 1",
            "Tháng 2",
            "Tháng 3",
            "Tháng 4",
            "Tháng 5",
            "Tháng 6",
            "Tháng 7",
            "Tháng 8",
            "Tháng 9",
            "Tháng 10",
            "Tháng 11",
            "Tháng 12"});
            this.cmbMonth.Location = new System.Drawing.Point(85, 47);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(120, 28);
            this.cmbMonth.TabIndex = 1;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // lblBirthdayCount
            // 
            this.lblBirthdayCount.AutoSize = true;
            this.lblBirthdayCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBirthdayCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblBirthdayCount.Location = new System.Drawing.Point(250, 50);
            this.lblBirthdayCount.Name = "lblBirthdayCount";
            this.lblBirthdayCount.Size = new System.Drawing.Size(50, 20);
            this.lblBirthdayCount.TabIndex = 2;
            this.lblBirthdayCount.Text = "0 NV";
            // 
            // dgvBirthdays
            // 
            this.dgvBirthdays.AllowUserToAddRows = false;
            this.dgvBirthdays.AllowUserToDeleteRows = false;
            this.dgvBirthdays.BackgroundColor = System.Drawing.Color.White;
            this.dgvBirthdays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBirthdays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBirthdays.Location = new System.Drawing.Point(0, 80);
            this.dgvBirthdays.Name = "dgvBirthdays";
            this.dgvBirthdays.ReadOnly = true;
            this.dgvBirthdays.RowHeadersWidth = 51;
            this.dgvBirthdays.RowTemplate.Height = 24;
            this.dgvBirthdays.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBirthdays.Size = new System.Drawing.Size(992, 367);
            this.dgvBirthdays.TabIndex = 1;
            // 
            // lblBirthdaysTitle
            // 
            this.lblBirthdaysTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBirthdaysTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblBirthdaysTitle.Location = new System.Drawing.Point(20, 10);
            this.lblBirthdaysTitle.Name = "lblBirthdaysTitle";
            this.lblBirthdaysTitle.Size = new System.Drawing.Size(800, 30);
            this.lblBirthdaysTitle.TabIndex = 3;
            this.lblBirthdaysTitle.Text = "Danh sách nhân viên có sinh nhật";
            this.lblBirthdaysTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 60);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(20, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // EmployeeOverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblTitle);
            this.Name = "EmployeeOverviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tổng quan Nhân viên";
            this.Load += new System.EventHandler(this.EmployeeOverviewForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
            this.tabPositions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).EndInit();
            this.tabBirthdays.ResumeLayout(false);
            this.panelBirthdayFilter.ResumeLayout(false);
            this.panelBirthdayFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBirthdays)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.Label lblStatisticsTitle;
        private System.Windows.Forms.TabPage tabPositions;
        private System.Windows.Forms.DataGridView dgvPositions;
        private System.Windows.Forms.Label lblPositionsTitle;
        private System.Windows.Forms.TabPage tabBirthdays;
        private System.Windows.Forms.Panel panelBirthdayFilter;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label lblBirthdayCount;
        private System.Windows.Forms.DataGridView dgvBirthdays;
        private System.Windows.Forms.Label lblBirthdaysTitle;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnRefresh;
    }
}