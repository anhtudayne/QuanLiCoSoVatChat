USE vc;
GO

-- =============================================
-- FUNCTIONS CHO PHÂN CÔNG CA TRỰC
-- =============================================





-- 4. FUNCTION TÍNH TỔNG GIỜ LÀM VIỆC CỦA NHÂN VIÊN TRONG THÁNG
CREATE OR ALTER FUNCTION fn_GetTotalWorkHoursInMonth
(
    @NhanVienID INT,
    @Thang INT,
    @Nam INT
)
RETURNS DECIMAL(8,2)
AS
BEGIN
    DECLARE @TotalHours DECIMAL(8,2) = 0;
    
    SELECT @TotalHours = ISNULL(SUM(DATEDIFF(MINUTE, GioBatDau, GioKetThuc) / 60.0), 0)
    FROM PhanCongCa
    WHERE NhanVienID = @NhanVienID
      AND MONTH(NgayLamViec) = @Thang
      AND YEAR(NgayLamViec) = @Nam;
    
    RETURN @TotalHours;
END
GO

-- 5. FUNCTION TÍNH LƯƠNG CỦA TẤT CẢ NHÂN VIÊN TRONG THÁNG
CREATE OR ALTER FUNCTION fn_GetAllEmployeeSalariesInMonth
(
    @Thang INT,
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        nv.NhanVienID,
        nv.HoTen,
        nv.ChucVu,
        nv.TrangThai,
        ISNULL(tl.LuongCoBanTheoGio, 0) AS LuongCoBanTheoGio,
        dbo.fn_GetTotalWorkHoursInMonth(nv.NhanVienID, @Thang, @Nam) AS TongGioLamViec,
        CAST(
            ISNULL(tl.LuongCoBanTheoGio, 0) * 
            dbo.fn_GetTotalWorkHoursInMonth(nv.NhanVienID, @Thang, @Nam) 
            AS DECIMAL(18,2)
        ) AS TongLuong,
        @Thang AS Thang,
        @Nam AS Nam
    FROM NhanVien nv
    LEFT JOIN ThongTinLuongNhanVien tl ON nv.NhanVienID = tl.NhanVienID 
                                      AND tl.Thang = @Thang 
                                      AND tl.Nam = @Nam
    WHERE nv.TrangThai = N'Đang làm'
);
GO

-- 6. FUNCTION TÍNH LƯƠNG CỦA MỘT NHÂN VIÊN CỤ THỂ TRONG THÁNG
CREATE OR ALTER FUNCTION fn_GetEmployeeSalaryInMonth
(
    @NhanVienID INT,
    @Thang INT,
    @Nam INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TongLuong DECIMAL(18,2) = 0;
    DECLARE @LuongCoBanTheoGio DECIMAL(18,2) = 0;
    DECLARE @TongGioLamViec DECIMAL(8,2) = 0;
    
    -- Lấy lương cơ bản theo giờ
    SELECT @LuongCoBanTheoGio = ISNULL(LuongCoBanTheoGio, 0)
    FROM ThongTinLuongNhanVien
    WHERE NhanVienID = @NhanVienID AND Thang = @Thang AND Nam = @Nam;
    
    -- Lấy tổng giờ làm việc
    SET @TongGioLamViec = dbo.fn_GetTotalWorkHoursInMonth(@NhanVienID, @Thang, @Nam);
    
    -- Tính tổng lương
    SET @TongLuong = @LuongCoBanTheoGio * @TongGioLamViec;
    
    RETURN @TongLuong;
END
GO

-- 7. FUNCTION LẤY CHI TIẾT LƯƠNG NHÂN VIÊN CỤ THỂ
CREATE OR ALTER FUNCTION fn_GetEmployeeSalaryDetails
(
    @NhanVienID INT,
    @Thang INT,
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        nv.NhanVienID,
        nv.HoTen,
        nv.ChucVu,
        nv.TrangThai,
        ISNULL(tl.LuongCoBanTheoGio, 0) AS LuongCoBanTheoGio,
        dbo.fn_GetTotalWorkHoursInMonth(nv.NhanVienID, @Thang, @Nam) AS TongGioLamViec,
        CAST(
            ISNULL(tl.LuongCoBanTheoGio, 0) * 
            dbo.fn_GetTotalWorkHoursInMonth(nv.NhanVienID, @Thang, @Nam) 
            AS DECIMAL(18,2)
        ) AS TongLuong,
        @Thang AS Thang,
        @Nam AS Nam
    FROM NhanVien nv
    LEFT JOIN ThongTinLuongNhanVien tl ON nv.NhanVienID = tl.NhanVienID 
                                      AND tl.Thang = @Thang 
                                      AND tl.Nam = @Nam
    WHERE nv.NhanVienID = @NhanVienID
);
GO



-- 10. FUNCTION KIỂM TRA CA LÀM VIỆC CÓ HỢP LỆ KHÔNG (thời gian bắt đầu < kết thúc)
CREATE OR ALTER FUNCTION fn_IsValidShiftTime
(
    @GioBatDau TIME,
    @GioKetThuc TIME
)
RETURNS BIT
AS
BEGIN
    DECLARE @IsValid BIT = 0;
    
    IF @GioBatDau < @GioKetThuc
    BEGIN
        SET @IsValid = 1;
    END
    
    RETURN @IsValid;
END
GO

-- 11. FUNCTION LẤY DANH SÁCH NHÂN VIÊN ACTIVE (CHO COMBOBOX)
CREATE OR ALTER FUNCTION fn_GetActiveEmployees()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        NhanVienID,
        HoTen,
        ChucVu,
        HoTen + N' (' + CAST(NhanVienID AS NVARCHAR(10)) + N')' AS DisplayText
    FROM NhanVien 
    WHERE TrangThai = N'Đang làm'
);
GO

-- 12. FUNCTION LẤY DANH SÁCH TÊN CA TRỰC (CHO COMBOBOX)
CREATE OR ALTER FUNCTION fn_GetShiftTypes()
RETURNS TABLE
AS
RETURN
(
    SELECT DISTINCT TenCa
    FROM PhanCongCa
    WHERE TenCa IS NOT NULL
    UNION
    SELECT N'Ca sáng' AS TenCa
    UNION 
    SELECT N'Ca chiều' AS TenCa
    UNION
    SELECT N'Ca tối' AS TenCa
);
GO

-- 13. FUNCTION LẤY GIỜ BẮT ĐẦU VÀ KẾT THÚC THEO LOẠI CA
CREATE OR ALTER FUNCTION fn_GetShiftTimeByType
(
    @LoaiCa NVARCHAR(20)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        CASE 
            WHEN @LoaiCa = N'Ca sáng' THEN CAST('06:00' AS TIME)
            WHEN @LoaiCa = N'Ca chiều' THEN CAST('14:00' AS TIME)
            
        END AS GioBatDau,
        CASE 
            WHEN @LoaiCa = N'Ca sáng' THEN CAST('14:00' AS TIME)
            WHEN @LoaiCa = N'Ca chiều' THEN CAST('22:00' AS TIME)
            
        END AS GioKetThuc
);
GO

-- 14. FUNCTION KIỂM TRA NHÂN VIÊN CÓ THỂ LÀM CA KHÔNG
CREATE OR ALTER FUNCTION fn_CanEmployeeWorkShift
(
    @NhanVienID INT,
    @NgayLamViec DATE,
    @LoaiCa NVARCHAR(20)
)
RETURNS BIT
AS
BEGIN
    DECLARE @CanWork BIT = 1;
    DECLARE @GioBatDau TIME, @GioKetThuc TIME;
    
    -- Lấy giờ của ca
    SELECT @GioBatDau = GioBatDau, @GioKetThuc = GioKetThuc
    FROM fn_GetShiftTimeByType(@LoaiCa);
    
    -- Kiểm tra nhân viên active
    IF dbo.fn_IsEmployeeActive(@NhanVienID) = 0
    BEGIN
        SET @CanWork = 0;
        RETURN @CanWork;
    END
    
    -- Kiểm tra xung đột thời gian
    IF dbo.fn_CheckTimeConflict(@NhanVienID, @NgayLamViec, @GioBatDau, @GioKetThuc, NULL) = 1
    BEGIN
        SET @CanWork = 0;
        RETURN @CanWork;
    END
    
    -- Kiểm tra giới hạn giờ làm
    IF dbo.fn_CheckDailyHourLimit(@NhanVienID, @NgayLamViec, @GioBatDau, @GioKetThuc, NULL) = 0
    BEGIN
        SET @CanWork = 0;
        RETURN @CanWork;
    END
    
    RETURN @CanWork;
END
GO

