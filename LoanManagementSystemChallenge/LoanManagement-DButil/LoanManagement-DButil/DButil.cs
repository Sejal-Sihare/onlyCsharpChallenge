using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement_DButil
{
    public  static class DButil
    {
        public static SqlConnection getDBConnection()
        {
           
                SqlConnection conn;
            string connectionstring = "Data Source=LAPTOP-J8HVGTPS\\SQLEXPRESS;Initial Catalog=LoanManagement;Integrated Security=True";
                conn = new SqlConnection(connectionstring);

                return conn;
            }


        }
    }

