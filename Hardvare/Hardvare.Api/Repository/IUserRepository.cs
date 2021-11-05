using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hardvare.Api.Models;
using Hardvare.Api.Models.QueryModels;

namespace Hardvare.Api.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(Guid userId);
    }
}
