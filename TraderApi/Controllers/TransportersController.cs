﻿using System;
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
    public class TransportersController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public TransportersController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/Transporters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transporter>>> GetTransporter()
        {
          if (_context.Transporter == null)
          {
              return NotFound();
          }
            return await _context.Transporter.Where(a=>a.IsDeleted == false).ToListAsync();
        }

        // GET: api/Transporters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transporter>> GetTransporter(int id)
        {
          if (_context.Transporter == null)
          {
              return NotFound();
          }
            var transporter = await _context.Transporter.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);

            if (transporter == null)
            {
                return NotFound();
            }

            return transporter;
        }

        // PUT: api/Transporters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporter(int id, Models.TransporterRequest transporter)
        {
            var transporterDb = await _context.Transporter.FindAsync(id);
            if (id != transporterDb.Id)
            {
                return BadRequest();
            }

            _context.Entry(transporterDb).State = EntityState.Modified;

            try
            {
                transporterDb.Name = transporter.Name;
                transporterDb.Mobile1 = transporter.Mobile1;
                transporterDb.ModifiedBy = transporter.UsedBy;
                transporterDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransporterExists(id))
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

        // POST: api/Transporters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transporter>> PostTransporter(Models.TransporterRequest transporter)
        {
          if (_context.Transporter == null)
          {
              return Problem("Entity set 'TraderApiContext.Transporter'  is null.");
          }
            Transporter transporterDb = new Transporter();
            try
            {
                transporterDb.Name = transporter.Name;
                transporterDb.Mobile1 = transporter.Mobile1;
                transporterDb.CreatedBy = transporter.UsedBy;
                transporterDb.CreatedDate = DateTime.Now;
                _context.Transporter.Add(transporterDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransporterExists(transporterDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTransporter", new { id = transporterDb.Id }, transporterDb);
        }

        // DELETE: api/Transporters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporter(int id)
        {
            if (_context.Transporter == null)
            {
                return NotFound();
            }
            var transporter = await _context.Transporter.FindAsync(id);
            if (transporter == null)
            {
                return NotFound();
            }

            _context.Transporter.Remove(transporter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransporterExists(int id)
        {
            return (_context.Transporter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}