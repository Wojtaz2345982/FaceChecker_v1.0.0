using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackHeroes.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int LessonId { get; set; }

        [JsonIgnore]
        public Lesson? Lesson { get; set; }
        public string? StudentKey { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = default!;
        public bool WasThere { get; set; } = false;
        public bool? WasLate { get; set; }
    }
}
