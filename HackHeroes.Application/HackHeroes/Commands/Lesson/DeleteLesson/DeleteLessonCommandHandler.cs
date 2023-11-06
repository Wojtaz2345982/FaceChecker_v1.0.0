using AutoMapper;
using HackHeroes.Application.HackHeroes.Commands.Lesson.DeleteLesson;
using HackHeroes.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Lesson.DeleteLesson
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public DeleteLessonCommandHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetLessonByEncodedLesson(request.EncodedLesson!);

            if (lesson == null)
            {
                return;
            }

            await _lessonRepository.Delete(lesson);
        }
    }
}
