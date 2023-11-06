using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.GetClassById
{
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, Class>
    {
        private readonly IHackHeroesRepository _hackHeroesRepository;

        public GetClassByIdQueryHandler(IHackHeroesRepository hackHeroesRepository)
        {
            _hackHeroesRepository = hackHeroesRepository;
        }
        public async Task<Class> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var @class = await _hackHeroesRepository.GetClassbyId(request.ClassId);

            return @class;  
        }
    }
}
