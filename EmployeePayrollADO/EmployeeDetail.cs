using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayrollADO
{
    public class EmployeeDetail
    {
        static string connectionstring = @"Data source=(localdb)\MSSQLLocalDB;Initial Catalog=EmpPay;Integrated Security=SSPI";
        static SqlConnection connection = new SqlConnection(connectionstring);

        public void EstablishConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.CONNECTION_FAILED, "Connection not found");
                }
            }
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.CONNECTION_FAILED, "Connection not found");
                }
            }
        }
    }
}
