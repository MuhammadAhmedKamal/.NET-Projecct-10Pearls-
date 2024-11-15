using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_website.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Backend_website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public EmployeeTasksController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTasks>>> GetEmployeeTasks()
        {
            return await _context.EmployeeTasks.ToListAsync();
        }

        // GET: api/EmployeeTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTasks>> GetEmployeeTask(int id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);

            if (employeeTask == null)
            {
                return NotFound();
            }

            return employeeTask;
        }

        // POST: api/EmployeeTasks
        [HttpPost]
        public async Task<ActionResult<EmployeeTasks>> CreateEmployeeTask(EmployeeTasks employeeTask)
        {
            if (employeeTask == null || string.IsNullOrWhiteSpace(employeeTask.Tasks) || string.IsNullOrWhiteSpace(employeeTask.TasksDescription))
            {
                return BadRequest("Task name and description cannot be empty.");
            }

            _context.EmployeeTasks.Add(employeeTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeTask), new { id = employeeTask.Id }, employeeTask);
        }

        // PUT: api/EmployeeTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeTask(int id, EmployeeTasks employeeTask)
        {
            if (id != employeeTask.Id)
            {
                return BadRequest("Task ID mismatch.");
            }

            if (employeeTask == null || string.IsNullOrWhiteSpace(employeeTask.Tasks) || string.IsNullOrWhiteSpace(employeeTask.TasksDescription))
            {
                return BadRequest("Task name and description cannot be empty.");
            }

            _context.Entry(employeeTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/EmployeeTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTask(int id)
        {
            var employeeTask = await _context.EmployeeTasks.FindAsync(id);
            if (employeeTask == null)
            {
                return NotFound();
            }

            _context.EmployeeTasks.Remove(employeeTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeTaskExists(int id)
        {
            return _context.EmployeeTasks.Any(e => e.Id == id);
        }
    }
}


