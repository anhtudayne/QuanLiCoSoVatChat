USE vc;
GO

-- =============================================
-- VIEWS CHO QUẢN LÝ CƠ SỞ VẬT CHẤT
-- =============================================

-- 1. VIEW TỔNG QUAN CSVC (KÈM LOẠI, VỊ TRÍ, TRẠNG THÁI)
CREATE OR ALTER VIEW vw_CSVCOverview
AS
SELECT 
    -- Thông tin cơ bản CSVC
    c.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    c.GiaTri,
    c.TinhTrang,
    c.GhiChu,
    
    -- Thông tin loại CSVC
    c.LoaiID,
    ISNULL(l.TenLoai, N'Chưa phân loại') AS TenLoai,
    l.MoTa AS MoTaLoai,
    
    -- Thông tin vị trí
    c.ViTriID,
    CASE 
        WHEN v.ViTriID IS NOT NULL 
        THEN v.KhuVuc + N' - Tầng ' + CAST(v.Tang AS NVARCHAR(5))
        ELSE N'Chưa xác định vị trí'
    END AS ViTri,
    v.Tang,
    v.KhuVuc,
    v.MoTa AS MoTaViTri,
    
    -- Thông tin nhà cung cấp
    c.TenNhaCungCap,
    c.SoDienThoaiNCC,
    c.EmailNCC,
    
    -- Thông tin sử dụng và bảo hành
    t.NgayMua,
    t.NgayHetBaoHanh,
    t.ThoiGianSuDungDuKien_Thang,
    t.GhiChu AS GhiChuSuDung,
    
    -- Tính toán thời gian sử dụng
    CASE 
        WHEN t.NgayMua IS NOT NULL 
        THEN DATEDIFF(MONTH, t.NgayMua, GETDATE())
        ELSE NULL 
    END AS SoThangDaSuDung,
    
    -- Tình trạng bảo hành
    CASE 
        WHEN t.NgayHetBaoHanh IS NULL THEN N'Không có thông tin'
        WHEN t.NgayHetBaoHanh >= GETDATE() THEN N'Còn bảo hành'
        ELSE N'Hết bảo hành'
    END AS TinhTrangBaoHanh,
    
    -- Số ngày còn lại của bảo hành
    CASE 
        WHEN t.NgayHetBaoHanh IS NOT NULL AND t.NgayHetBaoHanh >= GETDATE()
        THEN DATEDIFF(DAY, GETDATE(), t.NgayHetBaoHanh)
        ELSE 0
    END AS SoNgayConBaoHanh,
    
    -- Trạng thái thanh lý
    CASE 
        WHEN tl.ThanhLyID IS NOT NULL THEN N'Đã thanh lý'
        ELSE N'Chưa thanh lý'
    END AS TrangThaiThanhLy,
    
    tl.NgayThanhLy,
    tl.LyDoThanhLy,
    tl.GiaTriThanhLy

FROM CSVC c
    LEFT JOIN LoaiCSVC l ON c.LoaiID = l.LoaiID
    LEFT JOIN ViTri v ON c.ViTriID = v.ViTriID
    LEFT JOIN ThongTinSuDung t ON c.CSVCID = t.CSVCID
    LEFT JOIN ThanhLyCSVC tl ON c.CSVCID = tl.CSVCID;
GO

