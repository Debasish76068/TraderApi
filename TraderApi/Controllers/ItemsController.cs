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
    public class ItemsController : ControllerBase
    {
        private readonly TraderApiContext _context;
        private readonly ILogger<ItemsController> _logger;
        public ItemsController(TraderApiContext context, ILogger<ItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Items>>> GetItems()
        {
            try
            {
                _logger.LogInformation($" Getting Item Details Information for ItemsController.");
                if (_context.Items == null)
                {
                    return NotFound();
                }
                return await _context.Items.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Item Details Information for ItemsController: Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Item Details Information: Exception:{ex.Message}.");
            }
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> GetItems(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Item Details Information for ItemsController: {id}.");
                if (_context.Items == null)
                {
                    return NotFound();
                }
                var items = await _context.Items.FindAsync(id);

                if (items == null)
                {
                    return NotFound();
                }
                return items;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Item Details Information: Exception:{ex.Message}.");
            }
        }

        //// PUT: api/Items/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutItems(int id, Items items)
        //{
        //    if (id != items.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(items).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ItemsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Items
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Items>> PostItems(Items items)
        //{
        //  if (_context.Items == null)
        //  {
        //      return Problem("Entity set 'TraderApiContext.Items'  is null.");
        //  }
        //    _context.Items.Add(items);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetItems", new { id = items.Id }, items);
        //}

        //// DELETE: api/Items/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteItems(int id)
        //{
        //    if (_context.Items == null)
        //    {
        //        return NotFound();
        //    }
        //    var items = await _context.Items.FindAsync(id);
        //    if (items == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Items.Remove(items);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ItemsExists(int id)
        //{
        //    return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
