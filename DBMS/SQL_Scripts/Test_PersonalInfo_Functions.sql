-- ================================================
-- Test Script for Personal Info Functions
-- ================================================

USE vc;
GO

PRINT '=== Testing Personal Info Functions ===';
PRINT '';

-- Test 1: Test fn_GetNhanVienIDByUsername function
PRINT '1. Testing fn_GetNhanVienIDByUsername:';
PRINT '';

-- Test with existing usernames
DECLARE @TestUsername NVARCHAR(50) = 'admin';
DECLARE @TestNhanVienID INT;

SELECT @TestNhanVienID = dbo.fn_GetNhanVienIDByUsername(@TestUsername);
PRINT 'Username: ' + @TestUsername + ' -> NhanVienID: ' + CAST(ISNULL(@TestNhanVienID, 0) AS VARCHAR(10));

-- Test with non-existing username
SET @TestUsername = 'nonexistentuser';
SELECT @TestNhanVienID = dbo.fn_GetNhanVienIDByUsername(@TestUsername);
PRINT 'Username: ' + @TestUsername + ' -> NhanVienID: ' + CAST(ISNULL(@TestNhanVienID, 0) AS VARCHAR(10));

PRINT '';

-- Test 2: Test fn_GetNhanVienInfo function
PRINT '2. Testing fn_GetNhanVienInfo:';
PRINT '';

-- Test with existing NhanVienID
DECLARE @TestID INT = 1;
PRINT 'Getting info for NhanVienID: ' + CAST(@TestID AS VARCHAR(10));
SELECT 
    NhanVienID,
    TenNhanVien,
    NgaySinh,
    GioiTinh,
    SoDienThoai,
    Email,
    ChucVu,
    VaiTro,
    TrangThai
FROM dbo.fn_GetNhanVienInfo(@TestID);

PRINT '';

-- Test 3: Combined test - Get NhanVienID by Username then get info
PRINT '3. Combined Test - Username to Full Info:';
PRINT '';

SET @TestUsername = 'kythuat';
PRINT 'Testing complete flow for username: ' + @TestUsername;

-- Get NhanVienID
SELECT @TestNhanVienID = dbo.fn_GetNhanVienIDByUsername(@TestUsername);
PRINT 'Step 1 - NhanVienID found: ' + CAST(ISNULL(@TestNhanVienID, 0) AS VARCHAR(10));

-- Get full info if NhanVienID found
IF @TestNhanVienID > 0
BEGIN
    PRINT 'Step 2 - Employee Information:';
    SELECT 
        'Mã NV: ' + CAST(NhanVienID AS VARCHAR(10)) AS Info,
        'Tên: ' + TenNhanVien AS Name,
        'Chức vụ: ' + ISNULL(ChucVu, 'N/A') AS Position,
        'Vai trò: ' + ISNULL(VaiTro, 'N/A') AS Role,
        'Email: ' + ISNULL(Email, 'N/A') AS Email,
        'SĐT: ' + ISNULL(SoDienThoai, 'N/A') AS Phone
    FROM dbo.fn_GetNhanVienInfo(@TestNhanVienID);
END
ELSE
BEGIN
    PRINT 'No employee found for username: ' + @TestUsername;
END

PRINT '';

-- Test 4: Test permission with different roles
PRINT '4. Testing Role Permissions:';
PRINT '';

-- Test current user's permission
SELECT 
    USER_NAME() AS CurrentUser,
    IS_MEMBER('QuanLyRole') AS IsQuanLy,
    IS_MEMBER('KyThuatRole') AS IsKyThuat,
    IS_MEMBER('NhanVienTrucRole') AS IsNhanVienTruc;

PRINT '';

-- Test 5: List all available employees with accounts
PRINT '5. All Employees with Account Information:';
PRINT '';

SELECT 
    nv.NhanVienID,
    nv.HoTen AS TenNhanVien,
    nv.ChucVu,
    tk.Username,
    tk.Role AS VaiTro,
    nv.TrangThai
FROM NhanVien nv
LEFT JOIN TaiKhoan tk ON nv.NhanVienID = tk.NhanVienID
WHERE nv.TrangThai = N'Đang làm'
ORDER BY nv.NhanVienID;

PRINT '';
PRINT '=== Test Completed ===';

-- Performance test
PRINT '6. Performance Test:';
DECLARE @StartTime DATETIME = GETDATE();
DECLARE @Counter INT = 1;

WHILE @Counter <= 100
BEGIN
    SELECT @TestNhanVienID = dbo.fn_GetNhanVienIDByUsername('admin');
    SET @Counter = @Counter + 1;
END

DECLARE @EndTime DATETIME = GETDATE();
PRINT 'Time for 100 calls to fn_GetNhanVienIDByUsername: ' + 
      CAST(DATEDIFF(millisecond, @StartTime, @EndTime) AS VARCHAR(10)) + ' ms';

GO