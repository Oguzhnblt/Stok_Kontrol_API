using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Repositories.Context;
using Stok_Kontrol_API.Service.Abstract;

namespace Stok_Kontrol_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> service;

        public ProductController(IGenericService<Product> service)
        {
            this.service = service;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult TumUrunleriGetir()
        {
            return Ok(service.GetAll(t0 => t0.Kategori, t1 => t1.Tedarikci));
        }
        [HttpGet]
        public IActionResult AktifUrunleriGetir()
        {
            return Ok(service.GetActive(t0 => t0.Kategori, t1 => t1.Tedarikci));
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public IActionResult IDyeGoreUrunGetir(int id)
        {
            return Ok(service.GetByID(id));

        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult UrunEkle(Product product)
        {
            service.Add(product);
            return CreatedAtAction("IDyeGoreUrunGetir", new { id = product.ID }, product);
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public IActionResult UrunGuncelle(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            try
            {
                service.Update(product);
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ProductExists(id))
                {
                    return NotFound();
                }

            }
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public IActionResult UrunSil(int id)
        {
            var product = service.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }


            try
            {
                service.Remove(product);
                return Ok("Urun silindi!");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool ProductExists(int id)
        {
            return service.Any(e => e.ID == id);
        }

        [HttpGet("{id}")]
        public IActionResult UrunAktifleştir(int id)
        {
            var product = service.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                service.Activate(id);
                return Ok(service.GetByID(id));

            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
    }
}
