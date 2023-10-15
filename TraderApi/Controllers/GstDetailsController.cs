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
    public class GstDetailsController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public GstDetailsController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/GstDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GstDetail>>> GetGstDetail()
        {
          if (_context.GstDetail == null)
          {
              return NotFound();
          }
            return await _context.GstDetail.Where(a => a.IsDeleted == false).ToListAsync();
        }

        // GET: api/GstDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GstDetail>> GetGstDetail(int id)
        {
          if (_context.GstDetail == null)
          {
              return NotFound();
          }
            var gstDetail = await _context.GstDetail.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);

            if (gstDetail == null)
            {
                return NotFound();
            }

            return gstDetail;
        }

        // PUT: api/GstDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGstDetail(int id, Models.GstDetailRequest gstDetail)
        {
            var gstDetailDb = await _context.GstDetail.FindAsync(id);

            if (id != gstDetailDb.Id)
            {
                return BadRequest();
            }

            _context.Entry(gstDetailDb).State = EntityState.Modified;

            try
            {
                gstDetailDb.Name = gstDetail.Name;
                gstDetailDb.ApmcNumber = gstDetail.ApmcNumber;
                gstDetailDb.GstNumber = gstDetail.GstNumber;
                gstDetailDb.ModifiedBy = gstDetail.UsedBy;
                gstDetailDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GstDetailExists(id))
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

        // POST: api/GstDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GstDetail>> PostGstDetail(Models.GstDetailRequest gstDetail)
        {
          if (_context.GstDetail == null)
          {
              return Problem("Entity set 'TraderApiContext.GstDetail'  is null.");
          }
            GstDetail gstDetailDb=new GstDetail();
            try
            {   
                gstDetailDb.Name = gstDetail.Name;
                gstDetailDb.ApmcNumber = gstDetail.ApmcNumber;
                gstDetailDb.GstNumber = gstDetail.GstNumber;
                gstDetailDb.CreatedBy = gstDetail.UsedBy;
                gstDetailDb.CreatedDate = DateTime.Now;
                _context.GstDetail.Add(gstDetailDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GstDetailExists(gstDetailDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetGstDetail", new { id = gstDetailDb.Id }, gstDetailDb);
        }

        // DELETE: api/GstDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGstDetail(int id)
        {
            if (_context.GstDetail == null)
            {
                return NotFound();
            }
            var gstDetail = await _context.GstDetail.FindAsync(id);
            if (gstDetail == null)
            {
                return NotFound();
            }

            _context.GstDetail.Remove(gstDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GstDetailExists(int id)
        {
            return (_context.GstDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
