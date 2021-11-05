using System;

namespace Hardvare.Api.Models.QueryModels
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
