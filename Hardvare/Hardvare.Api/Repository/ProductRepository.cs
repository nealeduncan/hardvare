using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;
using Dapper;
using Hardvare.Api.Models.CommandModels;
using Hardvare.Api.Models.QueryModels;
using Microsoft.Extensions.Configuration;

namespace Hardvare.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private readonly int _pageSize = 5;
        public ProductRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("HardvareContext");
        }

        public async Task<List<Product>> GetProducts(int page, string searchTerm)
        {
            var sql = @"SELECT * FROM [Product]";

            if (!string.IsNullOrEmpty(searchTerm)) sql += $" WHERE Name like '%{searchTerm}%'";

            sql += "ORDER BY(SELECT Name) OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            await using var connection = new SqlConnection(this._connectionString);
            return connection.Query<Product>(sql, new
            {
                Offset = (page - 1) * this._pageSize,
                PageSize = this._pageSize
            }).ToList();
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            var sql = @"SELECT * FROM [Product] WHERE Id = @ProductId";

            await using var connection = new SqlConnection(this._connectionString);
            return await connection.QuerySingleOrDefaultAsync<Product>(sql, new {productId});
        }

        public async Task<Guid> AddProduct(AddProductModel model)
        {
            var sql = @"INSERT INTO [Product] OUTPUT inserted.id VALUES (@Id, @Name, @Description, @Price)";

            await using var connection = new SqlConnection(this._connectionString);
            return (Guid) await connection.ExecuteScalarAsync(sql, new { Id = Guid.NewGuid(), model.Name, model.Description, model.Price });
        }

        public async Task UpdateProduct(UpdateProductModel model)
        {
            var sql = @"UPDATE [Product] SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";

            await using var connection = new SqlConnection(this._connectionString);
            await connection.ExecuteAsync(sql, new { model.Name, model.Description, model.Price, model.Id });
        }

        public async Task DeleteProduct(DeleteProductModel model)
        {
            var sql = @"DELETE FROM [Product] WHERE Id = @Id";

            await using var connection = new SqlConnection(this._connectionString);
            await connection.ExecuteAsync(sql, new { model.Id });
        }
    }
}
