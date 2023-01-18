using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Service.Abstract;

namespace Stok_Kontrol_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IGenericService<Supplier> service;

        public SupplierController(IGenericService<Supplier> service)
        {
            this.service = service;
        }

        // GET: api/Supplier
        [HttpGet]
        public IActionResult TumTedarikcileriGetir()
        {
            return Ok(service.GetAll());
        }
        [HttpGet]
        public IActionResult AktifTedarikcileriGetir()
        {
            return Ok(service.GetActive());
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public IActionResult IDyeGoreTedarikciGetir(int id)
        {
            return Ok(service.GetByID(id));

        }

        // PUT: api/Supplier/5
        [HttpPost]
        public IActionResult TedarikciEkle(Supplier supplier)
        {
            service.Add(supplier);
            return CreatedAtAction("IDyeGoreTedarikciGetir", new { id = supplier.ID }, supplier);
        }

        // POST: api/Supplier
        [HttpPost("{id}")]
        public IActionResult TedarikciGuncelle(int id, Supplier supplier)
        {
            if (id != supplier.ID)
            {
                return BadRequest();
            }

            try
            {
                service.Update(supplier);
                return Ok(supplier);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!SupplierExists(id))
                {
                    return NotFound();
                }

            }
            return NoContent();
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public IActionResult TedarikciSil(int id)
        {
            var supplier = service.GetByID(id);
            if (supplier == null)
            {
                return NotFound();
            }


            try
            {
                
                service.Remove(supplier);
                return Ok("Tedarikçi silindi!");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool SupplierExists(int id)
        {
            return service.Any(e => e.ID == id);
        }

        [HttpGet("{id}")]
        public IActionResult TedarikciAktifleştir(int id)
        {
            var Supplier = service.GetByID(id);
            if (Supplier == null)
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
