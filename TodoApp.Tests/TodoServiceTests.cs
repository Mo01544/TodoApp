using Moq;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;
using Xunit;

namespace TodoApp.Tests
{
    public class TodoServiceTests
    {
        private readonly ITodoService _todoService;
        private readonly Mock<ITodoRepository> _todoRepositoryMock;

        public TodoServiceTests()
        {
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _todoService = new TodoService(_todoRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateTodoAsync_ShouldThrowException_WhenTitleIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _todoService.CreateTodoAsync("", "Test description"));
        }

        [Fact]
        public async Task CreateTodoAsync_ShouldReturnTodo_WhenTitleIsValid()
        {
            var todoItem = new TodoItem { Title = "Test Todo" };
            _todoRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<TodoItem>())).ReturnsAsync(todoItem);

            var result = await _todoService.CreateTodoAsync("Test Todo", null);

            Assert.Equal("Test Todo", result.Title);
            _todoRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<TodoItem>()), Times.Once);
        }
    }
}
