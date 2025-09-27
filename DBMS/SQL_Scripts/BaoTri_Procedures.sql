USE vc;
GO

-- =============================================
-- STORED PROCEDURES CHO QUẢN LÝ BẢO TRÌ
-- =============================================

-- 1. THÊM YÊU CẦU BẢO TRÌ MỚI
CREATE OR ALTER PROCEDURE sp_AddMaintenanceRequest
(
    @CSVCID INT,
    @NoiDung NVARCHAR(500),
    @NhanVienGiamSatID INT = NULL,
    @NhanVienKyThuatID INT = NULL,
    @ChiPhiDuKien DECIMAL(18,2) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra CSVC tồn tại và chưa bị thanh lý
        IF NOT EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID AND TinhTrang != N'Đã thanh lý')
        BEGIN
            RAISERROR(N'CSVC không tồn tại hoặc đã bị thanh lý!', 16, 1);
            RETURN;
        END
        
        -- Thêm yêu cầu bảo trì với ngày hiện tại
        INSERT INTO LichSuBaoTri (CSVCID, NgayYeuCau, NoiDung, ChiPhi, NhanVienGiamSatID, NhanVienKyThuatID, TrangThai)
        VALUES (@CSVCID, GETDATE(), @NoiDung, @ChiPhiDuKien, @NhanVienGiamSatID, @NhanVienKyThuatID, N'Chờ xử lý');
        
        -- Cập nhật trạng thái CSVC thành "Bảo trì"
        UPDATE CSVC 
        SET TinhTrang = N'Bảo trì'
        WHERE CSVCID = @CSVCID;
        
        PRINT N'Đã tạo yêu cầu bảo trì thành công!';
        
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- LẤY DANH SÁCH CSVC CÓ THỂ BẢO TRÌ (HỖ TRỢ CHO COMBOBOX)
CREATE OR ALTER PROCEDURE sp_GetAvailableCSVCForMaintenance
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CSVCID,
        c.TenCSVC,
        c.MaCSVC,
        c.TinhTrang
    FROM CSVC c
    WHERE c.TinhTrang NOT IN (N'Đã thanh lý')  -- Loại bỏ CSVC đã thanh lý, cho phép CSVC "Hỏng" được bảo trì
    AND NOT EXISTS (
        SELECT 1 
        FROM LichSuBaoTri bt 
        WHERE bt.CSVCID = c.CSVCID 
        AND bt.TrangThai IN (N'Chờ xử lý', N'Đang xử lý')  -- Loại bỏ CSVC đang có yêu cầu bảo trì chưa hoàn thành
    )
    ORDER BY c.TenCSVC;
END
GO

-- 2. CẬP NHẬT TRẠNG THÁI BẢO TRÌ
CREATE OR ALTER PROCEDURE sp_UpdateMaintenanceStatus
(
    @BaoTriID INT,
    @TrangThai NVARCHAR(50),
    @NgayHoanThanh DATE = NULL,
    @ChiPhiThucTe DECIMAL(18,2) = NULL,
    @GhiChuCapNhat NVARCHAR(500) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @CSVCID INT;
        DECLARE @TrangThaiCu NVARCHAR(50);
        
        -- Lấy thông tin hiện tại
        SELECT @CSVCID = CSVCID, @TrangThaiCu = TrangThai
        FROM LichSuBaoTri 
        WHERE BaoTriID = @BaoTriID;
        -- Cập nhật trạng thái bảo trì
        UPDATE LichSuBaoTri
        SET TrangThai = @TrangThai,
            NgayHoanThanh = CASE WHEN @TrangThai = N'Hoàn thành' THEN ISNULL(@NgayHoanThanh, GETDATE()) ELSE NgayHoanThanh END,
            ChiPhi = ISNULL(@ChiPhiThucTe, ChiPhi),
            NoiDung = CASE WHEN @GhiChuCapNhat IS NOT NULL THEN NoiDung + N' | Cập nhật: ' + @GhiChuCapNhat ELSE NoiDung END
        WHERE BaoTriID = @BaoTriID; 
        

        END
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- 3. LẤY DANH SÁCH CSVC CÓ TRONG LỊCH SỬ BẢO TRÌ (CHO FORM TÌM KIẾM)
CREATE OR ALTER PROCEDURE sp_GetCSVCInMaintenanceHistory
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT
        c.CSVCID,
        c.TenCSVC,
        c.MaCSVC
    FROM CSVC c
    INNER JOIN LichSuBaoTri bt ON c.CSVCID = bt.CSVCID
    ORDER BY c.TenCSVC;
END
GO

-- 4. TÌM KIẾM LỊCH SỬ BẢO TRÌ
CREATE OR ALTER PROCEDURE sp_SearchMaintenanceHistory
(
    @TenCSVC NVARCHAR(100) = NULL,
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.TenCSVC,
        bt.TrangThai,
        DATEDIFF(DAY, bt.NgayYeuCau, ISNULL(bt.NgayHoanThanh, GETDATE())) + 1 AS SoNgayXuLy
    FROM LichSuBaoTri bt
    INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID
    WHERE 
        (@TenCSVC IS NULL OR c.TenCSVC LIKE '%' + @TenCSVC + '%')
        AND (@TuNgay IS NULL OR bt.NgayYeuCau >= @TuNgay)
        AND (@DenNgay IS NULL OR bt.NgayYeuCau <= @DenNgay)
    ORDER BY bt.NgayYeuCau DESC;
END
GO

