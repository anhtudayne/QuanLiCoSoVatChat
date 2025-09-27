USE vc;
GO

-- =============================================
-- TRIGGERS TỰ ĐỘNG CHO BẢO TRÌ & THANH LÝ
-- =============================================


-- 2. TRIGGER KIỂM TRA VÀ CẬP NHẬT TRẠNG THÁI KHI BẢO TRÌ
CREATE OR ALTER TRIGGER tr_UpdateAssetStatusOnMaintenance
ON LichSuBaoTri
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Khi tạo yêu cầu bảo trì mới
    IF EXISTS (SELECT 1 FROM inserted WHERE TrangThai = N'Chờ xử lý')
    BEGIN
        UPDATE CSVC 
        SET TinhTrang = N'Bảo trì'
        WHERE CSVCID IN (
            SELECT CSVCID FROM inserted 
            WHERE TrangThai = N'Chờ xử lý'
        );
    END
    
    -- Khi hoàn thành bảo trì
    IF EXISTS (SELECT 1 FROM inserted WHERE TrangThai = N'Hoàn thành')
    BEGIN
        UPDATE CSVC 
        SET TinhTrang = N'Đang sử dụng'
        WHERE CSVCID IN (
            SELECT CSVCID FROM inserted 
            WHERE TrangThai = N'Hoàn thành'
        )
        AND TinhTrang != N'Đã thanh lý'; -- Không cập nhật nếu đã thanh lý
    END
    
    -- Khi hủy bỏ bảo trì
    IF EXISTS (SELECT 1 FROM inserted WHERE TrangThai = N'Hủy bỏ')
    BEGIN
        UPDATE CSVC 
        SET TinhTrang = N'Đang sử dụng'
        WHERE CSVCID IN (
            SELECT CSVCID FROM inserted 
            WHERE TrangThai = N'Hủy bỏ'
        )
        AND TinhTrang != N'Đã thanh lý';
    END
END
GO


-- 6. TRIGGER TỰ ĐỘNG CẬP NHẬT LỊCH BẢO TRÌ ĐỊNH KỲ
CREATE OR ALTER TRIGGER tr_UpdateNextMaintenanceDate
ON LichSuBaoTri
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Khi bảo trì hoàn thành, cập nhật hoặc tạo mới lịch bảo trì định kỳ
    -- Cập nhật lịch cũ nếu có
    UPDATE LichBaoTri 
    SET NgayBatDau = DATEADD(MONTH, ChuKyBaoTri_Thang, GETDATE())
    WHERE CSVCID IN (SELECT CSVCID FROM inserted WHERE TrangThai = N'Hoàn thành');
    
    -- Thêm lịch mới cho những CSVC chưa có lịch
    INSERT INTO LichBaoTri (CSVCID, ChuKyBaoTri_Thang, NgayBatDau)
    SELECT CSVCID, 6, DATEADD(MONTH, 6, GETDATE())
    FROM inserted 
    WHERE TrangThai = N'Hoàn thành'
    AND CSVCID NOT IN (SELECT CSVCID FROM LichBaoTri);
END
GO