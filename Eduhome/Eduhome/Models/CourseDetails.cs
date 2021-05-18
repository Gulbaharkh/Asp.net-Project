using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Models
{
    public class CourseDetails
    {
        public int Id { get; set; }
        public string AboutCourse { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }
        public string Details { get; set; }
        public DateTime? Start { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string Level { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assesments { get; set; }
        public int CourseFee { get; set; }
        [ForeignKey("Caption")]
        public int CaptionId { get; set; }
        public Caption Caption { get; set; }
    }
}
