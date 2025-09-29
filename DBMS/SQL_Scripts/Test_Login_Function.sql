-- =============================================
-- TEST LOGIN FUNCTION VÀ ROLE-BASED ACCESS
-- =============================================

USE vc;
GO

-- Kiểm tra dữ liệu hiện tại trong bảng TaiKhoan
SELECT * FROM TaiKhoan;
GO

-- Test function với các tài khoản có sẵn
PRINT '=== TEST FUNCTION fn_LoginAndGetRole ===';

-- Test với admin
SELECT 'admin' AS Username, '123456' AS Password, dbo.fn_LoginAndGetRole('admin', '123456') AS Role;

-- Test với kythuat01
SELECT 'kythuat01' AS Username, '123456' AS Password, dbo.fn_LoginAndGetRole('kythuat01', '123456') AS Role;

-- Test với nhanvien01
SELECT 'nhanvien01' AS Username, '123456' AS Password, dbo.fn_LoginAndGetRole('nhanvien01', '123456') AS Role;

-- Test với thông tin sai
SELECT 'wronguser' AS Username, 'wrongpass' AS Password, dbo.fn_LoginAndGetRole('wronguser', 'wrongpass') AS Role;

-- Kiểm tra các SQL Server Logins đã được tạo
PRINT '=== KIỂM TRA SQL SERVER LOGINS ===';
SELECT 
    name AS LoginName, 
    create_date, 
    default_database_name
FROM sys.server_principals 
WHERE type = 'S' AND name IN ('admin', 'kythuat01', 'nhanvien01')
ORDER BY name;

-- Kiểm tra Database Users và Roles
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

-- Thêm tài khoản test mới để kiểm tra trigger
PRINT '=== TEST TRIGGER VỚI TÀI KHOẢN MỚI ===';
INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID) 
VALUES ('testuser', 'testpass123', N'Nhân Viên Trực', 1);

-- Kiểm tra function với tài khoản vừa tạo
SELECT 'testuser' AS Username, 'testpass123' AS Password, dbo.fn_LoginAndGetRole('testuser', 'testpass123') AS Role;

-- Kiểm tra login và user mới được tạo
SELECT 
    name AS LoginName, 
    create_date 
FROM sys.server_principals 
WHERE type = 'S' AND name = 'testuser';

SELECT 
    u.name AS UserName,
    r.name AS RoleName
FROM sys.database_role_members rm
INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
INNER JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
WHERE u.name = 'testuser';

-- Cleanup - xóa tài khoản test
-- DELETE FROM TaiKhoan WHERE Username = 'testuser';

PRINT '=== TEST HOÀN TẤT ===';
GO