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
        private readonly IGenericService<Category> _service;

        public CategoryController(IGenericService<Category> service)
        {
            _service = service;
        }

        // GET: api/Category/TumKategorileriGetir
        [HttpGet]
        public IActionResult TumKategorileriGetir()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/Category/AktifKategorileriGetir/5
        [HttpGet]
        public IActionResult AktifKategorileriGetir()
        {
            return Ok(_service.GetActive());
        }

        [HttpGet("{id}")]
        public IActionResult IDyeGoreKategoriGetir(int id)
        {
            return Ok(_service.GetByID(id));
        }
        // POST: api/Category/KategoriEkle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult KategoriEkle(Category category)
        {
            _service.Add(category);

            return CreatedAtAction("IDyeGoreKategoriGetir", new { id = category.ID }, category);
        }

        // PUT: api/Category/KategorileriGuncelle/5
        [HttpPost("{id}")]
        public IActionResult KategorileriGuncelle(int id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            //_service.Entry(category).State = EntityState.Modified;

            try
            {
                _service.Update(category);
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



        // DELETE: api/Category/KategoriSil/5
        [HttpDelete("{id}")]
        public IActionResult KategoriSil(int id)
        {
            var category = _service.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                _service.Remove(category);
                return Ok("Kategori Silindi!");
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }


        private bool CategoryExists(int id)
        {
            return _service.Any(e => e.ID == id);
        }


        [HttpGet("{id}")]
        public IActionResult KategoriAktiflestir(int id)
        {
            var category = _service.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                _service.Activate(id);
                return Ok(_service.GetByID(id));

            }
            catch (Exception)
            {

                return BadRequest();
            }


        }
    }
}
