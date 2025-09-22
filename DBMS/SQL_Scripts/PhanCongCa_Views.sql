USE vc;
GO

-- =============================================
-- VIEWS CHO BÁO CÁO VÀ THỐNG KÊ PHÂN CÔNG CA
-- =============================================

-- 1. PROCEDURE CHI TIẾT PHÂN CÔNG CA (KẾT HỢP THÔNG TIN NHÂN VIÊN) - THAY THẾ VIEW
-- Đã chuyển thành procedure để có thể sử dụng ORDER BY
-- Sử dụng: EXEC sp_GetWorkShiftDetails

-- 2. VIEW THỐNG KÊ CA TRỰC THEO NHÂN VIÊN (THÁNG HIỆN TẠI)
CREATE OR ALTER VIEW vw_CurrentMonthEmployeeShiftStats
AS
SELECT 
    nv.NhanVienID,
    nv.HoTen,
    nv.ChucVu,
    nv.TrangThai,
    -- Thống kê ca trực tháng hiện tại
    COUNT(pc.PhanCongID) AS TongSoCaTrongThang,
    ISNULL(SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0), 0) AS TongGioLamTrongThang,
    ISNULL(AVG(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0), 0) AS TrungBinhGioMoiCa,
    -- Thống kê theo vai trò
    SUM(CASE WHEN pc.VaiTroTrongCa = N'Nhân viên trực' THEN 1 ELSE 0 END) AS SoCaNhanVienTruc,
    SUM(CASE WHEN pc.VaiTroTrongCa = N'Nhân viên kỹ thuật' THEN 1 ELSE 0 END) AS SoCaNhanVienKyThuat,
    SUM(CASE WHEN pc.VaiTroTrongCa = N'Trưởng ca' THEN 1 ELSE 0 END) AS SoCaTruongCa,
    -- Thống kê theo thời gian
    SUM(CASE WHEN pc.GioBatDau < '12:00' THEN 1 ELSE 0 END) AS SoCaSang,
    SUM(CASE WHEN pc.GioBatDau >= '12:00' AND pc.GioBatDau < '18:00' THEN 1 ELSE 0 END) AS SoCaChieu,
    SUM(CASE WHEN pc.GioBatDau >= '18:00' THEN 1 ELSE 0 END) AS SoCaToi,
    -- Lương tháng
    ISNULL(SUM(ttl.LuongCoBanTheoGio * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0)), 0) AS TongLuongThang
FROM NhanVien nv
LEFT JOIN PhanCongCa pc ON nv.NhanVienID = pc.NhanVienID 
    AND MONTH(pc.NgayLamViec) = MONTH(GETDATE())
    AND YEAR(pc.NgayLamViec) = YEAR(GETDATE())
LEFT JOIN ThongTinLuongNhanVien ttl ON nv.NhanVienID = ttl.NhanVienID
    AND ttl.Thang = MONTH(GETDATE())
    AND ttl.Nam = YEAR(GETDATE())
WHERE nv.TrangThai = N'Đang làm'
GROUP BY nv.NhanVienID, nv.HoTen, nv.ChucVu, nv.TrangThai;
GO

-- 3. VIEW LỊCH TRỰC THEO TUẦN
CREATE OR ALTER VIEW vw_WeeklySchedule
AS
SELECT 
    pc.NgayLamViec,
    DATENAME(WEEKDAY, pc.NgayLamViec) AS ThuTrongTuan,
    DATEPART(WEEK, pc.NgayLamViec) AS TuanTrongNam,
    pc.GioBatDau,
    pc.GioKetThuc,
    pc.TenCa,
    nv.HoTen,
    nv.ChucVu,
    pc.VaiTroTrongCa,
    pc.GhiChu,
    -- Phân loại ca
    CASE 
        WHEN pc.GioBatDau < '12:00' THEN N'Ca Sáng'
        WHEN pc.GioBatDau >= '12:00' AND pc.GioBatDau < '18:00' THEN N'Ca Chiều'
        ELSE N'Ca Tối'
    END AS LoaiCa,
    DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 AS SoGioLam
FROM PhanCongCa pc
INNER JOIN NhanVien nv ON pc.NhanVienID = nv.NhanVienID
WHERE pc.NgayLamViec BETWEEN 
    DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)) -- Thứ 2 tuần này
    AND 
    DATEADD(DAY, 7 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE)); -- Chủ nhật tuần này
GO

-- 4. VIEW BÁO CÁO TỔNG HỢP CA TRỰC THEO NGÀY
CREATE OR ALTER VIEW vw_DailyShiftSummary
AS
SELECT 
    pc.NgayLamViec,
    DATENAME(WEEKDAY, pc.NgayLamViec) AS ThuTrongTuan,
    -- Thống kê tổng quan
    COUNT(DISTINCT pc.NhanVienID) AS SoNhanVienLamViec,
    COUNT(pc.PhanCongID) AS TongSoCa,
    SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS TongGioLamViec,
    AVG(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS TrungBinhGioMoiCa,
    -- Thống kê theo loại ca
    SUM(CASE WHEN pc.GioBatDau < '12:00' THEN 1 ELSE 0 END) AS SoCaSang,
    SUM(CASE WHEN pc.GioBatDau >= '12:00' AND pc.GioBatDau < '18:00' THEN 1 ELSE 0 END) AS SoCaChieu,
    SUM(CASE WHEN pc.GioBatDau >= '18:00' THEN 1 ELSE 0 END) AS SoCaToi,
    -- Thống kê theo vai trò
    SUM(CASE WHEN pc.VaiTroTrongCa = N'Nhân viên trực' THEN 1 ELSE 0 END) AS SoNhanVienTruc,
    SUM(CASE WHEN pc.VaiTroTrongCa = N'Nhân viên kỹ thuật' THEN 1 ELSE 0 END) AS SoNhanVienKyThuat,
    SUM(CASE WHEN pc.VaiTroTrongCa = N'Trưởng ca' THEN 1 ELSE 0 END) AS SoTruongCa,
    -- Giờ làm việc sớm nhất và muộn nhất
    MIN(pc.GioBatDau) AS GioMoNhat,
    MAX(pc.GioKetThuc) AS GioDongCuaNhat,
    -- Tổng chi phí lương ngày
    SUM(ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0)) AS TongChiPhiLuong
FROM PhanCongCa pc
INNER JOIN NhanVien nv ON pc.NhanVienID = nv.NhanVienID
LEFT JOIN ThongTinLuongNhanVien ttl ON pc.NhanVienID = ttl.NhanVienID
    AND ttl.Thang = MONTH(pc.NgayLamViec)
    AND ttl.Nam = YEAR(pc.NgayLamViec)
GROUP BY pc.NgayLamViec;
GO

-- 5. VIEW THỐNG KÊ HIỆU SUẤT LÀM VIỆC THEO THÁNG
CREATE OR ALTER VIEW vw_MonthlyPerformanceStats
AS
SELECT 
    YEAR(pc.NgayLamViec) AS Nam,
    MONTH(pc.NgayLamViec) AS Thang,
    nv.NhanVienID,
    nv.HoTen,
    nv.ChucVu,
    -- Thống kê cơ bản
    COUNT(pc.PhanCongID) AS TongSoCa,
    SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS TongGioLam,
    AVG(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS TrungBinhGioMoiCa,
    -- Phân tích theo thời gian
    SUM(CASE WHEN DATEPART(WEEKDAY, pc.NgayLamViec) IN (1, 7) THEN 1 ELSE 0 END) AS SoCaCuoiTuan,
    SUM(CASE WHEN DATEPART(WEEKDAY, pc.NgayLamViec) NOT IN (1, 7) THEN 1 ELSE 0 END) AS SoCaThuong,
    -- Phân tích theo ca
    SUM(CASE WHEN pc.GioBatDau < '12:00' THEN DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 ELSE 0 END) AS GioLamCaSang,
    SUM(CASE WHEN pc.GioBatDau >= '12:00' AND pc.GioBatDau < '18:00' THEN DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 ELSE 0 END) AS GioLamCaChieu,
    SUM(CASE WHEN pc.GioBatDau >= '18:00' THEN DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 ELSE 0 END) AS GioLamCaToi,
    -- Tính lương
    SUM(ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0)) AS TongLuong,
    -- Đánh giá hiệu suất
    CASE 
        WHEN SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) >= 160 THEN N'Xuất sắc'
        WHEN SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) >= 120 THEN N'Tốt'
        WHEN SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) >= 80 THEN N'Trung bình'
        ELSE N'Cần cải thiện'
    END AS DanhGiaHieuSuat
FROM PhanCongCa pc
INNER JOIN NhanVien nv ON pc.NhanVienID = nv.NhanVienID
LEFT JOIN ThongTinLuongNhanVien ttl ON pc.NhanVienID = ttl.NhanVienID
    AND ttl.Thang = MONTH(pc.NgayLamViec)
    AND ttl.Nam = YEAR(pc.NgayLamViec)
GROUP BY YEAR(pc.NgayLamViec), MONTH(pc.NgayLamViec), nv.NhanVienID, nv.HoTen, nv.ChucVu;
GO

-- 6. VIEW PHÂN TÍCH XUNG ĐỘT CA TRỰC
CREATE OR ALTER VIEW vw_ShiftConflictAnalysis
AS
SELECT 
    pc1.NgayLamViec,
    pc1.NhanVienID AS NhanVien1ID,
    nv1.HoTen AS NhanVien1,
    pc1.GioBatDau AS GioBatDau1,
    pc1.GioKetThuc AS GioKetThuc1,
    pc2.NhanVienID AS NhanVien2ID,
    nv2.HoTen AS NhanVien2,
    pc2.GioBatDau AS GioBatDau2,
    pc2.GioKetThuc AS GioKetThuc2,
    CASE 
        WHEN pc1.GioBatDau = pc2.GioBatDau AND pc1.GioKetThuc = pc2.GioKetThuc THEN N'Trùng hoàn toàn'
        WHEN pc1.GioBatDau < pc2.GioKetThuc AND pc1.GioKetThuc > pc2.GioBatDau THEN N'Chồng chéo thời gian'
        ELSE N'Không xung đột'
    END AS LoaiXungDot,
    CASE 
        WHEN pc1.GioBatDau < pc2.GioKetThuc AND pc1.GioKetThuc > pc2.GioBatDau 
        THEN DATEDIFF(MINUTE, 
            CASE WHEN pc1.GioBatDau > pc2.GioBatDau THEN pc1.GioBatDau ELSE pc2.GioBatDau END,
            CASE WHEN pc1.GioKetThuc < pc2.GioKetThuc THEN pc1.GioKetThuc ELSE pc2.GioKetThuc END
        ) / 60.0
        ELSE 0 
    END AS SoGioChongCheo
FROM PhanCongCa pc1
INNER JOIN PhanCongCa pc2 ON pc1.NgayLamViec = pc2.NgayLamViec 
    AND pc1.PhanCongID < pc2.PhanCongID  -- Tránh duplicate
INNER JOIN NhanVien nv1 ON pc1.NhanVienID = nv1.NhanVienID
INNER JOIN NhanVien nv2 ON pc2.NhanVienID = nv2.NhanVienID
WHERE pc1.GioBatDau < pc2.GioKetThuc AND pc1.GioKetThuc > pc2.GioBatDau;  -- Chỉ lấy các ca có xung đột
GO

-- 7. VIEW TOP NHÂN VIÊN LÀM VIỆC CHĂM CHỈ NHẤT
CREATE OR ALTER VIEW vw_TopHardWorkingEmployees
AS
SELECT TOP 10
    nv.NhanVienID,
    nv.HoTen,
    nv.ChucVu,
    COUNT(pc.PhanCongID) AS TongSoCa_6Thang,
    SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS TongGioLam_6Thang,
    AVG(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS TrungBinhGioMoiCa,
    SUM(ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0)) AS TongLuong_6Thang,
    -- Tỷ lệ làm cuối tuần
    CAST(SUM(CASE WHEN DATEPART(WEEKDAY, pc.NgayLamViec) IN (1, 7) THEN 1 ELSE 0 END) AS FLOAT) / COUNT(pc.PhanCongID) * 100 AS TyLeLamCuoiTuan,
    -- Điểm đánh giá (có thể tùy chỉnh công thức)
    (SUM(DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) * 0.7) + 
    (COUNT(pc.PhanCongID) * 0.3) AS DiemDanhGia
FROM NhanVien nv
LEFT JOIN PhanCongCa pc ON nv.NhanVienID = pc.NhanVienID 
    AND pc.NgayLamViec >= DATEADD(MONTH, -6, GETDATE())  -- 6 tháng gần nhất
LEFT JOIN ThongTinLuongNhanVien ttl ON nv.NhanVienID = ttl.NhanVienID
    AND ttl.Thang = MONTH(pc.NgayLamViec)
    AND ttl.Nam = YEAR(pc.NgayLamViec)
WHERE nv.TrangThai = N'Đang làm'
GROUP BY nv.NhanVienID, nv.HoTen, nv.ChucVu
HAVING COUNT(pc.PhanCongID) > 0
ORDER BY DiemDanhGia DESC;
GO

-- 8. VIEW BÁO CÁO LƯƠNG CA TRỰC CHI TIẾT
CREATE OR ALTER VIEW vw_ShiftSalaryReport
AS
SELECT 
    pc.PhanCongID,
    pc.NgayLamViec,
    YEAR(pc.NgayLamViec) AS Nam,
    MONTH(pc.NgayLamViec) AS Thang,
    nv.NhanVienID,
    nv.HoTen,
    nv.ChucVu,
    pc.TenCa,
    pc.GioBatDau,
    pc.GioKetThuc,
    DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0 AS SoGioLam,
    ISNULL(ttl.LuongCoBanTheoGio, 0) AS LuongTheoGio,
    ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) AS LuongCa,
    -- Phụ cấp (có thể mở rộng)
    CASE 
        WHEN DATEPART(WEEKDAY, pc.NgayLamViec) IN (1, 7) THEN 
            ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) * 0.5  -- 150% cuối tuần
        ELSE 0
    END AS PhuCapCuoiTuan,
    CASE 
        WHEN pc.GioBatDau >= '22:00' OR pc.GioKetThuc <= '06:00' THEN 
            ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) * 0.3  -- 130% ca đêm
        ELSE 0
    END AS PhuCapCaDem,
    -- Tổng lương sau phụ cấp
    ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) +
    CASE WHEN DATEPART(WEEKDAY, pc.NgayLamViec) IN (1, 7) THEN 
        ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) * 0.5 ELSE 0 END +
    CASE WHEN pc.GioBatDau >= '22:00' OR pc.GioKetThuc <= '06:00' THEN 
        ISNULL(ttl.LuongCoBanTheoGio, 0) * (DATEDIFF(MINUTE, pc.GioBatDau, pc.GioKetThuc) / 60.0) * 0.3 ELSE 0 END AS TongLuongSauPhuCap
FROM PhanCongCa pc
INNER JOIN NhanVien nv ON pc.NhanVienID = nv.NhanVienID
LEFT JOIN ThongTinLuongNhanVien ttl ON pc.NhanVienID = ttl.NhanVienID
    AND ttl.Thang = MONTH(pc.NgayLamViec)
    AND ttl.Nam = YEAR(pc.NgayLamViec);
GO
