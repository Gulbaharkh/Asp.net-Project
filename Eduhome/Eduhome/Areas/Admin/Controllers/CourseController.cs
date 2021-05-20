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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Caption> courses = _context.CourseCaptions.Include(x=>x.CourseDetails).ToList();
            return View(courses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Caption course)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.CourseCaptions.Any(c => c.Title.ToLower().Trim() == course.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu adda kurs var");
                return View();
            }
            await _context.AddRangeAsync(course,course.CourseDetails);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
