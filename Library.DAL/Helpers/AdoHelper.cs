using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Helpers
{
    public class ADOHelper
    {
        string connectionString;

        public ADOHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }      

        public bool ExecuteProcedure(string procedureName, Action<SqlDataReader> executeAction, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);                        
                    }                  
     
                    var reader = command.ExecuteReader();

                    executeAction(reader);
                    
                    reader.Close();
                }
            }

            return true;
        }

        public bool ExecuteProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            var result = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                   result = command.ExecuteNonQuery();
                }
            }

            return result > -1;
        }

        internal void ExecuteProcedure(object sp_GetAuthersByBookId, Action<SqlDataReader> p, SqlParameter[] sqlParameters)
        {
            throw new NotImplementedException();
        }
    }
}
