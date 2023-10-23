using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TraderApi.Data;
using TraderApi.Data.Entities;

namespace TraderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDetailsController : ControllerBase
    {
        private readonly TraderApiContext _context;
        private readonly ILogger<AccountDetailsController> _logger;
        public AccountDetailsController(TraderApiContext context, ILogger<AccountDetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/AccountDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDetail>>> GetAccountDetail()
        {
            try
            {
                _logger.LogInformation($" Getting Account Details Information for AccountDetailsController ");
                if (_context.AccountDetail == null)
                {
                    return NotFound();
                }
                return await _context.AccountDetail.Where(a => a.IsDeleted == false).ToListAsync();
            }
            catch(Exception ex)
            {
               _logger.LogCritical($"Error: Unable to Getting Account Details Information for AccountDetailsController: Exception: {ex}.");
                return null;
            }
        }

        // GET: api/AccountDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDetail>> GetAccountDetail(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Account Details Information for AccountDetailsController: {id} ");
                if (_context.AccountDetail == null)
                {
                    return NotFound();
                }
                var accountDetail = await _context.AccountDetail.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);

                if (accountDetail == null)
                {
                    return NotFound();
                }

                return accountDetail;
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Error: Unable to Getting Account Details Information for AccountDetailsController: {id}, Exception: {ex}.");
                return null;
            }
        }

        // PUT: api/AccountDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountDetail(int id, Models.AccountDetailRequest accountDetail)
        {
            var accountDetailDb = await _context.AccountDetail.FindAsync(id);
            if (id != accountDetailDb.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountDetailDb).State = EntityState.Modified;

            try
            {
                _logger.LogInformation($"Processing Showing the PutAccountDetail  {accountDetailDb.Name}.");
                accountDetailDb.Name = accountDetail.Name;
                accountDetailDb.BankName = accountDetail.BankName;
                accountDetailDb.AccountNumber = accountDetail.AccountNumber;
                accountDetailDb.IfscCode = accountDetail.IfscCode;
                accountDetailDb.ModifiedBy = accountDetail.UsedBy;
                accountDetailDb.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PutAccountDetail: {accountDetailDb.Name}, Exception: {ex}");
                if (!AccountDetailExists(id))
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

        // POST: api/AccountDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDetail>> PostAccountDetail(Models.AccountDetailRequest accountDetail)
        {
          if (_context.AccountDetail == null)
          {
              return Problem("Entity set 'TraderApiContext.AccountDetail'  is null.");
          }
            AccountDetail accountDetailDb = new AccountDetail();
            try
            {
               _logger.LogInformation($"Processing update for PostAccountDetail  {accountDetailDb.Name}.");
                accountDetailDb.Name = accountDetail.Name;
                accountDetailDb.BankName = accountDetail.BankName;
                accountDetailDb.AccountNumber = accountDetail.AccountNumber;
                accountDetailDb.IfscCode = accountDetail.IfscCode;
                accountDetailDb.CreatedBy = accountDetail.UsedBy;
                accountDetailDb.CreatedDate = DateTime.Now;
                _context.AccountDetail.Add(accountDetailDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogCritical($"Error: Exception processing for PostAccountDetail: {accountDetailDb.Name}, Exception: {ex}.");
                if (!AccountDetailExists(accountDetailDb.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("Error: GetAccountDetail", new { id = accountDetailDb.Id }, accountDetailDb);
        }

        // DELETE: api/AccountDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDetail(int id)
        {
            try
            {
                _logger.LogInformation($" Getting Delete Account Details Information for AccountDetailsController: {id}.");
                if (_context.AccountDetail == null)
                {
                    return NotFound();
                }
                var accountDetail = await _context.AccountDetail.FindAsync(id);
                if (accountDetail == null)
                {
                    return NotFound();
                }
                accountDetail.IsDeleted = true;
                accountDetail.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
               _logger.LogCritical($"Error: Unable Delete Account Details Information for AccountDetailsController: {id}, Exception: {ex}.");
                return null;
            }
        }

        private bool AccountDetailExists(int id)
        {
            return (_context.AccountDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
