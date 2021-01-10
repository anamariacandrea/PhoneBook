using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using PhoneBookModel.Data;
using PhoneBookModel.Models;

namespace PhoneBook.Controllers
{
    public class PhoneNumbersController : Controller
    {
        private readonly PhoneBookContext _context;
        private string _baseURL = "http://localhost:56890/api/PhoneNumbers";

        public PhoneNumbersController(PhoneBookContext context)
        {
            _context = context;
        }

        // GET: PhoneNumbers
        public async Task<IActionResult> Index()
        {
            var client=new HttpClient();
            var response = await client.GetAsync(_baseURL);
            if (response.IsSuccessStatusCode)
            {
                var phoneNumber = JsonConvert.DeserializeObject<List<PhoneNumber>>(await response.Content.
                ReadAsStringAsync());
                return View(phoneNumber);
            }
            return NotFound();
        }

        // GET: PhoneNumbers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{ _baseURL}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var phoneNumber = JsonConvert.DeserializeObject<PhoneNumber>(
                await response.Content.ReadAsStringAsync());
                return View(phoneNumber);
            }
            return NotFound();
        }
       /* var phoneNumber = await _context.PhoneNumbers
                .FirstOrDefaultAsync(m => m.PhoneNumberID == id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return View(phoneNumber);
        }*/

        // GET: PhoneNumbers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumberID,PersonID,MailID,PhoneNumbers")] PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid) return View(phoneNumber);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(phoneNumber);
                var response = await client.PostAsync(_baseURL,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
{
ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
}
return View(phoneNumber);
}

        // GET: PhoneNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            return View(phoneNumber);
        }

        // POST: PhoneNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PhoneNumberID,PersonID,MailID,PhoneNumbers")] PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid) 
                return View(phoneNumber);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(phoneNumber);
            var response = await client.PutAsync($"{_baseURL}/{phoneNumber.PhoneNumberID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(phoneNumber);
        }

        // GET: PhoneNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseURL}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var phoneNumber = JsonConvert.DeserializeObject<PhoneNumber>(await response.Content.ReadAsStringAsync());
                return View(phoneNumber);
            }
            return new NotFoundResult();
        }

        // POST: PhoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("PhoneNumberID")] PhoneNumber phoneNumber)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete, $"{_baseURL}/{phoneNumber.PhoneNumberID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(phoneNumber), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record: {ex.Message}");
            }
            return View(phoneNumber);
        }

        /*private bool PhoneNumberExists(int id)
        {
            return _context.PhoneNumbers.Any(e => e.PhoneNumberID == id);
        }*/
    }
}
