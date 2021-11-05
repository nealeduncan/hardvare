using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hardvare.Api.Models.QueryModels;
using Hardvare.Api.Repository;

namespace Hardvare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await this._userRepository.GetUsers();
            return this.Ok(users);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id:guid}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var users = await this._userRepository.GetUser(id);

            return this.Ok(users);
        }
    }
}
