using System;

namespace Hardvare.Api.Models.CommandModels
{
    public class AddItemToCartModel
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
