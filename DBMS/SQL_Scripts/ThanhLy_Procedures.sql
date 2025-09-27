USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO THANH LÝ CSVC
-- =============================================

-- 1. THÊM THANH LÝ CSVC
CREATE OR ALTER PROCEDURE sp_AddAssetDisposal
(
    @CSVCID INT,
    @LyDoThanhLy NVARCHAR(500),
    @GiaTriThanhLy DECIMAL(18,2) = NULL,
    @NguoiThucHienID INT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra CSVC đã được thanh lý chưa
        IF EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID AND TinhTrang = N'Đã thanh lý')
        BEGIN
            RAISERROR(N'CSVC này đã được thanh lý trước đó!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra CSVC có đang trong quá trình bảo trì không
        IF EXISTS (
            SELECT 1 FROM LichSuBaoTri 
            WHERE CSVCID = @CSVCID 
            AND TrangThai IN (N'Chờ xử lý', N'Đang xử lý')
        )
        BEGIN
            RAISERROR(N'CSVC đang trong quá trình bảo trì, không thể thanh lý!', 16, 1);
            RETURN;
        END
        
        BEGIN TRANSACTION;
        
        -- Thêm vào bảng thanh lý với ngày hiện tại
        INSERT INTO ThanhLyCSVC (CSVCID, NgayThanhLy, LyDoThanhLy, GiaTriThanhLy, NguoiThucHienID)
        VALUES (@CSVCID, GETDATE(), @LyDoThanhLy, @GiaTriThanhLy, @NguoiThucHienID);
        
        -- Cập nhật trạng thái CSVC
        UPDATE CSVC 
        SET TinhTrang = N'Đã thanh lý'
        WHERE CSVCID = @CSVCID;
        
        -- Xóa lịch bảo trì định kỳ nếu có
        DELETE FROM LichBaoTri WHERE CSVCID = @CSVCID;
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 2. CẬP NHẬT THÔNG TIN THANH LÝ
CREATE OR ALTER PROCEDURE sp_UpdateAssetDisposal
(
    @ThanhLyID INT,
    @LyDoThanhLy NVARCHAR(500) = NULL,
    @GiaTriThanhLy DECIMAL(18,2) = NULL,
    @NgayThanhLy DATE = NULL,
    @NguoiThucHienID INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Cập nhật thông tin
        UPDATE ThanhLyCSVC
        SET 
            LyDoThanhLy = ISNULL(@LyDoThanhLy, LyDoThanhLy),
            GiaTriThanhLy = ISNULL(@GiaTriThanhLy, GiaTriThanhLy),
            NgayThanhLy = ISNULL(@NgayThanhLy, NgayThanhLy),
            NguoiThucHienID = ISNULL(@NguoiThucHienID, NguoiThucHienID)
        WHERE ThanhLyID = @ThanhLyID;
        
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO



-- 4. TÌM KIẾM LỊCH SỬ THANH LÝ
CREATE OR ALTER PROCEDURE sp_SearchAssetDisposal
(
    @CSVCID INT = NULL,
    @GiaTriThanhLy DECIMAL(18,2) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        tl.ThanhLyID,
        c.TenCSVC,
        tl.NgayThanhLy,
        tl.LyDoThanhLy,
        tl.GiaTriThanhLy
    FROM ThanhLyCSVC tl
    INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
    WHERE 
        (@CSVCID IS NULL OR tl.CSVCID = @CSVCID)
        AND (@GiaTriThanhLy IS NULL OR tl.GiaTriThanhLy = @GiaTriThanhLy)
    ORDER BY tl.NgayThanhLy DESC;
END
GO

