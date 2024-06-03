using System.Data;
using Entity.DTOs.Auth;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess.Auth
{
    public class AuthManagementRepository
    {
        private readonly OracleDbContext _dbContext;

        public AuthManagementRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Register(RegisterDto register)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  test_register register_type := register_type(:firstName, :lastName, :emailAddress, :password); begin   authManagement_pkg.register(p_register => test_register); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("firstName", OracleDbType.Varchar2).Value = register.FirstName;
                    command.Parameters.Add("lastName", OracleDbType.Varchar2).Value = register.LastName;
                    command.Parameters.Add("emailAddress", OracleDbType.Varchar2).Value = register.EmailAddress;
                    command.Parameters.Add("password", OracleDbType.Varchar2).Value = register.Password;

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
