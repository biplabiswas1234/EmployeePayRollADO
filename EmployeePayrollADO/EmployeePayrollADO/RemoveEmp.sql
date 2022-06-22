use [EmpPay]

Create PROCEDURE RemoveEmpDetails
 --add the parameters for the stored procedure here
@ID int
AS
SET XACT_ABORT ON;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
 --set NOCOUNT on addded to prevent extra result sets from interfering with select statments.
 SET NOCOUNT ON;
 DECLARE @result bit = 0;
 DECLARE @Emp_ID int = 0;
  --insert statements for procedure here 
set @Emp_ID = (SELECT ID from EmployeePayroll where ID = @ID);

IF (@Emp_ID = @ID)
BEGIN;
delete from EmployeePayroll where ID = @Emp_ID
END
ELSE
 BEGIN;
 THROW 50001, 'employee dont exist', -1;
 END

 COMMIT TRANSACTION
 set @result = 1;
 return @result;
 END TRY
 BEGIN CATCH

 IF (XACT_STATE()) = -1
 BEGIN
 PRINT
  'transaction is uncommitable' + 'rolling back transaction'
	ROLLBACK TRANSACTION;
	return @result;
	END;
 ELSE IF(XACT_STATE()) = 1
 BEGIN
 PRINT
  'transaction is commitable' + 'commiting back transaction'
  COMMIT TRANSACTION;
  set @result = 1;
  return @result;
  END
END CATCH
END

Exec RemoveEmpDetails 1