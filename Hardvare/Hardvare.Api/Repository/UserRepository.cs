using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;
using Dapper;
using Hardvare.Api.Models.QueryModels;
using Microsoft.Extensions.Configuration;

namespace Hardvare.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("HardvareContext");
        }

        public async Task<List<User>> GetUsers()
        {
            var sql = @"SELECT * FROM [User]";

            await using var connection = new SqlConnection(this._connectionString);
            var users = await connection.QueryAsync<User>(sql);

            foreach (var user in users)
            {
                await GetUserOrders(connection, user);
            }

            return users.ToList();
        }

        public async Task<User> GetUser(Guid userId)
        {
            var sql = @"SELECT * FROM [User] WHERE Id = @UserId";

            await using var connection = new SqlConnection(this._connectionString);
            var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { userId });

            await GetUserOrders(connection, user);
            return user;
        }

        private static async Task GetUserOrders(SqlConnection connection, User user)
        {
            var cartSql = @"SELECT * FROM [Cart] WHERE UserId = @UserId";

            var carts = await connection.QueryAsync<Cart>(cartSql, new {UserId = user.Id});

            foreach (var cart in carts)
            {
                var cartItemSql = @"SELECT * FROM [vw_GetCartItems] WHERE CartId = @CartId";

                var cartItems = await connection.QueryAsync<CartItem>(cartItemSql, new {CartId = cart.Id});

                cart.Items = new List<CartItem>(cartItems.ToList());
            }

            user.Orders = new List<Cart>(carts.ToList());
        }
    }
}
