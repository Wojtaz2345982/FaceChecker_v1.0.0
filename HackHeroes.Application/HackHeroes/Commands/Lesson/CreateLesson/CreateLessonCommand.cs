using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Lesson.CreateLesson
{
    public class CreateLessonCommand : LessonDto, IRequest
    {
        public string? ClassEncodedName { get; set; }
    }
}
