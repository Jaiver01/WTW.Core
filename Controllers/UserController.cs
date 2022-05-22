using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WTW.Core.Models;
using WTW.Core.Repository;
using WTW.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WTW.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;
        private SecurityService _securityService;

        public UserController(IUserRepository userRepository)
        {
            this._repository = userRepository;
            this._securityService = new SecurityService();
        }

        [HttpGet("Persons")]
        [ProducesResponseType(typeof(IEnumerable<Person>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPersons()
        {
            return Ok(this._repository.GetPersonsStoredProcedure());
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get()
        {
            return Ok(this._repository.GetUsers());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get(long id)
        {
            return Ok(this._repository.GetUser(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] User user)
        {
            user.Password = this._securityService.GenerateHash(user.Password);
            User userResponse = this._repository.CreateUser(user);
            return StatusCode(StatusCodes.Status201Created, userResponse);
        }

        [HttpPost("person")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] Person person)
        {
            Person personResponse = this._repository.CreatePerson(person);
            return StatusCode(StatusCodes.Status201Created, personResponse);
        }
    }
}
