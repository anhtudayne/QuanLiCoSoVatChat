USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO QUẢN LÝ NHÂN VIÊN
-- =============================================

-- 1. PROCEDURE LẤY DANH SÁCH NHÂN VIÊN
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
    ORDER BY NhanVienID;
END
GO

-- 2. PROCEDURE LẤY NHÂN VIÊN THEO ID
CREATE OR ALTER PROCEDURE sp_GetEmployeeById
    @NhanVienID INT
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
        ChucVu
    FROM NhanVien
    WHERE NhanVienID = @NhanVienID;
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

-- 4. PROCEDURE CẬP NHẬT NHÂN VIÊN
CREATE OR ALTER PROCEDURE sp_UpdateEmployee
    @NhanVienID INT,
    @HoTen NVARCHAR(100),
    @NgaySinh DATE = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @DiaChi NVARCHAR(200) = NULL,
    @SoDienThoai VARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @ChucVu NVARCHAR(50),
    @TrangThai NVARCHAR(20) = N'Đang làm'
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra nhân viên có tồn tại
        IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE NhanVienID = @NhanVienID)
        BEGIN
            RAISERROR('Không tìm thấy nhân viên!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra email trùng lặp (trừ chính nhân viên đó)
        IF EXISTS (SELECT 1 FROM NhanVien WHERE Email = @Email AND NhanVienID != @NhanVienID AND @Email IS NOT NULL)
        BEGIN
            RAISERROR('Email đã tồn tại trong hệ thống!', 16, 1);
            RETURN;
        END
        
        -- Cập nhật thông tin
        UPDATE NhanVien 
        SET 
            HoTen = @HoTen,
            NgaySinh = @NgaySinh,
            GioiTinh = @GioiTinh,
            DiaChi = @DiaChi,
            SoDienThoai = @SoDienThoai,
            Email = @Email,
            ChucVu = @ChucVu,
            TrangThai = @TrangThai
        WHERE NhanVienID = @NhanVienID;
        
        COMMIT TRANSACTION;
        
        SELECT 'SUCCESS' AS Result, 'Cập nhật nhân viên thành công!' AS Message;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO

-- 5. PROCEDURE XÓA NHÂN VIÊN (SOFT DELETE)
CREATE OR ALTER PROCEDURE sp_DeleteEmployee
    @NhanVienID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra nhân viên có tồn tại
        IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE NhanVienID = @NhanVienID)
        BEGIN
            RAISERROR('Không tìm thấy nhân viên!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra nhân viên có đang được phân công ca không
        IF EXISTS (SELECT 1 FROM PhanCongCa WHERE NhanVienID = @NhanVienID AND NgayLamViec >= CAST(GETDATE() AS DATE))
        BEGIN
            RAISERROR('Không thể xóa nhân viên đang có ca trực!', 16, 1);
            RETURN;
        END
        
        -- Soft delete: chuyển trạng thái thành "Đã nghỉ việc"
        UPDATE NhanVien 
        SET TrangThai = N'Đã nghỉ việc'
        WHERE NhanVienID = @NhanVienID;
        
        COMMIT TRANSACTION;
        
        SELECT 'SUCCESS' AS Result, 'Xóa nhân viên thành công!' AS Message;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'ERROR' AS Result, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO

-- 6. PROCEDURE TÌM KIẾM NHÂN VIÊN
CREATE OR ALTER PROCEDURE sp_SearchEmployees
    @SearchTerm NVARCHAR(100) = NULL,
    @ChucVu NVARCHAR(50) = NULL,
    @TrangThai NVARCHAR(20) = NULL
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
    WHERE 
        (@SearchTerm IS NULL OR 
         HoTen LIKE '%' + @SearchTerm + '%' OR 
         Email LIKE '%' + @SearchTerm + '%' OR 
         SoDienThoai LIKE '%' + @SearchTerm + '%')
        AND (@ChucVu IS NULL OR ChucVu = @ChucVu)
        AND (@TrangThai IS NULL OR TrangThai = @TrangThai)
    ORDER BY NhanVienID;
END
GO


-- 8. PROCEDURE LẤY DANH SÁCH CHỨC VỤ
CREATE OR ALTER PROCEDURE sp_GetPositions
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT ChucVu 
    FROM NhanVien 
    WHERE ChucVu IS NOT NULL 
    ORDER BY ChucVu;
END
GO

-- 9. PROCEDURE LẤY DANH SÁCH TRẠNG THÁI
CREATE OR ALTER PROCEDURE sp_GetEmployeeStatuses
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT TrangThai 
    FROM NhanVien 
    WHERE TrangThai IS NOT NULL 
    ORDER BY TrangThai;
END
GO


