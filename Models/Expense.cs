using ExpenseTracker.Models;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public string ExpenseId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; } = string.Empty;
        public decimal AmountDue { get; set; }
        public DateTime? DueDate { get; set; } = null; // Nullable to allow for expenses without a due date
        public decimal AmountPaid { get; set; } = 0.0m;
        public DateTime? PaidDate { get; set; } = null; // Nullable to allow for unpaid expenses
        public string Category { get; set; } = string.Empty;
        // Foreign key for User
        public string UserId { get; set; } = string.Empty;
        // Navigation property for User
        public User User { get; set; } = null!;
    }
}
