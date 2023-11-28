CREATE DATABASE ThiGK_63135901
GO
USE ThiGK_63135901
GO
CREATE TABLE BoMon
(
	MaBM nvarchar(10) PRIMARY KEY,
	TenBM nvarchar(50) NOT NULL
)
GO
CREATE TABLE GiangVien
(
	MaGV nvarchar(10) PRIMARY KEY,
	HoGV nvarchar(50) NOT NULL,
	TenGV nvarchar(10) NOT NULL,
	GioiTinh bit DEFAULT(1),
	NgaySinh date,
	Email nvarchar(50),
	AnhGV nvarchar(50),
	MaBM nvarchar(10) NOT NULL FOREIGN KEY REFERENCES BoMon(MaBM)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO

-- Dữ liệu cho bảng Lop
INSERT INTO BoMon (MaBM, TenBM)
VALUES
('BM001', 'English'),
('BM002', 'IT'),
('BM003', 'Physics');

-- Dữ liệu cho bảng SinhVien
INSERT INTO GiangVien (MaGV, HoGV, TenGV, GioiTinh, NgaySinh, AnhGV, Email, MaBM)
VALUES
('GV001', 'Nguyen', 'Hai', 1, '1990-05-15', 'avatar1.jpg', 'nguyenhai@email.com', 'BM001'),
('GV002', 'Tran', 'Linh', 0, '1985-12-10', 'avatar2.jpg', 'tranlinh@email.com', 'BM002'),
('GV003', 'Pham', 'Tung', 1, '1988-08-20', 'avatar3.jpg', 'phamtung@email.com', 'BM003');

GO
DROP PROCEDURE GiangVien_TimKiem
CREATE PROCEDURE GiangVien_TimKiem
    @MaGV varchar(8)=NULL,
	@BoMon nvarchar(40)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM GiangVien
       WHERE  (1=1)
       '
IF @MaGV IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaGV LIKE ''%'+@MaGV+'%'')
              '
IF @BoMon IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (MaBM LIKE ''%'+@BoMon+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END