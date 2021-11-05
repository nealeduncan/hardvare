using System;

namespace Hardvare.Api.Models.CommandModels
{
    public class AddProductModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
