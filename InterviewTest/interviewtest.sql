-- Table configuration for orders and returns
-- Run this script in you database
create table
    orders (
        orderId int auto_increment not null,
        orderNumber varchar(255) not null,
        customerName varchar(255) not null,
        productNumber varchar(255) not null,
        orderDate varchar(255) not null,
        Primary Key (orderId)
    );

create index idx_orderNumber on orders (orderNumber);

create table
    returns (
        returnId int auto_increment not null,
        returnNumber varchar(255) not null,
        orderNumber varchar(255) not null,
        productNumber varchar(255) not null,
        Primary Key (returnId),
        Foreign Key (orderNumber) references orders (orderNumber)
    );