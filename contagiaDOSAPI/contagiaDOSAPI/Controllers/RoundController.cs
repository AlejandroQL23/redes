using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using contagiaDOSAPI.Models.Entities;

namespace contagiaDOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;

        public RoundController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }

        // GET: api/Round/GetRounds
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Round>>> GetRounds()
        {
            return await _context.Round.Select(roundItem => new Round()
            {
                Id = roundItem.Id,
                Leader = roundItem.Leader,
                Psychowin = roundItem.Psychowin,
            }).ToListAsync();
        }

        // GET: api/Round/5
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Round>> GetRound(int id)
        {
            var round = await _context.Round.FindAsync(id);

            if (round == null)
            {
                return NotFound();
            }

            return round;
        }

        // PUT: api/Round/1 --->Also you have to put the id
        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> PutRound(Round round)
        {
            _context.Entry(round).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoundExists(round.Id))
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

        // POST: api/Round/PostRound
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> PostRound(Round round)
        {
            _context.Round.Add(round);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRounds", new { id = round.Id }, round);
        }

        // DELETE: api/Round/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Round>> DeleteRound(int id)
        {
            var round = await _context.Round.FindAsync(id);
            if (round == null)
            {
                return NotFound();
            }

            _context.Round.Remove(round);
            await _context.SaveChangesAsync();

            return round;
        }

        private bool RoundExists(int id)
        {
            return _context.Round.Any(e => e.Id == id);
        }
    }
}
