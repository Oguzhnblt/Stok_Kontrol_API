using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using System.Net.Http;

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
            List<Product> productList = new List<Product>();


            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/IDyeGoreSiparisGetir/{id}"))
                {
                    string apicevap = await cevap.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<List<Order>>(apicevap);
                }
                foreach (Order item in order)
                {
                    foreach (OrderDetails item1 in item.SiparisDetaylari)
                    {
                        using (var cevap = await httpClient.GetAsync($"{uri}/api/Product/IDyeGoreUrunGetir/{item1.ProductID}"))
                        {

                            string apicevap = await cevap.Content.ReadAsStringAsync();
                            productList.Add(JsonConvert.DeserializeObject<Product>(apicevap));

                        }
                    }
                }
            }


            return View(new Tuple<List<Order>, List<Product>>(order, productList));
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






        public async Task<IActionResult> SiparisReddet(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{uri}/api/Order/SiparisReddet/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }

    }
}
