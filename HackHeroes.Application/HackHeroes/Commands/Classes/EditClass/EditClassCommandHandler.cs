using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Classes.EditClass
{
    public class EditClassCommandHandler : IRequestHandler<EditClassCommand>
    {
        private readonly IHackHeroesRepository _repository;

        public EditClassCommandHandler(IHackHeroesRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(EditClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _repository.GetClassbyEncodedName(@request.EncodedName!);

            @class.Name = request.Name;

            @class.GenerateUniqueEncodedName();

            await _repository.Commit();
        }
    }
}
