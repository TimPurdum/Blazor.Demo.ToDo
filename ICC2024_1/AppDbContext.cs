using ICC2024_1.Client;
using System;
using Microsoft.EntityFrameworkCore;

namespace ICC2024_1;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<ToDoItem> ToDoItems { get; set; }
}
