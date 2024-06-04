using System.Data;
using Entity.DTOs.Auth;
using Entity.DTOs.BasketProduct;
using Entity.DTOs.Product;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DataAccess.BasketProduct
{
    public class BasketProductRepository
    {
        private readonly OracleDbContext _dbContext;

        public BasketProductRepository(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BasketProductDto>> GetBasketProductByCustomerId(int customerId)
        {
            var result = new List<BasketProductDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_basketProduct sys_refcursor; p_customerId number := :v_customerId;  begin :v_basketProduct := basketProductManager_pkg.getBasketProductByCustomerId(p_customerId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;

                    command.Parameters.Add("v_basketProduct", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["v_basketProduct"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new BasketProductDto
                            {
                                BasketId = reader.GetInt32(reader.GetOrdinal("basketId")),
                                BrandName = reader.GetString(reader.GetOrdinal("brandName")),
                                ProductName = reader.GetString(reader.GetOrdinal("productName")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                                ProductPrice = reader.GetDecimal(reader.GetOrdinal("productPrice")),
                                ProductQuantity = reader.GetInt32(reader.GetOrdinal("productQuantity")),
                                ProductIsSelected = reader.GetInt32(reader.GetOrdinal("productIsSelected"))
                            });
                        }
                    }
                }
            }

            return result;
        }

        public async Task AddProductToBasket(AddProductToBasketRequest addProductToBasketRequest)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare v_basketProduct basketProduct_type := basketProduct_type(:basketId, :productId, :productQuantity, :isSelected); begin  basketProductManager_pkg.addProductToBasket(v_basketProduct); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("basketId", OracleDbType.Varchar2).Value = addProductToBasketRequest.BasketId;
                    command.Parameters.Add("productId", OracleDbType.Varchar2).Value = addProductToBasketRequest.ProductId;
                    command.Parameters.Add("productQuantity", OracleDbType.Varchar2).Value = addProductToBasketRequest.ProductQuantity;
                    command.Parameters.Add("isSelected", OracleDbType.Varchar2).Value = addProductToBasketRequest.IsSelected;

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task DeleteproductToBasket(int basketId, int productId)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  p_basketId number := :v_basketId; p_productId number := :v_productId; begin  basketProductManager_pkg.deleteProductFromBasket(p_basketId,p_productId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("basketId", OracleDbType.Varchar2).Value = basketId;
                    command.Parameters.Add("productId", OracleDbType.Varchar2).Value = productId;


                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task DecreaseProductToBasket(AddProductToBasketRequest addProductToBasketRequest)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare v_basketProduct basketProduct_type := basketProduct_type(:basketId, :productId, :productQuantity, :isSelected); begin  basketProductManager_pkg.decreaseProductFromBasket(v_basketProduct); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("basketId", OracleDbType.Varchar2).Value = addProductToBasketRequest.BasketId;
                    command.Parameters.Add("productId", OracleDbType.Varchar2).Value = addProductToBasketRequest.ProductId;
                    command.Parameters.Add("productQuantity", OracleDbType.Varchar2).Value = addProductToBasketRequest.ProductQuantity;
                    command.Parameters.Add("isSelected", OracleDbType.Varchar2).Value = addProductToBasketRequest.IsSelected;

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public async Task ReverseSelectedStatusOfTheProductInBasket(int basketId, int productId)
        {
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                string query = "declare  p_basketId number := :v_basketId; p_productId number := :v_productId; begin  basketProductManager_pkg.reverseSelectedStatusOfTheProductInBasket(p_basketId,p_productId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.Parameters.Add("basketId", OracleDbType.Varchar2).Value = basketId;
                    command.Parameters.Add("productId", OracleDbType.Varchar2).Value = productId;


                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public async Task<List<BasketProductDto>> GetSelectedBasketProductByCustomerId(int customerId)
        {
            var result = new List<BasketProductDto>();
            using (OracleConnection conn = _dbContext.GetConnection())
            {
                await conn.OpenAsync();
                string query = "declare  v_basketProduct sys_refcursor; p_customerId number := :v_customerId;  begin :v_basketProduct := basketProductManager_pkg.getSelectedBasketProductByCustomerId(p_customerId); end;";
                using (OracleCommand command = new OracleCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("v_customerId", OracleDbType.Int32).Value = customerId;

                    command.Parameters.Add("v_basketProduct", OracleDbType.RefCursor, ParameterDirection.ReturnValue);

                    await command.ExecuteNonQueryAsync(); // Fonksiyonu çağır

                    using (var reader = ((OracleRefCursor)command.Parameters["v_basketProduct"].Value).GetDataReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new BasketProductDto
                            {
                                BasketId = reader.GetInt32(reader.GetOrdinal("basketId")),
                                BrandName = reader.GetString(reader.GetOrdinal("brandName")),
                                ProductName = reader.GetString(reader.GetOrdinal("productName")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                                ProductPrice = reader.GetDecimal(reader.GetOrdinal("productPrice")),
                                ProductQuantity = reader.GetInt32(reader.GetOrdinal("productQuantity"))
                            });
                        }
                    }
                }
            }

            return result;
        }

    }
}
