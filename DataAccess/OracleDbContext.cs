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

        public OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }

    }
}
