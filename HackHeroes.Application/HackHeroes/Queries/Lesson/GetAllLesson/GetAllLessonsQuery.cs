using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.Lesson.GetAllLesson
{
    public class GetAllLessonsQuery : IRequest<IEnumerable<LessonDto>>
    {
    }
}
