using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.WebUI.Areas.UserArea.Helpers;
using Stok_Kontrol_API.WebUI.Areas.UserArea.Models;

namespace Stok_Kontrol_API.WebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            if (SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet") != null)
            {
                ProductCart cart = SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet");

                List<ProductCartVM> productList = cart.CartProductList.Select(x => new ProductCartVM
                {
                    ID = x.ID,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    Stock = x.Stock,
                    Quantity = x.Quantity,

                }).ToList();

                return Json(productList, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            }

            return Json("Empty", System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        public async Task<JsonResult> Add(int id)
        {
            string uri = "https://localhost:7233";

            Product product = new Product(); // Eklenecek ürünü bulmak için.

            using (var HttpClient = new HttpClient())
            {
                using (var response = await HttpClient.GetAsync($"{uri}/api/Product/IDyeGoreUrunGetir/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            ProductCartVM pcVM = new ProductCartVM
            {
                ID = product.ID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                CategoryID = product.CategoryID,
                Stock = product.Stock,
                Quantity = 1 // Sepete 1 adet eklenecek.

            };

            if (SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet") != null)
            {
                ProductCart cart = SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet");
                cart.AddCart(pcVM);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "sepet", cart); // Var olan sepete ürün eklenmiş halini Session olarak tut.
            }

            else
            {
                // Hazırda bir sepet Session'ı yoksa

                ProductCart cart = new ProductCart();
                cart.AddCart(pcVM); // Ürünü ekle

                SessionHelper.SetObjectAsJson(HttpContext.Session, "sepet", cart); // Yeni bir sepet Session'ı oluştur ve cart nesnesini ekle.

            }

            return Json(""); // Boş bir Json döndürsün.
        }


        public JsonResult DecreaseCart(int id)
        {

            if (SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet") != null)
            {
                ProductCart cart = SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet");
                cart.DecreaseCart(id); // Var olan ürünün miktarını azalt.

                SessionHelper.SetObjectAsJson(HttpContext.Session, "sepet", cart);
            }

            return Json("");
        }

        public JsonResult İncreaseCart(int id)
        {

            if (SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet") != null)
            {
                ProductCart cart = SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet");
                cart.IncreaseCart(id); // Var olan ürünün miktarını arttır.

                SessionHelper.SetObjectAsJson(HttpContext.Session, "sepet", cart);


            }
            return Json("");
        }

        public JsonResult RemoveCart(int id)
        {

            ProductCart cart = SessionHelper.GetObjectFromJson<ProductCart>(HttpContext.Session, "sepet");
            cart.RemoveCart(id); // Var olan ürünü sepetten silmek için.

            SessionHelper.SetObjectAsJson(HttpContext.Session, "sepet", cart);


            return Json("");
        }

    }
}
