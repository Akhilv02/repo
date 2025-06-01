using ePizzaHub.Core.Contract;
using ePizzaHub.Models.ApiModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest userRequest) 
        {
            // Validation
            // Call BAL (Business Access Layer)
            // Call DAL (Data Access Layer)

            var result = await _userService.CreateUserRequestAsync(userRequest);

            return Ok();
        }
    }
}
