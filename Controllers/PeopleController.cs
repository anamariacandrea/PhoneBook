using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PhoneBookContext _context;

        public PeopleController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(string Cautare)
        {
            ViewData["CurrentFilter"] = Cautare;
            var people = from p in _context.People select p;
            if(!String.IsNullOrEmpty(Cautare))
            {
                people = people.Where(s => s.LastName.Contains(Cautare));
            }
            return View(await people.AsNoTracking().ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(s=>s.Details)
                .ThenInclude(e=>e.PhoneNumbers)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName")] Person person)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(person);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }catch(DbUpdateException /*ex*/)
            {
                ModelState.AddModelError("", "Nu se poate salva" + "Incercati din nou");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int ?id)
        {
            if (id != null)
            {
                return NotFound();
            }
            var peopleToUpdate = await _context.People.FirstOrDefaultAsync(s => s.PersonID == id);
            if(await TryUpdateModelAsync<Person>(
                peopleToUpdate,
                "",
                s=>s.LastName,s=>s.FirstName,s=>s.PhoneNumbers,s=>s.Details   ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateException /*ex*/)
                {
                    ModelState.AddModelError("", "Nu se poate salva" + " mai verificati inca o data");

                }
            }
                
            
            return View(peopleToUpdate);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["Error Message"]="Stergere nereusita. Incercati din nou";
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if(person==null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /*ex*/)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
          
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonID == id);
        }
    }
}
