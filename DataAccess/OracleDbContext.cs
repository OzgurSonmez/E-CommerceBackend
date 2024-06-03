using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public class OracleDbContext
    {
        private readonly string _connectionString;

        public OracleDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public T ExecuteFunction<T>(string functionName, OracleParameter[] parameters)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                using (var command = new OracleCommand(functionName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    // Add return parameter
                    var returnParameter = new OracleParameter("return_value", OracleDbType.Int64, ParameterDirection.ReturnValue);
                    command.Parameters.Add(returnParameter);

                    connection.Open();
                    command.ExecuteNonQuery();

                    var result = command.Parameters["return_value"].Value;

                    if (result == DBNull.Value)
                    {
                        throw new Exception("No data found.");
                    }

                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
        }
    }
}
