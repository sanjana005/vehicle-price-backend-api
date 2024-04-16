using Microsoft.AspNetCore.Mvc;

namespace vehicle_price_backend_api.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
