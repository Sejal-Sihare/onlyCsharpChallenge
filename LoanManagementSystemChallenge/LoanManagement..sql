
create database LoanManagement;
use  LoanManagement;
create table Customer( CustomerId int primary key, Name varchar(50), Email varchar(50), Phone varchar(50),Address varchar(50), CreditScore int );
create table Loan( LoanId int primary key,  CustomerId int,  PrincipalAmount int,   InterestRate int, LoanTerm int,LoanType varchar(50),loanStatus varchar(50)
foreign key(CustomerId) references Customer(CustomerId) on delete cascade on update cascade);


insert into Customer(CustomerId, Name , Email, Phone,Address,CreditScore)
VALUES (01, 'Agam', 'ar@gmail.com', '9988678','Indore',50),
(02, 'chandrika', 'ChandraN@gmail.com', '877668','Chennai',30),
(03, 'Deepak', 'deepak@gmail.com', '8775676','Pune',20),
(04, 'Deepika',  'dpal@gmail.com', '563748494','Banglore',60),
(05, 'Eshita',  'sharmae@gmail.com', '4378949','Nagpur',90);
select * from Customer;


insert into Loan(LoanId ,CustomerId , PrincipalAmount,InterestRate ,LoanTerm,LoanType,loanStatus) values
(10,1,3000,5,3,'CarLoan','Approved'),
(20,2,7000,6,3,'HomeLoan','Pending'),
(30,3,2000,5,3,'CarLoan','Approved'),
(40,1,3000,5,3,'CarLoan','Pending'),
(50,1,3000,5,3,'HomeLoan','Approved');
select * from Loan;
