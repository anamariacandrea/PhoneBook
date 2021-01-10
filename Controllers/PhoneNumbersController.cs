using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookModel.Data;
using PhoneBookModel.Models;

namespace PhoneBookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly PhoneBookContext _context;

        public PhoneNumbersController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: api/PhoneNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> GetPhoneNumbers()
        {
            return await _context.PhoneNumbers.ToListAsync();
        }

        // GET: api/PhoneNumbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneNumber>> GetPhoneNumber(int id)
        {
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNumber == null)
            {
                return NotFound();
            }

            return phoneNumber;
        }

        // PUT: api/PhoneNumbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoneNumber(int id, PhoneNumber phoneNumber)
        {
            if (id != phoneNumber.PhoneNumberID)
            {
                return BadRequest();
            }

            _context.Entry(phoneNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(id))
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

        // POST: api/PhoneNumbers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhoneNumber>> PostPhoneNumber(PhoneNumber phoneNumber)
        {
            _context.PhoneNumbers.Add(phoneNumber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneNumber", new { id = phoneNumber.PhoneNumberID }, phoneNumber);
        }

        // DELETE: api/PhoneNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoneNumber(int id)
        {
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            _context.PhoneNumbers.Remove(phoneNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneNumberExists(int id)
        {
            return _context.PhoneNumbers.Any(e => e.PhoneNumberID == id);
        }
    }
}
