CREATE DATABASE KT0720_63135901
GO
USE KT0720_63135901
GO
CREATE TABLE Lop
(
	MaLop nvarchar(10) PRIMARY KEY,
	TenLop nvarchar(50) NOT NULL
)
GO
CREATE TABLE SinhVien
(
	MaSV nvarchar(10) PRIMARY KEY,
	HoSV nvarchar(50) NOT NULL,
	TenSV nvarchar(10) NOT NULL,
	GioiTinh bit DEFAULT(1),
	NgaySinh date,
	AnhSV nvarchar(50),
	DiaChi nvarchar(100) NOT NULL,
	MaLop nvarchar(10) NOT NULL FOREIGN KEY REFERENCES Lop(MaLop)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO

-- Dữ liệu cho bảng Lop
INSERT INTO Lop (MaLop, TenLop)
VALUES
('L001', '10A1'),
('L002', '11B2'),
('L003', '12C3');

-- Dữ liệu cho bảng SinhVien
INSERT INTO SinhVien (MaSV, HoSV, TenSV, GioiTinh, NgaySinh, AnhSV, DiaChi, MaLop)
VALUES
('SV001', 'Nguyen', 'Van A', 1, '2000-01-15', 'anh1.jpg', '123 Duong ABC, TP.HCM', 'L001'),
('SV002', 'Tran', 'Thi B', 0, '2001-03-20', 'anh2.jpg', '456 Duong XYZ, Ha Noi', 'L002'),
('SV003', 'Le', 'Quoc C', 1, '1999-07-10', 'anh3.jpg', '789 Duong KLM, Da Nang', 'L003');

GO
DROP PROCEDURE SinhVien_TimKiem
CREATE PROCEDURE SinhVien_TimKiem
    @MaSV varchar(8)=NULL,
	@HoTen nvarchar(40)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM SinhVien
       WHERE  (1=1)
       '
IF @MaSV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaSV LIKE ''%'+@MaSV+'%'')
              '
IF @HoTen IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoSV+'' ''+TenSV LIKE N''%'+@HoTen+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END