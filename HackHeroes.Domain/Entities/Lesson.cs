using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HackHeroes.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Topic { get; set; }
        public int ClassId { get; set; }
        public bool WasStarted { get; set; } = false;
        public Class? Class { get; set; }
        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }
        public string? EncodedLesson { get; set; }
        public ICollection<Attendance>? AttendaceAtTheLesson { get; set; }
        public void EncodeLesson() => EncodedLesson = Date.Second.ToString() + "-"+ Date.Minute + "-" + Date.Microsecond + "-" + Topic;
    }
}
