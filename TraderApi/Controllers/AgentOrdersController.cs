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

        public AgentOrdersController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/AgentOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentOrder>>> GetAgentOrder()
        {
            if (_context.AgentOrder == null)
            {
                return NotFound();
            }
            return await _context.AgentOrder.Where(a => a.IsDeleted == false).ToListAsync();
        }

        // GET: api/AgentOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentOrder>> GetAgentOrder(int id)
        {
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

        // PUT: api/AgentOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgentOrder(int id, Models.OrderRequest AgentOrder)
        {
            var AgentOrderDb = await _context.AgentOrder.FindAsync(id);
            if (id != AgentOrderDb?.Id)
            {
                return BadRequest();
            }

            _context.Entry(AgentOrderDb).State = EntityState.Modified;

            try
            {
                AgentOrderDb.Name = AgentOrder.Name;
                AgentOrderDb.BagQuantity = AgentOrder.BagQuantity;
                AgentOrderDb.Rate = AgentOrder.Rate;
                AgentOrderDb.item = AgentOrder.item;
                AgentOrderDb.ModifiedBy = AgentOrder.UsedBy;
                AgentOrderDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
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
            catch (DbUpdateConcurrencyException)
            {
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

        private bool AgentOrderExists(int id)
        {
            return (_context.AgentOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string GenerateAgentOrderNumber(string type)
        {
            string AgentOrderNumber = string.Empty;
            int AgentOrderDefaultvalue = 00001;
            int? totalCount = _context.AgentOrder?.Count();
            AgentOrderNumber = DateTime.Now.Year.ToString("yy") + type + AgentOrderDefaultvalue;
            if (totalCount.HasValue)
            {
                AgentOrderNumber = DateTime.Now.Year.ToString("yy") + type + (AgentOrderDefaultvalue + totalCount);
            }
            return AgentOrderNumber;
        }
    }
}
