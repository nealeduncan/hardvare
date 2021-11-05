using System;

namespace Hardvare.Api.Models.CommandModels
{
    public class RemoveItemFromCartModel
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
