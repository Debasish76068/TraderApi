using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraderApi.Data;
using TraderApi.Data.Entities;

namespace TraderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentOrdersController : ControllerBase
    {
        private readonly TraderApiContext _context;
        private readonly ILogger<AgentsController> _logger;
        public AgentOrdersController(TraderApiContext context, ILogger<AgentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/AgentOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentOrder>>> GetAgentOrder()
        {
            try
            {
                _logger.LogInformation($" Getting Agent Details Information for AgentOrdersController.");
                if (_context.AgentOrder == null)
                {
                    return NotFound();
                }
                return await _context.AgentOrder.Where(a => a.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Agent Details Information for AgentOrdersController: Exception: {ex}.");
                throw;
            }
        }

        // GET: api/AgentOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentOrder>> GetAgentOrder(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Agent Order Details Information for AgentOrdersController: {id}.");
                if (_context.AgentOrder == null)
                {
                    return NotFound();
                }
                var AgentOrder = await _context.AgentOrder.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
                if (AgentOrder == null)
                {
                    return NotFound();
                }
                return AgentOrder;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Agent Order Details Information for AgentOrdersController: Exception: {id}, Exception: {ex}.");
                throw;
            }
        }

        // PUT: api/AgentOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgentOrder(int id, Models.OrderRequest AgentOrder)
        {
            if (_context.AgentOrder == null)
            {
                return NotFound();
            }
            var AgentOrderDb = await _context.AgentOrder.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
            if (id != AgentOrderDb?.Id)
            {
                return BadRequest();
            }
            _context.Entry(AgentOrderDb).State = EntityState.Modified;
            try
            {
                _logger.LogInformation($"Processing Showing the PutAgentOrder {AgentOrderDb.Name}.");
                AgentOrderDb.Name = AgentOrder.Name;
                AgentOrderDb.BagQuantity = AgentOrder.BagQuantity;
                AgentOrderDb.Rate = AgentOrder.Rate;
                AgentOrderDb.item = AgentOrder.item;
                AgentOrderDb.ModifiedBy = AgentOrder.UsedBy;
                AgentOrderDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PutAccountDetail:{AgentOrderDb.Name}, Exception: {ex}.");
                if (!AgentOrderExists(id))
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

        // POST: api/AgentOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AgentOrder>> PostAgentOrder(Models.OrderRequest AgentOrder)
        {
            if (_context.AgentOrder == null)
            {
                return Problem("Entity set 'TraderApiContext.AgentOrder'  is null.");
            }
            AgentOrder AgentOrderDb = new AgentOrder();
            try
            {
                _logger.LogInformation($"Processing Showing the PostAgentOrder {AgentOrderDb.Name}.");
                AgentOrderDb.OrderNo = GenerateAgentOrderNumber("A");
                AgentOrderDb.Name = AgentOrder.Name;
                AgentOrderDb.BagQuantity = AgentOrder.BagQuantity;
                AgentOrderDb.Rate = AgentOrder.Rate;
                AgentOrderDb.item = AgentOrder.item;
                AgentOrderDb.CreatedBy = AgentOrder.UsedBy;
                AgentOrderDb.CreatedDate = DateTime.Now;
                _context.AgentOrder.Add(AgentOrderDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PostAgentOrder: {AgentOrderDb.Name}, Exception: {ex}.");
                if (!AgentOrderExists(AgentOrderDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetAgentOrder", new { id = AgentOrderDb.Id }, AgentOrderDb);
        }

        // DELETE: api/AgentOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentOrder(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Delete Agent Order Detail Information for AgentOrdersController: {id}.");
                if (_context.AgentOrder == null)
                {
                    return NotFound();
                }
                var AgentOrder = await _context.AgentOrder.FindAsync(id);
                if (AgentOrder == null)
                {
                    return NotFound();
                }
                AgentOrder.IsDeleted = true;
                AgentOrder.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable Delete Agent Order Details Information for AgentOrdersController: {id}, Exception: {ex}.");
                throw;
            }
        }
        private bool AgentOrderExists(int id)
        {
            return (_context.AgentOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string GenerateAgentOrderNumber(string type)
        {
            int? totalCount = _context.AgentOrder?.Count();
            return $"{DateTime.Now.ToString("yy")}{type}{1 + totalCount.Value:D5}";
        }
    }
}
