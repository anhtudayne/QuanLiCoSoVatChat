USE vc;
GO

-- =============================================
-- FUNCTIONS THỐNG KÊ CHO BẢO TRÌ & THANH LÝ
-- =============================================

-- 1. FUNCTION TÍNH TỔNG CHI PHÍ BẢO TRÌ
CREATE OR ALTER FUNCTION fn_GetTotalMaintenanceCost
(
    @CSVCID INT = NULL,
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @LoaiID INT = NULL,
    @ViTriID INT = NULL
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(18,2);
    
    SELECT @TotalCost = SUM(ISNULL(bt.ChiPhi, 0))
    FROM LichSuBaoTri bt
    INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID
    WHERE 
        (@CSVCID IS NULL OR bt.CSVCID = @CSVCID)
        AND (@TuNgay IS NULL OR bt.NgayYeuCau >= @TuNgay)
        AND (@DenNgay IS NULL OR bt.NgayYeuCau <= @DenNgay)
        AND (@LoaiID IS NULL OR c.LoaiID = @LoaiID)
        AND (@ViTriID IS NULL OR c.ViTriID = @ViTriID)
        AND bt.TrangThai = N'Hoàn thành';
    
    RETURN ISNULL(@TotalCost, 0);
END
GO

-- 2. FUNCTION ĐẾM SỐ LẦN BẢO TRÌ
CREATE OR ALTER FUNCTION fn_GetMaintenanceCount
(
    @CSVCID INT = NULL,
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @TrangThai NVARCHAR(50) = NULL
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*)
    FROM LichSuBaoTri bt
    WHERE 
        (@CSVCID IS NULL OR bt.CSVCID = @CSVCID)
        AND (@TuNgay IS NULL OR bt.NgayYeuCau >= @TuNgay)
        AND (@DenNgay IS NULL OR bt.NgayYeuCau <= @DenNgay)
        AND (@TrangThai IS NULL OR bt.TrangThai = @TrangThai);
    
    RETURN ISNULL(@Count, 0);
END
GO

-- 3. FUNCTION TÍNH HIỆU QUẢ BẢO TRÌ (% hoàn thành đúng hạn)
CREATE OR ALTER FUNCTION fn_GetMaintenanceEfficiency
(
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @SoNgayHanChe INT = 7 -- Hoàn thành trong vòng 7 ngày được coi là đúng hạn
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @TotalCompleted INT;
    DECLARE @OnTimeCompleted INT;
    DECLARE @Efficiency DECIMAL(5,2);
    
    -- Thiết lập khoảng thời gian mặc định
    IF @TuNgay IS NULL
        SET @TuNgay = DATEADD(MONTH, -3, GETDATE());
    IF @DenNgay IS NULL
        SET @DenNgay = GETDATE();
    
    -- Đếm tổng số bảo trì hoàn thành
    SELECT @TotalCompleted = COUNT(*)
    FROM LichSuBaoTri
    WHERE TrangThai = N'Hoàn thành'
        AND NgayYeuCau BETWEEN @TuNgay AND @DenNgay
        AND NgayHoanThanh IS NOT NULL;
    
    -- Đếm số bảo trì hoàn thành đúng hạn
    SELECT @OnTimeCompleted = COUNT(*)
    FROM LichSuBaoTri
    WHERE TrangThai = N'Hoàn thành'
        AND NgayYeuCau BETWEEN @TuNgay AND @DenNgay
        AND NgayHoanThanh IS NOT NULL
        AND DATEDIFF(DAY, NgayYeuCau, NgayHoanThanh) <= @SoNgayHanChe;
    
    -- Tính hiệu quả
    IF @TotalCompleted > 0
        SET @Efficiency = CAST(@OnTimeCompleted AS DECIMAL(5,2)) / @TotalCompleted * 100;
    ELSE
        SET @Efficiency = 0;
    
    RETURN @Efficiency;
END
GO

-- 4. FUNCTION TÍNH CHI PHÍ BẢO TRÌ TRUNG BÌNH
CREATE OR ALTER FUNCTION fn_GetAverageMaintenanceCost
(
    @CSVCID INT = NULL,
    @LoaiID INT = NULL,
    @SoThangGanDay INT = 12
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @AvgCost DECIMAL(18,2);
    DECLARE @TuNgay DATE = DATEADD(MONTH, -@SoThangGanDay, GETDATE());
    
    SELECT @AvgCost = AVG(ISNULL(bt.ChiPhi, 0))
    FROM LichSuBaoTri bt
    INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID
    WHERE 
        (@CSVCID IS NULL OR bt.CSVCID = @CSVCID)
        AND (@LoaiID IS NULL OR c.LoaiID = @LoaiID)
        AND bt.NgayYeuCau >= @TuNgay
        AND bt.TrangThai = N'Hoàn thành'
        AND bt.ChiPhi > 0;
    
    RETURN ISNULL(@AvgCost, 0);
END
GO

-- 5. FUNCTION ĐÁNH GIÁ RỦI RO CSVC (dựa trên tần suất bảo trì)
CREATE OR ALTER FUNCTION fn_GetAssetRiskScore
(
    @CSVCID INT
)
RETURNS INT
AS
BEGIN
    DECLARE @RiskScore INT = 0;
    DECLARE @MaintenanceCount INT;
    DECLARE @TotalCost DECIMAL(18,2);
    DECLARE @MonthsSincePurchase INT;
    DECLARE @DaysSinceLastMaintenance INT;
    
    -- Đếm số lần bảo trì trong 12 tháng gần đây
    SELECT @MaintenanceCount = COUNT(*)
    FROM LichSuBaoTri
    WHERE CSVCID = @CSVCID
        AND NgayYeuCau >= DATEADD(MONTH, -12, GETDATE())
        AND TrangThai = N'Hoàn thành';
    
    -- Tính tổng chi phí bảo trì
    SELECT @TotalCost = SUM(ISNULL(ChiPhi, 0))
    FROM LichSuBaoTri
    WHERE CSVCID = @CSVCID
        AND TrangThai = N'Hoàn thành';
    
    -- Tính tuổi của CSVC
    SELECT @MonthsSincePurchase = DATEDIFF(MONTH, tsd.NgayMua, GETDATE())
    FROM ThongTinSuDung tsd
    WHERE tsd.CSVCID = @CSVCID;
    
    -- Tính số ngày từ lần bảo trì gần nhất
    SELECT @DaysSinceLastMaintenance = DATEDIFF(DAY, MAX(NgayHoanThanh), GETDATE())
    FROM LichSuBaoTri
    WHERE CSVCID = @CSVCID
        AND TrangThai = N'Hoàn thành'
        AND NgayHoanThanh IS NOT NULL;
    
    -- Tính điểm rủi ro (0-100)
    -- Tần suất bảo trì cao = rủi ro cao
    SET @RiskScore = @RiskScore + (@MaintenanceCount * 10);
    
    -- Chi phí bảo trì cao = rủi ro cao
    IF @TotalCost > 10000000 -- 10 triệu
        SET @RiskScore = @RiskScore + 20;
    ELSE IF @TotalCost > 5000000 -- 5 triệu
        SET @RiskScore = @RiskScore + 10;
    
    -- Tuổi cao = rủi ro cao
    IF @MonthsSincePurchase > 60 -- > 5 năm
        SET @RiskScore = @RiskScore + 25;
    ELSE IF @MonthsSincePurchase > 36 -- > 3 năm
        SET @RiskScore = @RiskScore + 15;
    
    -- Lâu không bảo trì = rủi ro cao
    IF @DaysSinceLastMaintenance > 365 -- > 1 năm
        SET @RiskScore = @RiskScore + 30;
    ELSE IF @DaysSinceLastMaintenance > 180 -- > 6 tháng
        SET @RiskScore = @RiskScore + 15;
    
    -- Giới hạn điểm từ 0-100
    IF @RiskScore > 100
        SET @RiskScore = 100;
    
    RETURN @RiskScore;
END
GO

-- 6. FUNCTION TÍNH TỶ LỆ KHẤU HAO CSVC
CREATE OR ALTER FUNCTION fn_GetDepreciationRate
(
    @CSVCID INT
)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @DepreciationRate DECIMAL(5,2) = 0;
    DECLARE @OriginalValue DECIMAL(18,2);
    DECLARE @DisposalValue DECIMAL(18,2);
    
    -- Lấy giá trị gốc và giá trị thanh lý
    SELECT 
        @OriginalValue = c.GiaTri,
        @DisposalValue = tl.GiaTriThanhLy
    FROM CSVC c
    LEFT JOIN ThanhLyCSVC tl ON c.CSVCID = tl.CSVCID
    WHERE c.CSVCID = @CSVCID;
    
    -- Tính tỷ lệ khấu hao
    IF @OriginalValue > 0 AND @DisposalValue IS NOT NULL
    BEGIN
        SET @DepreciationRate = ((@OriginalValue - @DisposalValue) / @OriginalValue) * 100;
    END
    
    RETURN @DepreciationRate;
END
GO

-- 7. FUNCTION LẤY DANH SÁCH TRẠNG THÁI BẢO TRÌ CHO COMBOBOX
CREATE OR ALTER FUNCTION fn_GetMaintenanceStatuses()
RETURNS TABLE
AS
RETURN
(
    SELECT DISTINCT
        TrangThai,
        TrangThai AS DisplayText,
        COUNT(*) OVER (PARTITION BY TrangThai) AS SoLuong
    FROM LichSuBaoTri
    WHERE TrangThai IS NOT NULL
);
GO

-- 8. FUNCTION TÍNH TỔNG GIÁ TRỊ THANH LÝ
CREATE OR ALTER FUNCTION fn_GetTotalDisposalValue
(
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @LoaiID INT = NULL,
    @ViTriID INT = NULL
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TotalValue DECIMAL(18,2);
    
    SELECT @TotalValue = SUM(ISNULL(tl.GiaTriThanhLy, 0))
    FROM ThanhLyCSVC tl
    INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
    WHERE 
        (@TuNgay IS NULL OR tl.NgayThanhLy >= @TuNgay)
        AND (@DenNgay IS NULL OR tl.NgayThanhLy <= @DenNgay)
        AND (@LoaiID IS NULL OR c.LoaiID = @LoaiID)
        AND (@ViTriID IS NULL OR c.ViTriID = @ViTriID);
    
    RETURN ISNULL(@TotalValue, 0);
END
GO

-- 9. FUNCTION ĐẾM SỐ CSVC THANH LÝ
CREATE OR ALTER FUNCTION fn_GetDisposalCount
(
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @LoaiID INT = NULL,
    @ViTriID INT = NULL
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*)
    FROM ThanhLyCSVC tl
    INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
    WHERE 
        (@TuNgay IS NULL OR tl.NgayThanhLy >= @TuNgay)
        AND (@DenNgay IS NULL OR tl.NgayThanhLy <= @DenNgay)
        AND (@LoaiID IS NULL OR c.LoaiID = @LoaiID)
        AND (@ViTriID IS NULL OR c.ViTriID = @ViTriID);
    
    RETURN ISNULL(@Count, 0);
END
GO

-- 10. FUNCTION TÍNH CHỈ SỐ TCO (Total Cost of Ownership)
CREATE OR ALTER FUNCTION fn_GetTotalCostOfOwnership
(
    @CSVCID INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TCO DECIMAL(18,2) = 0;
    DECLARE @PurchasePrice DECIMAL(18,2);
    DECLARE @MaintenanceCost DECIMAL(18,2);
    DECLARE @DisposalValue DECIMAL(18,2);
    
    -- Giá mua
    SELECT @PurchasePrice = ISNULL(GiaTri, 0)
    FROM CSVC
    WHERE CSVCID = @CSVCID;
    
    -- Chi phí bảo trì
    SELECT @MaintenanceCost = SUM(ISNULL(ChiPhi, 0))
    FROM LichSuBaoTri
    WHERE CSVCID = @CSVCID
        AND TrangThai = N'Hoàn thành';
    
    -- Giá trị thanh lý (nếu có)
    SELECT @DisposalValue = ISNULL(GiaTriThanhLy, 0)
    FROM ThanhLyCSVC
    WHERE CSVCID = @CSVCID;
    
    -- TCO = Giá mua + Chi phí bảo trì - Giá trị thanh lý
    SET @TCO = @PurchasePrice + ISNULL(@MaintenanceCost, 0) - @DisposalValue;
    
    RETURN @TCO;
END
GO