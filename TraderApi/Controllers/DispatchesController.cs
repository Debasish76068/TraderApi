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
    public class DispatchesController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public DispatchesController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/Dispatches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispatch>>> GetDispatch()
        {
          if (_context.Dispatch == null)
          {
              return NotFound();
          }
            return await _context.Dispatch.Where(a => a.IsDeleted == false).ToListAsync();
        }

        // GET: api/Dispatches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dispatch>> GetDispatch(int id)
        {
          if (_context.Dispatch == null)
          {
              return NotFound();
          }
            var dispatch = await _context.Dispatch.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);

            if (dispatch == null)
            {
                return NotFound();
            }

            return dispatch;
        }

        // PUT: api/Dispatches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispatch(int id, Models.DispatchRequest dispatch)
        {
            var dispatchDb = await _context.Dispatch.FindAsync(id);
            if (id != dispatchDb?.Id)
            {
                return BadRequest();
            }

            _context.Entry(dispatchDb).State = EntityState.Modified;

            try
            {                
                dispatchDb.BagQuantity = dispatch.BagQuantity;
                dispatchDb.Rate = dispatch.Rate;
                dispatchDb.Item = dispatch.Item;
                dispatchDb.DispatchDate = dispatch.DispatchDate;
                dispatchDb.BookingDate = dispatch.BookingDate;
                dispatchDb.ModifiedBy = dispatch.UsedBy;
                dispatchDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispatchExists(id))
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

        // POST: api/Dispatches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dispatch>> PostDispatch(Models.DispatchRequest dispatch)
        {
          if (_context.Dispatch == null)
          {
              return Problem("Entity set 'TraderApiContext.Dispatch'  is null.");
          }

            Dispatch dispatchDb = new Dispatch();
            try
            {                
                dispatchDb.BagQuantity = dispatch.BagQuantity;
                dispatchDb.Rate = dispatch.Rate;
                dispatchDb.Item = dispatch.Item;
                dispatchDb.CreatedBy = dispatch.UsedBy;
                dispatchDb.CreatedDate = DateTime.Now;
                dispatchDb.DispatchDate = dispatch.DispatchDate;
                dispatchDb.BookingDate = dispatch.BookingDate;
                _context.Dispatch.Add(dispatchDb);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!DispatchExists(dispatchDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return CreatedAtAction("GetDispatch", new { id = dispatchDb.Id }, dispatchDb);
        }

        // DELETE: api/Dispatches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispatch(int id)
        {
            if (_context.Dispatch == null)
            {
                return NotFound();
            }
            var dispatch = await _context.Dispatch.FindAsync(id);
            if (dispatch == null)
            {
                return NotFound();
            }
            dispatch.IsDeleted = true;
            dispatch.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispatchExists(int id)
        {
            return (_context.Dispatch?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
