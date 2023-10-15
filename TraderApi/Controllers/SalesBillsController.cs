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
    public class SalesBillsController : ControllerBase
    {
        private readonly TraderApiContext _context;

        public SalesBillsController(TraderApiContext context)
        {
            _context = context;
        }

        // GET: api/SalesBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesBill>>> GetSalesBill()
        {
          if (_context.SalesBill == null)
          {
              return NotFound();
          }
            return await _context.SalesBill.ToListAsync();
        }

        // GET: api/SalesBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesBill>> GetSalesBill(int id)
        {
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

        //// GET: api/SalesBills/5
        //[HttpGet("{purchaserId}")]
        //public async Task<ActionResult<SalesBill>> GePurchaserSalesBill(int purchaserId)
        //{
        //    if (_context.SalesBill == null)
        //    {
        //        return NotFound();
        //    }
        //    var salesBill = await _context.SalesBill.Where(a=>a.PurchaserId==purchaserId).FirstOrDefault();

        //    if (salesBill == null)
        //    {
        //        return NotFound();
        //    }

        //    return salesBill;
        //}


        // PUT: api/SalesBills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesBill(int id, Models.UpdateAmountRequest updateAmount)
        {
            var salesBillDb = await _context.SalesBill.FindAsync(id);
            if (id != salesBillDb?.Id)
            {
                return BadRequest();
            }

            _context.Entry(salesBillDb).State = EntityState.Modified;

            try
            {
                salesBillDb.Amount = updateAmount.Amount;
                salesBillDb.ModifiedBy = updateAmount.UsedBy;
                salesBillDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesBillExists(id))
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
                salesBillDb.IsAgentOrder = salesBill.IsAgentOrder;
                salesBillDb.BookingDate= salesBill.BookingDate;
                salesBillDb.SalesYear=salesBill.SalesYear;
                salesBillDb.SalesBillNumber=salesBill.SalesBillNumber;
                salesBillDb.BilitNumber=salesBill.BilitNumber;
                salesBillDb.TransporterName=salesBill.TransporterName;
                salesBillDb.AgentName=salesBill.AgentName;
                salesBillDb.Purchaser=salesBill.Purchaser;
                salesBillDb.Status = salesBill.Status;
                salesBillDb.Weight=salesBill.Weight;
                salesBillDb.CommissionPercentage=salesBill.CommissionPercentage;
                salesBillDb.TcsPercentage=salesBill.TcsPercentage;
                salesBillDb.PackingChargePerBag=salesBill.PackingChargePerBag;
                salesBillDb.Amount=salesBill.Amount;
                salesBillDb.Commission=salesBill.Commission;
                salesBillDb.PackingCharges=salesBill.PackingCharges;
                salesBillDb.Total=salesBill.Total;
                salesBillDb.Less=salesBill.Less;
                salesBillDb.GrossTotal=salesBill.GrossTotal;
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

            catch (DbUpdateConcurrencyException)
            {
                if (!SalesBillExists(salesBillDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
