using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using System.Text;

namespace Stok_Kontrol_API.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UserController : Controller
    {

        string uri = "https://localhost:7233";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/User/TumKullanicileriGetir"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<User>>(apiCevap);
                }
            }
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciAktiflestir(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{uri}/api/User/KullaniciAktiflestir/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> KullaniciSil(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{uri}/api/User/KullaniciSil/{id}"))
                {

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult KullaniciEkle()
        {
            return View(); // Sadece ekleme View'ını gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciEkle(User User)
        {
            User.isActive = true;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PostAsync($"{uri}/api/User/KullaniciEkle", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }




        static User updatedUser = new User();
        // İLgili Kullaniciyi güncelleme işleminin devamındaki (put) kullanacağımız için o metottan da ulaşabilmek adına globalde tanımlayalım.

        [HttpGet]
        public async Task<IActionResult> KullaniciGuncelle(int id)
        {

            // List<User> users = new List<User>();  // Tek bir tane Kullanici güncelleneceği için List olarak tutmaya gerek yok.
            if (ModelState.IsValid)
            {

                using (var httpClient = new HttpClient())
                {
                    using (var cevap = await httpClient.GetAsync($"{uri}/api/User/IDyeGoreKullaniciGetir/{id}"))
                    {
                        string apiCevap = await cevap.Content.ReadAsStringAsync();
                        updatedUser = JsonConvert.DeserializeObject<User>(apiCevap);

                    }
                }
            }
            return View(updatedUser); // Update edilecek Kullaniciyi güncelleme View'ına gösterecek.
        }

        [HttpPost]
        public async Task<IActionResult> KullaniciGuncelle(User currentUser) // Güncellenmiş Kullanici parametre olarak alınır.
        {

            // List<User> users = new List<User>();  // Tek bir tane Kullanici güncelleneceği için List olarak tutmaya gerek yok.


            using (var httpClient = new HttpClient())
            {
                currentUser.isActive = true;
                currentUser.AddedDate = DateTime.Now;
                currentUser.Password = updatedUser.Password;

                StringContent content = new StringContent(JsonConvert.SerializeObject(currentUser), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{uri}/api/User/KullaniciGuncelle/{currentUser.ID}", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");

        }
    }
}
