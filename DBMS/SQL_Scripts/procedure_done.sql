-- lấy danh sách nhân viên
use vc;
go

CREATE OR ALTER PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        NhanVienID,
        HoTen,
        NgaySinh,
        GioiTinh,
        DiaChi,
        SoDienThoai,
        Email,
        TrangThai,
        ChucVu,
        DATEDIFF(YEAR, NgaySinh, GETDATE()) AS Tuoi
    FROM NhanVien
    ORDER BY HoTen;
END
GO
-- 3. PROCEDURE THÊM NHÂN VIÊN MỚI
CREATE OR ALTER PROCEDURE sp_InsertEmployee
    @HoTen NVARCHAR(100),
    @NgaySinh DATE = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @DiaChi NVARCHAR(200) = NULL,
    @SoDienThoai VARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @ChucVu NVARCHAR(50),
    @NewEmployeeID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra email trùng lặp
        IF EXISTS (SELECT 1 FROM NhanVien WHERE Email = @Email AND @Email IS NOT NULL)
        BEGIN
            RAISERROR('Email đã tồn tại trong hệ thống!', 16, 1);
            RETURN;
        END
        
        -- Thêm nhân viên mới
        INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, ChucVu, TrangThai)
        VALUES (@HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @Email, @ChucVu, N'Đang làm');
        
        SET @NewEmployeeID = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        
        SELECT 'SUCCESS' AS Result, 'Thêm nhân viên thành công!' AS Message, @NewEmployeeID AS EmployeeID;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Message, 0 AS EmployeeID;
    END CATCH
END
GO

-- 0. PROCEDURE CHI TIẾT PHÂN CÔNG CA 
-- 4. PROCEDURE THÊM PHÂN CÔNG CA MỚI (ĐƠN GIẢN)