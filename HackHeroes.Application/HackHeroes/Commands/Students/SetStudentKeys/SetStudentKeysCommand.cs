using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.SetStudentKeys
{
    public class SetStudentKeysCommand : IRequest
    {
        public SetStudentKeysCommand(string encodedName)
        {
            EncodedName = encodedName;
        }
        public string EncodedName { get; set; } = default!;
    }
}
