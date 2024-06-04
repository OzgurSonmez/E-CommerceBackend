using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.BasketProduct;
using Entity.DTOs.CustomerProductFavorite;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.CustomerProductFavorite
{
    public class CustomerProductFavoriteRepository
    {
        private readonly OracleDbContext _dbContext;

        public CustomerProductFavoriteRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductToFavorite(int customerId, int productId)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  p_customerId number := :v_customerId ; p_productId number := :v_productId; begin  customerProductFavoriteManager_pkg.addProductToFavorite(p_customerId,p_productId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;
                    command.Parameters.Add("v_productId", OracleDbType.Int32).Value = productId;


                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task RemoveProductFromFavorite(int customerId, int productId)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  p_customerId number := :v_customerId ; p_productId number := :v_productId; begin  customerProductFavoriteManager_pkg.removeProductFromFavorite(p_customerId,p_productId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;
                    command.Parameters.Add("v_productId", OracleDbType.Int32).Value = productId;


                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<List<CustomerProductFavoriteDto>> GetCustomerProductFavorite(int customerId)
        {
            var result = new List<CustomerProductFavoriteDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_customerProductFavorite sys_refcursor; p_customerId number := :v_customerId;  begin :v_customerProductFavorite := customerProductFavoriteManager_pkg.getCustomerProductFavorite(p_customerId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;

                    command.Parameters.Add("v_customerProductFavorite", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["v_customerProductFavorite"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new CustomerProductFavoriteDto
                            {
                                BrandName = reader.GetString(reader.GetOrdinal("brandName")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                                ProductName = reader.GetString(reader.GetOrdinal("productName")),
                                ProductPrice = reader.GetDecimal(reader.GetOrdinal("productPrice"))
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
