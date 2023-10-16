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
    public class PurchasersController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public PurchasersController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/Purchasers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchaser>>> GetPurchaser()
        {
          if (_context.Purchaser == null)
          {
              return NotFound();
          }
            return await _context.Purchaser.Where(a=>a.IsDeleted==false).ToListAsync();
        }

        // GET: api/Purchasers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchaser>> GetPurchaser(int id)
        {
          if (_context.Purchaser == null)
          {
              return NotFound();
          }
            var purchaser = await _context.Purchaser.Where(a=>a.IsDeleted==false).FirstOrDefaultAsync(a=>a.Id==id);

            if (purchaser == null)
            {
                return NotFound();
            }

            return purchaser;
        }

        // PUT: api/Purchasers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaser(int id, Models.PurchaserRequest purchaser)
        {
            var purchaserDb = await _context.Purchaser.FindAsync(id);
            if (id != purchaserDb.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaserDb).State = EntityState.Modified;

            try
            {
                purchaserDb.Name = purchaser.Name;
                purchaserDb.Mobile1 = purchaser.Mobile1;
                purchaserDb.Mobile2= purchaser.Mobile2;
                purchaserDb.ModifiedBy = purchaser.UsedBy;
                purchaserDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaserExists(id))
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

        // POST: api/Purchasers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchaser>> PostPurchaser(Models.PurchaserRequest purchaser)
        {
          if (_context.Purchaser == null)
          {
              return Problem("Entity set 'TraderApiContext.Purchaser'  is null.");
          }
            Purchaser purchaserDb = new Purchaser();
            try
            {
                purchaserDb.Name = purchaser.Name;
                purchaserDb.Mobile1 = purchaser.Mobile1;
                purchaserDb.Mobile2 = purchaser.Mobile2;
                purchaserDb.CreatedBy = purchaser.UsedBy;
                purchaserDb.CreatedDate = DateTime.Now;
                _context.Purchaser.Add(purchaserDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaserExists(purchaserDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaser", new { id = purchaserDb.Id }, purchaserDb);
        }

        // DELETE: api/Purchasers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaser(int id)
        {
            if (_context.Purchaser == null)
            {
                return NotFound();
            }
            var purchaser = await _context.Purchaser.FindAsync(id);
            if (purchaser == null)
            {
                return NotFound();
            }

            purchaser.IsDeleted = true;
            purchaser.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaserExists(int id)
        {
            return (_context.Purchaser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
