using AutoMapper;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Lesson.CreateLesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand>
    {
         
        private readonly IMapper _mapper;
        private readonly ILessonRepository _repository;
        private readonly IUserContext _userContext;
        private readonly IHackHeroesRepository _classRepository;

        public CreateLessonCommandHandler(IMapper mapper, ILessonRepository repository, IUserContext userContext,IHackHeroesRepository classRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _userContext = userContext;
            _classRepository = classRepository;
        }

        public async Task Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var @class = await _classRepository.GetClassbyEncodedName(request.ClassEncodedName!);
             var lesson = _mapper.Map<Domain.Entities.Lesson>(request);
            lesson.Date = DateTime.Now;
            lesson.EncodeLesson();
            lesson.ClassId = @class.Id;
            lesson.CreatedById = _userContext.GetCurrentUser()!.Id;
            

            await _repository.Create(lesson);
        }
    }
}
