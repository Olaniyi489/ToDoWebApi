using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly ToDoAPIDbContext dbContext;

        public ToDoController(ToDoAPIDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDo()
        {
            return Ok(await dbContext.Todos.ToListAsync());

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetATask([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if(todo == null)
            {
                return NotFound("Invalid Id!");
            }

            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddToDoRequest addToDoRequest)
        {
            var todo = new ToDo()
            {
                Id = Guid.NewGuid(),
                Task = addToDoRequest.Task,
                Priority = addToDoRequest.Priority,
                Category = addToDoRequest.Category
            };

           await dbContext.Todos.AddAsync(todo);
           await dbContext.SaveChangesAsync();

            return Ok(todo); 
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateToDo([FromRoute] Guid id, UpdateToDoRequest updateToDoRequest)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if(todo != null)
            {
                todo.Task = updateToDoRequest.Task;
                todo.Priority = updateToDoRequest.Priority;
                todo.Category = updateToDoRequest.Category;
                
                await dbContext.SaveChangesAsync();

                return Ok(todo);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if( todo != null)
            {
                dbContext.Remove(todo);
                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }

            return NotFound("Task does not exist");
        }
    }
}
