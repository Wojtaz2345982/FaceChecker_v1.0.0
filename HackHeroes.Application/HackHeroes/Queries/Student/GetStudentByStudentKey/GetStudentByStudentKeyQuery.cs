using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Student.GetStudentByStudentKey
{
    public class GetStudentByStudentKeyQuery : StudentDto,IRequest<StudentDto>
    {
        
    }
}
