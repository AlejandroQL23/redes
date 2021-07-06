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
    public class GameController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;

        public GameController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }


        // GET: api/Game/GetGames
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Game.Select(gameItem => new Game()
            {
                GameId = gameItem.GameId,
                Name = gameItem.Name,
                Owner = gameItem.Owner,
                Password = gameItem.Password,
                Status = gameItem.Status
            }).ToListAsync();
        }


        [Route("[action]")]
        // GET: api/Game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Game.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Game/1 --->Also you have to put the id 
        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> PutGame(Game games)
        {
            _context.Entry(games).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(games.GameId))
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

        // POST: api/Game/PostGame
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game games)
        {
            _context.Game.Add(games);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGames", new { id = games.GameId }, games);
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
