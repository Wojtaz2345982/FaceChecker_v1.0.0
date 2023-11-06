using HackHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes
{
    public class ClassDto
    {
        public string Name { get; set; } = default!;
        public string? EncodedName { get;  set; } = default!;
        public List<Student> Students { get; set; } = new();
        public bool IsEditable { get; set; }
        
    }
}
