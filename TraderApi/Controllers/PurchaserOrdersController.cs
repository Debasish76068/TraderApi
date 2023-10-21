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
    public class PurchaserOrdersController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public PurchaserOrdersController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/PurchaserOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaserOrder>>> GetPurchaserOrder()
        {
          if (_context.PurchaserOrder == null)
          {
              return NotFound();
          }
            return await _context.PurchaserOrder.Where(a => a.IsDeleted == false).ToListAsync();
        }

        // GET: api/PurchaserOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaserOrder>> GetPurchaserOrder(int id)
        {
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

        // PUT: api/PurchaserOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaserOrder(int id, Models.OrderRequest PurchaserOrder)
        {
            var PurchaserOrderDb = await _context.PurchaserOrder.FindAsync(id);
            if (id != PurchaserOrderDb?.Id)
            {
                return BadRequest();
            }

            _context.Entry(PurchaserOrderDb).State = EntityState.Modified;

            try
            {
                PurchaserOrderDb.Name= PurchaserOrder.Name;
                PurchaserOrderDb.BagQuantity= PurchaserOrder.BagQuantity;
                PurchaserOrderDb.Rate= PurchaserOrder.Rate;
                PurchaserOrderDb.item = PurchaserOrder.item;
                PurchaserOrderDb.ModifiedBy = PurchaserOrder.UsedBy;
                PurchaserOrderDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
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
            catch (DbUpdateConcurrencyException)
            {
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

        private bool PurchaserOrderExists(int id)
        {
            return (_context.PurchaserOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string GeneratePurchaserOrderNumber(string type)
        {
            string PurchaserOrderNumber = string.Empty;
            int PurchaserOrderDefaultvalue = 00001;
            int? totalCount= _context.PurchaserOrder?.Count();
            PurchaserOrderNumber = DateTime.Now.Year.ToString("yy") + type + PurchaserOrderDefaultvalue;
            if (totalCount.HasValue)
            {
                PurchaserOrderNumber = DateTime.Now.Year.ToString("yy") + type +(PurchaserOrderDefaultvalue+ totalCount);
            }
            return PurchaserOrderNumber ;
        }
    }
}
