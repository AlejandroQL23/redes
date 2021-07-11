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
    public class PlayerController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;

        public PlayerController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }

        // GET: Player/GetPlayers
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            leader(1);

            return await _context.Player.Select(playerItem => new Player()
            {
                Id = playerItem.Id,
                Name = playerItem.Name,
                GameId = playerItem.GameId,
                Psycho = playerItem.Psycho

            }).ToListAsync();
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        // GET: Player/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var players = await _context.Player.FindAsync(id);

            if (players == null)
            {
                return NotFound();
            }

            return players;
        }


        // PUT: Player/1 --->Also you have to put the id 
        [EnableCors("GetAllPolicy")]
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

        // POST: Player/PostPlayer
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player players)
        {
            players.Psycho = false;
            _context.Player.Add(players);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayers", new { id = players.Id }, players);
        }

        // DELETE: Player/5
        [EnableCors("GetAllPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }


        [EnableCors("GetAllPolicy")]
        //----------------------------------------------------------------------------
        // POST: Player/PostPlayer/3 //PRUEBA, PUEDE ESTAR MAL
        public ActionResult PostRed(int gameId)
        {
            int[] array = (from player in _context.Player where player.GameId == gameId select player.Id ).ToArray();
            int id = array.Length;


            string resultToReturn = "";
            if (id < 5 || id > 10)
            {
                resultToReturn = "Número incorrecto de jugadores, solo se admiten de 5 a 10 y usted ingresó: " + id;
            }
            else
            if (id == 5)
            {
                resultToReturn = logic(id, 3, 2, array);
            }
            else if (id == 6)
            {
                resultToReturn = logic(id, 4, 2, array);
            }
            else if (id == 7)
            {
                resultToReturn = logic(id, 4, 3, array);
            }
            else if (id == 8)
            {
                resultToReturn = logic(id, 5, 3, array);
            }
            else if (id == 9)
            {
                resultToReturn = logic(id, 6, 3, array);
            }
            else if (id == 10)
            {
                resultToReturn = logic(id, 6, 4, array);
            }

            return Ok(resultToReturn);
        }

        [EnableCors("GetAllPolicy")]
        public string logic(int id, int e, int p, int[] array)
        {
            int ejemplares = e;
            int psicopatas = p;
            string returnV = "";
            int[] arrayGenerico = array;
            

            Random r = new Random();
            for (int i = 1; i <= id; i++)
            {
                int x = r.Next(0, 2);
                Console.WriteLine(x);
                if (x == 0)
                {
                    if (ejemplares > 0)
                    {
                        string res = arrayGenerico[i - 1] + " ES EJEMPLAR\n";

                        var result = (from pl in _context.Player where pl.Id == arrayGenerico[i - 1] select pl).SingleOrDefault();
                        result.Psycho = false;
                        _context.SaveChanges();

                        returnV = string.Concat(returnV, res);
                        ejemplares--;
                    }
                    else
                    {
                        var result = (from pl in _context.Player where pl.Id == arrayGenerico[i - 1] select pl).SingleOrDefault();
                        result.Psycho = true;
                        _context.SaveChanges();

                        string res = arrayGenerico[i - 1] + " ES PSICOPATA\n";
                        returnV = string.Concat(returnV, res);
                        psicopatas--;
                    }

                }
                else
                {
                    if (psicopatas > 0)
                    {
                        var result = (from pl in _context.Player where pl.Id == arrayGenerico[i - 1] select pl).SingleOrDefault();
                        result.Psycho = true;
                        _context.SaveChanges();
                        string res = arrayGenerico[i - 1] + " ES PSICOPATA\n";
                        returnV = string.Concat(returnV, res);
                        psicopatas--;
                    }
                    else
                    {
                        var result = (from pl in _context.Player where pl.Id == arrayGenerico[i - 1] select pl).SingleOrDefault();
                        result.Psycho = false;
                        _context.SaveChanges();
                        string res = arrayGenerico[i - 1] + " ES EJEMPLAR\n";
                        returnV = string.Concat(returnV, res);
                        ejemplares--;
                    }

                }

            }
            return returnV;
        }

        [EnableCors("GetAllPolicy")]
        public void leader(int gameId) {
            Player[] array = (from player in _context.Player where player.GameId == gameId select player).ToArray();
            Random r = new Random();
            int x = r.Next(0, array.Length);
            Console.WriteLine(array[x].Name);
        }


    }
}
