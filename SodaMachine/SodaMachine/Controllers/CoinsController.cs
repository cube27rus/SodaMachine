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
    public class CoinsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CoinsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Coins
        [HttpGet]
        public IEnumerable<Coin> GetCoin()
        {
            return _context.Coin;
        }

        // GET: api/Coins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coin = await _context.Coin.FindAsync(id);

            if (coin == null)
            {
                return NotFound();
            }

            return Ok(coin);
        }

        // PUT: api/Coins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoin([FromRoute] int id, [FromBody] Coin coin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coin.Id)
            {
                return BadRequest();
            }

            _context.Entry(coin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinExists(id))
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

        // POST: api/Coins
        [HttpPost]
        public async Task<IActionResult> PostCoin([FromBody] Coin coin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Coin.Add(coin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoin", new { id = coin.Id }, coin);
        }

        // DELETE: api/Coins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coin = await _context.Coin.FindAsync(id);
            if (coin == null)
            {
                return NotFound();
            }

            _context.Coin.Remove(coin);
            await _context.SaveChangesAsync();

            return Ok(coin);
        }

        private bool CoinExists(int id)
        {
            return _context.Coin.Any(e => e.Id == id);
        }
    }
}