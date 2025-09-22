USE vc;
GO

-- =============================================
-- FUNCTIONS HỖ TRỢ CHO QUẢN LÝ NHÂN VIÊN
-- =============================================

-- 4. FUNCTION ĐẾM SỐ NHÂN VIÊN THEO CHỨC VỤ
CREATE OR ALTER FUNCTION fn_CountEmployeesByPosition(@ChucVu NVARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*)
    FROM NhanVien
    WHERE ChucVu = @ChucVu AND TrangThai = N'Đang làm';
    
    RETURN ISNULL(@Count, 0);
END
GO



-- 6. FUNCTION THỐNG KÊ NHÂN VIÊN THEO TRẠNG THÁI
CREATE OR ALTER FUNCTION fn_GetEmployeeStatistics()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        TrangThai,
        COUNT(*) AS SoLuong,
        CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM NhanVien) AS DECIMAL(5,2)) AS TyLePhanTram
    FROM NhanVien
    GROUP BY TrangThai
);
GO

-- 7. FUNCTION LẤY DANH SÁCH NHÂN VIÊN CÓ SINH NHẬT TRONG THÁNG
CREATE OR ALTER FUNCTION fn_GetBirthdayEmployees(@Thang INT = NULL)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        NhanVienID,
        HoTen,
        NgaySinh,
        ChucVu,
        SoDienThoai,
        Email,
        DAY(NgaySinh) AS NgaySinhNhat,
        -- Tính tuổi sắp tới trực tiếp
        DATEDIFF(YEAR, NgaySinh, GETDATE()) + 1 AS TuoiSapToi
    FROM NhanVien
    WHERE TrangThai = N'Đang làm'
      AND (@Thang IS NULL OR MONTH(NgaySinh) = @Thang)
      AND NgaySinh IS NOT NULL
);
GO

