USE vc;
GO
-- Grant permissions for roles
GRANT EXECUTE ON fn_GetNhanVienIDByUsername TO QuanLyRole;
GRANT EXECUTE ON fn_GetNhanVienIDByUsername TO KyThuatRole;
GRANT EXECUTE ON fn_GetNhanVienIDByUsername TO NhanVienTrucRole;

GRANT SELECT ON fn_GetNhanVienInfo TO QuanLyRole;
GRANT SELECT ON fn_GetNhanVienInfo TO KyThuatRole;
GRANT SELECT ON fn_GetNhanVienInfo TO NhanVienTrucRole;

-- Tạo 3 role chính
CREATE ROLE QuanLyRole;
CREATE ROLE KyThuatRole;
CREATE ROLE NhanVienTrucRole;
GO

-- Cấp Control (toàn quyền) cho Admin trên tất cả tables
GRANT CONTROL ON LoaiCSVC TO QuanLyRole;
GRANT CONTROL ON ViTri TO QuanLyRole;
GRANT CONTROL ON NhanVien TO QuanLyRole;
GRANT CONTROL ON ThongTinLuongNhanVien TO QuanLyRole;
GRANT CONTROL ON CSVC TO QuanLyRole;
GRANT CONTROL ON PhanCongCa TO QuanLyRole;
GRANT CONTROL ON ThongTinSuDung TO QuanLyRole;
GRANT CONTROL ON LichSuBaoTri TO QuanLyRole;
GRANT CONTROL ON LichBaoTri TO QuanLyRole;
GRANT CONTROL ON ThanhLyCSVC TO QuanLyRole;
GRANT CONTROL ON LichSuSuKien TO QuanLyRole;
GRANT CONTROL ON TaiKhoan TO QuanLyRole;

-- Cấp Execute Stored Procedures được sử dụng trong C# cho Admin
GRANT EXECUTE ON sp_AddTaiKhoan TO QuanLyRole;
GRANT EXECUTE ON sp_ResetPassword TO QuanLyRole;
GRANT EXECUTE ON sp_GetAllEmployees TO QuanLyRole;
GRANT EXECUTE ON sp_GetEmployeeById TO QuanLyRole;
GRANT EXECUTE ON sp_InsertEmployee TO QuanLyRole;
GRANT EXECUTE ON sp_UpdateEmployee TO QuanLyRole;
GRANT EXECUTE ON sp_DeleteEmployee TO QuanLyRole;
GRANT EXECUTE ON sp_SearchEmployees TO QuanLyRole;
GRANT EXECUTE ON sp_GetPositions TO QuanLyRole;
GRANT EXECUTE ON sp_GetEmployeeStatuses TO QuanLyRole;
GRANT EXECUTE ON sp_AddCSVC TO QuanLyRole;
GRANT EXECUTE ON sp_UpdateCSVC TO QuanLyRole;
GRANT EXECUTE ON sp_DeleteCSVC TO QuanLyRole;
GRANT EXECUTE ON sp_SearchCSVC TO QuanLyRole;
GRANT EXECUTE ON sp_GetAvailableCSVCForMaintenance TO QuanLyRole;
GRANT EXECUTE ON sp_AddMaintenanceRequest TO QuanLyRole;
GRANT EXECUTE ON sp_UpdateMaintenanceStatus TO QuanLyRole;
GRANT EXECUTE ON sp_GetCSVCInMaintenanceHistory TO QuanLyRole;
GRANT EXECUTE ON sp_SearchMaintenanceHistory TO QuanLyRole;
GRANT EXECUTE ON sp_CreateMaintenanceFromSchedule TO QuanLyRole;
GRANT EXECUTE ON sp_DeleteMaintenanceSchedule TO QuanLyRole;
GRANT EXECUTE ON sp_AddAssetDisposal TO QuanLyRole;
GRANT EXECUTE ON sp_UpdateAssetDisposal TO QuanLyRole;
GRANT EXECUTE ON sp_SearchAssetDisposal TO QuanLyRole;
GRANT EXECUTE ON sp_GetWorkShiftDetails TO QuanLyRole;
GRANT EXECUTE ON sp_GetWorkShiftById TO QuanLyRole;
GRANT EXECUTE ON sp_InsertWorkShift TO QuanLyRole;
GRANT EXECUTE ON sp_UpdateWorkShift TO QuanLyRole;
GRANT EXECUTE ON sp_DeleteWorkShift TO QuanLyRole;

-- Cấp Execute Functions được sử dụng trong C# cho Admin
GRANT SELECT ON fn_GetCSVCStatuses TO QuanLyRole;
GRANT EXECUTE ON fn_GetTotalDisposalValue TO QuanLyRole;
GRANT EXECUTE ON fn_GetDisposalCount TO QuanLyRole;
GRANT SELECT ON fn_GetEmployeeStatistics TO QuanLyRole;
GRANT EXECUTE ON fn_CountEmployeesByPosition TO QuanLyRole;
GRANT SELECT ON fn_GetBirthdayEmployees TO QuanLyRole;
GRANT EXECUTE ON fn_GetTotalMaintenanceCost TO QuanLyRole;
GRANT EXECUTE ON fn_GetMaintenanceCount TO QuanLyRole;
GRANT EXECUTE ON fn_GetCSVCCountByType TO QuanLyRole;
GRANT EXECUTE ON fn_GetTotalAssetValue TO QuanLyRole;
GRANT EXECUTE ON fn_GetCSVCCountByStatus TO QuanLyRole;
GRANT EXECUTE ON fn_GetCSVCCountByLocation TO QuanLyRole;
GRANT SELECT ON fn_GetAllEmployeeSalariesInMonth TO QuanLyRole;
GRANT SELECT ON fn_GetEmployeeSalaryDetails TO QuanLyRole;

-- Cấp Select Views được sử dụng trong C# cho Admin
GRANT SELECT ON vw_BaoTri TO QuanLyRole;
GRANT SELECT ON vw_LichBaoTriDinhKy TO QuanLyRole;
GRANT SELECT ON vw_ThanhLy TO QuanLyRole;
GRANT SELECT ON vw_CSVCForDisposal TO QuanLyRole;
GRANT SELECT ON v_LichSuSuKien TO QuanLyRole;
GRANT SELECT ON vw_CSVCOverview TO QuanLyRole;
GO


--PHÂN QUYỀN CHO NHÂN VIÊN KỸ THUẬT


-- Quyền trên Tables (Chủ yếu về bảo trì và CSVC)
GRANT SELECT ON LoaiCSVC TO KyThuatRole;
GRANT SELECT ON ViTri TO KyThuatRole;
GRANT SELECT ON NhanVien TO KyThuatRole;
GRANT SELECT ON CSVC TO KyThuatRole;
GRANT SELECT, INSERT, UPDATE ON LichSuBaoTri TO KyThuatRole;
GRANT SELECT ON LichBaoTri TO KyThuatRole;
GRANT SELECT ON ThongTinSuDung TO KyThuatRole;
GRANT SELECT ON PhanCongCa TO KyThuatRole;
GRANT SELECT ON LichSuSuKien TO KyThuatRole;

-- Stored Procedures liên quan đến bảo trì được sử dụng trong C#
GRANT EXECUTE ON sp_GetAvailableCSVCForMaintenance TO KyThuatRole;
GRANT EXECUTE ON sp_AddMaintenanceRequest TO KyThuatRole;
GRANT EXECUTE ON sp_UpdateMaintenanceStatus TO KyThuatRole;
GRANT EXECUTE ON sp_GetCSVCInMaintenanceHistory TO KyThuatRole;
GRANT EXECUTE ON sp_SearchMaintenanceHistory TO KyThuatRole;
GRANT EXECUTE ON sp_CreateMaintenanceFromSchedule TO KyThuatRole;
GRANT EXECUTE ON sp_SearchCSVC TO KyThuatRole;
GRANT EXECUTE ON sp_GetAllEmployees TO KyThuatRole;

-- Functions liên quan đến bảo trì và CSVC được sử dụng trong C#
GRANT EXECUTE ON fn_GetCSVCCountByType TO KyThuatRole;
GRANT EXECUTE ON fn_GetCSVCCountByStatus TO KyThuatRole;
GRANT EXECUTE ON fn_GetTotalAssetValue TO KyThuatRole;
GRANT EXECUTE ON fn_GetTotalMaintenanceCost TO KyThuatRole;
GRANT EXECUTE ON fn_GetMaintenanceCount TO KyThuatRole;

-- Views liên quan đến bảo trì và CSVC được sử dụng trong C#
GRANT SELECT ON vw_CSVCOverview TO KyThuatRole;
GRANT SELECT ON vw_BaoTri TO KyThuatRole;
GRANT SELECT ON vw_LichBaoTriDinhKy TO KyThuatRole;
GRANT SELECT ON v_LichSuSuKien TO KyThuatRole;
GO


-- PHÂN QUYỀN CHO NHÂN VIÊN TRỰC


-- Quyền trên Tables (Chỉ xem thông tin cơ bản)
GRANT SELECT ON LoaiCSVC TO NhanVienTrucRole;
GRANT SELECT ON ViTri TO NhanVienTrucRole;
GRANT SELECT ON NhanVien TO NhanVienTrucRole;
GRANT SELECT, INSERT ON CSVC TO NhanVienTrucRole;
GRANT SELECT, INSERT ON ThongTinSuDung TO NhanVienTrucRole;
GRANT SELECT ON LichSuBaoTri TO NhanVienTrucRole;
GRANT SELECT ON PhanCongCa TO NhanVienTrucRole;
GRANT SELECT ON ThongTinLuongNhanVien TO NhanVienTrucRole;

-- Quyền tạo yêu cầu bảo trì và thêm CSVC mới
GRANT INSERT ON LichSuBaoTri TO NhanVienTrucRole;
-- Stored Procedures cơ bản được sử dụng trong C#
GRANT EXECUTE ON sp_SearchCSVC TO NhanVienTrucRole;
GRANT EXECUTE ON sp_AddCSVC TO NhanVienTrucRole;
GRANT EXECUTE ON sp_AddMaintenanceRequest TO NhanVienTrucRole;
GRANT EXECUTE ON sp_GetAllEmployees TO NhanVienTrucRole;
GRANT EXECUTE ON sp_GetWorkShiftDetails TO NhanVienTrucRole;
GRANT EXECUTE ON sp_GetWorkShiftById TO NhanVienTrucRole;

-- Functions cơ bản được sử dụng trong C#
GRANT EXECUTE ON fn_GetCSVCCountByType TO NhanVienTrucRole;
GRANT SELECT ON fn_GetCSVCStatuses TO NhanVienTrucRole;
GRANT SELECT ON fn_GetAllEmployeeSalariesInMonth TO NhanVienTrucRole;
GRANT SELECT ON fn_GetEmployeeSalaryDetails TO NhanVienTrucRole;

-- Views cơ bản được sử dụng trong C#
GRANT SELECT ON vw_CSVCOverview TO NhanVienTrucRole;
GO

-- =============================================
-- BƯỚC 5: TẠO SQL SERVER LOGINS VÀ USERS (OPTIONAL)
-- =============================================

-- Tạo SQL Server Logins (Chỉ tham khảo - có thể bỏ qua nếu dùng Integrated Security)
/*
CREATE LOGIN [admin_user] WITH PASSWORD = 'Admin@123456';
CREATE LOGIN [kythuat_user] WITH PASSWORD = 'Tech@123456';
CREATE LOGIN [staff_user] WITH PASSWORD = 'Staff@123456';

-- Tạo Database Users
CREATE USER [admin_user] FOR LOGIN [admin_user];
CREATE USER [kythuat_user] FOR LOGIN [kythuat_user];
CREATE USER [staff_user] FOR LOGIN [staff_user];

-- Gán Users vào Roles
ALTER ROLE QuanLyRole ADD MEMBER [admin_user];
ALTER ROLE KyThuatRole ADD MEMBER [kythuat_user];
ALTER ROLE NhanVienTrucRole ADD MEMBER [staff_user];
*/

-- =============================================
-- BƯỚC 6: KIỂM TRA PHÂN QUYỀN
-- =============================================

-- Kiểm tra quyền của từng role
SELECT 
    r.name AS RoleName,
    p.permission_name,
    p.state_desc,
    s.name AS SecurableName,
    s.type_desc AS SecurableType
FROM sys.database_role_members rm
    INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
    INNER JOIN sys.database_permissions p ON r.principal_id = p.grantee_principal_id
    INNER JOIN sys.objects s ON p.major_id = s.object_id
WHERE r.name IN ('QuanLyRole', 'KyThuatRole', 'NhanVienTrucRole')
ORDER BY r.name, s.name, p.permission_name;

-- =============================================
-- HƯỚNG DẪN SỬ DỤNG
-- =============================================

/*
1. CÁCH GÁN USER VÀO ROLE:
   ALTER ROLE QuanLyRole ADD MEMBER [tên_user];

2. CÁCH XÓA USER KHỎI ROLE:
   ALTER ROLE QuanLyRole DROP MEMBER [tên_user];

3. CÁCH KIỂM TRA QUYỀN CỦA USER:
   SELECT * FROM fn_my_permissions(NULL, 'DATABASE');

4. TRONG C# APPLICATION:
   - Dùng connection string khác nhau cho từng role
   - Hoặc dùng EXECUTE AS USER để chuyển đổi context
*/

PRINT 'Role-based security setup completed successfully!';
PRINT 'QuanLyRole: Full control on all tables + Execute permissions on 29 procedures, 14 functions, 6 views used in C# forms';
PRINT 'KyThuatRole: Maintenance permissions + Execute on 8 procedures, 5 functions, 4 views for maintenance operations';
PRINT 'NhanVienTrucRole: Basic read permissions + INSERT on CSVC & ThongTinSuDung + Execute on 6 procedures, 4 functions, 1 view for staff operations';
PRINT '';
PRINT 'NOTE: This script only includes database objects actually used in the C# application forms.';
GO