﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Domain.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
