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
    public class GroupController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;

        public GroupController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }

        //GET: Group/GetGroups
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Group.Select(groupItem => new Group()
            {
                Id = groupItem.Id,
                Name = groupItem.Name,
                RoundId = groupItem.RoundId
            }).ToListAsync();
        }

        // GET: Group/5
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var group = await _context.Group.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // PUT: Group/1 --->Also you have to put the id 
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> PutGroup(Group group)
        {
            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.Id))
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

        // POST: Group/PostGroup
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup([FromBody] string[] a, [FromHeader] int gameId)
        {
            int[] arrayLastRound = (from rounds in _context.Round where rounds.GameId == gameId select rounds.Id).ToArray();
            int lastRound = arrayLastRound[arrayLastRound.Length - 1];
            Group group = new Group();
            for (int i = 0; i < a.Length; i++)
            {
                group.Id = 0;
                group.Name = a[i];
                group.RoundId = lastRound;

                _context.Group.Add(group);
                await _context.SaveChangesAsync();

            }

            return CreatedAtAction("GetGroups", new { id = group.Id }, group);
        }

        // DELETE: Group/5
        [EnableCors("GetAllPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> DeleteGroup(int id)
        {
            var group = await _context.Group.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _context.Group.Remove(group);
            await _context.SaveChangesAsync();

            return group;
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }
    }
}
