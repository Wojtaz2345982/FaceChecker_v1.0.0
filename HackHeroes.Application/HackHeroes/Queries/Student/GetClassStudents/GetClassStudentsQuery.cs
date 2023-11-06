using HackHeroes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Student.GetClassStudents
{
    public class GetClassStudentsQuery : IRequest<IEnumerable<Domain.Entities.Student>>
    {
        public GetClassStudentsQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
        public string EncodedName { get; set; }
    }
}
