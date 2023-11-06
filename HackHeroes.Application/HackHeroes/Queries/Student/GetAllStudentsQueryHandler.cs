using AutoMapper;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using MediatR;


namespace HackHeroes.Application.HackHeroes.Queries.Student
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto?>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentsRepository _repository;
        private readonly IHackHeroesRepository _hackHeroesRepository;
     

        public GetAllStudentsQueryHandler(IMapper mapper, IStudentsRepository repository, IHackHeroesRepository hackHeroesRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _hackHeroesRepository = hackHeroesRepository;
     
        }


        public async Task<IEnumerable<StudentDto?>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _repository.GetAllStudents(request.EncodedName);

            var @class = await _hackHeroesRepository.GetClassbyEncodedName(request.EncodedName);


            

            await _repository.SetStudentsNumber(@class.EncodedName!);
           
            var dtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            var sortedDtos = dtos.OrderBy(student => student.Number).ToList();

            foreach (var student in sortedDtos)
            {
                student.StudentKey = student.Number + "-" + @class.Name.ToLower();
            }


            return sortedDtos;
        }
    }
}
