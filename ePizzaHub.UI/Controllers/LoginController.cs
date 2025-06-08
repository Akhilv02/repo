using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Models.ApiModels.Response;
using ePizzaHub.UI.Models.ApiModels.Request;
using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ePizzaHub.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("ePizzaAPI");

                var userDetails = await client.GetFromJsonAsync<ValidateUserResponse>($"Auth?userName={request.EmailAddress}&password={request.Password}");

                if (userDetails is not null)
                {
                    List<Claim> claims = [new Claim(ClaimTypes.Name, "sample@123")];

                    await GenerateTicket(claims);

                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel request)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("ePizzaAPI");

                var userRequest = new CreateUserRequestModel()
                {
                    Email = request.Email,
                    Name = request.UserName,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                };

                HttpResponseMessage? userDetails = await client.PostAsJsonAsync<CreateUserRequestModel>("User", userRequest);

                userDetails.EnsureSuccessStatusCode();
            }

            return View();
        }

        private async Task GenerateTicket(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties()
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });
        }
    }
}
