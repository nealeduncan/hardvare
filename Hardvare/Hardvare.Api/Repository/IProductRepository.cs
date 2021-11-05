using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hardvare.Api.Models;
using Hardvare.Api.Models.CommandModels;
using Hardvare.Api.Models.QueryModels;

namespace Hardvare.Api.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int page, string searchTerm);
        Task<Product> GetProduct(Guid productId);
        Task<Guid> AddProduct(AddProductModel model);
        Task UpdateProduct(UpdateProductModel model);
        Task DeleteProduct(DeleteProductModel model);
    }
}
