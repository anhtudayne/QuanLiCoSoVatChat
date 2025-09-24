namespace DBMS
{
    partial class AddCSVCForm
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
            this.groupBoxThongTinCoBan = new System.Windows.Forms.GroupBox();
            this.txtTenCSVC = new System.Windows.Forms.TextBox();
            this.txtMaCSVC = new System.Windows.Forms.TextBox();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.cboViTri = new System.Windows.Forms.ComboBox();
            this.nudGiaTri = new System.Windows.Forms.NumericUpDown();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblTenCSVC = new System.Windows.Forms.Label();
            this.lblMaCSVC = new System.Windows.Forms.Label();
            this.lblLoai = new System.Windows.Forms.Label();
            this.lblViTri = new System.Windows.Forms.Label();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.groupBoxNhaCungCap = new System.Windows.Forms.GroupBox();
            this.txtNhaCungCap = new System.Windows.Forms.TextBox();
            this.txtSDTNCC = new System.Windows.Forms.TextBox();
            this.txtEmailNCC = new System.Windows.Forms.TextBox();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.lblSDTNCC = new System.Windows.Forms.Label();
            this.lblEmailNCC = new System.Windows.Forms.Label();
            this.groupBoxThongTinSuDung = new System.Windows.Forms.GroupBox();
            this.dtpNgayMua = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayHetBaoHanh = new System.Windows.Forms.DateTimePicker();
            this.nudThoiGianSuDung = new System.Windows.Forms.NumericUpDown();
            this.txtGhiChuSuDung = new System.Windows.Forms.TextBox();
            this.lblNgayMua = new System.Windows.Forms.Label();
            this.lblNgayHetBaoHanh = new System.Windows.Forms.Label();
            this.lblThoiGianSuDung = new System.Windows.Forms.Label();
            this.lblGhiChuSuDung = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.groupBoxThongTinCoBan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaTri)).BeginInit();
            this.groupBoxNhaCungCap.SuspendLayout();
            this.groupBoxThongTinSuDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiGianSuDung)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "➕ THÊM CƠ SỞ VẬT CHẤT MỚI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxThongTinCoBan
            // 
            this.groupBoxThongTinCoBan.Controls.Add(this.lblTinhTrang);
            this.groupBoxThongTinCoBan.Controls.Add(this.cboTinhTrang);
            this.groupBoxThongTinCoBan.Controls.Add(this.lblGiaTri);
            this.groupBoxThongTinCoBan.Controls.Add(this.nudGiaTri);
            this.groupBoxThongTinCoBan.Controls.Add(this.lblViTri);
            this.groupBoxThongTinCoBan.Controls.Add(this.cboViTri);
            this.groupBoxThongTinCoBan.Controls.Add(this.lblLoai);
            this.groupBoxThongTinCoBan.Controls.Add(this.cboLoai);
            this.groupBoxThongTinCoBan.Controls.Add(this.lblMaCSVC);
            this.groupBoxThongTinCoBan.Controls.Add(this.txtMaCSVC);
            this.groupBoxThongTinCoBan.Controls.Add(this.lblTenCSVC);
            this.groupBoxThongTinCoBan.Controls.Add(this.txtTenCSVC);
            this.groupBoxThongTinCoBan.Controls.Add(this.lblGhiChu);
            this.groupBoxThongTinCoBan.Controls.Add(this.txtGhiChu);
            this.groupBoxThongTinCoBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxThongTinCoBan.Location = new System.Drawing.Point(20, 80);
            this.groupBoxThongTinCoBan.Name = "groupBoxThongTinCoBan";
            this.groupBoxThongTinCoBan.Size = new System.Drawing.Size(560, 200);
            this.groupBoxThongTinCoBan.TabIndex = 1;
            this.groupBoxThongTinCoBan.TabStop = false;
            this.groupBoxThongTinCoBan.Text = "Thông tin cơ bản";
            // 
            // txtTenCSVC
            // 
            this.txtTenCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtTenCSVC.Location = new System.Drawing.Point(120, 25);
            this.txtTenCSVC.Name = "txtTenCSVC";
            this.txtTenCSVC.Size = new System.Drawing.Size(200, 21);
            this.txtTenCSVC.TabIndex = 1;
            // 
            // txtMaCSVC
            // 
            this.txtMaCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMaCSVC.Location = new System.Drawing.Point(420, 25);
            this.txtMaCSVC.Name = "txtMaCSVC";
            this.txtMaCSVC.Size = new System.Drawing.Size(120, 21);
            this.txtMaCSVC.TabIndex = 3;
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(120, 55);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(200, 23);
            this.cboLoai.TabIndex = 5;
            // 
            // cboViTri
            // 
            this.cboViTri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboViTri.FormattingEnabled = true;
            this.cboViTri.Location = new System.Drawing.Point(420, 55);
            this.cboViTri.Name = "cboViTri";
            this.cboViTri.Size = new System.Drawing.Size(120, 23);
            this.cboViTri.TabIndex = 7;
            // 
            // nudGiaTri
            // 
            this.nudGiaTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.nudGiaTri.Location = new System.Drawing.Point(120, 85);
            this.nudGiaTri.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudGiaTri.Name = "nudGiaTri";
            this.nudGiaTri.Size = new System.Drawing.Size(200, 21);
            this.nudGiaTri.TabIndex = 9;
            this.nudGiaTri.ThousandsSeparator = true;
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboTinhTrang.FormattingEnabled = true;
            this.cboTinhTrang.Location = new System.Drawing.Point(420, 85);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(120, 23);
            this.cboTinhTrang.TabIndex = 11;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtGhiChu.Location = new System.Drawing.Point(120, 115);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(420, 60);
            this.txtGhiChu.TabIndex = 13;
            // 
            // lblTenCSVC
            // 
            this.lblTenCSVC.AutoSize = true;
            this.lblTenCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTenCSVC.Location = new System.Drawing.Point(15, 28);
            this.lblTenCSVC.Name = "lblTenCSVC";
            this.lblTenCSVC.Size = new System.Drawing.Size(76, 15);
            this.lblTenCSVC.TabIndex = 0;
            this.lblTenCSVC.Text = "Tên CSVC (*)";
            // 
            // lblMaCSVC
            // 
            this.lblMaCSVC.AutoSize = true;
            this.lblMaCSVC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblMaCSVC.Location = new System.Drawing.Point(350, 28);
            this.lblMaCSVC.Name = "lblMaCSVC";
            this.lblMaCSVC.Size = new System.Drawing.Size(54, 15);
            this.lblMaCSVC.TabIndex = 2;
            this.lblMaCSVC.Text = "Mã số:";
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLoai.Location = new System.Drawing.Point(15, 58);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(32, 15);
            this.lblLoai.TabIndex = 4;
            this.lblLoai.Text = "Loại:";
            // 
            // lblViTri
            // 
            this.lblViTri.AutoSize = true;
            this.lblViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblViTri.Location = new System.Drawing.Point(350, 58);
            this.lblViTri.Name = "lblViTri";
            this.lblViTri.Size = new System.Drawing.Size(36, 15);
            this.lblViTri.TabIndex = 6;
            this.lblViTri.Text = "Vị trí:";
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.AutoSize = true;
            this.lblGiaTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblGiaTri.Location = new System.Drawing.Point(15, 88);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(49, 15);
            this.lblGiaTri.TabIndex = 8;
            this.lblGiaTri.Text = "Giá trị:";
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTinhTrang.Location = new System.Drawing.Point(350, 88);
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.Size = new System.Drawing.Size(66, 15);
            this.lblTinhTrang.TabIndex = 10;
            this.lblTinhTrang.Text = "Tình trạng:";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblGhiChu.Location = new System.Drawing.Point(15, 118);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(53, 15);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // groupBoxNhaCungCap
            // 
            this.groupBoxNhaCungCap.Controls.Add(this.lblEmailNCC);
            this.groupBoxNhaCungCap.Controls.Add(this.txtEmailNCC);
            this.groupBoxNhaCungCap.Controls.Add(this.lblSDTNCC);
            this.groupBoxNhaCungCap.Controls.Add(this.txtSDTNCC);
            this.groupBoxNhaCungCap.Controls.Add(this.lblNhaCungCap);
            this.groupBoxNhaCungCap.Controls.Add(this.txtNhaCungCap);
            this.groupBoxNhaCungCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxNhaCungCap.Location = new System.Drawing.Point(20, 300);
            this.groupBoxNhaCungCap.Name = "groupBoxNhaCungCap";
            this.groupBoxNhaCungCap.Size = new System.Drawing.Size(560, 90);
            this.groupBoxNhaCungCap.TabIndex = 2;
            this.groupBoxNhaCungCap.TabStop = false;
            this.groupBoxNhaCungCap.Text = "Thông tin nhà cung cấp";
            // 
            // txtNhaCungCap
            // 
            this.txtNhaCungCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtNhaCungCap.Location = new System.Drawing.Point(120, 25);
            this.txtNhaCungCap.Name = "txtNhaCungCap";
            this.txtNhaCungCap.Size = new System.Drawing.Size(200, 21);
            this.txtNhaCungCap.TabIndex = 1;
            // 
            // txtSDTNCC
            // 
            this.txtSDTNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtSDTNCC.Location = new System.Drawing.Point(420, 25);
            this.txtSDTNCC.Name = "txtSDTNCC";
            this.txtSDTNCC.Size = new System.Drawing.Size(120, 21);
            this.txtSDTNCC.TabIndex = 3;
            // 
            // txtEmailNCC
            // 
            this.txtEmailNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtEmailNCC.Location = new System.Drawing.Point(120, 55);
            this.txtEmailNCC.Name = "txtEmailNCC";
            this.txtEmailNCC.Size = new System.Drawing.Size(200, 21);
            this.txtEmailNCC.TabIndex = 5;
            // 
            // lblNhaCungCap
            // 
            this.lblNhaCungCap.AutoSize = true;
            this.lblNhaCungCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblNhaCungCap.Location = new System.Drawing.Point(15, 28);
            this.lblNhaCungCap.Name = "lblNhaCungCap";
            this.lblNhaCungCap.Size = new System.Drawing.Size(90, 15);
            this.lblNhaCungCap.TabIndex = 0;
            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // lblSDTNCC
            // 
            this.lblSDTNCC.AutoSize = true;
            this.lblSDTNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSDTNCC.Location = new System.Drawing.Point(350, 28);
            this.lblSDTNCC.Name = "lblSDTNCC";
            this.lblSDTNCC.Size = new System.Drawing.Size(65, 15);
            this.lblSDTNCC.TabIndex = 2;
            this.lblSDTNCC.Text = "Điện thoại:";
            // 
            // lblEmailNCC
            // 
            this.lblEmailNCC.AutoSize = true;
            this.lblEmailNCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblEmailNCC.Location = new System.Drawing.Point(15, 58);
            this.lblEmailNCC.Name = "lblEmailNCC";
            this.lblEmailNCC.Size = new System.Drawing.Size(42, 15);
            this.lblEmailNCC.TabIndex = 4;
            this.lblEmailNCC.Text = "Email:";
            // 
            // groupBoxThongTinSuDung
            // 
            this.groupBoxThongTinSuDung.Controls.Add(this.lblGhiChuSuDung);
            this.groupBoxThongTinSuDung.Controls.Add(this.txtGhiChuSuDung);
            this.groupBoxThongTinSuDung.Controls.Add(this.lblThoiGianSuDung);
            this.groupBoxThongTinSuDung.Controls.Add(this.nudThoiGianSuDung);
            this.groupBoxThongTinSuDung.Controls.Add(this.lblNgayHetBaoHanh);
            this.groupBoxThongTinSuDung.Controls.Add(this.dtpNgayHetBaoHanh);
            this.groupBoxThongTinSuDung.Controls.Add(this.lblNgayMua);
            this.groupBoxThongTinSuDung.Controls.Add(this.dtpNgayMua);
            this.groupBoxThongTinSuDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxThongTinSuDung.Location = new System.Drawing.Point(20, 410);
            this.groupBoxThongTinSuDung.Name = "groupBoxThongTinSuDung";
            this.groupBoxThongTinSuDung.Size = new System.Drawing.Size(560, 120);
            this.groupBoxThongTinSuDung.TabIndex = 3;
            this.groupBoxThongTinSuDung.TabStop = false;
            this.groupBoxThongTinSuDung.Text = "Thông tin sử dụng";
            // 
            // dtpNgayMua
            // 
            this.dtpNgayMua.Checked = false;
            this.dtpNgayMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpNgayMua.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayMua.Location = new System.Drawing.Point(120, 25);
            this.dtpNgayMua.Name = "dtpNgayMua";
            this.dtpNgayMua.ShowCheckBox = true;
            this.dtpNgayMua.Size = new System.Drawing.Size(200, 21);
            this.dtpNgayMua.TabIndex = 1;
            // 
            // dtpNgayHetBaoHanh
            // 
            this.dtpNgayHetBaoHanh.Checked = false;
            this.dtpNgayHetBaoHanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpNgayHetBaoHanh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetBaoHanh.Location = new System.Drawing.Point(420, 25);
            this.dtpNgayHetBaoHanh.Name = "dtpNgayHetBaoHanh";
            this.dtpNgayHetBaoHanh.ShowCheckBox = true;
            this.dtpNgayHetBaoHanh.Size = new System.Drawing.Size(120, 21);
            this.dtpNgayHetBaoHanh.TabIndex = 3;
            // 
            // nudThoiGianSuDung
            // 
            this.nudThoiGianSuDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.nudThoiGianSuDung.Location = new System.Drawing.Point(120, 55);
            this.nudThoiGianSuDung.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudThoiGianSuDung.Name = "nudThoiGianSuDung";
            this.nudThoiGianSuDung.Size = new System.Drawing.Size(200, 21);
            this.nudThoiGianSuDung.TabIndex = 5;
            // 
            // txtGhiChuSuDung
            // 
            this.txtGhiChuSuDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtGhiChuSuDung.Location = new System.Drawing.Point(420, 55);
            this.txtGhiChuSuDung.Multiline = true;
            this.txtGhiChuSuDung.Name = "txtGhiChuSuDung";
            this.txtGhiChuSuDung.Size = new System.Drawing.Size(120, 50);
            this.txtGhiChuSuDung.TabIndex = 7;
            // 
            // lblNgayMua
            // 
            this.lblNgayMua.AutoSize = true;
            this.lblNgayMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblNgayMua.Location = new System.Drawing.Point(15, 28);
            this.lblNgayMua.Name = "lblNgayMua";
            this.lblNgayMua.Size = new System.Drawing.Size(67, 15);
            this.lblNgayMua.TabIndex = 0;
            this.lblNgayMua.Text = "Ngày mua:";
            // 
            // lblNgayHetBaoHanh
            // 
            this.lblNgayHetBaoHanh.AutoSize = true;
            this.lblNgayHetBaoHanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblNgayHetBaoHanh.Location = new System.Drawing.Point(350, 28);
            this.lblNgayHetBaoHanh.Name = "lblNgayHetBaoHanh";
            this.lblNgayHetBaoHanh.Size = new System.Drawing.Size(105, 15);
            this.lblNgayHetBaoHanh.TabIndex = 2;
            this.lblNgayHetBaoHanh.Text = "Ngày hết bảo hành:";
            // 
            // lblThoiGianSuDung
            // 
            this.lblThoiGianSuDung.AutoSize = true;
            this.lblThoiGianSuDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblThoiGianSuDung.Location = new System.Drawing.Point(15, 58);
            this.lblThoiGianSuDung.Name = "lblThoiGianSuDung";
            this.lblThoiGianSuDung.Size = new System.Drawing.Size(122, 15);
            this.lblThoiGianSuDung.TabIndex = 4;
            this.lblThoiGianSuDung.Text = "Thời gian sử dụng (tháng):";
            // 
            // lblGhiChuSuDung
            // 
            this.lblGhiChuSuDung.AutoSize = true;
            this.lblGhiChuSuDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblGhiChuSuDung.Location = new System.Drawing.Point(350, 58);
            this.lblGhiChuSuDung.Name = "lblGhiChuSuDung";
            this.lblGhiChuSuDung.Size = new System.Drawing.Size(53, 15);
            this.lblGhiChuSuDung.TabIndex = 6;
            this.lblGhiChuSuDung.Text = "Ghi chú:";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(350, 550);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(470, 550);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "❌ Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // AddCSVCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.groupBoxThongTinSuDung);
            this.Controls.Add(this.groupBoxNhaCungCap);
            this.Controls.Add(this.groupBoxThongTinCoBan);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCSVCForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm CSVC mới";
            this.Load += new System.EventHandler(this.AddCSVCForm_Load);
            this.groupBoxThongTinCoBan.ResumeLayout(false);
            this.groupBoxThongTinCoBan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiaTri)).EndInit();
            this.groupBoxNhaCungCap.ResumeLayout(false);
            this.groupBoxNhaCungCap.PerformLayout();
            this.groupBoxThongTinSuDung.ResumeLayout(false);
            this.groupBoxThongTinSuDung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiGianSuDung)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBoxThongTinCoBan;
        private System.Windows.Forms.TextBox txtTenCSVC;
        private System.Windows.Forms.TextBox txtMaCSVC;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.ComboBox cboViTri;
        private System.Windows.Forms.NumericUpDown nudGiaTri;
        private System.Windows.Forms.ComboBox cboTinhTrang;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblTenCSVC;
        private System.Windows.Forms.Label lblMaCSVC;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.Label lblViTri;
        private System.Windows.Forms.Label lblGiaTri;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.GroupBox groupBoxNhaCungCap;
        private System.Windows.Forms.TextBox txtNhaCungCap;
        private System.Windows.Forms.TextBox txtSDTNCC;
        private System.Windows.Forms.TextBox txtEmailNCC;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.Label lblSDTNCC;
        private System.Windows.Forms.Label lblEmailNCC;
        private System.Windows.Forms.GroupBox groupBoxThongTinSuDung;
        private System.Windows.Forms.DateTimePicker dtpNgayMua;
        private System.Windows.Forms.DateTimePicker dtpNgayHetBaoHanh;
        private System.Windows.Forms.NumericUpDown nudThoiGianSuDung;
        private System.Windows.Forms.TextBox txtGhiChuSuDung;
        private System.Windows.Forms.Label lblNgayMua;
        private System.Windows.Forms.Label lblNgayHetBaoHanh;
        private System.Windows.Forms.Label lblThoiGianSuDung;
        private System.Windows.Forms.Label lblGhiChuSuDung;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}