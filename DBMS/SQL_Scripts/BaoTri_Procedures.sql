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
    @ChiPhiDuKien DECIMAL(18,2) = NULL,
    @NgayYeuCau DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra input
    IF @NgayYeuCau IS NULL
        SET @NgayYeuCau = GETDATE();
    
    BEGIN TRY
        -- Kiểm tra CSVC tồn tại và chưa bị thanh lý
        IF NOT EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID AND TinhTrang != N'Đã thanh lý')
        BEGIN
            RAISERROR(N'CSVC không tồn tại hoặc đã bị thanh lý!', 16, 1);
            RETURN;
        END
        
        -- Thêm yêu cầu bảo trì
        INSERT INTO LichSuBaoTri (CSVCID, NgayYeuCau, NoiDung, ChiPhi, NhanVienGiamSatID, NhanVienKyThuatID, TrangThai)
        VALUES (@CSVCID, @NgayYeuCau, @NoiDung, @ChiPhiDuKien, @NhanVienGiamSatID, @NhanVienKyThuatID, N'Chờ xử lý');
        
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
        
        IF @CSVCID IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy yêu cầu bảo trì!', 16, 1);
            RETURN;
        END
        
        -- Cập nhật trạng thái bảo trì
        UPDATE LichSuBaoTri
        SET TrangThai = @TrangThai,
            NgayHoanThanh = CASE WHEN @TrangThai = N'Hoàn thành' THEN ISNULL(@NgayHoanThanh, GETDATE()) ELSE NgayHoanThanh END,
            ChiPhi = ISNULL(@ChiPhiThucTe, ChiPhi),
            NoiDung = CASE WHEN @GhiChuCapNhat IS NOT NULL THEN NoiDung + N' | Cập nhật: ' + @GhiChuCapNhat ELSE NoiDung END
        WHERE BaoTriID = @BaoTriID;
        
        -- Cập nhật trạng thái CSVC
        IF @TrangThai = N'Hoàn thành'
        BEGIN
            UPDATE CSVC 
            SET TinhTrang = N'Đang sử dụng'
            WHERE CSVCID = @CSVCID;
        END
        ELSE IF @TrangThai = N'Hủy bỏ'
        BEGIN
            -- Trả về trạng thái cũ nếu hủy bỏ
            UPDATE CSVC 
            SET TinhTrang = N'Đang sử dụng'
            WHERE CSVCID = @CSVCID;
        END
        
        PRINT N'Đã cập nhật trạng thái bảo trì thành công!';
        
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- 3. TÌM KIẾM LỊCH SỬ BẢO TRÌ
CREATE OR ALTER PROCEDURE sp_SearchMaintenanceHistory
(
    @CSVCID INT = NULL,
    @TrangThai NVARCHAR(50) = NULL,
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @NhanVienGiamSatID INT = NULL,
    @KeyWord NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        bt.BaoTriID,
        bt.CSVCID,
        c.TenCSVC,
        c.MaCSVC,
        bt.NgayYeuCau,
        bt.NgayHoanThanh,
        bt.NoiDung,
        bt.ChiPhi,
        bt.TrangThai,
        nv1.HoTen AS NhanVienGiamSat,
        nv2.HoTen AS NhanVienKyThuat,
        DATEDIFF(DAY, bt.NgayYeuCau, ISNULL(bt.NgayHoanThanh, GETDATE())) AS SoNgayXuLy
    FROM LichSuBaoTri bt
    INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID
    LEFT JOIN NhanVien nv1 ON bt.NhanVienGiamSatID = nv1.NhanVienID
    LEFT JOIN NhanVien nv2 ON bt.NhanVienKyThuatID = nv2.NhanVienID
    WHERE 
        (@CSVCID IS NULL OR bt.CSVCID = @CSVCID)
        AND (@TrangThai IS NULL OR bt.TrangThai = @TrangThai)
        AND (@TuNgay IS NULL OR bt.NgayYeuCau >= @TuNgay)
        AND (@DenNgay IS NULL OR bt.NgayYeuCau <= @DenNgay)
        AND (@NhanVienGiamSatID IS NULL OR bt.NhanVienGiamSatID = @NhanVienGiamSatID)
        AND (@KeyWord IS NULL OR bt.NoiDung LIKE '%' + @KeyWord + '%' OR c.TenCSVC LIKE '%' + @KeyWord + '%')
    ORDER BY bt.NgayYeuCau DESC;
END
GO

-- 4. LẤY THỐNG KÊ BẢO TRÌ THEO THỜI GIAN
CREATE OR ALTER PROCEDURE sp_GetMaintenanceStatsByPeriod
(
    @TuNgay DATE,
    @DenNgay DATE,
    @LoaiThongKe NVARCHAR(20) = N'TONG_HOP' -- TONG_HOP, THEO_THANG, THEO_CSVC
)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @LoaiThongKe = N'TONG_HOP'
    BEGIN
        SELECT 
            COUNT(*) AS TongSoYeuCau,
            COUNT(CASE WHEN TrangThai = N'Hoàn thành' THEN 1 END) AS DaHoanThanh,
            COUNT(CASE WHEN TrangThai = N'Đang xử lý' THEN 1 END) AS DangXuLy,
            COUNT(CASE WHEN TrangThai = N'Chờ xử lý' THEN 1 END) AS ChoXuLy,
            SUM(ISNULL(ChiPhi, 0)) AS TongChiPhi,
            AVG(CASE WHEN NgayHoanThanh IS NOT NULL THEN DATEDIFF(DAY, NgayYeuCau, NgayHoanThanh) END) AS TrungBinhNgayXuLy
        FROM LichSuBaoTri
        WHERE NgayYeuCau BETWEEN @TuNgay AND @DenNgay;
    END
    ELSE IF @LoaiThongKe = N'THEO_THANG'
    BEGIN
        SELECT 
            YEAR(NgayYeuCau) AS Nam,
            MONTH(NgayYeuCau) AS Thang,
            COUNT(*) AS SoYeuCau,
            SUM(ISNULL(ChiPhi, 0)) AS TongChiPhi,
            AVG(CASE WHEN NgayHoanThanh IS NOT NULL THEN DATEDIFF(DAY, NgayYeuCau, NgayHoanThanh) END) AS TrungBinhNgayXuLy
        FROM LichSuBaoTri
        WHERE NgayYeuCau BETWEEN @TuNgay AND @DenNgay
        GROUP BY YEAR(NgayYeuCau), MONTH(NgayYeuCau)
        ORDER BY Nam, Thang;
    END
    ELSE IF @LoaiThongKe = N'THEO_CSVC'
    BEGIN
        SELECT 
            c.CSVCID,
            c.TenCSVC,
            c.MaCSVC,
            COUNT(bt.BaoTriID) AS SoLanBaoTri,
            SUM(ISNULL(bt.ChiPhi, 0)) AS TongChiPhi,
            MAX(bt.NgayYeuCau) AS LanBaoTriGanNhat
        FROM CSVC c
        LEFT JOIN LichSuBaoTri bt ON c.CSVCID = bt.CSVCID 
            AND bt.NgayYeuCau BETWEEN @TuNgay AND @DenNgay
        GROUP BY c.CSVCID, c.TenCSVC, c.MaCSVC
        HAVING COUNT(bt.BaoTriID) > 0
        ORDER BY SoLanBaoTri DESC;
    END
END
GO