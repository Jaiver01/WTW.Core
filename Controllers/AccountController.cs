using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using WTW.Core.Repository;
using WTW.Core.Models;
using WTW.Core.Services;

namespace WTW.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserRepository _repository;
        private SecurityService _securityService;

        public AccountController(IUserRepository userRepository)
        {
            this._repository = userRepository;
            this._securityService = new SecurityService();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Login(UserInfo userInfo)
        {
            User user = this._repository.GetUserByEmail(userInfo.Email);

            if (user != null && this._securityService.Equals(user.Password, userInfo.Password))
            {
                return Ok(user);
            } 

            return StatusCode(StatusCodes.Status401Unauthorized);
        }

        [HttpPost("logout")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
