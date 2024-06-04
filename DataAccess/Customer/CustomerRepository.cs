using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.Customer;
using Entity.DTOs.Email;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.Customer
{
    public class CustomerRepository
    {
        private readonly OracleDbContext _dbContext;

        public CustomerRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerEmailDto> GetEmailIdByEmailAddress(int emailId)
        {
            var result = new CustomerEmailDto();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare v_customerId number; p_emailId number := :v_emailId; begin   :v_customerId := customerManager_pkg.getCustomerIdByEmailId(p_emailId);  end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add("v_emailId", OracleDbType.Int32).Value = emailId;
                    command.Parameters.Add("v_customerId", OracleDbType.Int32, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync();

                    result.CustomerId = ((OracleDecimal)command.Parameters["v_customerId"].Value).ToInt32();
                    result.EmailId = emailId;
                }
            }

            return result;
        }
    }
}
