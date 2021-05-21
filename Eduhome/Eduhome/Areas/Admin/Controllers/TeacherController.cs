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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Teacher> teachers = _context.Teacher.Include(x => x.TeacherDetails).ToList();
            return View(teachers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Teacher.Any(c => c.Name.ToLower().Trim() == teacher.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "The person already exists!");
                return View();
            }
            await _context.AddRangeAsync(teacher, teacher.TeacherDetails);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return NotFound();
            Teacher teacher = await _context.Teacher.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _context.Teacher.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _context.Teacher.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {

            if (id == null) return NotFound();
            Teacher teacher = await _context.Teacher.FirstOrDefaultAsync(c => c.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teacher teacher)
        {
            if (id == null) return NotFound();
            if (teacher == null) return NotFound();
            Teacher teacherView = await _context.Teacher.FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return View(teacherView);
            }
            Caption captionDb = await _context.CourseCaptions.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == teacher.Name.ToLower().Trim());
            if (captionDb != null && captionDb.Id != id)
            {
                ModelState.AddModelError("Course Name", "This Course already exists!");
                return View(teacherView);
            }
            teacherView.Name = teacher.Name;
            teacherView.Position = teacher.Position;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
