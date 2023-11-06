using HackHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Domain.Services
{
    public interface IAttendanceRepository
    {
        Task Create(HackHeroes.Domain.Entities.Attendance attendance);
        Task ChangeAttendance(int studentId, int lessonId, bool isThere);
        Task Delete(HackHeroes.Domain.Entities.Attendance attendance);
        Task<IEnumerable<Attendance>> GetLessonAttendance(HackHeroes.Domain.Entities.Lesson lesson);
        Task ChangeAttendanceIsLate(int studentId, int lessonId, bool isLate);
        Task Commit();
    }
}
