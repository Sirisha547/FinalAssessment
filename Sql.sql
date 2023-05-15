create database ProductManagement

use ProductManagement

create table Products
(
ProductId int identity primary key,
ProductName varchar(50),
Descrp varchar(100),
Quantity int,
Price Bigint
)

select * from Products