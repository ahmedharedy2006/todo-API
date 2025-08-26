using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_API.Data;
using todo_API.Models;
using Task = todo_API.Models.Task;

namespace todo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly AppDbContext _db;
        public TaskController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAlTasks()
        {
            var tasks = _db.Tasks
       .Include(t => t.List)
       .Select(t => new
       {
           t.Id,
           t.Title,
           t.Description,
           t.DueDate,
           t.IsCompleted,
           List = new
           {
               t.List.Id,
               t.List.Name,
               t.List.Description
           }
       })
       .ToList();





            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Invalid task data.");
            }

            _db.Tasks.Add(new Task
            {
                Title = newTask.Title,
                Description = newTask.Description,
                DueDate = DateTime.Today,
                IsCompleted = newTask.IsCompleted,
                ListId = newTask.ListId
            });
            await _db.SaveChangesAsync();

            var task = await _db.Tasks.Where(t => t.Title == newTask.Title).FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetAlTasks), task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskDto updatedTask)
        {

            var existingTask = _db.Tasks.Find(id);
            if (existingTask == null)
            {
                return NotFound("Task not found.");
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = DateTime.Today;
            existingTask.IsCompleted = updatedTask.IsCompleted;
            existingTask.ListId = updatedTask.ListId;
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            _db.Tasks.Remove(task);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
