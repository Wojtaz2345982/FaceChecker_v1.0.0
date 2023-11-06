using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IStudentsRepository _studentsRepository;

        public DeleteStudentCommandHandler(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }
        public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
             var studentToDelete = await _studentsRepository.GetByStudentKey(request.StudentKey!);

            await _studentsRepository.Delete(studentToDelete!);
        }
    }
}
