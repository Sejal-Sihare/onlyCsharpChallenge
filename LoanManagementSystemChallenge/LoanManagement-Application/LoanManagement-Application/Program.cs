using LoanManagementSystem_BuisnessLayer.Repository;
using LoanManagementSystem_BuisnessLayer.Service;
using LoanManagementSystem_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the loan Management System");
            LoanRepository loanRepository = new LoanRepository();
            LoanService loanService = new LoanService(loanRepository);
            Console.WriteLine("To Apply for Loan type 1");
            Console.WriteLine("To Calculate Interest type 2");
            Console.WriteLine("To get Loan Status type 3");
            Console.WriteLine("To Calculate EMI type 4");
            Console.WriteLine("To Calculate Loan Repayment 5");
            Console.WriteLine("To get All the Loan Details type 6");
            Console.WriteLine("To get Loan by Id type 7");
           
            var choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(choice);

            switch (choice)
            {
                case 1:
       bool result4 = loanService.ApplyLoan(new Loan() { LoanId = 6, CustomerId = 1, PrincipalAmount = 400, InterestRate = 4, LoanTerm = 5, LoanType = "CarLoan", loanStatus = "pending"});
                    if (result4)
                    {
                        Console.WriteLine("Loan is added Successfully");
                    }
                    else
                    {
                        Console.WriteLine("Unsucessful insertion");
                    }
                    break;
                case 2:
                    int a = loanService.CalculateInterest(20);
                    Console.WriteLine($"The following is the interest{a}");
                    break;
                case 3:
                    string status= loanService.LoanStatus(10);
                    Console.WriteLine($"The following is the status{status}");
                    break;
                case 4:
                    decimal d = loanService.calculateEMI(40);
                    Console.WriteLine($"The Emi is{d}");
                    break;
                   
                case 5:
                    loanService.loanRepayment(20, 20000);
                    break;
                
                case 6:
                    loanService.getAllLoan();
                    break;

                case 7:
                    loanService.getLoanById(10);
                    break;
            }


            Console.WriteLine("**************Thank You for Shopping********");
            Console.ReadKey();

        }
    }
}
