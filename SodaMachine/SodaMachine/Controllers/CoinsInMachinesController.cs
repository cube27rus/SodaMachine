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
    public class CoinsInMachinesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CoinsInMachinesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/CoinsInMachines
        [HttpGet]
        public IEnumerable<CoinsInMachine> GetCoinsInMachine()
        {
            return _context.CoinsInMachine.Include(x => x.Coin);
        }

        // GET: api/CoinsInMachines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoinsInMachine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coinsInMachine = await _context.CoinsInMachine.FindAsync(id);

            if (coinsInMachine == null)
            {
                return NotFound();
            }

            return Ok(coinsInMachine);
        }

        // PUT: api/CoinsInMachines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoinsInMachine([FromRoute] int id, [FromBody] CoinsInMachine coinsInMachine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coinsInMachine.Id)
            {
                return BadRequest();
            }

            _context.Entry(coinsInMachine).State = EntityState.Modified;
            _context.Entry(coinsInMachine.Coin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinsInMachineExists(id))
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

        // POST: api/CoinsInMachines
        [HttpPost]
        public async Task<IActionResult> PostCoinsInMachine([FromBody] CoinsInMachine coinsInMachine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoinsInMachine.Add(coinsInMachine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoinsInMachine", new { id = coinsInMachine.Id }, coinsInMachine);
        }

        // DELETE: api/CoinsInMachines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoinsInMachine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coinsInMachine = await _context.CoinsInMachine.FindAsync(id);
            if (coinsInMachine == null)
            {
                return NotFound();
            }

            _context.CoinsInMachine.Remove(coinsInMachine);
            await _context.SaveChangesAsync();

            return Ok(coinsInMachine);
        }

        private bool CoinsInMachineExists(int id)
        {
            return _context.CoinsInMachine.Any(e => e.Id == id);
        }
    }
}