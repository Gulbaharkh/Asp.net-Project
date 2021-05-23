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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _db.Teacher.ToListAsync();

            return View(teachers);
        }
        public IActionResult Details(int id)
        {

            TeacherDetails detail = _db.TeacherDetails.Include(det => det.Teacher)
                .FirstOrDefault(det => det.TeacherId == id);


            return View(detail);
        }
        public async Task<IActionResult> Search(string search)
        {
            List<Teacher> teachers = await _db.Teacher.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToListAsync();

            return View(teachers);
        }
    }
}
