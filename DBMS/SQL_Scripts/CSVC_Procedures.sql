USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO QUẢN LÝ CƠ SỞ VẬT CHẤT
-- =============================================

-- 1. PROCEDURE THÊM CSVC MỚI (KÈM THÔNG TIN SỬ DỤNG)
CREATE OR ALTER PROCEDURE sp_AddCSVC
    @TenCSVC NVARCHAR(100),
    @MaCSVC VARCHAR(50),
    @LoaiID INT = NULL,
    @ViTriID INT = NULL,
    @GiaTri DECIMAL(18,2) = 0,
    @TenNhaCungCap NVARCHAR(100) = NULL,
    @SoDienThoaiNCC VARCHAR(20) = NULL,
    @EmailNCC NVARCHAR(100) = NULL,
    @TinhTrang NVARCHAR(50) = N'Đang sử dụng',
    @GhiChu NVARCHAR(500) = NULL,
    -- Thông tin sử dụng
    @NgayMua DATE = NULL,
    @NgayHetBaoHanh DATE = NULL,
    @ThoiGianSuDungDuKien_Thang INT = NULL,
    @GhiChuSuDung NVARCHAR(200) = NULL,
    @CSVCID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra dữ liệu đầu vào
        IF @TenCSVC IS NULL OR LEN(TRIM(@TenCSVC)) = 0
        BEGIN
            RAISERROR(N'Tên CSVC không được để trống', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra trùng mã CSVC
        IF @MaCSVC IS NOT NULL AND EXISTS(SELECT 1 FROM CSVC WHERE MaCSVC = @MaCSVC)
        BEGIN
            RAISERROR(N'Mã CSVC đã tồn tại', 16, 1);
            RETURN;
        END
        
        -- Thêm CSVC mới
        INSERT INTO CSVC (
            TenCSVC, MaCSVC, LoaiID, ViTriID, GiaTri,
            TenNhaCungCap, SoDienThoaiNCC, EmailNCC,
            TinhTrang, GhiChu
        )
        VALUES (
            @TenCSVC, @MaCSVC, @LoaiID, @ViTriID, @GiaTri,
            @TenNhaCungCap, @SoDienThoaiNCC, @EmailNCC,
            @TinhTrang, @GhiChu
        );
        
        SET @CSVCID = SCOPE_IDENTITY();
        
        -- Thêm thông tin sử dụng nếu có
        IF @NgayMua IS NOT NULL
        BEGIN
            INSERT INTO ThongTinSuDung (
                CSVCID, NgayMua, NgayHetBaoHanh,
                ThoiGianSuDungDuKien_Thang, GhiChu
            )
            VALUES (
                @CSVCID, @NgayMua, @NgayHetBaoHanh,
                @ThoiGianSuDungDuKien_Thang, @GhiChuSuDung
            );
        END
        
        COMMIT TRANSACTION;
        
        SELECT @CSVCID AS NewCSVCID;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- 2. PROCEDURE CẬP NHẬT THÔNG TIN CSVC
CREATE OR ALTER PROCEDURE sp_UpdateCSVC
    @CSVCID INT,
    @TenCSVC NVARCHAR(100) = NULL,
    @MaCSVC VARCHAR(50) = NULL,
    @LoaiID INT = NULL,
    @ViTriID INT = NULL,
    @GiaTri DECIMAL(18,2) = NULL,
    @TenNhaCungCap NVARCHAR(100) = NULL,
    @SoDienThoaiNCC VARCHAR(20) = NULL,
    @EmailNCC NVARCHAR(100) = NULL,
    @TinhTrang NVARCHAR(50) = NULL,
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Kiểm tra CSVC có tồn tại
        IF NOT EXISTS(SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID)
        BEGIN
            RAISERROR(N'CSVC không tồn tại', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra trùng mã CSVC (nếu thay đổi)
        IF @MaCSVC IS NOT NULL AND EXISTS(
            SELECT 1 FROM CSVC 
            WHERE MaCSVC = @MaCSVC AND CSVCID != @CSVCID
        )
        BEGIN
            RAISERROR(N'Mã CSVC đã tồn tại', 16, 1);
            RETURN;
        END
        
        -- Cập nhật thông tin
        UPDATE CSVC 
        SET
            TenCSVC = ISNULL(@TenCSVC, TenCSVC),
            MaCSVC = ISNULL(@MaCSVC, MaCSVC),
            LoaiID = ISNULL(@LoaiID, LoaiID),
            ViTriID = ISNULL(@ViTriID, ViTriID),
            GiaTri = ISNULL(@GiaTri, GiaTri),
            TenNhaCungCap = ISNULL(@TenNhaCungCap, TenNhaCungCap),
            SoDienThoaiNCC = ISNULL(@SoDienThoaiNCC, SoDienThoaiNCC),
            EmailNCC = ISNULL(@EmailNCC, EmailNCC),
            TinhTrang = ISNULL(@TinhTrang, TinhTrang),
            GhiChu = ISNULL(@GhiChu, GhiChu)
        WHERE CSVCID = @CSVCID;
        
        SELECT @@ROWCOUNT AS RowsAffected;
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- 3. PROCEDURE XÓA CSVC (KIỂM TRA RÀNG BUỘC)
CREATE OR ALTER PROCEDURE sp_DeleteCSVC
    @CSVCID INT,
    @ForceDelete BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra CSVC có tồn tại
        IF NOT EXISTS(SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID)
        BEGIN
            RAISERROR(N'CSVC không tồn tại', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra ràng buộc (nếu không force delete)
        IF @ForceDelete = 0
        BEGIN
            -- Kiểm tra lịch sử bảo trì
            IF EXISTS(SELECT 1 FROM LichSuBaoTri WHERE CSVCID = @CSVCID)
            BEGIN
                RAISERROR(N'Không thể xóa CSVC đã có lịch sử bảo trì. ', 16, 1);
                RETURN;
            END
            -- Kiểm tra đã thanh lý
            IF EXISTS(SELECT 1 FROM ThanhLyCSVC WHERE CSVCID = @CSVCID)
            BEGIN
                RAISERROR(N'Không thể xóa CSVC đã được thanh lý', 16, 1);
                RETURN;
            END
        END
        
        -- Xóa dữ liệu liên quan (CASCADE sẽ tự động xóa)
        DELETE FROM CSVC WHERE CSVCID = @CSVCID;
        
        COMMIT TRANSACTION;
        
        SELECT @@ROWCOUNT AS RowsDeleted;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- 4. PROCEDURE TÌM KIẾM CSVC THEO 3 TIÊU CHÍ ĐỀN GIẢN
CREATE OR ALTER PROCEDURE sp_SearchCSVC
    @TenCSVC NVARCHAR(100) = NULL,    -- Tìm theo tên CSVC
    @LoaiID INT = NULL,               -- Tìm theo loại CSVC
    @TinhTrang NVARCHAR(50) = NULL    -- Tìm theo tình trạng
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CSVCID,
        c.TenCSVC,
        c.MaCSVC,
        c.LoaiID,
        ISNULL(l.TenLoai, N'Chưa phân loại') AS TenLoai,
        c.ViTriID,
        CASE 
            WHEN v.ViTriID IS NOT NULL 
            THEN v.KhuVuc + N' - Tầng ' + CAST(v.Tang AS NVARCHAR(5))
            ELSE N'Chưa xác định'
        END AS ViTri,
        c.GiaTri,
        c.TenNhaCungCap,
        c.SoDienThoaiNCC,
        c.TinhTrang,
        c.GhiChu,
        -- Thông tin sử dụng cơ bản
        t.NgayMua,
        t.NgayHetBaoHanh
    FROM CSVC c
        LEFT JOIN LoaiCSVC l ON c.LoaiID = l.LoaiID
        LEFT JOIN ViTri v ON c.ViTriID = v.ViTriID
        LEFT JOIN ThongTinSuDung t ON c.CSVCID = t.CSVCID
    WHERE 
        (@TenCSVC IS NULL OR c.TenCSVC LIKE '%' + @TenCSVC + '%')
        AND (@LoaiID IS NULL OR c.LoaiID = @LoaiID)
        AND (@TinhTrang IS NULL OR c.TinhTrang = @TinhTrang)
    ORDER BY c.CSVCID;
END
GO
