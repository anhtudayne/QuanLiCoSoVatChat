USE vc;
GO

-- =============================================
-- TRIGGERS TỰ ĐỘNG CHO BẢO TRÌ & THANH LÝ
-- =============================================

-- 1. TRIGGER TỰ ĐỘNG TẠO LỊCH BẢO TRÌ KHI MUA CSVC MỚI
CREATE OR ALTER TRIGGER tr_AutoCreateMaintenanceSchedule
ON ThongTinSuDung
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tự động tạo lịch bảo trì cho CSVC mới (chu kỳ 6 tháng)
    INSERT INTO LichBaoTri (CSVCID, ChuKyBaoTri_Thang, NgayBatDau, GhiChu)
    SELECT 
        i.CSVCID,
        6, -- Chu kỳ mặc định 6 tháng
        DATEADD(MONTH, 6, i.NgayMua), -- Bảo trì lần đầu sau 6 tháng
        N'Lịch bảo trì tự động được tạo khi mua CSVC'
    FROM inserted i
    INNER JOIN CSVC c ON i.CSVCID = c.CSVCID
    WHERE c.TinhTrang != N'Đã thanh lý'
    AND NOT EXISTS (SELECT 1 FROM LichBaoTri WHERE CSVCID = i.CSVCID);
END
GO

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

-- 3. TRIGGER KIỂM TRA RÀNG BUỘC TRƯỚC KHI THANH LÝ
CREATE OR ALTER TRIGGER tr_ValidateBeforeDisposal
ON ThanhLyCSVC
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ErrorMessage NVARCHAR(500);
    
    -- Kiểm tra CSVC có đang trong quá trình bảo trì không
    IF EXISTS (
        SELECT 1 FROM inserted i
        INNER JOIN LichSuBaoTri bt ON i.CSVCID = bt.CSVCID
        WHERE bt.TrangThai IN (N'Chờ xử lý', N'Đang xử lý')
    )
    BEGIN
        SET @ErrorMessage = N'Không thể thanh lý CSVC đang trong quá trình bảo trì!';
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END
    
    -- Kiểm tra CSVC đã được thanh lý chưa
    IF EXISTS (
        SELECT 1 FROM inserted i
        INNER JOIN CSVC c ON i.CSVCID = c.CSVCID
        WHERE c.TinhTrang = N'Đã thanh lý'
    )
    BEGIN
        SET @ErrorMessage = N'CSVC này đã được thanh lý trước đó!';
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END
    
    -- Nếu tất cả kiểm tra đều hợp lệ, thực hiện insert
    INSERT INTO ThanhLyCSVC (CSVCID, NgayThanhLy, LyDoThanhLy, GiaTriThanhLy, NguoiThucHienID)
    SELECT CSVCID, NgayThanhLy, LyDoThanhLy, GiaTriThanhLy, NguoiThucHienID
    FROM inserted;
    
    -- Cập nhật trạng thái CSVC
    UPDATE CSVC 
    SET TinhTrang = N'Đã thanh lý'
    WHERE CSVCID IN (SELECT CSVCID FROM inserted);
    
    -- Xóa lịch bảo trì định kỳ
    DELETE FROM LichBaoTri 
    WHERE CSVCID IN (SELECT CSVCID FROM inserted);
END
GO

-- 4. TRIGGER AUDIT LOG CHO BẢO TRÌ
CREATE OR ALTER TRIGGER tr_AuditMaintenanceChanges
ON LichSuBaoTri
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Tạo bảng audit log nếu chưa có
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AuditLog_BaoTri')
    BEGIN
        CREATE TABLE AuditLog_BaoTri (
            LogID INT IDENTITY PRIMARY KEY,
            BaoTriID INT,
            Action NVARCHAR(10), -- INSERT, UPDATE, DELETE
            OldValues NVARCHAR(MAX),
            NewValues NVARCHAR(MAX),
            ChangedBy NVARCHAR(100) DEFAULT SYSTEM_USER,
            ChangedDate DATETIME DEFAULT GETDATE()
        );
    END
    
    -- Ghi log cho INSERT
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog_BaoTri (BaoTriID, Action, NewValues)
        SELECT 
            BaoTriID,
            'INSERT',
            'CSVCID=' + CAST(CSVCID AS NVARCHAR(10)) + 
            ',TrangThai=' + TrangThai + 
            ',NgayYeuCau=' + CONVERT(NVARCHAR(10), NgayYeuCau, 103) +
            ',ChiPhi=' + CAST(ISNULL(ChiPhi, 0) AS NVARCHAR(20))
        FROM inserted;
    END
    
    -- Ghi log cho UPDATE
    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog_BaoTri (BaoTriID, Action, OldValues, NewValues)
        SELECT 
            i.BaoTriID,
            'UPDATE',
            'TrangThai=' + d.TrangThai + ',ChiPhi=' + CAST(ISNULL(d.ChiPhi, 0) AS NVARCHAR(20)),
            'TrangThai=' + i.TrangThai + ',ChiPhi=' + CAST(ISNULL(i.ChiPhi, 0) AS NVARCHAR(20))
        FROM inserted i
        INNER JOIN deleted d ON i.BaoTriID = d.BaoTriID
        WHERE i.TrangThai != d.TrangThai OR ISNULL(i.ChiPhi, 0) != ISNULL(d.ChiPhi, 0);
    END
    
    -- Ghi log cho DELETE
    IF NOT EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO AuditLog_BaoTri (BaoTriID, Action, OldValues)
        SELECT 
            BaoTriID,
            'DELETE',
            'CSVCID=' + CAST(CSVCID AS NVARCHAR(10)) + 
            ',TrangThai=' + TrangThai
        FROM deleted;
    END
END
GO

-- 5. TRIGGER CẢNH BÁO CHI PHÍ BẢO TRÌ CAO
CREATE OR ALTER TRIGGER tr_HighMaintenanceCostAlert
ON LichSuBaoTri
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ThresholdAmount DECIMAL(18,2) = 5000000; -- 5 triệu VND
    
    -- Tạo bảng cảnh báo nếu chưa có
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'MaintenanceAlerts')
    BEGIN
        CREATE TABLE MaintenanceAlerts (
            AlertID INT IDENTITY PRIMARY KEY,
            BaoTriID INT,
            CSVCID INT,
            AlertType NVARCHAR(50),
            AlertMessage NVARCHAR(500),
            AlertDate DATETIME DEFAULT GETDATE(),
            IsResolved BIT DEFAULT 0
        );
    END
    
    -- Kiểm tra chi phí bảo trì cao
    INSERT INTO MaintenanceAlerts (BaoTriID, CSVCID, AlertType, AlertMessage)
    SELECT 
        i.BaoTriID,
        i.CSVCID,
        N'CHI_PHI_CAO',
        N'Chi phí bảo trì ' + CAST(i.ChiPhi AS NVARCHAR(20)) + N' VND vượt ngưỡng cho phép!'
    FROM inserted i
    INNER JOIN CSVC c ON i.CSVCID = c.CSVCID
    WHERE i.ChiPhi > @ThresholdAmount
    AND NOT EXISTS (
        SELECT 1 FROM MaintenanceAlerts ma 
        WHERE ma.BaoTriID = i.BaoTriID 
        AND ma.AlertType = N'CHI_PHI_CAO'
        AND ma.IsResolved = 0
    );
    
    -- Kiểm tra CSVC bảo trì quá thường xuyên (>3 lần trong 6 tháng)
    INSERT INTO MaintenanceAlerts (BaoTriID, CSVCID, AlertType, AlertMessage)
    SELECT 
        i.BaoTriID,
        i.CSVCID,
        N'BAO_TRI_THUONG_XUYEN',
        N'CSVC ' + c.TenCSVC + N' đã bảo trì quá ' + CAST(maintenance_count.SoLan AS NVARCHAR(10)) + N' lần trong 6 tháng!'
    FROM inserted i
    INNER JOIN CSVC c ON i.CSVCID = c.CSVCID
    CROSS APPLY (
        SELECT COUNT(*) AS SoLan
        FROM LichSuBaoTri bt 
        WHERE bt.CSVCID = i.CSVCID 
        AND bt.NgayYeuCau >= DATEADD(MONTH, -6, GETDATE())
        AND bt.TrangThai = N'Hoàn thành'
    ) maintenance_count
    WHERE maintenance_count.SoLan > 3
    AND NOT EXISTS (
        SELECT 1 FROM MaintenanceAlerts ma 
        WHERE ma.CSVCID = i.CSVCID 
        AND ma.AlertType = N'BAO_TRI_THUONG_XUYEN'
        AND ma.IsResolved = 0
        AND ma.AlertDate >= DATEADD(MONTH, -1, GETDATE()) -- Không spam cảnh báo
    );
END
GO

-- 6. TRIGGER TỰ ĐỘNG CẬP NHẬT NGÀY BẢO TRÌ KẾ TIẾP
CREATE OR ALTER TRIGGER tr_UpdateNextMaintenanceDate
ON LichSuBaoTri
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Khi bảo trì hoàn thành, cập nhật ngày bắt đầu chu kỳ mới
    IF EXISTS (
        SELECT 1 FROM inserted i
        INNER JOIN deleted d ON i.BaoTriID = d.BaoTriID
        WHERE i.TrangThai = N'Hoàn thành' 
        AND d.TrangThai != N'Hoàn thành'
        AND i.NgayHoanThanh IS NOT NULL
    )
    BEGIN
        UPDATE LichBaoTri
        SET NgayBatDau = i.NgayHoanThanh
        FROM LichBaoTri lb
        INNER JOIN inserted i ON lb.CSVCID = i.CSVCID
        INNER JOIN deleted d ON i.BaoTriID = d.BaoTriID
        WHERE i.TrangThai = N'Hoàn thành' 
        AND d.TrangThai != N'Hoàn thành'
        AND i.NgayHoanThanh IS NOT NULL;
    END
END
GO