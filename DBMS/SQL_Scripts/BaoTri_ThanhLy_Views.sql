USE vc;
GO

-- =============================================
-- VIEWS CHO BẢO TRÌ & THANH LÝ
-- =============================================

-- 1. VIEW CHO BẢO TRÌ
CREATE OR ALTER VIEW vw_BaoTri
AS
SELECT 
    bt.BaoTriID,
    bt.CSVCID,
    c.TenCSVC,
    bt.NgayYeuCau,
    bt.NgayHoanThanh,
    bt.NoiDung,
    bt.ChiPhi,
    bt.TrangThai
    
FROM LichSuBaoTri bt
INNER JOIN CSVC c ON bt.CSVCID = c.CSVCID;
GO

-- 2. VIEW CHO LỊCH BẢO TRÌ ĐỊNH KỲ
CREATE OR ALTER VIEW vw_LichBaoTriDinhKy
AS
SELECT 
    lb.LichID,
    lb.CSVCID,
    c.TenCSVC,
    lb.ChuKyBaoTri_Thang,
    lb.NgayBatDau,
    CASE 
        WHEN DATEDIFF(DAY, GETDATE(), lb.NgayBatDau) < 0 THEN N'Quá hạn'
        WHEN DATEDIFF(DAY, GETDATE(), lb.NgayBatDau) <= 7 THEN N'Sắp đến hạn'
        ELSE N'Chưa đến hạn'
    END AS TrangThai
FROM LichBaoTri lb
INNER JOIN CSVC c ON lb.CSVCID = c.CSVCID
WHERE c.TinhTrang != N'Đã thanh lý';
GO

-- 3. VIEW CHO THANH LÝ
CREATE OR ALTER VIEW vw_ThanhLy
AS
SELECT 
    tl.ThanhLyID,
    tl.CSVCID,
    c.TenCSVC,
    tl.NgayThanhLy,
    tl.LyDoThanhLy,
    tl.GiaTriThanhLy,
    c.GiaTri AS GiaTriGoc,
    tl.NguoiThucHienID,
    nv.HoTen AS TenNguoiThucHien,
    nv.ChucVu AS ChucVuNguoiThucHien
FROM ThanhLyCSVC tl
INNER JOIN CSVC c ON tl.CSVCID = c.CSVCID
LEFT JOIN NhanVien nv ON tl.NguoiThucHienID = nv.NhanVienID;
GO

-- 4. VIEW CHO CHỌN CSVC CẦN THANH LÝ
CREATE OR ALTER VIEW vw_CSVCForDisposal
AS
SELECT 
    c.CSVCID,
    c.TenCSVC,
    c.MaCSVC,
    l.TenLoai,
    c.GiaTri,
    c.TinhTrang,
    CONCAT(N'Tầng ', vt.Tang, N' - ', vt.KhuVuc) AS TenViTri,
    -- Ngày mua (nếu có)
    ts.NgayMua
FROM CSVC c
LEFT JOIN LoaiCSVC l ON c.LoaiID = l.LoaiID
LEFT JOIN ViTri vt ON c.ViTriID = vt.ViTriID
LEFT JOIN ThongTinSuDung ts ON c.CSVCID = ts.CSVCID
WHERE c.TinhTrang != N'Đã thanh lý' AND c.TinhTrang != N'Bảo trì'
  -- Không có trong danh sách đã thanh lý
  AND NOT EXISTS (SELECT 1 FROM ThanhLyCSVC tl WHERE tl.CSVCID = c.CSVCID)
  -- Không có yêu cầu bảo trì đang chờ xử lý hoặc đang thực hiện
  AND NOT EXISTS (
      SELECT 1 FROM LichSuBaoTri bt 
      WHERE bt.CSVCID = c.CSVCID 
      AND bt.TrangThai IN (N'Chờ xử lý', N'Đang xử lý')
  );
GO

