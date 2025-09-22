USE vc;
GO

-- 1. BẢNG LOẠI CSVC
INSERT INTO LoaiCSVC (TenLoai, MoTa)
VALUES 
(N'Thiết bị điện', N'Đèn, quạt, máy lạnh, ổ cắm'),
(N'Nội thất', N'Bàn, ghế, kệ trưng bày'),
(N'Thiết bị văn phòng', N'Máy in, máy tính, máy quét');
GO

-- 2. BẢNG VỊ TRÍ
INSERT INTO ViTri (Tang, KhuVuc, MoTa)
VALUES
(1, N'Quầy thu ngân', N'Khu vực thanh toán chính'),
(1, N'Khu thực phẩm', N'Trưng bày thực phẩm đông lạnh, tươi sống'),
(2, N'Phòng quản lý', N'Nơi làm việc của nhân viên quản lý'),
(2, N'Kho hàng', N'Kho chứa hàng hóa');
GO

-- 3. BẢNG NHÂN VIÊN
INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, ChucVu)
VALUES
(N'Nguyễn Văn A', '1995-05-12', N'Nam', N'123 Q1, TP.HCM', '0911222333', 'nv.a@coopmart.vn', N'Nhân viên trực'),
(N'Trần Thị B', '1998-08-20', N'Nữ', N'45 Q3, TP.HCM', '0933444555', 'nv.b@coopmart.vn', N'Nhân viên kỹ thuật'),
(N'Lê Văn C', '2000-03-15', N'Nam', N'12 Q5, TP.HCM', '0944555666', 'nv.c@coopmart.vn', N'Nhân viên trực'),
(N'Phạm Thị D', '1997-11-22', N'Nữ', N'78 Q7, TP.HCM', '0977665544', 'nv.d@coopmart.vn', N'Nhân viên kỹ thuật');
GO

-- 4. BẢNG THÔNG TIN LƯƠNG NHÂN VIÊN
INSERT INTO ThongTinLuongNhanVien (NhanVienID, Thang, Nam, LuongCoBanTheoGio)
VALUES
(1, 9, 2025, 30000),
(2, 9, 2025, 40000),
(3, 9, 2025, 30000),
(4, 9, 2025, 40000);
GO

-- 5. BẢNG CƠ SỞ VẬT CHẤT
INSERT INTO CSVC (TenCSVC, MaCSVC, LoaiID, ViTriID, GiaTri, TenNhaCungCap, SoDienThoaiNCC, EmailNCC, TinhTrang, GhiChu)
VALUES
(N'Quạt trần Panasonic', 'QT001', 1, 1, 2500000, N'Công ty Điện Máy Hòa Phát', '0909123456', 'dienmayhp@gmail.com', N'Đang sử dụng', N'Lắp tại quầy thu ngân'),
(N'Bàn thu ngân gỗ công nghiệp', 'BT001', 2, 1, 5000000, N'Công ty Nội Thất Sài Gòn', '0912345678', 'noithatsaigon@gmail.com', N'Đang sử dụng', N'Dùng cho quầy thu ngân số 1'),
(N'Máy in hóa đơn Epson', 'MI001', 3, 3, 3500000, N'Công ty Văn Phòng Phẩm An Phú', '0922334455', 'anphuoffice@gmail.com', N'Bảo trì', N'Đang kiểm tra trục in'),
(N'Kệ trưng bày đông lạnh', 'KE001', 2, 2, 7000000, N'Công ty Nội Thất Sài Gòn', '0912345678', 'noithatsaigon@gmail.com', N'Đã thanh lý', N'Kệ đã thay mới, chuẩn bị bỏ');
GO

-- 6. BẢNG PHÂN CÔNG CA (chỉ trong ngày, không qua hôm sau)
INSERT INTO PhanCongCa (NhanVienID, TenCa, GioBatDau, GioKetThuc, NgayLamViec, VaiTroTrongCa, GhiChu)
VALUES
(1, N'Ca sáng', '06:00', '14:00', '2025-09-01', N'Nhân viên trực', N'Phụ trách quầy thu ngân'),
(2, N'Ca sáng', '06:00', '14:00', '2025-09-01', N'Nhân viên kỹ thuật', N'Hỗ trợ kỹ thuật tại quầy'),
(3, N'Ca chiều', '14:00', '22:00', '2025-09-01', N'Nhân viên trực', N'Trông coi khu thực phẩm'),
(4, N'Ca chiều', '14:00', '22:00', '2025-09-01', N'Nhân viên kỹ thuật', N'Hỗ trợ kỹ thuật buổi chiều');
GO

-- 7. BẢNG THÔNG TIN SỬ DỤNG
INSERT INTO ThongTinSuDung (CSVCID, NgayMua, NgayHetBaoHanh, ThoiGianSuDungDuKien_Thang, GhiChu)
VALUES
(1, '2023-01-10', '2026-01-10', 60, N'Còn bảo hành'),
(2, '2022-05-15', '2024-05-15', 48, N'Hết bảo hành'),
(3, '2024-07-20', '2026-07-20', 36, N'Đang trong thời gian bảo trì'),
(4, '2022-08-10', '2025-08-10', 72, N'Đã thanh lý, không còn sử dụng');
GO

-- 8. BẢNG LỊCH SỬ BẢO TRÌ
INSERT INTO LichSuBaoTri (CSVCID, NgayYeuCau, NgayHoanThanh, NoiDung, ChiPhi, NhanVienGiamSatID, NhanVienKyThuatID, TrangThai)
VALUES
(1, '2025-08-01', '2025-08-05', N'Bảo dưỡng quạt trần', 200000, 1, 2, N'Hoàn thành'),
(2, '2025-08-15', NULL, N'Sửa chân bàn thu ngân', 0, 1, 4, N'Chờ xử lý');
GO

-- 9. BẢNG LỊCH BẢO TRÌ ĐỊNH KỲ
INSERT INTO LichBaoTri (CSVCID, ChuKyBaoTri_Thang, NgayBatDau, GhiChu)
VALUES
(1, 12, '2025-01-01', N'Bảo trì định kỳ hằng năm'),
(3, 6, '2025-07-20', N'Bảo trì định kỳ máy in hóa đơn');
GO

-- 10. BẢNG THANH LÝ CSVC
INSERT INTO ThanhLyCSVC (CSVCID, NgayThanhLy, LyDoThanhLy, GiaTriThanhLy, NguoiThucHienID)
VALUES
(4, '2025-09-10', N'Kệ hỏng nặng, đã thay mới', 1000000, 1);
GO
