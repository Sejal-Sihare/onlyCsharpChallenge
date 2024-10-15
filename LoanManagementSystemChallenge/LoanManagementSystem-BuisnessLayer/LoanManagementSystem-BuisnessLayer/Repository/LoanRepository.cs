using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using LoanManagement_DButil;
using LoanManagementSystem_Entity;

namespace LoanManagementSystem_BuisnessLayer.Repository
{
    public class LoanRepository : ILoanRepository
    {
        public bool ApplyLoan(Loan loan)
        {
            bool status = false;
            using (SqlConnection conn = DButil.getDBConnection())
            {
                conn.Open();
                string query = "insert into Loan (LoanId,CustomerId,PrincipalAmount,InterestRate,LoanTerm,LoanType,loanStatus)values(@LoanId,@CustomerId,@PrincipalAmount,@InterestRate,@LoanTerm,@LoanType,@loanStatus);";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
                cmd.Parameters.AddWithValue("@CustomerId", loan.CustomerId);
                cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);
                cmd.Parameters.AddWithValue("@loanStatus", loan.loanStatus);

                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                   status = true;
                }
               
            }
            return status;
        }

        public int CalculateEmi(int LoanId)
        {
            throw new NotImplementedException();
        }
        // Calculating the interest
        public int CalculateInterest(int LoanId)
        {
           using(var conn = DButil.getDBConnection())
            {
                int result =0;
               

                string query = "select PrincipalAmount,InterestRate,LoanTenure from Loan";

                SqlCommand cmd = new SqlCommand(query, conn);


                SqlDataReader sqlDataReader = cmd.ExecuteReader();
             

                while (sqlDataReader.Read())
                {
                 int  pa = Convert.ToInt32(sqlDataReader[0]);
                  int  ir = Convert.ToInt32(sqlDataReader[1]);
                  int  lt = Convert.ToInt32(sqlDataReader[2]);
                    result = (int)(pa * ir * lt) / 12;

                    return result;
                }

                return result;

            }
        }
        
        // Getting details of all the Loans
        public void getAllLoan()
        {
            using (var conn = DButil.getDBConnection())
            { // Open connection
                conn.Open();
                string query = "select * from Loan";

                SqlCommand cmd = new SqlCommand(query, conn);
                // getting the Loandetail for a specific  LoanId

                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                Console.WriteLine("LoanID\tCustomerID\tPrincipalAmount\tInterestRate\tLoanTerm\tLoanType\tLoanStatus ");


                while (sqlDataReader.Read())
                {
                    Console.WriteLine(sqlDataReader[0] + "\t\t " + sqlDataReader[1]

                        + "\t \t " + sqlDataReader[2] + "\t\t " + sqlDataReader[3] + "\t \t " + sqlDataReader[4] + "\t\t " + sqlDataReader[5] + "\t \t" + sqlDataReader[6]);
                }
            }
        }
        // getting the loan by Id of loan
        public void getLoanById(int LoanId)
        {
            using (SqlConnection conn = DButil.getDBConnection())
            {
                conn.Open();
                string query = "select * from Loan where LoanId = '" + LoanId + "'";
                SqlCommand cmd = new SqlCommand(query, conn);


                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                Console.WriteLine("LoanID\tCustomerID\tPrincipalAmount\tInterestRate\tLoanTerm\tLoanType\tLoanStatus ");


               if(sqlDataReader.Read())
                {
                    Console.WriteLine(sqlDataReader[0] + "\t\t " + sqlDataReader[1]

                        + "\t \t " + sqlDataReader[2] + "\t\t " + sqlDataReader[3] + "\t \t " + sqlDataReader[4] + "\t\t " + sqlDataReader[5] + "\t \t" + sqlDataReader[6]);
                }
            }
        }

        public decimal calculateEMI(int loanId)
        {
            string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanId = @LoanId";

            using (var conn =DButil.getDBConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal principalAmount = reader.GetDecimal(0);
                        decimal annualInterestRate = reader.GetDecimal(1);
                        int loanTerm = reader.GetInt32(2);

                        // Calculate monthly interest rate
                        decimal monthlyInterestRate = annualInterestRate / 12 / 100;

                        // Calculate EMI using the formula
                        decimal emi = (principalAmount * monthlyInterestRate * (decimal)Math.Pow((double)(1 + monthlyInterestRate), loanTerm)) /
                                      ((decimal)Math.Pow((double)(1 + monthlyInterestRate), loanTerm) - 1);

                        return emi;
                    }
                    else
                    {
                        throw new InvalidLoanException($"Loan with ID {loanId} not found.");
                    }
                }
            }
        }

        public string LoanStatus(int LoanId)
        {
           string status = null;
            using (SqlConnection conn = DButil.getDBConnection())
            {// open Coonection
                conn.Open();
                string query = "select LoanStatus from Loan where LoanId = '" + LoanId + "'";
// Getting the loan status

                SqlCommand cmd = new SqlCommand(query, conn);


                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                Console.WriteLine("LoanStatus ");


                while (sqlDataReader.Read())
                {
                    status = Convert.ToString(sqlDataReader[0]);
                    return status;
                }

            }
            return status;
        }
        //Claculating Loan Repayment
        public void loanRepayment(int loanId, int amount)
        {
            Decimal payableEMI = calculateEMI(loanId);

            string selectQuery = "SELECT PrincipalAmount FROM Loan WHERE LoanId = @LoanId";

            using (var conn = DButil.getDBConnection())
            {
                // Open the connection
                conn.Open();

                // Execute select query to retrieve PrincipalAmount
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        // Loan not found, throw an exception or return
                        throw new ArgumentException("Loan not found for the provided loan ID.");
                    }

                    int principalAmount = reader.GetInt32(0);

                    // Close the reader after use
                    reader.Close();

                    // If amount is less than payableEMI, reject the payment
                    if (amount < payableEMI)
                    {
                        throw new ArgumentException("Amount cannot be less than the payable EMI.");
                    }
                    else
                    {
                        Decimal numberOfEMIsPaid = amount / payableEMI;

                        if (principalAmount % payableEMI == 0)
                        {
                            Console.WriteLine("Your EMI payment is accepted.");
                            Console.WriteLine("Thanks for paying for {0} EMIs", numberOfEMIsPaid);
                        }
                        else
                        {
                            Console.WriteLine("Partial payment accepted, excess will be refunded.");
                            Console.WriteLine("Thanks for paying for {0} EMIs", numberOfEMIsPaid);
                        }

                        // Updating the loan record to reflect new principal amount
                        Decimal remainingPrincipal = principalAmount - (numberOfEMIsPaid * payableEMI);

                        string updateQuery = "UPDATE Loans SET PrincipalAmount = @RemainingPrincipal WHERE LoanId = @LoanId";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@RemainingPrincipal", remainingPrincipal);
                            updateCmd.Parameters.AddWithValue("@LoanId", loanId);

                            // Execute the update query
                            int rowsAffected = updateCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Loan record updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update loan record.");
                            }
                        }
                    }
                }
            }
        }
    }
    // Subclass HomeLoan Derived from Parent Loan class
    public class HomeLoan : Loan
    {
        public string PropertyAddress;
        public int propertyvalue;

    }
    // Subclass CarLoan Derived from Parent Loan class
    public class Carloan : Loan
    {
        public string CarModel;
        public string CarValue;
    }

    
}
