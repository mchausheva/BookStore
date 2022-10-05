namespace BookStore.Models.Requests
{
    public class AddBookRequest
    {
        public int Id { get; set; }
        public string Title { get; init; }
        public int AuthorId { get; init; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Price { get; set; }
    }
}
