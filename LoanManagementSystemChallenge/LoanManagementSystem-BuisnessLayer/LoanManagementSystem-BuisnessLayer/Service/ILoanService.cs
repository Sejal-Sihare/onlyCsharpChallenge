using LoanManagementSystem_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem_BuisnessLayer.Service
{
   public interface ILoanService
    {
        bool ApplyLoan(Loan loan);

        int CalculateInterest(int LoanId);

        string LoanStatus(int LoanId);

        decimal calculateEMI(int loanId);

        void loanRepayment(int loanId, int amount);
        void getAllLoan();

        void getLoanById(int LoanId);
    }
}
