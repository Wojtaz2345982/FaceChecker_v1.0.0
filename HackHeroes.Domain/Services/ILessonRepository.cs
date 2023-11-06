using HackHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Domain.Services
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Domain.Entities.Lesson?>> GetAllLessons();
        Task Create(HackHeroes.Domain.Entities.Lesson lesson);
        Task<Lesson?> GetLessonByEncodedLesson(string encodedLesson);
        Task Delete(HackHeroes.Domain.Entities.Lesson lesson);
    }
}
