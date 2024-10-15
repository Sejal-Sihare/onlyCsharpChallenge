using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem_Entity
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int CustomerId { get; set; }
        public int PrincipalAmount { get; set; }
        public int InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string loanStatus { get; set; }
    }
}

