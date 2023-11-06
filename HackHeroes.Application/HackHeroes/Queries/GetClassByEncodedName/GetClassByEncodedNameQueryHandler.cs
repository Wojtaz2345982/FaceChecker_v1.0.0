using AutoMapper;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Queries.GetClassByEncodedName
{
    public class GetClassByEncodedNameQueryHandler : IRequestHandler<GetClassByEncodedNameQuery, ClassDto>
    {
        private readonly IMapper _mapper;
        private readonly IHackHeroesRepository _repository;

        public GetClassByEncodedNameQueryHandler(IMapper mapper, IHackHeroesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ClassDto> Handle(GetClassByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var @class = await _repository.GetClassbyEncodedName(request.EncodedName);

            var dto = _mapper.Map<ClassDto>(@class);

            return dto;
        }
    }
}
