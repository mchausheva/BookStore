namespace BookStore.Models.Requests
{
    public class UpdateBookRequest
    {
        public int Id { get; set; }
        public string Title { get; init; }
        public int AuthorId { get; init; }
    }
}
