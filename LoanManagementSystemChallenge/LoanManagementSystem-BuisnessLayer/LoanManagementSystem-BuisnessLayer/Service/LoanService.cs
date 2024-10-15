using LoanManagementSystem_BuisnessLayer.Repository;
using LoanManagementSystem_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem_BuisnessLayer.Service
{
    public class LoanService : ILoanService
    {

        ILoanRepository _loanRepository;

       public  LoanService(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public bool ApplyLoan(Loan loan)
        {
            return _loanRepository.ApplyLoan(loan);
        }
        public decimal calculateEMI(int loanId)
        {
            return _loanRepository.calculateEMI(loanId);
        }
       

        public int CalculateInterest(int LoanId)
        {
            return _loanRepository.CalculateInterest(LoanId);
        }

        public void getAllLoan()
        {
            _loanRepository.getAllLoan();
        }

        public void getLoanById(int LoanId)
        {
           _loanRepository.getLoanById(LoanId);
        }

         public void loanRepayment(int loanId, int amount)
        {
            _loanRepository.loanRepayment(loanId, amount);
        }

        public string LoanStatus(int LoanId)
        {
            return _loanRepository.LoanStatus(LoanId);
        }
    }
}
