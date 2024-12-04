using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Infrastructure.Repositories
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext _dbContext;

        public TodoRepository(TodoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync();
            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync() => await _dbContext.TodoItems.ToListAsync();

        public async Task<IEnumerable<TodoItem>> GetPendingAsync() => await _dbContext.TodoItems.Where(t => !t.IsCompleted).ToListAsync();

        public async Task<TodoItem?> GetByIdAsync(int id) => await _dbContext.TodoItems.FindAsync(id);

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _dbContext.TodoItems.Update(todoItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
