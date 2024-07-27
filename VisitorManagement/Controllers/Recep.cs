using Microsoft.AspNetCore.Mvc;

namespace VisitorManagement.Controllers
{
    public class Recep : Controller
    {
        public IActionResult Recept()
        {
            return View();
        }
    }
}
