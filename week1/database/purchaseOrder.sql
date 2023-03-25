set dateformat dmy

create database purchaseOrder
use purchaseOrder

create table company
(
	id int identity(1, 1) primary key not null,
	name varchar(50),
	address varchar(50)
)

create table purchaseOrder
(
	id int identity(1, 1) primary key not null,
	purchaseOrderStatus int,
	issuanceDate datetime,
	deliveryDate datetime,
	invoiceDate datetime,
	paymentDate datetime,
	buyer int foreign key references company(id),
	vendor int foreign key references company(id),
	reference varchar(50)
)

create table lineItem
(
	id int identity(1, 1) primary key not null,
	description varchar(50),
	quantity int,
	cost money,
	purchaseOrderID int foreign key references purchaseOrder(id),
)

insert into company (name, address) values ('Wiley E. Coyote', 'sandy desert')
insert into company (name, address) values ('Acme corp.', 'big city')
insert into purchaseOrder (issuanceDate, buyer, vendor) values (getDate(), 1, 2)
insert into lineItem (description, quantity, cost, purchaseOrderID) values ('Road runner trap', 1, 100, 1)