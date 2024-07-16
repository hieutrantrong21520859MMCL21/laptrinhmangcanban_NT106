create database QuanLyNhaHang
use QuanLyNhaHang

-- Tao bang TaiKhoan de luu tru thong tin nguoi dung
create table TaiKhoan
(
	MaTK varchar(4) primary key,
	TenDangNhap varchar(20) not null,
	HoTen nvarchar(50) not null,
	MatKhau varchar(50) not null,
	VaiTro nvarchar(10) not null
)

-- Tao bang KhachHang
create table KhachHang
(
	MaKH varchar(4) primary key,
	HoTen nvarchar(50) not null,
	SoDienThoai varchar(10) not null
)

-- Tao bang NhanVien
create table NhanVien
(
	MaNV varchar(4) primary key,
	Hoten nvarchar(50) not null,
	NgayVL smalldatetime not null,
	Luong int not null,
)

-- Tao bang Ban
create table Ban
(
	MaBan varchar(3) primary key,
	TinhTrangBan nvarchar(10) not null
)

-- Tao bang ThucDon
create table ThucDon
(
	MaMon varchar(4) primary key,
	TenMon nvarchar(50) not null,
	DonVi nvarchar(10) not null,
	Gia int not null,
)

-- Tao bang HoaDon
create table HoaDon
(
	MaHD varchar(4) primary key,
	MaNV varchar(4) foreign key references NhanVien(MaNV),
	MaKH varchar(4) foreign key references KhachHang(MaKH),
	MaBan varchar(3) foreign key references Ban(MaBan),
	NgayHD smalldatetime not null,
	TriGia int not null,
)

-- Tao bang ChiTietHoaDon
create table ChiTietHoaDon
(
	MaHD varchar(4) foreign key references HoaDon(MaHD),
	MaMon varchar(4) foreign key references ThucDon(MaMon),
	SoLuong int not null,
	constraint PK_CTHD primary key (MaHD, MaMon)
)

-- Them du lieu vao bang TaiKhoan
insert into TaiKhoan values ('TK01', 'admin', N'Lê Quốc Khánh', 'z+B4CO0uv/A=', N'Quản lý')
insert into TaiKhoan values ('TK02', 'hieudeptrai', N'Trần Trọng Hiếu', 'HA0gFKkJpH6uV7gbJcB30A==', N'Nhân viên')

-- Them du lieu vao bang KhachHang
insert into KhachHang values ('KH01', N'Nguyễn Mai Ngọc', '0825452467')
insert into KhachHang values ('KH02', N'Trần Minh Long', '0917325476')

-- Them du lieu vao bang NhanVien
insert into NhanVien values ('NV01', N'Ngô Thanh Tuấn', '13/4/2006', 15800000)
insert into NhanVien values ('NV02', N'Lê Thị Phi Yến', '21/4/2006', 10500000)

-- Them du lieu vao bang Ban
insert into Ban values ('T01', N'Trống')
insert into Ban values ('T02', N'Trống')
insert into Ban values ('T03', N'Trống')
insert into Ban values ('T04', N'Trống')
insert into Ban values ('T05', N'Trống')
insert into Ban values ('T06', N'Trống')

-- Them du lieu vao bang ThucDon
insert into ThucDon values ('MA01', N'Gà xào sả ớt', N'Đĩa', 50000)
insert into ThucDon values ('MA02', N'Cá lóc hấp bầu', N'Đĩa', 70000)
insert into ThucDon values ('MA03', N'Cơm chiên dương châu', N'Đĩa', 55000)
insert into ThucDon values ('MA04', N'Bún chả Obama', N'Tô', 100000)
insert into ThucDon values ('MA05', N'Vịt quay nguyên con', N'Đĩa', 120000)


select * from ChiTietHoaDon

delete from Ban
alter table NhanVien alter column Luong int
alter table HoaDon alter column TriGia int
update Ban set TinhTrangBan=N'Trống'