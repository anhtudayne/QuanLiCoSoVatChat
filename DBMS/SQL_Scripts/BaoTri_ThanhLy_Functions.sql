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
    @DenNgay DATE = NULL
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(18,2);
    
    SELECT @TotalCost = SUM(ISNULL(bt.ChiPhi, 0))
    FROM LichSuBaoTri bt
    WHERE 
        (@CSVCID IS NULL OR bt.CSVCID = @CSVCID)
        AND (@TuNgay IS NULL OR bt.NgayYeuCau >= @TuNgay)
        AND (@DenNgay IS NULL OR bt.NgayYeuCau <= @DenNgay)
        AND bt.TrangThai = N'Hoàn thành';
    
    RETURN ISNULL(@TotalCost, 0);
END
GO

-- 2. FUNCTION ĐẾM SỐ LẦN BẢO TRÌ
CREATE OR ALTER FUNCTION fn_GetMaintenanceCount
(
    @CSVCID INT = NULL,
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL
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
        AND (@DenNgay IS NULL OR bt.NgayYeuCau <= @DenNgay);
    
    RETURN ISNULL(@Count, 0);
END
GO
    RETURN ISNULL(@Count, 0);
END
GO

-- 8. FUNCTION TÍNH TỔNG GIÁ TRỊ THANH LÝ
CREATE OR ALTER FUNCTION fn_GetTotalDisposalValue()
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TotalValue DECIMAL(18,2);
    
    SELECT @TotalValue = SUM(ISNULL(GiaTriThanhLy, 0))
    FROM ThanhLyCSVC;
    
    RETURN ISNULL(@TotalValue, 0);
END
GO


-- 9. FUNCTION ĐẾM SỐ CSVC ĐÃ THANH LÝ
CREATE OR ALTER FUNCTION fn_GetDisposalCount()
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;

    SELECT @Count = COUNT(*)
    FROM ThanhLyCSVC;

    RETURN ISNULL(@Count, 0);
END
GO

