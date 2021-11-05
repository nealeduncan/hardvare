using System;
using System.Threading.Tasks;

namespace Hardvare.Api.Services
{
    public interface IUserService
    {
        Task<bool> IsUserAnAdmin(Guid userId);
    }
}
