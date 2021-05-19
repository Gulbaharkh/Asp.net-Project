using Eduhome.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Caption> CourseCaptions { get; set; }
        public DbSet<CourseDetails> CourseDetails { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<TeacherDetails> TeacherDetails { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
       
    }
}
