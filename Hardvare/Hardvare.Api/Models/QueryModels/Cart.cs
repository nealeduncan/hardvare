using System;
using System.Collections.Generic;

namespace Hardvare.Api.Models.QueryModels
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
