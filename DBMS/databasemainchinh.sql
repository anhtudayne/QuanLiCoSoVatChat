USE vc;
GO

-- 1. BẢNG LOẠI CSVC
CREATE TABLE LoaiCSVC (
    LoaiID INT IDENTITY PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(200)
);
GO

-- 2. BẢNG VỊ TRÍ
CREATE TABLE ViTri (
    ViTriID INT IDENTITY PRIMARY KEY,
    Tang INT NOT NULL,
    KhuVuc NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(200)
);
GO

-- 3. BẢNG NHÂN VIÊN (đã gộp chức vụ)
CREATE TABLE NhanVien (
    NhanVienID INT IDENTITY PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    SoDienThoai VARCHAR(20),
    Email NVARCHAR(100) UNIQUE,
    TrangThai NVARCHAR(20) DEFAULT N'Đang làm',
    ChucVu NVARCHAR(50) NOT NULL -- gộp từ bảng ChucVu
);
GO

-- 4. BẢNG THÔNG TIN LƯƠNG NHÂN VIÊN
CREATE TABLE ThongTinLuongNhanVien (
    LuongID INT IDENTITY PRIMARY KEY,
    NhanVienID INT,
    Thang INT NOT NULL,
    Nam INT NOT NULL,
    LuongCoBanTheoGio DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Luong_NhanVien FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE CASCADE,
    CONSTRAINT CK_Luong_LuongCoBan CHECK (LuongCoBanTheoGio >= 0),
    CONSTRAINT UQ_LuongThang UNIQUE (NhanVienID, Thang, Nam)
);
GO

-- 5. BẢNG CƠ SỞ VẬT CHẤT (đã gộp NhaCungCap và Tình Trạng)
CREATE TABLE CSVC (
    CSVCID INT IDENTITY PRIMARY KEY,
    TenCSVC NVARCHAR(100) NOT NULL,
    MaCSVC VARCHAR(50) UNIQUE,
    LoaiID INT,
    ViTriID INT,
    GiaTri DECIMAL(18,2),
    -- Gộp thông tin nhà cung cấp
    TenNhaCungCap NVARCHAR(100),
    SoDienThoaiNCC VARCHAR(20),
    EmailNCC NVARCHAR(100),
    -- Gộp tình trạng
    TinhTrang NVARCHAR(50) NOT NULL DEFAULT N'Đang sử dụng',
    GhiChu NVARCHAR(500),
    CONSTRAINT FK_CSVC_Loai FOREIGN KEY (LoaiID) REFERENCES LoaiCSVC(LoaiID) ON DELETE SET NULL,
    CONSTRAINT FK_CSVC_ViTri FOREIGN KEY (ViTriID) REFERENCES ViTri(ViTriID) ON DELETE SET NULL,
    CONSTRAINT CK_CSVC_GiaTri CHECK (GiaTri >= 0)
);
GO

-- 6. BẢNG PHÂN CÔNG CA (đã gộp CaTruc)
CREATE TABLE PhanCongCa (
    PhanCongID INT IDENTITY PRIMARY KEY,
    NhanVienID INT,
    TenCa NVARCHAR(50) NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    NgayLamViec DATE NOT NULL,
    VaiTroTrongCa NVARCHAR(20) NOT NULL DEFAULT N'Nhân viên trực',
    GhiChu NVARCHAR(200),
    CONSTRAINT FK_PhanCong_NhanVien FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE CASCADE
    
);
GO

-- 7. BẢNG THÔNG TIN SỬ DỤNG VÀ BẢO HÀNH (chuyển NgayMua vào đây)
CREATE TABLE ThongTinSuDung (
    CSVCID INT PRIMARY KEY,
    NgayMua DATE NOT NULL,
    NgayHetBaoHanh DATE,
    ThoiGianSuDungDuKien_Thang INT,
    GhiChu NVARCHAR(200),
    CONSTRAINT FK_ThongTinSuDung_CSVC FOREIGN KEY (CSVCID) REFERENCES CSVC(CSVCID) ON DELETE CASCADE
);
GO

-- 8. BẢNG LỊCH SỬ BẢO TRÌ
CREATE TABLE LichSuBaoTri (
    BaoTriID INT IDENTITY PRIMARY KEY,
    CSVCID INT,
    NgayYeuCau DATE NOT NULL DEFAULT GETDATE(),
    NgayHoanThanh DATE,
    NoiDung NVARCHAR(500),
    ChiPhi DECIMAL(18,2),
    NhanVienGiamSatID INT,
    NhanVienKyThuatID INT,
    TrangThai NVARCHAR(50) DEFAULT N'Chờ xử lý',
    CONSTRAINT FK_LichSuBaoTri_CSVC FOREIGN KEY (CSVCID) REFERENCES CSVC(CSVCID) ON DELETE CASCADE,
    CONSTRAINT FK_LichSuBaoTri_NhanVien FOREIGN KEY (NhanVienGiamSatID) REFERENCES NhanVien(NhanVienID) ON DELETE SET NULL,
    CONSTRAINT FK_LichSuBaoTri_NhanVienKyThuat FOREIGN KEY (NhanVienKyThuatID) REFERENCES NhanVien(NhanVienID) ON DELETE NO ACTION,
    CONSTRAINT CK_BaoTri_ChiPhi CHECK (ChiPhi >= 0)
);
GO

-- 9. BẢNG LỊCH BẢO TRÌ ĐỊNH KỲ
CREATE TABLE LichBaoTri (
    LichID INT IDENTITY PRIMARY KEY,
    CSVCID INT,
    ChuKyBaoTri_Thang INT NOT NULL DEFAULT 6,
    NgayBatDau DATE NOT NULL,
    NgayKeTiep AS (DATEADD(month, ChuKyBaoTri_Thang, NgayBatDau)),
    GhiChu NVARCHAR(200),
    CONSTRAINT FK_LichBaoTri_CSVC FOREIGN KEY (CSVCID) REFERENCES CSVC(CSVCID) ON DELETE CASCADE
);
GO

-- 10. BẢNG THANH LÝ CSVC
CREATE TABLE ThanhLyCSVC (
    ThanhLyID INT IDENTITY PRIMARY KEY,
    CSVCID INT UNIQUE,
    NgayThanhLy DATE NOT NULL DEFAULT GETDATE(),
    LyDoThanhLy NVARCHAR(500),
    GiaTriThanhLy DECIMAL(18,2),
    NguoiThucHienID INT,
    CONSTRAINT FK_ThanhLy_CSVC FOREIGN KEY (CSVCID) REFERENCES CSVC(CSVCID) ON DELETE CASCADE,
    CONSTRAINT FK_ThanhLy_NhanVien FOREIGN KEY (NguoiThucHienID) REFERENCES NhanVien(NhanVienID) ON DELETE SET NULL,
    CONSTRAINT CK_ThanhLy_GiaTri CHECK (GiaTriThanhLy >= 0)
);
GO

-- 11. BẢNG LỊCH SỬ SỰ KIỆN
CREATE TABLE LichSuSuKien (
    SuKienID INT IDENTITY PRIMARY KEY,
    Ngay DATETIME NOT NULL DEFAULT GETDATE(),
    NoiDung NVARCHAR(500) NOT NULL
);
GO

-- 12. BẢNG TÀI KHOẢN
CREATE TABLE TaiKhoan (
    TaiKhoanID INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    NhanVienID INT NULL,
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE SET NULL,
    CONSTRAINT CK_TaiKhoan_Role CHECK (Role IN (N'Quản Lý', N'Nhân Viên Kỹ Thuật', N'Nhân Viên Trực'))
);
GO

-- Dữ liệu mẫu cho bảng TaiKhoan
INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID) VALUES 
(N'admin', N'123456', N'Quản Lý', NULL),  -- Admin
(N'kythuat01', N'123456', N'Nhân Viên Kỹ Thuật', 1), -- Nhân viên kỹ thuật (NhanVienID = 1)
(N'nhanvien01', N'123456', N'Nhân Viên Trực', 2); -- Nhân viên trực (NhanVienID = 2)
GO

-- =============================================
-- STORED PROCEDURE QUẢN LÝ TÀI KHOẢN
-- =============================================

-- PROCEDURE THÊM TÀI KHOẢN MỚI
CREATE OR ALTER PROCEDURE sp_AddTaiKhoan
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @Role NVARCHAR(50),
    @NhanVienID INT = NULL,
    @Result INT OUTPUT,
    @Message NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra username đã tồn tại chưa
        IF EXISTS (SELECT 1 FROM TaiKhoan WHERE Username = @Username)
        BEGIN
            SET @Result = -1;
            SET @Message = N'Tên đăng nhập đã tồn tại!';
            RETURN;
        END
        
        -- Kiểm tra role hợp lệ
        IF @Role NOT IN (N'Quản Lý', N'Nhân Viên Kỹ Thuật', N'Nhân Viên Trực')
        BEGIN
            SET @Result = -2;
            SET @Message = N'Vai trò không hợp lệ! Chỉ chấp nhận: Quản Lý, Nhân Viên Kỹ Thuật, Nhân Viên Trực';
            RETURN;
        END
        
        -- Kiểm tra NhanVienID nếu không phải Quản Lý
        IF @Role != N'Quản Lý' AND @NhanVienID IS NULL
        BEGIN
            SET @Result = -3;
            SET @Message = N'Nhân viên phải có NhanVienID!';
            RETURN;
        END
        
        -- Kiểm tra NhanVienID có tồn tại không (nếu được cung cấp)
        IF @NhanVienID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM NhanVien WHERE NhanVienID = @NhanVienID)
        BEGIN
            SET @Result = -4;
            SET @Message = N'NhanVienID không tồn tại!';
            RETURN;
        END
        
        -- Thêm tài khoản mới
        INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID)
        VALUES (@Username, @Password, @Role, @NhanVienID);
        
        SET @Result = 1;
        SET @Message = N'Thêm tài khoản thành công!';
        
    END TRY
    BEGIN CATCH
        SET @Result = -999;
        SET @Message = N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO

-- PROCEDURE RESET MẬT KHẨU
CREATE OR ALTER PROCEDURE sp_ResetPassword
    @Username NVARCHAR(50),
    @NewPassword NVARCHAR(100),
    @Result INT OUTPUT,
    @Message NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra username có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE Username = @Username)
        BEGIN
            SET @Result = -1;
            SET @Message = N'Tên đăng nhập không tồn tại!';
            RETURN;
        END
        
        -- Kiểm tra mật khẩu mới không được rỗng
        IF @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = ''
        BEGIN
            SET @Result = -2;
            SET @Message = N'Mật khẩu mới không được để trống!';
            RETURN;
        END
        
        -- Cập nhật mật khẩu mới
        UPDATE TaiKhoan 
        SET Password = @NewPassword 
        WHERE Username = @Username;
        
        SET @Result = 1;
        SET @Message = N'Reset mật khẩu thành công!';
        
    END TRY
    BEGIN CATCH
        SET @Result = -999;
        SET @Message = N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO
