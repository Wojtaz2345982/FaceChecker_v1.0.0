using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students.EditStudentImage
{
    public class EditStudentImageCommandHandler : IRequestHandler<EditStudentImageCommand>
    {
        private readonly IStudentsRepository _studentsRepository;
      

        public EditStudentImageCommandHandler(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
           
        }
        public async Task Handle(EditStudentImageCommand request, CancellationToken cancellationToken)
        {
            

              var student = await _studentsRepository.GetByStudentKey(request.StudentKey!);

            if(student == null)
            {
                return;
            }


            student.ImageUrl = request.Image;
             
            await _studentsRepository.Commit();
            

        }
    }
}
