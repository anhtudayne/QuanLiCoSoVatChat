USE vc;
GO

-- =============================================
-- TRIGGER TỰ ĐỘNG TẠO LOGIN, USER VÀ GÁN ROLE KHI INSERT VÀO BẢNG TAIKHOAN
-- =============================================

CREATE OR ALTER TRIGGER trg_TaiKhoan_AutoCreateLoginUser
ON TaiKhoan
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Username NVARCHAR(50);
    DECLARE @Password NVARCHAR(100);
    DECLARE @Role NVARCHAR(50);
    DECLARE @SqlCmd NVARCHAR(MAX);
    DECLARE @DatabaseName NVARCHAR(50) = 'vc';
    DECLARE @RoleName NVARCHAR(50);
    
    -- Lấy thông tin từ bản ghi vừa insert
    SELECT 
        @Username = Username,
        @Password = Password,
        @Role = Role
    FROM inserted;
    
    -- Mapping role từ bảng TaiKhoan sang SQL Server roles
    SET @RoleName = CASE 
        WHEN @Role = N'Quản Lý' THEN 'QuanLyRole'
        WHEN @Role = N'Nhân Viên Kỹ Thuật' THEN 'KyThuatRole'
        WHEN @Role = N'Nhân Viên Trực' THEN 'NhanVienTrucRole'
        ELSE NULL
    END;
    
    -- Chỉ thực hiện nếu role hợp lệ
    IF @RoleName IS NOT NULL
    BEGIN
        BEGIN TRY
            -- 1. TẠO SQL SERVER LOGIN (nếu chưa tồn tại)
            IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @Username AND type = 'S')
            BEGIN
                SET @SqlCmd = 'CREATE LOGIN [' + @Username + '] WITH PASSWORD = ''' + @Password + ''', DEFAULT_DATABASE = [' + @DatabaseName + '];';
                EXEC sp_executesql @SqlCmd;
            END
            
            -- 2. TẠO DATABASE USER (nếu chưa tồn tại)
            IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @Username AND type = 'S')
            BEGIN
                SET @SqlCmd = 'CREATE USER [' + @Username + '] FOR LOGIN [' + @Username + '];';
                EXEC sp_executesql @SqlCmd;
            END
            
            -- 3. GÁN USER VÀO ROLE
            IF NOT EXISTS (
                SELECT 1 
                FROM sys.database_role_members rm
                INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
                INNER JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
                WHERE r.name = @RoleName AND u.name = @Username
            )
            BEGIN
                SET @SqlCmd = 'ALTER ROLE [' + @RoleName + '] ADD MEMBER [' + @Username + '];';
                EXEC sp_executesql @SqlCmd;
            END
            
        END TRY
        BEGIN CATCH
            -- Có thể throw lại lỗi hoặc không tùy vào yêu cầu
            -- THROW;
        END CATCH
    END
END;
GO

-- =============================================
-- TRIGGER XÓA LOGIN VÀ USER KHI XÓA TÀI KHOẢN
-- =============================================

CREATE OR ALTER TRIGGER trg_TaiKhoan_AutoDeleteLoginUser
ON TaiKhoan
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Username NVARCHAR(50);
    DECLARE @Role NVARCHAR(50);
    DECLARE @SqlCmd NVARCHAR(MAX);
    
    -- Lấy thông tin từ bản ghi vừa xóa
    SELECT 
        @Username = Username,
        @Role = Role
    FROM deleted;
    
    BEGIN TRY
        -- 1. XÓA USER KHỎI DATABASE (nếu tồn tại)
        IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @Username AND type = 'S')
        BEGIN
            SET @SqlCmd = 'DROP USER [' + @Username + '];';
            EXEC sp_executesql @SqlCmd;
        END
        
        -- 2. XÓA LOGIN KHỎI SQL SERVER (nếu tồn tại)
        IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @Username AND type = 'S')
        BEGIN
            SET @SqlCmd = 'DROP LOGIN [' + @Username + '];';
            EXEC sp_executesql @SqlCmd;
        END
        
    END TRY
    BEGIN CATCH
        -- Có thể xử lý lỗi nếu cần
    END CATCH
END;
GO

-- =============================================
-- TRIGGER CẬP NHẬT PASSWORD KHI UPDATE TÀI KHOẢN
-- =============================================

CREATE OR ALTER TRIGGER trg_TaiKhoan_AutoUpdatePassword
ON TaiKhoan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Chỉ thực hiện khi Password bị thay đổi
    IF UPDATE(Password)
    BEGIN
        DECLARE @Username NVARCHAR(50);
        DECLARE @NewPassword NVARCHAR(100);
        DECLARE @SqlCmd NVARCHAR(MAX);
        
        -- Lấy thông tin từ bản ghi vừa update
        SELECT 
            @Username = Username,
            @NewPassword = Password
        FROM inserted;
        
        BEGIN TRY
            -- Cập nhật password cho SQL Server Login
            IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @Username AND type = 'S')
            BEGIN
                SET @SqlCmd = 'ALTER LOGIN [' + @Username + '] WITH PASSWORD = ''' + @NewPassword + ''';';
                EXEC sp_executesql @SqlCmd;
            END
            
        END TRY
        BEGIN CATCH
            -- Có thể xử lý lỗi nếu cần
        END CATCH
    END
    
    -- Xử lý khi Role bị thay đổi
    IF UPDATE(Role)
    BEGIN
        DECLARE @OldRole NVARCHAR(50);
        DECLARE @NewRole NVARCHAR(50);
        DECLARE @OldRoleName NVARCHAR(50);
        DECLARE @NewRoleName NVARCHAR(50);
        
        SELECT @Username = Username, @NewRole = Role FROM inserted;
        SELECT @OldRole = Role FROM deleted;
        
        -- Mapping roles
        SET @OldRoleName = CASE 
            WHEN @OldRole = N'Quản Lý' THEN 'QuanLyRole'
            WHEN @OldRole = N'Nhân Viên Kỹ Thuật' THEN 'KyThuatRole'
            WHEN @OldRole = N'Nhân Viên Trực' THEN 'NhanVienTrucRole'
            ELSE NULL
        END;
        
        SET @NewRoleName = CASE 
            WHEN @NewRole = N'Quản Lý' THEN 'QuanLyRole'
            WHEN @NewRole = N'Nhân Viên Kỹ Thuật' THEN 'KyThuatRole'
            WHEN @NewRole = N'Nhân Viên Trực' THEN 'NhanVienTrucRole'
            ELSE NULL
        END;
        
        BEGIN TRY
            -- Xóa khỏi role cũ
            IF @OldRoleName IS NOT NULL
            BEGIN
                SET @SqlCmd = 'ALTER ROLE [' + @OldRoleName + '] DROP MEMBER [' + @Username + '];';
                EXEC sp_executesql @SqlCmd;
            END
            
            -- Thêm vào role mới
            IF @NewRoleName IS NOT NULL
            BEGIN
                SET @SqlCmd = 'ALTER ROLE [' + @NewRoleName + '] ADD MEMBER [' + @Username + '];';
                EXEC sp_executesql @SqlCmd;
            END
            
        END TRY
        BEGIN CATCH
            -- Có thể xử lý lỗi nếu cần
        END CATCH
    END
END;
GO

-- =============================================
-- HƯỚNG DẪN SỬ DỤNG
-- =============================================

/*
1. TRIGGER TỰ ĐỘNG HOẠT ĐỘNG KHI:
   - INSERT: Tạo SQL Server Login, Database User, gán vào Role
   - DELETE: Xóa Database User và SQL Server Login
   - UPDATE: Cập nhật Password hoặc thay đổi Role

2. MAPPING ROLES:
   - 'Quản Lý' → 'QuanLyRole'
   - 'Nhân Viên Kỹ Thuật' → 'KyThuatRole'
   - 'Nhân Viên Trực' → 'NhanVienTrucRole'

3. TEST TRIGGER:
   INSERT INTO TaiKhoan (Username, Password, Role, NhanVienID) 
   VALUES ('testuser', 'testpass123', N'Nhân Viên Trực', 1);

4. KIỂM TRA KẾT QUẢ:
   -- Kiểm tra SQL Server Logins
   SELECT name, create_date FROM sys.server_principals WHERE type = 'S' AND name = 'testuser';
   
   -- Kiểm tra Database Users và Roles
   SELECT 
       u.name AS UserName,
       r.name AS RoleName
   FROM sys.database_role_members rm
   INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
   INNER JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
   WHERE u.name = 'testuser';

5. LƯU Ý BẢO MẬT:
   - Password được lưu dưới dạng plain text trong trigger này
   - Trong production nên hash password trước khi lưu
   - Cân nhắc sử dụng Windows Authentication thay vì SQL Authentication
*/

PRINT 'Đã tạo thành công các triggers cho tự động quản lý SQL Server Login và User!';
PRINT 'Trigger sẽ tự động tạo/xóa/cập nhật Login và User khi thao tác với bảng TaiKhoan.';
GO

-- =============================================
-- FUNCTION KIỂM TRA ĐĂNG NHẬP VÀ TRẢ VỀ ROLE
-- =============================================

CREATE OR ALTER FUNCTION fn_LoginAndGetRole
(
    @username NVARCHAR(50),
    @password NVARCHAR(50)
)
RETURNS NVARCHAR(50) -- Trả về tên vai trò hoặc thông báo lỗi
AS
BEGIN
    DECLARE @role NVARCHAR(50);
    DECLARE @isValid BIT;
    
    -- Kiểm tra username và password có hợp lệ không
    SELECT @isValid = CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM TaiKhoan 
            WHERE username = @username 
                AND TaiKhoan.password = @password
        )
        THEN 1 
        ELSE 0 
    END;
    
    IF @isValid = 1
    BEGIN
        -- Nếu đăng nhập hợp lệ, lấy vai trò của user
        SELECT @role = dp.name 
        FROM sys.database_role_members AS drm
        JOIN sys.database_principals AS dp ON drm.role_principal_id = dp.principal_id
        JOIN sys.database_principals AS dp2 ON drm.member_principal_id = dp2.principal_id
        WHERE dp2.name = @username;
        
        -- Nếu user không thuộc vai trò nào thì trả về thông báo không có vai trò
        IF @role IS NULL
        BEGIN
            SET @role = N'User này không có role nào';
        END
    END
    ELSE
    BEGIN
        -- Nếu đăng nhập không hợp lệ
        SET @role = N'Sai username hoặc password';
    END
    
    RETURN @role;
END
GO



