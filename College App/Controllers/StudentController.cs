using College_App.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace College_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var studentDTO = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });

            // OK - 200 Success
            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model == null)
                return BadRequest();

            var newId = CollegeRepository.Students.LastOrDefault()!.Id + 1;

            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            CollegeRepository.Students.Add(student);
            
            model.Id = student.Id;

            // Status - 201
            // fetch api GetStudentById
            // Return new student details
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0) return BadRequest();

            var existingStudent = CollegeRepository.Students.FirstOrDefault(s => s.Id == model.Id);

            if (existingStudent == null) return NotFound();

            existingStudent.StudentName = model.StudentName;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.Email;

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0) return BadRequest();

            var existingStudent = CollegeRepository.Students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null) return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (ModelState.IsValid) return BadRequest();

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.Email;

            return NoContent();
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
