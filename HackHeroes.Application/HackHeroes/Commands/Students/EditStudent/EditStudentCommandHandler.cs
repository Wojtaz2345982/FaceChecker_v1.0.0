using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.EditStudent
{
    public class EditStudentCommandHandler : IRequestHandler<EditStudentCommand>
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IHackHeroesRepository _hackHeroesRepository;

        public EditStudentCommandHandler(IStudentsRepository studentsRepository, IHackHeroesRepository hackHeroesRepository)
        {
            _studentsRepository = studentsRepository;
            _hackHeroesRepository = hackHeroesRepository;
        }
        public async Task Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentsRepository.GetByStudentKey(request.StudentKey!);
                      
            student!.FirstName = request.FirstName;
            student.LastName = request.LastName;

          


            await _studentsRepository.Commit();


        }
    }
}
