using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;

namespace Stok_Kontrol_API.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {

        string uri = "https://localhost:7233";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Order> orders = new List<Order>();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/TumSiparisleriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                    orders = JsonConvert.DeserializeObject<List<Order>>(apiCevap);
                }
            }
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> SiparisDetayiGor(int id)
        {
            List<Order> order = new List<Order>();
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/IDyeGoreSiparisGetir/{id}"))
                {
                    string apicevap = await cevap.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<List<Order>>(apicevap);
                }
            }
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> SiparisOnayla(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/SiparisOnayla/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }






        public async Task<IActionResult> SiparisSil(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{uri}/api/Order/SiparisSil/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }

    }
}
