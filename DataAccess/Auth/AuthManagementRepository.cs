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

        public async Task Register(RegisterDto registerDto)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  v_register register_type := register_type(:firstName, :lastName, :emailAddress, :password); begin   authManagement_pkg.register(p_register => v_register); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("firstName", OracleDbType.Varchar2).Value = registerDto.FirstName;
                    command.Parameters.Add("lastName", OracleDbType.Varchar2).Value = registerDto.LastName;
                    command.Parameters.Add("emailAddress", OracleDbType.Varchar2).Value = registerDto.EmailAddress;
                    command.Parameters.Add("password", OracleDbType.Varchar2).Value = registerDto.Password;

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task Login(LoginDto loginDto)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  v_login login_type := login_type(:emailAddress , :password); begin  authManagement_pkg.login(p_login => v_login);  end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("emailAddress", OracleDbType.Varchar2).Value = loginDto.EmailAddress;
                    command.Parameters.Add("password", OracleDbType.Varchar2).Value = loginDto.Password;

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
