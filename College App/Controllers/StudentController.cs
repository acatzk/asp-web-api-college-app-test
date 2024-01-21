using College_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace College_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return new List<Student>(){
                new Student
                {
                    Id = 1,
                    StudentName = "Student 1",
                    Email = "student1@gmail.com",
                    Address = "Address 1 frrom stduent"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Student 2",
                    Email = "student2@gmail.com",
                    Address = "Address 2 frrom stduent"
                }
            };
        }
    }
}
