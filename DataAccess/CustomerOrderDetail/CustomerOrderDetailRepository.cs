using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.CustomerOrder;
using Entity.DTOs.CustomerOrderDetail;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.CustomerOrderDetail
{
    public class CustomerOrderDetailRepository
    {
        private readonly OracleDbContext _dbContext;

        public CustomerOrderDetailRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerOrderDetailDto>> getCustomerOrderDetailByCustomerOrderId(int customerOrderId)
        {
            var result = new List<CustomerOrderDetailDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_customerOrderDetail sys_refcursor; p_customerOrderId number := :v_customerOrderId;  begin :v_customerOrderDetail := customerOrderDetailManager_pkg.getCustomerOrderDetailByCustomerOrderId(p_customerOrderId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("v_customerOrderId", OracleDbType.Int32).Value = customerOrderId;

                    command.Parameters.Add("v_customerOrderDetail", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["v_customerOrderDetail"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new CustomerOrderDetailDto
                            {
                                BrandName = reader.GetString(reader.GetOrdinal("brandName")),
                                ProductName = reader.GetString(reader.GetOrdinal("productName")),
                                ProductQuantity = reader.GetInt32(reader.GetOrdinal("productQuantity")),
                                ProductUnitPrice = reader.GetDecimal(reader.GetOrdinal("productUnitPrice"))
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
