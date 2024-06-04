using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.DeliveryAddress;
using Entity.DTOs.Product;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.DeliveryAddress
{
    public class DeliveryAddressRepository
    {
        private readonly OracleDbContext _dbContext;

        public DeliveryAddressRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DeliveryAddressDto>> GetDeliveryAddressesByCustomerId(int customerId)
        {
            var result = new List<DeliveryAddressDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_deliveryAddress sys_refcursor; p_customerId number := :v_customerId;  begin :v_deliveryAddress := deliveryAddressManager_pkg.getDeliveryAddressDetailByCustomerId(p_customerId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;

                    command.Parameters.Add("v_deliveryAddress", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["v_deliveryAddress"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new DeliveryAddressDto
                            {
                                FullName = reader.GetString(reader.GetOrdinal("fullName")),
                                DeliveryAddressDetail = reader.GetString(reader.GetOrdinal("deliveryAddressDetail")),
                                DeliveryAddressId = reader.GetInt32(reader.GetOrdinal("deliveryAddressId")),
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
