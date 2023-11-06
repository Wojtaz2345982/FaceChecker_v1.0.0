using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes
{
    public class AttendanceDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StudentKey { get; set; }
        public int StudentId { get; set; }
        public bool WasThere { get; set; }
        public bool? WasLate { get; set; }
    }
}
