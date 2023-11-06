using AutoMapper;
using Flurl.Http;
using HackHeroes.Application.HackHeroes;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudent;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudentImage;
using HackHeroes.Application.HackHeroes.Queries.GetClassById;
using HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonDbObjectByEncodedLesson;
using HackHeroes.Application.HackHeroes.Queries.Student.GetClassStudents;
using HackHeroes.Application.HackHeroes.Queries.Student.GetStudentByStudentKey;
using HackHeroes.Domain.Entities;
using HackHeroes.Domain.Services;
using HackHeroes.Infrastructure.Repositories;
using HackHeroes0._1.Models.postModels;
using HackHeroes0._1.Models.PostResponse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Wangkanai.Detection.Services;
using Wangkanai.Detection.Models;

namespace HackHeroes0._1.Controllers
{
    public class CameraController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IWebHostEnvironment _enivortment;
        private readonly IDetectionService _detectionService;
        public CameraController(IWebHostEnvironment enivortment, IMediator mediator, IMapper mapper, IAttendanceRepository attendanceRepository, IDetectionService detectionService)       
        {
            _enivortment = enivortment;
            _mediator = mediator;
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
            _detectionService = detectionService;
        }

        [Authorize]
        [Route("/Camera/{studentKey}/EditStudentImage")]
        public async Task<IActionResult> EditStudentImage(string studentKey)
        {
            if (_detectionService.Device.Type == Device.Mobile || _detectionService.Device.Type == Device.Tablet)
            {
                return Content("Przepraszamy, ta akcja nie jest dostępna na telefonach.");
            }

            var dto = await _mediator.Send(new GetStudentByStudentKeyQuery() { StudentKey = studentKey });

            var model = _mapper.Map<EditStudentImageCommand>(dto);

            return View(model);
        }


        [Authorize]
        [Route("/Camera/{studentKey}/EditStudentImage")]
        [HttpPost]
        public async Task<IActionResult> EditStudentImage(string name, EditStudentImageCommand command)
        {

            if (_detectionService.Device.Type == Device.Mobile || _detectionService.Device.Type == Device.Tablet)
            {
                return Content("Przepraszamy, ta akcja nie jest dostępna na telefonach.");
            }

            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                         
                            var fileName = file.FileName;

                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            var fileExtension = Path.GetExtension(fileName);

                            var newFileName = string.Concat(myUniqueFileName, fileExtension);

                            // Zamiast zapisywać plik na dysku, wczytujemy go do MemoryStream
                            using var memoryStream = new MemoryStream();
                            await file.CopyToAsync(memoryStream);

                            // Konwersja MemoryStream do tablicy bajtów
                            var imageBytes = memoryStream.ToArray();

                            command.Image = imageBytes;

                        }

                        await _mediator.Send(command);

                        
                    }

                    return RedirectToAction("Index", "HackHeroes");

                }
                else
                {
                   return RedirectToAction("Index", "HackHeroes");

                }
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        [Authorize]
        [Route("FaceChecker/{encodedLesson}/StartLesson")]
        public async Task<IActionResult> StartLesson(string encodedLesson)
        {


            var lesson = await _mediator.Send(new GetLessonDbObjectByEncodedLessonQuery(encodedLesson));
            var dto = _mapper.Map<LessonDto>(lesson);
            dto.Class = await _mediator.Send(new GetClassByIdQuery(lesson.ClassId));
            var students = await _mediator.Send(new GetClassStudentsQuery(dto.Class.EncodedName!));

            dto.Class.Students = students.ToList();

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

      
            var files = HttpContext.Request.Form.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var fileName = file.FileName;

                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        var fileExtension = Path.GetExtension(fileName);

                        var newFileName = string.Concat(myUniqueFileName, fileExtension);


                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);


                        var imageBytes = memoryStream.ToArray();

                        await SendToAiApi(imageBytes, students.ToList(), lesson, _attendanceRepository, false);

                    }
                }
            }



            return Ok();
        }


        [HttpPost]
        [Authorize]
        [Route("FaceChecker/{encodedLesson}/LateStudent")]
        public async Task<IActionResult> LateStudent(string encodedLesson)
        {

            var lesson = await _mediator.Send(new GetLessonDbObjectByEncodedLessonQuery(encodedLesson));
            var dto = _mapper.Map<LessonDto>(lesson);
            dto.Class = await _mediator.Send(new GetClassByIdQuery(lesson.ClassId));
            var students = await _mediator.Send(new GetClassStudentsQuery(dto.Class.EncodedName!));

            dto.Class.Students = students.ToList();

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            var files = HttpContext.Request.Form.Files;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var fileName = file.FileName;

                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        var fileExtension = Path.GetExtension(fileName);

                        var newFileName = string.Concat(myUniqueFileName, fileExtension);


                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);


                        var imageBytes = memoryStream.ToArray();

                        await SendToAiApi(imageBytes, students.ToList(), lesson, _attendanceRepository, true);

                    }
                }
            }



            return Ok();
        }


        //Zapytanie do API
        private async Task SendToAiApi(byte[] imageBytes, List<Student> studentsList, Lesson lesson, IAttendanceRepository repository, bool isLate)
        {

            await Console.Out.WriteLineAsync("Wyslano zapytanie");

            string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
            //Console.WriteLine(base64String);

            var request = new ApiRequest()
            {
                klasa = lesson.Class!.EncodedName!,
                image = base64String
            };

         

            await Console.Out.WriteLineAsync(request.klasa);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(request));

            var postResult = await "Adres serwera API"
                 .WithHeaders(new
                 {
                     Content_Type = "application/json"
                 }, true)
                .PostJsonAsync(request);





            if (postResult != null)
            {
                var text = await postResult.ResponseMessage.Content.ReadAsStringAsync();
                FacesRecognized response = JsonConvert.DeserializeObject<FacesRecognized>(text)!;

                await Console.Out.WriteLineAsync(text);

                if (!string.IsNullOrEmpty(response.unrecognized_faces))
                {
                    var url = response.unrecognized_faces;

                    var img = string.Concat("data:image/jpg;base64,", url);


                    HttpContext.Session.SetString("UnrecognizedFaces", img);
                }

                if(response.recognized_faces != null)
                {
                    if(!isLate)
                    {
                        foreach (var face in response.recognized_faces)
                        {
                            foreach (var student in studentsList)
                            {
                                if (student.StudentKey == face)
                                {
                                    await repository.ChangeAttendance(student.Id, lesson.Id, true);
                                }
                            }
                        }

                    }else
                    {
                        foreach (var face in response.recognized_faces)
                        {
                            foreach (var student in studentsList)
                            {
                                if (student.StudentKey == face)
                                {
                                    await repository.ChangeAttendanceIsLate(student.Id, lesson.Id, true);
                                }
                            }
                        }
                    }

                }



            }


        }

       
    }
}
