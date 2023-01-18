using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using System.Text;

namespace Stok_Kontrol_API.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {

        string uri = "https://localhost:7233";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product> Products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Product/TumUrunleriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                    Products = JsonConvert.DeserializeObject<List<Product>>(apiCevap);
                }
            }
            return View(Products);
        }


        [HttpGet]
        public async Task<IActionResult> UrunAktifleştir(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Product/UrunAktifleştir/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> UrunSil(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{uri}/api/Product/UrunSil/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UrunEkle()
        {
            List<Category> aktifKategoriler = new List<Category>();
            List<Supplier> aktifTedarikciler = new List<Supplier>();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Product/AktifKategorileriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    aktifKategoriler = JsonConvert.DeserializeObject<List<Category>>(apiCevap);
                }

                using (var cevap = await httpClient.GetAsync($"{uri}/api/Product/AktifTedarikcileriGetir"))
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
        public async Task<IActionResult> UrunEkle(Product Product)
        {
            Product.isActive = true;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Product), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Product/UrunEkle", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }




        static Product updatedProduct = new Product();
        // İLgili Urunyi güncelleme işleminin devamındaki (put) kullanacağımız için o metottan da ulaşabilmek adına globalde tanımlayalım.

        [HttpGet]
        public async Task<IActionResult> UrunGuncelle(int id)
        {

            // List<Product> categories = new List<Product>();  // Tek bir tane Urun güncelleneceği için List olarak tutmaya gerek yok.

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Product/IDyeGoreUrunGetir/{id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    updatedProduct = JsonConvert.DeserializeObject<Product>(apiCevap);

                }
            }
            return View(updatedProduct); // Update edilecek Urunyi güncelleme View'ına gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> UrunGuncelle(Product currentProduct) // Güncellenmiş Urun parametre olarak alınır.
        {

            // List<Product> categories = new List<Product>();  // Tek bir tane Urun güncelleneceği için List olarak tutmaya gerek yok.


            using (var httpClient = new HttpClient())
            {
                currentProduct.isActive = true;
                currentProduct.AddedDate = DateTime.Now;

                StringContent content = new StringContent(JsonConvert.SerializeObject(currentProduct), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Product/UrunGuncelle/{currentProduct.ID}", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
