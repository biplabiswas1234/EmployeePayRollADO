using System;

namespace EmployeePayrollADO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Employee empPayroll = new Employee();
            EmployeeDetail employeeDetail = new EmployeeDetail();
            int option = 0;
            do
            {
                Console.WriteLine("1: Establish Connection");
                Console.WriteLine("2: Close Connection");
                Console.WriteLine("3: Add Employee Data");
                Console.WriteLine("0: Exit");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        employeeDetail.EstablishConnection();
                        break;
                    case 2:
                        employeeDetail.CloseConnection();
                        break;
                    case 3:
                        Console.WriteLine("Enter the Name");
                        string name = Console.ReadLine();
                        empPayroll.Name = name;
                        Console.WriteLine("Enter Join Date");
                        string date = Console.ReadLine();
                        empPayroll.StartDate = Convert.ToDateTime(date);
                        Console.WriteLine("Enter the Gender");
                        string gender = Console.ReadLine();
                        empPayroll.Gender = gender;
                        Console.WriteLine("Enter Phone Number");
                        long phone = Convert.ToInt64(Console.ReadLine());
                        empPayroll.PhoneNumber = phone;
                        Console.WriteLine("Enter Address");
                        string address = Console.ReadLine();
                        empPayroll.Address = address;
                        Console.WriteLine("Enter the departmaent");
                        string department = Console.ReadLine();
                        empPayroll.Department = department;
                        Console.WriteLine("Enter Basic Pay");
                        double basicpay = Convert.ToInt64(Console.ReadLine());
                        empPayroll.BasicPay = basicpay;
                        Console.WriteLine("Enter Deduction");
                        double deduction = Convert.ToInt64(Console.ReadLine());
                        empPayroll.Deduction = deduction;
                        Console.WriteLine("Enter the Taxable Pay");
                        double tax = Convert.ToInt64(Console.ReadLine());
                        empPayroll.TaxablePay = tax;
                        Console.WriteLine("Enter the Income Tax");
                        double Income = Convert.ToInt64(Console.ReadLine());
                        empPayroll.IncomeTax = Income;
                        Console.WriteLine("Enter the Net Pay");
                        double Net = Convert.ToInt64(Console.ReadLine());
                        empPayroll.NetPay = Net;
                        employeeDetail.InsertEmployeeData(empPayroll);
                        break;
                }
            }
            while (option != 0);
        }
    }
}

