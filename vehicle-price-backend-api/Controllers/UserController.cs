using Microsoft.AspNetCore.Mvc;

namespace vehicle_price_backend_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public IActionResult InGetdex()
        {
            return View();
        }
    }
}
