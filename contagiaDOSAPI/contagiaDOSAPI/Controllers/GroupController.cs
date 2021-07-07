﻿using System;
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
    public class GroupController : ControllerBase
    {
        private readonly contagiaDOSredesContext _context;

        public GroupController(contagiaDOSredesContext context)
        {
            _context = new contagiaDOSredesContext();
        }

        //GET: api/Group/GetGroups
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Group.Select(groupItem => new Group()
            {
                Id = groupItem.Id,
                PlayerId = groupItem.PlayerId,
                Name = groupItem.Name
            }).ToListAsync();
        }

        [Route("[action]")]
        // GET: api/Group/5
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

        // PUT: api/Group/1 --->Also you have to put the id 
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

        // POST: api/Group/PostGroup
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group groups)
        {
            _context.Group.Add(groups);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroups", new { id = groups.Id }, groups);
        }

        // DELETE: api/Group/5
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