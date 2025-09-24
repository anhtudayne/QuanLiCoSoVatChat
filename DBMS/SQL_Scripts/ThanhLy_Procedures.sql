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
    @NguoiThucHienID INT,
    @NgayThanhLy DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra input
        IF @NgayThanhLy IS NULL
            SET @NgayThanhLy = GETDATE();
        
        -- Kiểm tra CSVC tồn tại và chưa bị thanh lý
        IF NOT EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID)
        BEGIN
            RAISERROR(N'CSVC không tồn tại!', 16, 1);
            RETURN;
        END
        
        IF EXISTS (SELECT 1 FROM CSVC WHERE CSVCID = @CSVCID AND TinhTrang = N'Đã thanh lý')
        BEGIN
            RAISERROR(N'CSVC này đã được thanh lý trước đó!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra nhân viên tồn tại
        IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE NhanVienID = @NguoiThucHienID)
        BEGIN
            RAISERROR(N'Nhân viên thực hiện không tồn tại!', 16, 1);
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
        
        -- Thêm vào bảng thanh lý
        INSERT INTO ThanhLyCSVC (CSVCID, NgayThanhLy, LyDoThanhLy, GiaTriThanhLy, NguoiThucHienID)
        VALUES (@CSVCID, @NgayThanhLy, @LyDoThanhLy, @GiaTriThanhLy, @NguoiThucHienID);
        
        -- Cập nhật trạng thái CSVC
        UPDATE CSVC 
        SET TinhTrang = N'Đã thanh lý'
        WHERE CSVCID = @CSVCID;
        
        -- Xóa lịch bảo trì định kỳ nếu có
        DELETE FROM LichBaoTri WHERE CSVCID = @CSVCID;
        
        COMMIT TRANSACTION;
        
        PRINT N'Đã thanh lý CSVC thành công!';
        
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
        -- Kiểm tra thanh lý tồn tại
        IF NOT EXISTS (SELECT 1 FROM ThanhLyCSVC WHERE ThanhLyID = @ThanhLyID)
        BEGIN
            RAISERROR(N'Không tìm thấy thông tin thanh lý!', 16, 1);
            RETURN;
        END
        
        -- Cập nhật thông tin
        UPDATE ThanhLyCSVC
        SET 
            LyDoThanhLy = ISNULL(@LyDoThanhLy, LyDoThanhLy),
            GiaTriThanhLy = ISNULL(@GiaTriThanhLy, GiaTriThanhLy),
            NgayThanhLy = ISNULL(@NgayThanhLy, NgayThanhLy),
            NguoiThucHienID = ISNULL(@NguoiThucHienID, NguoiThucHienID)
        WHERE ThanhLyID = @ThanhLyID;
        
        PRINT N'Đã cập nhật thông tin thanh lý thành công!';
        
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- 3. HỦY THANH LÝ (KHÔI PHỤC CSVC)
CREATE OR ALTER PROCEDURE sp_CancelAssetDisposal
(
    @ThanhLyID INT,
    @LyDoHuy NVARCHAR(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @CSVCID INT;
        
        -- Lấy thông tin CSVC
        SELECT @CSVCID = CSVCID 
        FROM ThanhLyCSVC 
        WHERE ThanhLyID = @ThanhLyID;
        
        IF @CSVCID IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy thông tin thanh lý!', 16, 1);
            RETURN;
        END
        
        BEGIN TRANSACTION;
        
        -- Xóa thông tin thanh lý
        DELETE FROM ThanhLyCSVC WHERE ThanhLyID = @ThanhLyID;
        
        -- Khôi phục trạng thái CSVC
        UPDATE CSVC 
        SET TinhTrang = N'Đang sử dụng'
        WHERE CSVCID = @CSVCID;
        
        -- Ghi log vào lịch sử bảo trì
        INSERT INTO LichSuBaoTri (CSVCID, NgayYeuCau, NoiDung, TrangThai)
        VALUES (@CSVCID, GETDATE(), N'Khôi phục từ thanh lý. Lý do: ' + @LyDoHuy, N'Hoàn thành');
        
        COMMIT TRANSACTION;
        
        PRINT N'Đã hủy thanh lý và khôi phục CSVC thành công!';
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 4. TÌM KIẾM LỊCH SỬ THANH LÝ
CREATE OR ALTER PROCEDURE sp_SearchAssetDisposal
(
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @NguoiThucHienID INT = NULL,
    @KeyWord NVARCHAR(100) = NULL,
    @LoaiCSVCID INT = NULL,
    @ViTriID INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        tl.ThanhLyID,
        tl.CSVCID,
        c.TenCSVC,
        c.MaCSVC,
        lc.TenLoai,
        vt.KhuVuc + N' - Tầng ' + CAST(vt.Tang AS NVARCHAR(5)) AS ViTri,
        tl.NgayThanhLy,
        tl.LyDoThanhLy,
        tl.GiaTriThanhLy,
        c.GiaTri AS GiaTriGoc,
        CASE 
            WHEN c.GiaTri > 0 AND tl.GiaTriThanhLy IS NOT NULL 
            THEN ((c.GiaTri - tl.GiaTriThanhLy) / c.GiaTri * 100)
            ELSE NULL 
        END AS TyLeKhauHao,
        nv.HoTen AS NguoiThucHien,
        -- Thời gian sử dụng
        DATEDIFF(DAY, tsd.NgayMua, tl.NgayThanhLy) AS SoNgaySuDung,
        DATEDIFF(MONTH, tsd.NgayMua, tl.NgayThanhLy) AS SoThangSuDung
    FROM ThanhLyCSVC tl
    INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
    LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
    LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
    LEFT JOIN NhanVien nv ON tl.NguoiThucHienID = nv.NhanVienID
    LEFT JOIN ThongTinSuDung tsd ON c.CSVCID = tsd.CSVCID
    WHERE 
        (@TuNgay IS NULL OR tl.NgayThanhLy >= @TuNgay)
        AND (@DenNgay IS NULL OR tl.NgayThanhLy <= @DenNgay)
        AND (@NguoiThucHienID IS NULL OR tl.NguoiThucHienID = @NguoiThucHienID)
        AND (@LoaiCSVCID IS NULL OR c.LoaiID = @LoaiCSVCID)
        AND (@ViTriID IS NULL OR c.ViTriID = @ViTriID)
        AND (@KeyWord IS NULL OR 
             c.TenCSVC LIKE '%' + @KeyWord + '%' OR 
             c.MaCSVC LIKE '%' + @KeyWord + '%' OR
             tl.LyDoThanhLy LIKE '%' + @KeyWord + '%')
    ORDER BY tl.NgayThanhLy DESC;
END
GO

-- 5. LẤY THỐNG KÊ THANH LÝ
CREATE OR ALTER PROCEDURE sp_GetDisposalStatistics
(
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @LoaiThongKe NVARCHAR(20) = N'TONG_HOP' -- TONG_HOP, THEO_THANG, THEO_LOAI, THEO_VI_TRI
)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Thiết lập khoảng thời gian mặc định (1 năm gần nhất)
    IF @TuNgay IS NULL
        SET @TuNgay = DATEADD(YEAR, -1, GETDATE());
    IF @DenNgay IS NULL
        SET @DenNgay = GETDATE();
    
    IF @LoaiThongKe = N'TONG_HOP'
    BEGIN
        SELECT 
            COUNT(*) AS TongSoThanhLy,
            SUM(ISNULL(c.GiaTri, 0)) AS TongGiaTriGoc,
            SUM(ISNULL(tl.GiaTriThanhLy, 0)) AS TongGiaTriThanhLy,
            SUM(ISNULL(c.GiaTri, 0)) - SUM(ISNULL(tl.GiaTriThanhLy, 0)) AS TongGiaTriKhauHao,
            AVG(CASE WHEN tsd.NgayMua IS NOT NULL THEN DATEDIFF(DAY, tsd.NgayMua, tl.NgayThanhLy) END) AS TrungBinhNgaySuDung
        FROM ThanhLyCSVC tl
        INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
        LEFT JOIN ThongTinSuDung tsd ON c.CSVCID = tsd.CSVCID
        WHERE tl.NgayThanhLy BETWEEN @TuNgay AND @DenNgay;
    END
    ELSE IF @LoaiThongKe = N'THEO_THANG'
    BEGIN
        SELECT 
            YEAR(tl.NgayThanhLy) AS Nam,
            MONTH(tl.NgayThanhLy) AS Thang,
            COUNT(*) AS SoLuongThanhLy,
            SUM(ISNULL(c.GiaTri, 0)) AS TongGiaTriGoc,
            SUM(ISNULL(tl.GiaTriThanhLy, 0)) AS TongGiaTriThanhLy
        FROM ThanhLyCSVC tl
        INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
        WHERE tl.NgayThanhLy BETWEEN @TuNgay AND @DenNgay
        GROUP BY YEAR(tl.NgayThanhLy), MONTH(tl.NgayThanhLy)
        ORDER BY Nam, Thang;
    END
    ELSE IF @LoaiThongKe = N'THEO_LOAI'
    BEGIN
        SELECT 
            lc.LoaiID,
            lc.TenLoai,
            COUNT(*) AS SoLuongThanhLy,
            SUM(ISNULL(c.GiaTri, 0)) AS TongGiaTriGoc,
            SUM(ISNULL(tl.GiaTriThanhLy, 0)) AS TongGiaTriThanhLy,
            AVG(CASE WHEN tsd.NgayMua IS NOT NULL THEN DATEDIFF(DAY, tsd.NgayMua, tl.NgayThanhLy) END) AS TrungBinhNgaySuDung
        FROM ThanhLyCSVC tl
        INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
        LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
        LEFT JOIN ThongTinSuDung tsd ON c.CSVCID = tsd.CSVCID
        WHERE tl.NgayThanhLy BETWEEN @TuNgay AND @DenNgay
        GROUP BY lc.LoaiID, lc.TenLoai
        ORDER BY SoLuongThanhLy DESC;
    END
    ELSE IF @LoaiThongKe = N'THEO_VI_TRI'
    BEGIN
        SELECT 
            vt.ViTriID,
            vt.KhuVuc + N' - Tầng ' + CAST(vt.Tang AS NVARCHAR(5)) AS ViTri,
            COUNT(*) AS SoLuongThanhLy,
            SUM(ISNULL(c.GiaTri, 0)) AS TongGiaTriGoc,
            SUM(ISNULL(tl.GiaTriThanhLy, 0)) AS TongGiaTriThanhLy
        FROM ThanhLyCSVC tl
        INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
        LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
        WHERE tl.NgayThanhLy BETWEEN @TuNgay AND @DenNgay
        GROUP BY vt.ViTriID, vt.KhuVuc, vt.Tang
        ORDER BY SoLuongThanhLy DESC;
    END
END
GO