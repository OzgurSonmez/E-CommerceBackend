using System.Data;
using Entity.DTOs.Brand;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.Brand
{
    public class BrandRepository
    {
        private readonly OracleDbContext _dbContext;

        public BrandRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BrandDto>> GetBrands()
        {
            var result = new List<BrandDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  c_brands sys_refcursor; begin :c_brands := brandManager_pkg.getAllBrands(); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add("c_brands", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["c_brands"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new BrandDto
                            {
                                BrandId = reader.GetInt32(reader.GetOrdinal("brandId")),
                                BrandName = reader.GetString(reader.GetOrdinal("brandName"))
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
