using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.Auth;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess.OrderManagement
{
    public class OrderManagementRepository
    {
        private readonly OracleDbContext _dbContext;

        public OrderManagementRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CompleteOrder(int customerId, int deliveryAddressId)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  p_customerId number := :v_customerId; p_deliveryAddressId number := :v_deliveryAddressId; begin   orderManagement_pkg.completeOrder(p_customerId, p_deliveryAddressId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;
                    command.Parameters.Add("v_deliveryAddressId", OracleDbType.Int32).Value = deliveryAddressId;

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
