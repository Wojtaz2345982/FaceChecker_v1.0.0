using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonDbObjectByEncodedLesson
{
    public class GetLessonDbObjectByEncodedLessonQueryHandler : IRequestHandler<GetLessonDbObjectByEncodedLessonQuery, Domain.Entities.Lesson>
    {
        private readonly ILessonRepository _lessonRepository;

        public GetLessonDbObjectByEncodedLessonQueryHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Domain.Entities.Lesson> Handle(GetLessonDbObjectByEncodedLessonQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetLessonByEncodedLesson(request.EncodedLesson);

            if(lesson == null )
            {
                return null!;
            }

            return lesson;
        }
    }
}
