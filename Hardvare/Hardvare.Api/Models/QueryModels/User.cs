using System;
using System.Collections.Generic;

namespace Hardvare.Api.Models.QueryModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public List<Cart> Orders { get; set; }
    }
}
