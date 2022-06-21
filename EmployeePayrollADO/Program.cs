namespace EmployeePayrollADO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EmployeeDetail employeeDetail = new EmployeeDetail();
            employeeDetail.CloseConnection();
        }
    }
}