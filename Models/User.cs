namespace ExpenseTracker.Models
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string? UserName { get; set; }
    }
}
