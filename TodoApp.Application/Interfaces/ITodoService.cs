using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoService
    {
        Task<TodoItem> CreateTodoAsync(string title, string? description);
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
        Task<TodoItem?> MarkTodoAsCompletedAsync(int id);
    }
}
