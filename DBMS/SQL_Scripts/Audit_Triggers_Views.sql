CREATE OR ALTER TRIGGER trg_Log_Insert_NhanVien
ON NhanVien
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LichSuSuKien (NoiDung)
    SELECT N'Thêm mới nhân viên: ' + i.HoTen
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER trg_Log_Insert_CSVC
ON CSVC
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LichSuSuKien (NoiDung)
    SELECT N'Thêm mới cơ sở vật chất: ' + i.TenCSVC
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER trg_Log_Insert_BaoTri
ON LichSuBaoTri
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LichSuSuKien (NoiDung)
    SELECT N'Thêm mới vào bảo trì cho CSVC: ' + c.TenCSVC
    FROM inserted i
    INNER JOIN CSVC c ON i.CSVCID = c.CSVCID;
END;
GO

CREATE OR ALTER TRIGGER trg_Log_Insert_ThanhLy
ON ThanhLyCSVC
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO LichSuSuKien (NoiDung)
    SELECT N'Thêm mới thanh lý cho CSVC: ' + c.TenCSVC
    FROM inserted i
    INNER JOIN CSVC c ON i.CSVCID = c.CSVCID;
END;
GO

CREATE OR ALTER VIEW v_LichSuSuKien
AS
SELECT 
    SuKienID,
    FORMAT(Ngay, 'dd/MM/yyyy HH:mm:ss') AS ThoiGian,
    NoiDung
FROM LichSuSuKien
GO
