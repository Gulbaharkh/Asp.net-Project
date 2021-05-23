using Eduhome.DAL;
using Eduhome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Event> events = _context.Event.Include(x => x.EventDetails).ToList();
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event events)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Event.Any(c => c.Title.ToLower().Trim() == events.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "This Event already exists!");
                return View();
            }
            await _context.AddRangeAsync(events, events.EventDetails);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Event events = await _context.Event.FirstOrDefaultAsync(c => c.Id == id);
            if (events == null) return NotFound();
            return View(events);
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Event events = await _context.Event.FirstOrDefaultAsync(c => c.Id == id);
            if (events == null) return NotFound();
            return View(events);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Event events = await _context.Event.FirstOrDefaultAsync(c => c.Id == id);
            if (events == null) return NotFound();
            _context.Event.Remove(events);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Event events = await _context.Event.FirstOrDefaultAsync(c => c.Id == id);
            if (events == null) return NotFound();
            return View(events);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Event events)
        {
            if (id == null) return NotFound();
            if (events == null) return NotFound();
            Event eventView = await _context.Event.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(eventView);
            }
            Event eventsDb = await _context.Event.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == events.Title.ToLower().Trim());
            if (eventsDb != null && eventsDb.Id != id)
            {
                ModelState.AddModelError("Course Name", "This category already exist.");
                return View(eventView);
            }
            eventView.Title = events.Title;
            eventView.Photo = events.Photo;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
