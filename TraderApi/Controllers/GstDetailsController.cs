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
        private readonly ILogger<GstDetailsController> _logger;

        public GstDetailsController(TraderApiContext context, ILogger<GstDetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/GstDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GstDetail>>> GetGstDetail()
        {
            try
            {
                _logger.LogInformation($" Getting Gst Detail Information for GstDetailsControllerr.");
                if (_context.GstDetail == null)
                {
                    return NotFound();
                }
                return await _context.GstDetail.Where(a => a.IsDeleted == false).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Gst Detail Information for GstDetailsController: Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Gst Detail Information: Exception:{ex.Message}.");
            }
        }

        // GET: api/GstDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GstDetail>> GetGstDetail(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Gst Detail Information for GstDetailsController: {id}.");
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
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Gst Detail Information for GstDetailsController: Exception: {id}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Gst Detail Information: Exception:{ex.Message}.");
            }
        }

        // PUT: api/GstDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGstDetail(int id, Models.GstDetailRequest gstDetail)
        {
            if (_context.GstDetail == null)
            {
                return NotFound();
            }
            var gstDetailDb = await _context.GstDetail.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
            if (id != gstDetailDb?.Id)
            {
                return BadRequest();
            }
            _context.Entry(gstDetailDb).State = EntityState.Modified;
            try
            {
                _logger.LogInformation($"Processing Showing the PutGstDetail {gstDetailDb.Name} ");
                gstDetailDb.Name = gstDetail.Name;
                gstDetailDb.ApmcNumber = gstDetail.ApmcNumber;
                gstDetailDb.GstNumber = gstDetail.GstNumber;
                gstDetailDb.ModifiedBy = gstDetail.UsedBy;
                gstDetailDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PutGstDetail: {gstDetailDb.Name}, Exception: {ex}.");
                if (!GstDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, $"An error occurred while updating Gst Detail Information: Exception:{ex.Message}.");
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
                _logger.LogInformation($"Processing Showing the PostGstDetail {gstDetailDb.Name} ");
                gstDetailDb.Name = gstDetail.Name;
                gstDetailDb.ApmcNumber = gstDetail.ApmcNumber;
                gstDetailDb.GstNumber = gstDetail.GstNumber;
                gstDetailDb.CreatedBy = gstDetail.UsedBy;
                gstDetailDb.CreatedDate = DateTime.Now;
                _context.GstDetail.Add(gstDetailDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PostGstDetail: {gstDetailDb.Name}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while inserting Gst Detail Information: Exception:{ex.Message}.");
            }
            return CreatedAtAction("GetGstDetail", new { id = gstDetailDb.Id }, gstDetailDb);
        }

        // DELETE: api/GstDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGstDetail(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Delete Gst Detail Information for GstDetailsController: {id}.");
                if (_context.GstDetail == null)
                {
                    return NotFound();
                }
                var gstDetail = await _context.GstDetail.FindAsync(id);
                if (gstDetail == null)
                {
                    return NotFound();
                }
                gstDetail.IsDeleted = true;
                gstDetail.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Error: Unable Delete Gst Detail Information for GstDetailsController: {id}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while deleting Gst Detail Information: Exception:{ex.Message}.");
            }
        }
        private bool GstDetailExists(int id)
        {
            return (_context.GstDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
