using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem_BuisnessLayer
{
    public class InvalidLoanException : Exception
    {
        public InvalidLoanException() : base("Loan Not Found Exception") { }
        public InvalidLoanException(string message) : base(message) { }

        public InvalidLoanException(string message, Exception inner) : base(message, inner) { }
    }
}
