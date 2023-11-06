using HackHeroes.Application.HackHeroes.Lesson.CreateLesson;
using HackHeroes.Application.HackHeroes;

namespace HackHeroes0._1.Models.ViewModels
{
    public class CreateLessonViewModel
    {
        public CreateLessonCommand LessonCommand { get; set; } = default!;
        public IEnumerable<ClassDto> AvailableClasses { get; set; } = default!;
    }
}
