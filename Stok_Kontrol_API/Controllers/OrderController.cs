using Microsoft.AspNetCore.Mvc;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Entities.Enums;
using Stok_Kontrol_API.Service.Abstract;

namespace Stok_Kontrol_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericService<User> userService;
        private readonly IGenericService<Product> productService;
        private readonly IGenericService<OrderDetails> odService;
        private readonly IGenericService<Order> orderService;

        public OrderController(IGenericService<User> userService, IGenericService<Product> productService, IGenericService<OrderDetails> odService, IGenericService<Order> orderService)
        {
            this.userService = userService;
            this.productService = productService;
            this.odService = odService;
            this.orderService = orderService;
        }

        // TODO : OrderController içerisinde sepetten siparişi tamamlanan ürünler için DB'de sipariş kaydı ve siparişin detaylarını oluşturun. Siparişin durumuna göre (Bekliyor, Onaylandı, Reddedildi gibi). Stoktan ilgili üründen ilgili adet kadar düşürelecek Action Method yazın.


        // GET: api/Supplier
        [HttpGet]
        public IActionResult TumSiparisleriGetir()
        {
            return Ok(orderService.GetAll(x => x.SiparisDetayları, y => y.Kullanıcı));
        }
        [HttpGet]
        public IActionResult AktifSiparisleriGetir()
        {
            return Ok(orderService.GetActive());
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public IActionResult IDyeGoreSiparisGetir(int id)
        {
            return Ok(orderService.GetByID(id, x => x.SiparisDetayları, y => y.Kullanıcı));

        }

        [HttpGet]
        public IActionResult BekleyenSiparisleriGetir()
        {
            return Ok(orderService.GetDefault(x => x.Status == Status.Pending).ToList());
        }

        [HttpGet]
        public IActionResult OnaylananSiparisleriGetir()
        {
            return Ok(orderService.GetDefault(x => x.Status == Status.Confirmed).ToList());
        }

        [HttpGet]
        public IActionResult ReddedilenSiparisleriGetir()
        {
            return Ok(orderService.GetDefault(x => x.Status == Status.Cancelled).ToList());
        }


        [HttpGet("{id}")]
        public IActionResult SiparisOnayla(int id)
        {
            Order confirmedOrder = orderService.GetByID(id);
            if (confirmedOrder == null)
            {
                return NotFound();
            }
            else
            {
                List<OrderDetails> detaylar = odService.GetDefault(x => x.OrderID == confirmedOrder.ID).ToList();

                foreach (OrderDetails item in detaylar)
                {
                    Product productInOrder = productService.GetByID(item.ProductID);
                    productInOrder.Stock -= Convert.ToInt16(item.Quantity);
                    productService.Update(productInOrder);
                }

                confirmedOrder.Status = Status.Confirmed;
                confirmedOrder.isActive = false;
                orderService.Update(confirmedOrder);

                return Ok(confirmedOrder);
            }
        }
        [HttpGet("{id}")]
        public IActionResult SiparisReddet(int id)
        {
            Order cancelledOrder = orderService.GetByID(id);
            if (cancelledOrder == null)
            {
                return NotFound();
            }
            else
            {
                cancelledOrder.Status = Status.Cancelled;
                cancelledOrder.isActive = false;
                orderService.Update(cancelledOrder);

                return Ok(cancelledOrder);

            }
        }

    }
}
