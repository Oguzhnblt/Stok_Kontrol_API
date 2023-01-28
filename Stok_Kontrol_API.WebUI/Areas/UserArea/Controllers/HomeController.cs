using Microsoft.AspNetCore.Mvc;

namespace Stok_Kontrol_API.WebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
