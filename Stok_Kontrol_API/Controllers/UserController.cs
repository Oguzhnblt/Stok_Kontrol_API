using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stok_Kontrol_API.Entities.Entities;
using Stok_Kontrol_API.Service.Abstract;

namespace Stok_Kontrol_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<User> service;

        public UserController(IGenericService<User> service)
        {
            this.service = service;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult TumKullanicileriGetir()
        {
            return Ok(service.GetAll());
        }
        [HttpGet]
        public IActionResult AktifKullanicileriGetir()
        {
            return Ok(service.GetActive());
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult IDyeGoreKullaniciGetir(int id)
        {
            return Ok(service.GetByID(id));

        }

        // PUT: api/User/5
        [HttpPost]
        public IActionResult KullaniciEkle(User user)
        {
            service.Add(user);
            return CreatedAtAction("IDyeGoreKullaniciGetir", new { id = user.ID }, user);
        }

        // POST: api/User
        [HttpPut]
        public IActionResult KullaniciGuncelle(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            try
            {
                service.Update(user);
                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!UserExists(id))
                {
                    return NotFound();
                }

            }
            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult KullaniciSil(int id)
        {
            var user = service.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }


            try
            {

                service.Remove(user);
                return Ok("Kullanıcı silindi!");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        private bool UserExists(int id)
        {
            return service.Any(e => e.ID == id);
        }

        [HttpGet("{id}")]
        public IActionResult KullaniciAktiflestir(int id)
        {
            var user = service.GetByID(id);
            if (user == null)
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

        [HttpGet]
        public IActionResult Login(string email, string parola)
        {
            if (service.Any(x => x.Email == email && x.Password == parola))
            {
                User logged = service.GetByDefault(x => x.Email == email && x.Password == parola);
                return Ok(logged);
            }

            return NotFound();
        }
    }
}
