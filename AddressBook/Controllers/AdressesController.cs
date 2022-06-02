using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBook.Models;
using Serilog;
using Serilog.AspNetCore;


namespace AddressBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly AdressDbContext _context;

        public AdressesController(AdressDbContext context)
        {
            _context = context;
        }

        //// GET: api/Adress
        [HttpGet]
        public async Task<ActionResult<Adress>> GetNewestAdress()
        {
            Log.Information("The global logger has been configured");

            if (!_context.Adresses.Any())
            {
                return NotFound();
            }
            var adress = await _context.Adresses.LastOrDefaultAsync();
            
            return adress;
        }

        //// GET: api/Adresses/5
        [Route("api/{City}")]
        [HttpGet]
        public async Task<ActionResult<List<Adress>>> GetAdressByCity(string City)
        {

            if (!_context.Adresses.Any())
            {
                return NotFound();
            }

            var addresses = await _context.Adresses.Where(x => x.City == City).ToListAsync();

            return addresses;
        }

        // POST: api/PostAdresses
       
        [HttpPost]
        public async Task<ActionResult<Adress>> PostAdress(Adress adress)
        {
            if (_context.Adresses == null)
            {
                return Problem("Entity set 'AdressDbContext.Adresses'  is null.");
            }
            _context.Adresses.Add(adress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdress", new
            { id = adress.Id }, adress);
        }

        //// DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdress(int id)
        {
            if (_context.Adresses == null)
            {
                return NotFound();
            }
            var adress = await _context.Adresses.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }

            _context.Adresses.Remove(adress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdressExists(int id)
        {
            return (_context.Adresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
