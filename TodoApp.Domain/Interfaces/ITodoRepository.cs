using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<IEnumerable<TodoItem>> GetPendingAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task UpdateAsync(TodoItem todoItem);
    }
}
