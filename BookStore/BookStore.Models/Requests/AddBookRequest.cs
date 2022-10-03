namespace BookStore.Models.Requests
{
    public class AddBookRequest
    {
        public string Title { get; init; }
        public int AuthorId { get; init; }
    }
}
