namespace BookStore.Models.Models.Users
{
    public record Purchase
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Book> Books { get; set; } 
        public decimal TotalMoney { get; set; }
    }
}
