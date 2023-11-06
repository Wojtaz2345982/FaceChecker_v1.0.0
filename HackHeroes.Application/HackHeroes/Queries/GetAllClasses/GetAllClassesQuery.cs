using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.GetAllClasses
{
    public class GetAllClassesQuery : IRequest<IEnumerable<ClassDto>>
    {
    }
}
