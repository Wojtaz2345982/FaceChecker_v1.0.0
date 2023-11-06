using AutoMapper;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand>
    {
        private readonly IHackHeroesRepository _repository;
        private readonly IStudentsRepository _studentsRepository;

        public AddStudentCommandHandler(IHackHeroesRepository classRepository, IStudentsRepository studentsRepository)
        {
            _repository = classRepository;
            _studentsRepository = studentsRepository;
        }
        public async Task Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var @class = await _repository.GetClassbyEncodedName(request.ClassEncodedName);

            var student = new Domain.Entities.Student()
            {
                Class = @class,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Number = request.Number,
                ClassId = @class.Id,                             
            };



            await _studentsRepository.Create(student);

            await _studentsRepository.SetStudentsNumber(@class.EncodedName!);
             
            student.SetStudentKey();
        }
    }
}
