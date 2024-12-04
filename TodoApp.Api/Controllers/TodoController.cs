using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // POST /api/todo
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            var newTodo = await _todoService.CreateTodoAsync(todoItem.Title, todoItem.Description);
            return CreatedAtAction(nameof(GetAllTodos), new { id = newTodo.Id }, newTodo);
        }

        // GET /api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodos()
            => Ok(await _todoService.GetAllTodosAsync());

        // GET /api/todo/pending
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetPendingTodos()
            => Ok(await _todoService.GetPendingTodosAsync());

        // PUT /api/todo/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkTodoAsCompleted(int id)
        {
            var todoItem = await _todoService.MarkTodoAsCompletedAsync(id);
            if (todoItem == null) return NotFound();
            return Ok(todoItem);
        }
    }
}
