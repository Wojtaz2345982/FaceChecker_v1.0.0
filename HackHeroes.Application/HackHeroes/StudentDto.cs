using HackHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes
{
    public class StudentDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Class Class { get; set; } = default!;
        public int Number {  get; set; }
        public string? ImageUrl { get; set; }
        public string? StudentKey { get; set; }
    }
}
