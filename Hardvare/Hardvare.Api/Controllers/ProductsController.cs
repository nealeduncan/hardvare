using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hardvare.Api.Models.CommandModels;
using Hardvare.Api.Models.QueryModels;
using Hardvare.Api.Repository;
using Hardvare.Api.Services;

namespace Hardvare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;

        public ProductsController(IProductRepository productRepository, IUserService userService)
        {
            this._productRepository = productRepository;
            this._userService = userService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int p = 1, string searchTerm = null)
        {
            var products = await this._productRepository.GetProducts(p, searchTerm);

            return this.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductModel model)
        {
            if (await this._userService.IsUserAnAdmin(model.UserId) == false) return this.Unauthorized();

            var productId = await this._productRepository.AddProduct(model);

            return this.Created(new Uri(this.Url.Link("GetProduct", new { id = productId })), new { productId });
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductModel model)
        {
            if (await this._userService.IsUserAnAdmin(model.UserId) == false) return this.Unauthorized();
            model.Id = id;
            await this._productRepository.UpdateProduct(model);

            return this.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, [FromBody] DeleteProductModel model)
        {
            if (await this._userService.IsUserAnAdmin(model.UserId) == false) return this.Unauthorized();
            model.Id = id;
            await this._productRepository.DeleteProduct(model);

            return this.NoContent();
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await this._productRepository.GetProduct(id);

            return this.Ok(product);
        }
    }
}
