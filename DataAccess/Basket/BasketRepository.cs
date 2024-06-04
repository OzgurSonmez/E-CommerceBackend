
using System.Data;
using Entity.DTOs.Basket;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.Basket
{
    public class BasketRepository
    {
        private readonly OracleDbContext _dbContext;

        public BasketRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BasketDto> GetBasketIdByCustomerId(int customerId)
        {
            var result = new BasketDto();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare v_basketId number; p_customerId number := :v_customerId; begin   :v_basketId := basketManager_pkg.getBasketIdByCustomerId(p_customerId);  end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add("v_customerId", OracleDbType.Varchar2).Value = customerId;
                    command.Parameters.Add("v_basketId", OracleDbType.Int32, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync();

                    result.BasketId = ((OracleDecimal)command.Parameters["v_basketId"].Value).ToInt32();
                    
                }
            }

            return result;
        }
    }
}
