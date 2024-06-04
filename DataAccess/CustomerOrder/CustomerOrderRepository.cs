using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.BasketProduct;
using Entity.DTOs.CustomerOrder;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.CustomerOrder
{
    public class CustomerOrderRepository
    {
        private readonly OracleDbContext _dbContext;

        public CustomerOrderRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerOrderDto>> GetCustomerOrderByCustomerId(int customerId)
        {
            var result = new List<CustomerOrderDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_customerOrder sys_refcursor; p_customerId number := :v_customerId;  begin :v_customerOrder := customerOrderManager_pkg.getCustomerOrderByCustomerId(p_customerId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;

                    command.Parameters.Add("v_customerOrder", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["v_customerOrder"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new CustomerOrderDto
                            {
                                CustomerOrderId = reader.GetInt32(reader.GetOrdinal("customerOrderId")),
                                OrderNo = reader.GetString(reader.GetOrdinal("orderNo")),
                                OrderDate = reader.GetDateTime(reader.GetOrdinal("orderDate")),
                                TotalPrice = reader.GetDecimal(reader.GetOrdinal("totalPrice")),
                                OrderStatus = reader.GetString(reader.GetOrdinal("orderStatus")),
                                FullName = reader.GetString(reader.GetOrdinal("fullName")),
                                DeliveryAddressDetail = reader.GetString(reader.GetOrdinal("deliveryAddressDetail")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("phoneNumber")),
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
