using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.SetStudentKeys
{
    public class SetStudentKeysCommandHandler : IRequestHandler<SetStudentKeysCommand>
    {
        private readonly IStudentsRepository _studentsRepository;

        public SetStudentKeysCommandHandler(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }
        public async Task Handle(SetStudentKeysCommand request, CancellationToken cancellationToken)
        {
            await _studentsRepository.SetStudentsNumber(request.EncodedName);
        }
    }
}
