using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_API.Data;
using todo_API.Models;

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

        [HttpDelete("{id}")]
        public IActionResult DeleteList(int id)
        {
            var list = _db.Lists.Find(id);
            if (list == null)
            {
                return NotFound("List not found.");
            }

            _db.Lists.Remove(list);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateList(int id, [FromBody] ListDto updatedList)
        {

            var existingList = _db.Lists.Find(id);
            if (existingList == null)
            {
                return NotFound("List not found.");
            }

            existingList.Name = updatedList.Name;
            existingList.Description = updatedList.Description;
            _db.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetList(int id)
        {
            var list = _db.Lists.Include(l => l.Tasks)
                .FirstOrDefault(l => l.Id == id);
            if (list == null)
            {
                return NotFound("List not found.");
            }

            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateList([FromBody] ListDto newList)
        {
            if (newList == null)
            {
                return BadRequest("Invalid list data.");
            }

            var list = new List
            {
                Name = newList.Name,
                Description = newList.Description
            };

            _db.Lists.Add(list);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetAllLists), new { id = list.Id }, list);
        }



    }
}
