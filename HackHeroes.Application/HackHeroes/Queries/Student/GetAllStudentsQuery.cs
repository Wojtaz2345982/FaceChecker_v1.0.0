using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Student
{
    public class GetAllStudentsQuery : IRequest<IEnumerable<StudentDto>>
    {
        public GetAllStudentsQuery(string encodedName)
        {
            EncodedName = encodedName;
        }

        public string EncodedName { get; set; }

    }
}
