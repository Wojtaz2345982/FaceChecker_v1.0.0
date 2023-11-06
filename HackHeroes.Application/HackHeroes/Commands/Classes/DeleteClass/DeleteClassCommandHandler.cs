using AutoMapper;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Classes.DeleteClass
{
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand>
    {
        private readonly IMapper _mapper;
        private readonly IHackHeroesRepository _repository;
        private readonly IStudentsRepository _studentsRepository;

        public DeleteClassCommandHandler(IMapper mapper, IHackHeroesRepository repository, IStudentsRepository studentsRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _studentsRepository = studentsRepository;
        }
        public async Task Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _repository.GetClassbyEncodedName(request.EncodedName!);


            if (@class == null)
            {
                return;
            }

            await _studentsRepository.DeleteStudentsFromClass(@class.EncodedName);

            await _repository.Delete(@class);

        }
    }
}
