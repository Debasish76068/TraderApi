using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraderApi.Data;
using TraderApi.Data.Entities;

namespace TraderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public AgentsController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgent()
        {
          if (_context.Agent == null)
          {
              return NotFound();
          }
            return await _context.Agent.Where(a=>a.IsDeleted==false).ToListAsync();
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
          if (_context.Agent == null)
          {
              return NotFound();
          }
            var agent = await _context.Agent.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a=>a.Id==id);

            if (agent == null)
            {
                return NotFound();
            }

            return agent;
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgent(int id, Models.AgentRequest agent)
        {
            var agentDb = await _context.Agent.FindAsync(id);
            if (id != agentDb.Id)
            {
                return BadRequest();
            }

            _context.Entry(agentDb).State = EntityState.Modified;

            try
            {
                agentDb.Name = agent.Name;
                agentDb.Mobile1 = agent.Mobile1;
                agentDb.Mobile2 = agent.Mobile2;
                agentDb.ModifiedBy = agent.UsedBy;
                agentDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
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

        // POST: api/Agents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(Models.AgentRequest agent)
        {
            if (_context.Agent == null)
          {
              return Problem("Entity set 'TraderApiContext.Agent'  is null.");
          }
            Agent agentDb = new Agent();
            try
            {
                agentDb.Name = agent.Name;
                agentDb.Mobile1 = agent.Mobile1;
                agentDb.Mobile2 = agent.Mobile2;
                agentDb.CreatedBy = agent.UsedBy;
                agentDb.CreatedDate = DateTime.Now;
                _context.Agent.Add(agentDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(agentDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetAgent", new { id = agentDb.Id }, agentDb);
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            if (_context.Agent == null)
            {
                return NotFound();
            }
            var agent = await _context.Agent.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            agent.IsDeleted=true;
            agent.ModifiedDate = DateTime.Now;
           // _context.Agent.Remove(agent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgentExists(int id)
        {
            return (_context.Agent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
