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
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        public CourseController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Caption> courses =await _db.CourseCaptions.ToListAsync();
            
            return View(courses);
        }
        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            CourseDetails detail = _db.CourseDetails.Include(det=>det.Caption)
                .FirstOrDefault(det=>det.CaptionId==id);
            if (detail == null) return RedirectToAction("Index");

            return View(detail);
        }
        public async Task<IActionResult> Search(string search)
        {
            List<Caption> courses = await _db.CourseCaptions.Where(c=>c.Title.ToLower().Trim().Contains(search.ToLower().Trim())).ToListAsync();
            
            return View(courses);
        }
    }
}
