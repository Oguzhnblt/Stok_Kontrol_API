﻿using Microsoft.AspNetCore.Mvc;

namespace Stok_Kontrol_API.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
