-- =============================================
-- TEST INTEGRATION CỦA CÁC FUNCTION LƯƠNG VỚI C#
-- =============================================
USE vc;
GO

PRINT '==============================';
PRINT 'TESTING Integration with C#';
PRINT '==============================';

-- Tạo dữ liệu test nếu chưa có
PRINT 'Setting up test data...';

-- Thêm dữ liệu lương test nếu chưa có
IF NOT EXISTS (SELECT 1 FROM ThongTinLuongNhanVien WHERE Thang = 9 AND Nam = 2025)
BEGIN
    INSERT INTO ThongTinLuongNhanVien (NhanVienID, Thang, Nam, LuongCoBanTheoGio)
    SELECT TOP 5 
        NhanVienID, 
        9, 
        2025,
        CASE 
            WHEN ChucVu = N'Quản lý' THEN 50000
            WHEN ChucVu = N'Giám sát' THEN 35000
            ELSE 25000
        END
    FROM NhanVien 
    WHERE TrangThai = N'Đang làm'
    ORDER BY NhanVienID;
    
    PRINT 'Added salary data for September 2025';
END

-- Thêm dữ liệu phân công ca test
IF NOT EXISTS (SELECT 1 FROM PhanCongCa WHERE MONTH(NgayLamViec) = 9 AND YEAR(NgayLamViec) = 2025)
BEGIN
    DECLARE @Counter INT = 1;
    DECLARE @MaxNV INT = (SELECT MAX(NhanVienID) FROM NhanVien WHERE TrangThai = N'Đang làm');
    
    WHILE @Counter <= 20  -- Tạo 20 ca làm việc test
    BEGIN
        INSERT INTO PhanCongCa (NhanVienID, TenCa, GioBatDau, GioKetThuc, NgayLamViec, VaiTroTrongCa)
        VALUES (
            (@Counter % @MaxNV) + 1,  -- Cycle through employee IDs
            N'Ca sáng',
            '08:00',
            '16:00',
            DATEADD(DAY, @Counter % 30, '2025-09-01'),  -- Spread across September
            N'Nhân viên trực'
        );
        
        SET @Counter = @Counter + 1;
    END
    
    PRINT 'Added shift data for September 2025';
END

PRINT '';

-- Test 1: Function được sử dụng trong ViewSalaryForm
PRINT 'Test 1: fn_GetAllEmployeeSalariesInMonth (ViewSalaryForm)';
PRINT '--------------------------------------------------------';
SELECT TOP 5 * FROM dbo.fn_GetAllEmployeeSalariesInMonth(9, 2025) ORDER BY TongLuong DESC;

PRINT '';

-- Test 2: Function cho báo cáo chi tiết nhân viên
PRINT 'Test 2: fn_GetEmployeeSalaryDetails (Individual Employee)';
PRINT '---------------------------------------------------------';
SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(1, 9, 2025);

PRINT '';

-- Test 3: Function cho báo cáo theo quý (SalaryReportForm)
PRINT 'Test 3: fn_GetAllEmployeeSalariesInQuarter (SalaryReportForm)';
PRINT '-------------------------------------------------------------';
SELECT TOP 3 * FROM dbo.fn_GetAllEmployeeSalariesInQuarter(3, 2025) ORDER BY TongLuongQuy DESC;

PRINT '';

-- Test 4: Function cho báo cáo theo năm
PRINT 'Test 4: fn_GetAllEmployeeSalariesInYear (Annual Report)';
PRINT '------------------------------------------------------';
SELECT TOP 3 * FROM dbo.fn_GetAllEmployeeSalariesInYear(2025) ORDER BY TongLuongNam DESC;

PRINT '';

-- Test 5: Simulation của C# ViewSalaryForm query
PRINT 'Test 5: Simulation ViewSalaryForm Queries';
PRINT '-----------------------------------------';

-- Query cho "Tất cả nhân viên" trong tháng 9/2025
PRINT 'Query: Tất cả nhân viên tháng 9/2025';
SELECT 
    COUNT(*) AS TotalEmployees,
    SUM(TongGioLamViec) AS TotalHours,
    SUM(TongLuong) AS TotalSalary,
    AVG(TongLuong) AS AvgSalary
FROM dbo.fn_GetAllEmployeeSalariesInMonth(9, 2025);

PRINT '';

-- Query cho nhân viên cụ thể với nhiều tháng
PRINT 'Query: Employee ID=1 cho cả năm 2025';
SELECT 
    Thang,
    TongGioLamViec,
    TongLuong
FROM (
    SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(1, 1, 2025)
    UNION ALL SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(1, 2, 2025)
    UNION ALL SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(1, 3, 2025)
    UNION ALL SELECT * FROM dbo.fn_GetEmployeeSalaryDetails(1, 9, 2025)  -- Tháng có dữ liệu
) AS AllMonths
WHERE TongLuong > 0
ORDER BY Thang;

PRINT '';

-- Test 6: Performance test
PRINT 'Test 6: Performance Comparison';
PRINT '------------------------------';
DECLARE @StartTime DATETIME = GETDATE();

-- Test function performance
SELECT COUNT(*) AS RecordCount FROM dbo.fn_GetAllEmployeeSalariesInMonth(9, 2025);

DECLARE @EndTime DATETIME = GETDATE();
PRINT 'Function execution time: ' + CAST(DATEDIFF(MILLISECOND, @StartTime, @EndTime) AS VARCHAR(10)) + ' ms';

PRINT '';

-- Test 7: Data validation
PRINT 'Test 7: Data Validation';
PRINT '-----------------------';
SELECT 
    'Data Validation' AS TestType,
    CASE 
        WHEN EXISTS (SELECT 1 FROM dbo.fn_GetAllEmployeeSalariesInMonth(9, 2025) WHERE TongLuong < 0) 
        THEN 'FAILED: Negative salary found'
        ELSE 'PASSED: No negative salaries'
    END AS ValidationResult
UNION ALL
SELECT 
    'Hours Validation',
    CASE 
        WHEN EXISTS (SELECT 1 FROM dbo.fn_GetAllEmployeeSalariesInMonth(9, 2025) WHERE TongGioLamViec < 0)
        THEN 'FAILED: Negative hours found'
        ELSE 'PASSED: No negative hours'
    END;

PRINT '';
PRINT 'C# INTEGRATION TESTING COMPLETED!';
PRINT 'Functions are ready for use in WinForms!';
PRINT '==============================';