using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_API.Data;
using todo_API.Models;

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

    }
}
