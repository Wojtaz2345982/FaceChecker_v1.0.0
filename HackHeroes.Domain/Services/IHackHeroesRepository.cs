using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Domain.Services
{
    public interface IHackHeroesRepository
    {
        Task Create(Domain.Entities.Class @class);
        Task<Domain.Entities.Class?> GetClassByName(string name, string currentUserId);
        Task<IEnumerable<Entities.Class>> GetAllClasses();
        Task<Domain.Entities.Class> GetClassbyEncodedName(string encodedName);
        Task Delete(Domain.Entities.Class @class);
        Task<Domain.Entities.Class> GetClassbyId(int id);
        Task Commit();
    }
}
