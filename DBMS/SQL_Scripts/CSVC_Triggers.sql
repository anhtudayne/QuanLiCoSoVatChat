USE vc;
GO

-- =============================================
-- TRIGGERS CHO QUẢN LÝ CƠ SỞ VẬT CHẤT
-- =============================================

-- 1. TRIGGER TỰ ĐỘNG TẠO LỊCH BẢO TRÌ ĐỊNH KỲ SAU KHI THÊM THÔNG TIN SỬ DỤNG
CREATE OR ALTER TRIGGER tr_ThongTinSuDung_AfterInsert
ON ThongTinSuDung
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Tạo lịch bảo trì định kỳ mặc định cho CSVC mới
        INSERT INTO LichBaoTri (CSVCID, ChuKyBaoTri_Thang, NgayBatDau, GhiChu)
        SELECT 
            i.CSVCID,
            CASE 
                WHEN l.TenLoai LIKE N'%Thiết bị điện%' THEN 6  -- 6 tháng cho thiết bị điện
                WHEN l.TenLoai LIKE N'%Nội thất%' THEN 12      -- 12 tháng cho nội thất
                WHEN l.TenLoai LIKE N'%Văn phòng%' THEN 3      -- 3 tháng cho thiết bị văn phòng
                ELSE 6                                         -- Mặc định 6 tháng
            END AS ChuKy,
            DATEADD(MONTH, 1, i.NgayMua) AS NgayBatDau,
            N'Lịch bảo trì được tạo tự động khi thêm thông tin sử dụng CSVC'
        FROM inserted i
        INNER JOIN CSVC c ON i.CSVCID = c.CSVCID
        LEFT JOIN LoaiCSVC l ON c.LoaiID = l.LoaiID
        WHERE c.TinhTrang != N'Đã thanh lý'  -- Không tạo lịch cho CSVC đã thanh lý
          AND NOT EXISTS (SELECT 1 FROM LichBaoTri WHERE CSVCID = i.CSVCID); -- Không tạo trùng
        
    END TRY
    BEGIN CATCH
    END CATCH
END
GO

-- 2. TRIGGER KIỂM TRA TRẠNG THÁI HỢP LỆ KHI CẬP NHẬT (AFTER UPDATE)
CREATE OR ALTER TRIGGER tr_CSVC_BeforeUpdate
ON CSVC
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra các ràng buộc nghiệp vụ
        DECLARE @InvalidUpdates TABLE (
            CSVCID INT,
            ErrorMessage NVARCHAR(500)
        );
        
        -- Kiểm tra 1: Không cho phép thay đổi trạng thái từ "Đã thanh lý" sang trạng thái khác
        INSERT INTO @InvalidUpdates (CSVCID, ErrorMessage)
        SELECT 
            d.CSVCID,
            N'Không thể thay đổi trạng thái của CSVC đã thanh lý'
        FROM deleted d
        INNER JOIN inserted i ON d.CSVCID = i.CSVCID
        WHERE d.TinhTrang = N'Đã thanh lý' 
          AND i.TinhTrang != N'Đã thanh lý';
        
        -- Kiểm tra 2: Không cho phép thay đổi sang "Đã thanh lý" nếu chưa có trong bảng ThanhLyCSVC
        INSERT INTO @InvalidUpdates (CSVCID, ErrorMessage)
        SELECT 
            i.CSVCID,
            N'Phải thực hiện thanh lý CSVC trước khi thay đổi trạng thái thành "Đã thanh lý"'
        FROM inserted i
        INNER JOIN deleted d ON i.CSVCID = d.CSVCID
        WHERE i.TinhTrang = N'Đã thanh lý' 
          AND d.TinhTrang != N'Đã thanh lý'
          AND NOT EXISTS (SELECT 1 FROM ThanhLyCSVC WHERE CSVCID = i.CSVCID);
        
        -- Kiểm tra 3: Không cho phép giá trị âm
        INSERT INTO @InvalidUpdates (CSVCID, ErrorMessage)
        SELECT 
            i.CSVCID,
            N'Giá trị CSVC không được âm'
        FROM inserted i
        WHERE i.GiaTri < 0;
        
        -- Nếu có lỗi, rollback và thông báo
        IF EXISTS (SELECT 1 FROM @InvalidUpdates)
        BEGIN
            ROLLBACK TRANSACTION;
            DECLARE @ErrorMsg NVARCHAR(4000);
            SELECT TOP 1 @ErrorMsg = ErrorMessage FROM @InvalidUpdates;
            RAISERROR(@ErrorMsg, 16, 1);
            RETURN;
        END
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END CATCH
END
GO

-- 3. TRIGGER KIỂM TRA RÀNG BUỘC TRƯỚC KHI XÓA (AFTER DELETE)
CREATE OR ALTER TRIGGER tr_CSVC_BeforeDelete
ON CSVC
FOR DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Kiểm tra các ràng buộc trước khi cho phép xóa
        DECLARE @BlockedDeletes TABLE (
            CSVCID INT,
            ErrorMessage NVARCHAR(500)
        );
        
        -- Kiểm tra 1: CSVC đang có lịch sử bảo trì chưa hoàn thành
        INSERT INTO @BlockedDeletes (CSVCID, ErrorMessage)
        SELECT 
            d.CSVCID,
            N'Không thể xóa CSVC đang có yêu cầu bảo trì chưa hoàn thành'
        FROM deleted d
        WHERE EXISTS (
            SELECT 1 FROM LichSuBaoTri 
            WHERE CSVCID = d.CSVCID 
              AND TrangThai IN (N'Chờ xử lý', N'Đang thực hiện')
        );
        
        -- Nếu có ràng buộc, rollback và thông báo
        IF EXISTS (SELECT 1 FROM @BlockedDeletes)
        BEGIN
            ROLLBACK TRANSACTION;
            DECLARE @ErrorMsg NVARCHAR(4000);
            SELECT TOP 1 @ErrorMsg = ErrorMessage FROM @BlockedDeletes;
            RAISERROR(@ErrorMsg, 16, 1);
            RETURN;
        END
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
        RETURN;
    END CATCH
END
GO
