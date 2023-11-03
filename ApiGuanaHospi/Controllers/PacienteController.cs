using Microsoft.AspNetCore.Mvc;

namespace ApiGuanaHospi.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
