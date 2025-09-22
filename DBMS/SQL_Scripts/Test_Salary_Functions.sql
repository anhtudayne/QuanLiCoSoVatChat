-- =============================================
-- TEST SCRIPT CHO CÁC FUNCTION TÍNH LƯƠNG
-- =============================================
USE vc;
GO

PRINT '==============================';
PRINT 'TESTING Salary Functions';
PRINT '==============================';

-- Test 1: Kiểm tra function tính tổng giờ làm việc
PRINT 'Test 1: fn_GetTotalWorkHoursInMonth';
PRINT '------------------------------------';
DECLARE @TestMonth INT = 9;
DECLARE @TestYear INT = 2025;

-- Test với một nhân viên cụ thể (giả sử NhanVienID = 1)
SELECT 
    N'Test Tổng giờ làm việc' AS TestCase,
    1 AS NhanVienID,
    @TestMonth AS Thang,
    @TestYear AS Nam,
    dbo.fn_GetTotalWorkHoursInMonth(1, @TestMonth, @TestYear) AS TongGioLamViec;

PRINT '';

-- Test 2: Kiểm tra function tính lương của một nhân viên
PRINT 'Test 2: fn_GetEmployeeSalaryInMonth';
PRINT '-----------------------------------';
SELECT 
    N'Test Lương nhân viên' AS TestCase,
    1 AS NhanVienID,
    @TestMonth AS Thang,
    @TestYear AS Nam,
    dbo.fn_GetEmployeeSalaryInMonth(1, @TestMonth, @TestYear) AS TongLuong;

PRINT '';

-- Test 3: Kiểm tra function lấy thông tin lương chi tiết
PRINT 'Test 3: fn_GetEmployeeSalaryDetails';
PRINT '-----------------------------------';
SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(1, @TestMonth, @TestYear);

PRINT '';

-- Test 4: Kiểm tra function tính lương tất cả nhân viên
PRINT 'Test 4: fn_GetAllEmployeeSalariesInMonth';
PRINT '----------------------------------------';
SELECT * FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear)
ORDER BY HoTen;

PRINT '';

-- Test 5: Thống kê tổng lương toàn bộ công ty trong tháng
PRINT 'Test 5: Thống kê tổng lương công ty';
PRINT '----------------------------------';
SELECT 
    @TestMonth AS Thang,
    @TestYear AS Nam,
    COUNT(*) AS SoNhanVien,
    SUM(TongGioLamViec) AS TongGioLamViecToanCongTy,
    SUM(TongLuong) AS TongLuongToanCongTy,
    AVG(TongLuong) AS LuongTrungBinh,
    MIN(TongLuong) AS LuongThapNhat,
    MAX(TongLuong) AS LuongCaoNhat
FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear);

PRINT '';

-- Test 6: Thống kê lương theo chức vụ
PRINT 'Test 6: Thống kê lương theo chức vụ';
PRINT '----------------------------------';
SELECT 
    ChucVu,
    COUNT(*) AS SoNhanVien,
    SUM(TongGioLamViec) AS TongGioLamViec,
    SUM(TongLuong) AS TongLuong,
    AVG(TongLuong) AS LuongTrungBinh
FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear)
GROUP BY ChucVu
ORDER BY TongLuong DESC;

PRINT '';

-- Test 7: Nhân viên có lương cao nhất và thấp nhất
PRINT 'Test 7: Top 3 nhân viên lương cao nhất';
PRINT '-------------------------------------';
SELECT TOP 3
    HoTen,
    ChucVu,
    TongGioLamViec,
    LuongCoBanTheoGio,
    TongLuong
FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear)
WHERE TongLuong > 0
ORDER BY TongLuong DESC;

PRINT '';
PRINT 'TESTING COMPLETED!';
PRINT '==============================';