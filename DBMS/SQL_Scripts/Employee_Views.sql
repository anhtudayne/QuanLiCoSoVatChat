USE vc;
GO

-- =============================================
-- VIEWS CHO BÁO CÁO VÀ THỐNG KÊ NHÂN VIÊN
-- =============================================

-- 1. VIEW THÔNG TIN CHI TIẾT NHÂN VIÊN (TÍNH LƯƠNG THEO GIỜ LÀM THỰC TẾ)
CREATE OR ALTER VIEW vw_EmployeeDetails
AS
SELECT 
    nv.NhanVienID,
    nv.HoTen,
    nv.NgaySinh,
    -- Tính tuổi trực tiếp
    DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) AS Tuoi,
    nv.GioiTinh,
    nv.DiaChi,
    nv.SoDienThoai,
    nv.Email,
    nv.TrangThai,
    nv.ChucVu,
    -- Thông tin lương hiện tại
    ttl.LuongCoBanTheoGio,
    -- Số ca trực trong tháng hiện tại
    ISNULL(ca_count.SoCaTrongThang, 0) AS SoCaTrongThang,
    -- Tổng giờ làm trong tháng hiện tại
    ISNULL(ca_hours.TongGioLam, 0) AS TongGioLamTrongThang,
    -- Lương thực tế = LuongCoBanTheoGio * số giờ làm thực tế
    ttl.LuongCoBanTheoGio * ISNULL(ca_hours.TongGioLam, 0) AS LuongThucTe
FROM NhanVien nv
LEFT JOIN ThongTinLuongNhanVien ttl ON nv.NhanVienID = ttl.NhanVienID 
    AND ttl.Thang = MONTH(GETDATE()) 
    AND ttl.Nam = YEAR(GETDATE())
LEFT JOIN (
    SELECT 
        pc.NhanVienID,
        COUNT(*) AS SoCaTrongThang
    FROM PhanCongCa pc 
    WHERE MONTH(pc.NgayLamViec) = MONTH(GETDATE()) 
      AND YEAR(pc.NgayLamViec) = YEAR(GETDATE())
    GROUP BY pc.NhanVienID
) ca_count ON nv.NhanVienID = ca_count.NhanVienID
LEFT JOIN (
    SELECT 
        pc.NhanVienID,
        SUM(DATEDIFF(HOUR, pc.GioBatDau, pc.GioKetThuc)) AS TongGioLam
    FROM PhanCongCa pc 
    WHERE MONTH(pc.NgayLamViec) = MONTH(GETDATE()) 
      AND YEAR(pc.NgayLamViec) = YEAR(GETDATE())
    GROUP BY pc.NhanVienID
) ca_hours ON nv.NhanVienID = ca_hours.NhanVienID;
GO

-- 2. VIEW THỐNG KÊ NHÂN VIÊN THEO CHỨC VỤ
CREATE OR ALTER VIEW vw_EmployeeByPosition
AS
SELECT 
    ChucVu,
    COUNT(*) AS TongSo,
    COUNT(CASE WHEN TrangThai = N'Đang làm' THEN 1 END) AS DangLam,
    COUNT(CASE WHEN TrangThai = N'Đã nghỉ việc' THEN 1 END) AS DaNghiViec,
    CAST(COUNT(CASE WHEN TrangThai = N'Đang làm' THEN 1 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) AS TyLeDangLam
FROM NhanVien
GROUP BY ChucVu;
GO

-- 3. VIEW NHÂN VIÊN CÓ SINH NHẬT TRONG THÁNG
CREATE OR ALTER VIEW vw_BirthdayThisMonth
AS
SELECT 
    NhanVienID,
    HoTen,
    NgaySinh,
    DAY(NgaySinh) AS NgaySinhNhat,
    -- Tính tuổi sắp tới trực tiếp
    DATEDIFF(YEAR, NgaySinh, GETDATE()) + 1 AS TuoiSapToi,
    ChucVu,
    SoDienThoai,
    Email
FROM NhanVien
WHERE TrangThai = N'Đang làm'
  AND MONTH(NgaySinh) = MONTH(GETDATE())
  AND NgaySinh IS NOT NULL;
GO

-- 4. VIEW BÁO CÁO LƯƠNG NHÂN VIÊN
CREATE OR ALTER VIEW vw_SalaryReport
AS
SELECT 
    nv.NhanVienID,
    nv.HoTen,
    nv.ChucVu,
    ttl.Thang,
    ttl.Nam,
    ttl.LuongCoBanTheoGio,
    -- Tính số giờ làm thực tế từ ca trực
    ISNULL(ca_stats.TongGioLam, 0) AS TongGioLamThucTe,
    -- Lương theo giờ thực tế
    ttl.LuongCoBanTheoGio * ISNULL(ca_stats.TongGioLam, 0) AS LuongThucTe,
    -- Lương cơ bản (8h x 22 ngày)
    ttl.LuongCoBanTheoGio * 8 * 22 AS LuongCoBan
FROM NhanVien nv
INNER JOIN ThongTinLuongNhanVien ttl ON nv.NhanVienID = ttl.NhanVienID
LEFT JOIN (
    SELECT 
        pc.NhanVienID,
        MONTH(pc.NgayLamViec) AS Thang,
        YEAR(pc.NgayLamViec) AS Nam,
        SUM(DATEDIFF(HOUR, pc.GioBatDau, pc.GioKetThuc)) AS TongGioLam
    FROM PhanCongCa pc
    GROUP BY pc.NhanVienID, MONTH(pc.NgayLamViec), YEAR(pc.NgayLamViec)
) ca_stats ON nv.NhanVienID = ca_stats.NhanVienID 
    AND ttl.Thang = ca_stats.Thang 
    AND ttl.Nam = ca_stats.Nam
WHERE nv.TrangThai = N'Đang làm';
GO



