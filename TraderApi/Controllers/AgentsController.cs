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
        private readonly ILogger<AgentsController> _logger;
        public AgentsController(TraderApiContext context, ILogger<AgentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgent()
        {
            try
            {
                _logger.LogInformation($" Getting Agent Details Information for AgentsController.");
                if (_context.Agent == null)
                {
                    return NotFound();
                }
                return await _context.Agent.Where(a => a.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Agent Details Information for AgentsController: Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Agent Details Information: Exception:{ex.Message}.");
            }
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Agent Details Information for AgentsController: {id}.");
                if (_context.Agent == null)
                {
                    return NotFound();
                }
                var agent = await _context.Agent.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
                if (agent == null)
                {
                    return NotFound();
                }
                return agent;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Agent Details Information for AgentsController: Exception: {id}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Agent Details Information: Exception:{ex.Message}.");
            }
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgent(int id, Models.AgentRequest agent)
        {
            if (_context.Agent == null)
            {
                return NotFound();
            }
            var agentDb = await _context.Agent.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
            if (id != agentDb?.Id)
            {
                return BadRequest();
            }
            _context.Entry(agentDb).State = EntityState.Modified;
            try
            {
                _logger.LogInformation($"Processing Showing the PutAgent {agentDb.Name}.");
                agentDb.Name = agent.Name;
                agentDb.Mobile1 = agent.Mobile1;
                agentDb.Mobile2 = agent.Mobile2;
                agentDb.ModifiedBy = agent.UsedBy;
                agentDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PutAgent:{agentDb.Name}, Exception: {ex}.");
                if (!AgentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, $"An error occurred while updating Agent Details Information: Exception:{ex.Message}.");
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
                _logger.LogInformation($"Processing update for PostAgent {agentDb.Name}.");
                agentDb.Name = agent.Name;
                agentDb.Mobile1 = agent.Mobile1;
                agentDb.Mobile2 = agent.Mobile2;
                agentDb.CreatedBy = agent.UsedBy;
                agentDb.CreatedDate = DateTime.Now;
                _context.Agent.Add(agentDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PostAgent:{agentDb.Name}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while inserting Agent Details Information: Exception:{ex.Message}.");
            }
            return CreatedAtAction("GetAgent", new { id = agentDb.Id }, agentDb);
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Delete Agent Detail Information for AgentsController.: {id}.");
                if (_context.Agent == null)
                {
                    return NotFound();
                }
                var agent = await _context.Agent.FindAsync(id);
                if (agent == null)
                {
                    return NotFound();
                }
                agent.IsDeleted = true;
                agent.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable Delete Agent Details Information for AgentsController: {id}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while deleting Agent Details Information: Exception:{ex.Message}.");
            }
        }
        private bool AgentExists(int id)
        {
            return (_context.Agent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
