
using HackHeroes.Application.ApplicationUser;
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
    //Repozytorium klas
    public class HackHeroesRepository : IHackHeroesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HackHeroesRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task Create(HackHeroes.Domain.Entities.Class @class)
        {
            _dbContext.Classes.Add(@class);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Class @class)
        {
            _dbContext.Classes.Remove(@class);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Class>> GetAllClasses()
        {
             var result = await _dbContext.Classes.ToListAsync();

            return result;
        }

       

        public async Task<Domain.Entities.Class> GetClassbyEncodedName(string encodedName)
        {
            var result = await _dbContext.Classes.FirstAsync(c => c.EncodedName!.ToLower() == encodedName.ToLower());

            return result;
        }

        public async Task<Domain.Entities.Class> GetClassbyId(int id)
        {
            var result = await _dbContext.Classes.FirstAsync(c => c.Id == id);

            return result;
        }

        public async Task<Domain.Entities.Class?> GetClassByName(string name,string currentUserId)
        {
            return await _dbContext.Classes.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.CreatedById == currentUserId);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

      
    }
}
