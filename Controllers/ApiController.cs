using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
