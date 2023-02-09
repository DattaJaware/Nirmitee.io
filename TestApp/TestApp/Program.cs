using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.Write("Please Enter following Data ");
            Console.WriteLine();
            p.GetInput(0);
        }
        EmpDetails emp = new EmpDetails();
        public void validateData(string inputData, int i)
        {
            switch (i)
            {
                case (int)Inputs.EmpName:
                    emp.EmpName = inputData;
                    GetInput(i + 1);
                    break;
                case (int)Inputs.BasicSalary:
                case (int)Inputs.ConvAllow:
                case (int)Inputs.MedicalAlow:
                case (int)Inputs.HRA:
                    try
                    {
                        bool isHRA = true;
                        if (i == (int)Inputs.BasicSalary)
                            emp.BasicSalary = Convert.ToDouble(inputData);
                        else if (i == (int)Inputs.ConvAllow)
                            emp.ConvAllow = Convert.ToDouble(inputData);
                        else if (i == (int)Inputs.MedicalAlow)
                            emp.MedicalAlow = Convert.ToDouble(inputData);
                        else if (i == (int)Inputs.HRA)
                        {
                            emp.HRA = Convert.ToDouble(inputData);
                            if (emp.HRA > 100)
                            {
                                Console.WriteLine("Please HRA percentage less than 100 percent.");
                                isHRA = false;
                                GetInput(i);
                            }
                        }
                        if(isHRA)
                        GetInput(i + 1);

                    }
                    catch
                    {
                        Console.WriteLine("Please Enter Valid Data");
                        GetInput(i);
                    }
                    break;
                case (int)Inputs.Days:
                    try
                    {
                        if (i == (int)Inputs.Days)
                        {
                            emp.Days = Convert.ToInt32(inputData);
                            if (emp.Days > getCurrentMonth())
                            {
                                Console.WriteLine("Please Enter number of days less than or equal to curent month days");
                                GetInput(i);
                            }
                            else
                            {
                                GetInput(i + 1);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please Enter Valid Data");
                        GetInput(i);
                    }
                    break;
                default:
                    CalculateSalaryTax(emp);
                    break;

            }
        }
        public enum Inputs
        {
            EmpName = 0,
            BasicSalary = 1,
            Days = 2,
            HRA = 3,
            ConvAllow = 4,
            MedicalAlow = 5,
        }
        public enum Month
        {
            JAN = 31,
            FEB = 28,
            MAR = 31,
            APR = 30,
            MAY = 31,
            JUN = 30,
            JUL = 31,
            AUG = 31,
            SEP = 30,
            OCT = 31,
            NOV = 30,
            DEC = 31
        }
        public string GetInput(int i)
        {
            if (i == (int)Inputs.EmpName)
            {
                Console.Write("Employee Name: ");
                validateData(Console.ReadLine(), i);
            }
            else if (i == (int)Inputs.BasicSalary)
            {
                Console.Write("Basic Salary: ");
                validateData(Console.ReadLine(), i);
            }
            else if (i == (int)Inputs.Days)
            {
                Console.Write("Number of days worked in a month: ");
                validateData(Console.ReadLine(), i);
            }
            else if (i == (int)Inputs.HRA)
            {
                Console.Write("HRA Percentage(e.g. 20%): ");
                validateData(Console.ReadLine(), i);
            }
            else if (i == (int)Inputs.ConvAllow)
            {
                Console.Write("Conveyanance Allowance Amount: ");
                validateData(Console.ReadLine(), i);
            }
            else if (i == (int)Inputs.MedicalAlow)
            {
                Console.Write("Medical Allowance Amount: ");
                validateData(Console.ReadLine(), i);
            }
            else validateData("", -1);

            return "";
        }
        public int getCurrentMonth()
        {
            DateTime now = DateTime.Now;
            string currentMonth = now.ToString("MMM").ToUpper();
            return (int)Enum.Parse(typeof(Month), currentMonth);
        }
        public void CalculateSalaryTax(EmpDetails emp)
        {

            //NOTE : month is not defined so taking current month to calculate salary.

            //Salary Calculation.
            int value = getCurrentMonth();
            double daySalary = (emp.BasicSalary / value);
            double salary = daySalary * emp.Days;

            //Taxincome Calculation.
            //considering HRA is n percentage of basic salary
            double HRA = (emp.HRA / 100) * emp.BasicSalary;
            double TaxableIncome = salary - (HRA + emp.ConvAllow + emp.MedicalAlow);

            //Tax Amount Calculaton.
            double taxPercentage = TaxableIncome <= 250000 ? 0 : (TaxableIncome <= 500000 ? 5 : (TaxableIncome <= 100000 ? 20 : 30));
            double tax = (taxPercentage / 100) * TaxableIncome;

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Output : ");
            Console.WriteLine();
            Console.WriteLine("Employee Name : " + emp.EmpName);
            Console.WriteLine("Salary        : " + salary.ToString("N3"));
            Console.WriteLine("Taxable Income: " + TaxableIncome.ToString("N3"));
            Console.WriteLine("Tax Amount   : " + tax.ToString("N3"));
            Console.ReadLine();
        }
    }
    public class EmpDetails
    {
        public string EmpName { get; set; }
        public double BasicSalary { get; set; }
        public double ConvAllow { get; set; }
        public double MedicalAlow { get; set; }
        public double HRA { get; set; }
        public int Days { get; set; }
    }

    //Test Cases
    //1. Basic salary,No of Days worked,HRA , Conveyance allowance,Medical Allowance should be number
    //2. No of Days worked should be less than current month (consider checking output for current month)
    //3. HRA percentage should be less than 100%
    //4. if input is invalid and not supported in above format the ask again to give input with message
}
