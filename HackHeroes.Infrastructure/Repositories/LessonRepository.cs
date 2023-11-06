using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using HackHeroes.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAttendanceRepository _attendanceRepository;

        public LessonRepository(ApplicationDbContext dbContext, IAttendanceRepository attendanceRepository)
        {
            _dbContext = dbContext;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Lesson?>> GetAllLessons()
        {
            var result = await _dbContext.Lessons.ToListAsync();

            return result;
        }

        public async Task Create(HackHeroes.Domain.Entities.Lesson lesson)
        {
            _dbContext.Lessons.Add(lesson);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(HackHeroes.Domain.Entities.Lesson lesson)
        {
            var attendances = await _attendanceRepository.GetLessonAttendance(lesson);

            foreach (var attendance in attendances)
            {
               await _attendanceRepository.Delete(attendance);
            }

            _dbContext.Lessons.Remove(lesson);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Lesson?> GetLessonByEncodedLesson(string encodedLesson)
        {
            var result = await _dbContext.Lessons.FirstOrDefaultAsync(les => les.EncodedLesson == encodedLesson);

            return result;
        }

    }
}
