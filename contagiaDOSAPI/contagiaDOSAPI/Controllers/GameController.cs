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
    public class GameController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;
        PlayerController playerController;
        RoundController roundController;
        GroupController groupController;
        public GameController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }


        // GET: Game/GetGames
        [EnableCors("GetAllPolicy")]
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame([FromHeader] string name, [FromHeader] string password, int id)
        {

            var game = await _context.Game.FindAsync(id);
            name = game.Owner;
            password = game.Password;
            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        //---------------------------------------------------------------------
        // PUT: Game/1
        [EnableCors("GetAllPolicy")]
        [Route("{games.GameId}/[action]")]
        [HttpPut]
        public async Task<IActionResult> join([FromHeader] string temporalp, [FromHeader] string password, [FromBody] Game games) // NO RETORNA NADA - return ok porque tiene que retornar
        {
            var temporalPlayers = (from game in _context.Game where game.GameId == games.GameId select game.Players).FirstOrDefault();

  
            games.Players = temporalPlayers + " , " + temporalp;
            games.Status = "Lobby";


            _context.Entry(games).State = EntityState.Modified;

            try
            {

                savePlayer(temporalp, games);
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
            return Ok();
        }
        //---------------------------------------------------------------------

        [EnableCors("GetAllPolicy")]
        [Route("{games.GameId}/[action]")]
        [HttpPut]
        public async Task<IActionResult> PutStatus(Game games)
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
            return Ok();
        }

        //---------------------------------------------------------------------
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> savePlayer(string playername, Game games)
        {

            Player p = new Player
            {
                Id = 0,
                Name = playername,
                GameId = games.GameId,
                Psycho = false//
            };

            playerController = new PlayerController(_context);
            playerController.PostPlayer(p);
            games.Player = (ICollection<Player>)p;

            await _context.SaveChangesAsync();




            return CreatedAtAction("GetGames", new { id = games.GameId }, games);
        }

        //---------------------------------------------------------------------
        // POST: Game/
        // //POST NUEVO: Game/create
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> create([FromHeader] string name, [FromBody] Game games)
        {
            games.Owner = name;
            games.Players = games.Owner;
            games.Status = "Lobby";
            _context.Game.Add(games);

            await _context.SaveChangesAsync();

            Player p = new Player
            {
                Id = 0,
                Name = games.Owner,
                GameId = games.GameId,
                Psycho = false//
            };

            playerController = new PlayerController(_context);
            playerController.PostPlayer(p);


            return CreatedAtAction("GetGames", new { id = games.GameId }, games);
        }
        //---------------------------------------------------------------------
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
        //---------------------------------------------------------------------
        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
        //---------------------------------------------------------------------
        [EnableCors("GetAllPolicy")]
        [Route("{gameId}/[action]")]
        [HttpHead("{gameId}")]
        public async Task<ActionResult<Game>> start([FromHeader] string name, [FromHeader] string password, int gameId)
        {
            Game gameStatus = (from game in _context.Game where game.GameId == gameId select game).FirstOrDefault(); ;
            playerController = new PlayerController(_context);
            roundController = new RoundController(_context);
            playerController.PostRed(gameId);
            Round round = new Round();
            round.GameId = gameId;
            roundController.PostRound(round);
            gameStatus.Status = "Leader";
            PutStatus(gameStatus);

            return Ok();
        }
        //---------------------------------------------------------------------
        [EnableCors("GetAllPolicy")]
        [Route("{gameId}/[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> group([FromHeader] string name, [FromHeader] string password, int gameId, [FromBody]string[] registerForm)
        {

            groupController = new GroupController(_context);
            groupController.PostGroup(registerForm,gameId);

            return Ok();

        }
        //---------------------------------------------------------------------
        [EnableCors("GetAllPolicy")]
        [Route("{gameId}/[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> go([FromHeader] string name, [FromHeader] string password, int gameId, [FromBody] bool IsPsycho)
        {

            roundController = new RoundController(_context);
            if (IsPsycho)
                {


                    Round[] arrayLastRound = (from rounds in _context.Round where rounds.GameId == gameId select rounds).ToArray();
                    Round lastRound = arrayLastRound[arrayLastRound.Length - 1];
                    lastRound.Psychowin = true;
                    roundController.PutRound(lastRound);

                }
                Game gamePut = (from games in _context.Game where games.GameId == gameId select games).FirstOrDefault();
                Round[] psycho = (from rounds in _context.Round where (rounds.GameId == gameId) && (rounds.Psychowin == true) select rounds).ToArray();
                Round[] exemplary = (from rounds in _context.Round where (rounds.GameId == gameId) && (rounds.Psychowin == false) select rounds).ToArray();
               
            if (psycho.Length >= 3 || exemplary.Length>=3)
            {
                gamePut.Status = "Ended";
                PutStatus(gamePut);
            }
            else
            {

                Round round = new Round();
                round.GameId = gameId;
                roundController.PostRound(round);
            }
            
            return Ok();

        }



    }
}
