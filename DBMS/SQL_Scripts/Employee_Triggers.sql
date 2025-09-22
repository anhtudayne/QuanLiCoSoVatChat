USE vc;
GO


-- 3. TRIGGER TỰ ĐỘNG TẠO THÔNG TIN LƯƠNG KHI THÊM NHÂN VIÊN MỚI
CREATE OR ALTER TRIGGER tr_NhanVien_CreateSalaryInfo
ON NhanVien
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tự động tạo thông tin lương cho nhân viên mới với lương cơ bản
    INSERT INTO ThongTinLuongNhanVien (NhanVienID, Thang, Nam, LuongCoBanTheoGio)
    SELECT 
        i.NhanVienID,
        MONTH(GETDATE()),
        YEAR(GETDATE()),
        CASE 
            WHEN i.ChucVu = N'Nhân viên kỹ thuật' THEN 40000
            WHEN i.ChucVu = N'Nhân viên trực' THEN 30000
            ELSE 35000
        END
    FROM inserted i
    WHERE NOT EXISTS (
        SELECT 1 FROM ThongTinLuongNhanVien 
        WHERE NhanVienID = i.NhanVienID 
          AND Thang = MONTH(GETDATE()) 
          AND Nam = YEAR(GETDATE())
    );
END
GO

