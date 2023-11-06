using HackHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes
{
    public class LessonDto
    {
        public DateTime Date { get; set; }
        public string? Topic { get; set; }
        public Class? Class { get; set; }
        public bool IsEditable { get; set; }
        public bool WasStarted { get; set; }
        public string? EncodedLesson { get; set; }
        public ICollection<AttendanceDto>? AttendaceAtTheLesson { get; set; }
    }
}
