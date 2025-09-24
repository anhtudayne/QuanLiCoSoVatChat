USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO BẢO TRÌ ĐỊNH KỲ
-- =============================================

-- 1. TẠO LỊCH BẢO TRÌ ĐỊNH KỲ CHO CSVC
CREATE OR ALTER PROCEDURE sp_CreateMaintenanceSchedule
(
    @CSVCID INT,
    @ChuKyBaoTri_Thang INT = 6,
    @NgayBatDau DATE = NULL,
    @GhiChu NVARCHAR(200) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra input
        IF @NgayBatDau IS NULL
            SET @NgayBatDau = GETDATE();
            
        IF @ChuKyBaoTri_Thang <= 0
        BEGIN
            RAISERROR(N'Chu kỳ bảo trì phải lớn hơn 0 tháng!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra CSVC tồn tại
        IF NOT EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID)
        BEGIN
            RAISERROR(N'CSVC không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra đã có lịch bảo trì chưa
        IF EXISTS (SELECT 1 FROM LichBaoTri WHERE CSVCID = @CSVCID)
        BEGIN
            RAISERROR(N'CSVC này đã có lịch bảo trì định kỳ!', 16, 1);
            RETURN;
        END
        
        -- Tạo lịch bảo trì mới
        INSERT INTO LichBaoTri (CSVCID, ChuKyBaoTri_Thang, NgayBatDau, GhiChu)
        VALUES (@CSVCID, @ChuKyBaoTri_Thang, @NgayBatDau, @GhiChu);
        
        PRINT N'Đã tạo lịch bảo trì định kỳ thành công!';
        
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- 2. CẬP NHẬT LỊCH BẢO TRÌ ĐỊNH KỲ
CREATE OR ALTER PROCEDURE sp_UpdateMaintenanceSchedule
(
    @LichID INT,
    @ChuKyBaoTri_Thang INT = NULL,
    @NgayBatDau DATE = NULL,
    @GhiChu NVARCHAR(200) = NULL
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
        
        -- Cập nhật thông tin
        UPDATE LichBaoTri
        SET 
            ChuKyBaoTri_Thang = ISNULL(@ChuKyBaoTri_Thang, ChuKyBaoTri_Thang),
            NgayBatDau = ISNULL(@NgayBatDau, NgayBatDau),
            GhiChu = ISNULL(@GhiChu, GhiChu)
        WHERE LichID = @LichID;
        
        PRINT N'Đã cập nhật lịch bảo trì thành công!';
        
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

-- 4. KIỂM TRA VÀ TẠO YÊU CẦU BẢO TRÌ ĐỊNH KỲ
CREATE OR ALTER PROCEDURE sp_CheckAndCreateScheduledMaintenance
(
    @SoNgayTruoc INT = 7 -- Tạo yêu cầu trước hạn bao nhiêu ngày
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @Count INT = 0;
        
        -- Tạo bảng tạm để lưu các CSVC cần bảo trì
        CREATE TABLE #CSVCCanBaoTri (
            CSVCID INT,
            LichID INT,
            NgayKeTiep DATE,
            ChuKyBaoTri_Thang INT,
            TenCSVC NVARCHAR(100)
        );
        
        -- Tìm các CSVC cần bảo trì trong thời gian tới
        INSERT INTO #CSVCCanBaoTri
        SELECT 
            lb.CSVCID,
            lb.LichID,
            lb.NgayKeTiep,
            lb.ChuKyBaoTri_Thang,
            c.TenCSVC
        FROM LichBaoTri lb
        INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID
        WHERE 
            lb.NgayKeTiep <= DATEADD(DAY, @SoNgayTruoc, GETDATE())
            AND c.TinhTrang != N'Đã thanh lý'
            -- Kiểm tra chưa có yêu cầu bảo trì nào trong vòng 30 ngày gần đây
            AND NOT EXISTS (
                SELECT 1 FROM LichSuBaoTri bt 
                WHERE bt.CSVCID = lb.CSVCID 
                AND bt.NgayYeuCau >= DATEADD(DAY, -30, GETDATE())
                AND bt.TrangThai IN (N'Chờ xử lý', N'Đang xử lý')
            );
        
        -- Tạo yêu cầu bảo trì cho từng CSVC
        DECLARE cur_baotri CURSOR FOR
        SELECT CSVCID, LichID, NgayKeTiep, TenCSVC FROM #CSVCCanBaoTri;
        
        DECLARE @CSVCID INT, @LichID INT, @NgayKeTiep DATE, @TenCSVC NVARCHAR(100);
        
        OPEN cur_baotri;
        FETCH NEXT FROM cur_baotri INTO @CSVCID, @LichID, @NgayKeTiep, @TenCSVC;
        
        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Tạo yêu cầu bảo trì định kỳ
            INSERT INTO LichSuBaoTri (CSVCID, NgayYeuCau, NoiDung, TrangThai)
            VALUES (
                @CSVCID, 
                GETDATE(), 
                N'Bảo trì định kỳ cho ' + @TenCSVC + N' (Hạn: ' + CONVERT(NVARCHAR(10), @NgayKeTiep, 103) + N')',
                N'Chờ xử lý'
            );
            
            -- Cập nhật ngày bắt đầu chu kỳ tiếp theo
            UPDATE LichBaoTri
            SET NgayBatDau = @NgayKeTiep
            WHERE LichID = @LichID;
            
            SET @Count = @Count + 1;
            
            FETCH NEXT FROM cur_baotri INTO @CSVCID, @LichID, @NgayKeTiep, @TenCSVC;
        END
        
        CLOSE cur_baotri;
        DEALLOCATE cur_baotri;
        
        DROP TABLE #CSVCCanBaoTri;
        
        PRINT N'Đã tạo ' + CAST(@Count AS NVARCHAR(10)) + N' yêu cầu bảo trì định kỳ!';
        
    END TRY
    BEGIN CATCH
        IF CURSOR_STATUS('global', 'cur_baotri') >= 0
        BEGIN
            CLOSE cur_baotri;
            DEALLOCATE cur_baotri;
        END
        
        IF OBJECT_ID('tempdb..#CSVCCanBaoTri') IS NOT NULL
            DROP TABLE #CSVCCanBaoTri;
            
        THROW;
    END CATCH
END
GO

-- 5. LẤY DANH SÁCH CSVC SẮP ĐẾN HẠN BẢO TRÌ
CREATE OR ALTER PROCEDURE sp_GetUpcomingMaintenance
(
    @SoNgayTiepTheo INT = 30
)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        lb.LichID,
        lb.CSVCID,
        c.TenCSVC,
        c.MaCSVC,
        lb.ChuKyBaoTri_Thang,
        lb.NgayBatDau,
        lb.NgayKeTiep,
        DATEDIFF(DAY, GETDATE(), lb.NgayKeTiep) AS SoNgayConLai,
        lb.GhiChu,
        -- Lần bảo trì gần nhất
        (SELECT TOP 1 NgayHoanThanh 
         FROM LichSuBaoTri bt 
         WHERE bt.CSVCID = lb.CSVCID 
         AND bt.TrangThai = N'Hoàn thành'
         ORDER BY bt.NgayHoanThanh DESC) AS LanBaoTriGanNhat,
        -- Kiểm tra có yêu cầu đang chờ không
        CASE WHEN EXISTS (
            SELECT 1 FROM LichSuBaoTri bt 
            WHERE bt.CSVCID = lb.CSVCID 
            AND bt.TrangThai IN (N'Chờ xử lý', N'Đang xử lý')
        ) THEN N'Có yêu cầu đang chờ' ELSE N'Chưa có yêu cầu' END AS TrangThaiYeuCau
    FROM LichBaoTri lb
    INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID
    WHERE 
        c.TinhTrang != N'Đã thanh lý'
        AND lb.NgayKeTiep <= DATEADD(DAY, @SoNgayTiepTheo, GETDATE())
    ORDER BY lb.NgayKeTiep ASC;
END
GO