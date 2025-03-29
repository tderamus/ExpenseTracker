using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

public class ExpenseTrackerDbContext : DbContext
{
    public DbContextOptions<ExpenseTrackerDbContext> Options { get; }
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> context) : base(context)
    {
    }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Auto generate the primary key for User
        modelBuilder.Entity<User>()
            .Property(u => u.UserId)
            .ValueGeneratedOnAdd();

        // Auto generate the primary key for Expense
        modelBuilder.Entity<Expense>()
            .Property(e => e.ExpenseId)
            .ValueGeneratedOnAdd();

        // Define the relationship between Expense and User
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}
