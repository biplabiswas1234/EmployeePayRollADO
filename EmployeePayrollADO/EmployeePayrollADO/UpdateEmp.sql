use [EmpPay]

CREATE PROCEDURE UpdateEmpDetails
@ID int,
@Name varchar(50),
@BasicPay Money
AS
SET XACT_ABORT ON;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
  DECLARE @row_count INTEGER
 
    update Payroll Set BasicPay = @BasicPay where ID = @ID;

select EmployeePayroll.ID, Name,Gender, PhoneNumber, Address,StartDate , Department,BasicPay,Deduction,TaxablePay, IncomeTax, NetPay from
EmployeePayroll left join DepartmentTable on EmployeePayroll.ID = DepartmentTable.Id left join Payroll on EmployeePayroll.ID = Payroll.ID

 SELECT @row_count = @@ROWCOUNT
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
SELECT ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
IF(XACT_STATE()) = -1
 BEGIN
  PRINT
  'transaction is uncommitable' + 'rolling back transaction'
  ROLLBACK TRANSACTION;
  END;
ELSE IF(XACT_STATE()) = 1
 BEGIN
  PRINT
   'transaction is commitable' + 'rolling back transaction'
   COMMIT TRANSACTION;
 END;
ELSE
 BEGIN
 PRINT
  'transaction is failed'
  ROLLBACK TRANSACTION;
  END;
END CATCH
 return @row_count
END