use [EmpPay]

Alter PROCEDURE EmpInDateRange
@FromDate Date,
@ToDate Date 

AS
SET XACT_ABORT ON;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
SET NOCOUNT ON;

declare @result bit = 0;

select EmployeePayroll.ID, Name, StartDate, Gender, PhoneNumber, Address, Department,BasicPay,Deduction,TaxablePay, IncomeTax, NetPay from
EmployeePayroll left join DepartmentTable on EmployeePayroll.ID = DepartmentTable.Id left join Payroll on EmployeePayroll.ID = Payroll.ID where StartDate between @FromDate and @ToDate
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

Exec EmpInDateRange '2020-01-01' , '2020-10-01'
select * from EmployeePayroll
