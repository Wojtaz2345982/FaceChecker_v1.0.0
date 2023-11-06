using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Student.GetClassStudents
{
    public class GetClassStudentsQueryHandler : IRequestHandler<GetClassStudentsQuery, IEnumerable<Domain.Entities.Student>>
    {
        private readonly IStudentsRepository _studentsRepository;

        public GetClassStudentsQueryHandler(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }
        public async Task<IEnumerable<Domain.Entities.Student>> Handle(GetClassStudentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentsRepository.GetAllStudents(request.EncodedName);

            return result!;
        }
    }
}
