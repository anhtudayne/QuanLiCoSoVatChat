-- =============================================
-- SCRIPT TEST CHO 3 FUNCTION CHÍNH
-- =============================================

USE vc;
GO

PRINT '========================================='
PRINT 'TESTING 3 MAIN SALARY FUNCTIONS'
PRINT '========================================='

-- Test với tháng hiện tại
DECLARE @TestMonth INT = MONTH(GETDATE())
DECLARE @TestYear INT = YEAR(GETDATE())

PRINT 'Testing with Month: ' + CAST(@TestMonth AS VARCHAR(2)) + ', Year: ' + CAST(@TestYear AS VARCHAR(4))
PRINT ''

-- =============================================
-- 1. TEST FUNCTION fn_GetTotalWorkHoursInMonth
-- =============================================
PRINT '1. Testing fn_GetTotalWorkHoursInMonth...'

SELECT TOP 3
    nv.NhanVienID,
    nv.HoTen,
    dbo.fn_GetTotalWorkHoursInMonth(nv.NhanVienID, @TestMonth, @TestYear) AS TotalHours
FROM NhanVien nv
WHERE nv.TrangThai = N'Đang làm'
ORDER BY nv.NhanVienID

PRINT 'fn_GetTotalWorkHoursInMonth - COMPLETED'
PRINT ''

-- =============================================
-- 2. TEST FUNCTION fn_GetAllEmployeeSalariesInMonth
-- =============================================
PRINT '2. Testing fn_GetAllEmployeeSalariesInMonth...'

SELECT TOP 5 *
FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear)
ORDER BY TongLuong DESC

PRINT 'fn_GetAllEmployeeSalariesInMonth - COMPLETED'
PRINT ''

-- =============================================
-- 3. TEST FUNCTION fn_GetEmployeeSalaryDetails
-- =============================================
PRINT '3. Testing fn_GetEmployeeSalaryDetails...'

-- Lấy ID của nhân viên đầu tiên
DECLARE @TestEmployeeID INT
SELECT TOP 1 @TestEmployeeID = NhanVienID 
FROM NhanVien 
WHERE TrangThai = N'Đang làm'
ORDER BY NhanVienID

PRINT 'Testing with Employee ID: ' + CAST(@TestEmployeeID AS VARCHAR(10))

SELECT *
FROM dbo.fn_GetEmployeeSalaryDetails(@TestEmployeeID, @TestMonth, @TestYear)

PRINT 'fn_GetEmployeeSalaryDetails - COMPLETED'
PRINT ''

-- =============================================
-- SUMMARY TEST
-- =============================================
PRINT '========================================='
PRINT 'SUMMARY STATISTICS'
PRINT '========================================='

SELECT 
    'Total Active Employees' AS Description,
    COUNT(*) AS Count
FROM NhanVien 
WHERE TrangThai = N'Đang làm'

UNION ALL

SELECT 
    'Employees with Salary Data' AS Description,
    COUNT(*) AS Count
FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear)
WHERE TongLuong > 0

UNION ALL

SELECT 
    'Total Salary This Month' AS Description,
    CAST(SUM(TongLuong) AS INT) AS Count
FROM dbo.fn_GetAllEmployeeSalariesInMonth(@TestMonth, @TestYear)

PRINT ''
PRINT 'ALL FUNCTION TESTS COMPLETED SUCCESSFULLY!'
PRINT '========================================='