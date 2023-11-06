using AutoMapper;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Lesson.GetAllLesson
{
    public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQuery, IEnumerable<LessonDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILessonRepository _repository;
        private readonly IHackHeroesRepository _classRepository;

        public GetAllLessonsQueryHandler(IMapper mapper, ILessonRepository repository, IHackHeroesRepository classRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<LessonDto>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            var lessons = await _repository.GetAllLessons();

            foreach (var les in lessons)
            {
                les!.Class = await _classRepository.GetClassbyId(les.ClassId);
            }

            var dtos = _mapper.Map<IEnumerable<LessonDto>>(lessons);

            var ownersLessons = dtos.Where(dto => dto.IsEditable == true);

            var rewersedOwnersDtos = ownersLessons.Reverse().ToList();

            return rewersedOwnersDtos;
        }
    }
}
