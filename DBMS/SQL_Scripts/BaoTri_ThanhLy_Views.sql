USE vc;
GO

-- =============================================
-- VIEWS BÁO CÁO CHO BẢO TRÌ & THANH LÝ
-- =============================================

-- 1. VIEW TỔNG QUAN BẢO TRÌ
CREATE OR ALTER VIEW vw_MaintenanceOverview
AS
SELECT 
    bt.BaoTriID,
    bt.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    lc.TenLoai AS LoaiCSVC,
    vt.KhuVuc + N' - Tầng ' + CAST(vt.Tang AS NVARCHAR(5)) AS ViTri,
    bt.NgayYeuCau,
    bt.NgayHoanThanh,
    bt.NoiDung,
    bt.ChiPhi,
    bt.TrangThai,
    nv1.HoTen AS NhanVienGiamSat,
    nv2.HoTen AS NhanVienKyThuat,
    DATEDIFF(DAY, bt.NgayYeuCau, ISNULL(bt.NgayHoanThanh, GETDATE())) AS SoNgayXuLy,
    -- Thông tin bổ sung
    c.GiaTri AS GiaTriCSVC,
    CASE 
        WHEN c.GiaTri > 0 AND bt.ChiPhi > 0 
        THEN (bt.ChiPhi / c.GiaTri * 100)
        ELSE 0 
    END AS TyLeChiPhiBaoTri,
    -- Đánh giá mức độ ưu tiên
    CASE 
        WHEN bt.TrangThai = N'Chờ xử lý' AND DATEDIFF(DAY, bt.NgayYeuCau, GETDATE()) > 7 THEN N'Khẩn cấp'
        WHEN bt.TrangThai = N'Đang xử lý' AND DATEDIFF(DAY, bt.NgayYeuCau, GETDATE()) > 14 THEN N'Cần chú ý'
        WHEN bt.TrangThai = N'Chờ xử lý' THEN N'Bình thường'
        ELSE N'Đã xử lý'
    END AS MucDoUuTien
FROM LichSuBaoTri bt
INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID
LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
LEFT JOIN NhanVien nv1 ON bt.NhanVienGiamSatID = nv1.NhanVienID
LEFT JOIN NhanVien nv2 ON bt.NhanVienKyThuatID = nv2.NhanVienID;
GO

-- 2. VIEW LỊCH BẢO TRÌ ĐỊNH KỲ
CREATE OR ALTER VIEW vw_MaintenanceSchedule
AS
SELECT 
    lb.LichID,
    lb.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    lc.TenLoai AS LoaiCSVC,
    vt.KhuVuc + N' - Tầng ' + CAST(vt.Tang AS NVARCHAR(5)) AS ViTri,
    lb.ChuKyBaoTri_Thang,
    lb.NgayBatDau,
    lb.NgayKeTiep,
    DATEDIFF(DAY, GETDATE(), lb.NgayKeTiep) AS SoNgayConLai,
    lb.GhiChu,
    -- Lần bảo trì gần nhất
    last_maintenance.NgayHoanThanh AS LanBaoTriGanNhat,
    last_maintenance.ChiPhi AS ChiPhiLanTruoc,
    -- Trạng thái hiện tại
    CASE 
        WHEN DATEDIFF(DAY, GETDATE(), lb.NgayKeTiep) < 0 THEN N'Quá hạn'
        WHEN DATEDIFF(DAY, GETDATE(), lb.NgayKeTiep) <= 7 THEN N'Sắp đến hạn'
        WHEN DATEDIFF(DAY, GETDATE(), lb.NgayKeTiep) <= 30 THEN N'Trong tháng'
        ELSE N'Chưa đến hạn'
    END AS TrangThaiHan,
    -- Kiểm tra có yêu cầu đang chờ không
    CASE 
        WHEN pending_request.SoYeuCauChoXuLy > 0 THEN N'Có yêu cầu đang chờ'
        ELSE N'Chưa có yêu cầu'
    END AS TrangThaiYeuCau,
    c.TinhTrang AS TrangThaiCSVC
FROM LichBaoTri lb
INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID
LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
-- Lần bảo trì gần nhất
OUTER APPLY (
    SELECT TOP 1 NgayHoanThanh, ChiPhi
    FROM LichSuBaoTri bt 
    WHERE bt.CSVCID = lb.CSVCID 
    AND bt.TrangThai = N'Hoàn thành'
    ORDER BY bt.NgayHoanThanh DESC
) last_maintenance
-- Yêu cầu đang chờ xử lý
OUTER APPLY (
    SELECT COUNT(*) AS SoYeuCauChoXuLy
    FROM LichSuBaoTri bt 
    WHERE bt.CSVCID = lb.CSVCID 
    AND bt.TrangThai IN (N'Chờ xử lý', N'Đang xử lý')
) pending_request;
GO

-- 3. VIEW TỔNG QUAN THANH LÝ
CREATE OR ALTER VIEW vw_DisposalOverview
AS
SELECT 
    tl.ThanhLyID,
    tl.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    lc.TenLoai AS LoaiCSVC,
    vt.KhuVuc + N' - Tầng ' + CAST(vt.Tang AS NVARCHAR(5)) AS ViTri,
    tl.NgayThanhLy,
    tl.LyDoThanhLy,
    tl.GiaTriThanhLy,
    c.GiaTri AS GiaTriGoc,
    nv.HoTen AS NguoiThucHien,
    -- Thông tin sử dụng
    tsd.NgayMua,
    DATEDIFF(DAY, tsd.NgayMua, tl.NgayThanhLy) AS SoNgaySuDung,
    DATEDIFF(MONTH, tsd.NgayMua, tl.NgayThanhLy) AS SoThangSuDung,
    DATEDIFF(YEAR, tsd.NgayMua, tl.NgayThanhLy) AS SoNamSuDung,
    -- Tính toán khấu hao
    CASE 
        WHEN c.GiaTri > 0 AND tl.GiaTriThanhLy IS NOT NULL 
        THEN ((c.GiaTri - tl.GiaTriThanhLy) / c.GiaTri * 100)
        ELSE NULL 
    END AS TyLeKhauHao,
    c.GiaTri - ISNULL(tl.GiaTriThanhLy, 0) AS GiaTriKhauHao,
    -- Tổng chi phí bảo trì
    maintenance_cost.TongChiPhiBaoTri,
    maintenance_cost.SoLanBaoTri,
    -- TCO (Total Cost of Ownership)
    c.GiaTri + ISNULL(maintenance_cost.TongChiPhiBaoTri, 0) - ISNULL(tl.GiaTriThanhLy, 0) AS TCO,
    -- Đánh giá hiệu quả sử dụng
    CASE 
        WHEN DATEDIFF(MONTH, tsd.NgayMua, tl.NgayThanhLy) < 12 THEN N'Thanh lý sớm'
        WHEN DATEDIFF(MONTH, tsd.NgayMua, tl.NgayThanhLy) < 36 THEN N'Sử dụng ngắn hạn'
        WHEN DATEDIFF(MONTH, tsd.NgayMua, tl.NgayThanhLy) < 60 THEN N'Sử dụng trung hạn'
        ELSE N'Sử dụng dài hạn'
    END AS DanhGiaSuDung
FROM ThanhLyCSVC tl
INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
LEFT JOIN NhanVien nv ON tl.NguoiThucHienID = nv.NhanVienID
LEFT JOIN ThongTinSuDung tsd ON c.CSVCID = tsd.CSVCID
-- Tổng chi phí bảo trì
OUTER APPLY (
    SELECT 
        SUM(ISNULL(ChiPhi, 0)) AS TongChiPhiBaoTri,
        COUNT(*) AS SoLanBaoTri
    FROM LichSuBaoTri bt 
    WHERE bt.CSVCID = tl.CSVCID 
    AND bt.TrangThai = N'Hoàn thành'
) maintenance_cost;
GO

-- 4. VIEW DASHBOARD TỔNG HỢP
CREATE OR ALTER VIEW vw_AssetManagementDashboard
AS
SELECT 
    -- Thống kê CSVC
    (SELECT COUNT(*) FROM CSVC WHERE TinhTrang != N'Đã thanh lý') AS TongSoCSVC,
    (SELECT SUM(ISNULL(GiaTri, 0)) FROM CSVC WHERE TinhTrang != N'Đã thanh lý') AS TongGiaTriCSVC,
    
    -- Thống kê bảo trì trong tháng
    (SELECT COUNT(*) FROM LichSuBaoTri WHERE MONTH(NgayYeuCau) = MONTH(GETDATE()) AND YEAR(NgayYeuCau) = YEAR(GETDATE())) AS SoYeuCauBaoTriThangNay,
    (SELECT COUNT(*) FROM LichSuBaoTri WHERE TrangThai = N'Chờ xử lý') AS SoYeuCauChoXuLy,
    (SELECT COUNT(*) FROM LichSuBaoTri WHERE TrangThai = N'Đang xử lý') AS SoYeuCauDangXuLy,
    (SELECT SUM(ISNULL(ChiPhi, 0)) FROM LichSuBaoTri WHERE MONTH(NgayYeuCau) = MONTH(GETDATE()) AND YEAR(NgayYeuCau) = YEAR(GETDATE()) AND TrangThai = N'Hoàn thành') AS ChiPhiBaoTriThangNay,
    
    -- Thống kê bảo trì định kỳ
    (SELECT COUNT(*) FROM LichBaoTri lb INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID WHERE c.TinhTrang != N'Đã thanh lý') AS SoCSVCCoLichBaoTri,
    (SELECT COUNT(*) FROM LichBaoTri lb INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID WHERE c.TinhTrang != N'Đã thanh lý' AND lb.NgayKeTiep <= DATEADD(DAY, 30, GETDATE())) AS SoCSVCSapDenHanBaoTri,
    (SELECT COUNT(*) FROM LichBaoTri lb INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID WHERE c.TinhTrang != N'Đã thanh lý' AND lb.NgayKeTiep < GETDATE()) AS SoCSVCQuaHanBaoTri,
    
    -- Thống kê thanh lý
    (SELECT COUNT(*) FROM ThanhLyCSVC WHERE MONTH(NgayThanhLy) = MONTH(GETDATE()) AND YEAR(NgayThanhLy) = YEAR(GETDATE())) AS SoCSVCThanhLyThangNay,
    (SELECT COUNT(*) FROM CSVC WHERE TinhTrang = N'Đã thanh lý') AS TongSoCSVCDaThanhLy,
    (SELECT SUM(ISNULL(GiaTriThanhLy, 0)) FROM ThanhLyCSVC WHERE MONTH(NgayThanhLy) = MONTH(GETDATE()) AND YEAR(NgayThanhLy) = YEAR(GETDATE())) AS GiaTriThanhLyThangNay,
    
    -- Hiệu quả bảo trì
    dbo.fn_GetMaintenanceEfficiency(DATEADD(MONTH, -3, GETDATE()), GETDATE(), 7) AS HieuQuaBaoTri3ThangGanDay,
    
    -- Cảnh báo
    (SELECT COUNT(*) FROM MaintenanceAlerts WHERE IsResolved = 0) AS SoCanhBaoChuaXuLy;
GO

-- 5. VIEW BÁO CÁO CHI PHÍ THEO THỜI GIAN
CREATE OR ALTER VIEW vw_CostReportByPeriod
AS
SELECT 
    YEAR(bt.NgayYeuCau) AS Nam,
    MONTH(bt.NgayYeuCau) AS Thang,
    lc.TenLoai,
    COUNT(*) AS SoLanBaoTri,
    SUM(ISNULL(bt.ChiPhi, 0)) AS TongChiPhi,
    AVG(ISNULL(bt.ChiPhi, 0)) AS ChiPhiTrungBinh,
    MIN(ISNULL(bt.ChiPhi, 0)) AS ChiPhiThapNhat,
    MAX(ISNULL(bt.ChiPhi, 0)) AS ChiPhiCaoNhat,
    AVG(DATEDIFF(DAY, bt.NgayYeuCau, bt.NgayHoanThanh)) AS TrungBinhNgayXuLy
FROM LichSuBaoTri bt
INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID
LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
WHERE bt.TrangThai = N'Hoàn thành'
    AND bt.NgayYeuCau >= DATEADD(YEAR, -2, GETDATE()) -- 2 năm gần đây
GROUP BY YEAR(bt.NgayYeuCau), MONTH(bt.NgayYeuCau), lc.TenLoai;
GO

-- 6. VIEW TOP CSVC CẦN CHÚ Ý
CREATE OR ALTER VIEW vw_HighRiskAssets
AS
SELECT TOP 50
    c.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    lc.TenLoai,
    vt.KhuVuc + N' - Tầng ' + CAST(vt.Tang AS NVARCHAR(5)) AS ViTri,
    c.TinhTrang,
    dbo.fn_GetAssetRiskScore(c.CSVCID) AS DiemRuiRo,
    maintenance_stats.SoLanBaoTri12Thang,
    maintenance_stats.TongChiPhiBaoTri,
    dbo.fn_GetTotalCostOfOwnership(c.CSVCID) AS TCO,
    tsd.NgayMua,
    DATEDIFF(MONTH, tsd.NgayMua, GETDATE()) AS TuoiCSVC_Thang,
    last_maintenance.NgayBaoTriGanNhat,
    DATEDIFF(DAY, last_maintenance.NgayBaoTriGanNhat, GETDATE()) AS SoNgayTuBaoTriGanNhat,
    -- Đề xuất hành động
    CASE 
        WHEN dbo.fn_GetAssetRiskScore(c.CSVCID) >= 80 THEN N'Cần thay thế ngay'
        WHEN dbo.fn_GetAssetRiskScore(c.CSVCID) >= 60 THEN N'Cần đánh giá thay thế'
        WHEN dbo.fn_GetAssetRiskScore(c.CSVCID) >= 40 THEN N'Tăng cường bảo trì'
        ELSE N'Theo dõi thường xuyên'
    END AS DeXuatHanhDong
FROM CSVC c
LEFT JOIN LoaiCSVC lc ON c.LoaiID = lc.LoaiID
LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
LEFT JOIN ThongTinSuDung tsd ON c.CSVCID = tsd.CSVCID
-- Thống kê bảo trì 12 tháng gần đây
OUTER APPLY (
    SELECT 
        COUNT(*) AS SoLanBaoTri12Thang,
        SUM(ISNULL(ChiPhi, 0)) AS TongChiPhiBaoTri
    FROM LichSuBaoTri bt 
    WHERE bt.CSVCID = c.CSVCID 
    AND bt.NgayYeuCau >= DATEADD(MONTH, -12, GETDATE())
    AND bt.TrangThai = N'Hoàn thành'
) maintenance_stats
-- Lần bảo trì gần nhất
OUTER APPLY (
    SELECT TOP 1 NgayHoanThanh AS NgayBaoTriGanNhat
    FROM LichSuBaoTri bt 
    WHERE bt.CSVCID = c.CSVCID 
    AND bt.TrangThai = N'Hoàn thành'
    ORDER BY bt.NgayHoanThanh DESC
) last_maintenance
WHERE c.TinhTrang != N'Đã thanh lý'
ORDER BY dbo.fn_GetAssetRiskScore(c.CSVCID) DESC;
GO