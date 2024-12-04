using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItem> CreateTodoAsync(string title, string? description)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty");

            var todoItem = new TodoItem
            {
                Title = title,
                Description = description
            };

            return await _todoRepository.CreateAsync(todoItem);
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync() => await _todoRepository.GetAllAsync();

        public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync() => await _todoRepository.GetPendingAsync();

        public async Task<TodoItem?> MarkTodoAsCompletedAsync(int id)
        {
            var todoItem = await _todoRepository.GetByIdAsync(id);
            if (todoItem == null) return null;

            todoItem.IsCompleted = true;
            await _todoRepository.UpdateAsync(todoItem);

            return todoItem;
        }
    }
}
