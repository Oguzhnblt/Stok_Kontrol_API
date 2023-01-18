using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using System.Text;

namespace Stok_Kontrol_API.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SupplierController : Controller
    {

        string uri = "https://localhost:7233";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Supplier/TumTedarikcileriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                    suppliers = JsonConvert.DeserializeObject<List<Supplier>>(apiCevap);
                }
            }
            return View(suppliers);
        }


        [HttpGet]
        public async Task<IActionResult> TedarikciAktifleştir(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Supplier/TedarikciAktifleştir/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> TedarikciSil(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{uri}/api/Supplier/TedarikciSil/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult TedarikciEkle()
        {
            return View(); // Sadece ekleme View'ını gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> TedarikciEkle(Supplier supplier)
        {
            supplier.isActive = true;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Supplier/TedarikciEkle", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }




        static Supplier updatedSupplier = new Supplier();
        // İLgili Tedarikciyi güncelleme işleminin devamındaki (put) kullanacağımız için o metottan da ulaşabilmek adına globalde tanımlayalım.

        [HttpGet]
        public async Task<IActionResult> TedarikciGuncelle(int id)
        {

            // List<Supplier> categories = new List<Supplier>();  // Tek bir tane Tedarikci güncelleneceği için List olarak tutmaya gerek yok.

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Supplier/IDyeGoreTedarikciGetir/{id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    updatedSupplier = JsonConvert.DeserializeObject<Supplier>(apiCevap);

                }
            }
            return View(updatedSupplier); // Update edilecek Tedarikciyi güncelleme View'ına gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> TedarikciGuncelle(Supplier currentSupplier) // Güncellenmiş Tedarikci parametre olarak alınır.
        {

            // List<Supplier> categories = new List<Supplier>();  // Tek bir tane Tedarikci güncelleneceği için List olarak tutmaya gerek yok.


            using (var httpClient = new HttpClient())
            {
                currentSupplier.isActive = true;
                currentSupplier.AddedDate = DateTime.Now;

                StringContent content = new StringContent(JsonConvert.SerializeObject(currentSupplier), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Supplier/TedarikciGuncelle/{currentSupplier.ID}", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
