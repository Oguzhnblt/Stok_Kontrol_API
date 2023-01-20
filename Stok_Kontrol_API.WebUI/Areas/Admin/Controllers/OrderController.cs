using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Entities.Enums;
using System.Text;

namespace Stok_Kontrol_API.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {

        string uri = "https://localhost:7233";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Order> Orders = new List<Order>();
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/TumSiparisleriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                    Orders = JsonConvert.DeserializeObject<List<Order>>(apiCevap);
                }
            }
            return View(Orders);
        }




        [HttpGet]
        public async Task<IActionResult> SiparisEkle()
        {

            List<Category> aktifKategoriler = new List<Category>();
            List<Supplier> aktifTedarikciler = new List<Supplier>();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/AktifKullaniciGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    aktifKategoriler = JsonConvert.DeserializeObject<List<Category>>(apiCevap);
                }

                using (var cevap = await httpClient.GetAsync($"{uri}/api/Order/AktifTedarikcileriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    aktifTedarikciler = JsonConvert.DeserializeObject<List<Supplier>>(apiCevap);
                }
            }



            ViewBag.AktifKategoriler = aktifKategoriler;
            ViewBag.AktifTedarikciler = aktifTedarikciler;

            return View(); // Sadece ekleme View'ını gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> SiparisEkle(Order order)
        {

            Order receivedOrder = new Order();

            using (var httpClient = new HttpClient())
            {
                var comtents = new MultipartFormDataContent
                {
                    { new StringContent(order.UserID.ToString()), "Id" },
                    { new StringContent(order.SiparisDetayları.ToString()) ,"Sipariş Detayları" },
                    { new StringContent(order.Kullanıcı.ID.ToString()), "Id" },
                    { new StringContent(order.Kullanıcı.ID.ToString()), "Id" }
                };

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Order/SiparisEkle", comtents))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
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
