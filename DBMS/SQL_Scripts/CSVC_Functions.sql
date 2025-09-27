USE vc;
GO

-- =============================================
-- FUNCTIONS CHO QUẢN LÝ CƠ SỞ VẬT CHẤT
-- =============================================

-- 1. FUNCTION ĐẾM CSVC THEO LOẠI
CREATE OR ALTER FUNCTION fn_GetCSVCCountByType
(
    @LoaiID INT = NULL
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    IF @LoaiID IS NULL
    BEGIN
        -- Đếm tất cả CSVC
        SELECT @Count = COUNT(*)
        FROM CSVC
        WHERE TinhTrang != N'Đã thanh lý';
    END
    ELSE
    BEGIN
        -- Đếm CSVC theo loại cụ thể
        SELECT @Count = COUNT(*)
        FROM CSVC
        WHERE LoaiID = @LoaiID
          AND TinhTrang != N'Đã thanh lý';
    END
    
    RETURN ISNULL(@Count, 0);
END
GO

-- 2. FUNCTION ĐẾM CSVC THEO VỊ TRÍ
CREATE OR ALTER FUNCTION fn_GetCSVCCountByLocation
(
    @ViTriID INT = NULL
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    IF @ViTriID IS NULL
    BEGIN
        -- Đếm tất cả CSVC
        SELECT @Count = COUNT(*)
        FROM CSVC
        WHERE TinhTrang != N'Đã thanh lý';
    END
    ELSE
    BEGIN
        -- Đếm CSVC theo vị trí cụ thể
        SELECT @Count = COUNT(*)
        FROM CSVC
        WHERE ViTriID = @ViTriID
          AND TinhTrang != N'Đã thanh lý';
    END
    
    RETURN ISNULL(@Count, 0);
END
GO

-- 3. FUNCTION ĐẾM CSVC THEO TRẠNG THÁI
CREATE OR ALTER FUNCTION fn_GetCSVCCountByStatus
(
    @TinhTrang NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*)
    FROM CSVC
    WHERE TinhTrang = @TinhTrang;
    
    RETURN ISNULL(@Count, 0);
END
GO

-- 4. FUNCTION TÍNH TỔNG GIÁ TRỊ TÀI SẢN
CREATE OR ALTER FUNCTION fn_GetTotalAssetValue
(
    @LoaiID INT = NULL,
    @ViTriID INT = NULL,
    @TinhTrang NVARCHAR(50) = NULL,
    @ExcludeLiquidated BIT = 1
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TotalValue DECIMAL(18,2);
    
    SELECT @TotalValue = SUM(ISNULL(GiaTri, 0))
    FROM CSVC
    WHERE 
        (@LoaiID IS NULL OR LoaiID = @LoaiID)
        AND (@ViTriID IS NULL OR ViTriID = @ViTriID)
        AND (@TinhTrang IS NULL OR TinhTrang = @TinhTrang)
        AND (@ExcludeLiquidated = 0 OR TinhTrang != N'Đã thanh lý');
    
    RETURN ISNULL(@TotalValue, 0);
END
GO

-- 7. FUNCTION LẤY DANH SÁCH LOẠI CSVC CHO COMBOBOX
CREATE OR ALTER FUNCTION fn_GetCSVCTypes()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        LoaiID,
        TenLoai,
        TenLoai + N' (' + CAST(dbo.fn_GetCSVCCountByType(LoaiID) AS NVARCHAR(10)) + N')' AS DisplayText
    FROM LoaiCSVC
    WHERE EXISTS(SELECT 1 FROM CSVC WHERE LoaiID = LoaiCSVC.LoaiID)
);
GO

-- 8. FUNCTION LẤY DANH SÁCH VỊ TRÍ CHO COMBOBOX
CREATE OR ALTER FUNCTION fn_GetCSVCLocations()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        ViTriID,
        KhuVuc + N' - Tầng ' + CAST(Tang AS NVARCHAR(5)) AS TenViTri,
        KhuVuc + N' - Tầng ' + CAST(Tang AS NVARCHAR(5)) + 
        N' (' + CAST(dbo.fn_GetCSVCCountByLocation(ViTriID) AS NVARCHAR(10)) + N')' AS DisplayText
    FROM ViTri
    WHERE EXISTS(SELECT 1 FROM CSVC WHERE ViTriID = ViTri.ViTriID)
);
GO
-- Function lấy tình trạng CSVC cho combobox
CREATE OR ALTER FUNCTION fn_GetCSVCStatuses()
RETURNS TABLE
AS
RETURN
(
    SELECT TinhTrang, TinhTrang AS DisplayText
    FROM (VALUES 
        (N'Đang sử dụng'),
        (N'Bảo trì'),
        (N'Hỏng'),
        (N'Đã thanh lý')
    ) AS Statuses(TinhTrang)
);