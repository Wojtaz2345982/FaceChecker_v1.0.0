using AutoMapper;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonByEncodedLesson
{
    public class GetLessonByEncodedLessonQueryHandler : IRequestHandler<GetLessonByEncodedLessonQuery, LessonDto>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public GetLessonByEncodedLessonQueryHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<LessonDto> Handle(GetLessonByEncodedLessonQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetLessonByEncodedLesson(request.EncodedLesson);

            var dto = _mapper.Map<LessonDto>(lesson);

            return dto;

        }
    }
}
