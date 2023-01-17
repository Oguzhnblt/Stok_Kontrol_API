using Microsoft.AspNetCore.Mvc;

namespace Stok_Kontrol_API.WebUI.Areas.Supplier.Controllers
{
    [Area("Supplier")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
