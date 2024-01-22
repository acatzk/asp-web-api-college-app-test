using College_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace College_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            // OK - 200 Success
            return Ok(CollegeRepository.Students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<Student> GetStudentById(int id)
        {
            // Badrequest - 400 - Badrequest - Client error
            if (id <= 0) 
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            // Not Found - 404 - Notfound - Client error
            if (student == null) 
                return NotFound($"The student with id {id} not found");

            // Ok - 200 Success
            return Ok(student);
        }

        [HttpGet]
        [Route("{name}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            // Badrequest - 400 - Badrequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();

            // Not Found - 404 - Notfound - Client error
            if (student == null)
                return NotFound($"The student with name {name} not found");

            // Ok - 200 Success
            return Ok(student);
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudentById(int id) { 
        
              // Badrequest - 400 - Badrequest - Client error
            if (id <= 0) 
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            // Not Found - 404 - Notfound - Client error
            if (student == null) 
                return NotFound($"The student with id {id} not found");

            CollegeRepository.Students.Remove(student!);

            return true;
        }
    }
}
