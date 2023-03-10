using Microsoft.AspNetCore.Mvc;
using SchoolRegistration.Models;
using SchoolRegistration.Repository.Base;
using System.Xml.Linq;

namespace SchoolRegistration.Controllers
{
    /// <summary>
    /// Student api controller
    /// </summary>
    [ApiController]
    [Route("/api/Student")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _repository;
        private readonly ITeacherStudentRepository _ts_repository;
        private readonly ITeacherRepository _teacher_repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <param name="teacher_repository"></param>
        /// <param name="ts_repository"></param>
        public StudentController(ILogger<StudentController> logger, 
            IStudentRepository repository, 
            ITeacherRepository teacher_repository,
            ITeacherStudentRepository ts_repository)
        {
            _logger = logger;
            _repository = repository;
            _ts_repository = ts_repository;
            _teacher_repository = teacher_repository;
        }

        /// <summary>
        /// Retrive all students
        /// </summary>
        /// <returns>list of all students</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var students = _repository.GetAll();
            if (students != null)
                return Ok(students);
            else
                return NotFound("No results found.");
        }

        /// <summary>
        /// Get all students in star section
        /// </summary>
        /// <returns>List of student in a star section</returns>
        [HttpGet("GetAllStarSectionStudents")]
        public IActionResult GetAllStarSectionStudents()
        {
            var students = _repository.GetStudentsInStarSection();
            if (students != null)
                return Ok(students);
            else
                return NotFound("No results found.");
        }

        /// <summary>
        /// Get all students in a specific Id
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Student record</returns>
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var students = _repository.GetById(id);
            if (students != null)
                return Ok(students);
            else
                return NotFound("No results found.");
        }

        /// <summary>
        /// Get all students with a specific or part of name
        /// </summary>
        /// <param name="name">the name to be search</param>
        /// <returns>Student record</returns>
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var students = _repository.GetByName(name.ToUpper());
            if (students != null)
                return Ok(students);
            else
                return NotFound("No results found.");
        }

        /// <summary>
        /// Update student record
        /// </summary>
        /// <param name="id">Student Id to update</param>
        /// <param name="entity">Student record </param>
        /// <returns>Update student record</returns>
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, Student entity)
        {
            //Change the adviser
            if (_ts_repository.IsMaxStudentPerTeacher(entity.Adviser))
            {
                entity.Adviser = _ts_repository.GetFreeTeacher();
                if (entity.Adviser == 0)
                {
                    return BadRequest("No available teachers.");
                }
            }
            var students = _repository.Update(id, entity);
            if(students == null)
            {
                return NotFound("Student Id does not exists");
            }
            return Ok(students);
        }

        /// <summary>
        /// Add new student record
        /// </summary>
        /// <param name="data">Student to record</param>
        /// <returns>Newly added student record</returns>
        [HttpPost("Create")]
        public IActionResult Add(CreateStudent data)
        {
            if (_ts_repository.IsMaxStudent())
            {
                return BadRequest("Maximum student population reach.");
            }
            Student entity = new Student();

            entity.IsStarSection = _repository.IsQualifiedForStarSection(entity);
            entity.Adviser = _ts_repository.GetFreeTeacher();
            entity.FirstName = data.FirstName;
            entity.LastName = data.LastName;
            entity.Age = data.Age;
            entity.BirthDate = data.BirthDate;
            entity.OldGPA = data.OldGPA;
            entity.StudentIDNumber = _repository.GetLastStudentIdNo();
            
            //Change the adviser
            if (_ts_repository.IsMaxStudentPerTeacher(entity.Adviser))
            {
                entity.Adviser = _ts_repository.GetFreeTeacher();
                if (entity.Adviser == 0)
                {
                    return BadRequest("No available teachers.");
                }
            }
            else
            {
                //Add to star section adviser
                if (entity.IsStarSection)
                {
                    entity.Adviser = _teacher_repository.GetStarSectionAdviserId();
                }
            }

            var students = _repository.Add(entity);
            if (students != null)
            {
                _ts_repository.Add(new TeacherStudent { TeacherId = entity.Adviser, StudentId = students.Id });
                return Ok(students);
            }
            
            return NotFound("Unable to add this student.");
        }

        /// <summary>
        /// Delete student record
        /// </summary>
        /// <param name="id">Student id to delete</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var data = _repository.GetById(id);
            if(data != null) 
            {
                int idData = _repository.Delete(id);
                if (idData > 0)
                {
                    _ts_repository.Delete(_ts_repository.GetTeacherStudentId(data.Adviser, data.Id));
                    return Ok("Student deleted sucessfully.");
                }
            }
            return NotFound("Student Id does not exists");
        }
    }
}