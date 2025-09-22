USE vc;
GO

-- =============================================
-- TRIGGERS CHO PHÂN CÔNG CA TRỰC
-- =============================================

-- 1. TRIGGER KIỂM TRA VÀ VALIDATE DỮ LIỆU KHI INSERT
CREATE OR ALTER TRIGGER tr_PhanCongCa_Insert
ON PhanCongCa
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @PhanCongID INT, @NhanVienID INT, @NgayLamViec DATE;
    DECLARE @GioBatDau TIME, @GioKetThuc TIME, @HoTen NVARCHAR(100);
    
    -- Lấy thông tin từ bản ghi vừa insert
    SELECT 
        @PhanCongID = i.PhanCongID,
        @NhanVienID = i.NhanVienID,
        @NgayLamViec = i.NgayLamViec,
        @GioBatDau = i.GioBatDau,
        @GioKetThuc = i.GioKetThuc,
        @HoTen = nv.HoTen
    FROM INSERTED i
    INNER JOIN NhanVien nv ON i.NhanVienID = nv.NhanVienID;
    
    -- Kiểm tra ngày làm việc không được ở quá khứ (trừ hôm nay)
    IF @NgayLamViec < CAST(GETDATE() AS DATE)
    BEGIN
        RAISERROR(N'Không thể phân công ca cho ngày đã qua!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
    -- Kiểm tra thời gian làm việc hợp lý (ca tối thiểu 1 giờ, tối đa 12 giờ)
    DECLARE @SoGio DECIMAL(5,2) = DATEDIFF(MINUTE, @GioBatDau, @GioKetThuc) / 60.0;
    
    IF @SoGio < 1
    BEGIN
        RAISERROR(N'Ca làm việc phải tối thiểu 1 giờ!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
    IF @SoGio > 12
    BEGIN
        RAISERROR(N'Ca làm việc không được vượt quá 12 giờ!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
END
GO

-- 2. TRIGGER KIỂM TRA VÀ VALIDATE DỮ LIỆU KHI UPDATE
CREATE OR ALTER TRIGGER tr_PhanCongCa_Update
ON PhanCongCa
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @PhanCongID INT, @NhanVienID INT, @NgayLamViec DATE;
    DECLARE @GioBatDau TIME, @GioKetThuc TIME, @HoTen NVARCHAR(100);
    DECLARE @NgayLamViecCu DATE, @NhanVienIDCu INT;
    
    -- Lấy thông tin từ bản ghi sau khi update
    SELECT 
        @PhanCongID = i.PhanCongID,
        @NhanVienID = i.NhanVienID,
        @NgayLamViec = i.NgayLamViec,
        @GioBatDau = i.GioBatDau,
        @GioKetThuc = i.GioKetThuc,
        @HoTen = nv.HoTen
    FROM INSERTED i
    INNER JOIN NhanVien nv ON i.NhanVienID = nv.NhanVienID;
    
    -- Lấy thông tin cũ
    SELECT 
        @NgayLamViecCu = NgayLamViec,
        @NhanVienIDCu = NhanVienID
    FROM DELETED;
    
    -- Kiểm tra không được sửa ca đã qua (trừ hôm nay)
    IF @NgayLamViecCu < CAST(GETDATE() AS DATE)
    BEGIN
        RAISERROR(N'Không thể sửa ca trực đã qua!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
    -- Kiểm tra thời gian làm việc hợp lý
    DECLARE @SoGio DECIMAL(5,2) = DATEDIFF(MINUTE, @GioBatDau, @GioKetThuc) / 60.0;
    
    IF @SoGio < 1 OR @SoGio > 12
    BEGIN
        RAISERROR(N'Ca làm việc phải từ 1 đến 12 giờ!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
END
GO

-- 3. TRIGGER KIỂM TRA VÀ GHI LOG KHI DELETE
CREATE OR ALTER TRIGGER tr_PhanCongCa_Delete
ON PhanCongCa
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @PhanCongID INT, @NhanVienID INT, @NgayLamViec DATE;
    DECLARE @GioBatDau TIME, @GioKetThuc TIME, @HoTen NVARCHAR(100);
    
    -- Lấy thông tin từ bản ghi bị xóa
    SELECT 
        @PhanCongID = d.PhanCongID,
        @NhanVienID = d.NhanVienID,
        @NgayLamViec = d.NgayLamViec,
        @GioBatDau = d.GioBatDau,
        @GioKetThuc = d.GioKetThuc,
        @HoTen = nv.HoTen
    FROM DELETED d
    INNER JOIN NhanVien nv ON d.NhanVienID = nv.NhanVienID;
    
    -- Kiểm tra không được xóa ca đã qua
    IF @NgayLamViec < CAST(GETDATE() AS DATE)
    BEGIN
        RAISERROR(N'Không thể xóa ca trực đã qua!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
    
END
GO

