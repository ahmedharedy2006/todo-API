using Microsoft.AspNetCore.Mvc;
using todo_API.Data;

namespace todo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : Controller
    {
        private readonly AppDbContext _db;
        public ListController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAllLists()
        {
            var lists = _db.Lists.ToList();

            if (lists == null || !lists.Any())
            {
                return NotFound("No lists found.");
            }

            return Ok(lists);
        }
    }
}
