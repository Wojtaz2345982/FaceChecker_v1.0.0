using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonDbObjectByEncodedLesson
{
    public class GetLessonDbObjectByEncodedLessonQuery : IRequest<Domain.Entities.Lesson>
    {
        public GetLessonDbObjectByEncodedLessonQuery(string encodedLesson)
        {
            EncodedLesson = encodedLesson;
        }
        public string EncodedLesson { get; set; }
    }
}
