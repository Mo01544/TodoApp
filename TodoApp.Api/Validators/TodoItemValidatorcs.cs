using FluentValidation;
using TodoApp.Domain.Entities;

namespace TodoApp.Api.Validators
{
    public class TodoItemValidator : AbstractValidator<TodoItem>
    {
        public TodoItemValidator()
        {
            RuleFor(todo => todo.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");


            RuleFor(todo => todo.IsCompleted)
                .Must(isCompleted => isCompleted == false || isCompleted == true)
                .WithMessage("IsCompleted should be true or false.");


            RuleFor(todo => todo.CreatedDate)
                .NotEmpty().WithMessage("CreatedDate is required.")
                .Must(date => date <= DateTime.Now).WithMessage("CreatedDate cannot be in the future.");
        }
    }
}
