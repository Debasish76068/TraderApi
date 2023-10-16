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
    public class OrdersController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public OrdersController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
          if (_context.Order == null)
          {
              return NotFound();
          }
            return await _context.Order.Where(a => a.IsDeleted == false).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Order == null)
          {
              return NotFound();
          }
            var order = await _context.Order.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Models.OrderRequest order)
        {
            var orderDb = await _context.Order.FindAsync(id);
            if (id != orderDb.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDb).State = EntityState.Modified;

            try
            {
                orderDb.AgentName= order.AgentName;
                orderDb.BagQuantity= order.BagQuantity;
                orderDb.Rate= order.Rate;
                orderDb.item = order.item;
                orderDb.ModifiedBy = order.UsedBy;
                orderDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Models.OrderRequest order)
        {
          if (_context.Order == null)
          {
              return Problem("Entity set 'TraderApiContext.Order'  is null.");
          }
            Order orderDb = new Order();
            try
            {
                orderDb.AgentName = order.AgentName;
                orderDb.BagQuantity = order.BagQuantity;
                orderDb.Rate = order.Rate;
                orderDb.item = order.item;
                orderDb.CreatedBy = order.UsedBy;
                orderDb.CreatedDate = DateTime.Now;
                _context.Order.Add(orderDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(orderDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = orderDb.Id }, orderDb);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.IsDeleted = true;
            order.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
