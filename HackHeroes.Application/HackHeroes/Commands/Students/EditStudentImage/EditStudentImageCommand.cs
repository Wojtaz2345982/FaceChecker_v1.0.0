using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.EditStudentImage
{
    public class EditStudentImageCommand : StudentDto, IRequest
    {
        public byte[]?  Image { get; set; }
    }
}
