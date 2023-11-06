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
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(HackHeroes.Domain.Entities.Attendance attendance)
        {
            _dbContext.Attendance.Add(attendance);

            await _dbContext.SaveChangesAsync();
        }


        public async Task Delete(HackHeroes.Domain.Entities.Attendance attendance)
        {
            _dbContext.Attendance.Remove(attendance);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Attendance>> GetLessonAttendance(HackHeroes.Domain.Entities.Lesson lesson)
        {
            var result = await _dbContext.Attendance.Where(a => a.LessonId == lesson.Id).ToListAsync();

            return result;
        }

        public async Task ChangeAttendance(int studentId,int lessonId,bool isThere)
        {
            var attendance = await _dbContext.Attendance.FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId);

            if(attendance == null)
            {
                return;
            }

            if(attendance.WasThere == true)
            {
                return;
            }

            attendance!.WasThere = isThere;

            await _dbContext.SaveChangesAsync();
        }


        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeAttendanceIsLate(int studentId, int lessonId, bool isLate)
        {
            var attendance = await _dbContext.Attendance.FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId);

            if (attendance == null)
            {
                return;
            }

            if (attendance.WasLate == true)
            {
                return;
            }

            attendance!.WasLate = isLate;

            await _dbContext.SaveChangesAsync();
        }
    }
}
