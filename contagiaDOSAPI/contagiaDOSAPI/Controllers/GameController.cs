using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using contagiaDOSAPI.Models.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace contagiaDOSAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[EnableCors("AllowOrigin")]
    public class GameController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;
        PlayerController playerController;

        public GameController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }


        // GET: Game/GetGames
        [EnableCors("GetAllPolicy")]
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

        // GET: Game/5
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
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

        // PUT: Game/1 --->Also you have to put the id 
        [EnableCors("GetAllPolicy")]
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

        // POST: Game/PostGame
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game games)
        {
            games.Status = "Lobby";
            _context.Game.Add(games);
            await _context.SaveChangesAsync();

            Player p = new Player
            {
                Id = 0,
                Name = games.Owner,
                GameId = games.GameId,
                Psycho = false
            };
            playerController = new PlayerController(_context);
            playerController.PostPlayer(p);

            return CreatedAtAction("GetGames", new { id = games.GameId }, games);
        }

        // DELETE: Game/5
        [EnableCors("GetAllPolicy")]
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

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpHead("{gameId}")]
        public async Task<ActionResult<Game>> start(int gameId)
        {
            playerController = new PlayerController(_context);
            playerController.PostRed(gameId);
            return Ok();
        }

    }
}
