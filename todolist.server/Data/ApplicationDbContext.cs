﻿using Microsoft.EntityFrameworkCore;
using TodoList.Server.Entities;

namespace TodoList.Server.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public virtual DbSet<TodoItem> TodoItems { get; set; }
	}
}
