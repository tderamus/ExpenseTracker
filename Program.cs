using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNpgsql<ExpenseTrackerDbContext>(builder.Configuration["ExpenseTrackerDbConnectionString"]);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Create New User
app.MapPost("/users", async (ExpenseTrackerDbContext db, User user) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.UserId}", user);
});

// Create New Expense
app.MapPost("/expenses", async (ExpenseTrackerDbContext db, Expense expense) =>
{
    db.Expenses.Add(expense);
    await db.SaveChangesAsync();
    return Results.Created($"/expenses/{expense.ExpenseId}", expense);
});

// Get All Users
app.MapGet("/users", async (ExpenseTrackerDbContext db) =>
{
    var users = await db.Users.ToListAsync();
    if (users.Count == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(users);
});

// Get User by ID
app.MapGet("/users/{userId}", async (ExpenseTrackerDbContext db, string userId) =>
{
    var user = await db.Users.FindAsync(userId);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

// Get All Expenses
app.MapGet("/expenses", async (ExpenseTrackerDbContext db) =>
{
    var expenses = await db.Expenses.ToListAsync();
    if (expenses.Count == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(expenses);
});

// Get Expense by ID
app.MapGet("/expenses/{expenseId}", async (ExpenseTrackerDbContext db, string expenseId) =>
{
    var expense = await db.Expenses.FindAsync(expenseId);
    if (expense == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(expense);
});

// Get All Expenses for a User
app.MapGet("/users/{userId}/expenses", async (ExpenseTrackerDbContext db, string userId) =>
{
    var expenses = await db.Expenses
        .Where(e => e.UserId == userId)
        .ToListAsync();
    if (expenses.Count == 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(expenses);
});

// Update User by ID
app.MapPut("/users/{userId}", async (ExpenseTrackerDbContext db, string userId, User user) =>
{
    if (userId != user.UserId)
    {
        return Results.BadRequest("User ID mismatch");
    }
    db.Entry(user).State = EntityState.Modified;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

