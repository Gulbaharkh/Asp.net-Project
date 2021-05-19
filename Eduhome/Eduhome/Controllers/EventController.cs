using Eduhome.DAL;
using Eduhome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        public EventController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _db.Event.ToListAsync();

            return View(events);
        }
        public IActionResult Details(int id)
        {

            EventDetails detail = _db.EventDetails.Include(det => det.Event).Include(x=>x.EventSpeakers).ThenInclude(x=>x.Speaker)
                .FirstOrDefault(det => det.EventId == id);


            return View(detail);
        }
    }
}
