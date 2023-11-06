using HackHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Domain.Services
{
    public interface IStudentsRepository
    {
        Task Create(Domain.Entities.Student student);
        Task Commit();
        Task<IEnumerable<Domain.Entities.Student?>> GetAllStudents(string encodedName);
        Task<IEnumerable<Domain.Entities.Student?>> GetAllStudents();
        Task<Domain.Entities.Student?> GetByStudentKey(string studentKey);
        Task DeleteStudentsFromClass(string encodedName);
        Task SetStudentsNumber(string encodedName);     
        Task Delete(Student student);
    }
}
