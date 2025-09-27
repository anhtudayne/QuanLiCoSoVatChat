USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO BẢO TRÌ ĐỊNH KỲ
-- =============================================

-- 2. TẠO YÊU CẦU BẢO TRÌ TỪ LỊCH ĐỊNH KỲ
CREATE OR ALTER PROCEDURE sp_CreateMaintenanceFromSchedule
(
    @LichID INT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @CSVCID INT;
        DECLARE @TenCSVC NVARCHAR(100);
        
        -- Lấy thông tin CSVC từ lịch bảo trì
        SELECT @CSVCID = lb.CSVCID, @TenCSVC = c.TenCSVC
        FROM LichBaoTri lb
        INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID
        WHERE lb.LichID = @LichID;
        
        -- Kiểm tra lịch tồn tại
        IF @CSVCID IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy lịch bảo trì!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra CSVC chưa bị thanh lý
        IF EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID AND TinhTrang = N'Đã thanh lý')
        BEGIN
            RAISERROR(N'CSVC đã bị thanh lý, không thể tạo yêu cầu bảo trì!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra CSVC chưa có yêu cầu bảo trì đang chờ xử lý
        IF EXISTS (SELECT 1 FROM LichSuBaoTri WHERE CSVCID = @CSVCID AND TrangThai IN (N'Chờ xử lý', N'Đang xử lý'))
        BEGIN
            RAISERROR(N'CSVC này đã có yêu cầu bảo trì đang chờ xử lý hoặc đang thực hiện!', 16, 1);
            RETURN;
        END
        
        -- Tạo yêu cầu bảo trì định kỳ
        INSERT INTO LichSuBaoTri (CSVCID, NgayYeuCau, NoiDung, TrangThai)
        VALUES (@CSVCID, GETDATE(), N'Bảo trì định kỳ', N'Chờ xử lý');
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- 3. XÓA LỊCH BẢO TRÌ ĐỊNH KỲ
CREATE OR ALTER PROCEDURE sp_DeleteMaintenanceSchedule
(
    @LichID INT
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra lịch tồn tại
        IF NOT EXISTS (SELECT 1 FROM LichBaoTri WHERE LichID = @LichID)
        BEGIN
            RAISERROR(N'Không tìm thấy lịch bảo trì!', 16, 1);
            RETURN;
        END
        
        -- Xóa lịch bảo trì
        DELETE FROM LichBaoTri WHERE LichID = @LichID;
        
        PRINT N'Đã xóa lịch bảo trì thành công!';
        
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO


