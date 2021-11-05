using System;
using System.Threading.Tasks;
using Hardvare.Api.Repository;

namespace Hardvare.Api.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> IsUserAnAdmin(Guid userId)
        {
            var user = await this._userRepository.GetUser(userId);
            return user.Role == "ADMIN";
        }
    }
}