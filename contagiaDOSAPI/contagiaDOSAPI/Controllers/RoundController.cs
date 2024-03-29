﻿using System;
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
    //[EnableCors]
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RoundController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;
        PlayerController playerController;
        public RoundController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }

        // GET: Round/GetRounds
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Round>>> GetRounds()
        {
            return await _context.Round.Select(roundItem => new Round()
            {
                Id = roundItem.Id,
                Leader = roundItem.Leader,
                Psychowin = roundItem.Psychowin,
                GameId = roundItem.GameId,
                RoundNumber = roundItem.RoundNumber
            }).ToListAsync();
        }

        // GET: Round/5
        [EnableCors("GetAllPolicy")]
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
         
        // PUT: Round/1 --->Also you have to put the id
        [EnableCors("GetAllPolicy")]
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

        // POST: Round/PostRound
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Game>> PostRound(Round round)
        {
            playerController = new PlayerController(_context);
            round.Leader = playerController.leader(round.GameId);
            Round[] arrayLastRoundNumber = (from rounds in _context.Round where rounds.GameId == round.GameId select rounds).ToArray();
            round.RoundNumber = arrayLastRoundNumber.Length;
            round.Psychowin = false;
            _context.Round.Add(round);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRounds", new { id = round.Id }, round);
        }

        // DELETE: Round/5
        [EnableCors("GetAllPolicy")]
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
