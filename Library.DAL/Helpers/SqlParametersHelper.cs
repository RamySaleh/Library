using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Helpers
{
    public class SqlParametersHelper
    {
        List<SqlParameter> sqlParameters;
        public SqlParametersHelper()
        {
            sqlParameters = new List<SqlParameter>();
        }

        public SqlParametersHelper AddParameter(string name,object value,SqlDbType type)
        {
            var parameter = new SqlParameter(name, type);
            parameter.Value = value;
            sqlParameters.Add(parameter);

            return this;
        }

        public SqlParameter[] GetParameters()
        {
            return sqlParameters.ToArray();
        }
    }
}
