using Microsoft.AspNetCore.Mvc;
using MogoDB.Models;
using MogoDB.Services;

namespace MogoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService studentService;

        public EmployeeController(IEmployeeService studentService)
        {
            this.studentService = studentService;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            return studentService.Get();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(string id)
        {
            var student = studentService.Get(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            return student;
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee student)
        {
            studentService.Create(student);

            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Employee student)
        {
            var existingStudent = studentService.Get(id);

            if (existingStudent == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            studentService.Update(id, student);

            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var student = studentService.Get(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            studentService.Remove(student.Id);

            return Ok($"Student with Id = {id} deleted");
        }
    }
}
