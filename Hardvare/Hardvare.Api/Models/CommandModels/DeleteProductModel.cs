using System;

namespace Hardvare.Api.Models.CommandModels
{
    public class DeleteProductModel
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
