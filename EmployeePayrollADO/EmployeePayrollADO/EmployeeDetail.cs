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
        public int InsertEmployeeData(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertEmployeeDetails", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ID", employee.ID);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    command.Parameters.AddWithValue("@Deduction", employee.Deduction);
                    command.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", employee.IncomeTax);
                    command.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    var returnParameter = command.Parameters.Add("@new_identity", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    Console.WriteLine(employee.ID + "," + employee.Name + "," + employee.StartDate + "," + employee.Gender + "," + employee.PhoneNumber + "," + employee.Address + "," + employee.Department + "," + employee.BasicPay + "," + employee.Deduction + "," + employee.TaxablePay + "," + employee.IncomeTax + "," + employee.NetPay);
                    command.ExecuteNonQuery();
                    connection.Close();
                    var result = returnParameter.Value;
                    return (int)result;
                    Console.WriteLine("Contact is added");
                }
            }
            catch (Exception)
            {
                throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.INSERTION_ERROR, "Insertion error");
            }
            return 0;
        }
        public Employee UpdateSalary(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("UpdateEmpDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", employee.ID);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    employee = new Employee();
                    connection.Open();
                    SqlDataReader rd = command.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            employee.ID = (int)rd["ID"];
                            employee.Name = (string)rd["Name"];
                            employee.StartDate = (DateTime)rd["StartDate"];
                            employee.Gender = (string)rd["Gender"];
                            employee.PhoneNumber = (Int64)rd["PhoneNumber"];
                            employee.Address = (string)rd["Address"];
                            employee.Department = (string)rd["Department"];
                            employee.BasicPay = (Int32)rd["BasicPay"];
                            employee.Deduction = (Int32)rd["Deduction"];
                            employee.TaxablePay = (Int32)rd["TaxablePay"];
                            employee.IncomeTax = (Int32)rd["IncomeTax"];
                            employee.NetPay = (Int32)rd["NetPay"];
                        }
                        if (employee == null)
                        {
                            throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.NO_DATA_FOUND, "Data not found");
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.NO_DATA_FOUND, "Data not found");
            }
            return employee;
        }
        public List<Employee> EmployeeData_InDataRange(DateTime fromDate, DateTime toDate)
        {
            Employee employee = new Employee();
            List<Employee> employeeData = new List<Employee>();
            SqlConnection connection = new SqlConnection(connectionstring);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("EmpInDateRange", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    connection.Open();
                    SqlDataReader rd = command.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            employee = new Employee
                            {
                                ID = rd.IsDBNull(0) ? default : rd.GetInt32(0),
                                Name = rd.IsDBNull(1) ? default : rd.GetString(1),
                                StartDate = rd.IsDBNull(11) ? default : rd.GetDateTime(2),
                                Gender = rd.IsDBNull(5) ? default : rd.GetString(3),
                                PhoneNumber = rd.IsDBNull(2) ? default : rd.GetInt64(4),
                                Address = rd.IsDBNull(3) ? default : rd.GetString(5),
                                Department = rd.IsDBNull(4) ? default : rd.GetString(6),
                                BasicPay = rd.IsDBNull(6) ? default : rd.GetInt32(7),
                                Deduction = rd.IsDBNull(7) ? default : rd.GetInt32(8),
                                TaxablePay = rd.IsDBNull(8) ? default : rd.GetInt32(9),
                                IncomeTax = rd.IsDBNull(9) ? default : rd.GetInt32(10),
                                NetPay = rd.IsDBNull(10) ? default : rd.GetInt32(11),
                            };
                            employeeData.Add(employee);
                            Console.WriteLine(employee.ID + "," + employee.Name + "," + employee.StartDate + "," + employee.Gender + "," + employee.PhoneNumber + "," + employee.Address + "," + employee.Department + "," + employee.BasicPay + "," + employee.Deduction + "," + employee.TaxablePay + "," + employee.IncomeTax + "," + employee.NetPay);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.NO_DATA_FOUND, "Data not found");
            }
            return employeeData;
        }
        public bool RemoveDetails(Employee employee)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("RemoveEmpDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", employee.ID);
                    connection.Open();
                    var result = command.ExecuteNonQuery();

                    if (result != 0)
                    {
                        Console.WriteLine("Contact is deleted");
                        return true;
                    }
                    return false;
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new EmployeePayrollException(EmployeePayrollException.ExceptionType.NO_DATA_FOUND, "Data not found");
            }
        }
    }
}
