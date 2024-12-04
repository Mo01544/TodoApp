using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Interfaces;
using TodoApp.Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<TodoItemValidator>();

// Register InMemory Database with DbContext
builder.Services.AddDbContext<TodoAppDbContext>(options =>
    options.UseInMemoryDatabase("TodoDb"));

// Register application services and repositories
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

// Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers for the Web API
builder.Services.AddControllers()
    .AddFluentValidation();  // Add FluentValidation to MVC

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
