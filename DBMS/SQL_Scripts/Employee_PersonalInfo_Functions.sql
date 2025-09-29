-- ================================================
-- Employee Personal Info Functions
-- ================================================

USE vc;
GO

-- Lấy NhanVienID từ Username
CREATE OR ALTER FUNCTION fn_GetNhanVienIDByUsername(@Username NVARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @NhanVienID INT;
    
    SELECT @NhanVienID = nv.NhanVienID
    FROM NhanVien nv
    INNER JOIN TaiKhoan tk ON nv.NhanVienID = tk.NhanVienID
    WHERE tk.Username = @Username;
    
    RETURN ISNULL(@NhanVienID, 0);
END;
GO

-- Lấy thông tin chi tiết nhân viên theo NhanVienID
CREATE OR ALTER FUNCTION fn_GetNhanVienInfo(@NhanVienID INT)
RETURNS TABLE
AS
RETURN (
    SELECT 
        nv.NhanVienID,
        nv.HoTen AS TenNhanVien,
        nv.NgaySinh,
        nv.GioiTinh,
        nv.SoDienThoai,
        nv.Email,
        nv.DiaChi,
        nv.ChucVu,
        nv.TrangThai,
        tk.Username,
        tk.Role AS VaiTro
    FROM NhanVien nv
    LEFT JOIN TaiKhoan tk ON nv.NhanVienID = tk.NhanVienID
    WHERE nv.NhanVienID = @NhanVienID
);
GO

-- Grant permissions for roles
GRANT EXECUTE ON fn_GetNhanVienIDByUsername TO QuanLyRole;
GRANT EXECUTE ON fn_GetNhanVienIDByUsername TO KyThuatRole;
GRANT EXECUTE ON fn_GetNhanVienIDByUsername TO NhanVienTrucRole;

GRANT SELECT ON fn_GetNhanVienInfo TO QuanLyRole;
GRANT SELECT ON fn_GetNhanVienInfo TO KyThuatRole;
GRANT SELECT ON fn_GetNhanVienInfo TO NhanVienTrucRole;
GO

PRINT 'Employee Personal Info Functions created successfully!';