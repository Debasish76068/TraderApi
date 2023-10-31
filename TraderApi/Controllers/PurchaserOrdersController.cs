using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraderApi.Data;
using TraderApi.Data.Entities;

namespace TraderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaserOrdersController : ControllerBase
    {
        private readonly TraderApiContext _context;
        private readonly ILogger<AgentsController> _logger;
        public PurchaserOrdersController(TraderApiContext context, ILogger<AgentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/PurchaserOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaserOrder>>> GetPurchaserOrder()
        {
            try
            {
                _logger.LogInformation($" Getting Purchaser Order Detail Information for PurchaserOrdersController.");
                if (_context.PurchaserOrder == null)
                {
                    return NotFound();
                }
                return await _context.PurchaserOrder.Where(a => a.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Purchaser Order Detail Information for PurchaserOrdersController: Exception: {ex}.");
                throw;
            }
        }

        // GET: api/PurchaserOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaserOrder>> GetPurchaserOrder(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Purchaser Order Detail Information for PurchaserOrdersController: {id}.");
                if (_context.PurchaserOrder == null)
                {
                    return NotFound();
                }
                var PurchaserOrder = await _context.PurchaserOrder.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
                if (PurchaserOrder == null)
                {
                    return NotFound();
                }
                return PurchaserOrder;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Purchaser Order Detail Information for PurchaserOrdersController: Exception: {id}, Exception: {ex}.");
                throw;
            }
        }

        // PUT: api/PurchaserOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaserOrder(int id, Models.OrderRequest PurchaserOrder)
        {
            if (_context.PurchaserOrder == null)
            {
                return NotFound();
            }
            var PurchaserOrderDb = await _context.PurchaserOrder.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
            if (id != PurchaserOrderDb?.Id)
            {
                return BadRequest();
            }
            _context.Entry(PurchaserOrderDb).State = EntityState.Modified;
            try
            {
                _logger.LogInformation($"Processing Showing the PutPurchaserOrder {PurchaserOrderDb.Name}.");
                PurchaserOrderDb.Name = PurchaserOrder.Name;
                PurchaserOrderDb.BagQuantity = PurchaserOrder.BagQuantity;
                PurchaserOrderDb.Rate = PurchaserOrder.Rate;
                PurchaserOrderDb.item = PurchaserOrder.item;
                PurchaserOrderDb.ModifiedBy = PurchaserOrder.UsedBy;
                PurchaserOrderDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PutAccountDetail: {PurchaserOrderDb.Name}, Exception: {ex}.");
                if (!PurchaserOrderExists(id))
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

        // POST: api/PurchaserOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaserOrder>> PostPurchaserOrder(Models.OrderRequest PurchaserOrder)
        {
            if (_context.PurchaserOrder == null)
            {
                return Problem("Entity set 'TraderApiContext.PurchaserOrder'  is null.");
            }
            PurchaserOrder PurchaserOrderDb = new PurchaserOrder();
            try
            {
                _logger.LogInformation($"Processing Showing the PostPurchaserOrder {PurchaserOrderDb.Name}.");
                PurchaserOrderDb.OrderNo = GeneratePurchaserOrderNumber("P");
                PurchaserOrderDb.Name = PurchaserOrder.Name;
                PurchaserOrderDb.BagQuantity = PurchaserOrder.BagQuantity;
                PurchaserOrderDb.Rate = PurchaserOrder.Rate;
                PurchaserOrderDb.item = PurchaserOrder.item;
                PurchaserOrderDb.CreatedBy = PurchaserOrder.UsedBy;
                PurchaserOrderDb.CreatedDate = DateTime.Now;
                _context.PurchaserOrder.Add(PurchaserOrderDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PostPurchaserOrder: {PurchaserOrderDb.Name}, Exception: {ex}.");
                if (!PurchaserOrderExists(PurchaserOrderDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetPurchaserOrder", new { id = PurchaserOrderDb.Id }, PurchaserOrderDb);
        }

        // DELETE: api/PurchaserOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaserOrder(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Delete Purchaser Order Detail Information for PurchaserOrdersController: {id}.");
                if (_context.PurchaserOrder == null)
                {
                    return NotFound();
                }
                var PurchaserOrder = await _context.PurchaserOrder.FindAsync(id);
                if (PurchaserOrder == null)
                {
                    return NotFound();
                }
                PurchaserOrder.IsDeleted = true;
                PurchaserOrder.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable Delete Purchaser Order Details Information for PurchaserOrdersController: {id}, Exception: {ex}.");
                throw;
            }
        }

        private bool PurchaserOrderExists(int id)
        {
            return (_context.PurchaserOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string GeneratePurchaserOrderNumber(string type)
        {
            int? totalCount = _context.PurchaserOrder?.Count();
            return $"{DateTime.Now.ToString("yy")}{type}{1 + totalCount.Value:D5}";
        }
    }
}
