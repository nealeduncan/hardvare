using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Hardvare.Api.Models.CommandModels;
using Hardvare.Api.Models.QueryModels;
using Hardvare.Api.Repository;

namespace Hardvare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] AddCartModel model)
        {
            var cartId = await this._cartRepository.AddCart(model);

            return this.Created(new Uri(this.Url.Link("GetCart", new { id = cartId })), new { cartId });
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> AddProductToCart([FromRoute] Guid id, [FromBody] AddItemToCartModel model)
        {
            model.CartId = id;
            await this._cartRepository.AddItemToCart(model);

            return this.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        [Route("{id:guid}/{productId:guid}")]
        public async Task<IActionResult> RemoveProductFromCart([FromRoute] Guid id, [FromRoute] Guid productId)
        {
            await this._cartRepository.RemoveItemFromCart(new RemoveItemFromCartModel{CartId = id, ProductId = productId});

            return this.NoContent();
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Cart>> GetCart(Guid id)
        {
            var cart = await this._cartRepository.GetCart(id);

            return this.Ok(cart);
        }
    }
}
