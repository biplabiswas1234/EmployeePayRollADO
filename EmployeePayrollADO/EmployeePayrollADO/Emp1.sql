create table EmployeePayroll
(
ID int identity(1,1) primary key not null,
Name varchar(10),
Gender varchar(1),
PhoneNumber bigint,
Address varchar(10), 
StartDate Date
);
--drop table EmployeePayroll
create table Payroll
(
ID int not null,
BasicPay int,
TaxablePay int,
Deduction int,
IncomeTax int,
NetPay int,
constraint Payroll_foreign_Key_EmpID foreign key(ID) references EmployeePayroll(ID) on delete cascade
);
select * from Payroll


create database EmpPayRollService
use [EmpPayRollService]

create PROCEDURE InsertEmployeeDetails 
(@ID int,
@Name varchar(50),
@Gender varchar(1),
@PhoneNumber bigint,
@Address varchar(50),
@StartDate date,
@Department varchar(50),
@BasicPay bigint,
@Deduction int,
@TaxablePay int,
@IncomeTax int,
@NetPay int
)
select * from EmployeePayroll
AS
SET XACT_ABORT ON;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;
declare @new_identity int = 0
declare @result bit = 0;
Insert into EmployeePayroll(Name,Gender,PhoneNumber,Address, StartDate) Values (@Name,@Gender,@PhoneNumber,@Address,@StartDate);
select @new_identity = @@IDENTITY
insert into Payroll(Payroll.ID,BasicPay,Deduction,TaxablePay,IncomeTax,NetPay) values (@new_identity,@BasicPay,@Deduction,@TaxablePay,@IncomeTax,@NetPay);
insert into DepartmentTable (DepartmentTable.ID,Department)values(@new_identity,@Department);
Commit Transaction
Set @result = 1;
return @result;
END TRY
Begin Catch

if(XACT_STATE()) = -1
 Begin
  Print
  'transaction is uncommitable' + 'rolling back transaction'
 ROLLBACK TRANSACTION;
 RETURN @result;
 End;
else if (XACT_STATE()) = 1
Begin
Print
   'transaction is commitable' + 'commiting back transaction'
   COMMIT TRANSACTION
   set @result = 1;
   return @result;
   END
END Catch
END


select EmployeePayroll.ID, Name,Gender, PhoneNumber, Address,StartDate , Department,BasicPay,Deduction,TaxablePay, IncomeTax, NetPay from
EmployeePayroll left join DepartmentTable on EmployeePayroll.ID = DepartmentTable.Id left join Payroll on EmployeePayroll.ID = Payroll.ID

Exec InsertEmployeeDetails 1,'Biplab','M',98989898,'Pune','2022-02-02','IT',30000,100,100,100,27000