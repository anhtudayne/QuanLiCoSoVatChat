USE vc;
GO

-- =============================================
-- SCRIPT RESET VÀ TẠO DỮ LIỆU MẪU CHO BẢNG TAIKHOAN
-- =============================================

PRINT 'Bắt đầu reset bảng TaiKhoan và tạo dữ liệu mẫu...';
GO

-- BƯỚC 1: XÓA CÁC SQL SERVER LOGINS VÀ USERS CŨ (NẾU CÓ)
-- Xóa các logins test trước đó
IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'admin' AND type = 'S')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'admin' AND type = 'S')
        DROP USER [admin];
    DROP LOGIN [admin];
    PRINT 'Đã xóa login admin cũ';
END

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'kythuat01' AND type = 'S')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'kythuat01' AND type = 'S')
        DROP USER [kythuat01];
    DROP LOGIN [kythuat01];
    PRINT 'Đã xóa login kythuat01 cũ';
END

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'nhanvien01' AND type = 'S')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'nhanvien01' AND type = 'S')
        DROP USER [nhanvien01];
    DROP LOGIN [nhanvien01];
    PRINT 'Đã xóa login nhanvien01 cũ';
END

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'manager01' AND type = 'S')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'manager01' AND type = 'S')
        DROP USER [manager01];
    DROP LOGIN [manager01];
    PRINT 'Đã xóa login manager01 cũ';
END

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'kythuat02' AND type = 'S')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'kythuat02' AND type = 'S')
        DROP USER [kythuat02];
    DROP LOGIN [kythuat02];
    PRINT 'Đã xóa login kythuat02 cũ';
END

IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'staff01' AND type = 'S')
BEGIN
    IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'staff01' AND type = 'S')
        DROP USER [staff01];
    DROP LOGIN [staff01];
    PRINT 'Đã xóa login staff01 cũ';
END

-- BƯỚC 2: XÓA DỮ LIỆU CŨ TRONG BẢNG TAIKHOAN
DELETE FROM TaiKhoan;
PRINT 'Đã xóa tất cả dữ liệu trong bảng TaiKhoan';

-- Reset IDENTITY nếu có
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('TaiKhoan') AND name = 'TaiKhoanID' AND is_identity = 1)
BEGIN
    DBCC CHECKIDENT('TaiKhoan', RESEED, 0);
    PRINT 'Đã reset IDENTITY cho bảng TaiKhoan';
END

-- BƯỚC 3: INSERT DỮ LIỆU MẪU MỚI
-- Triggers sẽ tự động tạo SQL Server Logins và Users

-- Kiểm tra dữ liệu NhanVien trước khi insert
PRINT '=== KIỂM TRA DỮ LIỆU NHÂN VIÊN ===';
SELECT NhanVienID, HoTen, ChucVu FROM NhanVien WHERE NhanVienID IN (1, 2);

-- Insert 3 tài khoản cơ bản cho 3 vai trò
PRINT 'Đang insert tài khoản admin...';
INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID) VALUES 
('admin', '123456', N'Quản Lý', NULL);
PRINT 'Đã insert admin';

PRINT 'Đang insert tài khoản kythuat01...';
INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID) VALUES 
('kythuat01', '123456', N'Nhân Viên Kỹ Thuật', 2);
PRINT 'Đã insert kythuat01';

PRINT 'Đang insert tài khoản nhanvien01...';
INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID) VALUES 
('nhanvien01', '123456', N'Nhân Viên Trực', 1);
PRINT 'Đã insert nhanvien01';

PRINT 'Đã insert thành công 3 tài khoản cơ bản';
GO

-- BƯỚC 4: KIỂM TRA KẾT QUẢ
PRINT '=== KIỂM TRA DỮ LIỆU BẢNG TAIKHOAN ===';
SELECT 
    TaiKhoanID,
    Username,
    Role,
    NhanVienID,
    CASE 
        WHEN NhanVienID IS NOT NULL THEN (SELECT HoTen FROM NhanVien WHERE NhanVienID = TaiKhoan.NhanVienID)
        ELSE N'Không liên kết'
    END AS TenNhanVien
FROM TaiKhoan
ORDER BY TaiKhoanID;

-- BƯỚC 5: KIỂM TRA SQL SERVER LOGINS ĐÃ ĐƯỢC TẠO
PRINT '=== KIỂM TRA SQL SERVER LOGINS ===';
SELECT 
    name AS LoginName,
    create_date,
    default_database_name
FROM sys.server_principals 
WHERE type = 'S' 
    AND name IN ('admin', 'kythuat01', 'nhanvien01')
ORDER BY name;

-- BƯỚC 6: KIỂM TRA DATABASE USERS VÀ ROLES
PRINT '=== KIỂM TRA DATABASE USERS VÀ ROLES ===';
SELECT 
    u.name AS UserName,
    r.name AS RoleName,
    u.create_date
FROM sys.database_role_members rm
INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
INNER JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
WHERE u.name IN ('admin', 'kythuat01', 'nhanvien01')
ORDER BY u.name, r.name;

-- BƯỚC 7: TEST FUNCTION LOGIN
PRINT '=== TEST FUNCTION fn_LoginAndGetRole ===';
SELECT 'admin' AS Username, '123456' AS Password, dbo.fn_LoginAndGetRole('admin', '123456') AS Role
UNION ALL
SELECT 'kythuat01', '123456', dbo.fn_LoginAndGetRole('kythuat01', '123456')
UNION ALL
SELECT 'nhanvien01', '123456', dbo.fn_LoginAndGetRole('nhanvien01', '123456')
UNION ALL
SELECT 'wronguser', 'wrongpass', dbo.fn_LoginAndGetRole('wronguser', 'wrongpass')
ORDER BY Username;

PRINT '=== RESET VÀ TẠO DỮ LIỆU MẪU HOÀN TẤT ===';
PRINT 'Bây giờ bạn có thể test đăng nhập với các tài khoản sau:';
PRINT '1. admin / 123456 (Quản Lý → MainForm)';
PRINT '2. kythuat01 / 123456 (Nhân Viên Kỹ Thuật → TechnicalMainForm)';
PRINT '3. nhanvien01 / 123456 (Nhân Viên Trực → StaffMainForm)';
GO