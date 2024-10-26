using ICC2024_1;
using ICC2024_1.Client;
using ICC2024_1.Client.Pages;
using ICC2024_1.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepository, ServerRepository>();

var app = builder.Build();

app.Services
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<AppDbContext>()
    .Database
    .EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapPost("/api/ToDoItems", async (ToDoItem toDoItem, IRepository repo) =>
{
    int id = await repo.AddAsync(toDoItem);
    return Results.Text(id.ToString());
});

app.MapDelete("/api/ToDoItems/{id}", async (int id, IRepository repo) =>
{
    await repo.DeleteAsync<ToDoItem>(id);
    return Results.NoContent();
});

app.MapGet("/api/ToDoItems", async (IRepository repo) =>
{
    return await repo.GetAllAsync<ToDoItem>();
});

app.MapGet("/api/ToDoItems/{id}", async (int id, IRepository repo) =>
{
    return await repo.GetAsync<ToDoItem>(id);
});

app.MapPut("/api/ToDoItems/{id}", async (int id, ToDoItem toDoItem, IRepository repo) =>
{
    if (id != toDoItem.Id)
    {
        return Results.BadRequest();
    }
    await repo.UpdateAsync(toDoItem);
    return Results.NoContent();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ICC2024_1.Client._Imports).Assembly);

app.Run();
