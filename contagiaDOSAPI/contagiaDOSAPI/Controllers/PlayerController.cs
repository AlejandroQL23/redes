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
    public class PlayerController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;

        public PlayerController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }

        // GET: api/Player/GetPlayers
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.Select(playerItem => new Player()
            {
                Id = playerItem.Id,
                Name = playerItem.Name,
                Leader = playerItem.Leader,
                Rol = playerItem.Rol,
                Infected = playerItem.Infected

            }).ToListAsync();
        }

        [Route("[action]")]
        // GET: api/Player/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var players = await _context.Players.FindAsync(id);

            if (players == null)
            {
                return NotFound();
            }

            return players;
        }


        // PUT: api/Player/1 --->Also you have to put the id 
        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> PutPlayer(Player players)
        {

            _context.Entry(players).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(players.Id))
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

        // POST: api/Player/PostPlayer
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player players)
        {
            _context.Players.Add(players);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayers", new { id = players.Id }, players);
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
