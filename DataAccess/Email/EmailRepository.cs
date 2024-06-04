using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.Auth;
using Entity.DTOs.Brand;
using Entity.DTOs.Email;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.Email
{
    public class EmailRepository
    {
        private readonly OracleDbContext _dbContext;

        public EmailRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmailDto> GetEmailIdByEmailAddress(string emailAddress)
        {
            var result = new EmailDto();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare v_emailId number; p_emailAddress varchar2(255) := :v_emailAddress; begin   :v_emailId := emailManager_pkg.getEmailIdByEmailAddress(p_emailAddress);  end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add("v_emailAddress", OracleDbType.Varchar2).Value = emailAddress;                  
                    command.Parameters.Add("v_emailId", OracleDbType.Int32, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); 
                    
                    result.EmailId = ((OracleDecimal)command.Parameters["v_emailId"].Value).ToInt32();
                    result.EmailAddress = emailAddress;
                }
            }

            return result;
        }

    }
}
