using ePizzaHub.Core.Contract;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("get-item-count")]
        public async Task<IActionResult> GetCartItemCount(Guid guid)
        {
            var itemCount = await _cartService.GetCartItemsCountAsync(guid);

            return Ok(itemCount);
        }
    }
}
