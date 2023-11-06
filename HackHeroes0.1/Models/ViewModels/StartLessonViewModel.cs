using HackHeroes.Application.HackHeroes;

namespace HackHeroes0._1.Models.ViewModels
{
    public class StartLessonViewModel
    {
        public LessonDto Lesson { get; set; } = default!;
        public IEnumerable<AttendanceDto> Attendance { get; set; } = default!;
    }
}
