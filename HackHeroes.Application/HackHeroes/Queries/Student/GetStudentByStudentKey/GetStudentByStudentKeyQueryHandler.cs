using AutoMapper;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Student.GetStudentByStudentKey
{
    public class GetStudentByStudentKeyQueryHandler : IRequestHandler<GetStudentByStudentKeyQuery, StudentDto>
    {
        private readonly IStudentsRepository _repository;
        private readonly IMapper _mapper;

        public GetStudentByStudentKeyQueryHandler(IStudentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<StudentDto> Handle(GetStudentByStudentKeyQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByStudentKey(request.StudentKey!);

            var dto = _mapper.Map<StudentDto>(result);

            return dto;
        }
    }
}
