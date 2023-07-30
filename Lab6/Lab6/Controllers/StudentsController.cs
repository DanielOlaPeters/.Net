using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.Data;
using Lab6.Models;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Successfully retrieved list of students, so return 200 OK
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error, return 500 Internal Server Error
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Successfully found the student, so return 200 OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Student not found, so return 404 Not Found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error, return 500 Internal Server Error
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(); // Returning 404 Not Found when student is not found
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // Successfully updated the student, so return 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Invalid request data, so return 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Student not found, so return 404 Not Found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error, return 500 Internal Server Error
        public async Task<ActionResult<Student>> PutStudent(Guid id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest(); // Returning 400 Bad Request when IDs don't match
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound(); // Returning 404 Not Found when student is not found
                }
                else
                {
                    throw;
                }
            }

            // Retrieve the updated student from the database
            var updatedStudent = await _context.Students.FindAsync(id);

            return updatedStudent;
        }


        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // Successfully created the student, so return 201 Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Invalid request data, so return 400 Bad Request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error, return 500 Internal Server Error
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // Successfully deleted the student, so return 204 No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Student not found, so return 404 Not Found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Internal server error, return 500 Internal Server Error
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(); // Returning 404 Not Found when student is not found
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
