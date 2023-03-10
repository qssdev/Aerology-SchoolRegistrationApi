using Microsoft.AspNetCore.Mvc;
using SchoolRegistration.Models;
using SchoolRegistration.Repository.Base;
using System.Xml.Linq;

namespace SchoolRegistration.Controllers
{
    /// <summary>
    /// Teacher api controller
    /// </summary>
    [ApiController]
    [Route("/api/Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly ITeacherRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public TeacherController(ILogger<StudentController> logger, ITeacherRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Retrive all teacher
        /// </summary>
        /// <returns>list of all teacher</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var teacher = _repository.GetAll();
            if (teacher != null)
                return Ok(teacher);
            else
                return NotFound("No results found.");
        }

        /// <summary>
        /// Get all teacher in a specific Id
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Student record</returns>
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var teacher = _repository.GetById(id);
            if (teacher != null)
                return Ok(teacher);
            else
                return NotFound("No results found.");
        }

        /// <summary>
        /// Get all teacher with a specific or part of name
        /// </summary>
        /// <param name="name">the name to be search</param>
        /// <returns>Student record</returns>
        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var teacher = _repository.GetByName(name.ToUpper());
            if (teacher != null)
                return Ok(teacher);
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
        public IActionResult Update(int id, Teacher entity)
        {
            var teacher = _repository.Update(id, entity);
            if (teacher == null)
            {
                return NotFound("Student Id does not exists");
            }
            return Ok(teacher);
        }

        /// <summary>
        /// Add new student record
        /// </summary>
        /// <param name="data">Student to record</param>
        /// <returns>Newly added student record</returns>
        [HttpPost("Create")]
        public IActionResult Add(CreateTeacher data)
        {
            if (_repository.MaxTeacherCount())
            {
                return BadRequest("Maximum teacher population reach.");
            }
            
            Teacher entity = new Teacher();
            entity.FirstName = data.FirstName;
            entity.LastName = data.LastName;
            entity.Age = data.Age;
            entity.BirthDate = data.BirthDate;
            entity.IsStarSectionAdviser = data.IsStarSectionAdviser;
            entity.TeacherIDNumber = _repository.GetLastTeacherIdNo();

            var teacher = _repository.Add(entity);
            if (teacher == null)
            {
                return NotFound("Unable to add this student.");
            }
            return Ok(teacher);
        }
    }
}