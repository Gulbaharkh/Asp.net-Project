using Eduhome.DAL;
using Eduhome.Extensions;
using Eduhome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            if (course.Photo == null) {
                ModelState.AddModelError("Photo", "Please take Photo");
                return View();
            }
            string folder = Path.Combine("front", "img", "course");
            string filename = await course.Photo.SaveFileAsync(_env.WebRootPath, folder);
            bool isExist = _context.CourseCaptions.Any(c => c.Title.ToLower().Trim() == course.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "This Course already exists!");
                return View();
            }
            course.Image = filename;
            await _context.AddRangeAsync(course,course.CourseDetails);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Caption caption = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Id == id);
            if (caption == null) return NotFound();
            return View(caption);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Caption caption = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Id == id);
            if (caption == null) return NotFound();
            return View(caption);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Caption caption = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Id == id);
            if (caption == null) return NotFound();
            _context.CourseCaptions.Remove(caption);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Caption caption = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Id == id);
            if (caption == null) return NotFound();
            return View(caption);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Caption caption)
        {
            if (id == null) return NotFound();
            if (caption == null) return NotFound();
            Caption captionView = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(captionView);
            }
            Caption captionDb = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == caption.Title.ToLower().Trim());
            if (captionDb != null && captionDb.Id != id)
            {
                ModelState.AddModelError("Course Name", "This category already exist.");
                return View(captionView);
            }
            captionView.Title = caption.Title;
            captionView.Description = caption.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
