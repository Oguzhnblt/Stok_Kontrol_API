using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using System.Text;

namespace Stok_Kontrol_API.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {

        string uri = "https://localhost:7233";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Category/TumKategorileriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                    categories = JsonConvert.DeserializeObject<List<Category>>(apiCevap);
                }
            }
            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> KategoriAktifleştir(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Category/KategoriAktifleştir/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> KategoriSil(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{uri}/api/Category/KategoriSil/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult KategoriEkle()
        {
            return View(); // Sadece ekleme View'ını gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> KategoriEkle(Category category)
        {
            category.isActive = true;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Category/KategoriEkle", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }




        static Category updatedCategory = new Category();
        // İLgili kategoriyi güncelleme işleminin devamındaki (put) kullanacağımız için o metottan da ulaşabilmek adına globalde tanımlayalım.

        [HttpGet]
        public async Task<IActionResult> KategoriGuncelle(int id)
        {

            // List<Category> categories = new List<Category>();  // Tek bir tane kategori güncelleneceği için List olarak tutmaya gerek yok.

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/Category/IDyeGoreKategoriGetir/{id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    updatedCategory = JsonConvert.DeserializeObject<Category>(apiCevap);

                }
            }
            return View(updatedCategory); // Update edilecek kategoriyi güncelleme View'ına gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> KategoriGuncelle(Category currentCategory) // Güncellenmiş kategori parametre olarak alınır.
        {

            // List<Category> categories = new List<Category>();  // Tek bir tane kategori güncelleneceği için List olarak tutmaya gerek yok.


            using (var httpClient = new HttpClient())
            {
                currentCategory.isActive = true;
                currentCategory.AddedDate = DateTime.Now;

                StringContent content = new StringContent(JsonConvert.SerializeObject(currentCategory), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/Category/KategoriGuncelle/{currentCategory.ID}", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
