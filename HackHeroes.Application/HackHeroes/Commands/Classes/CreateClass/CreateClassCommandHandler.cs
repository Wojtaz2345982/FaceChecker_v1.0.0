using AutoMapper;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Classes.CreateClass
{
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand>
    {
        private readonly IMapper _mapper;
        private readonly IHackHeroesRepository _repository;
        private readonly IUserContext _userContext;

        public CreateClassCommandHandler(IMapper mapper, IHackHeroesRepository repository, IUserContext userContext)
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var @class = _mapper.Map<Domain.Entities.Class>(request);
            var dto = _mapper.Map<ClassDto>(@class);

            @class.GenerateUniqueEncodedName();

            @class.CreatedById = _userContext.GetCurrentUser()!.Id;
                    

            await _repository.Create(@class);


        }
    }
}
