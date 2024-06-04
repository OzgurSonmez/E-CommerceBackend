
using System.Data;
using Entity.DTOs.Category;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.Category
{
    public class CategoryRepository
    {
        private readonly OracleDbContext _dbContext;

        public CategoryRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var result = new List<CategoryDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  c_categories sys_refcursor; begin :c_categories := categoryManager_pkg.getAllCategories(); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add("c_categories", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["c_categories"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new CategoryDto
                            {
                                CategoryId = reader.GetInt32(reader.GetOrdinal("categoryId")),
                                CategoryName = reader.GetString(reader.GetOrdinal("categoryName")),
                                ParentCategotyId = reader.GetInt32(reader.GetOrdinal("parentCategoryId"))
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
