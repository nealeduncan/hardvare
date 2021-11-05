using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Hardvare.Api.Models.CommandModels;
using Hardvare.Api.Models.QueryModels;
using Microsoft.Extensions.Configuration;

namespace Hardvare.Api.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;
        public CartRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("HardvareContext");
        }
        public async Task<Guid> AddCart(AddCartModel model)
        {
            var sql = @"INSERT INTO [Cart] OUTPUT inserted.id VALUES (@Id, @UserId)";

            await using var connection = new SqlConnection(this._connectionString);
            return (Guid)await connection.ExecuteScalarAsync(sql, new { Id = Guid.NewGuid(), model.UserId });
        }

        public async Task AddItemToCart(AddItemToCartModel model)
        {
            var sql = @"INSERT INTO [CartItem] OUTPUT inserted.id VALUES (@Id, @CartId, @ProductId, @Quantity)";

            await using var connection = new SqlConnection(this._connectionString);
            await connection.ExecuteScalarAsync(sql, new { Id = Guid.NewGuid(), model.CartId, model.ProductId, model.Quantity });
        }

        public async Task RemoveItemFromCart(RemoveItemFromCartModel model)
        {
            var sql = @"DELETE FROM [CartItem] WHERE CartId = @Id AND ProductId = @ProductId";

            await using var connection = new SqlConnection(this._connectionString);
            await connection.ExecuteScalarAsync(sql, new { model.CartId, model.ProductId });
        }

        public async Task<Cart> GetCart(Guid cartId)
        {
            var sql = @"SELECT * FROM [Cart] WHERE Id = @CartId";

            await using var connection = new SqlConnection(this._connectionString);
            var cart = await connection.QuerySingleOrDefaultAsync<Cart>(sql, new { cartId });

            var cartSql = @"SELECT * FROM [vw_GetCartItems] WHERE CartId = @CartId";

            var cartItems = await connection.QueryAsync<CartItem>(cartSql, new { cartId });
            cart.Items = cartItems.ToList();
            return cart;
        }
    }
}
