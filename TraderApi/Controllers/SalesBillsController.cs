using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraderApi.Data;
using TraderApi.Data.Entities;

namespace TraderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesBillsController : ControllerBase
    {
        private readonly TraderApiContext _context;
        private readonly ILogger<SalesBillsController> _logger;
        public SalesBillsController(TraderApiContext context, ILogger<SalesBillsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/SalesBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesBill>>> GetSalesBill()
        {
            try
            {
                _logger.LogInformation($" Getting Sales Bills Detail Information for SalesBillsController.");
                if (_context.SalesBill == null)
                {
                    return NotFound();
                }
                return await _context.SalesBill.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Sales Bills Detail Information for SalesBillsController: Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Sales Bills Detail Information: Exception:{ex.Message}.");
            }
        }

        // GET: api/SalesBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesBill>> GetSalesBill(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Sales Bills Detail Information for SalesBillsController: {id}.");
                if (_context.SalesBill == null)
                {
                    return NotFound();
                }
                var salesBill = await _context.SalesBill.FindAsync(id);
                if (salesBill == null)
                {
                    return NotFound();
                }
                return salesBill;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Sales Bills Detail Information for SalesBillsController: Exception: {id}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Sales Bills Detail Information: Exception:{ex.Message}.");
            }
        }

        // GET: api/SalesBills/5
        [HttpGet("{purchaserId}")]
        public async Task<ActionResult<SalesBill>> GePurchaserSalesBill(int purchaserId)
        {
            try
            {
                _logger.LogInformation($" Getting Purchaser Sales Bills Detail Information for SalesBillsController: {purchaserId}.");
                if (_context.SalesBill == null)
                {
                    return NotFound();
                }
                var salesBill = await _context.SalesBill.Where(a => a.PurchaserId == purchaserId).FirstOrDefaultAsync();
                if (salesBill == null)
                {
                    return NotFound();
                }
                return salesBill;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Purchaser Sales Bills Detail Information for SalesBillsController: Exception: {purchaserId}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while retrieving Purchaser Sales Bills Detail Information: Exception:{ex.Message}.");
            }
        }

        // PUT: api/SalesBills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesBill(int id, Models.UpdateAmountRequest updateAmount)
        {
            if (_context.SalesBill == null)
            {
                return NotFound();
            }
            var salesBillDb = await _context.SalesBill.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
            if (id != salesBillDb?.Id)
            {
                return BadRequest();
            }
            _context.Entry(salesBillDb).State = EntityState.Modified;
            try
            {
                _logger.LogInformation($"Processing Showing the PutSalesBill {salesBillDb.SalesBillNumber} ");
                salesBillDb.Amount = updateAmount.Amount;
                salesBillDb.ModifiedBy = updateAmount.UsedBy;
                salesBillDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PutSalesBill: {salesBillDb.SalesBillNumber}, Exception: {ex}.");
                if (!SalesBillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, $"An error occurred while updating Sales Bills Detail Information: Exception:{ex.Message}.");
                }
            }
            return NoContent();
        }

        // POST: api/SalesBills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesBill>> PostSalesBill(Models.SalesBillRequest salesBill)
        {
          if (_context.SalesBill == null)
          {
              return Problem("Entity set 'TraderApiContext.SalesBill'  is null.");
          }
            SalesBill salesBillDb = new SalesBill();
            try
            {
                _logger.LogInformation($"Processing Showing the PostSalesBill {salesBillDb.SalesBillNumber} ");
                salesBillDb.IsAgentOrder = salesBill.IsAgentOrder;
                salesBillDb.BookingDate = salesBill.BookingDate;
                salesBillDb.SalesYear = salesBill.SalesYear;
                salesBillDb.SalesBillNumber = salesBill.SalesBillNumber;
                salesBillDb.BilitNumber = salesBill.BilitNumber;
                salesBillDb.TransporterId = salesBill.TransporterId;
                salesBillDb.TransporterName = salesBill.TransporterName;
                salesBillDb.AgentId = salesBill.AgentId;
                salesBillDb.AgentName = salesBill.AgentName;
                salesBillDb.PurchaserId = salesBill.PurchaserId;
                salesBillDb.Purchaser = salesBill.Purchaser;
                salesBillDb.Status = salesBill.Status;
                salesBillDb.Weight = salesBill.Weight;
                salesBillDb.CommissionPercentage = salesBill.CommissionPercentage;
                salesBillDb.TcsPercentage = salesBill.TcsPercentage;
                salesBillDb.PackingChargePerBag = salesBill.PackingChargePerBag;
                salesBillDb.Amount = salesBill.Amount;
                salesBillDb.Commission = salesBill.Commission;
                salesBillDb.PackingCharges = salesBill.PackingCharges;
                salesBillDb.Total = salesBill.Total;
                salesBillDb.Less = salesBill.Less;
                salesBillDb.GrossTotal = salesBill.GrossTotal;
                salesBillDb.CreatedBy = salesBill.UsedBy;
                salesBillDb.CreatedDate = DateTime.Now;
                foreach (var item in salesBill.Particulars)
                {
                    Particulars particulars = new Particulars();
                    particulars.Item = item.Item;
                    particulars.Rate = item.Rate;
                    particulars.Bags = item.Bags;
                    salesBillDb.Particulars.Add(particulars);
                }
                salesBillDb.CreatedBy = salesBill.UsedBy;
                _context.SalesBill.Add(salesBillDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PostSalesBill: {salesBillDb.SalesBillNumber}, Exception: {ex}.");
                return StatusCode(500, $"An error occurred while inserting Sales Bills Detail Information: Exception:{ex.Message}.");
            }
            return CreatedAtAction("GetSalesBill", new { id = salesBillDb.Id }, salesBillDb);
        }

        //// DELETE: api/SalesBills/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSalesBill(int id)
        //{
        //    if (_context.SalesBill == null)
        //    {
        //        return NotFound();
        //    }
        //    var salesBill = await _context.SalesBill.FindAsync(id);
        //    if (salesBill == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.SalesBill.Remove(salesBill);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool SalesBillExists(int id)
        {
            return (_context.SalesBill?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
