using AutoMapper;
using HackHeroes.Application.ApplicationUser;
using HackHeroes.Application.HackHeroes;
using HackHeroes.Application.HackHeroes.Commands.Classes.DeleteClass;
using HackHeroes.Application.HackHeroes.Commands.Classes.EditClass;
using HackHeroes.Application.HackHeroes.Commands.Lesson.DeleteLesson;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudent;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudentImage;
using HackHeroes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.Mappings
{
    public class HackHeroesMappingProfile : Profile
    {
        public HackHeroesMappingProfile(IUserContext userContext)
        {
            //Profil mapowania

            var user = userContext.GetCurrentUser();

            CreateMap<Class, ClassDto>()
                .ForMember(c => c.IsEditable, opt => opt
                .MapFrom(src => user != null && src.CreatedById == user!.Id))
                .ReverseMap();

            CreateMap<Lesson, LessonDto>()
                .ForMember(l => l.IsEditable, opt => opt
                .MapFrom(src => user != null && src.CreatedById == user!.Id))
                .ReverseMap();


            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Class, opt => opt.Ignore())
                .ForMember(dto => dto.ImageUrl, opt => opt
                .MapFrom(s => string.Concat("data:image/jpg;base64,", Convert.ToBase64String(s.ImageUrl!, 0, s.ImageUrl!.Length))))
            .ReverseMap();
            CreateMap<ClassDto, DeleteClassCommand>()
            .ReverseMap();

            CreateMap<ClassDto, EditClassCommand>()
                .ReverseMap();

            CreateMap<StudentDto, EditStudentCommand>()
                .ReverseMap();

            CreateMap<StudentDto, EditStudentImageCommand>()
                .ReverseMap();

            CreateMap<DeleteLessonCommand, LessonDto>()
                .ReverseMap();
                
            
            CreateMap<Attendance,AttendanceDto>()              
                .ReverseMap();
        }
    }
}
