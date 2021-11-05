using System;
using System.Threading.Tasks;
using Hardvare.Api.Models;
using Hardvare.Api.Models.CommandModels;
using Hardvare.Api.Models.QueryModels;

namespace Hardvare.Api.Repository
{
    public interface ICartRepository
    {
        Task<Guid> AddCart(AddCartModel model);
        Task AddItemToCart(AddItemToCartModel model);
        Task RemoveItemFromCart(RemoveItemFromCartModel model);
        Task<Cart> GetCart(Guid cartId);
    }
}