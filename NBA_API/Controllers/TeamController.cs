using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBA_API.Models;

namespace NBA_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly NBA_DBContext _context;
        public TeamController(NBA_DBContext context)
        {
            _context = context;

        }

        // GET: api/Team
        // list all the team for a user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var UserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _context.Team.Where(p => p.Id == UserId).ToListAsync(); ;
        }

        // GET: api/Team/5
        // [HttpGet("{TeamName}")]
        // public async Task<ActionResult<Team>> GetTeam(string teamname)
        // {
        //     var team = await _context.Team.FindAsync(teamname);

        //     if (team == null)
        //     {
        //         return NotFound();
        //     }

        //     return team;
        // }

        [HttpPost]
        // Add new team to user's account
        public async Task<ActionResult<bool>> PostTeam([FromQuery] string teamname)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            // UserId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Team team = new Team(teamname, UserId);
            try
            {
                _context.Team.Add(team);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Unsuccessful"); ;

            }

            return Ok("Add Team Successfully");
        }


        // PUT: api/Team/5

        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTeam(int id, Team team)
        // {
        //     if (id != team.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(team).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TeamExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // DELETE: api/Team/5
        [HttpDelete]
        public async Task<IActionResult> DeleteTeam(string teamname)
        {
           var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var team = _context.Team.FirstOrDefault(p => p.Id == UserId && p.TeamName == teamname);
            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            await _context.SaveChangesAsync();

            return Ok("Delete Team Successfullly");
        }

        // private bool TeamExists(int id)
        // {
        //     return _context.Teams.Any(e => e.Id == id);
        // }
    }
}