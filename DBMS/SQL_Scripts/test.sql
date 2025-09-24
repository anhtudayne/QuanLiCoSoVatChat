
-- 2. VIEW CSVC CẦN BẢO TRÌ TRONG 30 NGÀY TỚI
CREATE OR ALTER VIEW vw_CSVCNeedMaintenance
AS
SELECT 
    c.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    l.TenLoai,
    v.KhuVuc + N' - Tầng ' + CAST(v.Tang AS NVARCHAR(5)) AS ViTri,
    c.TinhTrang,
    lb.NgayKeTiep AS NgayBaoTriDuKien,
    lb.ChuKyBaoTri_Thang,
    DATEDIFF(DAY, GETDATE(), lb.NgayKeTiep) AS SoNgayConLai,
    lb.GhiChu
FROM CSVC c
    INNER JOIN LichBaoTri lb ON c.CSVCID = lb.CSVCID
    LEFT JOIN LoaiCSVC l ON c.LoaiID = l.LoaiID
    LEFT JOIN ViTri v ON c.ViTriID = v.ViTriID
WHERE 
    c.TinhTrang != N'Đã thanh lý'
    AND lb.NgayKeTiep <= DATEADD(DAY, 30, GETDATE())
    AND lb.NgayKeTiep >= GETDATE();
GO

-- 3. VIEW THỐNG KÊ CSVC THEO LOẠI
CREATE OR ALTER VIEW vw_CSVCStatisticsByType
AS
SELECT 
    l.LoaiID,
    l.TenLoai,
    COUNT(c.CSVCID) AS SoLuongCSVC,
    SUM(CASE WHEN c.TinhTrang = N'Đang sử dụng' THEN 1 ELSE 0 END) AS DangSuDung,
    SUM(CASE WHEN c.TinhTrang = N'Bảo trì' THEN 1 ELSE 0 END) AS DangBaoTri,
    SUM(CASE WHEN c.TinhTrang = N'Hỏng' THEN 1 ELSE 0 END) AS Hỏng,
    SUM(CASE WHEN c.TinhTrang = N'Đã thanh lý' THEN 1 ELSE 0 END) AS DaThanhLy,
    SUM(ISNULL(c.GiaTri, 0)) AS TongGiaTri,
    AVG(ISNULL(c.GiaTri, 0)) AS GiaTriTrungBinh
FROM LoaiCSVC l
    LEFT JOIN CSVC c ON l.LoaiID = c.LoaiID
GROUP BY l.LoaiID, l.TenLoai;
GO

-- 4. VIEW THỐNG KÊ CSVC THEO VỊ TRÍ
CREATE OR ALTER VIEW vw_CSVCStatisticsByLocation
AS
SELECT 
    v.ViTriID,
    v.Tang,
    v.KhuVuc,
    v.KhuVuc + N' - Tầng ' + CAST(v.Tang AS NVARCHAR(5)) AS TenViTri,
    COUNT(c.CSVCID) AS SoLuongCSVC,
    SUM(CASE WHEN c.TinhTrang = N'Đang sử dụng' THEN 1 ELSE 0 END) AS DangSuDung,
    SUM(CASE WHEN c.TinhTrang = N'Bảo trì' THEN 1 ELSE 0 END) AS DangBaoTri,
    SUM(CASE WHEN c.TinhTrang = N'Hỏng' THEN 1 ELSE 0 END) AS Hỏng,
    SUM(CASE WHEN c.TinhTrang = N'Đã thanh lý' THEN 1 ELSE 0 END) AS DaThanhLy,
    SUM(ISNULL(c.GiaTri, 0)) AS TongGiaTri
FROM ViTri v
    LEFT JOIN CSVC c ON v.ViTriID = c.ViTriID
GROUP BY v.ViTriID, v.Tang, v.KhuVuc;
GO