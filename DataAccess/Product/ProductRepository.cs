using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs.Auth;
using Entity.DTOs.Product;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.Product
{
    public class ProductRepository
    {
        private readonly OracleDbContext _dbContext;

        public ProductRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FilteredProductDto>> GetFilteredProducts(FilteredProductRequestDto filteredProductRequestDto)
        {
            var result = new List<FilteredProductDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_filterProduct filterProduct_type := filterProduct_type(:categoryId, :brandId, :productName, :minPrice, :maxPrice, :orderBy, :orderDirection);  c_products sys_refcursor; begin :c_products := productManager_pkg.filterProduct(v_filterProduct); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("categoryId", OracleDbType.Int32).Value = filteredProductRequestDto.CategoryId;
                    command.Parameters.Add("brandId", OracleDbType.Int32).Value = filteredProductRequestDto.BrandId;
                    command.Parameters.Add("productName", OracleDbType.Varchar2).Value = filteredProductRequestDto.ProductName;
                    command.Parameters.Add("minPrice", OracleDbType.Decimal).Value = filteredProductRequestDto.MinPrice;
                    command.Parameters.Add("maxPrice", OracleDbType.Decimal).Value = filteredProductRequestDto.MaxPrice;
                    command.Parameters.Add("orderBy", OracleDbType.Varchar2).Value = filteredProductRequestDto.OrderBy;
                    command.Parameters.Add("orderDirection", OracleDbType.Varchar2).Value = filteredProductRequestDto.OrderDirection;

                    command.Parameters.Add("c_products", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["c_products"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new FilteredProductDto
                            {
                                CategoryId = reader.GetInt32(reader.GetOrdinal("categoryId")),
                                CategoryName = reader.GetString(reader.GetOrdinal("categoryName")),
                                BrandId = reader.GetInt32(reader.GetOrdinal("brandId")),
                                BrandName = reader.GetString(reader.GetOrdinal("brandName")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                                ProductName = reader.GetString(reader.GetOrdinal("productName")),
                                Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                DiscountPercentage = reader.GetInt16(reader.GetOrdinal("discount")),
                                FavCount = reader.GetInt32(reader.GetOrdinal("favcount")),
                            });
                        }
                    }
                }
            }

            return result;
        }

    }
}
