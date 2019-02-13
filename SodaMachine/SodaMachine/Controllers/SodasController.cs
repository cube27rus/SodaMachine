using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodaMachine.Domain;
using SodaMachine.Domain.Models;

namespace SodaMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SodasController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SodasController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Sodas
        [HttpGet]
        public IEnumerable<Soda> GetSoda()
        {
            return _context.Soda;
        }

        // GET: api/Sodas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoda([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var soda = await _context.Soda.FindAsync(id);

            if (soda == null)
            {
                return NotFound();
            }

            return Ok(soda);
        }

        // PUT: api/Sodas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoda([FromRoute] int id, [FromBody] Soda soda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != soda.Id)
            {
                return BadRequest();
            }

            _context.Entry(soda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SodaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sodas
        [HttpPost]
        public async Task<IActionResult> PostSoda([FromBody] Soda soda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Soda.Add(soda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSoda", new { id = soda.Id }, soda);
        }

        // DELETE: api/Sodas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoda([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var soda = await _context.Soda.FindAsync(id);
            if (soda == null)
            {
                return NotFound();
            }

            _context.Soda.Remove(soda);
            await _context.SaveChangesAsync();

            return Ok(soda);
        }

        private bool SodaExists(int id)
        {
            return _context.Soda.Any(e => e.Id == id);
        }
    }
}