using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.Helpers
{
    public class ADOHelper
    {
        string connectionString;

        public ADOHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ExecuteScript(string script)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    connection.Open();                                        
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
