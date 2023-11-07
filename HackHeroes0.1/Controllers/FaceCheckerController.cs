using AutoMapper;
using HackHeroes.Application.HackHeroes.Commands.Classes.CreateClass;
using HackHeroes.Application.HackHeroes.Commands.Classes.DeleteClass;
using HackHeroes.Application.HackHeroes.Commands.Classes.EditClass;
using HackHeroes.Application.HackHeroes.Queries.GetAllClasses;
using HackHeroes.Application.HackHeroes.Queries.GetClassByEncodedName;
using HackHeroes.Application.HackHeroes.Queries.Student;
using HackHeroes.Domain.Entities;
using HackHeroes0._1.Models;
using HackHeroes.Application.HackHeroes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HackHeroes.Application.HackHeroes.Commands.Students;
using HackHeroes.Application.HackHeroes.Commands.Students.DeleteStudent;
using HackHeroes.Application.HackHeroes.Queries.Student.GetStudentByStudentKey;
using HackHeroes.Application.HackHeroes.Commands.Students.EditStudent;
using HackHeroes0._1.Extensions;
using HackHeroes.Application.HackHeroes.Lesson.CreateLesson;
using HackHeroes0._1.Models.ViewModels;
using HackHeroes.Application.HackHeroes.Queries.Lesson.GetAllLesson;
using HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonByEncodedLesson;
using HackHeroes.Application.HackHeroes.Queries.GetClassById;
using HackHeroes.Application.HackHeroes.Queries.Lesson.GetLessonDbObjectByEncodedLesson;
using HackHeroes.Application.HackHeroes.Queries.Student.GetClassStudents;
using HackHeroes.Domain.Services;
using HackHeroes0._1.Models.postModels;
using Flurl.Http;
using HackHeroes.Application.HackHeroes.Commands.Lesson.DeleteLesson;
using HackHeroes0._1.Models.GetModels;
using Newtonsoft.Json;
using Wangkanai.Detection.Services;
using Wangkanai.Detection.Models;

namespace HackHeroes0._1.Controllers
{
    public class FaceCheckerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IDetectionService _detectionService;

        public FaceCheckerController(IMediator mediator,IMapper mapper, IAttendanceRepository attendanceRepository, IDetectionService detectionService) 
        {
            _mediator = mediator;
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
            _detectionService = detectionService; 
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ClassIndex()
        {
            var classes = await _mediator.Send(new GetAllClassesQuery());
            return View(classes);
        }


        [Authorize]
        [Route("FaceChecker/{encodedName}/StudentsIndex")]
        public async Task<IActionResult> StudentsIndex(string encodedName)
        {
            var students = await _mediator.Send(new GetAllStudentsQuery(encodedName));

            return View(students);
        }


        [Authorize]
        public ActionResult Create()
        {           
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateClassCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Utworzono klasę: {command.Name}");

            return RedirectToAction("ClassIndex","FaceChecker");
        }


        [Authorize]
        [Route("FaceChecker/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(string encodedName)
        {

            var dto = await _mediator.Send(new GetClassByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            DeleteClassCommand model = _mapper.Map<DeleteClassCommand>(dto);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("FaceChecker/{encodedName}/Delete")]
        public async Task<IActionResult> Delete(string encodedName, DeleteClassCommand command)
        {


            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Klasa została usunięta");

            return RedirectToAction("ClassIndex", "FaceChecker");
        }

        [Authorize]
        [Route("FaceChecker/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetClassByEncodedNameQuery(encodedName));
            var students = await _mediator.Send(new GetClassStudentsQuery(encodedName));
            dto.Students = students.ToList();

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            return View(dto);
        }

        [Authorize]
        [Route("FaceChecker/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetClassByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditClassCommand model = _mapper.Map<EditClassCommand>(dto);            //TO DO ACCESSY


            return View(model);
        }


        [Authorize]
        [HttpPost]
        [Route("FaceChecker/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditClassCommand command)
        {

            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            return RedirectToAction("ClassIndex", "FaceChecker"); //TODO: Refactor
        }

        [Authorize]
        [Route("FaceChecker/{studentKey}/EditStudent")]
        public async Task<IActionResult> EditStudent(string studentKey)
        {
            var dto = await _mediator.Send(new GetStudentByStudentKeyQuery() {StudentKey = studentKey});

            var model = _mapper.Map<EditStudentCommand>(dto);

            return View(model);
        }
     

        [Authorize]
        [HttpPost]
        [Route("FaceChecker/{studentKey}/EditStudent")]
        public async Task<IActionResult> EditStudent(string studentKey, EditStudentCommand command)
        {
           
            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            return RedirectToAction("ClassIndex", "FaceChecker"); 
        }


        [Authorize]
        [HttpPost]
        [Route("FaceChecker/Student")]
        public async Task<IActionResult> AddStudent(AddStudentCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            await _mediator.Send(command);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("FaceChecker/{studentKey}/Student")]
        public async Task<IActionResult> DeleteStudent(string studentKey,DeleteStudentCommand command)
        {
            command.StudentKey = studentKey;
                   
            await _mediator.Send(command);

            return Ok();
        }


        [Authorize]
        [HttpGet]
        [Route("FaceChecker/{encodedName}/Student")]
        public async Task<IActionResult> GetAllStudents(string encodedName)
        {
             var data = await _mediator.Send(new GetAllStudentsQuery(encodedName));

            return Ok(data);
        }


        [Authorize]
        public async Task<IActionResult> LessonsIndex()
        {
            var lessons = await _mediator.Send(new GetAllLessonsQuery());
            return View(lessons);
        }

        [Authorize]
        public async Task<IActionResult> CreateLesson()
        {
            var viewModel = new CreateLessonViewModel
            {
                LessonCommand = new CreateLessonCommand(),
                AvailableClasses = await _mediator.Send(new GetAllClassesQuery())
            };

         
            return View(viewModel);
            
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonViewModel command1)
        {
            CreateLessonCommand command = new CreateLessonCommand()
            {
                ClassEncodedName = command1.LessonCommand.ClassEncodedName,
                Topic = command1.LessonCommand.Topic
            };
            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Utworzono lekcję: {command.Topic}");

            return RedirectToAction("LessonsIndex", "FaceChecker");
        }

        [Authorize]
        [Route("FaceChecker/{encodedLesson}/DeleteLesson")]
        public async Task<IActionResult> DeleteLesson(string encodedLesson)
        {

            var dto = await _mediator.Send(new GetLessonByEncodedLessonQuery(encodedLesson));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            DeleteLessonCommand model = _mapper.Map<DeleteLessonCommand>(dto);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("FaceChecker/{encodedLesson}/DeleteLesson")]
        public async Task<IActionResult> DeleteLesson(DeleteLessonCommand command)
        {


            if (!ModelState.IsValid)
            {
                return View(command);

            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Lekcja została usunięta");

            return RedirectToAction("LessonsIndex", "FaceChecker");
        }

        [Authorize]
        [Route("FaceChecker/{encodedLesson}/PrepereLesson")]
        public async Task<IActionResult> PrepereLesson(string encodedLesson)
        {

            if (_detectionService.Device.Type == Device.Mobile || _detectionService.Device.Type == Device.Tablet)
            {
                return Content("Przepraszamy, ta akcja nie jest dostępna na telefonach.");
            }

            var lesson = await _mediator.Send(new GetLessonDbObjectByEncodedLessonQuery(encodedLesson));
            var dto = _mapper.Map<LessonDto>(lesson);
            dto.Class = await _mediator.Send(new GetClassByIdQuery(lesson.ClassId));
            var students = await _mediator.Send(new GetClassStudentsQuery(dto.Class.EncodedName!));

            dto.Class.Students = students.ToList();

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            var model = new ClassRequest(dto.Class.EncodedName!);
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(model).ToString());

            var postResult = await "Adres do API"
                .WithHeaders(new
                {
                    Content_Type = "application/json",
                }, true)
                .PostJsonAsync(model);



            await Console.Out.WriteLineAsync(await postResult.ResponseMessage.Content.ReadAsStringAsync());

            return View(dto);
        }

        [Authorize]
        [Route("FaceChecker/{encodedLesson}/StartLesson")]
        public async Task<IActionResult> StartLesson(string encodedLesson)
        {

            if (_detectionService.Device.Type == Device.Mobile || _detectionService.Device.Type == Device.Tablet)
            {
                return Content("Przepraszamy, ta akcja nie jest dostępna na telefonach.");
            }

            var lesson = await _mediator.Send(new GetLessonDbObjectByEncodedLessonQuery(encodedLesson));

            if (lesson.WasStarted)
            {
                return RedirectToAction("LessonsIndex", "FaceChecker");
            }

            var dto = _mapper.Map<LessonDto>(lesson);
            dto.Class = await _mediator.Send(new GetClassByIdQuery(lesson.ClassId));
            var students = await _mediator.Send(new GetClassStudentsQuery(dto.Class.EncodedName!));
            lesson.WasStarted = true;
            dto.Class.Students = students.ToList();

            await _attendanceRepository.Commit();

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            var attendance = new List<AttendanceDto>();

          if(students.ToList().Count > 0)
          {

                foreach (var student in dto.Class.Students)
                {
                    var att = new AttendanceDto()
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        StudentKey = student.StudentKey,
                        StudentId = student.Id

                    };
                    attendance.Add(att);
                }

                foreach (var attend in attendance)
                {
                    var attendanceModel = new Attendance()
                    {
                        LessonId = lesson.Id,
                        StudentId = attend.StudentId,
                        StudentKey = attend.StudentKey

                    };

                    await _attendanceRepository.Create(attendanceModel);
                }
            }


           
                                                     
            var model = new StartLessonViewModel()
            {
                Lesson = dto,
                Attendance = attendance
            };


            return View(model);
        }

        [Authorize]
        [Route("FaceChecker/{encodedLesson}/DetailsLesson")]
        public async Task<IActionResult> DetailsLesson(string encodedLesson)
        {
            var lesson = await _mediator.Send(new GetLessonDbObjectByEncodedLessonQuery(encodedLesson));

            var dto = _mapper.Map<LessonDto>(lesson);

            dto.Class = await _mediator.Send(new GetClassByIdQuery(lesson.ClassId));

            var attendances = await _attendanceRepository.GetLessonAttendance(lesson);

            var attendancesDto = _mapper.Map<IEnumerable<AttendanceDto>>(attendances);

            foreach (var attendance in attendancesDto)
            {
                var student = await _mediator.Send(new GetStudentByStudentKeyQuery() { StudentKey = attendance.StudentKey });
                if(student == null)
                {
                    break;
                }
                attendance.FirstName =  student.FirstName;
                attendance.LastName = student.LastName;
            }

            dto.AttendaceAtTheLesson = attendancesDto.ToList();


            var students = await _mediator.Send(new GetClassStudentsQuery(dto.Class.EncodedName!));

            dto.Class.Students = students.ToList();

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            return View(dto);
        }


        [HttpGet]
        [Route("FaceChecker/{encodedLesson}/GetStudentAttendance")]
        public async Task<IActionResult> GetStudentAttendance(string encodedLesson)
        {
            
            var lesson = await _mediator.Send(new GetLessonDbObjectByEncodedLessonQuery(encodedLesson));

            var attendanceList = await _attendanceRepository.GetLessonAttendance(lesson);

            return Ok(attendanceList);
        }

        [HttpGet]
        [Route("FaceChecker/GetUnrecognizedFaces")]
        public async Task<IActionResult> GetUnrecognizedFaces()
        {
            // Odczytanie wartości z sesji
            var unrecognizedFaceUrl = HttpContext.Session.GetString("UnrecognizedFaces");

            if (string.IsNullOrEmpty(unrecognizedFaceUrl) || unrecognizedFaceUrl.Length < 40)
            {
                return Ok(new IntruderModel(""));
            }

            IntruderModel intruderModel = new IntruderModel(unrecognizedFaceUrl);

            HttpContext.Session.SetString("UnrecognizedFaces", "");

            return Ok(intruderModel);

        }


        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
