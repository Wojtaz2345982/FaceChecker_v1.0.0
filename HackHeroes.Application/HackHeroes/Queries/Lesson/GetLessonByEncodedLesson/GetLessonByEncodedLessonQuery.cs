using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonByEncodedLesson
{
    public class GetLessonByEncodedLessonQuery : IRequest<LessonDto>
    {
        public GetLessonByEncodedLessonQuery(string encodedLesson)
        {
            EncodedLesson = encodedLesson;
        }
        public string EncodedLesson { get; set; }
    }
}
