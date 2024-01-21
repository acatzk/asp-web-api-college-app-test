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
        public IEnumerable<Student> GetStudents()
        {
            return CollegeRepository.Students;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        [HttpGet]
        [Route("{name}", Name = "GetStudentByName")]
        public Student GetStudentByName(string name)
        {
            return CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteStudentById")]
        public bool DeleteStudentById(int id)
        {
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            CollegeRepository.Students.Remove(student);

            return true;
        }
    }
}
