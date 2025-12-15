using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebApi.Controllers {
    [ApiController]
    [Route("/test")]
    public class HomeController : Controller {
        [HttpGet]
        public async Task<IActionResult> Index() {
            return Ok();
        }
    }
}
