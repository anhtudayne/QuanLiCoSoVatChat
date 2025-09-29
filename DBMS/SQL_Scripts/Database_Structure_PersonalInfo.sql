-- ================================================
-- Database Structure Summary for Personal Info
-- ================================================

USE vc;
GO

PRINT '=== CẤUP TRÚC DATABASE CHO THÔNG TIN CÁ NHÂN ===';
PRINT '';

-- 1. Cấu trúc bảng NhanVien
PRINT '1. BẢNG NHANVIEN:';
SELECT 
    COLUMN_NAME AS 'Tên Cột',
    DATA_TYPE AS 'Kiểu Dữ Liệu',
    CHARACTER_MAXIMUM_LENGTH AS 'Độ Dài',
    IS_NULLABLE AS 'Có Thể NULL'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'NhanVien'
ORDER BY ORDINAL_POSITION;

PRINT '';

-- 2. Cấu trúc bảng TaiKhoan
PRINT '2. BẢNG TAIKHOAN:';
SELECT 
    COLUMN_NAME AS 'Tên Cột',
    DATA_TYPE AS 'Kiểu Dữ Liệu',
    CHARACTER_MAXIMUM_LENGTH AS 'Độ Dài',
    IS_NULLABLE AS 'Có Thể NULL'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'TaiKhoan'
ORDER BY ORDINAL_POSITION;

PRINT '';

PRINT '';
PRINT '=== SỰ KHÁC BIỆT SO VỚI FUNCTION CŨ ===';
PRINT '';
PRINT 'CÁC THAY ĐỔI CHÍNH:';
PRINT '- TenNhanVien -> HoTen (trong bảng NhanVien)';
PRINT '- VaiTro -> Role (trong bảng TaiKhoan)';
PRINT '- NgayVaoLam: ĐÃ LOẠI BỎ hoàn toàn';
PRINT '- LuongCoBan: ĐÃ LOẠI BỎ hoàn toàn (bảo mật thông tin)';
PRINT '- Form chỉ hiển thị thông tin cơ bản: ID, Tên, Sinh, Giới tính, Liên lạc, Chức vụ, Trạng thái, Tài khoản';
PRINT '';

-- 4. Dữ liệu mẫu có sẵn
PRINT '4. DỮ LIỆU MẪU CÓ SẴN:';
SELECT 
    nv.NhanVienID,
    nv.HoTen,
    nv.ChucVu,
    tk.Username,
    tk.Role,
    nv.TrangThai
FROM NhanVien nv
LEFT JOIN TaiKhoan tk ON nv.NhanVienID = tk.NhanVienID
ORDER BY nv.NhanVienID;

GO