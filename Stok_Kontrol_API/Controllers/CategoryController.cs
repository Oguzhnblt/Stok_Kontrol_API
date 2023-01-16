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
    public class CategoryController : ControllerBase
    {
        private readonly Service.Abstract.IGenericService<Category> service;

        public CategoryController(IGenericService<Category> service)
        {
            this.service = service;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult TumKategorileriGetir()
        {
            return Ok(service.GetAll());
        }
        public IActionResult AktifKategorileriGetir()
        {
            return Ok(service.GetActive());
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult IDyeGoreKategoriGetir(int id)
        {
            return Ok(service.GetByID(id));

        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public IActionResult KategoriEkle(Category category)
        {
            service.Add(category);
            return CreatedAtAction("IdyeGoreKategoriGetir", new { id = category.ID }, category);
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult KategoriGuncelle(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            try
            {
                service.Update(category);
                return Ok(category);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!CategoryExists(id))
                {
                    return NotFound();
                }

            }
            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult KategoriSil(int id)
        {
            var category = service.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }


            try
            {
                service.Remove(category);
                return Ok("Kategori silindi!");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool CategoryExists(int id)
        {
            return service.Any(e => e.ID == id);
        }

        [HttpGet("{id}")]
        public IActionResult KategroiAktifleştir(int id)
        {
            var category = service.GetByID(id);
            if (category == null)
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
            return Ok("Kategori silindi!");
        }
    }
}
