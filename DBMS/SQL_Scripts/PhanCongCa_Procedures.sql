USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO PHÂN CÔNG CA TRỰC
-- =============================================

-- 0. PROCEDURE CHI TIẾT PHÂN CÔNG CA (THAY THẾ VIEW vw_WorkShiftDetails)
CREATE OR ALTER PROCEDURE sp_GetWorkShiftDetails
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        pc.PhanCongID,
        pc.NhanVienID,
        nv.HoTen,
        nv.NgaySinh,
        DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) AS Tuoi,
        pc.TenCa,
        pc.GioBatDau,
        pc.GioKetThuc,
        pc.NgayLamViec,
        MONTH(pc.NgayLamViec) AS Thang,
        YEAR(pc.NgayLamViec) AS Nam,
        DATENAME(WEEKDAY, pc.NgayLamViec) AS ThuTrongTuan,
        pc.VaiTroTrongCa,
        pc.GhiChu,
        -- Tính toán các thông tin bổ sung
        DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 AS SoGioLam,
        CASE 
            WHEN pc.NgayLamViec = CAST(GETDATE() AS DATE) THEN N'Hôm nay'
            WHEN pc.NgayLamViec > CAST(GETDATE() AS DATE) THEN N'Tương lai'
            ELSE N'Đã qua'
        END AS TinhTrangCa
    FROM PhanCongCa pc
    INNER JOIN NhanVien nv ON pc.NhanVienID = nv.NhanVienID
    ORDER BY pc.NgayLamViec DESC, 
        pc.GioBatDau ASC,
        nv.HoTen ASC;
END
GO



-- 2. PROCEDURE LẤY PHÂN CÔNG CA THEO ID
CREATE OR ALTER PROCEDURE sp_GetWorkShiftById
    @PhanCongID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        pc.PhanCongID,
        pc.NhanVienID,
        nv.HoTen,
        nv.ChucVu,
        pc.TenCa,
        pc.GioBatDau,
        pc.GioKetThuc,
        pc.NgayLamViec,
        pc.VaiTroTrongCa,
        pc.GhiChu,
        DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 AS SoGioLam
    FROM PhanCongCa pc
    INNER JOIN NhanVien nv ON pc.NhanVienID = nv.NhanVienID
    WHERE pc.PhanCongID = @PhanCongID;
END
GO


-- 4. PROCEDURE THÊM PHÂN CÔNG CA MỚI (ĐƠN GIẢN)
CREATE OR ALTER PROCEDURE sp_InsertWorkShift
    @NhanVienID INT,
    @NgayLamViec DATE,
    @TenCa NVARCHAR(50),
    @GioBatDau TIME,
    @GioKetThuc TIME,
    @VaiTroTrongCa NVARCHAR(100),
    @GhiChu NVARCHAR(255) = NULL,
    @Success BIT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra input bắt buộc
        IF @NhanVienID IS NULL OR @NgayLamViec IS NULL OR @TenCa IS NULL 
           OR @GioBatDau IS NULL OR @GioKetThuc IS NULL OR @VaiTroTrongCa IS NULL
        BEGIN
            SET @Success = 0;
            SET @Message = N'Vui lòng nhập đầy đủ thông tin bắt buộc!';
            RETURN;
        END
        
        -- Kiểm tra nhân viên tồn tại
        IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE NhanVienID = @NhanVienID)
        BEGIN
            SET @Success = 0;
            SET @Message = N'Nhân viên không tồn tại!';
            RETURN;
        END
        
        -- Kiểm tra thời gian hợp lệ
        IF @GioBatDau >= @GioKetThuc
        BEGIN
            SET @Success = 0;
            SET @Message = N'Giờ bắt đầu phải nhỏ hơn giờ kết thúc!';
            RETURN;
        END
        
        -- Kiểm tra trùng ca trong ngày
        IF EXISTS (
            SELECT 1 FROM PhanCongCa 
            WHERE NhanVienID = @NhanVienID 
              AND NgayLamViec = @NgayLamViec
              AND (
                  (@GioBatDau >= GioBatDau AND @GioBatDau < GioKetThuc)
                  OR (@GioKetThuc > GioBatDau AND @GioKetThuc <= GioKetThuc)
                  OR (@GioBatDau <= GioBatDau AND @GioKetThuc >= GioKetThuc)
              )
        )
        BEGIN
            SET @Success = 0;
            SET @Message = N'Nhân viên đã có ca trực trùng thời gian trong ngày này!';
            RETURN;
        END
        
        -- Thêm phân công ca mới
        INSERT INTO PhanCongCa (
            NhanVienID, 
            NgayLamViec, 
            TenCa, 
            GioBatDau, 
            GioKetThuc, 
            VaiTroTrongCa, 
            GhiChu
        )
        VALUES (
            @NhanVienID, 
            @NgayLamViec, 
            @TenCa, 
            @GioBatDau, 
            @GioKetThuc, 
            @VaiTroTrongCa, 
            @GhiChu
        );
        
        SET @Success = 1;
        SET @Message = N'Thêm phân công ca thành công!';
        
    END TRY
    BEGIN CATCH
        SET @Success = 0;
        SET @Message = N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO

-- 5. PROCEDURE CẬP NHẬT PHÂN CÔNG CA
CREATE OR ALTER PROCEDURE sp_UpdateWorkShift
    @PhanCongID INT,
    @NhanVienID INT,
    @NgayLamViec DATE,
    @TenCa NVARCHAR(50),
    @GioBatDau TIME,
    @GioKetThuc TIME,
    @VaiTroTrongCa NVARCHAR(100),
    @GhiChu NVARCHAR(255) = NULL,
    @Success BIT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra phân công ca có tồn tại
        IF NOT EXISTS (SELECT 1 FROM PhanCongCa WHERE PhanCongID = @PhanCongID)
        BEGIN
            SET @Success = 0;
            SET @Message = N'Phân công ca không tồn tại!';
            RETURN;
        END
        
        -- Cập nhật phân công ca 
        UPDATE PhanCongCa 
        SET 
            NhanVienID = @NhanVienID,
            NgayLamViec = @NgayLamViec,
            TenCa = @TenCa,
            GioBatDau = @GioBatDau,
            GioKetThuc = @GioKetThuc,
            VaiTroTrongCa = @VaiTroTrongCa,
            GhiChu = @GhiChu
        WHERE PhanCongID = @PhanCongID;
        
        SET @Success = 1;
        SET @Message = N'Cập nhật phân công ca thành công!';
        
    END TRY
    BEGIN CATCH
        SET @Success = 0;
        SET @Message = N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO

-- 6. PROCEDURE XÓA PHÂN CÔNG CA
CREATE OR ALTER PROCEDURE sp_DeleteWorkShift
    @PhanCongID INT,
    @Success BIT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra phân công ca có tồn tại
        IF NOT EXISTS (SELECT 1 FROM PhanCongCa WHERE PhanCongID = @PhanCongID)
        BEGIN
            SET @Success = 0;
            SET @Message = N'Phân công ca không tồn tại!';
            RETURN;
        END
        
        -- Xóa phân công ca
        DELETE FROM PhanCongCa WHERE PhanCongID = @PhanCongID;
        
        SET @Success = 1;
        SET @Message = N'Xóa phân công ca thành công!';
        
    END TRY
    BEGIN CATCH
        SET @Success = 0;
        SET @Message = N'Lỗi: ' + ERROR_MESSAGE();
    END CATCH
END
GO

