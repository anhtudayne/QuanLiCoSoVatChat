-- =============================================
-- TEST ROLE-BASED SECURITY VỚI CONNECTION STRINGS KHÁC NHAU
-- =============================================

USE vc;
GO

PRINT '=== KIỂM TRA QUYỀN CỦA TỪNG ROLE ===';

-- Kiểm tra quyền của admin (QuanLyRole)
PRINT 'Quyền của admin (QuanLyRole):';
SELECT 
    dp.class_desc,
    dp.permission_name,
    dp.state_desc,
    s.name AS object_name
FROM sys.database_permissions dp
LEFT JOIN sys.objects s ON dp.major_id = s.object_id
INNER JOIN sys.database_principals pr ON dp.grantee_principal_id = pr.principal_id
WHERE pr.name = 'QuanLyRole'
ORDER BY dp.class_desc, s.name;

-- Kiểm tra quyền của kythuat01 (KyThuatRole)  
PRINT 'Quyền của kythuat01 (KyThuatRole):';
SELECT 
    dp.class_desc,
    dp.permission_name,
    dp.state_desc,
    s.name AS object_name
FROM sys.database_permissions dp
LEFT JOIN sys.objects s ON dp.major_id = s.object_id
INNER JOIN sys.database_principals pr ON dp.grantee_principal_id = pr.principal_id
WHERE pr.name = 'KyThuatRole'
ORDER BY dp.class_desc, s.name;

-- Kiểm tra quyền của nhanvien01 (NhanVienTrucRole)
PRINT 'Quyền của nhanvien01 (NhanVienTrucRole):';
SELECT 
    dp.class_desc,
    dp.permission_name,
    dp.state_desc,
    s.name AS object_name
FROM sys.database_permissions dp
LEFT JOIN sys.objects s ON dp.major_id = s.object_id
INNER JOIN sys.database_principals pr ON dp.grantee_principal_id = pr.principal_id
WHERE pr.name = 'NhanVienTrucRole'
ORDER BY dp.class_desc, s.name;

PRINT '=== TEST QUYỀN THỰC TẾ ===';

-- Test với admin (có thể làm tất cả)
EXECUTE AS USER = 'admin';
SELECT 'ADMIN - Có thể select NhanVien:' AS Test;
SELECT COUNT(*) FROM NhanVien;
REVERT;

-- Test với kythuat01 (chỉ có thể làm công việc kỹ thuật)
EXECUTE AS USER = 'kythuat01';
SELECT 'KYTHUAT01 - Có thể select CSVC:' AS Test;
SELECT COUNT(*) FROM CSVC;
REVERT;

-- Test với nhanvien01 (quyền hạn chế)
EXECUTE AS USER = 'nhanvien01';
SELECT 'NHANVIEN01 - Có thể select CSVC:' AS Test;
SELECT COUNT(*) FROM CSVC;
REVERT;

PRINT '=== CONNECTION STRING EXAMPLES ===';
PRINT 'Admin connection string:';
PRINT 'Data Source=.;Initial Catalog=vc;User ID=admin;Password=123456;';
PRINT '';
PRINT 'KyThuat connection string:';
PRINT 'Data Source=.;Initial Catalog=vc;User ID=kythuat01;Password=123456;';
PRINT '';
PRINT 'NhanVien connection string:';  
PRINT 'Data Source=.;Initial Catalog=vc;User ID=nhanvien01;Password=123456;';

PRINT '=== CÁCH TEST TRONG C# ===';
PRINT '1. Đăng nhập với admin/123456 → MainForm với full quyền';
PRINT '2. Đăng nhập với kythuat01/123456 → TechnicalMainForm với quyền kỹ thuật';
PRINT '3. Đăng nhập với nhanvien01/123456 → StaffMainForm với quyền hạn chế';
PRINT '';
PRINT 'Mỗi form sẽ sử dụng connection string khác nhau và có quyền khác nhau!';
GO