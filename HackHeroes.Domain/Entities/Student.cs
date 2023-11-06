using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HackHeroes.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int ClassId { get; set; }     
        public Class Class { get; set; } = default!;
        public int Number { get; set; }
        public string? StudentKey { get; set; }
        public byte[]? ImageUrl { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public void SetStudentKey() => StudentKey = Number + "-" + Class.Name.ToLower();
    }
}
